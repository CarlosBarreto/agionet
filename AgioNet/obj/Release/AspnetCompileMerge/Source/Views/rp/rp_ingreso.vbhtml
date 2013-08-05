@code
    Dim mi As AgioNet.rp_ReciboModel = TempData("Model")
End Code

<div class="rp_left-contenedor">
    <span class="title"> Información del Recibo de la Orden</span>

    <div class="form-row">&nbsp; </div>
    <div class="form-row">
        <span class="Span-b"><strong>Orden: </strong></span>
        <span class="Span-c">@mi.OrderID </span> 
        <span class="Span-b"><strong>No. Serie</strong></span>
        <span class="Span-c">@mi.SerialNo</span>
    </div>
    
    <div class="form-row">
        <span class="Span-a"><strong>SKU No</strong></span>
        <span class="Span-h">@mi.SKU</span>
        <span class="Span-a"><strong>No. Parte</strong></span>
        <span class="Span-h">@mi.PartNo</span>
        <span class="Span-a"><strong>Modelo</strong></span>
        <span class="Span-h">@mi.Model</span>
    </div>

    <div class="form-row">
        <span class="Span-a"><strong>Guía</strong></span>
        <span class="Span-h">@mi.TrackNo</span>
        <span class="Span-a"><strong>Fecha</strong></span>
        <span class="Span-h">@mi.ScanDate</span>
        <span class="Span-a"><strong>Escaneada Por</strong></span>
        <span class="Span-h">@mi.ScanBy</span>
    </div>
    <div class="form-row">&nbsp; </div>
</div>

<div class="form-row">&nbsp; </div>

<div class="rp_left-contenedor">
    <span class="title"> Información del Ingreso de la Orden</span>

    <div class="form-row">&nbsp; </div>
    <div class="form-row">
        <span class="Span-h"><strong>Tipo de Empaque: </strong></span>
        <span class="Span-c">@mi.PackType </span> 
        <span class="Span-h"><strong>¿Empaque Dañado?</strong></span>
        <span class="Span-c">@mi.PackDamage</span>
    </div>

     <div class="form-row">
        <span class="Span-h"><strong>Daño No documentado: </strong></span>
        <span class="Span-c">@mi.NonDocPack </span> 
        <span class="Span-h"><strong>Empacado Correctamente</strong></span>
        <span class="Span-c">@mi.CorrectPack</span>
    </div>

     <div class="form-row">
        <span class="Span-b"><strong>Accesorios</strong></span>
        <span class="Span-j">@mi.Accesories </span> 
    </div>
    <div class="form-row">
        <span class="Span-b"><strong>Cosmetico</strong></span>
        <span class="Span-j">@mi.Cosmetic</span>
    </div>

     <div class="form-row">
        <span class="Span-b"><strong>Garantía</strong></span>
        <span class="Span-c">@mi.Warranty </span> 
        <span class="Span-b"><strong>Re-repair</strong></span>
        <span class="Span-c">@mi.rerepair</span>
    </div>

     <div class="form-row">
        <span class="Span-b"><strong>Comentarios: </strong></span>
        <span class="Span-j">@mi.Comment </span> 
    </div>
    <div class="form-row">&nbsp; </div>
</div>
