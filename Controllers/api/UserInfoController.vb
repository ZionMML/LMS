Imports System.Net
Imports System.Web.Http
Imports LMS.Models
Imports Newtonsoft.Json.Linq

Namespace Controllers.api
    Public Class UserInfoController
        Inherits ApiController

        Private objUser As b_LMS_User
        Private intfB_User As b_LMS_User_Interface


        ' GET: api/UserInfo
        Public Function GetValues() As IEnumerable(Of Users)
            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

            Dim dsUsers = Nothing
            Dim userList As New List(Of Users)
            Dim strMsg = Nothing

            Try
                dsUsers = New DataSet

                objUser = New b_LMS_User
                intfB_User = objUser
                intfB_User.b_Get_LMS_Users(Nothing, Nothing, strMsg, dsUsers)

                If dsUsers.Tables.Count <= 0 Or dsUsers.Tables(0).Rows.Count <= 0 Then
                    strMsg = "(Error) No records found in LMS Users table."
                    ModelState.AddModelError("Info", strMsg)
                    userList = Nothing
                Else
                    For Each sdr As DataRow In dsUsers.Tables(0).Rows
                        Dim users As New Users
                        users.ID = sdr("ID").ToString()
                        users.USER_ID = sdr("USER_ID").ToString()
                        users.USER_NAME = sdr("USER_NAME").ToString()
                        users.COMPANY = sdr("COMPANY").ToString()
                        users.TEAM_LEAD_NAME = sdr("TEAM_LEAD_NAME").ToString()
                        users.MAX_ANNUAL_LEAVE = sdr("MAX_ANNUAL_LEAVE").ToString()
                        users.ANNUAL_LEAVE_BALANCE = sdr("ANNUAL_LEAVE_BALANCE").ToString()
                        users.MAX_MEDICAL_LEAVE = sdr("MAX_MEDICAL_LEAVE").ToString()
                        users.MEDICAL_LEAVE_BALANCE = sdr("MEDICAL_LEAVE_BALANCE").ToString()
                        users.MAX_OTHER_LEAVE = sdr("MAX_OTHER_LEAVE").ToString()
                        users.OTHER_LEAVE_BALANCE = sdr("OTHER_LEAVE_BALANCE").ToString()
                        users.CREATED_BY = sdr("CREATED_BY").ToString()
                        users.CREATED_DATE = sdr("CREATED_DATE").ToString()
                        users.UPDATED_BY = sdr("UPDATED_BY").ToString()
                        users.UPDATED_DATE = sdr("UPDATED_DATE").ToString()
                        userList.Add(users)
                    Next
                End If

                Return userList

            Catch ex As Exception

            End Try

        End Function

        <Web.Http.HttpPost>
        Public Function DeleteValue(<FromBody> ByVal data As JObject) As Hashtable

            Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name

            Dim listStatus = New Hashtable
            Dim strMsg = Nothing

            Try
                Dim strId As String = data("ID").ToObject(Of String)

                objUser = New b_LMS_User
                intfB_User = objUser
                intfB_User.b_Delete_LMS_User(strId, strMsg)
                listStatus.Add("ErrMsg", strMsg)

                Return listStatus

            Catch ex As Exception

            End Try
        End Function
    End Class
End Namespace