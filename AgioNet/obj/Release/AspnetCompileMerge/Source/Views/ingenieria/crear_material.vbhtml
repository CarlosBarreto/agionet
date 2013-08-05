@ModelType AgioNet.MaterialMasterModel

@Code
    ViewData("Title") = "Ingenieria - Crear Material"
    Session("Section") = "ingenieria"
    
    
    Dim OptionList As List(Of SelectListItem) = New List(Of SelectListItem)
    Dim Model() As AgioNet.CommodityListModel = TempData("Model")
    
    For Each Md As AgioNet.CommodityListModel In Model
        Dim _optFail As New SelectListItem
        _optFail.Text = Md.CommodityName
        _optFail.Value = Md.CommodityName
        OptionList.Add(_optFail)
    Next
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<h2 class="TituloFormulario">Proceso Crear Material</h2>

@Using Html.BeginForm()
     @<div id="Formulario">    
        <div class="row">
            <span class="Span-b"><span class="pcenter">@Html.LabelFor(Function(m) m.Commodity)</span></span>
            <span class="Span-c"> @Html.DropDownListFor(Function(m) m.Commodity, OptionList)</span>
        </div>
        
         <div class="row">
            <span class="Span-b"><span class="pcenter">@Html.LabelFor(Function(m) m.PartNo)</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.PartNo)</span>
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
            <span class="Span-c">&nbsp;</span>
            <span class="Span-b">&nbsp;</span>
            <span class="Span-a"> <input type="submit" value="Crear" class="Button" /> </span>
        </div>
    </div>
End Using