@ModelType AgioNet.SearchOrderModel

@Code
    ViewData("Title") = "Servicio a Clientes - Listado de ordenes"
    Session("Section") = "sc"
    
    Dim Read() As AgioNet.OrderListModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, rowsPerPage:=22)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Listado de Ordenes</h2>
@Using Html.BeginForm() '"material_master", "ingenieria", FormMethod.Get, new AjaxOptions { .InsertionMode=InsertionMode.Replace, .UpdateTargetId="resultados" } )
    @<div id="Formulario">       
        <div class="row">
            <span class="Span-a"><span class="pcenter">Orden: </span></span>
            <span class="Span-i">@Html.TextBoxFor(Function(m) m.OrderID)</span>

             <span class="Span-a">
                 <span class="FRaight">
                    <input type="submit" value="Buscar" class="Button" />
                </span>
             </span>
        </div>
        <div class="row"> &nbsp; </div>
    </div>
End Using

@grid.GetHtml(columns:=grid.Columns( _
    grid.Column("OrderID", "Orden"), _
    grid.Column("OrderDate", "Fecha"), _
    grid.Column("CustomerName", "Cliente"), _
    grid.Column("Email", "Email"), _
    grid.Column("Delivery", "Flete"), _
    grid.Column("DeliveryTime", "Horario Recolección"), _
    grid.Column("ProductClass", "Clase Producto"), _
    grid.Column("ProductType", "Tipo Producto"), _
    grid.Column("ProductModel", "Modelo Producto"), _
    grid.Column("PartNo", "Numero de Parte"), _
    grid.Column("SerialNo", "Numero de Serie") _
))

    