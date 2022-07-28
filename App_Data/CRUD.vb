Imports mysql.data.mysqlclient
Public Module CRUD
    Public Function strConnection() As MySqlConnection
        Return New MySqlConnection("server=localhost;uid=hbstudent;pwd=hbstudent;database=lms")
    End Function

    Public strConn As MySqlConnection = strConnection()

    Public result As String
    Public cmd As New MySqlCommand
    Public da As New MySqlDataAdapter
    Public dt As New DataTable

    Public Sub GetData(ByVal sql As String, ByVal ds As DataSet, ByRef errMsg As String)
        Try
            dt = New DataTable
            'ds = New DataSet()
            strConn.Open()
            With cmd
                .Connection = strConn
                .CommandText = sql
                da.SelectCommand = cmd
                da.Fill(dt)
                ds.Tables.Add(dt)
            End With
        Catch ex As Exception
            errMsg = "GetData Error:" + ex.ToString
        Finally
            strConn.Close()
            da.Dispose()
        End Try
    End Sub

    Public Sub ExeData(ByVal sql As String, ByRef errMsg As String)
        Try
            strConn.Open()
            With cmd
                .Connection = strConn
                .CommandText = sql
                result = cmd.ExecuteNonQuery
                If result = 1 Then
                    errMsg = "Data processed successfully."
                Else
                    errMsg = "Data process failed."
                End If
            End With
        Catch ex As Exception
            errMsg = "GetData Error:" + ex.ToString
        Finally
            strConn.Close()
            da.Dispose()
        End Try
    End Sub
End Module
