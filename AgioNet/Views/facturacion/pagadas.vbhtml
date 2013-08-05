@ModelType Agionet.ScanOrderModel
    
@Code
    ViewData("Title") = "Facturacion - Listado de Facturas Pagadas"
    Session("Section") = "facturacion"
    
     Dim Read() As AgioNet.facturaListModel = TempData("Model")
    Dim grid As WebGrid = New WebGrid(Read, canPage:=False)

End Code
<script type="text/javascript">
    $(document).ready(function () {
        $("#Comment").html("@User.Identity.Name : Registrar el pago de una orden... ");
    });
</script>

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Listado de Facturas Pagadas</h2>

@grid.GetHtml(columns:=grid.Columns( _
    grid.Column("NoFactura", "Número de Factura"), _
    grid.Column("SubTotal", "SubTotal", style:="SubTotal"), _
    grid.Column("IVA", "IVA", style:="IVA"), _
    grid.Column("Total", "Total", style:="Total") _
))
<!-- <id.Column("Enviar", "Enviar", format:=Function(item) Html.ActionLink("Enviar", "sendinvoice", "facturacion", New With {.NoFactura = item.NoFactura}, vbNull), style:="Link_") _ -->
<script src='@Url.Content("~/Scripts/jquery.agiotech.js")' type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        //Aplicar formato moneda
        $(".SubTotal").numeric({ prefix: '$ ', cents: true });
        $(".IVA").numeric({ prefix: '$ ', cents: true });
        $(".Total").numeric({ prefix: '$ ', cents: true });
    });
</script>