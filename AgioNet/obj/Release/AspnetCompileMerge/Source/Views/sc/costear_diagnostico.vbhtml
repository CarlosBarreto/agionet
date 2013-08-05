@ModelType AgioNet.AppCostModel

@Code
    ViewData("Title") = "Servicio a clientes - Costeo"
    Session("Section") = "sc"
    
    Dim Read As AgioNet.AppCostModel = TempData("model")
    Dim TipoReparacion As String = TempData("TipoReparacion")
    'Dim Order As Object = TempData("OrderInfo")
    
    'Dim grid As WebGrid = New WebGrid(Read)
    Dim OptionList As List(Of SelectListItem) = New List(Of SelectListItem)
    
    Dim _opt As New SelectListItem With {.Text = "Aprovado", .Value = "AP"}
    OptionList.Add(_opt)
    _opt = New SelectListItem With {.Text = "No Aprobado", .Value = "NAP"}
    OptionList.Add(_opt)
    
End Code
<script src='@Url.Content("~/Scripts/jquery.agiotech.js")' type="text/javascript"></script>

<script type="text/javascript"> 
    $(document).ready(function () {
        //Iniciar validacion
        $("#Costo").val("0");
        $("#Flete").val("0");
        $("#GastosImportacion").val("0");

        $("#Costo").numeric({ prefix: '$ ', cents: true });
        $("#Flete").numeric({ prefix: '$ ', cents: true });
        $("#GastosImportacion").numeric({ prefix: '$ ', cents: true });

        var data = "@Read.Comentario";
        if (data != "") {
            $("#Comentario").html("@Replace(Read.Comentario, vbNewLine, "\n")");
        }

        $("#LeadTime").datepicker({
            changeMonth: true,
            changeYear: true,
            showOtherMonths: true,
            selectOtherMonths: true,
            showButtonPanel: true,
            dateFormat: "dd/mm/yy",
            firstDay: 1,
            showOn: "both",
            buttonImage: "@Url.Content("~/Content/css/agioTheme/images/calendar.gif")"
        });

        $("#Costo").change(function () { 
            $("#Costo").numeric({ prefix: '$ ', cents: true });
        });

        $("#Flete").change(function () {
            $("#Flete").numeric({ prefix: '$ ', cents: true });
        });

        $("#GastosImportacion").change(function () {
            $("#GastosImportacion").numeric({ prefix: '$ ', cents: true });
        });

    }); 
</script>

<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/diag.css")' rel="stylesheet" type="text/css" />

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Aquí se muestran los formularios -->
<div id="main-ContIzquierda">
    <!-- Inicia diseño del formulario -->
    <h2 class="TituloFormulario">Costear una Parte</h2>
    <div id="Formulario">
        @Using Html.BeginForm()
            @<div id="Formulario6">
                <div class="row">
                    <span class="Span-a"><span class="PCenter">Tipo Reparación</span></span>
                    <span class="Span-c"> @Html.TextBox("TipoReparacion", TipoReparacion, New With {.Disabled = "True"})</span>
                </div>

                <div class="row">
                  <span class="Span-a"> <span class="PCenter">Orden: </span> </span>
                  <span class="Span-c"> @Html.TextBoxFor(Function(m) m.OrderID, New With {.Value = Session("OrderID")}) </span>
                </div>
                <div class="row">
                    <span class="Span-a"><span class="PCenter">Costo</span></span>
                    <span class="Span-c"> @Html.TextBoxFor(Function(m) m.Costo, New With { .Value = Read.Costo })  </span>
                </div>

                <div class="row">
                    <span class="Span-a"><span class="PCenter">Flete</span></span>
                    <span class="Span-c"> @Html.TextBoxFor(Function(m) m.Flete, New With {.Value = Read.Flete})  </span>
                </div>

                <div class="row">
                    <span class="Span-a"><span class="PCenter">Gastos de Importación</span></span>
                    <span class="Span-c"> @Html.TextBoxFor(Function(m) m.GastosImportacion, New With {.Value = Read.GastosImportacion})  </span>
                </div>

                <div class="row">
                    <span class="Span-a"><span class="PCenter">LeadTime</span></span>
                    <span class="Span-c"> @Html.TextBoxFor(Function(m) m.LeadTime, New With {.Value = Read.LeadTime, .Style="width: 75%;"}) </span>
                    <span class="Span-a">(dd/mm/aaaa)</span>
                </div>
                
                <div class="row">
                    <span class="Span-a"><span class="PCenter">Proveedor</span></span>
                    <span class="Span-c"> @Html.TextBoxFor(Function(m) m.Proveedor, New With {.Value = Read.Proveedor})  </span>
                </div>

                <div class="row">
                    <span class="Span-a"><span class="PCenter">Comentarios:</span></span>
                    <span class="Span-e">@Html.TextAreaFor(Function(m) m.Comentario, New With {.cols = 45, .rows = 3})</span>
                </div>
                <div class ="row">&nbsp;</div>
                <div class ="row">&nbsp;</div>
                <div class="row">
                    <span class="Span-a"><span class="PCenter">CostBy</span></span>
                    <span class="Span-c">@Html.TextBoxFor(Function(m) m.CostBy, New With {.Value = User.Identity.Name}) </span>
                </div>
                <div class="row">
                    <span class="Span-c">&nbsp;</span>
                    <span class="Span-c">&nbsp;</span>
                    <span class="Span-a"><input type="submit" value="Enviar" class="Button" /></span>
                </div>
                <div class ="row">&nbsp;</div>
            </div>
        End Using

        </div>
    </div>


<!-- Aquí va la información de la orden -->
<div id="main-ContDerecha" class="bg-fondoborder">
    @Html.Action("LoadOrderInfo")
</div>
