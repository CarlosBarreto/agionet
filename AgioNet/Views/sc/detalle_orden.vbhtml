@ModelType AgioNet.CreateAssurantOrderModel 
    
@Code
    ViewData("Title") = "Servicio a Clientes"
    Session("Section") = "sc"
    
    Dim OrderID As String = TempData("OrderID")
    Dim Model As AgioNet.CreateAssurantOrderModel = TempData("Model")
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- UPD 2013.06.17 - CarlosB - Inicia maquetado Reporte Global - Begin -->
<div id="rp_main">
    <nav id="rp_main-nav">
        <ul>
            <li><a href="#">Inicio</a></li>
            <li><a href="#Cliente">Cliente</a></li>
            <li><a href="#Recibo">Recibo</a></li>
            <li><a href="#Ingreso">Ingreso</a></li>
            <li><a href="#Diagnostico">Diagnostico</a></li>
            <li><a href="#Costeo">Costeo Partes</a></li>
            <li><a href="#">Compras</a></li>
            <li><a href="#">Reparación</a></li>
            <li><a href="#">Calidad</a></li>
            <li><a href="#">Embarque</a></li>
            <li><a href="#">Facturación</a></li>
        </ul>
    </nav>

    <!-- Cuerpo principal del reporte -->
    <div id="rp_left">
        <h2 class="title">Detalle de la orden [ @OrderID ]</h2>
        <!-- Información del cliente -->
        <section id="Cliente">    
            <div class="rp_left-contenedor">
                <span class="title">Información del Cliente</span>
                <span class="form-row">&nbsp;</span>
                <div class="form-row">
                    <span class="Span-b"><strong>Orden</strong></span>
                    <span class="Span-b">@OrderID</span>
                    <span class="Span-b"><strong>Tipo de Cliente</strong></span>
                    <span class="Span-b">@Model.CustomerType</span>
                    <span class="Span-c"><strong>Notificación de Servicio</strong></span>
                    <span class="Span-b">@Model.NotificacionServicio</span>
                </div>

                <div class="form-row">
                    <span class="Span-b"><strong>Nombre</strong></span>
                    <span class="Span-e">@Model.CustomerName</span>
                </div>
                
                <div class="form-row">
                    <span class="Span-b"><strong>Razón Social</strong> </span>
                    <span class="Span-d">@Model.RazonSocial</span>
                    <span class="Span-a"><strong>RFC</strong></span>
                    <span class="Span-c">@Model.RFC</span>

                </div>
                 
                <div class="form-row">
                    <span class="Span-b"><strong>Email</strong></span>
                    <span class="Span-e">@Model.Email</span>
                </div>

                <div class="form-row">
                    <span class="Span-b"><strong>Dirección</strong></span>
                    <span class="Span-j">
                        @code
                            Dim strResp As String
                            strResp = Model.Address & " No " & Model.ExternalNumber
                            If Model.InternalNumber <> "" And Model.InternalNumber <> "-" And Model.InternalNumber <> "--" Then strResp = strResp & " Int " & Model.InternalNumber
                        End Code
                            @strResp
                    </span>
                </div>

                <div class="form-row">
                    <span class="Span-a"><strong>Colonia</strong>   </span>
                    <span class="Span-h">@Model.Address2</span>
                    <span class="Span-g"><strong>Ciudad</strong></span>
                    <span class="Span-h">@Model.City</span>
                    <span class="Span-a"><strong>Estado</strong></span>
                    <span class="Span-h">@Model.State </span>
                </div>
       
                <div class="form-row">
                    <span class="Span-a"><strong>país</strong></span>
                    <span class="Span-h">@Model.Country</span>
                    <span class="Span-g"><strong>Código Postal</strong></span>
                    <span class="Span-h">@Model.ZipCode</span>
                    <span class="Span-a"><strong>Horario</strong></span>
                    <span class="Span-h">@Model.DeliveryTime</span>
                </div>

                <div class="form-row">
                    <span class="Span-a"><strong>Tel Casa</strong></span>
                    <span class="Span-h">@Model.Telephone</span>
                    <span class="Span-g"><strong>Tel Trabajo</strong></span>
                    <span class="Span-h">@Model.Telephone2</span>
                    <span class="Span-a"><strong>Celular</strong></span>
                    <span class="Span-h">@Model.Telephone3</span>
                </div>
        </div>

            <div class="form-row">&nbsp;</div>
            <div class="rp_left-contenedor">
                <span class="title">Información del Producto</span>
                <span class="form-row">&nbsp;</span>

                <div class="form-row">
                    <span class="Span-b"><strong>Clase Producto</strong></span>
                    <span class="Span-c">@Model.ProductClass</span>
                    <span class="Span-b"><strong>Tipo Producto</strong></span>
                    <span class="Span-c">@Model.ProductType</span>
                </div>

                <div class="form-row">
                    <span class="Span-b"><strong>Marca</strong></span>
                    <span class="Span-c">@Model.ProductTrademark</span>
                    <span class="Span-b"><strong>Modelo</strong></span>
                    <span class="Span-c">@Model.ProductModel</span>
                </div>

                <div class="form-row">
                    <span class="Span-b"><strong>Descripción</strong></span>
                    <span class="Span-j">@Model.productDescription</span>
                </div>

                <div class="form-row">
                    <span class="Span-b"><strong>Número de Parte</strong></span>
                    <span class="Span-c">@Model.PartNumber</span>
                    <span class="Span-b"><strong>Número de Serie</strong></span>
                    <span class="Span-c">@Model.SerialNumber</span>
                </div>
            </div>

            <div class="form-row">&nbsp;</div>
            <div class="rp_left-contenedor">
                <span class="title">Información de la Falla</span>
                <span class="form-row">&nbsp;</span>

                <div class="form-row">
                    <span class="Span-b"><strong>Clase de Servicio</strong></span>
                    <span class="Span-c">@Model.ServiceType</span>
                    <span class="Span-b"><strong>Otra Clase</strong></span>
                    <span class="Span-c"></span>
                </div>

                <div class="form-row">&nbsp; </div>
                <strong>Falla Presentada</strong> @Model.FailureType <br /><br />
                <strong>Comentario</strong> @Model.Comment <br /><br />
        
                <div class="form-row">
                    <span class="Span-b"><strong>Incluir Flete</strong></span>
                    <span class="Span-c">@Model.Delivery</span>
                </div>
            </div>
        </section>

        <span class="form-row"><span class="separador">&nbsp;</span></span>
        <span class="form-row">&nbsp;</span>

        <section id="Recibo">
             <div class="rp_left-contenedor" id="rp_recibo">
                <img src="../../Content/images/ajax-loader.gif" />
            </div>
        </section>

        <span class="form-row"><span class="separador">&nbsp;</span></span>
        <span class="form-row">&nbsp;</span>
        <section id="Ingreso">
            <div id="rp_ingreso">
                <img src="../../Content/images/ajax-loader.gif" />
            </div>
        </section>

        <span class="form-row"><span class="separador">&nbsp;</span></span>
        <span class="form-row">&nbsp;</span>
        <section id="Diagnostico">
            <div id="rp_diagnostic">
                <img src="../../Content/images/ajax-loader.gif" />
            </div>
        </section>

        <span class="form-row"><span class="separador">&nbsp;</span></span>
        <span class="form-row">&nbsp;</span>
        <section id="Costeo">
            <div id="rp_costpart">
                <img src="../../Content/images/ajax-loader.gif" />
            </div>
        </section>

        <!-- 2013.07.11 -- CarlosB -->
        <!-- Compras -->
        <span class="form-row"><span class="separador">&nbsp;</span></span>
        <span class="form-row">&nbsp;</span>

        <section id="Compras">
             <div id="rp_compras">
                <img src="../../Content/images/ajax-loader.gif" />
            </div>
        </section>
    </div>

    <!-- Resumen del reporte -->
    <div id="rp_right">
        <img src="../../Content/images/ajax-loader.gif" />
    </div>

