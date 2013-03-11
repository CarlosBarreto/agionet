@ModelType AgioNet.SearchOrderModel

@Code
    ViewData("Title") = "Pagina Principal de Empaque"
    Session("Section") = "empaque"
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
