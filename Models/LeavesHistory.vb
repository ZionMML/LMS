Imports System.ComponentModel.DataAnnotations

Namespace Models
    Public Class LeavesHistory

        Public ID As Integer?
        Public Property USER_ID As String
        Public Property LEAVE_TYPE As String
        Public Property LEAVE_FROM As String ',
        Public Property LEAVE_FROM_AMPM As String 'varchar(2)
        Public Property LEAVE_TO As String ',
        Public Property LEAVE_TO_AMPM As String 'varchar(2)
        Public Property TOTAL_TAKEN_LEAVE As Integer
        Public Property REMARKS As String 'varchar(100)
        Public Property STATUS As String 'varchar(20)
        Public Property DOCUMENTS As String 'varchar(200)
        Public Property UPDATED_BY As String 'varchar(10)
        Public Property UPDATED_DATE As String ',
        Public Property APPROVED_BY As String 'varchar(10)
        Public Property APPROVED_DATE As String ',

    End Class
End Namespace

