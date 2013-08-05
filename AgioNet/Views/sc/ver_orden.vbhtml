@ModelType AgioNet.SearchOrderModel

@Code
    ViewData("Title") = "Servicio a Clientes - Listado de ordenes"
    Session("Section") = "sc"
    
    Dim Read() As AgioNet.OrderListModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, rowsPerPage:=100)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Listado de Ordenes</h2>
<div id="Formulario">       
    <div class="row">
        @Using Html.BeginForm("detalle_orden", "sc", FormMethod.Get) '"material_master", "ingenieria", FormMethod.Get, new AjaxOptions { .InsertionMode=InsertionMode.Replace, .UpdateTargetId="resultados" } )
            @<span class="Span-i"><span class="pcenter">Buscar Por Orden: </span></span>
            @<span class="Span-i">@Html.TextBoxFor(Function(m) m.OrderID)</span>
            @<span class="Span-a">
                <span class="FRaight"> <input type="submit" value="Buscar" class="Button" /> </span>
             </span>
        End Using

        <!-- Busqueda por nombre del cliente -->
        @Using Html.BeginForm("ver_orden", "sc", FormMethod.Get) '"material_master", "ingenieria", FormMethod.Get, new AjaxOptions { .InsertionMode=InsertionMode.Replace, .UpdateTargetId="resultados" } )
            @<span class="Span-i"><span class="pcenter">Buscar por Nombre: </span></span>
            @<span class="Span-i">@Html.TextBoxFor(Function(m) m.Nombre)</span>
            @<span class="Span-i"><span class="pcenter">Buscar por No Serie: </span></span>
            @<span class="Span-i">@Html.TextBoxFor(Function(m) m.SerialNo)</span>
            @<span class="Span-a">
                <span class="FRaight"> <input type="submit" value="Buscar" class="Button" /> </span>
             </span>
        End Using

    </div>        
    <div class="row"> &nbsp; </div>
</div>


@grid.GetHtml(columns:=grid.Columns( _
    grid.Column("Item", format:=Function(item) item.WebGrid.Rows.IndexOf(item) + 1), _
    grid.Column("OrderID", "Orden"), _
    grid.Column("OrderDate", "Fecha"), _
    grid.Column("CustomerName", "Cliente"), _
    grid.Column("Email", "Email"), _
    grid.Column("ProductClass", "Clase Producto"), _
    grid.Column("ProductType", "Tipo Producto"), _
    grid.Column("ProductModel", "Modelo Producto"), _
    grid.Column("PartNo", "Numero de Parte"), _
    grid.Column("SerialNo", "Numero de Serie"), _
    grid.Column("Status", "Estatus"), _
    grid.Column(format:=Function(item) Html.ActionLink("Detalle", "detalle_orden", "sc", New With {.OrderID = item.OrderID}, vbNull), style:="Link_") _
))
