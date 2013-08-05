@ModelType AgioNet.scanOrdermodel

@Code
    ViewData("Title") = "Servicio a clientes - Cotización de cliente"
    Session("Section") = "sc"
    
    Dim Read() As AgioNet.OrdenesCosteadasModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=false)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Ordenes listas para cotizar a cliente</h2>
<div id="Formulario">       
    <div class="row">
        @Using Html.BeginForm("cotizar_cliente", "sc", FormMethod.Get) '"material_master", "ingenieria", FormMethod.Get, new AjaxOptions { .InsertionMode=InsertionMode.Replace, .UpdateTargetId="resultados" } )
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
    grid.Column("StartDiagnostic", "Inicio Diagnóstico"), _
    grid.Column("CloseDiagnostic", "Cierre Diagnóstico"), _
    grid.Column("CostDate", "Fecha Costeo de Parte"), _
    grid.Column("ProductModel", "Modelo"), _
    grid.Column("ProductDescription", "Descripción"), _
    grid.Column("PartNumber", "Número de Parte"), _
    grid.Column(format:=Function(item) Html.ActionLink("Cotizar", "cotizarcliente", "sc", New With {.OrderID = item.OrderID}, vbNull)) _
))

<!-- grid.Column("SerialNumber", "Número de Serie"), _ -->