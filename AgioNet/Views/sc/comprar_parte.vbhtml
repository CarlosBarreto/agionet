@ModelType AgioNet.OrderListModel

@Code
    ViewData("Title") = "Servicio a clientes - Comprar Parte"
    Session("Section") = "sc"
    
    Dim Read() As AgioNet.ComprarPartesListModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, rowsPerPage:=25)
End Code

<style type="text/css">
   /* .Orden { width: 10%; }
    .FechaIngreso { width: 10% }
    .PartNo { width:12%; }
    .Description { width: 20% }
    .Comprado { width: 8%; }*/
    .Suministro { /* Div que corta las palabras largas */
        width: 250px; /* ancho que necesitas */
        overflow: auto;
        white-space: -moz-pre-wrap; /* Mozilla */
        white-space: -hp-pre-wrap; /* HP printers */
        white-space: -o-pre-wrap; /* Opera 7 */
        white-space: -pre-wrap; /* Opera 4-6 */
        white-space: pre-wrap; /* CSS 2.1 */
        white-space: pre-line; /* CSS 3 (and 2.1 as well, actually) */
        word-wrap: break-word; /* IE */
        -moz-binding: url('xbl.xml#wordwrap'); /* Firefox (using XBL) */ 
    }
  /*  .ETA { width: 8%; }
    .LeadTime { width: 8%; }
    .Autorizado { width: 10%; }
    .TipoReparacion { width: 8%; }
    .Reparar { width: 8%; }
      */
</style>

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Partes Cotizadas Listas Para Comprar</h2>
<div id="Formulario">       
    <div class="row">
        @Using Html.BeginForm("comprar_parte", "sc", FormMethod.Get) '"material_master", "ingenieria", FormMethod.Get, new AjaxOptions { .InsertionMode=InsertionMode.Replace, .UpdateTargetId="resultados" } )
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
    grid.Column("OrderID", "Orden", style:="Orden"), _
    grid.Column("Incoming", "Fecha Ingreso", style:="FechaIngreso"), _
    grid.Column("PartNo", "Numero de Parte", style:="PartNo"), _
    grid.Column("ProductDescription", "Descripción", style:="Description"), _
    grid.Column("Comprado", "Comprado", style:="Comprado"), _
    grid.Column(format:=Function(item) Html.Raw(String.Format( item.FuenteSuministro.ToString)), style:="Suministro"), _
    grid.Column("ETAParte", "ETA de la Parte", style:="ETA"), _
    grid.Column("FechaCompromiso", "Fecha Compromiso", style:="LeadTime"), _
    grid.Column("ApprovalDate", "Fecha Autorización", style:="Autorizado"), _
    grid.Column("TipoReparacion", "Tipo de reparación", style:="TipoReparacion"), _
    grid.Column(format:=Function(item) If(item.Comprado = "", Html.ActionLink("Comprar", "comprarparte", "sc", New With {.OrderID = item.OrderID}, vbNull), ""), style:="Link_") _
))
