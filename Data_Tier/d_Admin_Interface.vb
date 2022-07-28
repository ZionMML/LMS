Imports LMS.ViewModels

Public Interface d_Admin_Interface

    Sub d_Get_Admin_Users(ByVal strId As String,
                          ByVal strUserId As String,
                          ByRef errMsg As String,
                          ByRef dsAdmin As DataSet)
    Sub d_Get_MaxID(ByRef maxId As Integer,
                    ByRef errMsg As String)
    Sub d_Delete_Admin(ByVal strId As String,
                       ByRef errMsg As String)
    Sub d_Insert_Admin(ByVal adminInfo As AdminsViewModel,
                              ByVal newId As Integer,
                              ByVal currentUserId As String,
                              ByRef errMsg As String)
    Sub d_Update_Admin(ByVal adminInfo As AdminsViewModel,
                       ByVal userId As String,
                       ByVal currentUserId As String,
                       ByRef errMsg As String)
End Interface
