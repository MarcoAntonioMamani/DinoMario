Imports System.Data
'Imports System.Data.SqlClient   'Lib para conexion con SQL Server
Imports System.Data.OleDb       'Lib para conexion con Access
Public Class MetodoDatos2

    Public Shared Function CrearComando(Optional _RutaDB As String = "") As OleDbCommand 'SqlCommand
        Dim _cadenaConexion = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + _RutaDB + ";Persist Security Info=False;"
        Dim _conexion As New OleDbConnection() 'SqlConnection()
        _conexion.ConnectionString = _cadenaConexion
        Dim _comando As New OleDbCommand() 'SqlCommand()
        _comando = _conexion.CreateCommand()
        _comando.CommandType = CommandType.Text
        'abrir
        _comando.Connection.Open()
        Return _comando
    End Function

    Public Shared Function CerrarComando(_comando As OleDbCommand) As Boolean
        Try
            _comando.Connection.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    'Public Shared Function CrearComando2() As OleDbCommand 'SqlCommand
    '    Dim _cadenaConexion = Configuracion.CadenaConexion
    '    Dim _conexion As New OleDbConnection() 'SqlConnection()
    '    _conexion.ConnectionString = _cadenaConexion
    '    Dim _comando As New OleDbCommand() 'SqlCommand()
    '    _comando = _conexion.CreateCommand()
    '    _comando.CommandType = CommandType.Text
    '    Return _comando
    'End Function
    Public Shared Function EjecutarComandoSelect(Comando As OleDbCommand) As DataTable
        Dim _tabla As New DataTable()
        Try
            'Comando.Connection.Open()
            Dim _adaptador As New OleDbDataAdapter 'SqlDataAdapter()
            _adaptador.SelectCommand = Comando

            _adaptador.Fill(_tabla)
        Catch ex As Exception
            MsgBox(ex.Message)
            'Finally
            'Comando.Connection.Close()
        End Try
        Return _tabla
    End Function
    Public Shared Function EjecutarInsert(Comando As OleDbCommand) As Boolean
        Dim _tabla As New DataTable()
        Dim _Err As Boolean = False

        Try
            'Comando.Connection.Open()
            Comando.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
            _Err = True
            'Finally
            'Comando.Connection.Close()
        End Try
        Return _Err
    End Function
End Class
