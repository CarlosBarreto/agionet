@Code
    ViewData("Title") = "ingenieria - Administración de estaciones"
    Session("Section") = "ingenieria"

End Code
<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/ing.css")' rel="stylesheet" type="text/css" />
<script src='@Url.Content("~/Scripts/jquery.unobtrusive-ajax.js")' type="text/javascript"></script>

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<div id="MenuTab-Estaciones">
    <ul class="ul-tab">
        <li class="li-tab">@Ajax.ActionLink("Crear Estación", "crear_estacion", "ingenieria", New AjaxOptions With {.UpdateTargetId = "tab-content"}) </li>
        <li class="li-tab">@Ajax.ActionLink("Editar Estación", "editar_estacion", "ingenieria", New AjaxOptions With {.UpdateTargetId = "tab-content"})</li>
        <li class="li-tab">@Ajax.ActionLink("Borrar Estación", "borrar_estacion", "ingenieria", New AjaxOptions With {.UpdateTargetId = "tab-content"})</li>
        <li class="li-tab">@Ajax.ActionLink("Asignar Responsable", "asignar_responsable", "ingenieria", New AjaxOptions With {.UpdateTargetId = "tab-content"})</li>
        <li class="li-tab">@Ajax.ActionLink("Asignar Usuarios", "asignar_usuario", "ingenieria", New AjaxOptions With {.UpdateTargetId = "tab-content"}) </li>
    </ul>
</div>

<div id="tab-content">
    <!-- Aqui va el contenido -->
    @Html.Partial("_estacionesMainPartial")
    codigo perronamente maquetado U.u
</div>

<!-- Ejemplos 
            grid.Column(format:=Function(item) Ajax.ActionLink("Request", "AddRequestPart", "REPAIR", _
                                                           New With {.PartNo = item.PartNo, .OrderID = Session("OrderID"), .User = User.Identity.Name}, _
                                                           New AjaxOptions With {.UpdateTargetId = "PartiesRequested"})) _
    -->