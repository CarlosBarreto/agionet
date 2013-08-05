@ModelType AgioNet.facturarModel

@Code
    ViewData("Title") = "Facturacion - Facturar"
    Session("Section") = "facturacion"
    
    Dim Read() As AgioNet.PrecioFacturacionModel = TempData("Model")
    Dim eModel As AgioNet.DatosCotizacionExtModel = TempData("eModel")
    
    Dim grid As WebGrid = New WebGrid(Read, canPage:=False)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Proceso Facturación</h2>
<div id="Formulario">

@Using Html.BeginForm()
    
    @grid.GetHtml(columns:=grid.Columns( _
        grid.Column("OrderID", "Orden", format:=Function(item) Html.TextBoxFor(Function(m) m.SubOrder, New With {.size = 17, .Value = item.OrderID})), _
        grid.Column("PartNo", "Numero de parte", format:=Function(item) Html.TextBoxFor(Function(m) m.PartNo, New With {.size = 10, .Value = item.PartNo})), _
        grid.Column("Description", "Descripción"), _
        grid.Column("Precio", "Precio", format:=Function(item) Html.TextBoxFor(Function(m) m.Precio, New With {.class = "precio", .size = 5, .Value = item.Precio})), _
        grid.Column("Commodity", "Comodity"), _
        grid.Column("TipoReparacion", "Tipo Reparación") _
    ))

    @<div id="Formulario2">      
        <div class="row">&nbsp;</div>

        <div class="row-facturar">
            <span class="label"><span class="PCenter">Sub Total</span></span>
            <span class="data"> @Html.TextBoxFor(Function(m) m.SubTotal, New With {.Value = eModel.Subtotal}) </span>
        </div>
        <div class="row-facturar">
            <span class="label"><span class="PCenter">IVA</span></span>
            <span class="data"> @Html.TextBoxFor(Function(m) m.IVA, New With {.Value = eModel.IVA}) </span>
        </div>
        <div class="row-facturar">
            <span class="label"><span class="PCenter">Total</span></span>
            <span class="data"> @Html.TextBoxFor(Function(m) m.Total, New With {.Value = eModel.Total})</span>
        </div>
    </div>
    
    @<div id="Formulario3">
        <div class="row">&nbsp;</div>
        <!-- Upd 2013.03.19 CarlosB Agregar LeadTime-->
        <div class="row">
            <span class="label">Lead Time</span>
            <span class="data"> @eModel.leadTime
                <!-- Html.TextBox("LeadTime", eModel.leadTime)-->
            </span>
        </div>
        <!-- Upd 2013.04.16 CarlosB Agregar a)Falla reportada, b) Retroalimentación, c) Resumen de la falla			
            d) Despues de lead time, pero antes del tiem 2. agregar la fecha original de ingreso del equipo				
        -->
        <div class="row">
            <span class="label">Recibida El:</span>
            <span class="data"> @eModel.InDate
                <!-- Html.TextBox("Ingreso", eModel.InDate)-->
                </span>
        </div>
        <div class="row">
            <span class="label">Falla Reportada</span>
            <span class="data"> @eModel.Failure
                <!-- Html.TextBox("FallaReportada", eModel.Failure)-->
            </span>
        </div>

        <div class="row">
            <span class="label">Retro-Alimentación</span>
            <span class="data"> @eModel.Retro
                <!-- Html.TextBox("Retroalimentación", eModel.Retro)-->
            </span>
        </div>

        <div class="row">
            <span class="label">Resumen</span>
            <span class="data"> @eModel.Comments
                <!-- Html.TextBox("Resumen", eModel.Comments)-->
            </span>
        </div>

        <div class="row">
            <span class="label">Comentarios Cliente</span>
            <span class="data"> @eModel.Comment</span>
        </div>
    </div>
    @<div id="Formulario4">
        <!-- Using Html.BeginForm() -->
        <div class="row">
            <span class="label" style="color:red;">No. factura</span>
            <span class="data">@Html.TextBoxfor(Function(m) m.NoFactura)</span>
        </div>
        <div class="row">
            <span class="label">Comentario Cotizacion</span> 
            <div class="data">
                @Html.HiddenFor(Function(m) m.OrderID, New With {.Value = eModel.OrderID})
                @Html.TextAreaFor(Function(m) m.Comentario, New With {.rows = 3, .cols = 43})
            </div>
        </div>
        <div class ="row">&nbsp;</div>   
        <div class="row">
            <span class="Span-c">&nbsp;</span>
            <span class="Span-b">&nbsp;</span>
            <span class="Span-a"><input type="submit" value="Guardar" class="Button" /></span>
        </div>        
   </div>
End Using

</div>

<script src='@Url.Content("~/Scripts/jquery.agiotech.js")' type="text/javascript"></script>
<script type="text/javascript"> 
    var Counter = 0;

    $(document).ready(function () {
        //Colocar los signos de $ (Moneda)
        Currency();
        calculate(); //Calcular los valores iniciales

        $("input[type=text]").focus().select();
        $("input[type=text]").click(function () {
            $(this).select();
        });

        $(".precio").change(function () {
            calculate();
        });
    });

    function calculate() {
        var _SubTotal_ = 0;
        var _IVA_ = 0;
        var _Total_ = 0;

        var precio = 0;

        //Calcular _SubTotal_
        $(".precio").each(function () {
            precio = $(this).val();
            precio = precio.replace('$', '');
            precio = precio.replace(',', '');
            _SubTotal_ = _SubTotal_ + parseFloat(precio);
        });

        _SubTotal_ = _SubTotal_;

        //Calcular IVA
        _IVA_ = parseFloat(_SubTotal_ * 0.16);

        // Calcular Total
        _Total_ = parseFloat(_SubTotal_ + _IVA_);

        $("#SubTotal").val(_SubTotal_);
        $("#IVA").val(_IVA_);
        $("#Total").val(_Total_);

        Currency();
    }

    function Currency() {
        $(".Costo").numeric({ prefix: '$ ', cents: true });
        $(".cUtil").numeric({ prefix: '$ ', cents: true });
        $(".precio").numeric({ prefix: '$ ', cents: true });
        $("#ManoObra").numeric({ prefix: '$ ', cents: true });
        $("#Viaje").numeric({ prefix: '$ ', cents: true });
        $("#SubTotal").numeric({ prefix: '$ ', cents: true });
        $("#IVA").numeric({ prefix: '$ ', cents: true });
        $("#Total").numeric({ prefix: '$ ', cents: true });

        $("#LeadTime").datepicker({ dateFormat: "dd/mm/yy" });
    }
</script>
