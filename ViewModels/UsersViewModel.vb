Imports System.ComponentModel.DataAnnotations
Namespace ViewModels
    Public Class UsersViewModel
        Public ID As Integer?

        <Required>
        <StringLength(10)>
        <Display(Name:="User ID")>
        Public Property USER_ID As String

        <Required>
        <StringLength(40)>
        <Display(Name:="User Name")>
        Public Property USER_NAME As String

        <Required>
        <StringLength(20)>
        <Display(Name:="Password")>
        Public Property PASSWORD As String

        <Required>
        <StringLength(40)>
        <Display(Name:="Company")>
        Public Property COMPANY As String

        <Required>
        <StringLength(40)>
        <Display(Name:="Team Lead Name")>
        Public Property TEAM_LEAD_NAME As String

        <Required>
        <Display(Name:="Entitled Annual Leave")>
        Public Property MAX_ANNUAL_LEAVE As Integer

        Public Property ANNUAL_LEAVE_BALANCE As Integer

        <Required>
        <Display(Name:="Entitled Medical Leave")>
        Public Property MAX_MEDICAL_LEAVE As Integer

        Public Property MEDICAL_LEAVE_BALANCE As Integer

        <Required>
        <Display(Name:="Entitled Other Leave")>
        Public Property MAX_OTHER_LEAVE As Integer

        Public Property OTHER_LEAVE_BALANCE As Integer

        <Display(Name:="Created By")>
        Public Property CREATED_BY As String

        <Display(Name:="Created Date")>
        Public Property CREATED_DATE As String

        <Display(Name:="Updated By")>
        Public Property UPDATED_BY As String

        <Display(Name:="Updated Date")>
        Public Property UPDATED_DATE As String

        Public Property UPDATE_PWD_FLAG As String

        Public Property EDIT_FLAG As String

        Public Property EXISTING_USERID As String

    End Class
End Namespace


