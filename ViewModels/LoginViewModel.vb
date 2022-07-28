Imports System.ComponentModel.DataAnnotations

Namespace ViewModels
    Public Class LoginViewModel
        <Required(ErrorMessage:="User ID is required")>
        <Display(Name:="User ID")>
        <StringLength(10)>
        Public Property USER_ID As String

        <Required>
        <Display(Name:="Password")>
        <StringLength(68)>
        Public Property PASSWORD As String
    End Class
End Namespace

