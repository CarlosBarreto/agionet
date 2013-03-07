@ModelType AgioNet.StationByUser

@Code
    ViewData("Title") = "Diagnostico - Realizar pruebas"
    Session("Section") = "reparacion"
End Code

<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/diag.css")' rel="stylesheet" type="text/css" />
<script src='@Url.Content("~/Scripts/jquery.unobtrusive-ajax.js")' type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function () {
        var y = parseInt($("#Contenedor").css("height")) - 160;
        //alert(y);
        $("#tab-content").css("height", y);

        // Modifcar 
    });
</script>

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<div id="MenuTab-Estaciones">
    <ul class="ul-tab">
        <li class="li-tab">@Ajax.ActionLink("Remplazo", "remplazo_parte", "reparacion", New AjaxOptions With {.UpdateTargetId = "tab-ContIzquierda"}) </li>
        <li class="li-tab">@Ajax.ActionLink("Nueva Parte", "nueva_parte", "reparacion", New AjaxOptions With {.UpdateTargetId = "tab-ContIzquierda"})</li>
        <li class="li-tab">@Ajax.ActionLink("Reparación", "reparacion", "reparacion", New AjaxOptions With {.UpdateTargetId = "tab-ContIzquierda"})</li>
        <!-- <li class="li-tab">@Ajax.ActionLink("Solicitar Parte", "AddFailure", "diagnostico", New AjaxOptions With {.UpdateTargetId = "tab-ContIzquierda"})</li> -->
        <li class="li-tab">@Html.ActionLink("Reparar otra orden", "index", "reparacion")</li>
    </ul>
</div>

<div id="tab-content">
  <!-- Aquí se muestran los formularios -->
  <div id="tab-ContIzquierda">
      @Html.Partial("_ReplacePartForm")
  </div>

  <!-- Aquí va la información de la orden -->
  <div id="tab-ContDerecha" class="bg-fondoborder">
    <div id="FailureInfo">
        @Html.Partial("_FailureInfoPartial")
    </div>
    
  </div>
</div>
