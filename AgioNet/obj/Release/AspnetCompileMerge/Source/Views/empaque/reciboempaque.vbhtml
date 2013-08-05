@ModelType Agionet.ScanOrderModel
    
@Code
    ViewData("Title") = "Empaque - Recibo de Empaque Producto Terminado"
    Session("Section") = "empaque"
    Dim data As Object = TempData("Model")

End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/diag.css")' rel="stylesheet" type="text/css" />

<span class="row">&nbsp;</span>
<div id="main-ContIzquierda" class="bg-fondoborder">
    <div class="form-row">&nbsp</div>
    <div class="form-row">&nbsp; <strong style="color: blue;">INFORMACIÓN DE INGRESO</strong></div>
    <div class="form-row">
        <span class="Span-k"><strong>Fecha Ingreso: </strong> @data.checkinDate </span>
    </div>
    <div class="form-row">
        <span class="Span-k"><strong>Tipo empaque: </strong> @data.PackType </span>
    </div>
    <div class="form-row">
        <span class="Span-k"><strong>Accesorios: </strong> @data.Accesories</span>
    </div>
    <div class="form-row">&nbsp</div>
    <div class="form-row">
        <span class="Span-k"><strong>Consmetico / Observaciones: </strong> @data.Cosmetic </span>
    </div>
    <div class="form-row">&nbsp</div>
    <div class="form-row">
        <span class="Span-k"><strong>Comentarios: </strong> @data.Comment </span>
    </div>
    <div class="form-row">&nbsp</div>
    <div class="form-row">
        <span class="Span-k"><strong>Falla Reportada: </strong> @data.ReportedFailure </span>
    </div>
    <div class="form-row">&nbsp</div>
    <div class="form-row">&nbsp</div>
</div>

<div id="main-ContDerecha" class="bg-fondoborder">
    <div class="form-row">
        <span class="Span-e"><strong>Orden: </strong>  @data.OrderId </span>
    </div>

    <div class="form-row">
        <span class="Span-e"><strong>Número de Serie: </strong>  @data.SerialNo </span>
    </div>

    <div class="form-row">
        <span class="Span-e"><strong>SKU No: </strong> @data.PartNo </span>
    </div>

    <div class="form-row">
        <span class="Span-e"><strong>Modelo:</strong>  @data.Model </span>
    </div>

    <div class="form-row">
        <span class="Span-e"><strong>Descripción: </strong> @data.Descripcion </span>
    </div>
    <div class="form-row">
        <span class="Span-e"><strong>Marca: </strong> @data.Trademark </span>
    </div>
</div>

<span class="form-row">&nbsp;</span>
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Proceso Recibo de Empaque Producto Terminado</h2>
<div id="ContenedorOrderIDForm">
@Using Html.BeginForm()
    @<span class="row">
        <span class="Span-c"><label>Comentarios</label> </span>
        <span class="Span-d">
            @Html.HiddenFor(Function(m) m.OrderID, New With { .Value = data.OrderID})
            @Html.TextAreaFor(Function(m) m.Comment, New With {.rows = 6, .cols = 53})
        </span>
        <span class="Span-i"> <input type="submit" value="Empacar" class="Button" /></span>
     </span>
    @<span class="row">&nbsp;</span>
    @<span class="row">&nbsp;</span>
End Using
</div>



<script type="text/javascript">
    $(document).ready(function () {
        $("#Comment").html("@User.Identity.Name : Recibo en empaque... ");
    });
</script>