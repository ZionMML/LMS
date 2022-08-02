Imports System.Net
Imports System.Web.Http
Imports LMS.Models
Imports Newtonsoft.Json.Linq

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

                If dsAdmins.Tables.Count <= 0 Or dsAdmins.Tables(0).Rows.Count <= 0 Then
                    errMsg = "(Error) No records found in LMS Admins table."
                    ModelState.AddModelError("Info", errMsg)
                    adminList = Nothing
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

        <HttpPost>
        Public Function DeleteValue(<FromBody> ByVal data As JObject) As Hashtable
            Dim listStatus = New Hashtable

            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

            Try
                Dim strId As String = data("ID").ToObject(Of String)
                Dim strMsg As String = Nothing

                objAdmin = New b_Admin
                intfB_Admin = objAdmin
                intfB_Admin.b_Delete_Admin(strId, strMsg)

                listStatus.Add("ErrMsg", strMsg)

                Return listStatus

            Catch ex As Exception

            End Try
        End Function
    End Class
End Namespace