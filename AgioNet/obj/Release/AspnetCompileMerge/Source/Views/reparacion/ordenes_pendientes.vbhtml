@ModelType AgioNet.PendientesreparacionModel

@Code
    ViewData("Title") = "Reparacion - Pendientes de Reparar"
    Session("Section") = "reparacion"
    
    Dim Read() As AgioNet.PendientesreparacionModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Ordenes Pendientes de Reparar</h2>

@grid.GetHtml(columns:=grid.Columns( _
    grid.Column("OrderID", "Orden"), _
    grid.Column("Comments", "Comentario Ap"), _
    grid.Column("ApprovalDate", "Aprobado el"), _
    grid.Column("PartNumber", "Número de Parte"), _
    grid.Column("ProductDescription", "Descripción"), _
    grid.Column("SerialNumber", "Número de Serie"), _
    grid.Column("Failure", "Failure"), _
    grid.Column("Solution", "Solution"), _
    grid.Column("Source", "Source"), _
    grid.Column("Comment", "Comentarios") _
))
