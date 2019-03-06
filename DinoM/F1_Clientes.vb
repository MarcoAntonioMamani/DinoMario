
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX

Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid

Imports GMap.NET.MapProviders
Imports GMap.NET
Imports GMap.NET.WindowsForms.Markers
Imports GMap.NET.WindowsForms
Imports GMap.NET.WindowsForms.ToolTips
Imports DevComponents.DotNetBar.Controls


Public Class F1_Clientes

#Region "Variables Locales"
#Region "MApas"
    Dim _Punto As Integer
    Dim _ListPuntos As List(Of PointLatLng)
    Dim _Overlay As GMapOverlay
    Dim _latitud As Double = 0
    Dim _longitud As Double = 0
#End Region
    Dim RutaGlobal As String = gs_CarpetaRaiz
    Dim RutaTemporal As String = "C:\Temporal"
    Dim Modificado As Boolean = False
    Dim nameImg As String = "Default.jpg"
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Public _Tipo As Integer
    Dim NumiVendedor As Integer
#End Region
#Region "Metodos Privados"

    Private Sub _prIniciarTodo()

        Me.Text = "CLIENTES"
        _prMaxLength()
        '_prCargarComboLibreriaZona(cbZona)
        _prCargarComboLibreria(cbZona, 3, 3)
        _prCargarComboLibreria(cbCiudad, 3, 1)
        _prCargarComboLibreria(cbProvincia, 3, 2)
        _prCargarComboLibreriaSucursal(cbSucursal)
        _prAsignarPermisos()
        _PMIniciarTodo()


        Dim blah As New Bitmap(New Bitmap(My.Resources.man_18), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        cbZona.ReadOnly = True
        JGrM_Buscador.FilterMode = FilterMode.None
        tbtitulo.Focus()
    End Sub
    Private Sub _prCargarComboLibreriaSucursal(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarSucursales()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("aanumi").Width = 60
            .DropDownList.Columns("aanumi").Caption = "COD"
            .DropDownList.Columns.Add("aabdes").Width = 500
            .DropDownList.Columns("aabdes").Caption = "SUCURSAL"
            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = dt
            .Refresh()
        End With
    End Sub



    Public Sub _prStyleJanus()
        GroupPanelBuscador.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.TextColor = Color.White
        JGrM_Buscador.RootTable.HeaderFormatStyle.FontBold = TriState.True
    End Sub

    Public Sub _prMaxLength()

        tbNombre.MaxLength = 50
        tbDireccion.MaxLength = 200
        tbTelf1.MaxLength = 50
    End Sub

    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaClienteLGeneral(cod1, cod2)
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 70
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("ycdes3").Width = 200
            .DropDownList.Columns("ycdes3").Caption = "DESCRIPCION"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub _prCargarComboLibreriaZona(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_prLibreriaClienteLGeneralZonas()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 60
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("ycdes3").Width = 500
            .DropDownList.Columns("ycdes3").Caption = "DESCRIPCION"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
    End Sub

    Private Sub _prCargarComboLibreriaFecuenciaVisita(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_prLibreriaClienteLGeneralFrecVisita()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 60
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("ycdes3").Width = 500
            .DropDownList.Columns("ycdes3").Caption = "DESCRIPCION"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub _prCargarComboLibreriaCategoriaPrecios(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_prLibreriaClienteLGeneralPrecios()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("ygnumi").Width = 60
            .DropDownList.Columns("ygnumi").Caption = "COD"
            .DropDownList.Columns.Add("ygdesc").Width = 500
            .DropDownList.Columns("ygdesc").Caption = "DESCRIPCION"
            .ValueMember = "ygnumi"
            .DisplayMember = "ygdesc"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub _prAsignarPermisos()

        Dim dtRolUsu As DataTable = L_prRolDetalleGeneral(gi_userRol, _nameButton)

        Dim show As Boolean = dtRolUsu.Rows(0).Item("ycshow")
        Dim add As Boolean = dtRolUsu.Rows(0).Item("ycadd")
        Dim modif As Boolean = dtRolUsu.Rows(0).Item("ycmod")
        Dim del As Boolean = dtRolUsu.Rows(0).Item("ycdel")

        If add = False Then
            btnNuevo.Visible = False
        End If
        If modif = False Then
            btnModificar.Visible = False
        End If
        If del = False Then
            btnEliminar.Visible = False
        End If
    End Sub
    Private Sub _prCrearCarpetaTemporal()

        If System.IO.Directory.Exists(RutaTemporal) = False Then
            System.IO.Directory.CreateDirectory(RutaTemporal)
        Else
            Try
                My.Computer.FileSystem.DeleteDirectory(RutaTemporal, FileIO.DeleteDirectoryOption.DeleteAllContents)
                My.Computer.FileSystem.CreateDirectory(RutaTemporal)
                'My.Computer.FileSystem.DeleteDirectory(RutaTemporal, FileIO.UIOption.AllDialogs, FileIO.RecycleOption.SendToRecycleBin)
                'System.IO.Directory.CreateDirectory(RutaTemporal)

            Catch ex As Exception

            End Try

        End If

    End Sub
    Private Sub _prCrearCarpetaImagenes()
        Dim rutaDestino As String = RutaGlobal + "\Imagenes\Imagenes ClienteDino\"

        If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes ClienteDino\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Imagenes") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes")
                If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes ClienteDino") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes\Imagenes ClienteDino")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes ClienteDino") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes\Imagenes ClienteDino")

                End If
            End If
        End If
    End Sub

    Private Sub _prCrearCarpetaReportes()
        Dim rutaDestino As String = RutaGlobal + "\Reporte\Reporte ClienteDino\"

        If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte ClienteDino\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Reporte") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte")
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte ClienteDino") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte ClienteDino")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte ClienteDino") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte ClienteDino")

                End If
            End If
        End If
    End Sub

