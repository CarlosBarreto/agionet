@ModelType AgioNet.EstacionesModel

@Code

    ViewData("Title") = "Ingeniería - Listado de estaciones"
    Session("Section") = "ingenieria"

    Dim Read() As AgioNet.EstacionesModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read)
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Listado de Ordenes</h2>

@grid.GetHtml(columns:=grid.Columns( _
                   grid.Column("Nombre", "Nombre"), _
                   grid.Column("Descripcion", "Descripción"), _
                   grid.Column("Usuario", "Responsable"), _
                   grid.Column("Proceso", "Proceso"), _
                   grid.Column(format:=Function(item) Ajax.ActionLink("Editar", "editarestacion", "ingenieria", _
                                                                      New With {.Nombre = item.Nombre}, _
                                                                      New AjaxOptions With {.UpdateTargetId = "tab-content"})) _
))
