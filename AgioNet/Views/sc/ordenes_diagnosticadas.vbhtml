@ModelType AgioNet.OrderListModel

@Code
    ViewData("Title") = "Servicio a clientes - Pendientes de Autorización"
    Session("Section") = "sc"
    
    Dim Read() As AgioNet.OrdenesDiagnosticadas = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Ordenes diagnosticadas listas para cotizar</h2>

@grid.GetHtml(columns:=grid.Columns( _
    grid.Column("OrderID", "Orden"), _
    grid.Column("Incoming", "Fecha Ingreso"), _
    grid.Column("FoundDate", "Fecha Diagnostico"), _
    grid.Column("ProductModel", "Modelo"), _
    grid.Column("ProductDescription", "Descripción"), _
    grid.Column("PartNumber", "Número de Parte"), _
    grid.Column("SerialNumber", "Número de Serie"), _
    grid.Column(format:=Function(item) Html.ActionLink("Cotizar", "costear_diagnostico", "sc", New With {.OrderID = item.OrderID}, vbNull)) _
))
