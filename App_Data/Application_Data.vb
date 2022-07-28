Imports System.Data.SqlClient
Imports LMS.CRUD
Public Module Application_Data

    Public Sub Get_Admin_Users(ByVal strId As String, ByVal strUserId As String, ByRef dsAdmin As DataSet, ByRef errMsg As String)
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String
        Try
            sql = "select * from LMS_ADMINS "
            If strId IsNot Nothing Then
                sql = sql + "where ID = '" & strId & "'"
            ElseIf strUserId IsNot Nothing Then
                sql = sql + "where USER_ID = '" & strUserId & "'"
            End If

            GetData(sql, dsAdmin, errMsg)

        Catch ex As Exception
            errMsg = "Error on Get_Admin_Users:" + ex.ToString
        End Try
    End Sub
End Module
