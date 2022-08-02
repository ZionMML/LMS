Imports LMS.ViewModels
Imports oracle.dataacess.client

Public Class d_LMS_User
    Implements d_LMS_User_Interface

    Public Sub d_Get_LMS_Users(strId As String,
                               strUserId As String,
                               ByRef errMsg As String,
                               ByRef dsUser As DataSet) Implements d_LMS_User_Interface.d_Get_LMS_Users
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String
        Try
            sql = "select * from LMS_USERS "
            If strId IsNot Nothing Then
                sql = sql + "where ID = '" & strId & "'"
            ElseIf strUserId IsNot Nothing Then
                sql = sql + "where USER_ID = '" & strUserId & "'"
            End If

            GetData(sql, dsUser, errMsg)

        Catch ex As Exception
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Sub

    Public Sub d_Get_MaxID(ByRef maxId As Integer,
                           ByRef errMsg As String) Implements d_LMS_User_Interface.d_Get_MaxID
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String = Nothing
        Dim dsAdmin As DataSet = Nothing
        Try
            dsAdmin = New DataSet

            sql = "select max(ID) as maxID from LMS_USERS "

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

    Public Sub d_Delete_LMS_User(strId As String,
                                 ByRef errMsg As String) Implements d_LMS_User_Interface.d_Delete_LMS_User
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String
        Try
            sql = "delete from LMS_USERS where ID = '" & strId & "'"

            ExeData(sql, errMsg)

        Catch ex As Exception
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Sub

    Public Sub d_Insert_LMS_User(userInfo As UsersViewModel,
                                 newId As Integer,
                                 currentUserId As String,
                                 ByRef errMsg As String) Implements d_LMS_User_Interface.d_Insert_LMS_User
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String
        Try
            sql = "insert into LMS_USERS (ID, USER_ID, USER_NAME, PASSWORD, COMPANY, TEAM_LEAD_NAME, " &
                    " MAX_ANNUAL_LEAVE, ANNUAL_LEAVE_BALANCE, MAX_MEDICAL_LEAVE, MEDICAL_LEAVE_BALANCE, " &
                    " MAX_OTHER_LEAVE, OTHER_LEAVE_BALANCE, CREATED_BY, CREATED_DATE) " &
                    " values(" & newId & ",'" & userInfo.USER_ID.ToUpper & "','" & userInfo.USER_NAME & "','" & userInfo.PASSWORD & "','" & userInfo.COMPANY & "','" & userInfo.TEAM_LEAD_NAME & "', " &
                    " " & userInfo.MAX_ANNUAL_LEAVE & ", " & userInfo.MAX_ANNUAL_LEAVE & ", " & userInfo.MAX_MEDICAL_LEAVE & ", " & userInfo.MAX_MEDICAL_LEAVE & ", " &
                    " " & userInfo.MAX_OTHER_LEAVE & ", " & userInfo.MAX_OTHER_LEAVE & ",'" & currentUserId & "',sysdate())"

            ExeData(sql, errMsg)

        Catch ex As Exception
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Sub

    Public Sub d_Update_LMS_User(userInfo As UsersViewModel, userId As String, currentUserId As String, updatePWDFlag As String, ByRef errMsg As String) Implements d_LMS_User_Interface.d_Update_LMS_User
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String
        Try

            If updatePWDFlag = "Y" Then
                sql = "update LMS_USERS set " &
             " PASSWORD = '" & userInfo.PASSWORD & "', " &
             " UPDATED_BY = '" & currentUserId & "', " &
             " UPDATED_DATE = sysdate() " &
             " where USER_ID = '" & userId.ToString.ToUpper & "'"
            Else
                sql = "update LMS_USERS set " &
             " USER_NAME = '" & userInfo.USER_NAME & "', " &
             " COMPANY = '" & userInfo.COMPANY & "', " &
             " TEAM_LEAD_NAME = '" & userInfo.TEAM_LEAD_NAME & "', " &
             " ANNUAL_LEAVE_BALANCE = (" & userInfo.MAX_ANNUAL_LEAVE & " - MAX_ANNUAL_LEAVE) + ANNUAL_LEAVE_BALANCE, " &
             " MAX_ANNUAL_LEAVE = " & userInfo.MAX_ANNUAL_LEAVE & ", " &
             " MEDICAL_LEAVE_BALANCE = (" & userInfo.MAX_MEDICAL_LEAVE & " - MAX_MEDICAL_LEAVE) + MEDICAL_LEAVE_BALANCE, " &
             " MAX_MEDICAL_LEAVE = " & userInfo.MAX_MEDICAL_LEAVE & ", " &
             " OTHER_LEAVE_BALANCE = (" & userInfo.MAX_OTHER_LEAVE & " - MAX_OTHER_LEAVE) + OTHER_LEAVE_BALANCE, " &
             " MAX_OTHER_LEAVE = " & userInfo.MAX_OTHER_LEAVE & ", " &
             " UPDATED_BY = '" & currentUserId & "', " &
             " UPDATED_DATE = sysdate() " &
             " where USER_ID = '" & userId.ToString.ToUpper & "'"
            End If

            ExeData(sql, errMsg)

        Catch ex As Exception
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Sub


End Class
