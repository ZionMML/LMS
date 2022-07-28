Imports System.Net
Imports System.Web.Http
Imports LMS.Models

Namespace Controllers.api
    Public Class AdminController
        Inherits ApiController

        Private objAdmin As b_Admin
        Private intfB_Admin As b_Admin_Interface

        ' GET: api/Admin
        Public Function GetValues() As IEnumerable(Of Admins)
            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

            Dim dsAdmins = Nothing
            Dim adminList As New List(Of Admins)
            Dim errMsg As String = Nothing

            Try
                dsAdmins = New DataSet

                objAdmin = New b_Admin
                intfB_Admin = objAdmin
                intfB_Admin.b_Get_Admin_Users(Nothing, Nothing, errMsg, dsAdmins)

                If dsAdmins.tables.count <= 0 Then

                Else
                    For Each sdr As DataRow In dsAdmins.Tables(0).rows
                        Dim admins As New Admins
                        admins.ID = sdr("ID").ToString()
                        admins.USER_ID = sdr("USER_ID").ToString()
                        admins.USER_NAME = sdr("USER_NAME").ToString()
                        admins.EMAIL_ADDR = sdr("EMAIL_ADDR").ToString()
                        admins.CREATED_BY = sdr("CREATED_BY").ToString()
                        admins.CREATED_DATE = sdr("CREATED_DATE").ToString()
                        admins.UPDATED_BY = sdr("UPDATED_BY").ToString()
                        admins.UPDATED_DATE = sdr("UPDATED_DATE").ToString()
                        adminList.Add(admins)
                    Next
                End If

                Return adminList
            Catch ex As Exception

            End Try
        End Function

        ' GET: api/Admin/5
        Public Function GetValue(ByVal id As Integer) As String
            Return "value"
        End Function

        ' POST: api/Admin
        Public Sub PostValue(<FromBody()> ByVal value As String)

        End Sub

        ' PUT: api/Admin/5
        Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

        End Sub

        ' DELETE: api/Admin/5
        Public Sub DeleteValue(ByVal id As Integer)

        End Sub
    End Class
End Namespace