</div>

<!--- scripts -->
<script src='@Url.Content("~/Scripts/jquery.agiotech.js")' type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            data: "{'OrderID':" + "'@OrderID'}",
            dataType: 'html',
            url: '@Url.Action("min_info", "rp", New With {.OrderID = OrderID })',
            success: function (result) {
                //(alert('Success');
                $("#rp_right").html(result);
            },
            error: function (error) {
                alert('fail');
            }
        });

        // recibo
        $.ajax({
            type: 'GET',
            data: "{'OrderID':" + "'@OrderID'}",
            dataType: 'html',
            url: '@Url.Action("rp_recibo", "rp", New With {.OrderID = OrderID})',
            success: function (result) {
                //(alert('Success');
                $("#rp_recibo").html(result);
            },
            error: function (error) {
                alert('fail');
            }
        });

        // ingreso
        $.ajax({
            type: 'GET',
            data: "{'OrderID':" + "'@OrderID'}",
            dataType: 'html',
            url: '@Url.Action("rp_ingreso", "rp", New With {.OrderID = OrderID})',
            success: function (result) {
                //(alert('Success');
                $("#rp_ingreso").html(result);
            },
            error: function (error) {
                alert('fail');
            }
        });

        // Diagnostico
        $.ajax({
            type: 'GET',
            data: "{'OrderID':" + "'@OrderID'}",
            dataType: 'html',
            url: '@Url.Action("rp_diagnostic", "rp", New With {.OrderID = OrderID})',
            success: function (result) {
                //(alert('Success');
                $("#rp_diagnostic").html(result);
            },
            error: function (error) {
                alert('fail');
            }
        });

        // costeo
        $.ajax({
            type: 'GET',
            data: "{'OrderID':" + "'@OrderID'}",
            dataType: 'html',
            url: '@Url.Action("rp_costpart", "rp", New With {.OrderID = OrderID})',
            success: function (result) {
                //(alert('Success');
                $("#rp_costpart").html(result);
                // Agregar los formatos de moneda
                $(".moneda").numeric({ prefix: '$ ', cents: true });
            },
            error: function (error) {
                alert('fail');
            }
        });

        // Compras
        $.ajax({
            type: 'GET',
            data: "{'OrderID':" + "'@OrderID'}",
            dataType: 'html',
            url: '@Url.Action("rp_compras", "rp", New With {.OrderID = OrderID})',
            success: function (result) {
                //(alert('Success');
                $("#rp_compras").html(result);
                // Agregar los formatos de moneda
                $(".moneda").numeric({ prefix: '$ ', cents: true });
            },
            error: function (error) {
                alert('fail');
            }
        });

        // Agregar los formatos de moneda
        $(".moneda").numeric({ prefix: '$ ', cents: true });

    });
</script>