@ModelType Agionet.ReportedFailureModel

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Falla reportada para la orden [@Session("OrderID")]</h2>

<div class="row">
    <span class="Span-c"><strong>OrderID: </strong>@Html.Encode(TempData("OrderID"))</span>
</div>

<div class="row">
    <span class="Span-e"><strong>Falla Reportada: </strong> @Html.Encode(TempData("RFail"))</span>
</div>

