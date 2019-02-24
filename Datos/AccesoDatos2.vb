Imports System.Data
'Imports System.Data.SqlClient Se cambio SqlCommand por OLeDBCommand
Imports System.Data.OleDb
Public Class AccesoDatos2
    Shared _comando As OleDbCommand
    Public Shared Sub D_abrirConexion(Optional _RutaDB As String = "")
        _comando = MetodoDatos2.CrearComando("C:\BD\BDDino.accdb")
    End Sub

    Public Shared Function D_CerrarConexion() As Boolean
        Try
            Return MetodoDatos2.CerrarComando(_comando)
        Catch ex As Exception
            Return False
        End Try
    End Function

    'Base de datos BDCure
    Public Shared Function D_Datos_Tabla(_Campos As String, _Tabla As String, _Where As String) As DataTable
        D_abrirConexion()
        ''Dim _comando As OleDbCommand = MetodoDatos.CrearComando()
        Dim sql = "SELECT " + _Campos + " FROM " + _Tabla + " WHERE " + _Where
        _comando.CommandText = sql
        Return MetodoDatos2.EjecutarComandoSelect(_comando)
    End Function
    Public Shared Function D_Maximo(_Tabla As String, _Campo As String, _Where As String) As DataTable
        ''Dim _comando As OleDbCommand = MetodoDatos.CrearComando()
        _comando.CommandText = "SELECT MAX(" + _Campo + ") FROM " + _Tabla + " WHERE " + _Where
        Return MetodoDatos2.EjecutarComandoSelect(_comando)
    End Function
    Public Shared Function D_Sum(_Tabla As String, _Where As String, _Campo1 As String, Optional _Campo2 As String = "") As DataTable
        ''Dim _comando As OleDbCommand = MetodoDatos.CrearComando()
        Dim Sql As String
        Sql = "SELECT SUM(" + _Campo1 + ")"
        If _Campo2 <> "" Then
            Sql = Sql + ", SUM(" + _Campo2 + ")"
        End If
        _comando.CommandText = Sql + " FROM " + _Tabla + " WHERE " + _Where
        Return MetodoDatos2.EjecutarComandoSelect(_comando)
    End Function
    Public Shared Function D_Insertar_Datos(_Tabla As String, _Campos As String) As Boolean
        ''Dim _comando As OleDbCommand = MetodoDatos.CrearComando()
        _comando.CommandText = "Insert Into " + _Tabla + " Values(" + _Campos + ")"

        Return MetodoDatos2.EjecutarInsert(_comando)

    End Function
    Public Shared Function D_Modificar_Datos(_Tabla As String, _Campos As String, _Where As String) As Boolean
        ''Dim _comando As OleDbCommand = MetodoDatos.CrearComando()
        _comando.CommandText = "Update " + _Tabla + " Set " + _Campos + " Where " + _Where

        Return MetodoDatos2.EjecutarInsert(_comando)

    End Function
    Public Shared Function D_Eliminar_Datos(_Tabla As String, _Where As String) As Boolean
        ''Dim _comando As OleDbCommand = MetodoDatos.CrearComando()
        _comando.CommandText = "Delete From " + _Tabla + " Where " + _Where

        Return MetodoDatos2.EjecutarInsert(_comando)
    End Function
    Public Shared Function D_Count_Tabla(_Tabla As String, _where As String) As DataTable
        ''Dim _comando As OleDbCommand = MetodoDatos.CrearComando()
        _comando.CommandText = "SELECT COUNT(*) FROM " + _Tabla + " WHERE " + _where
        Return MetodoDatos2.EjecutarComandoSelect(_comando)
    End Function

    'Base de datos inventario
    Public Shared Function D_Datos_Tabla2(_Campos As String, _Tabla As String, _Where As String) As DataTable
        ''Dim _comando As OleDbCommand = MetodoDatos.CrearComando2()
        _comando.CommandText = "SELECT " + _Campos + " FROM " + _Tabla + " WHERE " + _Where
        Return MetodoDatos2.EjecutarComandoSelect(_comando)
    End Function
    Public Shared Function D_Count_Tabla2(_Tabla As String, _where As String) As DataTable
        ''Dim _comando As OleDbCommand = MetodoDatos.CrearComando2()
        _comando.CommandText = "SELECT COUNT(*) FROM " + _Tabla + " WHERE " + _where
        Return MetodoDatos2.EjecutarComandoSelect(_comando)
    End Function
End Class
