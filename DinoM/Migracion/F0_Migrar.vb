Imports Logica.AccesoLogica
Public Class F0_Migrar
    Dim _BindingSource, _BindingSource1 As BindingSource
    Public Sub _prCargarClientes()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, "DBDinoM_Mario")
        Dim _Ds, _Ds1 As New DataSet
        _Ds = L_PersonasGeneral(1, " ydcli = 1")
        '_Ds = L_PersonasGeneral(1, " ydven = 1")
        _BindingSource = New BindingSource
        _BindingSource.DataSource = _Ds.Tables(0).DefaultView
        Dim dt As DataTable = _Ds.Tables(0).DefaultView.ToTable
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            'ByRef _ydnumi As String,
            '                                  _ydcod As String, _ydrazonsocial As String, _yddesc As String,
            '                                  _ydnumiVendedor As Integer, _ydzona As Integer, _yddct As Integer,
            '                                  _yddctnum As String, _yddirec As String, _ydtelf1 As String,
            '                                  _ydtelf2 As String, _ydcat As Integer, _ydest As Integer,
            '                                  _ydlat As Double, _ydlongi As Double, _ydobs As String,
            '                                  _ydfnac As String, _ydnomfac As String, _ydtip As Integer,
            '                                  _ydnit As String, _yddias As String, _ydlcred As String,
            '                                  _ydfecing As String, _ydultvent As String, _ydimg As String, _ydrut As String
            Dim bandera As Boolean = L_fnGrabarCLiente("0", dt.Rows(i).Item("ydper"), "", dt.Rows(i).Item("ydnom1"), dt.Rows(i).Item("ydclp"), dt.Rows(i).Item("ydcity"), dt.Rows(i).Item("ydtipd"), IIf(IsDBNull(dt.Rows(i).Item("ydnumd")), "", dt.Rows(i).Item("ydnumd")), IIf(IsDBNull(dt.Rows(i).Item("yddirec")), "", dt.Rows(i).Item("yddirec")), IIf(IsDBNull(dt.Rows(i).Item("ydtelef")), "", dt.Rows(i).Item("ydtelef")), IIf(IsDBNull(dt.Rows(i).Item("ydcelu")), "", dt.Rows(i).Item("ydcelu")), 0, 1, 0, 0, "", "01/01/1990", dt.Rows(i).Item("ydnom1"), 1, "", "", "", "01/01/2000", "01/01/2000", "Default.jpg", "", 0, 0, 0)


        Next

    End Sub

    Public Sub _prCargarProductos()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, "DBDinoM_Mario")
        Dim _Ds, _Ds1 As New DataSet
        _Ds = L_ProductosGeneral(0, "")
        _BindingSource = New BindingSource
        _BindingSource.DataSource = _Ds.Tables(0).DefaultView
        Dim dt As DataTable = _Ds.Tables(0).DefaultView.ToTable
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            'ByRef _yfnumi As String, _yfcprod As String,
            '                                  _yfcbarra As String, _yfcdprod1 As String,
            '                                  _yfcdprod2 As String, _yfgr1 As Integer, _yfgr2 As Integer,
            '                                  _yfgr3 As Integer, _yfgr4 As Integer, _yfMed As Integer, _yfumin As Integer, _yfusup As Integer, _yfvsup As Double, _yfsmin As Integer, _yfap As Integer, _yfimg As String

            Dim bandera As Boolean = L_fnGrabarProducto(dt.Rows(i).Item("yfnumi"), dt.Rows(i).Item("yfcprod"), "", dt.Rows(i).Item("yfcdprod1"), "", dt.Rows(i).Item("yfcomp"), dt.Rows(i).Item("yfacc"), dt.Rows(i).Item("yfpresen"), dt.Rows(i).Item("yflab"), dt.Rows(i).Item("yfcmed"), dt.Rows(i).Item("yfcmed"), dt.Rows(i).Item("yfcmed"), dt.Rows(i).Item("yfvsup"), dt.Rows(i).Item("yfsmin"), IIf(dt.Rows(i).Item("yfap") = "A", 1, 0), "Default.jpg")


        Next

    End Sub
    Public Sub _prModificarPrecios()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, "DBDinoM_Mario")
        Dim dtalmacen As DataTable = L_fnGeneralSucursales()

        For i As Integer = 0 To dtalmacen.Rows.Count - 1 Step 1
            Dim dtprecio As DataTable = L_fnListarProductosConPrecios(dtalmacen.Rows(i).Item("aanumi"))
            '' yhnumi,yhprod,yhcatpre,ygcod,yhprecio,estado
            Dim _DsDetalle As DataTable = L_ObtenerPreciosProductoDeAlmacen(dtalmacen.Rows(i).Item("aanumi"))

            For j As Integer = 0 To _DsDetalle.Rows.Count - 1 Step 1
                Dim codprod As Integer = _DsDetalle.Rows(j).Item("yfcprod")
                Dim k As Integer = 0
                Dim bandera As Boolean = False
                While (k < dtprecio.Rows.Count - 1 And bandera = False)
                    If (codprod = dtprecio.Rows(k).Item("yhprod")) Then
                        bandera = True
                    Else
                        k = k + 1

                    End If

                End While
                dtprecio.Rows(k).Item("yhprecio") = _DsDetalle.Rows(j).Item("ylpcostoul") ''1099
                dtprecio.Rows(k + 1).Item("yhprecio") = _DsDetalle.Rows(j).Item("ylpventaul")  ''70
                dtprecio.Rows(k).Item("estado") = 2
                dtprecio.Rows(k + 1).Item("estado") = 2
            Next
            Dim grabar As Boolean = L_fnGrabarPrecios("", dtalmacen.Rows(i).Item("aanumi"), dtprecio)
        Next



    End Sub
    Private Sub F0_Migrar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '_prCargarProductos()
        '_prCargarClientes()
    End Sub
End Class