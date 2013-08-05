@ModelType AgioNet.CotizarClienteModel

@Code
    ViewData("Title") = "Servicio a clientes - Costeo"
    Session("Section") = "sc"
    
    Dim Read() As AgioNet.SubOrderListModel = TempData("Model")
    Dim eModel As AgioNet.extDatosCotizacion = TempData("eModel")
    
    Dim grid As WebGrid = New WebGrid(Read)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/diag.css")' rel="stylesheet" type="text/css" />
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
        $("#iva").numeric({ prefix: '$ ', cents: true });
        $("#total").numeric({ prefix: '$ ', cents: true });

        $("#LeadTime").datepicker({
            changeMonth:true,
            changeYear:true,
            dateFormat: "dd/mm/yy",
            firstDay:1
        });
        $("#LeadTime").datepicker($.datepicker.regional['es']);

        $("#btCalcular").click(function () {
            var Costs = []; var Percs = []; var Uti = [];
            var cont = 0; var Util = 0; var costo = 0.0; var putilidad = 0.0; var sumatoria = 0.0;
            var precio; var ManoObra; var Viaje; var subtotal; var iva; var total;

            $(".Costo").each(function () {
                //Costs[cont] = $(this).html();
                costo = $(this).text().replace('$', '');
                costo = costo.replace(',','');
                //alert(costo);
                Costs[cont] = costo;
                cont++;
                Counter++;
            });

            costo = 0.0;
            cont = 0;
            $(".cpUtil").each(function () {
                if ($(this).val() == '') {
                    Percs[cont] = 1;
                } else {
                    Percs[cont] = $(this).val();
                }
                cont++;
            });

            //Calcular utilidad neta
            cont = 0;
            $(".cUtil").each(function () {
                costo = Costs[cont];
                //putilidad = (Percs[cont] / 100);
                //Util = parseFloat(costo) * parseFloat(putilidad);
                Util = (costo * 100 / (100 -Percs[cont]) ) - costo;
                $(this).val(Util.toFixed(2))
                cont++;
            });

            cont = 0;
            $(".precio").each(function () {
                costo = Costs[cont];
                //putilidad = (Percs[cont] / 100);
                //Util = parseFloat(costo) * parseFloat(putilidad);
                Util = (costo * 100 / (100 - Percs[cont])) - costo;
                precio = Math.ceil(parseFloat(costo) + Util);
                sumatoria = sumatoria + precio;
                $(this).val(precio)
                cont++;
            });

            //Terminar los cálculos
            ManoObra = $("#ManoObra").val().replace('$', '');
            ManoObra = ManoObra.replace(',', '');
            Viaje = $("#Viaje").val().replace('$', '');
            Viaje = Viaje.replace(',', '');

            subtotal = parseFloat(ManoObra) + parseFloat(Viaje) + parseFloat(sumatoria);
            $("#SubTotal").val(subtotal);

            iva = subtotal * 0.16;
            $("#IVA").val(iva);
            
            total = subtotal + iva;
            $("#Total").val(total);

            //Aplicar formato moneda
            $(".Costo").numeric({ prefix: '$ ', cents: true });
            $(".cUtil").numeric({ prefix: '$ ', cents: true });
            $(".precio").numeric({ prefix: '$ ', cents: true });
            $("#ManoObra").numeric({ prefix: '$ ', cents: true });
            $("#Viaje").numeric({ prefix: '$ ', cents: true });
            $("#SubTotal").numeric({ prefix: '$ ', cents: true });
            $("#IVA").numeric({ prefix: '$ ', cents: true });
            $("#Total").numeric({ prefix: '$ ', cents: true });
        });

        /*
        $("#Formularioo").submit(function (e) {
           // return e.preventDefault();
            alert("Pasa por aqui");
            for (i = 0; i < Counter; i++) {
                Util = Math.ceil((Costs[i] * 100 / (100 -Percs[i]) ) - Costs[i]);
                $.ajax({
                    url: 'sc/saveCostPart',
                    type: 'POST',
                    data: "costo=" + Costs[i] + "&pUtilidad=" + Percs[i] + "&Utilidad=" + Util,
                    datatype: "text",
                    success: function (obj) {
                        alert("Funciona");
                    }
                });
            }
        });*/
       
    });
</script>

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Ordenes listas para cotizar a cliente</h2>
<div id="Formulario">
@Using Html.BeginForm("cotizarcliente", "sc", FormMethod.Post, New With {.id = "Formularioo"})
    
