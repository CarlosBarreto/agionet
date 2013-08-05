@code
    Dim read As AgioNet.TestOrderInfoModel = Session("OrderInfo")
    
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<div class="form-row">
    <span class="Span-c"><strong>Orden: </strong> @read.OrderID</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>OrderDate: </strong> @read.OrderDate</span>
</div>

<div class="form-row">
    <span class="Span-c"><strong>TestID: </strong> @read.TestID</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Modelo: </strong> @read.ProductModel</span>
</div>

<!--div class="form-row">
    <span class="Span-e"><strong>Clase de Producto: </strong> @read.ProductClass</span>
</div-->

<div class="form-row">
    <span class="Span-e"><strong>Tipo de Producto: </strong> @read.ProductType</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Marca: </strong> @read.ProductTrademark</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Nún Parte: </strong> @read.PartNumber</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Descripción: </strong> @read.ProductDescription</span>
</div>
<div class="form-row">&nbsp;</div>
       


<div class="form-row">
    <span class="Span-e"><strong>Num Serie: </strong> @read.SerialNumber</span>
</div>
 
<div class="form-row--ex">
    <span class="Span-e"><strong>Falla: </strong> @read.FailureType</span>
</div>   
     
<div class="form-row--ex">
    <span class="Span-e"><strong>Comentarios: </strong> @read.Comment</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>DateLog: </strong> @read.DateLog</span>
</div>

<div class="form-row--ex">
    <span class="Span-e"><strong>TextLog: </strong> @read.TextLog</span>
</div>

<div class="form-row">&nbsp;</div>
<div class="form-row">&nbsp;</div>