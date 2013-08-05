@code
    Dim mi As AgioNet.rp_CheckinModel = TempData("Model")
End Code

<span class="title"> Información de Ingreso al Almacén </span>

<div class="form-row">&nbsp; </div>
<div class="form-row">
    <span class="Span-b"><strong>Orden: </strong></span>
    <span class="Span-c">@mi.OrderID </span> 
    <span class="Span-b"><strong>Guía</strong></span>
    <span class="Span-c">@mi.Guia</span>
</div>
    
<div class="form-row">
    <span class="Span-a"><strong>Longitud</strong></span>
    <span class="Span-h">@mi.lenght</span>
    <span class="Span-a"><strong>Ancho</strong></span>
    <span class="Span-h">@mi.Width</span>
    <span class="Span-a"><strong>Altura</strong></span>
    <span class="Span-h">@mi.Height</span>
</div>

<div class="form-row">
    <span class="Span-a"><strong>Peso Neto</strong></span>
    <span class="Span-h">@mi.Weight</span>
    <span class="Span-a"><strong>Peso Volumetrico</strong></span>
    <span class="Span-h">@mi.Dimweight</span>
    <span class="Span-a"><strong>Peso Embarque</strong></span>
    <span class="Span-h">@mi.Shipweight</span>
</div>

<div class="form-row">&nbsp; </div>