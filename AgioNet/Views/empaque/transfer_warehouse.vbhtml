@ModelType Agionet.ScanOrderModel
    
@Code
    ViewData("Title") = "Empaque - Mover al almacén (Transfer)"
    Session("Section") = "empaque"
    Dim Read() As AgioNet.reciboEmpaqueModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=false)
End Code
<script type="text/javascript">
    $(document).ready(function () {
        $("#Comment").html("@User.Identity.Name : Recibo en empaque... ");
    });
</script>
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Proceso Mover al Almacén</h2>


<div id="ContenedorOrderIDForm">
@Using Html.BeginForm()
    @<span class="row">
        <span class="Span-b">@Html.LabelFor(Function(m) m.OrderID) </span>
        <span class="Span-j">@Html.TextBoxFor(Function(m) m.OrderID, New With {.class = "text"})</span>
        
    </span>
    
    @<span class="row">&nbsp;</span>
    
       @<span class="row">
        <span class="Span-b">@Html.LabelFor(Function(m) m.Comment) </span>
        <span class="Span-e">@Html.TextAreaFor(Function(m) m.Comment, New With {.rows = 6, .cols = 53})</span>
        <span class="Span-a">&nbsp;</span>
        <span class="Span-a"> <input type="submit" value="SCAN" class="Button" /></span>
     </span>
    @<span class="row">&nbsp;</span>
    @<span class="row">&nbsp;</span>
End Using
</div>
<br /><br />
<h2 class="TituloFormulario">Listado de Ordenes Pendientes de Transferir al Almacén</h2>
@grid.GetHtml(columns:=grid.Columns( _
    grid.Column("OrderID", "Orden"), _
    grid.Column("ProductType", "Tipo de producto"), _
    grid.Column("SerialNo", "Numero de serie"), _
    grid.Column("Model", "Modelo"), _
    grid.Column("Description", "Descripcion"), _
    grid.Column("Entrega", "Fecha Compromiso Entrega") _
))
