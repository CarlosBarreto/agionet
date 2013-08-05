@ModelType AgioNet.facturaDetailModel

@Code
    ViewData("Title") = "Facturación - Enviar Factura"
    Session("Section") = "facturacion"
    
    Dim Read() As AgioNet.facturaListModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=False)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Proceso Enviar Factura </h2>
<div id="calidad-contenedor-form" class="calidad-contenedor-form">
    @Using Html.BeginForm()
        @<span class="row">
            <span class="Span-a"><label>Orden</label> </span>
            <span class="Span-d">@Html.TextBoxFor(Function(m) m.OrderID, New With {.class = "text"})</span>     
            <span class="Span-f">&nbsp;</span>
            <span class="Span-a"> <input type="submit" value="Autorizar" class="Button" /></span>   
        </span>
        @<div class="row"> &nbsp; </div>
    End Using
</div>

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Listado de Facturas Pendientes de Enviar</h2>

@grid.GetHtml(columns:=grid.Columns( _
    grid.Column("NoFactura", "Número de Factura"), _
    grid.Column("SubTotal", "SubTotal", style:="SubTotal"), _
    grid.Column("IVA", "IVA", style:="IVA"), _
    grid.Column("Total", "Total", style:="Total"), _
    grid.Column("Enviar", "Enviar", format:=Function(item) Html.ActionLink("Enviar", "sendinvoice", "facturacion", New With {.NoFactura = item.NoFactura}, vbNull), style:="Link_") _
))

<script src='@Url.Content("~/Scripts/jquery.agiotech.js")' type="text/javascript"></script>
<script type="text/javascript">
    $(function() {
        //Aplicar formato moneda
        $(".SubTotal").numeric({ prefix: '$ ', cents: true });
        $(".IVA").numeric({ prefix: '$ ', cents: true });
        $(".Total").numeric({ prefix: '$ ', cents: true });
    });
</script>