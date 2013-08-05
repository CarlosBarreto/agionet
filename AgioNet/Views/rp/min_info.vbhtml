@code
    Dim mi As AgioNet.mailing.MinOrderInfo = TempData("MinInfo")
End Code

<div class="rp_right-contenedor">
    <span class="title">
        Resumen de la orden
    </span>

    <div class="form-row">&nbsp; </div>
    <div class="form-row"><strong>Orden: </strong> @mi.OrderID </div>
    
    <div class="form-row"><strong>Modelo: </strong> @mi.Model</div>

    <div class="form-row"><strong>No. de Parte: </strong> @mi.PartNo</div>

    <div class="form-row"><strong>Descripción: </strong> @mi.Description</div>
    <div class="form-row">&nbsp; </div>

    <div class="form-row"><strong>No. Serie: </strong> @mi.SerialNo </div>

    <div class="form-row"><strong>Status Actual: </strong> @mi.Status</div>
    <div class="form-row">&nbsp; </div>
    <div class="form-row">&nbsp; </div>

    <strong>Falla Reportada: </strong> @mi.Failure <br /><br />
    
    <div class="form-row">&nbsp; </div>
    
</div>