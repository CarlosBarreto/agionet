@ModelType Agionet.ReciboPartesCompradasModel
    
@Code
    ViewData("Title") = "Almacen - Proceso de Recibo de Partes Compradas"
    Session("Section") = "almacen"
    
    Dim Read() As AgioNet.ReciboPartesCompradasModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=False)
End Code

<script type="text/javascript">
    $(document).ready(function () {
        $("#Comment").html("@User.Identity.Name : Procesar orden... ");
    });
</script>

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Proceso de Recibo de Partes Compradas</h2>
@Using Html.BeginForm("partes_compradas", "almacen", "", FormMethod.post) '"material_master", "ingenieria", FormMethod.Get, new AjaxOptions { .InsertionMode=InsertionMode.Replace, .UpdateTargetId="resultados" } )
    @<div id="Formulario">       
        <div class="row">
            <span class="Span-a"><span class="pcenter"><label>Orden</label></span></span>
            <span class="Span-i">@Html.TextBoxFor(Function(m) m.OrderID)</span>

            <span class="Span-a"><span class="pcenter"><label>Guia</label></span></span>
            <span class="Span-i">@Html.TextBoxFor(Function(m) m.TrackNo)</span>

            <span class="Span-b"><span class="pcenter"><label>Numero de Parte</label></span></span>
            <span class="Span-i">@Html.TextBoxFor(Function(m) m.PartNo)</span>

            <span class="Span-a"><span class="pcenter"><label>Proveedor</label></span></span>
            <span class="Span-i">@Html.TextBoxFor(Function(m) m.Proveedor)</span>

             <span class="Span-a"> <input type="submit" value="Buscar" class="Button" name="action"/> </span>
            <span class="Span-a"> <input type="submit" value="Recibir" class="Button" name="action"/> </span>
        </div>
    </div>
End Using


<div id="xx" style="display:block; height: 80%; width:100%; overflow-y:auto; overflow-x:hidden;">
    <h2 class="TituloFormulario">Listado de Pendientes de Recibir</h2>
    @grid.GetHtml(columns:=grid.Columns( _
            grid.Column("OrderID", "Orden"), _
            grid.Column("PartNo", "Número de Parte"), _
            grid.Column("Description", "Descripción"), _
            grid.Column("Proveedor", "Proveedor"), _
            grid.Column("TrackNo", "Guia"), _
            grid.Column("LeadTime", "ETA de la Parte"), _
            grid.Column("CostDate", "Fecha de Compra"), _
            grid.Column("Recibir", "Recibir", format:=Function(item) Html.ActionLink("Recibir", "partescompradas", "almacen", New With {.OrderID = item.OrderID}, vbNull)) _
    ))

</div>
 