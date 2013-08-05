@code
    Dim read As AgioNet.OrderInfoModel = TempData("Model")
    
End Code

<div class="form-row">
    <span class="Span-c"><strong>Orden: </strong> @read.OrderID</span>
</div>

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
<div class="form-row">&nbsp;</div>
<div class="form-row">&nbsp;</div> 
      
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
    <span class="Span-e"><strong>Tipo de Servicio: </strong> @read.ServiceType</span>
</div>
  
<div class="form-row--ex">
    <span class="Span-e"><strong>Falla: </strong> @read.FailureType</span>
</div>   
     
<div class="form-row--ex">
    <span class="Span-e"><strong>Comentarios: </strong> @read.Comment</span>
</div>
<div class="form-row">&nbsp;</div>