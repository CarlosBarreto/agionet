@ModelType AgioNet.ScanOrderModel

@Code
    ViewData("Title") = "Servicio a clientes - Pendientes de Autorización"
    Session("Section") = "sc"
    
    Dim Read() As AgioNet.AprobarRepararListModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=False)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Ordenes Pendientes de Autorización Para Reparar</h2>
<div id="Formulario">       
    <div class="row">
        @Using Html.BeginForm("aprobar_reparar", "sc", FormMethod.Get) '"material_master", "ingenieria", FormMethod.Get, new AjaxOptions { .InsertionMode=InsertionMode.Replace, .UpdateTargetId="resultados" } )
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
                   grid.Column("FechaIngreso", "Fecha Ingreso"), _
                   grid.Column("EnvioCotizacion", "Envio Cotización"), _
                   grid.Column("LeadTime", "Lead Time"), _
                   grid.Column("PartNo", "Número de Parte"), _
                   grid.Column("Description", "Descripción"), _
                   grid.Column("ProductType", "Tipo Producto"), _
                   grid.Column(format:=Function(item) Html.ActionLink("Autorizar", "aprobar_reparar_list", "sc", New With {.OrderID = item.OrderID}, vbNull)) _
))
