@ModelType Agionet.ReportedFailureModel

@Code
    ViewData("Title") = "Diagnostico - ver falla reportada"
    Session("Section") = "diagnostico"
End Code
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Ver falla Reportada</h2>

<div class="InfoCont">
    <span class="ShowLabel">OrderID</span>
    <span class="InfoLabel">@Html.Encode(TempData("OrderID")) </span>
</div>

<div class="InfoCont">
    <span class="ShowLabel">Reported Failure</span>
    <span class="InfoLabel">@Html.Encode(TempData("RFail"))</span>
</div>
