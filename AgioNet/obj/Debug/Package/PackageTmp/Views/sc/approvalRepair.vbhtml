@ModelType AgioNet.OrderListModel

@Code
    ViewData("Title") = "Servicio a clientes - Autorización de Reparación"
    Session("Section") = "sc"
    
    Dim Read() As AgioNet.AppFailureInfoModel = TempData("FailureInfo")
    Dim Order As Object = TempData("OrderInfo")
    
    Dim grid As WebGrid = New WebGrid(Read)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Información de la Orden</h2>
@Using Html.BeginForm()
    @<div id="Formulario">
        <div class="row">
            <span class="Span-l"><strong>Orden: </strong>@Order.OrderID </span>
            <span class="Span-l"><strong>Fecha origen: </strong>@Order.OrderDate </span>
            <span class="Span-l"><strong>Tipo de Cliente: </strong>@Order.CustomerType</span>
            <span class="Span-l"><strong>RFC: </strong>@Order.RFC</span>
        </div>

        <div class="row">
            <span class="Span-l"><strong>Email: </strong>@Order.Email </span>
            <span class="Span-l"><strong>Dirección: </strong>@Order.Address </span>
            <span class="Span-l"><strong>Número Ext: </strong>@Order.ExternalNumber</span>
            <span class="Span-l"><strong>Número Int: </strong>@Order.InternalNumber</span>
        </div>

        <div class="row">
            <span class="Span-l"><strong>Colonia: </strong>@Order.Address2</span>
            <span class="Span-l"><strong>Ciudad: </strong>@Order.City</span>
            <span class="Span-l"><strong>Estado: </strong>@Order.State</span>
            <span class="Span-l"><strong>País: </strong>@Order.Country</span>
        </div>
       
        <div class="row">
            <span class="Span-l"><strong>C.P.: </strong>@Order.ZipCode</span>
            <span class="Span-l"><strong>Teléfono: </strong>@Order.Telephone</span>
            <span class="Span-l"><strong>Teléfono2: </strong>@Order.Telephone2</span>
            <span class="Span-l"><strong>Teléfono3: </strong>@Order.Telephone3</span>
        </div>

        <div class="row">      
            <span class="Span-l"><strong>Inluye Flete: </strong>@Order.Delivery</span>
            <span class="Span-l"><strong>Horario Recolección: </strong>@Order.DeliveryTime</span>
            <span class="Span-l"><strong>Clase de Producto: </strong>@Order.ProductClass</span>
            <span class="Span-l"><strong>Tipo de Producto: </strong>@Order.ProductType</span>
        </div>

        <div class="row">
            <span class="Span-l"><strong>Marca: </strong>@Order.ProductTrademark</span>
            <span class="Span-l"><strong>Modelo: </strong>@Order.ProductModel</span>
            <span class="Span-l"><strong>Descripción: </strong>@Order.ProductDescription</span>
            <span class="Span-l"><strong>Número de parte: </strong>@Order.PartNumber</span>
        </div>

        <div class="row">
            <span class="Span-l"><strong>Número de serie: </strong>@Order.SerialNumber</span>
            <span class="Span-l"><strong>Revisión: </strong>@Order.Revision</span>
            <span class="Span-l"><strong>Tipo de servicio: </strong>@Order.ServiceType</span>
            <span class="Span-l"><strong>Falla reportada: </strong>@Order.FailureType </span>
        </div>

        <div class="row">
            
            <span class="Span-l"><strong>Comentarios: </strong>@Order.Comment </span>
            <span class="Span-l"><span class="pcenter"></span></span>
        </div>
    </div>
End Using

<h2 class="TituloFormulario">Listado de fallas para autorizar reparación</h2>

@grid.GetHtml(columns:=grid.Columns( _
                   grid.Column("OrderID", "Orden"), _
                   grid.Column("TestID", "ID de prueba"), _
                   grid.Column("TestDescription", "Descripción"), _
                   grid.Column("FailureID", "ID de Falla"), _
                   grid.Column("FailureDescription", "Descripción de falla"), _
                   grid.Column("PossibleSolution", "Posible Solución"), _
                   grid.Column("Log", "Reporte de Prueba"), _
                   grid.Column("TestResult", "Resultado de Prueba"), _
                   grid.Column("FoundBy", "Reportado Por"), _
                   grid.Column("FoundDate", "Fecha de reporte") _
))