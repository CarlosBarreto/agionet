@ModelType Agionet.InspeccionCalidadModel
    
@Code
   
    Dim OrderID As String = TempData("OrderID")
    
    Dim OptionList As List(Of SelectListItem) = New List(Of SelectListItem)
    Dim _opt As SelectListItem
    
    _opt = New SelectListItem With {.Text = "PASS", .Value = "PASS"}
    OptionList.Add(_opt)
    
    _opt = New SelectListItem With {.Text = "FAIL", .Value = "FAIL"}
    OptionList.Add(_opt)
    
    
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario--dg"> Realizar una prueba</h2>
@Using Html.BeginForm()
    @<div id="ContenedorOrderIDForm">
        <span class="row">
            <span class="Span-b">Orden: </span>
            <span class="Span-j">@Html.TextBoxFor(Function(m) m.OrderID, New With {.Value = OrderID, .class = "text"})</span>        
        </span>
    
        <span class="row">&nbsp;</span>
    
        <div class="row">
            <span class="Span-b">Resultado:</span>
            <span class="Span-b">@Html.DropDownListFor(Function(m) m.Result, OptionList, New With {.class="text"})
            </span>
         </div>
    
        <span class="row">
            <span class="Span-b">@Html.LabelFor(Function(m) m.Comment) </span>
            <span class="Span-j">@Html.TextAreaFor(Function(m) m.Comment, New With {.rows = 6, .cols = 53})</span>
         </span>
        <span class="row">&nbsp;</span>
        <span class="row">&nbsp;</span>
        <span class="row">
            <span class="Span-b">&nbsp;</span>
            <span class="Span-j">&nbsp;</span>
            <span class="Span-a"> <input type="submit" value="SCAN" class="Button" /></span>
         </span>
    </div>
End Using

