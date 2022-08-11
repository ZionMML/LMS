Imports LMS.ViewModels

Public Class d_Leaves
    Implements d_Leaves_Interface

    Public Sub d_Get_Leaves_Inquiry(startDt As String,
                                    endDt As String,
                                    ByRef errMsg As String,
                                    ByRef dsLeaves As DataSet) Implements d_Leaves_Interface.d_Get_Leaves_Inquiry
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String
        Try
            sql = " select H.*, U.USER_NAME from LMS_LEAVES_HISTORY H, " &
                  " LMS_USERS U where H.USER_ID = U.USER_ID "
            If String.IsNullOrEmpty(startDt) Then
                sql = sql & " and H.UPDATED_DATE between sysdate() - 30 " &
                            " and sysdate() + 30 "
            Else
                sql = sql & " And H.UPDATED_DATE between to_date('" & startDt & "','dd/mm/yyyy') " &
                            " and to_date('" & endDt & "','dd/mm/yyyy')"
            End If

            GetData(sql, dsLeaves, errMsg)

        Catch ex As Exception
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Sub

    Public Sub d_Get_Leaves_User_Summary(isAdmin As Boolean,
                                         currentUserId As String,
                                         ByRef errMsg As String,
                                         ByRef dsLeavesUser As DataSet) Implements d_Leaves_Interface.d_Get_Leaves_User_Summary
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String
        Try
            sql = " select distinct u.user_id, user_name, leave_types, entitled_leaves, " &
                " case h.status when 'Approved' then nvl (h.total_taken_leave, 0) " &
                " else 0 end total_leaves_taken, " &
                " nvl (u.leave_balance, 0) leave_balance " &
                " from (select * from lms_users u " &
                " unpivot ((entitled_leaves, leave_balance) for leave_types " &
                " in ((max_annual_leave, annual_leave_balance) as '" & LMS_Annual_Leave & "', " &
                " (max_medical_leave, medical_leave_balance) as '" & LMS_Medical_Leave & "', " &
                " (max_other_leave, other_leave_balance) as '" & LMS_Other_Leave & "')) "

            If Not isAdmin Then
                sql = sql & " where user_id = '" & currentUserId.ToUpper & "' "
            End If

            sql = sql & ") u left join lms_leaves_history h on h.user_id = u.user_id" *
                        " and h.leave_type = u.leave_types " &
                        " order by user_id, leave_types "

            GetData(sql, dsLeavesUser, errMsg)

        Catch ex As Exception
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Sub

    Public Sub d_Get_Leaves(id As String,
                            ByRef errMsg As String,
                            ByRef dsLeaves As DataSet) Implements d_Leaves_Interface.d_Get_Leaves
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String
        Try
            sql = " select * from LMS_LEAVES_HISTORY where ID = '" & id & "'"

            GetData(sql, dsLeaves, errMsg)

        Catch ex As Exception
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Sub

    Public Sub d_Get_MaxID(ByRef maxId As Integer, ByRef errMsg As String) Implements d_Leaves_Interface.d_Get_MaxID
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String = Nothing
        Dim dsAdmin As DataSet = Nothing
        Try
            dsAdmin = New DataSet

            sql = "select max(ID) as maxID from LMS_LEAVES_HISTORY "

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

    Public Sub d_Get_Approved_Leaves(ByRef errMsg As String,
                                     ByRef dsLeaves As DataSet) Implements d_Leaves_Interface.d_Get_Approved_Leaves
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String
        Try
            sql = " select u.USER_NAME, h.LEAVE_TYPE, h.LEAVE_FROM, h.LEAVE_FROM_AMPM, " &
                " h.LEAVE_TO, h.LEAVE_TO_AMPM from lms_leaves_history h, lms_users u " &
                " where h.USER_ID = u.USER_ID and h.STATUS = '" & LMS_Approved_Status & "' "

            GetData(sql, dsLeaves, errMsg)

        Catch ex As Exception
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Sub

    Public Sub d_Get_Leaves_Balance(leave_type As String,
                                    user_id As String,
                                    ByRef leaveBalance As Double,
                                    ByRef errMsg As String) Implements d_Leaves_Interface.d_Get_Leaves_Balance
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String
        Dim dsLeaves = Nothing
        Try
            dsLeaves = New DataSet

            sql = " select "
            Select Case leave_type
                Case LMS_Annual_Leave
                    sql = sql & " annual_leave_balance as leave_balance "
                Case LMS_Medical_Leave
                    sql = sql & " medical_leave_balance as leave_balance "
                Case LMS_Other_Leave
                    sql = sql & " other_leave_balance as leave_balance "
            End Select

            sql = sql & " from LMS_USERS where USER_ID = '" & user_id.ToUpper & "' "

            GetData(sql, dsLeaves, errMsg)

            If dsLeaves.tables.count > 0 And dsLeaves.tables(0).rows.count > 0 Then
                For Each dr As DataRow In dsLeaves.tables(0).rows
                    If IsDBNull(dr("leave_balance")) Then
                        errMsg = "(Error) Please contact Team Lead to update the Leave Balance in User profile."
                    Else
                        leaveBalance = CDbl(dr("leave_balance").ToString)
                    End If
                Next
            End If

        Catch ex As Exception
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Sub

    Public Sub d_Insert_Leaves(leavesInfo As HistoryViewModel,
                               newId As Integer,
                               currentUserId As String,
                               ByRef errMsg As String) Implements d_Leaves_Interface.d_Insert_Leaves
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String
        Try
            sql = "insert into LMS_LEAVES_HISTORY (ID, USER_ID, LEAVE_TYPE, " &
                  " LEAVE_FROM, LEAVE_FROM_AMPM, " &
                  " LEAVE_TO, LEAVE_TO_AMPM, " &
                  " TOTAL_TAKEN_LEAVE, REMARKS, " &
                  " STATUS, DOCUMENTS, UPDATED_BY, UPDATED_DATE) values (" &
                  "" & newId & ",'" & currentUserId.ToUpper & "','" & leavesInfo.LEAVE_TYPE & "', " &
                  " TO_DATE('" & CDate(leavesInfo.LEAVE_FROM) & "','dd/mm/yyyy'),'" & leavesInfo.LEAVE_FROM_AMPM & "', " &
                  " TO_DATE('" & CDate(leavesInfo.LEAVE_TO) & "','dd/mm/yyyy'),'" & leavesInfo.LEAVE_TO_AMPM & "', " &
                  "" & leavesInfo.HIDDEN_TOTAL_LEAVE_TAKEN & ",'" & leavesInfo.REMARKS & "', " &
                  "'" & LMS_Pending_Approve_Status & "','" & leavesInfo.HIDDEN_DOCUMENT & "'," &
                  "'" & currentUserId & "', sysdate())"

            ExeData(sql, errMsg)

        Catch ex As Exception
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Sub

    Public Sub d_Update_Leaves(leavesInfo As HistoryViewModel,
                               currentUserId As String,
                               ByRef errMsg As String) Implements d_Leaves_Interface.d_Update_Leaves
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String
        Try
            sql = "update LMS_LEAVES_HISTORY set " &
                  " LEAVE_TYPE = '" & leavesInfo.LEAVE_TYPE & "', " &
                  " LEAVE_FROM = TO_DATE('" & CDate(leavesInfo.LEAVE_FROM) & "','dd/mm/yyyy'), " &
                  " LEAVE_FROM_AMPM = '" & leavesInfo.LEAVE_FROM_AMPM & "', " &
                  " LEAVE_TO = TO_DATE('" & CDate(leavesInfo.LEAVE_TO) & "','dd/mm/yyyy'), " &
                  " LEAVE_TO_AMPM = '" & leavesInfo.LEAVE_TO_AMPM & "', " &
                  " TOTAL_TAKEN_LEAVE = " & leavesInfo.HIDDEN_TOTAL_LEAVE_TAKEN & ", " &
                  " REMARKS = '" & leavesInfo.REMARKS & "', " &
                  " DOCUMENTS = '" & leavesInfo.DOCUMENTS & "', " &
                  " UPDATED_BY = '" & currentUserId & "', " &
                  " UPDATED_DATE = sysdate() " &
                  " where ID = '" & leavesInfo.HIDDEN_ID & "'"

            ExeData(sql, errMsg)

        Catch ex As Exception
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Sub

    Public Sub d_Update_Leaves_Status_Balance(leavesInfo As HistoryViewModel,
                                              currentUserId As String,
                                              id As String,
                                              status As String,
                                              ByRef errMsg As String) Implements d_Leaves_Interface.d_Update_Leaves_Status_Balance
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String
        Try
            sql = "update LMS_LEAVES_HISTORY set STATUS = '" & status & "', "
            If status = LMS_Approved_Status Or status = LMS_Canceled_Status Then
                sql = sql & " APPROVED_BY = '" & currentUserId & "', APPROVED_DATE = sysdate() "
            ElseIf status = LMS_Pending_Cancel_Status Then
                sql = sql & " UPDATED_BY = '" & currentUserId & "', UPDATED_DATE = sysdate() "
            Else
                sql = sql & " UPDATED_BY = '" & currentUserId & "', UPDATED_DATE = sysdate(), " &
                            "APPROVED_BY = NULL, APPROVED_DATE = NULL "
            End If

            sql = sql & " where ID = '" & id & "'"

            ExeData(sql, errMsg)

            If String.IsNullOrEmpty(errMsg) Then
                If leavesInfo.ISADMIN = True And
                        (status = LMS_Approved_Status Or
                        (status = LMS_Canceled_Status And
                        leavesInfo.HIDDEN_APPROVED_BY IsNot Nothing)) Then

                    sql = " update lms_users set "
                    If status = LMS_Approved_Status Then
                        Select Case leavesInfo.LEAVE_TYPE
                            Case LMS_Annual_Leave
                                sql = sql & " annual_leave_balance = " &
                                " (annual_leave_balance - " & leavesInfo.HIDDEN_TOTAL_LEAVE_TAKEN & ") "
                            Case LMS_Medical_Leave
                                sql = sql & " medical_leave_balance = " &
                               " (medical_leave_balance - " & leavesInfo.HIDDEN_TOTAL_LEAVE_TAKEN & ") "
                            Case LMS_Other_Leave
                                sql = sql & " other_leave_balance = " &
                               " (other_leave_balance - " & leavesInfo.HIDDEN_TOTAL_LEAVE_TAKEN & ") "
                        End Select
                    ElseIf status = LMS_Canceled_Status And leavesInfo.HIDDEN_APPROVED_BY IsNot Nothing Then
                        'Approve By is not nothing means previously this leave was approved 
                        'and deducted from Leave Balance. So now cancelling time, need to add those leaves
                        'back to Leave Balance
                        Select Case leavesInfo.LEAVE_TYPE
                            Case LMS_Annual_Leave
                                sql = sql & " annual_leave_balance = " &
                                " (annual_leave_balance + " & leavesInfo.HIDDEN_TOTAL_LEAVE_TAKEN & ") "
                            Case LMS_Medical_Leave
                                sql = sql & " medical_leave_balance = " &
                               " (medical_leave_balance + " & leavesInfo.HIDDEN_TOTAL_LEAVE_TAKEN & ") "
                            Case LMS_Other_Leave
                                sql = sql & " other_leave_balance = " &
                               " (other_leave_balance + " & leavesInfo.HIDDEN_TOTAL_LEAVE_TAKEN & ") "
                        End Select
                    End If

                    sql = sql & " where user_id = '" & leavesInfo.HIDDEN_USER_ID.ToUpper & "' "

                    ExeData(sql, errMsg)
                End If

            End If

        Catch ex As Exception
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Sub

    Public Function d_Check_Leaves_Valid(userId As String,
                                         leaveId As String,
                                         leaveFrom As String,
                                         leaveTo As String,
                                         ByRef errMsg As String) As Boolean Implements d_Leaves_Interface.d_Check_Leaves_Valid
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Dim sql As String
        Dim dsLeaves = Nothing
        Try
            dsLeaves = New DataSet

            sql = " select 1 as count from lms_leaves_history " &
                  " where ((to_date('" & leaveFrom & "','dd/mm/yyyy') " &
                  " between leave_from and leave_to) or " &
                  " (to_date('" & leaveTo & "','dd/mm/yyyy') " &
                  " between leave_from and leave_to)) " &
                  " and user_id = '" & userId.ToUpper & "' and status <> '" & LMS_Canceled_Status & "' "
            If leaveId IsNot Nothing Then
                'For Edit record, need to exclude own record for checking
                sql = sql & " and id <> '" & leaveId & ""
            End If

            GetData(sql, dsLeaves, errMsg)

            If Not String.IsNullOrEmpty(errMsg) Then
                Return False
            End If

            If dsLeaves.tables.count > 0 And dsLeaves.tables(0).rows.count > 0 Then
                For Each dr As DataRow In dsLeaves.tables(0).rows
                    If Not IsDBNull(dr("count")) Then
                        errMsg = "(Error) This Leave Range " & leaveFrom & "-" & leaveTo & " was applied before."
                        Return False
                    End If
                Next
            End If

            Return True
        Catch ex As Exception
            errMsg = "Error on " + FUNC_NAME + ":" + ex.ToString
        End Try
    End Function
End Class
