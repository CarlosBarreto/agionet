@ModelType AgioNet.ScanOrderModel

@Code
    ViewData("Title") = "Reparacion - Pendientes de Reparar"
    Session("Section") = "reparacion"
    
    Dim Read() As AgioNet.PendientesreparacionModel = TempData("Model")
    Dim mailInfo As AgioNet.mailing.DiagnosticMailInfo = TempData("mailInfo")
    Dim OrderID As String = TempData("OrderID")
    Dim grid As WebGrid = New WebGrid(Read)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/diag.css")' rel="stylesheet" type="text/css" />

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Órdenes Pendientes de Reparar</h2>
<div id="Formulario">
    @grid.GetHtml(columns:=grid.Columns( _
        grid.Column("OrderID", "Orden"), _
        grid.Column("Comments", "Comentario Ap"), _
        grid.Column("ApprovalDate", "Lead Time"), _
        grid.Column("PartNumber", "Número de Parte"), _
        grid.Column("ProductDescription", "Descripción"), _
        grid.Column("SerialNumber", "Número de Serie"), _
        grid.Column("Failure", "Falla"), _
        grid.Column("Solution", "Solucion"), _
        grid.Column("Source", "Tipo Reparación"), _
        grid.Column("Comment", "Comentarios"), _
grid.Column("Comentario", "Comentario Compras"), _
        grid.Column("RepairStatus", "Reparado"), _
        grid.Column(format:=Function(item) Html.ActionLink("Reparar", "ordenespendientes", "reparacion", New With {.OrderID = item.OrderID}, vbNull), style:="Link_") _
    ))


    <!-- Aquí se muestran los formularios -->
    <div id="main-ContIzquierda">
        <div id="Formulario6">
            @Using Html.BeginForm("cerrar_reparacion", "reparacion", "", FormMethod.Post)
                @<span class="row">@Html.HiddenFor(Function(m) m.OrderID, New With {.Value = OrderID})</span>    
                @<span class="row">
                    <span class="Span-b"><label>Comentarios: </label> </span>
                    <span class="Span-d">@Html.TextAreaFor(Function(m) m.Comment, New With {.rows = 4, .cols = 53})</span>
                    <span class="Span-a">&nbsp;</span>
                    <span class="Span-a"> <input type="submit" value="Cerrar Reparación" class="Button" /></span>
                </span>
            End Using
        </div>
    </div>
</div>
<!-- Aquí va la información de la orden -->
<div id="main-ContDerecha" class="bg-fondoborder">
    <div class="form-row">
        <span class="Span-e"><strong>MARCA: </strong>@mailInfo.Marca </span>
    </div>
    <div class="form-row">
        <span class="Span-e"><strong>MODELO: </strong>@mailInfo.Modelo</span>
    </div>
    <div class="form-row">
        <span class="Span-e"><strong>SKU: </strong>@mailInfo.SKUNO</span>
    </div>
    <div class="form-row">
        <span class="Span-e"><strong>DESCRIPCIÓN: </strong>@mailInfo.Descripcion</span>
    </div>
    <div class="form-row">&nbsp;</div>

    <div class="form-row">
        <span class="Span-e"><strong>No SERIE: </strong>@mailInfo.Serie</span>
    </div>

    <div class="form-row">
        <span class="Span-e"><strong>FALLA REPORTADA: </strong>@mailInfo.FallaReportada</span>
    </div>
    <div class="form-row">&nbsp;</div>

    <div class="form-row">
        <span class="Span-e"><strong>RETRO FALLA:</strong>@mailInfo.RetroAlimentacion</span>
    </div>
    <div class="form-row">&nbsp;</div>
    <div class="form-row">&nbsp;</div>
    
    <div class="form-row">
        <span class="Span-e"><strong>RETRO TECNICO:</strong>@mailInfo.ComentarioTecnico</span>
    </div>
    <div class="form-row">&nbsp;</div>
    <div class="form-row">&nbsp;</div>

    <div class="form-row">
        <span class="Span-e"><strong>RETRO A.C.: </strong>@mailInfo.Comentario</span>
    </div>
    <div class="form-row">&nbsp;</div>
    <div class="form-row">&nbsp;</div>

    <div class="form-row">
        <span class="Span-e"><strong>FECHA DE ENTREGA:</strong>@mailInfo.FechaCompromiso</span>
    </div>
    <div class="form-row">&nbsp;</div>
</div>

<script type="text/javascript">
    function clicked(value) {
        //alert("Otra cosa"+value);
        $("#OrderID").val(value);
        $(".Formulario").submit();
    }

</script>