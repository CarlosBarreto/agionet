@ModelType Agionet.ReportedFailureModel
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Falla reportada para la orden [@Session("OrderID")]</h2>

<div class="row">
    <span class="Span-c"><strong>OrderID: </strong>@Html.Encode(TempData("OrderID"))</span>
</div>

<div class="row">
    <span class="Span-e"><strong>Falla Reportada: </strong> @TempData("RFail")</span>
</div>

