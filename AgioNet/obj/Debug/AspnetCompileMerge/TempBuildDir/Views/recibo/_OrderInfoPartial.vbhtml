@code
    Dim read As AgioNet.OrderInfoModel = TempData("Model")
    
End Code

<div class="form-row">
    <span class="Span-c"><strong>Orden: </strong> @read.OrderID</span>
</div>

<!--
<div class="form-row">
    <span class="Span-c"><strong>OrderDate:</strong>@read.OrderDate</span>
    <span class="Span-b"><strong>Flete:</strong>@read.Flete</span>
</div>

<div class="form-row">
    <span class="Span-c"><strong>Tipo de Cliente: </strong> @read.CustomerType</span>
</div>

<div class="form-row">
    <span class="Span-c"><strong>Nombre del Cliente: </strong> @read.CustomerName</span>
</div>

<div class="form-row">
    <span class="Span-c"><strong>Razón Social: </strong> @read.RazonSocial</span>
</div>

<div class="form-row">
    <span class="Span-c"><strong>Referencia: </strong> @read.Reference</span>
</div>

<div class="form-row">
    <span class="Span-b"><strong>RFC: </strong> @read.RFC</span>
    <span class="Span-c"><strong>Email: </strong> @read.Email</span>
</div>

<div class="form-row">
   <span class="Span-c"><strong>: </strong> @read.</span> 
</div>

<div class="form-row">
<span class="Span-d"><strong>Dirección: </strong> @read.Address No. @read.ENumber Int. @read.INumber </span>
</div>

<div class="form-row">
    <span class="Span-d"><strong>Colonia: </strong> @read.Address2</span>
</div>

<div class="form-row">
     <span class="Span-c"><strong>Ciudad: </strong> @read.City</span>
     <span class="Span-b"><strong>Estado: </strong> @read.State</span>
</div>
<div class="form-row">
     <span class="Span-c"><strong>País: </strong> @read.Country</span>
     <span class="Span-b"><strong>C.P.: </strong> @read.ZipCode</span>
</div>

<div class="form-row">
    <span class="Span-b"><strong>Tel Casa: </strong> @read.Tel</span>
    <span class="Span-b"><strong>Tel Ofi: </strong> @read.Tel2</span>
    <span class="Span-b"><strong>Cel: </strong> @read.Tel3</span>
</div>

<div class="form-row">
    <span class="Span-b"><strong>Tipo Rec: </strong> @read.Delivery</span>
    <span class="Span-c"><strong>Horario Rec: </strong> @read.DeliveryTime</span>
</div>

    
 -->

<div class="form-row">
    <span class="Span-e"><strong>Clase de Producto: </strong> @read.ProductClass</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Tipo de Producto: </strong> @read.ProductType</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Marca: </strong> @read.Trademark</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Modelo: </strong> @read.Model</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Descripción: </strong> @read.Description</span>
</div>
       
<div class="form-row">
    <span class="Span-e"><strong>SKUNo: </strong> @read.PartNo</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Num Serie: </strong> @read.SerialNo</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Revisión: </strong> @read.Revision</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Tipo de Sericio: </strong> @read.ServiceType</span>
</div>
  
<div class="form-row--ex">
    <span class="Span-e"><strong>Falla: </strong> @read.FailureType</span>
</div>   
     
<div class="form-row--ex">
    <span class="Span-e"><strong>Comentarios: </strong> @read.Comment</span>
</div>
<div class="form-row">&nbsp;</div>