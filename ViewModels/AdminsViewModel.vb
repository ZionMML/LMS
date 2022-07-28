Imports System.ComponentModel.DataAnnotations

Namespace ViewModels
    Public Class AdminsViewModel
        Public ID As Integer?

        <Required(ErrorMessage:="User ID is required")>
        <Display(Name:="User ID")>
        <StringLength(10)>
        Public Property USER_ID As String

        <Required(ErrorMessage:="User NAME is required")>
        <StringLength(40)>
        <Display(Name:="User Name")>
        Public Property USER_NAME As String

        <Required(ErrorMessage:="Password is required")>
        <StringLength(20)>
        <Display(Name:="Password")>
        Public Property PASSWORD As String

        <Required(ErrorMessage:="Email Address is required")>
        <StringLength(100)>
        <Display(Name:="Email Address")>
        Public Property EMAIL_ADDR As String

        <Display(Name:="Created By")>
        Public Property CREATED_BY As String

        <Display(Name:="Created Date")>
        Public Property CREATED_DATE As String

        <Display(Name:="Updated By")>
        Public Property UPDATED_BY As String

        <Display(Name:="Updated Date")>
        Public Property UPDATED_DATE As String

        Public Property EDIT_FLAG As String

        Public Property EXISTING_USERID As String

    End Class
End Namespace


