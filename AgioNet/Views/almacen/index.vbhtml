@Code
    ViewData("Title") = "Pagina Principal Almacén"
    Session("Section") = "almacen"
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If