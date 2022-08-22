Imports System.IO
Imports System.Web.Mvc
Imports LMS.ViewModels

Namespace Controllers
    Public Class HistoryController
        Inherits Controller

        Private objLeaves As b_Leaves
        Private intfB_Leaves As b_Leaves_Interface

        ' GET: History
        Function Index() As ActionResult
            If Session.Count = 0 Then
                Session.Clear()
                Session.Abandon()
                ViewBag.LogInUserId = Nothing
                Return RedirectToAction("Index", "Home")
            Else
                ViewBag.LogInUserId += String.Format("(Welcome {0}!", Session(strUserName))

                If Not IsNothing(TempData("Message")) Then
                    ModelState.AddModelError("Error", TempData("Message"))
                    TempData("Message") = Nothing
                End If

                Dim leaveInfo As New HistoryViewModel

                If Session(strAdminUser) = "Y" Then
                    leaveInfo.ISADMIN = True
                Else
                    leaveInfo.ISADMIN = False
                End If

                Return View("History_List", leaveInfo)
            End If
        End Function

        ' GET: History/Create
        Function Create() As ActionResult

            GenerateDropDownLists()

            Dim leaveInfo As New HistoryViewModel
            leaveInfo.USER_ID = Session(strUserId)
            leaveInfo.CURRENT_USER_ID = leaveInfo.USER_ID

            If Session(strAdminUser) = "Y" Then
                leaveInfo.ISADMIN = True
            Else
                leaveInfo.ISADMIN = False
            End If

            Return View("History_Details", leaveInfo)

        End Function

        ' GET: History/Edit/5
        Function Edit(ByVal id As Integer) As ActionResult

            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

            Dim dsLeaves = Nothing
            Dim currentUserId = Session(strUserId)
            Dim strMsg = Nothing

            Try
                dsLeaves = New DataSet

                dsLeaves = GetLeavesHistoryData(id)

                If dsLeaves.Tables.Count > 0 And dsLeaves.Tables(0).Rows.Count > 0 Then
                    Dim leaveInfo As New HistoryViewModel

                    For Each dr As DataRow In dsLeaves.Tables(0).Rows
                        leaveInfo.HIDDEN_ID = dr("ID").ToString()
                        leaveInfo.USER_ID = dr("USER_ID").ToString()
                        leaveInfo.HIDDEN_USER_ID = leaveInfo.USER_ID
                        leaveInfo.LEAVE_TYPE = dr("LEAVE_TYPE").ToString()
                        leaveInfo.LEAVE_FROM = CDate(dr("LEAVE_FROM").ToString()).ToString("dd-MM-yyyy")
                        leaveInfo.LEAVE_FROM_AMPM = dr("LEAVE_FROM_AMPM").ToString()
                        leaveInfo.LEAVE_TO = CDate(dr("LEAVE_TO").ToString()).ToString("dd-MM-yyyy")
                        leaveInfo.LEAVE_TO_AMPM = dr("LEAVE_TO_AMPM").ToString()
                        leaveInfo.TOTAL_TAKEN_LEAVE = dr("TOTAL_TAKEN_LEAVE").ToString()
                        leaveInfo.HIDDEN_TOTAL_LEAVE_TAKEN = leaveInfo.TOTAL_TAKEN_LEAVE
                        leaveInfo.REMARKS = dr("REMARKS").ToString()
                        leaveInfo.STATUS = dr("STATUS").ToString()
                        leaveInfo.HIDDEN_STATUS = leaveInfo.STATUS
                        leaveInfo.HIDDEN_DOCUMENT = dr("DOCUMENTS").ToString()
                        leaveInfo.HIDDEN_APPROVED_BY = dr("APPROVED_BY").ToString()

                        ViewBag.Message += GetUploadedFileLink(leaveInfo.USER_ID, leaveInfo.HIDDEN_DOCUMENT)
                    Next

                    If Session(strAdminUser) = "Y" Then
                        leaveInfo.ISADMIN = True
                    Else
                        leaveInfo.ISADMIN = False
                    End If

                    If Not IsNothing(TempData("Message")) Then
                        ModelState.AddModelError("Info", TempData("Message"))
                        TempData("Message") = Nothing
                    End If

                    If leaveInfo.ISADMIN = False And leaveInfo.USER_ID <> currentUserId Then
                        TempData("Message") = "You're not allowed to view others leave deails."
                        Return RedirectToAction("Index", "History")
                    Else
                        GenerateDropDownLists()

                        Return View("History_Details", leaveInfo)
                    End If

                End If

            Catch ex As Exception

            End Try

        End Function

        ' POST: History/Edit/5
        <HttpPost()>
        Function Edit(ByVal leaveInfo As HistoryViewModel, ByVal submitButton As String) As ActionResult
            Try
                Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

                Dim isEdit As Boolean = False
                Dim errMsg = Nothing
                Dim tmpErrMsg = Nothing
                Dim blnHas_Send As Boolean = False
                Dim intRetCode = Nothing

                Try
                    Select Case submitButton
                        Case "Submit"
                            Save(leaveInfo, isEdit, errMsg)

                        Case "Cancel Leave"
                            UpdateLeaveStatus(leaveInfo, LMS_Pending_Cancel_Status, errMsg)

                        Case "Resubmit Leave"
                            Save(leaveInfo, isEdit, errMsg)
                            UpdateLeaveStatus(leaveInfo, LMS_Pending_Approve_Status, errMsg)

                        Case "Approve"
                            If leaveInfo.HIDDEN_STATUS = LMS_Pending_Cancel_Status Then
                                UpdateLeaveStatus(leaveInfo, LMS_Canceled_Status, errMsg)
                            Else
                                UpdateLeaveStatus(leaveInfo, LMS_Approved_Status, errMsg)
                            End If

                        Case "Upload"
                            UploadDocument(leaveInfo)

                        Case "Remove"
                            RemoveDocument(leaveInfo)
                            If leaveInfo.HIDDEN_STATUS = LMS_Pending_Approve_Status Then
                                'update the removed file name in DB
                                Save(leaveInfo, isEdit, errMsg, submitButton)
                            End If

                    End Select

                    'Send Notification Email [begin]
                    'Send Notification Email [end]

                    Select Case submitButton
                        Case "Submit"
                            If isEdit = True Or errMsg IsNot Nothing Then
                                If errMsg IsNot Nothing Then
                                    ModelState.AddModelError("Error", errMsg)
                                End If

                                AssignHiddenValuesBackToOriginal(leaveInfo, Nothing)

                                GenerateDropDownLists()
                                ViewBag.Message += GetUploadedFileLink(leaveInfo.USER_ID, leaveInfo.HIDDEN_DOCUMENT)

                                Return View("History_Details", leaveInfo)
                            Else
                                Return View("History_Details")
                            End If
                        Case Else
                            If errMsg IsNot Nothing And submitButton <> "Remove" Then
                                ModelState.AddModelError("Error", errMsg)
                            End If

                            AssignHiddenValuesBackToOriginal(leaveInfo, submitButton)

                            GenerateDropDownLists()

                            Return View("History_Details", leaveInfo)

                    End Select
                Catch ex As Exception

                End Try

            Catch
                Return View()
            End Try
        End Function

        Private Sub GenerateDropDownLists()
            ViewData("LeaveTypesList") = GenerateLeaveTypes()
            ViewData("AMPMList") = GenerateAMPM()
        End Sub

        Private Shared Function GenerateLeaveTypes() As List(Of SelectListItem)
            Dim leaveTypes = New List(Of SelectListItem)
            leaveTypes.Add(New SelectListItem With {.Text = LMS_Annual_Leave, .Value = LMS_Annual_Leave})
            leaveTypes.Add(New SelectListItem With {.Text = LMS_Medical_Leave, .Value = LMS_Medical_Leave})
            leaveTypes.Add(New SelectListItem With {.Text = LMS_Other_Leave, .Value = LMS_Other_Leave})

            Return leaveTypes
        End Function

        Private Shared Function GenerateAMPM() As List(Of SelectListItem)
            Dim AMPMList = New List(Of SelectListItem)
            AMPMList.Add(New SelectListItem With {.Text = "AM", .Value = "AM"})
            AMPMList.Add(New SelectListItem With {.Text = "PM", .Value = "PM"})

            Return AMPMList
        End Function

        Private Function GetLeavesHistoryData(ByVal id As Integer) As DataSet
            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

            Dim dsLeaves = Nothing
            Dim strMsg = Nothing
            Try
                dsLeaves = New DataSet

                objLeaves = New b_Leaves
                intfB_Leaves = objLeaves
                intfB_Leaves.b_Get_Leaves(id, strMsg, dsLeaves)

                Return dsLeaves

            Catch ex As Exception

            End Try

        End Function

        Private Function GetUploadedFileLink(ByVal userId As String,
                                             ByVal fileName As String) As String
            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
            Dim aHref As String = Nothing

            aHref = String.Format("<a id=""afilename"" href=""/Upload/{0}/{1}"" target=""_blank"">{2}</a)",
                                                         userId, fileName, fileName)

            Return aHref

        End Function

        Private Sub AssignHiddenValuesBackToOriginal(ByRef leaveInfo As HistoryViewModel,
                                                     ByVal action As String)
            leaveInfo.ID = leaveInfo.HIDDEN_ID
            leaveInfo.USER_ID = Session(strUserId)
            leaveInfo.TOTAL_TAKEN_LEAVE = leaveInfo.HIDDEN_TOTAL_LEAVE_TAKEN
            leaveInfo.STATUS = leaveInfo.HIDDEN_STATUS
            If action = "Upload" Then
                If leaveInfo.PostedFile IsNot Nothing Then
                    leaveInfo.HIDDEN_DOCUMENT = leaveInfo.PostedFile.FileName
                End If
            Else
                leaveInfo.DOCUMENTS = leaveInfo.HIDDEN_DOCUMENT
            End If
        End Sub

        Public Function Save(ByVal leaveInfo As HistoryViewModel,
                             ByRef isEdit As Boolean,
                             ByRef errMsg As String,
                             Optional ByVal actionType As String = Nothing) As ActionResult
            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

            Dim currentUserId = Session(strUserId)

            Try
                Dim dsLeave = Nothing
                Dim maxId = Nothing
                Dim newId = Nothing
                Dim leaveId = Nothing

                dsLeave = New DataSet

                dsLeave = GetLeavesHistoryData(leaveInfo.HIDDEN_ID)

                If dsLeave.Tables.Count > 0 And dsLeave.Tables(0).Rows.Count > 0 Then
                    isEdit = True
                    leaveId = leaveInfo.HIDDEN_ID
                Else
                    isEdit = False
                End If

                If actionType <> "Remove" Then
                    If Not checkLeaveValid(currentUserId, leaveId,
                                           leaveInfo.LEAVE_FROM, leaveInfo.LEAVE_TO,
                                           errMsg) Then
                        Exit Function
                    End If

                    If Not checkLeaveBalance(currentUserId, leaveInfo.LEAVE_TYPE,
                                             leaveInfo.HIDDEN_TOTAL_LEAVE_TAKEN,
                                             errMsg) Then
                        Exit Function
                    End If

                    If leaveInfo.LEAVE_TYPE = LMS_Medical_Leave And leaveInfo.HIDDEN_DOCUMENT Is Nothing Then
                        errMsg = "(Error) For Medical Leave, please submit Medical Leave Letter file."
                        Exit Function
                    End If
                End If

                If isEdit = True Then
                    objLeaves = New b_Leaves
                    intfB_Leaves = objLeaves
                    intfB_Leaves.b_Update_Leaves(leaveInfo, currentUserId, errMsg)
                Else
                    objLeaves = New b_Leaves
                    intfB_Leaves = objLeaves
                    intfB_Leaves.b_Get_MaxID(maxId, errMsg)

                    newId = maxId + 1

                    objLeaves = New b_Leaves
                    intfB_Leaves = objLeaves
                    intfB_Leaves.b_Insert_Leaves(leaveInfo,
                                                 newId, currentUserId, errMsg)

                End If

            Catch ex As Exception

            End Try

        End Function

        Private Function checkLeaveBalance(ByVal user_id As String,
                                           ByVal leave_type As String,
                                           ByVal leaves_taken As String,
                                           ByRef errMsg As String) As Boolean
            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

            Dim sql = Nothing
            Dim dsLeave = Nothing
            Dim leaveBalance As Double = Nothing

            Try
                dsLeave = New DataSet

                objLeaves = New b_Leaves
                intfB_Leaves = objLeaves
                intfB_Leaves.b_Get_Leaves_Balance(leave_type,
                                                  user_id,
                                                  leaveBalance,
                                                  errMsg)

                If errMsg <> "" Then
                    Return False
                End If

                If leaveBalance < CDbl(leaves_taken) Then
                    errMsg = "(Error) Not enough " & leave_type & " to apply."
                    Return False
                Else
                    Return True
                End If

            Catch ex As Exception

            End Try
        End Function
        Private Function checkLeaveValid(ByVal userId As String,
                                         ByVal leaveId As String,
                                         ByVal leaveFrom As String,
                                         ByVal leaveTo As String,
                                         ByRef errMsg As String) As Boolean
            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

            Try
                objLeaves = New b_Leaves
                intfB_Leaves = objLeaves
                Return intfB_Leaves.b_Check_Leaves_Valid(userId,
                                                        leaveId,
                                                        leaveFrom,
                                                        leaveTo,
                                                        errMsg)
            Catch ex As Exception

            End Try
        End Function

        Private Function UpdateLeaveStatus(ByVal leaveInfo As HistoryViewModel,
                                           ByVal status As String,
                                           ByRef errMsg As String) As ActionResult
            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

            Dim id As String = leaveInfo.HIDDEN_ID
            Dim currentUserId = Session(strUserId)
            Dim sql As String = Nothing

            Try
                objLeaves = New b_Leaves
                intfB_Leaves = objLeaves
                intfB_Leaves.b_Update_Leaves_Status_Balance(leaveInfo,
                                                            currentUserId,
                                                            id, status, errMsg)
            Catch ex As Exception

            End Try
        End Function

        Private Function UploadDocument(ByVal leaveInfo As HistoryViewModel) As ActionResult
            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

            Dim currentUserId = Session(strUserId)
            Dim path As String = Server.MapPath("~/Upload/")

            path = path + currentUserId + "\"

            Try
                If Not Directory.Exists(path) Then
                    Directory.CreateDirectory(path)
                End If

                If leaveInfo.PostedFile IsNot Nothing Then
                    Dim fileName As String = leaveInfo.PostedFile.FileName

                    leaveInfo.PostedFile.SaveAs(path & fileName)
                    ViewBag.Message += GetUploadedFileLink(currentUserId, fileName)
                Else
                    ModelState.AddModelError("Error", "Please select the file to upload.")
                End If

            Catch ex As Exception

            End Try
        End Function

        Private Function RemoveDocument(ByVal leaveInfo As HistoryViewModel) As ActionResult
            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

            Dim currentUserId = Session(strUserId)
            Dim path As String = Server.MapPath("~/Upload/")

            path = path + currentUserId + "\"

            Try
                If Not Directory.Exists(path) Then
                    ModelState.AddModelError("Error", "File not found to remove.")
                End If

                If leaveInfo.HIDDEN_DOCUMENT IsNot Nothing Then
                    Dim fileName As String = leaveInfo.HIDDEN_DOCUMENT
                    IO.File.Delete(path & fileName)
                    leaveInfo.HIDDEN_DOCUMENT = Nothing
                    ViewBag.Message += String.Format("<a href=""#""></a>")
                Else
                    ModelState.AddModelError("Error", "No file to remove.")
                End If

            Catch ex As Exception

            End Try
        End Function
    End Class


End Namespace