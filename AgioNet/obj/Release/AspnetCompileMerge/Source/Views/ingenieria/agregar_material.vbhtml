@ModelType AgioNet.MaterialMasterModel

@Code
    ViewData("Title") = "Ingenieria - Agregar Material"
    Session("Section") = "ingenieria"   
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<h2 class="TituloFormulario">Ingenieria - Agregar Material</h2>

@Using Html.BeginForm()
     @<div id="Formulario">       
        <div class="row">
            <span class="Span-b"><span class="pcenter">@Html.LabelFor(Function(m) m.SKUNo)</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.SKUNo)</span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="pcenter">@Html.LabelFor(Function(m) m.PartNo)</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.PartNo)</span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="pcenter">@Html.LabelFor(Function(m) m.Commodity)</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.Commodity)</span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="pcenter">@Html.LabelFor(Function(m) m.Description)</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.Description)</span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="pcenter">@Html.LabelFor(Function(m) m.Alt)</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.Alt)</span>
        </div>
        
        <div class="row">
            <span class="FRaight"> <input type="submit" value="Guardar" class="Button" /> </span>
        </div>
    </div>
End Using