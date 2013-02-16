@Code
    ViewData("Title") = "Pagina Principal Ingeniería"
    Session("Section") = "ingenieria"
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If