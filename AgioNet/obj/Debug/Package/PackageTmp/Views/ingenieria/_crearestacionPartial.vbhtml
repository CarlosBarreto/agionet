@ModelType AgioNet.EstacionesModel
    
@Code
    ViewData("Title") = "_crearestacionPartial"
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<h2 class="TituloFormulario">Ingenieria - Crear estaciones</h2>
@Using Ajax.BeginForm("crear_estacion", "ingenieria", New AjaxOptions With {.UpdateTargetId = "tab-content"})
    @<div id="Formulario">       
        <div class="row">
            <span class="Span-b"><span class="pcenter">Nombre: </span></span>
            <span class="Span-d"> @Html.TextBoxFor(Function(m) m.Nombre)</span>
        </div>

        <div class="form-SpArea--dg">
            <span class="Span-b"><span class="pcenter">Descripción:</span></span>
            <span class="Span-d"> @Html.TextAreaFor(Function(m) m.Descripcion, New With {.cols = 32, .rows = 4})</span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="pcenter">Usuario: </span></span>
            <span class="Span-d"> 
                @Html.TextBox("txtNombre", User.Identity.Name, New With {.disabled = True})
                @Html.HiddenFor(Function(m) m.Usuario, New With {.Value = User.Identity.Name})
            </span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="pcenter">Proceso:</span></span>
            <span class="Span-d"> @Html.TextBoxFor(Function(m) m.Proceso)</span>
        </div>

        <div class="row">
            <span class="Span-b">&nbsp;</span>
            <span class="Span-c">&nbsp;</span>
            <span class="Span-a"> <input type="submit" value="Guardar" class="Button" /> </span>
        </div>
    </div>
end Using



