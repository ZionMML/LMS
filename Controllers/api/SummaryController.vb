Imports System.Web.Http
Imports System.Web.Mvc
Imports LMS.ViewModels
Imports Newtonsoft.Json.Linq

Namespace Controllers.api
    Public Class SummaryController
        Inherits ApiController

        Private objLeaves As b_Leaves
        Private intfB_Leaves As b_Leaves_Interface

        <Http.HttpPost>
        Public Function GetValues(<FromBody> ByVal data As JObject) As IEnumerable(Of SummaryViewModel)
            Dim currentUserId As String = data("userId").ToObject(Of String)
            Dim isAdmin As Boolean = data("isAdmin").ToObject(Of Boolean)
            Dim errMsg As String = Nothing

            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

            Dim dsLeaveSummary = Nothing
            Dim leavesList As New List(Of SummaryViewModel)

            Dim sql As String = Nothing

            Try
                dsLeaveSummary = New DataSet

                objLeaves = New b_Leaves
                intfB_Leaves = objLeaves
                intfB_Leaves.b_Get_Leaves_User_Summary(isAdmin, currentUserId.ToUpper, errMsg, dsLeaveSummary)

                If dsLeaveSummary.tables.count <= 0 Then

                Else
                    For Each sdr As DataRow In dsLeaveSummary.tables(0).rows
                        Dim leavesSummary As New SummaryViewModel
                        leavesSummary.USER_ID = sdr("USER_ID").ToString()
                        leavesSummary.USER_NAME = sdr("USER_NAME").ToString()
                        leavesSummary.LEAVE_TYPES = sdr("LEAVE_TYPES").ToString()
                        leavesSummary.TOTAL_LEAVES_TAKEN = sdr("TOTAL_LEAVES_TAKEN").ToString()
                        leavesSummary.ENTITLED_LEAVES = sdr("ENTITLED_LEAVES").ToString()
                        leavesSummary.LEAVE_BALANCE = sdr("LEAVE_BALANCE").ToString()
                        leavesList.Add(leavesSummary)
                    Next
                End If

                Return leavesList
            Catch ex As Exception

            End Try
        End Function
    End Class
End Namespace