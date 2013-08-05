@ModelType AgioNet.AprobarFacturacionModel

@Code
    ViewData("Title") = "Facturación - Facturar Ordenes "
    Session("Section") = "facturacion"
    
    Dim Read() As AgioNet.OnBillingModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=False)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Proceso Facturación de Ordenes Terceros</h2>
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
<h2 class="TituloFormulario">Listado de Ordenes Pendientes Facturar Terceros</h2>

@grid.GetHtml(columns:=grid.Columns( _
    grid.Column("OrderID", "Orden"), _
    grid.Column("PartNumber", "Número de Parte"), _
    grid.Column("SerialNumber", "Número de Serie"), _
    grid.Column("FailureType", "Falla"), _
    grid.Column("Costo", "Costo"), _
    grid.Column("LeadTime", "Tiempo de entrega"), _
    grid.Column("Facturacion", "Facturación", format:=Function(item) Html.ActionLink("Facturar", "invoice_alt", "facturacion", New With {.OrderID = item.OrderID}, vbNull), style:="Link_") _
))
