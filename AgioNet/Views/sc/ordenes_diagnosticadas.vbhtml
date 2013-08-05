@ModelType AgioNet.ScanOrderModel

@Code
    ViewData("Title") = "Servicio a clientes - Pendientes de Autorización"
    Session("Section") = "sc"
    
    Dim Read() As AgioNet.OrdenesDiagnosticadas = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=false)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Partes diagnosticadas listas para cotizar</h2>
<div id="Formulario">       
    <div class="row">
        @Using Html.BeginForm("ordenes_diagnosticadas", "sc", FormMethod.Get) '"material_master", "ingenieria", FormMethod.Get, new AjaxOptions { .InsertionMode=InsertionMode.Replace, .UpdateTargetId="resultados" } )
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
    grid.Column("Incoming", "Fecha Ingreso"), _
    grid.Column("FoundDate", "Fecha Diagnostico"), _
    grid.Column("ProductDescription", "Descripción"), _
    grid.Column("PartNumber", "Número de Parte"), _
    grid.Column("TipoReparacion", "Tipo de Reparación"), _
    grid.Column(format:=Function(item) Html.ActionLink("Cotizar", "costear_diagnostico", "sc", New With {.OrderID = item.OrderID}, vbNull)) _
))
<!--grid.Column("ProductModel", "Modelo"), _-->