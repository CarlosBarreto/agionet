@ModelType Agionet.ReportedFailureModel

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Falla reportada para la orden [@Session("OrderID")]</h2>

<div class="InfoCont">
    <span class="ShowLabel">OrderID</span>
    <span class="InfoLabel">@Html.Encode(TempData("OrderID")) </span>
</div>

<div class="InfoCont">
    <span class="ShowLabel">Reported Failure</span>
    <span class="InfoLabel">@Html.Encode(TempData("RFail"))</span>
</div>

