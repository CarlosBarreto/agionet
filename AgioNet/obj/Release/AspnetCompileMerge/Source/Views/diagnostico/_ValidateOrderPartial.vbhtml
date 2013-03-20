@Code
    ViewData("Title") = "_ValidateOrderPartial"
End Code

<h2>_ValidateOrderPartial</h2>
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If