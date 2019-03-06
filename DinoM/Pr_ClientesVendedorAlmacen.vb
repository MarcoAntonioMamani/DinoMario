Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports System.Data.OleDb
Imports DevComponents.DotNetBar.Controls
Public Class Pr_ClientesVendedorAlmacen
    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim bandera As Boolean = False
    Public _modulo As SideNavItem
    Public Sub _prIniciarTodo()
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)

        _PMIniciarTodo()
        Me.Text = "REPORTE CLIENTES"
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None

        bandera = True
    End Sub
    Public Sub _PMIniciarTodo()

        'TxtNombreUsu.Text = MGlobal.gs_usuario
        'TxtNombreUsu.ReadOnly = True

        Me.WindowState = FormWindowState.Maximized
        'Me.SupTabItemBusqueda.Visible = False

    End Sub
    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarReporte()
    End Sub
    Private Sub _prCargarReporte()
        Dim _dt As New DataTable
        _dt = L_fnReporteClienteAlmacenVendedor()
        If (_dt.Rows.Count > 0) Then

            If (swcliente.Value = True) Then

                Dim objrep As New R_ClientePorVendedor
                objrep.SetDataSource(_dt)

                objrep.SetParameterValue("usuario", L_Usuario)
                MReportViewer.ReportSource = objrep
                MReportViewer.Show()
                MReportViewer.BringToFront()
            Else
                Dim objrep As New R_ClientePorAlmacen
                objrep.SetDataSource(_dt)

                objrep.SetParameterValue("usuario", L_Usuario)
                MReportViewer.ReportSource = objrep
                MReportViewer.Show()
                MReportViewer.BringToFront()
            End If




        Else
            ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                                       My.Resources.INFORMATION, 2000,
                                       eToastGlowColor.Blue,
                                       eToastPosition.BottomLeft)
            MReportViewer.ReportSource = Nothing
        End If





    End Sub


    Private Sub Pr_ClientesVendedorAlmacen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _tab.Close()
        _modulo.Select()
    End Sub
End Class