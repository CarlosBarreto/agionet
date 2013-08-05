@ModelType AgioNet.ApprovalRepairModel

@Code
    ViewData("Title") = "Servicio a clientes - Autorización de Reparación"
    Session("Section") = "sc"
    
    Dim Read As AgioNet.CotizacionInfoModel = TempData("FailureInfo")
    'Dim Order As Object = TempData("OrderInfo")
    
    'Dim grid As WebGrid = New WebGrid(Read)
    Dim OptionList As List(Of SelectListItem) = New List(Of SelectListItem)
    Dim OptionList2 As List(Of SelectListItem) = New List(Of SelectListItem)
    
    Dim _opt As New SelectListItem With {.Text = "Aprobado", .Value = "AP"}
    OptionList.Add(_opt)
    _opt = New SelectListItem With {.Text = "No Aprobado", .Value = "NAP"}
    OptionList.Add(_opt)
    
    _opt = New SelectListItem With {.Text = "Cliente", .Value = "Cliente"}
    OptionList2.Add(_opt)
    _opt = New SelectListItem With {.Text = "Tercero", .Value = "Tercero"}
    OptionList2.Add(_opt)
    
    Dim OrderID As String = TempData("OrderID")
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
    <h2 class="TituloFormulario">Autorización del Item [OrderID]</h2>
    <div id="Formulario">
        @Using Html.BeginForm()
            @<div id="Formulario6">
                <div class="row">
                  <span class="Span-b"> <span class="PCenter">Aprobación: </span> </span>
                  <span class="Span-c"> @Html.DropDownListFor(Function(m) m.Approval, OptionList) </span>
                </div>
                <div class="row">
                    <span class="Span-b"><span class="PCenter">Gasto Absorvido Por</span></span>
                    <span class="Span-c">  @Html.DropDownListFor(Function(m) m.Absorvido, OptionList2) </span>
                </div>
                <div class="row">
                    <span class="Span-b">Comentarios:</span>
                    <span class="Span-e">@Html.TextAreaFor(Function(m) m.Comments, New With {.cols = 45, .rows = 3})</span>
                </div>
                <div class ="row">&nbsp;</div>
                
                <div class ="row">&nbsp;</div>
                <div class="row">
                    <span class="Span-c">&nbsp;</span>
                    <span class="Span-c">&nbsp;</span>
                    <span class="Span-a"><input type="submit" value="Enviar" class="Button" /></span>
                </div>
                <div class ="row">&nbsp;</div>
            </div>
        End Using

    </div>
    <div id="ContInfo2" style="margin-left:5px; width:500px;" class="bg-fondoborder">
       <div class="form-row">&nbsp;</div>
        <div class="form-row">
            <span class="Span-b"><strong>OrderID: </strong></span>
            <span class="Span-e">@Read.OrderID</span>
        </div>
        
        <div class="form-row">
            <span class="Span-b"><strong>Lead Time: </strong></span>
            <span class="Span-e">@Read.LeadTime</span>
        </div>
        <div class="form-row">&nbsp;</div>

        <div class="form-row">
            <span class="Span-b"><strong>Proveedor: </strong></span>
            <span class="Span-e">@Read.Proveedor</span>
        </div>
        <div class="form-row">&nbsp;</div>

        <div class="form-row">
            <span class="Span-b"><strong>Comentario</strong> </span>
            <span class="Span-e">@Read.Comentario</span>
        </div>
        <div class="form-row">&nbsp;</div>
        <div class="form-row">&nbsp;</div>
    </div>
</div>

<!-- Aquí va la información de la orden -->
<div id="main-ContDerecha" class="bg-fondoborder">
    @Html.Action("LoadOrderInfo")
</div>