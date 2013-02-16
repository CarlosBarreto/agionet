@Code
    ViewData("Title") = "Pagina Principal de Logística"
    Session("Section") = "logistica"
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If