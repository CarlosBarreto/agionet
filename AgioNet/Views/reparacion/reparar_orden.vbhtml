@ModelType AgioNet.StationByUser

@Code
    ViewData("Title") = "Diagnostico - Realizar pruebas"
    Session("Section") = "diagnostico"
       
    ' Actualizacion... No se va a mostrar listado de pruebas
    Dim myModel() As AgioNet.StationByUser = TempData("Model")
    Dim OptionList As List(Of SelectListItem) = New List(Of SelectListItem)
    
    Dim Count As Integer = 0
    Dim _Len As Integer
    _Len = myModel.Length - 1
    While Count <= _Len
        Dim _opt As New SelectListItem()
        _opt.Text = myModel(Count).stName & " -- " & myModel(Count).srDescription
        _opt.Value = myModel(Count).stName
        OptionList.Add(_opt)
        
        Count = Count + 1
    End While
End Code
<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/diag.css")' rel="stylesheet" type="text/css" />

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Aquí se muestran los formularios -->
<div id="main-ContIzquierda">

    <!-- Inicia diseño del formulario -->
    <h2 class="TituloFormulario--dg">Realizar pruebas</h2>
    <div class="form-Scann ">
        @Using Html.BeginForm()
            @<span class="row">
                <span class="Span-b"><span class="input-labelScann">@Html.LabelFor(Function(m) m.stName) </span></span>
                <span class="Span-e"><span class="input-textScann">@Html.DropDownListFor(Function(m) m.stName, OptionList)</span></span>
             </span>
            @<span class="row">&nbsp;</span>
            @<span class="row">
                <span class="Span-b"><span class="input-labelScann">@Html.LabelFor(Function(m) m.OrderID)</span></span>
                <span class="Span-e"><span class="input-textScann">@Html.TextBoxFor(Function(m) m.OrderID)</span></span>
            </span>
            @<span class="row">&nbsp;</span>
            @<span class="row">
                <span class="Span-a"><span class="input-labelScann">&nbsp;</span></span>
                <span class="Span-d"><span class="input-textScann">&nbsp;</span></span>
                <span class="Span-a"> <input type="submit" value="SCAN" class="input-ButtonScann" /></span>
             </span>
            
        End Using
    </div>
</div>

