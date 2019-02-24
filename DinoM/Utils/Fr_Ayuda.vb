Imports Janus.Windows.GridEX
Public Class Fr_Ayuda

#Region "ATRIBUTOS"
    Public dtBuscador As DataTable
    Public nombreVista As String
    Public posX As Integer
    Public posY As Integer
    Public seleccionado As Boolean
    Public Columna As Integer = -1
    Public filaSelect As Janus.Windows.GridEX.GridEXRow
    Public validar As Boolean
    Public listEstrucGrilla As List(Of Modelo.Celda)
    Dim n As Integer = 0
#End Region

#Region "METODOS PRIVADOS"
    Public Sub New(ByVal x As Integer, y As Integer, dt1 As DataTable, titulo As String, listEst As List(Of Modelo.Celda))
        dtBuscador = dt1
        posX = x
        posY = y
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.StartPosition = FormStartPosition.Manual
        Me.Location = New Point(posX, posY)
        GPPanelP.Text = titulo

        listEstrucGrilla = listEst

        seleccionado = False
        Columna = 1
        _PMCargarBuscador()
        'grJBuscador.Row = grJBuscador.FilterRow.RowIndex
        'grJBuscador.Col = 1
        'grJBuscador.Select()
        'grJBuscador.MoveTo(0)
        'grJBuscador.Col = Columna

        'grJBuscador.Focus()
        'grJBuscador.MoveTo(grJBuscador.FilterRow)
        'grJBuscador.Col = 2

        tbtitulo.Focus()
    End Sub
    Public Sub _prSeleccionar()
        If (Columna >= 0) Then

            grJBuscador.MoveTo(0)
            grJBuscador.Col = Columna
        End If
    End Sub


    Private Sub _PMCargarBuscador()

        Dim anchoVentana As Integer = 0

        grJBuscador.DataSource = dtBuscador
        grJBuscador.RetrieveStructure()


        For i = 0 To dtBuscador.Columns.Count - 1
            With grJBuscador.RootTable.Columns(i)
                If listEstrucGrilla.Item(i).visible = True Then
                    .Caption = listEstrucGrilla.Item(i).titulo
                    .Width = listEstrucGrilla.Item(i).tamano
                    .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    .CellStyle.FontSize = 9

                    Dim col As DataColumn = dtBuscador.Columns(i)
                    Dim tipo As Type = col.DataType
                    If tipo.ToString = "System.Int32" Or tipo.ToString = "System.Decimal" Then
                        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    End If
                    If listEstrucGrilla.Item(i).formato = String.Empty Then
                        .FormatString = listEstrucGrilla.Item(i).formato
                    End If

                    anchoVentana = anchoVentana + listEstrucGrilla.Item(i).tamano
                Else
                    .Visible = False
                End If
            End With
        Next

        'Habilitar Filtradores
        With grJBuscador
            '.DefaultFilterRowComparison = FilterConditionOperator.BeginsWith
            '.FilterMode = FilterMode.Automatic
            '.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges

            'diseño de la grilla
            .GroupByBoxVisible = False
            .VisualStyle = VisualStyle.Office2007
        End With


        'adaptar el tamaño de la ventana
        Me.Width = anchoVentana + 50
        grJBuscador.MoveTo(0)
        grJBuscador.Col = Columna
    End Sub
#End Region

    Private Sub ModeloAyuda_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
        If (e.KeyChar = ChrW(Keys.Escape)) Then
            e.Handled = True
            Me.Close()
        End If
    End Sub

    Private Sub grJBuscador_KeyDown(sender As Object, e As KeyEventArgs) Handles grJBuscador.KeyDown

        If e.KeyData = Keys.Escape Then
            Me.Close()
        End If

        If e.KeyData = Keys.Enter Then
            filaSelect = grJBuscador.GetRow()
            seleccionado = True
            Me.Close()
        End If
    End Sub

    Private Sub grJBuscador_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grJBuscador.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
        If (e.KeyChar = ChrW(Keys.Escape)) Then
            e.Handled = True
            Me.Close()
        End If
    End Sub

    Private Sub tbtitulo_TextChanged(sender As Object, e As EventArgs) Handles tbtitulo.TextChanged
        Dim dt As DataTable = CType(grJBuscador.DataSource, DataTable)
        If (tbtitulo.Text.Trim <> String.Empty) Then


            Dim criterio As String = tbtitulo.Text.Trim
            grJBuscador.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grJBuscador.RootTable.Columns("yfcdprod1"), Janus.Windows.GridEX.ConditionOperator.BeginsWith, criterio))


        Else
            grJBuscador.RootTable.RemoveFilter()


        End If
    End Sub

    Private Sub tbtitulo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbtitulo.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
        If (e.KeyChar = ChrW(Keys.Escape)) Then
            e.Handled = True
            Me.Close()
        End If
    End Sub

    Private Sub tbtitulo_KeyDown(sender As Object, e As KeyEventArgs) Handles tbtitulo.KeyDown
        If e.KeyData = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyData = Keys.Enter Then
            grJBuscador.Focus()

        End If
    End Sub

    Private Sub grJBuscador_SelectionChanged(sender As Object, e As EventArgs) Handles grJBuscador.SelectionChanged
        If (n = 0) Then
            tbtitulo.Focus()
            n = n + 1
        End If
    End Sub

    Private Sub grJBuscador_LayoutLoad(sender As Object, e As EventArgs) Handles grJBuscador.LayoutLoad
        tbtitulo.Focus()
    End Sub




    'Private Sub grJBuscador_FilterApplied(sender As Object, e As EventArgs) Handles grJBuscador.FilterApplied

    '    Dim dt As DataTable = CType(grJBuscador.DataSource, DataTable)
    '    If (_fnEsClientes() And grJBuscador.Col = grJBuscador.RootTable.Columns("yddesc").Index) Then
    '        Dim ee As String = grJBuscador.GetValue("yddesc").ToString

    '        Dim criterio As String = "FCIA. " + ee.Trim
    '        grJBuscador.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grJBuscador.RootTable.Columns("yddesc"), Janus.Windows.GridEX.ConditionOperator.BeginsWith, criterio))
    '    Else

    '    End If

    'End Sub

End Class