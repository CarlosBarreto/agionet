@ModelType AgioNet.EstacionesModel
    
@Code
    ViewData("Title") = "Ingeniería - Listado de estaciones"
    Session("Section") = "ingenieria"

    Dim Read As AgioNet.EstacionesModel = TempData("Model")

End Code
<script type="text/javascript">
    $(function () {
        $("#Descripcion").val("@Html.Raw(Read.Descripcion)");
    });
</script>
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<h2 class="TituloFormulario">Ingenieria - Edición de estaciones</h2>
@Using Ajax.BeginForm("editarestacion", "ingenieria", New AjaxOptions With {.UpdateTargetId = "tab-content"})
    @<div id="Formulario">       
        <div class="row">
            <span class="Span-b"><span class="pcenter">Nombre: </span></span>
            <span class="Span-d"> @Html.TextBoxFor(Function(m) m.Nombre, New With {.Value = Read.Nombre })</span>
        </div>

        <div class="form-SpArea--dg">
            <span class="Span-b"><span class="pcenter">Descripción:</span></span>
            <span class="Span-d"> @Html.TextAreaFor(Function(m) m.Descripcion, New With {.cols = 32, .rows = 4})</span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="pcenter">Usuario: </span></span>
            <span class="Span-d"> 
                @Html.TextBoxFor(Function(m) m.Usuario, New With {.Value = Read.Usuario })
            </span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="pcenter">Proceso:</span></span>
            <span class="Span-d"> @Html.TextBoxFor(Function(m) m.Proceso, New With {.Value = Read.Proceso })</span>
        </div>

        <div class="row">
            <span class="Span-b">&nbsp;</span>
            <span class="Span-c">&nbsp;</span>
            <span class="Span-a"> <input type="submit" value="Guardar" class="Button" /> </span>
        </div>
    </div>
end Using



