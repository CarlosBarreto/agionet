@ModelType AgioNet.UPDMaterialMasterModel

@Code
    ViewData("Title") = "Ingenieria - Quitar Material"
    Session("Section") = "ingenieria"
    
    Dim model As AgioNet.MaterialMasterModel = TempData("Model")
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<h2 class="TituloFormulario">Ingenieria - Quitar Material</h2>

@Using Html.BeginForm()
     @<div id="Formulario">       
        <div class="row">
            <span class="Span-b"><span class="pcenter">@Html.LabelFor(Function(m) m.SKUNo)</span></span>
            <span class="Span-c"> 
                @Html.TextBoxFor(Function(m) m.SKUNo, New With {.Value = model.SKUNo, .disabled = True})
            </span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="pcenter">@Html.LabelFor(Function(m) m.PartNo)</span></span>
            <span class="Span-c"> 
                @Html.TextBoxFor(Function(m) m.PartNo, New With {.Value = model.PartNo, .disabled = True})
            </span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="pcenter">@Html.LabelFor(Function(m) m.Commodity)</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.Commodity, New With {.Value = model.Commodity, .disabled = True})</span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="pcenter">@Html.LabelFor(Function(m) m.Description)</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.Description, New With {.Value = model.Description, .disabled = True})</span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="pcenter">@Html.LabelFor(Function(m) m.Alt)</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.Alt, New With {.Value = model.Alt, .disabled = True})</span>
        </div>
        
        <div class="row">
            <span class="Span-b">&nbsp;</span>
            <span class="Span-b">&nbsp;</span>
            <span class="Span-a"> <input type="submit" value="Quitar" class="Button" /> </span>
        </div>
    </div>
End Using