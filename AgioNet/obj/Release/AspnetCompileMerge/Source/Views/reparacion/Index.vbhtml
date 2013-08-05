@ModelType AgioNet.SearchOrderModel

@Code
    ViewData("Title") = "Pagina Principal Reparación"
    Session("Section") = "reparacion"
    
    Dim Read() As AgioNet.PendingOrdersModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=False)
End Code

<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/diag.css")' rel="stylesheet" type="text/css" />

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<h2 class="TituloFormulario">Proceso de Reparación de Órden</h2>

<div id="ContenedorOrderIDForm">
@Using Html.BeginForm()
    @<span class="row">
        <span class="Span-b">@Html.LabelFor(Function(m) m.OrderID) </span>
        <span class="Span-j">@Html.TextBoxFor(Function(m) m.OrderID, New With {.class = "text"})</span>
        <span class="Span-a"> <input type="submit" value="SCAN" class="Button" /></span>
    </span>
End Using
</div>

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Órdenes Pendientes de Reparar</h2>
 @grid.GetHtml(columns:=grid.Columns( _
            grid.Column("OrderID", "Orden"), _
            grid.Column("ProductType", "Tipo de producto"), _
            grid.Column("SerialNo", "Numero de serie"), _
            grid.Column("Model", "Modelo"), _
            grid.Column("Description", "Descripcion"), _
            grid.Column("DateR", "Fecha de ingreso"), _
            grid.Column("DateComp", "Fecha Compromiso") _
    ))

<!-- grid.Column("Reparar", "Reparar", format:=Function(item) Html.ActionLink("Reparar", "Index", "reparacion", New With {.onClick = "clicked(" & Chr(34) & item.OrderID & Chr(34) & "); return false"})) _ -->