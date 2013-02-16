@ModelType AgioNet.OrderListModel

@Code
    ViewData("Title") = "Servicio a Clientes - Listado de ordenes"
    Session("Section") = "sc"
    
    Dim Read() As AgioNet.OrderListModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, rowsPerPage:=22)
End Code

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Listado de Ordenes</h2>

@grid.GetHtml(columns:=grid.Columns( _
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
                   grid.Column(format:=Function(item) Html.ActionLink("Detalle", "detalle_", "sc", New With {.OrderID = item.OrderID}, vbNull), style:="Link_") _
))