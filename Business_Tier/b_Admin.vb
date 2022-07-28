Imports LMS.ViewModels

Public Class b_Admin
    Implements b_Admin_Interface
    Private ClsDObj As d_Admin
    Private intfDObj As d_Admin_Interface

    Public Sub b_Get_Admin_Users(strId As String,
                                 strUserId As String,
                                 ByRef errMsg As String,
                                 ByRef dsAdmin As DataSet) Implements b_Admin_Interface.b_Get_Admin_Users
        'Throw New NotImplementedException()
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_Admin
                intfDObj = ClsDObj
            End If

            intfDObj.d_Get_Admin_Users(strId,
                                       strUserId,
                                       errMsg,
                                       dsAdmin)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub

    Sub b_Get_MaxID(ByRef maxId As Integer,
                    ByRef errMsg As String) Implements b_Admin_Interface.b_Get_MaxID
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_Admin
                intfDObj = ClsDObj
            End If

            intfDObj.d_Get_MaxID(maxId,
                                 errMsg)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub

    Public Sub b_Delete_Admin(strId As String,
                              ByRef errMsg As String) Implements b_Admin_Interface.b_Delete_Admin
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_Admin
                intfDObj = ClsDObj
            End If

            intfDObj.d_Delete_Admin(strId,
                                    errMsg)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub

    Sub b_Insert_Admin(ByVal adminInfo As AdminsViewModel,
                       ByVal newId As Integer,
                       ByVal currentUserId As String,
                       ByRef errMsg As String) Implements b_Admin_Interface.b_Insert_Admin
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_Admin
                intfDObj = ClsDObj
            End If

            intfDObj.d_Insert_Admin(adminInfo,
                                    newId,
                                    currentUserId,
                                    errMsg)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try

    End Sub
    Public Sub b_Update_Admin(adminInfo As AdminsViewModel,
                              userId As String,
                              currentUserId As String,
                              ByRef errMsg As String) Implements b_Admin_Interface.b_Update_Admin
        Dim FUNC_NAME As String = Reflection.MethodBase.GetCurrentMethod.Name
        Try
            If ClsDObj Is Nothing Then
                ClsDObj = New d_Admin
                intfDObj = ClsDObj
            End If

            intfDObj.d_Update_Admin(adminInfo,
                                    userId,
                                    currentUserId,
                                    errMsg)
        Catch ex As Exception
            errMsg = FUNC_NAME & ex.ToString
        End Try
    End Sub
End Class
