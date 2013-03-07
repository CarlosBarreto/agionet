@ModelType AgioNet.AppCostModel

@Code
    ViewData("Title") = "Servicio a clientes - Costeo"
    Session("Section") = "sc"
    
    Dim Read As AgioNet.AppCostModel = TempData("model")
    'Dim Order As Object = TempData("OrderInfo")
    
    'Dim grid As WebGrid = New WebGrid(Read)
    Dim OptionList As List(Of SelectListItem) = New List(Of SelectListItem)
    
    Dim _opt As New SelectListItem With {.Text = "Aprovado", .Value = "AP"}
    OptionList.Add(_opt)
    _opt = New SelectListItem With {.Text = "No Aprobado", .Value = "NAP"}
    OptionList.Add(_opt)
    
End Code
<script type="text/javascript"> 
    $(document).ready(function () {
        var data = "@Read.Comentario";
        if (data != "") {
            $("#Comentario").html("@Replace(Read.Comentario, vbNewLine, "\n")");
        }
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
    <h2 class="TituloFormulario">Costear una Orden</h2>
    <div id="Formulario">
        @Using Html.BeginForm()
            @<div id="Formulario2">
                <div class="row">
                  <span class="Span-a"> <span class="PCenter">Orden: </span> </span>
                  <span class="Span-c"> @Html.TextBoxFor(Function(m) m.OrderID, New With {.Value = Session("OrderID")}) </span>
                </div>
                <div class="row">
                    <span class="Span-a"><span class="PCenter">Costo</span></span>
                    <span class="Span-c"> @Html.TextBoxFor(Function(m) m.Costo, New With { .Value = Read.Costo })  </span>
                </div>
                <div class="row">
                    <span class="Span-a"><span class="PCenter">LeadTime</span></span>
                    <span class="Span-c"> @Html.TextBoxFor(Function(m) m.LeadTime, New With {.Value = Read.LeadTime}) </span>
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
