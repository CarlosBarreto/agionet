@ModelType Agionet.ScanOrderModel
    
@Code
    ViewData("Title") = "Logística - Entrega Completa"
    Session("Section") = "logistica"
    
    Dim Read() As AgioNet.PendingProcessWarehouseModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=False)
End Code

<script type="text/javascript">
    $(document).ready(function () {
        $("#Comment").html("@User.Identity.Name : Equipo Entregado... ");
    });
</script>

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If


<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Proceso Entrega Completa</h2>

<div id="ContenedorOrderIDForm">
@Using Html.BeginForm()
    @<span class="row">
        <span class="Span-b">@Html.LabelFor(Function(m) m.OrderID) </span>
        <span class="Span-j">@Html.TextBoxFor(Function(m) m.OrderID, New With {.class = "text"})</span>
        
    </span>
    
    @<span class="row">&nbsp;</span>
    
    @<span class="row">
        <span class="Span-b">@Html.LabelFor(Function(m) m.Comment) </span>
        <span class="Span-j">@Html.TextAreaFor(Function(m) m.Comment, New With {.rows = 6, .cols = 53})</span>
     </span>
    @<span class="row">&nbsp;</span>
    @<span class="row">&nbsp;</span>
    @<span class="row">
        <span class="Span-b">&nbsp;</span>
        <span class="Span-j">&nbsp;</span>
        <span class="Span-a"> <input type="submit" value="SCAN" class="Button" /></span>
     </span>
End Using
</div>
<div id="xx" style="display:block; height: 80%; width:100%; overflow-y:scroll;">
    <h2 class="TituloFormulario">Listado de Ordenes Pendientes de Entregar </h2>

    @grid.GetHtml(columns:=grid.Columns( _
            grid.Column("OrderID", "Orden"), _
            grid.Column("Customer", "Cliente"), _
            grid.Column("ProductType", "Tipo de producto"), _
            grid.Column("SerialNo", "Numero de serie"), _
            grid.Column("Model", "Modelo"), _
            grid.Column("Description", "Descripcion") _
    ))

</div>