#End Region
#Region "METODOS SOBRECARGADOS"

    Public Overrides Sub _PMOHabilitar()

        cbCiudad.ReadOnly = False
        btnSearch.Visible = True
        tbNombre.ReadOnly = False
        tbDireccion.ReadOnly = False
        tbTelf1.ReadOnly = False
        cbProvincia.ReadOnly = False
        cbSucursal.ReadOnly = False
        cbZona.ReadOnly = False
        tbFnac.Enabled = True
        tbFIngr.Enabled = True
        tbUltVenta.Enabled = True
        Tbdias.ReadOnly = False
        TbLCred.ReadOnly = False
        swEstado.IsReadOnly = False
        _prCrearCarpetaImagenes()
        _prCrearCarpetaTemporal()


        cbSucursal.Focus()
        ''  SuperTabItem1.Visible =True 
    End Sub

    Public Overrides Sub _PMOInhabilitar()
        cbProvincia.ReadOnly = True
        tbCodigoOriginal.ReadOnly = True
        tbCodCliente.ReadOnly = True
        tbNombre.ReadOnly = True
        tbDireccion.ReadOnly = True
        tbTelf1.ReadOnly = True
        cbCiudad.ReadOnly = True
        cbSucursal.ReadOnly = True
        cbZona.ReadOnly = True
        tbFnac.Enabled = False
        tbFIngr.Enabled = False
        tbUltVenta.Enabled = False
        Tbdias.ReadOnly = True
        TbLCred.ReadOnly = True
        swEstado.IsReadOnly = True
        _prStyleJanus()
        JGrM_Buscador.Focus()
        btnSearch.Visible = False
        ' SuperTabItem1.Visible = False
    End Sub

    Public Overrides Sub _PMOLimpiar()
        tbCodigoOriginal.Clear()
        tbCodCliente.Clear()
        tbNombre.Clear()
        tbDireccion.Clear()
        tbTelf1.Clear()

        'cbCatPrec.SelectedIndex = -1
        'cbZona.SelectedIndex = -1
        'cbTipoDoc.SelectedIndex = -1

        tbFnac.Value = Now.Date
        tbFIngr.Value = Now.Date
        tbUltVenta.Value = Now.Date

        Tbdias.Text = 0
        TbLCred.Text = 0
        swEstado.Value = True


        _latitud = 0
        _longitud = 0
        If (cbProvincia.SelectedIndex < 0) Then

            If (CType(cbProvincia.DataSource, DataTable).Rows.Count > 0) Then
                cbProvincia.SelectedIndex = 0
            End If
        End If
        If (cbZona.SelectedIndex < 0) Then

            If (CType(cbZona.DataSource, DataTable).Rows.Count > 0) Then
                cbZona.SelectedIndex = 0
            End If
        End If
        If (cbCiudad.SelectedIndex < 0) Then

            If (CType(cbCiudad.DataSource, DataTable).Rows.Count > 0) Then
                cbCiudad.SelectedIndex = 0
            End If
        End If
        If (cbSucursal.SelectedIndex < 0) Then

            If (CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
                cbSucursal.SelectedIndex = 0
            End If
        End If
        NumiVendedor = 0
        tbVendedor.Clear()
        Dim dt As DataTable = L_fnNumiClientes()
        If (Not IsDBNull(dt)) Then
            If (dt.Rows.Count > 0) Then
                tbCodCliente.Text = dt.Rows(0).Item(0)
            End If
        End If
        cbSucursal.Focus()
    End Sub

    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbNombre.BackColor = Color.White
        tbDireccion.BackColor = Color.White
    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean

        'ByRef _ydnumi As String, _ydcod As String,
        '                                       _yddesc As String, _ydzona As Integer,
        '                                       _yddct As Integer, _yddctnum As String,
        '                                       _yddirec As String, _ydtelf1 As String,
        '                                       _ydtelf2 As String, _ydcat As Integer, _ydest As Integer, _ydlat As Double, _ydlongi As Double, _ydobs As String,
        '                                       _ydfnac As String, _ydnomfac As String,
        '                                       _ydtip As Integer, _ydnit As String, _ydfecing As String, _ydultvent As String, _ydimg As String

        Dim res As Boolean = L_fnGrabarCLiente(tbCodigoOriginal.Text, tbCodCliente.Text, tbNombre.Text, tbNombre.Text, NumiVendedor, cbZona.Value, 1, "0", tbDireccion.Text, tbTelf1.Text, "", 0, IIf(swEstado.Value = True, 1, 0), _latitud, _longitud, "", tbFnac.Value.ToString("yyyy/MM/dd"), "", _Tipo, "", Tbdias.Text, TbLCred.Text, tbFIngr.Value.ToString("yyyy/MM/dd"), tbUltVenta.Value.ToString("yyyy/MM/dd"), nameImg, 1, cbCiudad.Value, cbProvincia.Value, cbSucursal.Value)


        If res Then
            'Modificado = False
            '_fnMoverImagenRuta(RutaGlobal + "\Imagenes\Imagenes ClienteDino", nameImg)
            'nameImg = "Default.jpg"

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Cliente ".ToUpper + tbCodigoOriginal.Text + " Grabado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
            tbNombre.Focus()

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El producto no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
        Return res

    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim res As Boolean

        Dim nameImage As String = JGrM_Buscador.GetValue("ydimg")
        If (Modificado = False) Then
            res = L_fnModificarClientes(tbCodigoOriginal.Text, tbCodCliente.Text, tbNombre.Text, tbNombre.Text, NumiVendedor, cbZona.Value, 1, "0", tbDireccion.Text, tbTelf1.Text, "", 0, IIf(swEstado.Value = True, 1, 0), _latitud, _longitud, "", tbFnac.Value.ToString("yyyy/MM/dd"), "", _Tipo, "", Tbdias.Text, TbLCred.Text, tbFIngr.Value.ToString("yyyy/MM/dd"), tbUltVenta.Value.ToString("yyyy/MM/dd"), nameImg, 1, cbCiudad.Value, cbProvincia.Value, cbSucursal.Value)

        Else
            res = L_fnModificarClientes(tbCodigoOriginal.Text, tbCodCliente.Text, tbNombre.Text, tbNombre.Text, NumiVendedor, cbZona.Value, 1, "0", tbDireccion.Text, tbTelf1.Text, "", 0, IIf(swEstado.Value = True, 1, 0), _latitud, _longitud, "", tbFnac.Value.ToString("yyyy/MM/dd"), "", _Tipo, "", Tbdias.Text, TbLCred.Text, tbFIngr.Value.ToString("yyyy/MM/dd"), tbUltVenta.Value.ToString("yyyy/MM/dd"), nameImg, 1, cbCiudad.Value, cbProvincia.Value, cbSucursal.Value)

        End If
        If res Then

            'If (Modificado = True) Then
            '    _fnMoverImagenRuta(RutaGlobal + "\Imagenes\Imagenes ClienteDino", nameImg)
            '    Modificado = False
            'End If
            'nameImg = "Default.jpg"

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Cliente ".ToUpper + tbCodigoOriginal.Text + " modificado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter)

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "EL Cliente no pudo ser modificado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
        Return res
    End Function



    Public Function _fnActionNuevo() As Boolean
        Return tbCodigoOriginal.Text = String.Empty And tbDireccion.ReadOnly = False
    End Function

    Public Overrides Sub _PMOEliminarRegistro()

        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
        ef.Header = "mensaje principal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_fnEliminarClientes(tbCodigoOriginal.Text, mensajeError)
            If res Then


                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                ToastNotification.Show(Me, "Código de Cliente ".ToUpper + tbCodigoOriginal.Text + " eliminado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

                _PMFiltrar()
            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        End If


    End Sub
    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If tbNombre.Text = String.Empty Then
            tbNombre.BackColor = Color.Red
            MEP.SetError(tbNombre, "ingrese el nombre del Usuario!".ToUpper)
            _ok = False
        Else
            tbNombre.BackColor = Color.White
            MEP.SetError(tbNombre, "")
        End If
        If (cbProvincia.SelectedIndex < 0) Then

            If (CType(cbProvincia.DataSource, DataTable).Rows.Count > 0) Then
                cbProvincia.SelectedIndex = 0
            End If
        End If
        If (cbZona.SelectedIndex < 0) Then

            If (CType(cbZona.DataSource, DataTable).Rows.Count > 0) Then
                cbZona.SelectedIndex = 0
            End If
        End If
        If (cbCiudad.SelectedIndex < 0) Then

            If (CType(cbCiudad.DataSource, DataTable).Rows.Count > 0) Then
                cbCiudad.SelectedIndex = 0
            End If
        End If

        If (cbSucursal.SelectedIndex < 0) Then

            If (CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
                cbSucursal.SelectedIndex = 0
            End If
        End If
        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_fnGeneralClientes(_Tipo)
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelo.Celda)
        Dim listEstCeldas As New List(Of Modelo.Celda)
        'a.ydnumi, a.ydcod, a.yddesc, a.ydzona, a.yddct, a.yddctnum, a.yddirec, a.ydtelf1, a.ydtelf2, a.ydcat,
        'a.ydest, a.ydlat, a.ydlongi, a.ydprconsu, a.ydobs, a.ydfnac, a.ydnomfac, a.ydtip, a.ydnit, a.ydfecing, a.ydultvent,
        'a.ydimg,
        'a.ydfact, a.ydhact, a.yduact

        listEstCeldas.Add(New Modelo.Celda("ydnumi", False, "Código", 80))
        listEstCeldas.Add(New Modelo.Celda("ydcod", True, "Codigo".ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("ydrazonsocial", False, "Razon Social".ToUpper, 180))
        listEstCeldas.Add(New Modelo.Celda("yddesc", True, "Razon Social".ToUpper, 350))
        listEstCeldas.Add(New Modelo.Celda("ydzona", False))
        listEstCeldas.Add(New Modelo.Celda("yddct", False))
        listEstCeldas.Add(New Modelo.Celda("yddctnum", False, "N. Documento".ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("yddirec", True, "Direccion".ToUpper, 180))
        listEstCeldas.Add(New Modelo.Celda("ydtelf1", False))
        listEstCeldas.Add(New Modelo.Celda("ydtelf2", False))
        listEstCeldas.Add(New Modelo.Celda("ydcat", False))
        listEstCeldas.Add(New Modelo.Celda("ydest", False))
        listEstCeldas.Add(New Modelo.Celda("ydlat", False))
        listEstCeldas.Add(New Modelo.Celda("ydlongi", False))
        listEstCeldas.Add(New Modelo.Celda("ydprconsu", False))
        listEstCeldas.Add(New Modelo.Celda("ydobs", False, "Observacion".ToUpper, 180))
        listEstCeldas.Add(New Modelo.Celda("ydfnac", False, "Fecha Nacimiento".ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("ydnomfac", False, "Factura".ToUpper, 200))
        listEstCeldas.Add(New Modelo.Celda("ciudad", True, "ciudad".ToUpper, 200))
        listEstCeldas.Add(New Modelo.Celda("provincia", True, "provincia".ToUpper, 200))
        listEstCeldas.Add(New Modelo.Celda("zonatitulo", True, "ZONA".ToUpper, 200))
        listEstCeldas.Add(New Modelo.Celda("ydtip", False))
        listEstCeldas.Add(New Modelo.Celda("ydnit", False, "Nit".ToUpper, 120))
        listEstCeldas.Add(New Modelo.Celda("ydfecing", False))
        listEstCeldas.Add(New Modelo.Celda("ydultvent", False))
        listEstCeldas.Add(New Modelo.Celda("ydimg", False))
        listEstCeldas.Add(New Modelo.Celda("ydfact", False))
        listEstCeldas.Add(New Modelo.Celda("ydhact", False))
        listEstCeldas.Add(New Modelo.Celda("yduact", False))
        listEstCeldas.Add(New Modelo.Celda("ydrut", False))
        listEstCeldas.Add(New Modelo.Celda("yddias", False))
        listEstCeldas.Add(New Modelo.Celda("ydlcred", False))
        listEstCeldas.Add(New Modelo.Celda("visita", False, "FRECUENCIA DE VISITA", 120))
        listEstCeldas.Add(New Modelo.Celda("zona", True, "SUCURSAL".ToUpper, 200))
        listEstCeldas.Add(New Modelo.Celda("documento".ToUpper, False, "Tipo Documento".ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("ydnumivend", False))
        listEstCeldas.Add(New Modelo.Celda("vendedor", False))
        listEstCeldas.Add(New Modelo.Celda("ydprovincia", False))
        listEstCeldas.Add(New Modelo.Celda("ydciudad", False))
        listEstCeldas.Add(New Modelo.Celda("ydalmacen", False))
        Return listEstCeldas
    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos
        'a.ydnumi, a.ydcod, a.yddesc, a.ydzona, a.yddct, a.yddctnum, a.yddirec, a.ydtelf1, a.ydtelf2, a.ydcat,
        'a.ydest, a.ydlat, a.ydlongi, a.ydprconsu, a.ydobs, a.ydfnac, a.ydnomfac, a.ydtip, a.ydnit, a.ydfecing, a.ydultvent,
        'a.ydimg,
        'a.ydfact, a.ydhact, a.yduact ,a.ydrut ,visita
        Dim dt As DataTable = CType(JGrM_Buscador.DataSource, DataTable)
        Try
            tbCodigoOriginal.Text = JGrM_Buscador.GetValue("ydnumi").ToString
        Catch ex As Exception
            Exit Sub
        End Try
        With JGrM_Buscador
            tbCodigoOriginal.Text = .GetValue("ydnumi").ToString
            tbCodCliente.Text = .GetValue("ydcod").ToString
            tbNombre.Text = .GetValue("yddesc").ToString
            cbZona.Value = .GetValue("ydzona")
            tbDireccion.Text = .GetValue("yddirec").ToString
            tbTelf1.Text = .GetValue("ydtelf1").ToString
            swEstado.Value = .GetValue("ydest")
            _latitud = .GetValue("ydlat")
            _longitud = .GetValue("ydlongi")
            cbProvincia.Value = .GetValue("ydprovincia")
            cbCiudad.Value = .GetValue("ydciudad")
            cbSucursal.Value = .GetValue("ydalmacen")
            tbFnac.Value = .GetValue("ydfnac")
            Tbdias.Text = .GetValue("yddias").ToString
            TbLCred.Text = .GetValue("ydlcred").ToString
            tbFIngr.Value = .GetValue("ydfecing")
            tbUltVenta.Value = .GetValue("ydultvent")
            lbFecha.Text = CType(.GetValue("ydfact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("ydhact").ToString
            lbUsuario.Text = .GetValue("yduact").ToString
            NumiVendedor = IIf(IsDBNull(.GetValue("ydnumivend")), 0, .GetValue("ydnumivend"))
            tbVendedor.Text = IIf(IsDBNull(.GetValue("vendedor")), "", .GetValue("vendedor"))
        End With

        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString

    End Sub



    Public Overrides Sub _PMOHabilitarFocus()

        'With MHighlighterFocus
        '    .SetHighlightOnFocus(tbCodigo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbCodProd, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbStockMinimo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbCodBarra, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        '    .SetHighlightOnFocus(tbDescPro, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbDescCort, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo1, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo2, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo3, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo4, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbUMed, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(swEstado, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbUniVenta, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbUnidMaxima, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbConversion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)


        'End With
    End Sub

#End Region



    Private Sub F1_Clientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()

    End Sub






    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If btnGrabar.Enabled = True Then
            _PMInhabilitar()
            _PMUltimoRegistro()

        Else
            '  Public _modulo As SideNavItem
            _tab.Close()
            _modulo.Select()

        End If
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

    End Sub

    Private Sub JGrM_Buscador_KeyDown(sender As Object, e As KeyEventArgs) Handles JGrM_Buscador.KeyDown
        If e.KeyData = Keys.Enter Then
            If (MPanelSup.Visible = True) Then
                JGrM_Buscador.GroupByBoxVisible = True
                MPanelSup.Visible = False
                JGrM_Buscador.UseGroupRowSelector = True

            Else
                JGrM_Buscador.GroupByBoxVisible = False
                JGrM_Buscador.UseGroupRowSelector = True
                MPanelSup.Visible = True
            End If
        End If
    End Sub
    Function _fnAccesible() As Boolean
        Return tbNombre.ReadOnly = False

    End Function
    Private Sub tbVendedor_KeyDown(sender As Object, e As KeyEventArgs) Handles tbVendedor.KeyDown
        If (_fnAccesible()) Then
            If e.KeyData = Keys.Control + Keys.Enter Then

                Dim dt As DataTable

                dt = L_fnListarEmpleado()
                '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
                ',a.ydtelf1 ,a.ydfnac 

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("ydnumi,", False, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("ydcod", True, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("yddesc", True, "NOMBRE", 280))
                listEstCeldas.Add(New Modelo.Celda("yddctnum", True, "N. Documento".ToUpper, 150))
                listEstCeldas.Add(New Modelo.Celda("yddirec", True, "DIRECCION", 220))
                listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "Telefono".ToUpper, 200))
                listEstCeldas.Add(New Modelo.Celda("ydfnac", True, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 1
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 350
                ef.Context = "Seleccione Vendedor".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    If (IsNothing(Row)) Then
                        tbVendedor.Focus()
                        Return

                    End If
                    NumiVendedor = Row.Cells("ydnumi").Value
                    tbVendedor.Text = Row.Cells("yddesc").Value
                    tbDireccion.Focus()

                End If

            End If

        End If
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If (_fnAccesible()) Then


            Dim dt As DataTable

            dt = L_fnListarEmpleado()
            '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
            ',a.ydtelf1 ,a.ydfnac 

            Dim listEstCeldas As New List(Of Modelo.Celda)
            listEstCeldas.Add(New Modelo.Celda("ydnumi,", False, "ID", 50))
            listEstCeldas.Add(New Modelo.Celda("ydcod", True, "ID", 50))
            listEstCeldas.Add(New Modelo.Celda("yddesc", True, "NOMBRE", 280))
            listEstCeldas.Add(New Modelo.Celda("yddctnum", True, "N. Documento".ToUpper, 150))
            listEstCeldas.Add(New Modelo.Celda("yddirec", True, "DIRECCION", 220))
            listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "Telefono".ToUpper, 200))
            listEstCeldas.Add(New Modelo.Celda("ydfnac", True, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
            Dim ef = New Efecto
            ef.tipo = 3
            ef.dt = dt
            ef.SeleclCol = 1
            ef.listEstCeldas = listEstCeldas
            ef.alto = 50
            ef.ancho = 350
            ef.Context = "Seleccione Vendedor".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                If (IsNothing(Row)) Then
                    tbVendedor.Focus()
                    Return

                End If
                NumiVendedor = Row.Cells("ydnumi").Value
                tbVendedor.Text = Row.Cells("yddesc").Value
                tbDireccion.Focus()

            End If



        End If
    End Sub

    Private Sub TextBoxX1_TextChanged(sender As Object, e As EventArgs) Handles Tbdias.TextChanged

    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click

    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        P_GenerarReporte()
    End Sub
    Private Sub P_GenerarReporte()
        'Dim dt As DataTable = L_fnReportecliente()

        'If Not IsNothing(P_Global.Visualizador) Then
        '    P_Global.Visualizador.Close()
        'End If

        'P_Global.Visualizador = New Visualizador

        'Dim objrep As New R_Clientes
        ''' GenerarNro(_dt)
        '''objrep.SetDataSource(Dt1Kardex)
        'objrep.SetDataSource(dt)

        'P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
        'P_Global.Visualizador.Show() 'Comentar
        'P_Global.Visualizador.BringToFront() 'Comentar



    End Sub



    Private Sub LabelX8_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub tbtitulo_TextChanged(sender As Object, e As EventArgs) Handles tbtitulo.TextChanged
        Dim dt As DataTable = CType(JGrM_Buscador.DataSource, DataTable)
        If (tbtitulo.Text.Trim <> String.Empty) Then


            Dim criterio As String = "FCIA. " + tbtitulo.Text.Trim
            JGrM_Buscador.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(JGrM_Buscador.RootTable.Columns("yddesc"), Janus.Windows.GridEX.ConditionOperator.BeginsWith, criterio))


        Else
            JGrM_Buscador.RootTable.RemoveFilter()


        End If
    End Sub

    Private Sub tbtitulo_KeyDown(sender As Object, e As KeyEventArgs) Handles tbtitulo.KeyDown
        If e.KeyData = Keys.Enter Then
            JGrM_Buscador.Focus()

        End If
    End Sub
End Class