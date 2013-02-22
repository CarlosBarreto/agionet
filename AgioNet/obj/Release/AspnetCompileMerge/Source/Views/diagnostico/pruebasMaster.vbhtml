@ModelType AgioNet.StationByUser

@Code
    ViewData("Title") = "Diagnostico - Realizar pruebas"
    Session("Section") = "diagnostico"
    
   
    
End Code
<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/diag.css")' rel="stylesheet" type="text/css" />
<script src='@Url.Content("~/Scripts/jquery.unobtrusive-ajax.js")' type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function () {
        var y =  parseInt( $("#Contenedor").css("height") )- 160;
        //alert(y);
        $("#tab-content").css("height", y);
    });
</script>

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<div id="MenuTab-Estaciones">
    <ul class="ul-tab">
        <li class="li-tab">@Ajax.ActionLink("Ejecutar Prueba", "ExecuteTest", "diagnostico", New AjaxOptions With {.UpdateTargetId = "tab-ContIzquierda"}) </li>
        <li class="li-tab">@Ajax.ActionLink("Cancelar Prueba", "CancelTest", "diagnostico", New AjaxOptions With {.UpdateTargetId = "tab-ContIzquierda"})</li>
        <li class="li-tab">@Ajax.ActionLink("Detalle de prueba", "ViewTest", "diagnostico", New AjaxOptions With {.UpdateTargetId = "tab-ContIzquierda"})</li>
        <!-- <li class="li-tab">@Ajax.ActionLink("Registrar Falla Adicional", "asignar_responsable", "ingenieria", New AjaxOptions With {.UpdateTargetId = "tab-ContIzquierda"})</li> -->
        <li class="li-tab">@Ajax.ActionLink("Ver Falla Reportada", "falla_reportada", "diagnostico", New AjaxOptions With {.UpdateTargetId = "tab-ContIzquierda"}) </li>
        <li class="li-tab">@Html.ActionLink("Realizar otra Prueba", "realizar_pruebas", "diagnostico")</li>
    </ul>
</div>

<div id="tab-content">
  <!-- Aquí se muestran los formularios -->
  <div id="tab-ContIzquierda">
      @Html.Partial("_ExecuteTest")
  </div>

  <!-- Aquí va la información de la orden -->
  <div id="tab-ContDerecha" class="bg-fondoborder">
    @Html.Partial("_TestOrderInfoPartial")
  </div>
</div>
