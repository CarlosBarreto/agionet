@ModelType Agionet.SavePartesCompradasModel
    
@Code
    ViewData("Title") = "Almacen - Proceso de Recibo de Partes Compradas"
    Session("Section") = "almacen"
    
    Dim Read As AgioNet.ReciboPartesCompradasModel = TempData("Model")
End Code
<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/diag.css")' rel="stylesheet" type="text/css" />

<script type="text/javascript">
    $(document).ready(function () {
        $("#Comment").html("@User.Identity.Name : Procesar orden... ");
    });
</script>

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Aquí se muestran los formularios -->
<div id="main-ContIzquierda">
    <!-- Inicia diseño del formulario -->
    <h2 class="TituloFormulario">Proceso de Recibo de Partes Compradas</h2>
    <div id="Formulario">
        @Using Html.BeginForm()
            @<div id="Formulario6">
                <div class="row">
                    <span class="Span-b"><span class="PCenter">Orden</span></span>
                    <span class="Span-c"> @Html.TextBoxfor(function(m) m.OrderID, New With {.Value=Read.OrderID})</span>
                </div>

                <div class="row">
                    <span class="Span-b"><span class="PCenter">Numero de Parte</span></span>
                    <span class="Span-c"> @Html.TextBoxFor(Function(m) m.PartNo, New With {.Value = Read.PartNo})</span>
                </div>

                <div class="row">
                  <span class="Span-b"> <span class="PCenter">TrackNo: </span> </span>
                  <span class="Span-c">  @Html.TextBoxFor(Function(m) m.TrackNo, New With {.Value = Read.TrackNo}) </span>
                </div>
                
                <div class="row">
                    <span class="Span-b"><span class="PCenter">Comentarios:</span></span>
                    <span class="Span-e">@Html.TextAreaFor(Function(m) m.Comentario, New With {.cols = 45, .rows = 3})</span>
                </div>

                <div class ="row">&nbsp;</div>
                <div class="row">
                    <span class="Span-c">&nbsp;</span>
                    <span class="Span-a">&nbsp;</span>
                    <span class="Span-a"><input type="submit" value="Enviar" class="Button" /></span>
                </div>
                <div class ="row">&nbsp;</div>
            </div>
        End Using

        </div>
    </div>


<!-- Aquí va la información de la orden -->
<div id="main-ContDerecha" class="bg-fondoborder">
    <div class="form-row">
        <span class="Span-e"><strong>Orden: </strong> @Read.OrderID </span>
    </div>
    <div class="form-row">
        <span class="Span-e"><strong>Número de Parte: </strong> @Read.PartNo </span>
    </div>
    <div class="form-row">
        <span class="Span-e"><strong>Descripción: </strong> @Read.Description </span>
    </div>
    <div class="form-row">&nbsp; </div>

    <div class="form-row">
        <span class="Span-e"><strong>Proveedor: </strong> @Read.Proveedor </span>
    </div>
    <div class="form-row">
        <span class="Span-e"><strong>Comentario: </strong> @Read.Comentario </span>
    </div>
    <div class="form-row">&nbsp; </div>
    <div class="form-row">&nbsp; </div>

    <div class="form-row">
        <span class="Span-e"><strong>Guía: </strong> @Read.TrackNo </span>
    </div>
    <div class="form-row">
        <span class="Span-e"><strong>ETA de la Parte: </strong> @Read.LeadTime </span>
    </div>
    <div class="form-row">
        <span class="Span-e"><strong>Fecha de Compra </strong> @Read.CostDate </span>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $("#Comentario").html("@User.Identity.Name : Recibo de la parte... ");
    });
</script>