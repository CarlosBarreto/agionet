@code
    Layout = Nothing
    
    Response.AddHeader("Content-disposition", "attachment; filename=PendientesRecoleccion_" & Format(Now(), "yyyyMMdd_hhmm") & ".xls")
    Response.ContentType = "application/octet-stream"
    Dim Read() As AgioNet.OrderListModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read)
End Code

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