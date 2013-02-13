@Code
    ViewData("Title") = "Pagina Principal Reparación"
    Session("Section") = "REPAIR"
End Code

@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">REPAIR - Pagina Principal de Reparación </h2>

@Html.Action("LoadOrderInfo")
@Html.Action("LoadWnOrderInfo")