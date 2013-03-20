@ModelType AgioNet.ScanOrderModel

@Code
    ViewData("Title") = "Reparacion - Pendientes de Reparar"
    Session("Section") = "reparacion"
    
    Dim Read() As AgioNet.PendientesreparacionModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read)
End Code
<script type="text/javascript">
    function clicked(value) {
        //alert("Otra cosa"+value);
        $("#OrderID").val(value);
        $(".Formulario").submit();
    }

</script>
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Ordenes Pendientes de Reparar</h2>
@Using Html.BeginForm("Index", "reparacion", FormMethod.Post, New With {.class = "Formulario"})
    @Html.Hiddenfor(Function(m) m.OrderID)
End Using

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
    grid.Column("Comment", "Comentarios"), _
    grid.Column("Reparar", "Reparar", format:=Function(item) Html.ActionLink("Reparar", "Index", "reparacion", New With {.onClick = "clicked(" & Chr(34) & item.OrderID & Chr(34) & "); return false"})) _
))
