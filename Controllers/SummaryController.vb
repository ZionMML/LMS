Imports System.Web.Mvc
Imports LMS.ViewModels

Namespace Controllers
    Public Class SummaryController
        Inherits Controller

        ' GET: Summary
        Function Index() As ActionResult
            If Session.Count = 0 Then
                Session.Clear()
                Session.Abandon()
                ViewBag.LoginUserId = Nothing
                Return RedirectToAction("Index", "Home")
            Else
                ViewBag.LoginUserId += String.Format("(Welcome {0}!)", Session(strUserName))

                Dim leaveInfo As New SummaryViewModel

                leaveInfo.USER_ID = Session(strUserId)

                If Session(strAdminUser) = "Y" Then
                    leaveInfo.ISADMIN = True
                Else
                    leaveInfo.ISADMIN = False
                End If

                Return View("Summary_List", leaveInfo)
            End If
        End Function

    End Class
End Namespace