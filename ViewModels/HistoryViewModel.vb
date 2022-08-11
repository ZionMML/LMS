Imports System.ComponentModel.DataAnnotations

Namespace ViewModels
    Public Class HistoryViewModel
        Public ID As Integer?

        <StringLength(10)>
        <Display(Name:="User ID")>
        Public Property USER_ID As String

        <Display(Name:="User Name")>
        Public Property USER_NAME As String

        <StringLength(20)>
        <Display(Name:="Leave Type")>
        Public Property LEAVE_TYPE As String

        <Required>
        <Display(Name:="Leave From")>
        Public Property LEAVE_FROM As String

        <Display(Name:="Session")>
        Public Property LEAVE_FROM_AMPM As String

        <Required>
        <Display(Name:="Leave To")>
        Public Property LEAVE_TO As String

        <Display(Name:="Session")>
        Public Property LEAVE_TO_AMPM As String

        <Display(Name:="Total Leave Taken")>
        Public Property TOTAL_TAKEN_LEAVE As Decimal

        <Required>
        <StringLength(100)>
        <Display(Name:="Remarks")>
        Public Property REMARKS As String

        <Display(Name:="Status")>
        Public Property STATUS As String

        <StringLength(200)>
        <Display(Name:="Documents")>
        Public Property DOCUMENTS As String

        <RegularExpression("([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.pdf|)$", ErrorMessage:="Only image and PDF files are allowed.")>
        Public Property PostedFile As HttpPostedFileBase

        <Display(Name:="Updated By")>
        Public Property UPDATED_BY As String

        <Display(Name:="Updated Date")>
        Public Property UPDATED_DATE As String

        <Display(Name:="Approved By")>
        Public Property APPROVED_BY As String

        <Display(Name:="Approved Date")>
        Public Property APPROVED_DATE As String

        Public Property EDIT_FLAG As String

        Public Property CURRENT_USER_ID As String

        Public Property HIDDEN_ID As String

        Public Property HIDDEN_TOTAL_LEAVE_TAKEN As String

        Public Property HIDDEN_STATUS As String

        Public Property HIDDEN_DOCUMENT As String

        Public Property ISADMIN As Boolean

        Public Property HIDDEN_USER_ID As String

        Public Property HIDDEN_APPROVED_BY As String

        <Display(Name:="Start Date")>
        Public Property START_DATE As String

        <Display(Name:="End Date")>
        Public Property END_DATE As String



    End Class
End Namespace

