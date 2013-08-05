@code
    Dim mi() As AgioNet.rp_CostPartModel = TempData("Model")
    Dim mi2() As AgioNet.rp_CotizacionPartesModel = TempData("CotizacionPartes")
    Dim m3 As AgioNet.rp_CotizacionClienteModel = TempData("CotizacionCliente")
End Code

<div class="rp_left-contenedor">
    <span class="title"> Información Sobre las Partes Costeadas Para la Orden</span>
    <div class="form-row">&nbsp; </div>
     @For Each m As AgioNet.rp_CostPartModel In mi
        @<div class="form-row">
            <span class="Span-b"><strong>Orden: </strong></span>
            <span class="Span-c">@m.OrderID </span> 
        </div>
    
        @<div class="form-row">
            <span class="Span-a"><strong>Costo</strong></span>
            <span class="Span-h moneda">@m.Costo</span>
            <span class="Span-a"><strong>Flete</strong></span>
            <span class="Span-h moneda">@m.Flete</span>
            <span class="Span-a"><strong>Gastos Importación</strong></span>
            <span class="Span-h moneda">@m.GastosImportacion</span>
        </div>

        @<div class="form-row">
            <span class="Span-b"><strong>Sub Total</strong></span>
            <span class="Span-c moneda">@m.SubTotal</span>
            <span class="Span-b"><strong>Fecha Entrega</strong></span>
            <span class="Span-c">@m.LeadTime</span>
        </div>

        @<div class="form-row">
            <span class="Span-b"><strong>Proveedor</strong></span>
            <span class="Span-j">@m.Proveedor</span>
        </div>

        @<div class="form-row">
            <span class="Span-b"><strong>Comentarios</strong></span>
            <span class="Span-j">@m.Comentario</span>
        </div>
        @<div class="form-row">&nbsp; </div>

        @<div class="form-row">
            <span class="Span-b"><strong>Costeado Por</strong></span>
            <span class="Span-c">@m.CostBy</span>
            <span class="Span-b"><strong>Costeado El</strong></span>
            <span class="Span-c">@m.CostDate</span>
        </div>
        @<div class="form-row">&nbsp; </div>
    Next
</div>

<div class="form-row">&nbsp; </div>
<span class="title"> Información Sobre las Partes Cotizadas Para la Orden</span>
<div class="form-row">&nbsp; </div>

@For Each m2 As AgioNet.rp_CotizacionPartesModel In mi2
@<div class="rp_left-contenedor">
    
     
        <div class="form-row">
            <span class="Span-b"><strong>Orden: </strong></span>
            <span class="Span-c">@m2.OrderID </span> 
        </div>
            
        <div class="form-row">
            <span class="Span-b"><strong>Numero de Parte</strong></span>
            <span class="Span-b"> @m2.PartNo </span>
            <span class="Span-a"><strong>Descripción</strong></span>
            <span class="Span-d"> @m2.Description</span>
        </div>
        <div class="form-row">&nbsp; </div>
         
        <div class="form-row">
            <span class="Span-a"><strong>Costo</strong></span>
            <span class="Span-g moneda">@m2.Costo</span>
            <span class="Span-a"><strong>Utilidad</strong></span>
            <span class="Span-g">@m2.Utilidad %</span>
            <span class="Span-g"><strong>Utilidad Neta</strong></span>
            <span class="Span-g moneda">@m2.UtilidadNeta</span>
            <span class="Span-a"><strong>Precio</strong></span>
            <span class="Span-g moneda">@m2.Precio</span>
        </div>
        <div class="form-row">&nbsp; </div>
</div>
@<div class="form-row">&nbsp; </div>
Next

<div class="rp_left-contenedor">
    <span class="title"> Información Sobre la Cotización al cliente</span>
    <div class="form-row">&nbsp; </div>
        <div class="form-row">
            <span class="Span-b"><strong>Orden: </strong></span>
            <span class="Span-c">@m3.OrderID </span> 
        </div>
    
        <div class="form-row">
            <span class="Span-b"><strong>Partes:&nbsp;</strong><span class="moneda">@m3.Sumatoria</span></span> 
            <span class="Span-i"><strong>Mano Obra:&nbsp;</strong><span class="moneda">@m3.ManoObra</span></span>
            <span class="Span-b"><strong>Viaje:&nbsp;</strong><span class="moneda">@m3.Viaje</span></span>
            <span class="Span-i"><strong>Sub Total:&nbsp;</strong><span class="moneda">@m3.SubTotal</span></span>
            <span class="Span-b"><strong>IVA:&nbsp;</strong><span class="moneda">@m3.IVA</span></span>
            <span class="Span-b"><strong>Total:&nbsp;</strong><span class="moneda">@m3.Total</span></span>
        </div>

        <div class="form-row">
            <span class="Span-c"><strong>Fecha Compromiso</strong></span>
            <span class="Span-a">@m3.LeadTime</span>
            <span class="Span-b"><strong>Costeado Por</strong></span>
            <span class="Span-b">@m3.CostBy</span>
            <span class="Span-b"><strong>Costeado el:</strong></span>
            <span class="Span-c">@m3.CostDate</span>
        </div>
</div>