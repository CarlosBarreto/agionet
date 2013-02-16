@Code
    ViewData("Title") = "Pagina Principal Servicio a Clientes"
    Session("Section") = "sc"
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If