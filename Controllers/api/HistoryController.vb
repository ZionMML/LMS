Imports System.Net
Imports System.Web.Http
Imports LMS.ViewModels
Imports Newtonsoft.Json.Linq

Public Class HistoryController
    Inherits ApiController

    Private objLeaves As b_Leaves
    Private intfB_Leaves As b_Leaves_Interface

    ' GET api/<controller>
    Public Function GetValues() As IEnumerable(Of HistoryViewModel)

        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim leavesList As New List(Of HistoryViewModel)
        Dim strMsg = Nothing

        Try
            leavesList = RetrieveLeaves(Nothing, Nothing, strMsg)

            Return leavesList

        Catch ex As Exception

        End Try
    End Function

    <HttpPost>
    Public Function GetLeaves(<FromBody> ByVal data As JObject) As IEnumerable(Of HistoryViewModel)
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Dim leaveList As New List(Of HistoryViewModel)
        Dim strMsg = Nothing

        Try
            Dim startDt As String = data("startDate").ToObject(Of String)
            Dim endDt As String = data("endDate").ToObject(Of String)

            leaveList = RetrieveLeaves(startDt, endDt, strMsg)

            Return leaveList
        Catch ex As Exception

        End Try
    End Function

    <AcceptVerbs("CAL")>
    Public Function CalculateLeaves(<FromBody> ByVal data As JObject) As Hashtable
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            Dim intReturn_Code = 0
            Dim strMessage = Nothing
            Dim listStatus = New Hashtable

            Dim startDt As String = data("startDt").ToObject(Of String)
            Dim endDt As String = data("endDt").ToObject(Of String)
            Dim startAMPM As String = data("startAMPM").ToObject(Of String)
            Dim endAMPM As String = data("endAMPM").ToObject(Of String)

            Dim days As Decimal = DateDiff(DateInterval.Day, CDate(startDt), CDate(endDt))

            Dim tmpDate = CDate(CDate(startDt).ToString("dd/MM/yyyy"))
            Dim checkDate = Date.Parse(tmpDate).DayOfWeek

            Dim totalDays = days

            'to check Weekends or Holidays
            For i = 0 To totalDays
                If checkDate.ToString = "Saturday" Or checkDate.ToString = "Sunday" Then
                    days = days - 1
                Else
                    'check holidays
                    'if so, ...
                    ' days = days - 1 
                End If

                If i <> totalDays Then
                    tmpDate = tmpDate.AddDays(1)
                    checkDate = Date.Parse(tmpDate.ToString()).DayOfWeek
                End If
            Next

            If days = 0 Then
                If startAMPM = "PM" And endAMPM = "AM" Then
                    listStatus.Add("ErrMsg", "(Error) Same day Leave. Leave From is PM but Leave To cannot be AM.")
                    listStatus.Add("ttlLeaves", 0)
                    Return listStatus
                End If
            End If

            If startAMPM = endAMPM Then
                days = days + 0.5
            Else
                If days <> 0 And startAMPM = "PM" Then 'not same day leave and From Date is PM
                    days = days
                Else
                    days = days + 1
                End If
            End If

            listStatus.Add("ErrMsg", Nothing)
            listStatus.Add("ttlLeaves", days)

            Return listStatus

        Catch ex As Exception

        End Try
    End Function
    Private Function RetrieveLeaves(ByVal startDt As String,
                                    ByVal endDt As String,
                                    ByRef strMsg As String) As IEnumerable(Of HistoryViewModel)
        Dim dsLeaveHistory = Nothing
        Dim leavesList As New List(Of HistoryViewModel)
        Dim strSql As String = Nothing

        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

        Try
            dsLeaveHistory = New DataSet

            objLeaves = New b_Leaves
            intfB_Leaves = objLeaves
            intfB_Leaves.b_Get_Leaves_Inquiry(startDt, endDt, strMsg, dsLeaveHistory)

            If dsLeaveHistory.Tables.Count <= 0 Then

            Else
                For Each dr As DataRow In dsLeaveHistory.Tables(0).Rows
                    Dim leavesHistory As New HistoryViewModel
                    leavesHistory.ID = dr("ID").ToString
                    leavesHistory.USER_ID = dr("USER_ID").ToString
                    leavesHistory.USER_NAME = dr("USER_NAME").ToString
                    leavesHistory.LEAVE_TYPE = dr("LEAVE_TYPE").ToString
                    leavesHistory.LEAVE_FROM = dr("LEAVE_FROM").ToString
                    leavesHistory.LEAVE_FROM_AMPM = dr("LEAVE_FROM_AMPM").ToString
                    leavesHistory.LEAVE_TO = dr("LEAVE_TO").ToString
                    leavesHistory.LEAVE_TO_AMPM = dr("LEAVE_TO_AMPM").ToString
                    leavesHistory.TOTAL_TAKEN_LEAVE = dr("TOTAL_TAKEN_LEAVE").ToString
                    leavesHistory.STATUS = dr("STATUS").ToString
                    leavesHistory.UPDATED_BY = dr("UPDATED_BY").ToString
                    leavesHistory.UPDATED_DATE = dr("UPDATED_DATE").ToString
                    leavesHistory.APPROVED_BY = dr("APPROVED_BY").ToString
                    leavesHistory.APPROVED_DATE = dr("APPROVED_DATE").ToString
                    leavesList.Add(leavesHistory)
                Next
            End If

            Return leavesList

        Catch ex As Exception

        End Try
    End Function
End Class
