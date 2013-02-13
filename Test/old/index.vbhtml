@Code
    ViewData("Title") = "Pagina Principal OM"
    Session("Section") = "OM"
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If