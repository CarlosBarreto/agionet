@Code
    ViewData("Title") = "Pagina Principal Recibo"
    Session("Section") = "recibo"
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If