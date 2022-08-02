Imports LMS.ViewModels
Public Interface d_LMS_User_Interface
    Sub d_Get_LMS_Users(ByVal strId As String,
                             ByVal strUserId As String,
                             ByRef errMsg As String,
                             ByRef dsUser As DataSet)
    Sub d_Get_MaxID(ByRef maxId As Integer,
                    ByRef errMsg As String)
    Sub d_Delete_LMS_User(ByVal strId As String,
                       ByRef errMsg As String)
    Sub d_Insert_LMS_User(ByVal userInfo As UsersViewModel,
                              ByVal newId As Integer,
                              ByVal currentUserId As String,
                              ByRef errMsg As String)
    Sub d_Update_LMS_User(ByVal userInfo As UsersViewModel,
                       ByVal userId As String,
                       ByVal currentUserId As String,
                       ByVal updatePWDFlag As String,
                       ByRef errMsg As String)
End Interface
