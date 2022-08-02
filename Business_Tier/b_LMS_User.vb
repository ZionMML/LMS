Imports LMS.ViewModels

Public Class b_LMS_User
    Implements b_LMS_User_Interface

    Private ClsDObj As d_LMS_User
    Private intfDObj As d_LMS_User_Interface

    Public Sub b_Get_LMS_Users(strId As String,
                               strUserId As String,
                               ByRef errMsg As String,
                               ByRef dsUser As DataSet) Implements b_LMS_User_Interface.b_Get_LMS_Users
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_LMS_User
                intfDObj = ClsDObj
            End If

            intfDObj.d_Get_LMS_Users(strId,
                                       strUserId,
                                       errMsg,
                                       dsUser)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub

    Public Sub b_Get_MaxID(ByRef maxId As Integer, ByRef errMsg As String) Implements b_LMS_User_Interface.b_Get_MaxID
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_LMS_User
                intfDObj = ClsDObj
            End If

            intfDObj.d_Get_MaxID(maxId,
                                 errMsg)

        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub

    Public Sub b_Delete_LMS_User(strId As String, ByRef errMsg As String) Implements b_LMS_User_Interface.b_Delete_LMS_User
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_LMS_User
                intfDObj = ClsDObj
            End If

            intfDObj.d_Delete_LMS_User(strId,
                                       errMsg)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub

    Public Sub b_Insert_LMS_User(userInfo As UsersViewModel,
                                 newId As Integer,
                                 currentUserId As String,
                                 ByRef errMsg As String) Implements b_LMS_User_Interface.b_Insert_LMS_User
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_LMS_User
                intfDObj = ClsDObj
            End If

            intfDObj.d_Insert_LMS_User(userInfo,
                                       newId,
                                       currentUserId,
                                       errMsg)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub

    Public Sub b_Update_LMS_User(userInfo As UsersViewModel,
                                 userId As String,
                                 currentUserId As String,
                                 updatePWDFlag As String,
                                 ByRef errMsg As String) Implements b_LMS_User_Interface.b_Update_LMS_User
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_LMS_User
                intfDObj = ClsDObj
            End If

            intfDObj.d_Update_LMS_User(userInfo,
                                       userId,
                                       currentUserId,
                                       updatePWDFlag,
                                       errMsg)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub
End Class
