@ModelType AgioNet.EnviarCotizacionModel

@Code
    ViewData("Title") = "Servicio a clientes - Costeo"
    Session("Section") = "sc"
    
    Dim Read() As AgioNet.PrecioCotizacionModel = TempData("Model")
    Dim eModel As AgioNet.DatosCotizacionExtModel = TempData("eModel")
    Dim DatosEquipo As AgioNet.mailing.DiagnosticMailInfo = TempData("DatosEquipo")
    
    Dim grid As WebGrid = New WebGrid(Read)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/diag.css")' rel="stylesheet" type="text/css" />


<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Proceso Enviar Cotización</h2>
<div id="Formulario">

@grid.GetHtml(columns:=grid.Columns( _
    grid.Column("OrderID", "Orden"), _
    grid.Column("PartNo", "Numero de parte"), _
    grid.Column("Description", "Descripción"), _
    grid.Column("Commodity", "Comodity"), _
    grid.Column("SerialNo", "Número de serie"), _
    grid.Column("LeadTime", "Lead Time"), _
    grid.Column("Falla", "Falla"), _
    grid.Column("Solucion", "Solución"), _
    grid.Column("TipoReparacion", "Tipo Reparación"), _
    grid.Column("Comentario", "Comentario Compras"), _
    grid.Column("Precio", "Precio", style:="precio") _
))

<!-- Aquí se muestran los formularios -->
<div id="main-ContIzquierda">
    <div id="Formulario6">
        <div class="row">&nbsp;</div>
        <div class="row">
            <span class="Span-b"><span class="PCenter">Caso: </span></span>
            <span class="Span-c"> @eModel.OrderID 
                <!--Html.TextBox("OrderID", eModel.OrderID)-->
            </span>
        </div>
        <div class="row">
            <span class="Span-b"> <span class="PCenter">Mano de Obra: </span> </span>
            <span class="Span-c"> <span id="ManoObra">@eModel.ManoObra </span>
                <!-- Html.TextBox("ManoObra", eModel.ManoObra)-->
            </span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="PCenter">Viaje</span></span>
            <span class="Span-c"><span id="Viaje">@eModel.Viaje</span>
                <!-- Html.textbox("Viaje", eModel.Viaje)-->
            </span>
            <span class="Span-b"><span class="PCenter">Tipo Flete</span></span>
            <span class="Span-c">@eModel.Delivery</span>
        </div>
        <div class="row">
            <span class="Span-b"><span class="PCenter">Sub Total</span></span>
            <span class="Span-c"> <span id="SubTotal">@eModel.Subtotal </span>
                <!-- Html.TextBox("SubTotal", eModel.Subtotal) -->
            </span>
        </div>
        <div class="row">
            <span class="Span-b"><span class="PCenter">IVA</span></span>
            <span class="Span-c"><span id="IVA"> @eModel.IVA</span>
                <!-- Html.TextBox("IVA", eModel.IVA)-->
            </span>
        </div>
        <div class="row">
            <span class="Span-b"><span class="PCenter">Total</span></span>
            <span class="Span-c"><span id="Total"> @eModel.Total</span>
                <!-- Html.TextBox("Total", eModel.Total)-->
            </span>
        </div>
        <!-- Upd 2013.03.19 CarlosB Agregar LeadTime-->
        <div class="row">
            <span class="Span-b"><span class="PCenter">Lead Time</span></span>
            <span class="Span-c"> @eModel.leadTime
                <!-- Html.TextBox("LeadTime", eModel.leadTime)-->
            </span>
        </div>
        <div class="row">&nbsp;</div>
        <!-- Upd 2013.04.16 CarlosB Agregar a)Falla reportada, b) Retroalimentación, c) Resumen de la falla				
            d) Despues de lead time, pero antes del tiem 2. agregar la fecha original de ingreso del equipo				
        -->
        <!--<div class="bg-fondoborder">-->
        @Using Html.BeginForm()
            @<div class="form-row">
                <span class="Span-c"><strong style="color:blue;">Comentario Cotizacion</strong></span>
            </div>
        
            @<div class="form-SPArea--dgl">
                @Html.TextAreaFor(Function(m) m.Comentario, New With {.rows = 3, .cols = 55})
                @Html.HiddenFor(Function(m) m.OrderID, New With {.Value = eModel.OrderID})
                </div>
            @<div class="row">
                    <span class="Span-c">&nbsp;</span>
                    <span class="Span-a"><input type="submit" value="Guardar" class="Button" /></span>
                </div>
            @<div class ="row">&nbsp;</div>
        End Using
        <!--</div> -->
     </div>

   </div>
</div>

<!-- Aquí va la información de la orden -->
<div id="main-ContDerecha" class="bg-fondoborder">
    <div class="form-row">
        <span class="Span-c"><strong>MARCA: </strong> @DatosEquipo.Marca </span>
    </div>
    <div class="form-row">
        <span class="Span-c"><strong>MODELO: </strong>  @DatosEquipo.Modelo </span>
    </div>
    <div class="form-row">
        <span class="Span-c"><strong>SKU: </strong>  @DatosEquipo.SKUNO </span>
    </div>
    <div class="form-row">
        <span class="Span-e"><strong>DESCRIPCIÓN: </strong>  @DatosEquipo.Descripcion </span>
    </div>
    <div class="form-row">&nbsp;</div>
    <div class="form-row">
        <span class="Span-e"><strong>No SERIE: </strong>  @DatosEquipo.Serie </span>
    </div>
    <!-- Datos antiguos -->
    <div id="form-row">
        <span class="Span-e"><strong>Recibida El:</strong> @eModel.InDate </span>
    </div>

    <div class="form-row">
        <span class="Span-e"><strong>Falla Reportada</strong> @eModel.Failure </span>
    </div>
    <div class="form-row">&nbsp;</div>

    <div class="form-row">
        <span class="Span-e"><strong>Retro-Alimentación</strong> @eModel.Retro </span>
    </div>
    <div class="form-row">&nbsp;</div>

    <div class="form-row">
        <span class="Span-e"><strong>Comentarios Cliente</strong> @eModel.Comment</span>
    </div>
    <div class="form-row">&nbsp;</div>

    <div class="form-row">
        <span class="Span-e"><strong>Comentarios del Técnico:</strong> @eModel.Comments </span>
    </div>
    <div class="form-row">&nbsp;</div>
    <div class="form-row">&nbsp;</div>
    <div class="form-row">&nbsp;</div>
    <div class="form-row">&nbsp;</div>
    <div class="form-row">&nbsp;&nbsp;</div>
</div>

<script src='@Url.Content("~/Scripts/jquery.agiotech.js")' type="text/javascript"></script>
<script type="text/javascript"> 
    var Counter = 0;

    $(document).ready(function () {
        $(".Costo").numeric({ prefix: '$ ', cents: true });
        $(".cUtil").numeric({ prefix: '$ ', cents: true });
        $(".precio").numeric({ prefix: '$ ', cents: true });
        $("#ManoObra").numeric({ prefix: '$ ', cents: true });
        $("#Viaje").numeric({ prefix: '$ ', cents: true });
        $("#SubTotal").numeric({ prefix: '$ ', cents: true });
        $("#IVA").numeric({ prefix: '$ ', cents: true });
        $("#Total").numeric({ prefix: '$ ', cents: true });

        $("#LeadTime").datepicker({ dateFormat: "dd/mm/yy" });
        $("#Comentario").focus();
    });
</script>
