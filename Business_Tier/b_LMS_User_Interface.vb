Imports LMS.ViewModels

Public Interface b_LMS_User_Interface
    Sub b_Get_LMS_Users(ByVal strId As String,
                            ByVal strUserId As String,
                            ByRef errMsg As String,
                            ByRef dsUser As DataSet)
    Sub b_Get_MaxID(ByRef maxId As Integer,
                    ByRef errMsg As String)
    Sub b_Delete_LMS_User(ByVal strId As String,
                       ByRef errMsg As String)
    Sub b_Insert_LMS_User(ByVal userInfo As UsersViewModel,
                              ByVal newId As Integer,
                              ByVal currentUserId As String,
                              ByRef errMsg As String)
    Sub b_Update_LMS_User(ByVal userInfo As UsersViewModel,
                       ByVal userId As String,
                       ByVal currentUserId As String,
                       ByVal updatePWDFlag As String,
                       ByRef errMsg As String)
End Interface
