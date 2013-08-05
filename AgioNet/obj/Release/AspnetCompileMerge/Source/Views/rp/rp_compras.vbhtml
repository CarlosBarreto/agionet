@code
    Dim mi() As AgioNet.rp_CostPartModel = TempData("Model")
End Code

<span class="title"> Información Sobre las Partes Compradas Para la Orden</span>
<div class="form-row">&nbsp; </div>

@For Each m As AgioNet.rp_CostPartModel In mi
    @<div class="rp_left-contenedor">

        <div class="form-row">
            <span class="Span-i"><strong>Orden: </strong>@m.OrderID </span> 
        </div>
        <div class="form-row">
            <span class="Span-i"><strong>PartNo:</strong>@m.PartNo</span>        
            <span class="Span-e"><strong>Descripción:</strong> @m.Description </span>
        </div>
    
        <div class="form-row">
            <span class="Span-a"><strong>Costo</strong></span>
            <span class="Span-h moneda">@m.Costo</span>
            <span class="Span-a"><strong>Flete</strong></span>
            <span class="Span-h moneda">@m.Flete</span>
            <span class="Span-a"><strong>Gastos Importación</strong></span>
            <span class="Span-h moneda">@m.GastosImportacion</span>
        </div>

        <div class="form-row">
            <span class="Span-b"><strong>Sub Total</strong></span>
            <span class="Span-c moneda">@m.SubTotal</span>
            <span class="Span-b"><strong>Fecha Entrega</strong></span>
            <span class="Span-c">@m.LeadTime</span>
        </div>

        <div class="form-row">
            <span class="Span-b"><strong>Proveedor</strong></span>
            <span class="Span-j">@m.Proveedor</span>
        </div>

        <div class="form-row">
            <span class="Span-b"><strong>Comentarios</strong></span>
            <span class="Span-j">@m.Comentario</span>
        </div>
        <div class="form-row">&nbsp; </div>

        <div class="form-row">
            <span class="Span-b"><strong>Comprado Por</strong></span>
            <span class="Span-c">@m.CostBy</span>
            <span class="Span-b"><strong>Comprado El</strong></span>
            <span class="Span-c">@m.CostDate</span>
        </div>
        <div class="form-row">&nbsp; </div>    
</div>
Next