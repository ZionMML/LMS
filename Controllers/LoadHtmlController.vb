Imports System.Web.Mvc
Imports LMS.ViewModels

Namespace Controllers
    Public Class LoadHtmlController
        Inherits Controller

        Private objLeaves As b_Leaves
        Private intfB_Leaves As b_Leaves_Interface

        ' GET: LoadHtml
        Function Index() As ActionResult
            If Session.Count = 0 Then
                Session.Clear()
                Session.Abandon()
                ViewBag.LoginUserId = Nothing
                Return RedirectToAction("Index", "Home")
            Else
                ViewBag.LoginUserId += String.Format("(Welcome {0}!)", Session(strUserName))
                Return View()
            End If
        End Function

        Public Function GetEvents() As JsonResult
            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

            Dim dsLeaves = Nothing
            Dim leaveList As New List(Of HistoryViewModel)

            Dim errMsg = Nothing
            Dim tmpLeaveTo = Nothing

            dsLeaves = New DataSet

            objLeaves = New b_Leaves
            intfB_Leaves = objLeaves
            intfB_Leaves.b_Get_Approveb_Leaves(errMsg, dsLeaves)

            If dsLeaves.tables.count > 0 Then
                For Each sdr As DataRow In dsLeaves.tables(0).rows
                    Dim leaves As New HistoryViewModel
                    leaves.USER_NAME = sdr("USER_NAME").ToString()
                    leaves.LEAVE_TYPE = sdr("LEAVE_TYPE").ToString()
                    leaves.LEAVE_FROM = CDate(sdr("LEAVE_FROM").ToString()).ToString("dd-MM-yyyy")
                    leaves.LEAVE_FROM_AMPM = sdr("LEAVE_FROM_AMPM").ToString()
                    'fullcalendar javascript tool deducts 1 day for end day, so need to add 1 day in advance
                    leaves.LEAVE_TO = CDate(sdr("LEAVE_TO").ToString()).AddDays(1).ToString("dd-MM-yyyy")
                    leaves.LEAVE_TO_AMPM = sdr("LEAVE_TO_AMPM").ToString()
                    leaveList.Add(leaves)
                Next
            End If

            Return Json(leaveList, JsonRequestBehavior.AllowGet)

        End Function
    End Class
End Namespace