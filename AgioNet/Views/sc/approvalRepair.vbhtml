@ModelType AgioNet.OrderListModel

@Code
    ViewData("Title") = "Servicio a clientes - Autorización de Reparación"
    Session("Section") = "sc"
    
    Dim Read As AgioNet.AppFailureInfoModel = TempData("FailureInfo")
    'Dim Order As Object = TempData("OrderInfo")
    
    'Dim grid As WebGrid = New WebGrid(Read)
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
    <h2 class="TituloFormulario">Información de la falla</h2>
    <div id="Formulario">
        @Using Html.BeginForm()
            @<div id="Formulario2">
                <div class="row">
                  <span class="Span-c"></span>
                </div>
                <div class="row"></div>
            </div>
        End Using

        <div class="row">
            <span class="Span-b"><strong>OrderID: </strong></span>
            <span class="Span-e">@Read.OrderID</span>
        </div>

        <div class="row">
            <span class="Span-b"><strong>TestID: </strong></span>
            <span class="Span-e">@Read.TestID</span>
        </div>

        <div class="row">
            <span class="Span-b"><strong>TestDescription: </strong></span>
            <span class="Span-e">@Read.TestDescription</span>
        </div>

        <div class="row">
            <span class="Span-b"><strong>FailureID</strong> </span>
            <span class="Span-e">@Read.FailureID</span>
        </div>

        <div class="row">
            <span class="Span-b"><strong>Solution: </strong> </span>
            <span class="Span-e">@Read.Source</span>
        </div>

        <div class="row">
            <span class="Span-b"><strong>Source: </strong></span>
            <span class="Span-e">@Read.Source</span>
        </div>

        <div class="row">
            <span class="Span-b"><strong>FoundBy: </strong></span>
            <span class="Span-e">@Read.FoundBy</span>
        </div>

        <div class="row">
            <span class="Span-b"><strong>FoundDate: </strong></span>
            <span class="Span-e">@Read.FoundDate</span>
        </div>
    </div>
</div>
<!-- Aquí va la información de la orden -->
<div id="main-ContDerecha" class="bg-fondoborder">
    @Html.Action("LoadOrderInfo")
</div>

<div class="form">
    
</div>
