@ModelType AgioNet.OrderListModel

@Code
    ViewData("Title") = "Servicio a clientes - Pendientes de Autorización"
    Session("Section") = "sc"
    
    Dim Read() As AgioNet.OrderListModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Ordenes pendientes de autorización para reparar</h2>

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
                   grid.Column("SerialNo", "Numero de Serie"), _
                   grid.Column(format:=Function(item) Html.ActionLink("Autorización", "approvalRepair", "sc", New With {.OrderID = item.OrderID}, vbNull)) _
))
