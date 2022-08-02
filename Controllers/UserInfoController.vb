Imports System.Web.Mvc
Imports LMS.ViewModels

Namespace Controllers
    Public Class UserInfoController
        Inherits Controller

        Private objAdmin As b_Admin
        Private intfB_Admin As b_Admin_Interface

        Private objUser As b_LMS_User
        Private intfb_User As b_LMS_User_Interface

        ' GET: UserInfo
        Function Index() As ActionResult
            If Session.Count = 0 Then
                Session.Clear()
                Session.Abandon()
                ViewBag.LoginUserId = Nothing
                Return RedirectToAction("Index", "Home")
            Else
                ViewBag.LoginUserId += String.Format("(Welcome {0}!)", Session(strUserName))

                If Session(strAdminUser) = "Y" Then
                    Return View("User_List")
                Else
                    TempData("Message") = "(Error) Your access is denied."
                    Return RedirectToAction("Index", "Home")
                End If
            End If
        End Function

        ' GET: UserInfo/Create
        Function Create() As ActionResult
            GenerateDropDownList()
            Return View("User_Details")
        End Function

        ' GET: UserInfo/Edit/5
        Function Edit(ByVal id As Integer) As ActionResult
            Dim dsUser = Nothing
            Dim strMsg = Nothing

            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

            Try
                dsUser = New DataSet

                objUser = New b_LMS_User
                intfb_User = objUser
                intfb_User.b_Get_LMS_Users(id, Nothing, strMsg, dsUser)

                If dsUser.Tables.Count > 0 And dsUser.Tables(0).Rows.Count > 0 Then
                    Dim userInfo As New UsersViewModel
                    For Each sdr In dsUser.Tables(0).Rows
                        userInfo.ID = sdr("ID").ToString()
                        userInfo.USER_ID = sdr("USER_ID").ToString()
                        userInfo.EXISTING_USERID = userInfo.USER_ID
                        userInfo.USER_NAME = sdr("USER_NAME").ToString()
                        userInfo.COMPANY = sdr("COMPANY").ToString()
                        userInfo.TEAM_LEAD_NAME = sdr("TEAM_LEAD_NAME").ToString()
                        userInfo.MAX_ANNUAL_LEAVE = sdr("MAX_ANNUAL_LEAVE").ToString()
                        userInfo.MAX_MEDICAL_LEAVE = sdr("MAX_MEDICAL_LEAVE").ToString()
                        userInfo.MAX_OTHER_LEAVE = sdr("MAX_OTHER_LEAVE").ToString()
                    Next

                    userInfo.EDIT_FLAG = "Y"

                    If Not IsNothing(TempData("Message")) Then
                        ModelState.AddModelError("Info", TempData("Message"))
                        TempData("Message") = Nothing
                    End If

                    GenerateDropDownList()
                    Return View("User_Details", userInfo)
                End If
            Catch ex As Exception

            End Try

            Return View()
        End Function

        ' POST: UserInfo/Edit/5
        <HttpPost()>
        Function Edit(ByVal userInfo As UsersViewModel) As ActionResult
            Dim userId = Nothing
            Dim currentUserId = Nothing
            Dim strMsg = Nothing

            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

            Try
                currentUserId = Session(strUserId)

                If userInfo.EDIT_FLAG = "Y" Then
                    ModelState.Remove("USER_ID")
                    ModelState.Remove("PASSWORD")
                    userId = userInfo.EXISTING_USERID
                    'do assign the user id back to model user id
                    userInfo.USER_ID = userId
                ElseIf userInfo.UPDATE_PWD_FLAG = "Y" Then
                    ModelState.Remove("USER_ID")
                    ModelState.Remove("USER_NAME")
                    ModelState.Remove("COMPANY")
                    ModelState.Remove("TEAM_LEAD_NAME")
                Else
                    userId = userInfo.USER_ID
                End If

                If Not ModelState.IsValid Then
                    GenerateDropDownList()
                    Return View("User_Details", userInfo)
                End If

                Dim dsUser = Nothing
                Dim maxID = Nothing
                Dim newID = Nothing

                dsUser = New DataSet

                objUser = New b_LMS_User
                intfb_User = objUser
                intfb_User.b_Get_LMS_Users(Nothing, userId, strMsg, dsUser)

                If dsUser.tables.count > 0 And dsUser.tables(0).rows.count > 0 Then
                    objUser = New b_LMS_User
                    intfb_User = objUser
                    intfb_User.b_Update_LMS_User(userInfo,
                                                 userId.ToString.ToUpper,
                                                 currentUserId.ToString.ToUpper,
                                                 userInfo.UPDATE_PWD_FLAG,
                                                 strMsg)

                    ModelState.AddModelError("Message", strMsg)
                    GenerateDropDownList()
                    Return View("User_Details", userInfo)
                Else
                    objUser = New b_LMS_User
                    intfb_User = objUser
                    intfb_User.b_Insert_LMS_User(userInfo,
                                                 newID,
                                                 currentUserId,
                                                 strMsg)
                End If
                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        'GET: UserInfo/UpdatePassword
        Function UpdatePassword() As ActionResult
            If Session.Count = 0 Then
                Session.Clear()
                Session.Abandon()
                ViewBag.LoginUserId = Nothing
                Return RedirectToAction("Index", "Home")
            Else
                ViewBag.LoginUserId += String.Format("(Welcome {0}!)", Session(strUserName))

                If Session(strAdminUser) = "Y" Then
                    TempData("Message") = "(Error) You're Admin User, not allowed for password change."
                    Return RedirectToAction("Index", "Home")
                End If

                Dim userInfo As New UsersViewModel

                userInfo.UPDATE_PWD_FLAG = "Y"
                GenerateDropDownList()
                Return View("User_Details", userInfo)

            End If
        End Function

        Private Sub GenerateDropDownList()
            ViewData("TeamLeadList") = GenerateTeamLeads()
        End Sub

        Private Function GenerateTeamLeads() As List(Of SelectListItem)
            Dim teamLeads = New List(Of SelectListItem)
            Dim dsAdmin = Nothing
            Dim teamLeadName = Nothing
            Dim strMsg = Nothing

            dsAdmin = New DataSet

            objAdmin = New b_Admin
            intfB_Admin = objAdmin
            intfB_Admin.b_Get_Admin_Users(Nothing, Nothing, strMsg, dsAdmin)

            If dsAdmin.tables.count > 0 And dsAdmin.tables(0).rows.count > 0 Then
                For Each sdr As DataRow In dsAdmin.tables(0).rows
                    teamLeadName = sdr("USER_NAME").ToString
                    teamLeads.Add(New SelectListItem With {.Text = teamLeadName, .Value = teamLeadName})
                Next

                Return teamLeads
            Else
                Return Nothing
            End If
        End Function
    End Class
End Namespace