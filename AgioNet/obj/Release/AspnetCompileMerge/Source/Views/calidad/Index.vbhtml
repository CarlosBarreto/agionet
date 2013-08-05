@ModelType AgioNet.SearchOrderModel

@Code
    ViewData("Title") = "Pagina Principal de Calidad"
    Session("Section") = "calidad"
    
    Dim Read() As AgioNet.ProcesoCalidadModels = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=False)
End Code

<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/diag.css")' rel="stylesheet" type="text/css" />

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<h2 class="TituloFormulario">Proceso de Calidad</h2>

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
<h2 class="TituloFormulario">Ordenes en proceso de Calidad</h2>

@grid.GetHtml(columns:=grid.Columns( _
    grid.Column("OrderID", "Orden"), _
    grid.Column("PartNumber", "Número de Parte"), _
    grid.Column("SerialNumber", "Número de Serie"), _
    grid.Column("FailureType", "Falla"), _
    grid.Column("Comment", "Comentarios"), _
    grid.Column("DateComp", "Fecha Compromiso") _
))
<!-- Upd by carlosB 2013.05-10 --
  grid.Column("Reparar", "Reparar", format:=Function(item) Html.ActionLink("Inspección", "inspeccion_calidad", "calidad", New With {.OrderID = item.OrderID}, vbNull)) _
  - Quitar boton de reparar 
 -->  