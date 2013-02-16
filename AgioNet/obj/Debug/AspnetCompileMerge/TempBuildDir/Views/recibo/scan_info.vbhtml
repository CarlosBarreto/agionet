@ModelType AgioNet.ScanInfoModel

@Code
    ViewData("Title") = "Recibo - Ingresar información de la orden"
    Session("Section") = "recibo"
    
    Dim OrderID As String = Html.Encode(TempData("OrderID"))
    TempData.Keep("OrderID")
    TempData("Opt") = True
End Code
<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/reccss.css")' rel="stylesheet" type="text/css" />

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Aquí se muestran los formularios -->
<div id="main-ContIzquierda">
    <!-- Inicia diseño del formulario -->
    <h2 class="TituloFormulario--rec">Ingresar información de la orden</h2>

    @Using Html.BeginForm()
        @<div class="row">
            <span class="Span-b"><span class="pcenter">OrderID</span></span>
            <span class="Span-l"> @Html.TextBoxFor(Function(m) m.OrderID, New With {.Value = OrderID}) </span>
        </div>
        
        @<div class="row">
            <span class="Span-b"><span class="pcenter">Número de Parte</span></span>
            <span class="Span-l"> @Html.TextBoxFor(Function(m) m.PartNo) </span>

            <span class="Span-a"><span class="pcenter">Modelo</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.Model) </span>
         </div>
        
        @<div class="row">
            <span class="Span-b"><span class="pcenter">Número de Serie</span></span>
            <span class="Span-l"> @Html.TextBoxFor(Function(m) m.SerialNo) </span>
         </div>
        
        @<div class="row">
            <span class="Span-b"><span class="pcenter">TrackNo</span></span>
            <span class="Span-l"> @Html.TextBoxFor(Function(m) m.TrackNo)</span>
         </div>
        
        @<div class="row">
            <span class="Span-b"><span class="pcenter">ScanDate</span></span>
            <span class="Span-l"> @Html.TextBoxFor(Function(m) m.ScanDate, New With {.Value = Now()}) </span>
            
            <span class="Span-a"><span class="pcenter">Scan By</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.ScanBy, New With {.Value = User.Identity.Name}) </span>
         </div>
        
        @<div class="row"><!--Espacio en blanco -->&nbsp;</div>
        
        @<div class="row">
            <span class="Span-b">&nbsp;</span>
            <span class="Span-l">&nbsp;</span>
            <span class="Span-c">&nbsp;</span>
            <span class="Span-a"><input type="submit" value="Guardar" class="Button" /></span>
         </div>
    End Using

    <div class="form-aclaracion">
        <span class="form-aclaracion_Title">Nota:</span>
        <span class="form-aclaracion_Text">
            En caso de de que el <strong>Modelo</strong>, el <strong>Número de parte</strong>, o la <strong>Marca</strong> del producto 
            no esté dado de alta en el sistema, Favor de contactar a ingeniería 
            (Encargado, <a href="mailto:humberto.vega@agiotech.com">Humberto Vega</a> )
        </span>

    </div>
</div>

<!-- Aquí va la información de la orden -->
<div id="main-ContDerecha" class="bg-fondoborder">
    @Html.Action("LoadOrderInfo")
</div>