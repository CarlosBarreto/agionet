@ModelType AgioNet.SubOrderListModel

@Code
    ViewData("Title") = "Servicio a clientes - Costeo"
    Session("Section") = "sc"
    
    Dim Read() As AgioNet.SubOrderListModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read)
End Code
<script src='@Url.Content("~/Scripts/jquery.agiotech.js")' type="text/javascript"></script>
<script type="text/javascript"> 
    $(document).ready(function () {
        $(".Costo").numeric({ prefix: '$ ', cents: true });
        $(".cUtil").numeric({ prefix: '$ ', cents: true });
        $(".precio").numeric({ prefix: '$ ', cents: true });
        $("#ManoObra").numeric({ prefix: '$ ', cents: true });
        $("#Viaje").numeric({ prefix: '$ ', cents: true });
        $("#SubTotal").numeric({ prefix: '$ ', cents: true });
        $("#iva").numeric({ prefix: '$ ', cents: true });
        $("#total").numeric({ prefix: '$ ', cents: true });

        $("#LeadTime").datepicker({ dateFormat: "dd/mm/yy" });

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

            cont = 0;
            $(".cUtil").each(function () {
                costo = Costs[cont];
                putilidad = (Percs[cont] / 100);
                Util = parseFloat(costo) * parseFloat(putilidad);
                sumatoria = sumatoria + (parseFloat(costo) + Util);
                $(this).val(Util.toFixed(2))
                cont++;
            });

            cont = 0;
            $(".precio").each(function () {
                costo = Costs[cont];
                putilidad = (Percs[cont] / 100);
                Util = parseFloat(costo) * parseFloat(putilidad);
                precio = parseFloat(costo) + Util;
                $(this).val(precio)
                cont++;
            });

            //Terminar los cálculos
            ManoObra = $("#ManoObra").val().replace('$', '');
            ManoObra = ManoObra.replace(',', '');
            Viaje = $("#Viaje").val().replace('$', '');
            Viaje = Viaje.replace(',', '');

            subtotal = Math.ceil(parseFloat(ManoObra) + parseFloat(Viaje) + parseFloat(sumatoria));
            $("#SubTotal").val(subtotal);

            iva = subtotal * 0.16;
            $("#iva").val(iva);
            
            total = subtotal + iva;
            $("#total").val(total);

            //Aplicar formato moneda
            $(".Costo").numeric({ prefix: '$ ', cents: true });
            $(".cUtil").numeric({ prefix: '$ ', cents: true });
            $(".precio").numeric({ prefix: '$ ', cents: true });
            $("#ManoObra").numeric({ prefix: '$ ', cents: true });
            $("#Viaje").numeric({ prefix: '$ ', cents: true });
            $("#SubTotal").numeric({ prefix: '$ ', cents: true });
            $("#iva").numeric({ prefix: '$ ', cents: true });
            $("#total").numeric({ prefix: '$ ', cents: true });
        });
    });
</script>

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Ordenes listas para cotizar a cliente</h2>

@grid.GetHtml(columns:=grid.Columns( _
    grid.Column("OrderID", "Orden"), _
    grid.Column("PartNumber", "Numero de parte"), _
    grid.Column("Description", "Descripción"), _
    grid.Column("Commodity", "Comodity"), _
    grid.Column("SerialNumber", "Número de serie"), _
    grid.Column("Costo", "Costo", style:="Costo"), _
    grid.Column("LeadTime", "Lead Time"), _
    grid.Column("Failure", "Falla"), _
    grid.Column("Solution", "Solución"), _
    grid.Column("PUtilidad", "%Utilidad", format:=Function(item) Html.TextBox("Utilidad", 0, New With {.class = "cpUtil", .size = 5})), _
    grid.Column("UtilidadN", "Utilidad Neta", format:=Function(item) Html.TextBox("UtilidadNeta", 0, New With {.class = "cUtil", .size = 5})), _
    grid.Column("Precio", "Precio", format:=Function(item) Html.TextBox("precio", 0, New With {.class = "precio", .size = 5})) _
))

<div id="Formulario">
        @Using Html.BeginForm()
            @<div id="Formulario2">
                <div class="row">
                  <span class="Span-a"> <span class="PCenter">Mano de Obra: </span> </span>
                  <span class="Span-c"> @Html.TextBox("ManoObra", 500) </span>
                </div>

                <div class="row">
                    <span class="Span-a"><span class="PCenter">Viaje</span></span>
                    <span class="Span-c"> @Html.TextBox("Viaje", 500) </span>
                </div>
                <div class="row">
                    <span class="Span-a"><span class="PCenter">Sub Total</span></span>
                    <span class="Span-c"> @Html.TextBox("SubTotal") </span>
                </div>
                <div class="row">
                    <span class="Span-a"><span class="PCenter">IVA</span></span>
                    <span class="Span-e">@Html.TextBox("iva")</span>
                </div>
                <div class="row">
                    <span class="Span-a"><span class="PCenter">Total</span></span>
                    <span class="Span-e">@Html.TextBox("total")</span>
                </div>
                <!-- Upd 2013.03.19 CarlosB Agregar LeadTime-->
                <div class="row">
                    <span class="Span-a"><span class="PCenter">Lead Time</span></span>
                    <span class="Span-e">@Html.TextBox("LeadTime")</span>
                </div>
                <div class="row">
                    <span class="Span-c">&nbsp;</span>
                    <span class="Span-b">&nbsp;</span>
                    <span class="Span-a"><input type="button" value="Calcular" class="Button" id="btCalcular" /></span>
                    <span class="Span-a"><input type="submit" value="Guardar" class="Button" /></span>
                </div>
                <div class ="row">&nbsp;</div>
            </div>
        End Using

    </div>
