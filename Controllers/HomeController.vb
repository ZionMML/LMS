Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Private dsAdmin = Nothing

    Private objAdmin As b_Admin
    Private intfB_Admin As b_Admin_Interface

    Function Index(linktext As String) As ActionResult
        ViewData("Title") = "Home LMS Page"
        If linktext = "Logout" Then
            Session.Clear()
            Session.Abandon()
        End If

        If Not IsNothing(TempData("Messsage")) Then
            ModelState.AddModelError("Info", TempData("Message"))
            TempData("Message") = Nothing
        End If

        Return View()
    End Function

    Function Edit(ByVal loginInfo As ViewModels.LoginViewModel) As ActionResult
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim userName As String = Nothing
        Dim strMsg As String = Nothing
        Dim isPersistent As Boolean = True
        Dim isAdminUser As Boolean = False

        Try
            If checkAdminUser(UCase(loginInfo.USER_ID), loginInfo.PASSWORD, userName, strMsg) Then
                AddSession(UCase(loginInfo.USER_ID), userName, isPersistent)
                isAdminUser = True
            End If
            If isAdminUser Then
                Session(strAdminUser) = "Y"
                Return RedirectToAction("Index", "Admin")
            Else
                Session(strAdminUser) = Nothing
                Return RedirectToAction("Index", "History")
            End If

        Catch ex As Exception

        End Try
    End Function

    Private Function checkAdminUser(ByVal userId As String, ByVal pwd As String, ByRef userName As String, ByRef strMsg As String) As Boolean
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim password As String = Nothing
        Try
            dsAdmin = New DataSet

            objAdmin = New b_Admin
            intfB_Admin = objAdmin
            intfB_Admin.b_Get_Admin_Users(Nothing, userId, strMsg, dsAdmin)

            If dsAdmin.tables.count > 0 And dsAdmin.tables(0).rows.count > 0 Then
                For Each dr As DataRow In dsAdmin.tables(0).rows
                    userName = dr("USER_NAME").ToString
                    password = dr("PASSWORD").ToString
                Next

                If password = pwd Then
                    Return True
                Else
                    Return False
                End If

            Else
                Return False
            End If

        Catch ex As Exception
            strMsg = FUNC_NAME + ex.ToString
        End Try
    End Function

    Private Sub AddSession(ByVal userId As String, ByVal userName As String, ByVal isPersistent As Boolean)

        Session(strUserId) = userId.Trim()
        Session(strUserName) = userName.Trim()

        Dim userData = "ApplicationSpecific data for this user."
        Dim ticket As FormsAuthenticationTicket = New FormsAuthenticationTicket(1,
                                                                                userId,
                                                                                Now, Now.AddMinutes(30),
                                                                                isPersistent, userData,
                                                                                FormsAuthentication.FormsCookiePath)

        Dim encTicket = FormsAuthentication.Encrypt(ticket)
        Response.Cookies.Add(New HttpCookie(FormsAuthentication.FormsCookieName, encTicket))

    End Sub
End Class
