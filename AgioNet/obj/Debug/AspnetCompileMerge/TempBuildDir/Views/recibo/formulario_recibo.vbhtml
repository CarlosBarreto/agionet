@ModelType Agionet.ReceiveIndfoModel

@Code
    ViewData("Title") = "Recibo - Formulario Recibo"
    Session("Section") = "recibo"
    
    Dim OrderID As String = Html.Encode(TempData("OrderID"))
    Dim ReRepair As String = Html.Encode(TempData("ReRepair"))
    Dim Warranty As String = Html.Encode(TempData("Warranty"))
    
    TempData.Keep("OrderID")
    TempData.Keep("ReRepair")
    TempData.Keep("Warranty")
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
    <h2 class="TituloFormulario--rec">Formulario de recibo</h2>

    @Using Html.BeginForm()
        @<div class="row">
            <span class="Span-b"><span class="pcenter">OrderID</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.OrderID, New With {.Value = OrderID}) </span>
        </div>

        @<div class="row">
            <span class="Span-b"> <span class="pcenter">Tipo Empaque</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.PackType) </span>

            <span class="Span-b"> <span class="pcenter">Empaque dañado</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.PackDamage) </span>
        </div>

        @<div class="row">
            <span class="Span-b"><span class="pcenter">Daño no Doc</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.NonDocPack) </span>
            <span class="Span-b"><span class="pcenter">Emp Correctamente</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.CorrectPack) </span>
        </div>

        @<div class="form-SpArea">
            <span class="Span-b"> <span class="pcenter">Accesorios</span></span>
            <span class="Span-c"> @Html.TextAreaFor(Function(m) m.Accesories, New With {.rows = 5}) </span>

            <span class="Span-b"><span class="pcenter">Cosmetico / Información Adicional</span></span>
            <span class="Span-c"> @Html.TextAreaFor(Function(m) m.Cosmetic, New With {.rows = 5}) </span>
        </div>

        @<div class="row">
            <span class="Span-b"><span class="pcenter">Garantía</span></span>
            <span class="Span-c">
                <input type="text" id="_Warranty" name="_Warranty" value="@Warranty" disabled="disabled" />
                @Html.HiddenFor(Function (m) m.Warranty, New With {.Value = Warranty})
            </span>
            
            <span class="Span-b"><span class="pcenter">Re-repair</span></span>
            <span class="Span-c"> 
                <input type="text" id="_Rerepair" name="_Warranty" value="@ReRepair" disabled="disabled" />
                @Html.HiddenFor(Function(m) m.rerepair, New With {.Value = ReRepair})
            </span>
        </div>

        @<div class="form-SpArea">
            <span class="Span-b"><span class="pcenter">Comentarios</span></span>
            <span class="Span-c"> @Html.TextAreaFor(Function(m) m.Comment, New With {.rows = 5}) </span>
        </div>

        @<div class="row">
            <span class="FRaight">
                <input type="submit" value="Guardar" class="Button" />
            </span>
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