@grid.GetHtml(columns:=grid.Columns( _
    grid.Column("OrderID", "Orden", format:=Function(item) Html.TextBoxFor(Function(m) m.SubOrder, New With {.Value = item.OrderID})), _
    grid.Column("PartNumber", "Numero de parte"), _
    grid.Column("Description", "Descripción"), _
    grid.Column("Commodity", "Comodity"), _
    grid.Column("SerialNumber", "Número de serie"), _
    grid.Column("Costo", "Costo", style:="Costo"), _
    grid.Column("LeadTime", "Lead Time"), _
    grid.Column("Failure", "Falla"), _
    grid.Column("Solution", "Solución"), _
    grid.Column("Comentario", "Comentario Compras"), _
    grid.Column("TipoReparacion", "Tipo Reparación"), _
    grid.Column("PUtilidad", "%Utilidad", format:=Function(item) Html.TextBoxFor(Function(m) m.SubUtilidad, New With {.class = "cpUtil", .size = 5, .Value = 0})), _
    grid.Column("UtilidadN", "Utilidad Neta", format:=Function(item) Html.TextBox("UtilidadNeta", 0, New With {.class = "cUtil", .size = 5})), _
    grid.Column("Precio", "Precio", format:=Function(item) Html.TextBox("precio", 0, New With {.class = "precio", .size = 5})), _
    grid.Column("", "", format:=Function(item) Html.HiddenFor(Function(m) m.SubCosto, New With {.Value = item.Costo})) _
))

@<div id="main-ContIzquierda">    
    <div id="Formulario6">
        <div class="row">
            <span class="Span-a"><span class="PCenter">Caso: </span></span>
            <span class="Span-c">
                @Html.TextBoxFor(Function(m) m.OrderID, New With {.Value = tempdata("OrderID") })
            </span>
        </div>
        <div class="row">
            <span class="Span-a"> <span class="PCenter">Mano de Obra: </span> </span>
            <span class="Span-c"> 
                @Html.TextBoxfor(Function(m) m.ManoObra, New With { .Value = "358"})
            </span>
        </div>

                
        <div class="row">
            <span class="Span-a"><span class="PCenter">Flete</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.Viaje, New With {.Value = "500" }) </span>
            <span class="Span-a"><span class="Pcenter">Tipo Flete</span></span>
            <span class="Span-c">@Html.TextBox("Delivery", eModel.Delivery)</span>
        </div>
        <div class="row">
            <span class="Span-a"><span class="PCenter">Sub Total</span></span>
            <span class="Span-c"> @Html.TextBoxFor(Function(m) m.SubTotal) </span>
        </div>
        <div class="row">
            <span class="Span-a"><span class="PCenter">IVA</span></span>
            <span class="Span-c">@Html.TextBoxFor(Function(m) m.IVA)</span>
        </div>
        <div class="row">
            <span class="Span-a"><span class="PCenter">Total</span></span>
            <span class="Span-c">@Html.TextBoxfor(Function(m) m.Total)</span>
        </div>
        <!-- Upd 2013.03.19 CarlosB Agregar LeadTime-->
        <div class="row">
            <span class="Span-a"><span class="PCenter">Lead Time</span></span>
            <span class="Span-c">@Html.TextBoxfor(Function(m) m.LeadTime)  </span>
            <span class="Span-a">(DD/MM/AAAA)</span>
        </div>
        <!-- Upd 2013.04.16 CarlosB Agregar a)Falla reportada, b) Retroalimentación, c) Resumen de la falla				
            d) Despues de lead time, pero antes del tiem 2. agregar la fecha original de ingreso del equipo				
        -->
                
        <div class="row">
            <span class="Span-b">&nbsp;</span>
            <span class="Span-a"><input type="button" value="Calcular" class="Button" id="btCalcular" /></span>
            <span class="Span-a"><input type="submit" value="Guardar" class="Button" /></span>
        </div>
        <div class ="row">&nbsp;</div>
    </div>
</div>    
End Using
</div>


<!-- Aquí va la información de la orden -->
<div id="main-ContDerecha" class="bg-fondoborder">
    <div class="form-row">
        <span class="Span-e"><strong>Recibida El:</strong> @eModel.FechaIngreso </span>
    </div>

    <div class="form-row">
        <span class="Span-e"><strong>Tipo de producto: </strong> @eModel.ProductType </span>
    </div>

    <div class="form-row">
        <span class="Span-e"><strong>Falla Reportada: </strong> @eModel.Failure </span>
    </div>
    <div class="form-row"> &nbsp; </div>
    <div class="form-row"> &nbsp; </div>
    <div class="form-row"> &nbsp; </div>

    <div class="form-row">
        <span class="Span-e"><strong>Retro-Alimentación</strong> @eModel.Retro </span>
    </div>
    <div class="form-row"> &nbsp; </div>

    <div class="form-row">
        <span class="Span-e"><strong>Comentarios del Técnico:</strong> @eModel.Resumen </span>
    </div>
    <div class="form-row"> &nbsp; </div>
        <div class="form-row"> &nbsp; </div>

    <div class="form-row">
        <span class="Span-e"><strong>Comentarios Cliente:</strong> @eModel.ComentarioCliente </span>
    </div>
    <div class="form-row"> &nbsp; </div>
    <div class="form-row"> &nbsp; </div>
    <div class="form-row"> 
        <span class="Span-e"><strong>Número de reingresos: </strong> @eModel.Rerepair</span>
    </div>
</div>