@ModelType AgioNet.GuiaCompraModel

@Code
    ViewData("Title") = "Servicio a clientes - Comprar"
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
        var data = "@Read.Comentario";
        if (data != "") {
            $("#Comentario").html("@Replace(Read.Comentario, vbNewLine, "\n")");
        }

        $("#LeadTime").datepicker({ dateFormat: "dd/mm/yy" });

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
    <h2 class="TituloFormulario">Cargar Guia a la Compra</h2>
    <div id="Formulario">
        @Using Html.BeginForm()
           @<div id="Formulario6">
                <div class="row">
                    <span class="Span-b"><span class="PCenter">Guía de Compra</span></span>
                    <span class="Span-e"> 
                        @Html.HiddenFor(Function(m) m.OrderID, New With {.Value = Session("OrderID")})
                        @Html.TextBoxFor(Function(m) m.TrackNo)</span>
                </div>
               <div class="row">
                    <span class="Span-c">&nbsp;</span>
                    <span class="Span-c">&nbsp;</span>
                    <span class="Span-a"><input type="submit" value="Enviar" class="Button" /></span>
                </div>
            </div> 
        End Using
    </div>

    <h2 class="TituloFormulario">Datos de la Compra</h2>
    <div id="Formulario3">
        <div class="row">
            <span class="Span-a"><span class="PCenter">Tipo Reparación</span></span>
            <span class="Span-c"> @Html.TextBox("TipoReparacion", TipoReparacion, New With {.Disabled = "True"})</span>
        </div>

        <div class="row">
            <span class="Span-a"> <span class="PCenter">Orden: </span> </span>
            <span class="Span-c"> @Html.TextBox("OrderID", Session("OrderID"), New With {.Disabled = "True"}) </span>
        </div>
        <div class="row">
            <span class="Span-a"><span class="PCenter">Costo</span></span>
            <span class="Span-c"> @Html.TextBox("Costo", Read.Costo, New With {.Disabled = "True"})  </span>
        </div>

        <div class="row">
            <span class="Span-a"><span class="PCenter">Flete</span></span>
            <span class="Span-c"> @Html.TextBox("Flete", Read.Flete, New With {.Disabled = "True"})  </span>
        </div>

        <div class="row">
            <span class="Span-a"><span class="PCenter">Gastos de Importación</span></span>
            <span class="Span-c"> @Html.TextBox("GastosImportacion", Read.GastosImportacion, New With {.Disabled = "True"})  </span>
        </div>

        <div class="row">
            <span class="Span-a"><span class="PCenter">LeadTime</span></span>
            <span class="Span-c"> @Html.TextBox("LeadTime", Read.LeadTime, New With {.Disabled = "True"}) </span>
            <span class="Span-a">(dd/mm/aaaa)</span>
        </div>
                
        <div class="row">
            <span class="Span-a"><span class="PCenter">Proveedor</span></span>
            <span class="Span-c"> @Html.TextBox("Proveedor", Read.Proveedor, New With {.Disabled = "True"})  </span>
        </div>

        <div class="row">
            <span class="Span-a"><span class="PCenter">Comentarios:</span></span>
            <span class="Span-e">@Html.TextArea("Comentario", New With {.cols = 45, .rows = 3, .Disabled = "True"})</span>
        </div>
        <div class ="row">&nbsp;</div>
        <div class ="row">&nbsp;</div>
        <div class="row">
            <span class="Span-a"><span class="PCenter">CostBy</span></span>
            <span class="Span-c">@Html.TextBox("CostBy", User.Identity.Name, New With {.Disabled = "True"}) </span>
        </div>
    </div>
</div>

<!-- Aquí va la información de la orden -->
<div id="main-ContDerecha" class="bg-fondoborder">
    @Html.Action("LoadOrderInfo")
</div>
