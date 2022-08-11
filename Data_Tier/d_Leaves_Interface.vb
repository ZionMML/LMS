Imports LMS.ViewModels

Public Interface d_Leaves_Interface
    Sub d_Get_Leaves_Inquiry(ByVal startDt As String,
                             ByVal endDt As String,
                             ByRef errMsg As String,
                             ByRef dsLeaves As DataSet)
    Sub d_Get_Leaves_User_Summary(ByVal isAdmin As Boolean,
                                  ByVal currentUserId As String,
                                  ByRef errMsg As String,
                                  ByRef dsLeavesUser As DataSet)

    Sub d_Get_Leaves(ByVal id As String,
                     ByRef errMsg As String,
                     ByRef dsLeaves As DataSet)
    Sub d_Get_MaxID(ByRef maxId As Integer,
                    ByRef errMsg As String)
    Sub d_Get_Approved_Leaves(ByRef errMsg As String,
                                ByRef dsLeaves As DataSet)

    Sub d_Get_Leaves_Balance(ByVal leave_type As String,
                             ByVal user_id As String,
                             ByRef leaveBalance As Double,
                             ByRef errMsg As String)
    Sub d_Insert_Leaves(ByVal leavesInfo As HistoryViewModel,
                              ByVal newId As Integer,
                              ByVal currentUserId As String,
                              ByRef errMsg As String)
    Sub d_Update_Leaves(ByVal leavesInfo As HistoryViewModel,
                        ByVal currentUserId As String,
                        ByRef errMsg As String)

    Sub d_Update_Leaves_Status_Balance(ByVal leavesInfo As HistoryViewModel,
                        ByVal currentUserId As String,
                        ByVal id As String,
                        ByVal status As String,
                        ByRef errMsg As String)

    Function d_Check_Leaves_Valid(ByVal userId As String,
                        ByVal leaveId As String,
                        ByVal leaveFrom As String,
                        ByVal leaveTo As String,
                        ByRef errMsg As String) As Boolean
End Interface
