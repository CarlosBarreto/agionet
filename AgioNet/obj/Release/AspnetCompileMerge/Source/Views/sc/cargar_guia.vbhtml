@ModelType AgioNet.OrderListModel

@Code
    ViewData("Title") = "Servicio a clientes - Comprar Parte"
    Session("Section") = "sc"
    
    Dim Read() As AgioNet.OrdenesDiagnosticadas = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=False)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Partes Compradas en Proceso de Cargar Guía</h2>
<div id="Formulario">       
    <div class="row">
        @Using Html.BeginForm("cargar_guia", "sc", FormMethod.Get) '"material_master", "ingenieria", FormMethod.Get, new AjaxOptions { .InsertionMode=InsertionMode.Replace, .UpdateTargetId="resultados" } )
            @<span class="Span-i"><span class="pcenter">Buscar Por Orden: </span></span>
            @<span class="Span-i">@Html.TextBoxFor(Function(m) m.OrderID)</span>
            @<span class="Span-a">
                <span class="FRaight"> <input type="submit" value="Buscar" class="Button" /> </span>
             </span>
        End Using
    </div>        
    <div class="row"> &nbsp; </div>
</div>

@grid.GetHtml(columns:=grid.Columns( _
    grid.Column("OrderID", "Orden"), _
    grid.Column("PartNumber", "Número de Parte"), _
    grid.Column("ProductDescription", "Descripción"), _
    grid.Column("ETA", "Fecha ETA"), _
    grid.Column("Comprada", "TrackNo"), _
    grid.Column(format:=Function(item) If(item.Comprada = "", Html.ActionLink("Cargar", "cargarguia", "sc", New With {.OrderID = item.OrderID}, vbNull), ""), style:="Link_") _
))

<!-- Modificacion 2013-05-07 
grid.Column("Incoming", "Fecha Ingreso"), _
grid.Column("FoundDate", "Fecha Diagnostico"), _
grid.Column("ProductModel", "Modelo"), _
grid.Column("SerialNumber", "Número de Serie"), _
-->