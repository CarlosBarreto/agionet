
@Code
    ViewData("Title") = "Ingenieria - Módulo de estaciones"
    Session("Section") = "ingenieria"
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<h2 class="TituloFormulario">Ingenieria - Módulos estaciones</h2>

<p>Este módulo es el encargado de presentar los formularios para la edicion de las estaciones</p>