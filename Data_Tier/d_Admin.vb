Imports LMS.ViewModels

Public Class d_Admin
    Implements d_Admin_Interface

    Public Sub d_Get_Admin_Users(strId As String,
                                 strUserId As String,
                                 ByRef errMsg As String,
                                 ByRef dsAdmin As DataSet) Implements d_Admin_Interface.d_Get_Admin_Users
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
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Sub

    Sub d_Get_MaxID(ByRef maxId As Integer,
                    ByRef errMsg As String) Implements d_Admin_Interface.d_Get_MaxID
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String = Nothing
        Dim dsAdmin As DataSet = Nothing
        Try
            dsAdmin = New DataSet

            sql = "select max(ID) as maxID from LMS_ADMINS "

            GetData(sql, dsAdmin, errMsg)

            If dsAdmin.Tables.Count > 0 And dsAdmin.Tables(0).Rows.Count > 0 Then
                For Each dr As DataRow In dsAdmin.Tables(0).Rows
                    If dr("maxID").ToString() = "" Then
                        maxId = 0
                    Else
                        maxId = CInt(dr("maxID").ToString())
                    End If
                Next
            End If

        Catch ex As Exception
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Sub

    Public Sub d_Delete_Admin(strId As String,
                              ByRef errMsg As String) Implements d_Admin_Interface.d_Delete_Admin
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String
        Try
            sql = "delete from LMS_ADMINS where ID = '" & strId & "'"

            ExeData(sql, errMsg)

        Catch ex As Exception
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Sub

    Public Sub d_Insert_Admin(ByVal adminInfo As AdminsViewModel,
                              ByVal newId As Integer,
                              ByVal currentUserId As String,
                              ByRef errMsg As String) Implements d_Admin_Interface.d_Insert_Admin
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String
        Try
            sql = "insert into LMS_ADMINS (ID, USER_ID, USER_NAME, PASSWORD, EMAIL_ADDR, CREATED_BY, CREATED_DATE) "
            sql = sql + "values(" & newId & ",'" & adminInfo.USER_ID.ToUpper & "','" & adminInfo.USER_NAME & "','" & adminInfo.PASSWORD & "',"
            sql = sql + "'" & adminInfo.EMAIL_ADDR & "','" & currentUserId & "',sysdate())"

            ExeData(sql, errMsg)

        Catch ex As Exception
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Sub

    Public Sub d_Update_Admin(adminInfo As AdminsViewModel,
                              userId As String,
                              currentUserId As String,
                              ByRef errMsg As String) Implements d_Admin_Interface.d_Update_Admin
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String
        Try
            sql = "update LMS_ADMINS set " &
                " USER_NAME = '" & adminInfo.USER_NAME & "', " &
                " EMAIL_ADDR = '" & adminInfo.EMAIL_ADDR & "', " &
                " UPDATED_BY = '" & currentUserId & "', " &
                " UPDATED_DATE = sysdate() " &
                " where USER_ID = '" & userId.ToString.ToUpper & "'"

            ExeData(sql, errMsg)

        Catch ex As Exception
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Sub


End Class
