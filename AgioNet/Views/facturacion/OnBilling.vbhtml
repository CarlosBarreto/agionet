@ModelType AgioNet.OnBillingModel

@Code
    ViewData("Title") = "Facturación - Órdenes pendientes de facturar"
    Session("Section") = "facturacion"
    
    Dim Read() As AgioNet.OnBillingModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Ordenes en pendientes de facturar</h2>

@grid.GetHtml(columns:=grid.Columns( _
    grid.Column("OrderID", "Orden"), _
    grid.Column("PartNumber", "Número de Parte"), _
    grid.Column("SerialNumber", "Número de Serie"), _
    grid.Column("FailureType", "Falla"), _
    grid.Column("Costo", "Costo"), _
    grid.Column("LeadTime", "Tiempo de entrega") _
))
