@code
    Dim read As AgioNet.OrderPartInfoModel = TempData("OrderInfo")
    
End Code

<div class="form-row">
    <span class="Span-c"><strong>Orden: </strong> @read.OrderID</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Fecha: </strong> @read.OrderDate</span>
</div>

<div class="form-row">
    <span class="Span-c"><strong>Clase de producto: </strong> @read.ProductClass</span>
</div>

<div class="form-row">
    <span class="Span-c"><strong>Tipo de producto: </strong> @read.ProductType</span>
</div>

<div class="form-row">
    <span class="Span-i"><strong>Marca: </strong> @read.Trademark</span>
    <span class="Span-c"><strong>Modelo: </strong> @read.Model</span>
</div>

<div class="form-row">
    <span class="Span-i"><strong>Commodity:</strong> @read.ProductType</span>
    <span class="Span-i"><strong>SKU No:</strong> @read.PartNo</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Descripción: </strong> @read.Description</span>
</div>
<div class="form-row"> <span class="Span-e"> &nbsp; </span> </div>

<div class="form-row">
    <span class="Span-h"><strong>SerialNo: </strong> @read.SerialNumber</span>
    <span class="Span-c"><strong>Revision: </strong> @read.Revision</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Tipo de Servicio: </strong> @read.ServiceType</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Falla Reportada: </strong> @read.Failure</span>
</div>
<div class="form-row"> <span class="Span-e"> &nbsp; </span> </div>

<div class="form-row">
    <span class="Span-e"><strong>Comentario Cliente: </strong> @read.Comentario</span>
</div>
<div class="form-row"> <span class="Span-e"> &nbsp; </span> </div>

<!-- Datos de la parte -->
<div class="form-row"> <span class="Span-e"> &nbsp; </span> </div>
<div class="form-row">
    <span class="Span-c"><strong>Clase de producto: </strong> @read.sProductClass</span>
</div>

<div class="form-row">
    <span class="Span-c"><strong>Tipo de producto: </strong> @read.sProductType</span>
</div>

<div class="form-row">
    <span class="Span-i"><strong>Marca: </strong> @read.sTrademark</span>
    <!--<span class="Span-i"><strong>Modelo: </strong> @read.sModel</span> -->
</div>

<div class="form-row">
    <span class="Span-e"><strong>No Parte:</strong> @read.sPartNumber</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Alt:</strong> @read.sALT</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Descripción: </strong> @read.sDescription</span>
</div>
<div class="form-row"> <span class="Span-e"> &nbsp; </span> </div>

<div class="form-row">
    <span class="Span-e"><strong>SerialNo: </strong> @read.sSerialNumber</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Revision: </strong> @read.sRevision</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Falla Detectada: </strong> @read.sFailure</span>
</div>
<div class="form-row"> <span class="Span-e"> &nbsp; </span> </div>

<div class="form-row">
    <span class="Span-e"><strong>Solución: </strong> @read.sSolution</span>
</div>
<div class="form-row"> <span class="Span-e"> &nbsp; </span> </div>

<div class="form-row">
    <span class="Span-e"><strong>Registro de Falla: </strong> @read.sComment</span>
</div>
<div class="form-row"> <span class="Span-e"> &nbsp; </span> </div>
<div class="form-row"> <span class="Span-e"> &nbsp; </span> </div>

<div class="form-row">
    <span class="Span-e"><strong>Fuente de Suministro: </strong> @read.sSource</span>
</div>
<div class="form-row"> <span class="Span-e"> &nbsp; </span> </div>
<div class="form-row">&nbsp;</div>