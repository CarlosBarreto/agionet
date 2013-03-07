@code
    Dim read As AgioNet.PendientesreparacionModel = TempData("Model")
    
End Code

<div class="form-row">
    <span class="Span-c"><strong>Orden: </strong> @read.OrderID</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Comentario Aprobación: </strong> @read.Comments</span>
</div>
<span class="form-row">&nbsp;</span>
<span class="form-row">&nbsp;</span>

<div class="form-row">
    <span class="Span-e"><strong>Aprobado el: </strong> @read.ApprovalDate</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Número de Parte: </strong> @read.PartNumber</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Descripción: </strong> @read.ProductDescription</span>
</div>
<span class="form-row">&nbsp;</span>
<span class="form-row">&nbsp;</span>

<div class="form-row">
    <span class="Span-e"><strong>Número de serie: </strong> @read.SerialNumber</span>
</div>
       
<div class="form-row">
    <span class="Span-e"><strong>Falla: </strong> @read.Failure</span>
</div>
<span class="form-row">&nbsp;</span>
<span class="form-row">&nbsp;</span>

<div class="form-row">
    <span class="Span-e"><strong>Solución: </strong> @read.Solution</span>
</div>
<span class="form-row">&nbsp;</span>
<span class="form-row">&nbsp;</span>

<div class="form-row">
    <span class="Span-e"><strong>Fuente Suministro: </strong> @read.Source</span>
</div>
<span class="form-row">&nbsp;</span>
<span class="form-row">&nbsp;</span>
    
<div class="form-row">
    <span class="Span-e"><strong>Comentario: </strong>@read.Comment</span>
</div>
<div class="form-row">&nbsp;</div>
<div class="form-row">&nbsp;</div>