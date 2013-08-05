@ModelType AgioNet.facturaDetailModel

@Code
    ViewData("Title") = "Facturación - Enviar Factura"
    Session("Section") = "facturacion"
    
    Dim Read() As AgioNet.facturaDetailModel = TempData("Model")
    Dim suma As AgioNet.facturaListModel = TempData("Suma")
    Dim NoFactura As String = TempData("NoFactura")
    
    Dim grid As WebGrid = New WebGrid(Read, canPage:=False)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Proceso Enviar Factura </h2>
@grid.GetHtml(columns:=grid.Columns( _
    grid.Column("OrderID", "Orden"), _
    grid.Column("NoFactura", "Número de Factura"), _
    grid.Column("Reference", "Referencia"), _
    grid.Column("SubTotal", "SubTotal", style:="SubTotal"), _
    grid.Column("IVA", "IVA", style:="IVA"), _
    grid.Column("Total", "Total", style:="Total") _
))
<br />
 @Using Html.BeginForm()
    @<span class="row-btOnly">
        @Html.HiddenFor(Function(m) m.NoFactura, New With {.Value = NoFactura})
        <input type="submit" value="Autorizar" class="Button" />
    </span>
End Using




<script src='@Url.Content("~/Scripts/jquery.agiotech.js")' type="text/javascript"></script>
<script type="text/javascript">
    $(function() {
        //Agregar la sumatoria
        var tfoot = '<tfoot><tr> <td colspan="3" style="text-align: right; padding-right:20px; font-size:1.2em;"><strong>Totales</strong></td>';
        tfoot += '<td class="SubTotal"> @suma.SubTotal </td>';
        tfoot += '<td class="IVA"> @suma.IVA </td>';
        tfoot += '<td class="Total"> @suma.Total </td></tr></tfoot>';
        $('table').append(tfoot);

        //Aplicar formato moneda
        $(".SubTotal").numeric({ prefix: '$ ', cents: true });
        $(".IVA").numeric({ prefix: '$ ', cents: true });
        $(".Total").numeric({ prefix: '$ ', cents: true });
    });
</script>