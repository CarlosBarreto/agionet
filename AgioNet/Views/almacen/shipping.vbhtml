@ModelType Agionet.ShippingModel
    
@Code
    ViewData("Title") = "Almacen - Proceso de Embarque de Producto Terminado"
    Session("Section") = "almacen"
    
    Dim Read() As AgioNet.PendingProcessWarehouseModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=False)
End Code

<script type="text/javascript">
    $(document).ready(function () {
        $("#Comment").html("@User.Identity.Name : Procesar orden... ");
    });
</script>

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If


<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Proceso de Embarque de Producto Terminado</h2>

<div id="ContenedorOrderIDForm" style="height:200px; display:inline-block">
@Using Html.BeginForm()
    @<span class="row">
        <span class="Span-i"><label>Orden:</label> </span>
        <span class="Span-j">@Html.TextBoxFor(Function(m) m.OrderID, New With {.class = "text"})</span>    
    </span>
    @<span class="row">&nbsp;</span>
    
     @<span class="row">
        <span class="Span-i"><label>Guía:</label> </span>
        <span class="Span-j">@Html.TextBoxFor(Function(m) m.TRackNo, New With {.class = "text"})</span>    
    </span>
    @<span class="row">&nbsp;</span>
    
    @<span class="row-textarea">
        <span class="Span-i"><label>Comentario:</label> </span>
        <span class="Span-e">@Html.TextAreaFor(Function(m) m.Comment, New With {.rows = 3, .cols = 65})</span>
        <span class="Span-a"> <input type="submit" value="SCAN" class="Button" /></span>
     </span>
    @<span class="row">&nbsp;</span>
    
End Using
</div>

<div id="xx" style="display:block; height: 80%; width:100%; overflow-y:auto; overflow-x:hidden;">
    <h2 class="TituloFormulario">Listado de Ordenes Pendientes de Embarcar</h2>

    @grid.GetHtml(columns:=grid.Columns( _
            grid.Column("OrderID", "Orden"), _
            grid.Column("ProductType", "Tipo de producto"), _
            grid.Column("SerialNumber", "Numero de serie"), _
            grid.Column("ProductModel", "Modelo"), _
            grid.Column("ProductDescription", "Descripcion"), _
            grid.Column("Ingresado", "Ingresado"), _
            grid.Column("Procesado", "Recibo PT"), _
            grid.Column("DateComp", "Fecha Compromiso") _
    ))

</div>
 