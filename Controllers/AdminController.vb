Imports System.Web.Mvc
Imports LMS.ViewModels

Namespace Controllers
    Public Class AdminController
        Inherits Controller
        Private objAdmin As b_Admin
        Private intfB_Admin As b_Admin_Interface
        ' GET: Admin
        Function Index() As ActionResult
            If Session.Count = 0 Then
                Session.Clear()
                Session.Abandon()
                ViewBag.LoginUserId = Nothing
                Return RedirectToAction("Index", "Home")
            Else
                ViewBag.LoginUserId += String.Format("(Welcome {0}!)", Session(strUserName))

                If Session(strAdminUser) = "Y" Then
                    Return View("Admin_List")
                Else
                    TempData("Message") = "(Error) Your access is denied."
                    Return RedirectToAction("Index", "Home")
                End If
            End If

        End Function

        ' GET: Admin/Create
        Function Create() As ActionResult
            Return View("Admin_Details")
        End Function

        ' GET: Admin/Edit/5
        Function Edit(ByVal id As Integer) As ActionResult
            Dim dsAdmins = Nothing
            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
            Dim errMsg = Nothing

            Try
                dsAdmins = New DataSet

                objAdmin = New b_Admin
                intfB_Admin = objAdmin
                intfB_Admin.b_Get_Admin_Users(id, Nothing, errMsg, dsAdmins)

                If dsAdmins.tables.count > 0 And dsAdmins.tables(0).rows.count > 0 Then
                    Dim adminInfo As New AdminsViewModel
                    For Each sdr As DataRow In dsAdmins.tables(0).rows
                        adminInfo.ID = sdr("ID").ToString()
                        adminInfo.USER_ID = sdr("USER_ID").ToString()
                        adminInfo.EXISTING_USERID = sdr("USER_ID").ToString()
                        adminInfo.USER_NAME = sdr("USER_NAME").ToString()
                        adminInfo.EMAIL_ADDR = sdr("EMAIL_ADDR").ToString()
                    Next

                    adminInfo.EDIT_FLAG = "Y"

                    If Not IsNothing(TempData("Message")) Then
                        ModelState.AddModelError("Info", TempData("Message"))
                        TempData("Message") = Nothing
                    End If

                    Return View("Admin_Details", adminInfo)
                End If

            Catch ex As Exception

            End Try

        End Function

        ' POST: Admin/Edit/5
        <HttpPost()>
        Function Edit(ByVal adminInfo As AdminsViewModel) As ActionResult
            Dim userId = Nothing
            Dim currentUserId = Nothing
            Dim strMsg As String = Nothing

            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

            Try
                currentUserId = Session(strUserId)

                If adminInfo.EDIT_FLAG = "Y" Then
                    ModelState.Remove("USER_ID")
                    ModelState.Remove("PASSWORD")
                    userId = adminInfo.EXISTING_USERID
                    adminInfo.USER_ID = userId
                Else
                    userId = adminInfo.USER_ID
                End If

                If Not ModelState.IsValid Then
                    Return View("Admin_Details", adminInfo)
                End If

                Dim dsAdmin = Nothing
                Dim maxID = Nothing
                Dim newID = Nothing

                dsAdmin = New DataSet

                objAdmin = New b_Admin
                intfB_Admin = objAdmin
                intfB_Admin.b_Get_Admin_Users(Nothing, userId, strMsg, dsAdmin)

                If dsAdmin.tables.count > 0 And dsAdmin.tables(0).rows.count > 0 Then
                    objAdmin = New b_Admin
                    intfB_Admin = objAdmin
                    intfB_Admin.b_Update_Admin(adminInfo, userId, currentUserId, strMsg)

                    ModelState.AddModelError("Message", strMsg)
                    Return View("Admin_Details", adminInfo)
                Else
                    objAdmin = New b_Admin
                    intfB_Admin = objAdmin
                    intfB_Admin.b_Get_MaxID(maxID, strMsg)

                    newID = maxID + 1

                    objAdmin = New b_Admin
                    intfB_Admin = objAdmin
                    intfB_Admin.b_Insert_Admin(adminInfo, newID, currentUserId, strMsg)

                    Return View("Admin_List")
                End If

                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        ' GET: Admin/Delete/5
        Function Delete(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        ' POST: Admin/Delete/5
        <HttpPost()>
        Function Delete(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                ' TODO: Add delete logic here

                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function
    End Class
End Namespace