@ModelType Agionet.setCheckinModel
    
@Code
    ViewData("Title") = "Almacen - Recibo de Almacén"
    Session("Section") = "almacen"
End Code
<script type="text/javascript">
    $(document).ready(function () {
        $("#Comment").html("@User.Identity.Name : Órden Recibida en Almacén... ");

        $("#Weight").change(function () {
            pushPeso();
        });

        $("#lenght").change(function () {
            pushPeso();
        });

        $("#Width").change(function () {
            pushPeso();
        });

        $("#Height").change(function () {
            pushPeso();
        });

        Math._ceil = Math.ceil;
        //Now let’s *sigh* replace the native Math.round with our own:

        Math.ceil = function (number, precision)
        {
            precision = Math.abs(parseInt(precision)) || 0;
            var coefficient = Math.pow(10, precision);
            return Math._ceil(number * coefficient) / coefficient;
        }

        function getPeso() {
            var L = $("#lenght").val();
            var W = $("#Width").val();
            var H = $("#Height").val();

            if (L == 0) { L = 1; }
            if (W == 0) { W = 1; }
            if (H == 0) { H = 1; }

            var result = (L * W * H / 5000);
            return Math.ceil(result, 2);
        }

        function pushPeso() {
            var Dimw = getPeso();
            var Ship = $("#Weight").val();

            $("#Dimweight").val(Dimw);

            if (Dimw > Ship) {
                $("#Shipweight").val(Dimw);
            }
            else {
                $("#Shipweight").val(Ship);
            }
        }
    });
</script>
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If


<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Proceso Recibo de Almacén</h2>

<div id="ContenedorOrderIDForm">
@Using Html.BeginForm()
    @<span class="row">
        <span class="Span-b">@Html.LabelFor(Function(m) m.OrderID) </span>
        <span class="Span-j">@Html.TextBoxFor(Function(m) m.OrderID, New With {.class = "text"})</span>
        
    </span>
    
    @<span class="row">&nbsp;</span>
    
    @<span class="row">
        <span class="Span-b">@Html.LabelFor(Function(m) m.Guia)</span>
        <span class="Span-j">@Html.TextBoxFor(Function(m) m.Guia, New With {.class = "text"})</span>
     </span>
    
    @<span class="row">&nbsp;</span>
    
    @<span class="row">
        <span class="Span-b">@Html.LabelFor(Function(m) m.Comment) </span>
        <span class="Span-j">@Html.TextAreaFor(Function(m) m.Comment, New With {.rows = 6, .cols = 53})</span>
     </span>
    
    @<span class="row">&nbsp;</span>
    @<span class="row">&nbsp;</span>
    @<span class="row">&nbsp;</span>
    @<div class="row">
        <span class="Span-c"><label> Longitud</label></span>
        <span class="Span-b">
            @Html.TextBoxFor(Function(m) m.Lenght, New With {.class = "text"})
        </span>

        <span class="Span-c"><label>Ancho</label></span>
        <span class="Span-b">
            @Html.TextBoxFor(Function(m) m.width, New With {.class = "text"})
        </span>
    </div>
    @<span class="row">&nbsp;</span>
    @<div class="row">
        <span class="Span-c"><label>Altura</label></span>
        <span class="Span-b">
            @Html.TextBoxFor(Function(m) m.Height, New With {.class = "text"})
        </span>

        <span class="Span-c"><label>Peso Neto</label></span>
        <span class="Span-b">
            @Html.TextBoxFor(Function(m) m.Weight, New With {.class = "text"})
        </span>
    </div>
    @<span class="row">&nbsp;</span>
    @<div class="row">
        <span class="Span-c"><label>Peso vol</label></span>
        <span class="Span-b">
            @Html.TextBoxFor(Function(m) m.Dimweight, New With {.class = "text"})
        </span>

        <span class="Span-c"><label>Peso Embarque</label></span>
        <span class="Span-b">
            @Html.TextBoxFor(Function(m) m.Shipweight, New With {.class = "text"})
        </span>
    </div>
    
    @<span class="row">&nbsp;</span>
    @<div class="row">
        <span class="Span-c">&nbsp;</span>
        <span class="Span-c">&nbsp;</span>
        <span class="Span-b">&nbsp;</span>
        <span class="Span-b">
            <input type="submit" value="SCAN" class="Button" />
        </span>
    </div>
    

End Using
</div>
