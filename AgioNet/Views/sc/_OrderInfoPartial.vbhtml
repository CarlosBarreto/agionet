@code
    Dim read As AgioNet.AllOrderInfoModel = TempData("OrderInfo")
    
End Code

<div class="form-row">
    <span class="Span-c"><strong>Orden: </strong> @read.OrderID</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Fecha: </strong> @read.OrderDate</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Tipo de cliente: </strong> @read.CustomerType</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Nombre del cliente: </strong> @read.CustomerName</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>RFC: </strong> @read.RFC</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Email: </strong> @read.Email</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Dirección: </strong> @(read.Address & " No. " & read.ExternalNumber & " Int. " & read.InternalNumber)  </span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Colonia: </strong> @read.Address2</span>
</div>
  
<div class="form-row">
    <span class="Span-c"><strong>Ciudad: </strong> @read.City</span>
</div>

<div class="form-row">
    <span class="Span-c"><strong>Estado: </strong> @read.State</span>
</div>   
     
<div class="form-row">
    <span class="Span-b"><strong>País: </strong> @read.Country</span>
    <span class="Span-b"><strong>C.P.: </strong> @read.ZipCode</span>
</div>

<div class="form-row">
    <span class="Span-b"><strong>Tel: </strong> @read.Telephone</span>
    <span class="Span-b"><strong>Tel2: </strong> @read.Telephone2</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Celular: </strong> @read.Telephone3</span>
</div>

<div class="form-row">
    <span class="Span-b"><strong>Flete: </strong> @read.Delivery</span>
    <span class="Span-c"><strong>Horario de recolección: </strong> @read.DeliveryTime</span>
</div>

<div class="form-row"> &nbsp; </div>

<div class="form-row">
    <span class="Span-c"><strong>Clase de producto: </strong> @read.ProductClass</span>
</div>

<div class="form-row">
    <span class="Span-c"><strong>Tipo de producto: </strong> @read.ProductType</span>
</div>

<div class="form-row">
    <span class="Span-i"><strong>Marca: </strong> @read.ProductTrademark</span>
    <span class="Span-i"><strong>Modelo: </strong> @read.ProductModel</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Revision: </strong> @read.Revision</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Descripción: </strong> @read.ProductDescription</span>
</div>

<div class="form-row">
    <span class="Span-e"> &nbsp; </span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Tipo de Servicio: </strong> @read.ServiceType</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Falla: </strong> @read.FailureType</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Comentarios: </strong> @read.Comment</span>
</div>

<div class="form-row">&nbsp;</div>
<div class="form-row">&nbsp;</div>