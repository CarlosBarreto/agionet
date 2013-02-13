@ModelType Agionet.PendingOrdersModel
    
@Code
    ViewData("Title") = "Recibo - Ingresar orden"
    Session("Section") = "recibo"
    
    Dim Read() As AgioNet.PendingOrdersModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read)
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<h2 class="TituloFormulario">Ingresar una Orden</h2>

<div id="ContenedorOrderIDForm">
@Using Html.BeginForm()
    @<span class="row">
        <span class="Span-b">@Html.LabelFor(Function(m) m.OrderID) </span>
        <span class="Span-j">@Html.TextBoxFor(Function(m) m.OrderID, New With {.class = "text"})</span>
        <span class="Span-a"> <input type="submit" value="SCAN" class="Button" /></span>
    </span>
End Using
</div>

<h2 class="TituloFormulario">Listado de Ordenes Pendientes de recibir</h2>

@grid.GetHtml(columns:=grid.Columns( _
                   grid.Column("OrderID", "Orden"), _
                   grid.Column("Customer", "Cliente"), _
                   grid.Column("ProductType", "Tipo de producto"), _
                   grid.Column("SerialNo", "Numero de serie"), _
                   grid.Column("Model", "Modelo"), _
                   grid.Column("Description", "Descripcion"), _
                   grid.Column(format:=Function(item) Html.ActionLink("Recibir", "scan_info", "recibo", New With {.OrderID = item.OrderID}, vbNull)) _
))