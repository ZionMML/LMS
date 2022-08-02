Imports System.ComponentModel.DataAnnotations

Namespace Models
    Public Class Users
        Public ID As Integer?

        <Required>
        <StringLength(10)>
        Public Property USER_ID As String
        <Required>
        <StringLength(40)>
        Public Property USER_NAME As String
        <Required>
        <StringLength(20)>
        Public Property PASSWORD As String
        <Required>
        <StringLength(40)>
        Public Property COMPANY As String
        <Required>
        <StringLength(40)>
        Public Property TEAM_LEAD_NAME As String
        <Required>
        Public Property MAX_ANNUAL_LEAVE As Integer
        Public Property ANNUAL_LEAVE_BALANCE As Integer
        <Required>
        Public Property MAX_MEDICAL_LEAVE As Integer
        Public Property MEDICAL_LEAVE_BALANCE As Integer
        <Required>
        Public Property MAX_OTHER_LEAVE As Integer
        Public Property OTHER_LEAVE_BALANCE As Integer
        Public Property CREATED_BY As String
        Public Property CREATED_DATE As String
        Public Property UPDATED_BY As String
        Public Property UPDATED_DATE As String
        Public Property BALANCE_UPDATED_BY As String
        Public Property BALANCE_UPDATED_DATE As String

    End Class
End Namespace

