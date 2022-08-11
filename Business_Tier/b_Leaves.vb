Imports LMS.ViewModels

Public Class b_Leaves
    Implements b_Leaves_Interface

    Private ClsDObj As d_Leaves
    Private intfDObj As d_Leaves_Interface
    Public Sub b_Get_Leaves_Inquiry(startDt As String,
                                    endDt As String,
                                    ByRef errMsg As String,
                                    ByRef dsLeaves As DataSet) Implements b_Leaves_Interface.b_Get_Leaves_Inquiry
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_Leaves
                intfDObj = ClsDObj
            End If

            intfDObj.d_Get_Leaves_Inquiry(startDt,
                                       endDt,
                                       errMsg,
                                       dsLeaves)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub

    Public Sub b_Get_Leaves_User_Summary(isAdmin As Boolean,
                                         currentUserId As String,
                                         ByRef errMsg As String,
                                         ByRef dsLeavesUser As DataSet) Implements b_Leaves_Interface.b_Get_Leaves_User_Summary
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_Leaves
                intfDObj = ClsDObj
            End If

            intfDObj.d_Get_Leaves_User_Summary(isAdmin,
                                       currentUserId,
                                       errMsg,
                                       dsLeavesUser)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub

    Public Sub b_Get_Leaves(id As String,
                            ByRef errMsg As String,
                            ByRef dsLeaves As DataSet) Implements b_Leaves_Interface.b_Get_Leaves
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_Leaves
                intfDObj = ClsDObj
            End If

            intfDObj.d_Get_Leaves(id,
                                  errMsg,
                                  dsLeaves)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub

    Public Sub b_Get_MaxID(ByRef maxId As Integer, ByRef errMsg As String) Implements b_Leaves_Interface.b_Get_MaxID
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_Leaves
                intfDObj = ClsDObj
            End If

            intfDObj.d_Get_MaxID(maxId,
                                 errMsg)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub

    Public Sub b_Get_Approveb_Leaves(ByRef errMsg As String,
                                     ByRef dsLeaves As DataSet) Implements b_Leaves_Interface.b_Get_Approveb_Leaves
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_Leaves
                intfDObj = ClsDObj
            End If

            intfDObj.d_Get_Approved_Leaves(errMsg,
                                            dsLeaves)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub

    Public Sub b_Get_Leaves_Balance(leave_type As String,
                                    user_id As String,
                                    ByRef leaveBalance As Double,
                                    ByRef errMsg As String) Implements b_Leaves_Interface.b_Get_Leaves_Balance
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_Leaves
                intfDObj = ClsDObj
            End If

            intfDObj.d_Get_Leaves_Balance(leave_type,
                                          user_id,
                                          leaveBalance,
                                           errMsg)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub

    Public Sub b_Insert_Leaves(leavesInfo As HistoryViewModel,
                               newId As Integer,
                               currentUserId As String,
                               ByRef errMsg As String) Implements b_Leaves_Interface.b_Insert_Leaves
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_Leaves
                intfDObj = ClsDObj
            End If

            intfDObj.d_Insert_Leaves(leavesInfo,
                                     newId,
                                     currentUserId,
                                     errMsg)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub

    Public Sub b_Update_Leaves(leavesInfo As HistoryViewModel,
                               currentUserId As String,
                               ByRef errMsg As String) Implements b_Leaves_Interface.b_Update_Leaves
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_Leaves
                intfDObj = ClsDObj
            End If

            intfDObj.d_Update_Leaves(leavesInfo,
                                     currentUserId,
                                     errMsg)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub

    Public Sub b_Update_Leaves_Status_Balance(leavesInfo As HistoryViewModel,
                                              currentUserId As String,
                                              id As String,
                                              status As String,
                                              ByRef errMsg As String) Implements b_Leaves_Interface.b_Update_Leaves_Status_Balance
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_Leaves
                intfDObj = ClsDObj
            End If

            intfDObj.d_Update_Leaves_Status_Balance(leavesInfo,
                                     currentUserId,
                                     id,
                                     status,
                                     errMsg)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub

    Public Function b_Check_Leaves_Valid(userId As String,
                                         leaveId As String,
                                         leaveFrom As String,
                                         leaveTo As String,
                                         ByRef errMsg As String) As Boolean Implements b_Leaves_Interface.b_Check_Leaves_Valid
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_Leaves
                intfDObj = ClsDObj
            End If

            intfDObj.d_Check_Leaves_Valid(userId,
                                     leaveId,
                                     leaveFrom,
                                     leaveTo,
                                     errMsg)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Function
End Class
