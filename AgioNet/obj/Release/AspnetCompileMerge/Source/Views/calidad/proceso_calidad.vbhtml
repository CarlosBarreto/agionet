@ModelType AgioNet.ProcesoCalidadModels

@Code
    ViewData("Title") = "Calidad - En proceso de Calidad"
    Session("Section") = "calidad"
    
    Dim Read() As AgioNet.ProcesoCalidadModels = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Ordenes en proceso de Calidad</h2>

@grid.GetHtml(columns:=grid.Columns( _
    grid.Column("OrderID", "Orden"), _
    grid.Column("PartNumber", "Número de Parte"), _
    grid.Column("SerialNumber", "Número de Serie"), _
    grid.Column("FailureType", "Falla"), _
    grid.Column("Comment", "Comentarios") _
))
