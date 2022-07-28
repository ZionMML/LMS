Imports System.ComponentModel.DataAnnotations

Namespace Models
    Public Class Admins
        Public ID As Integer?

        <Required(ErrorMessage:="User ID is required")>
        <StringLength(10)>
        Public Property USER_ID As String

        <Required(ErrorMessage:="User NAME is required")>
        <StringLength(40)>
        Public Property USER_NAME As String

        <Required(ErrorMessage:="Password is required")>
        <StringLength(20)>
        Public Property PASSWORD As String

        <Required(ErrorMessage:="Email Address is required")>
        <StringLength(100)>
        Public Property EMAIL_ADDR As String

        Public Property CREATED_BY As String

        Public Property CREATED_DATE As String

        Public Property UPDATED_BY As String

        Public Property UPDATED_DATE As String

    End Class
End Namespace


