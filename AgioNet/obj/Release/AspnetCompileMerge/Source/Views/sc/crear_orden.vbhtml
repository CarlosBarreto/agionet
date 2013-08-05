@ModelType AgioNet.CreateOrderModel 
    
@Code
    ViewData("Title") = "Servicio a Clientes"
    Session("Section") = "sc"
    
    'Opciones para el tipo de usuario
    Dim opt_UserType As New List(Of String)
    opt_UserType.Add("Seleccionar")
    opt_UserType.Add("Agiofix")
    opt_UserType.Add("TWG")
    opt_UserType.Add("ASS")
    
    'Opciones para el tipo de servicio
    Dim opt_servicetype As List(Of String) = New List(Of String)
    opt_servicetype.Add("Seleccionar")
    opt_servicetype.Add("Mantenimiento")
    opt_servicetype.Add("Reparación")
    opt_servicetype.Add("Otra")
    
    'Opciones clase de producto
    Dim opt_prodClass As List(Of String) = New List(Of String)
    opt_prodClass.Add("Seleccionar")
    opt_prodClass.Add("Equipo")
    opt_prodClass.Add("Componente")
    opt_prodClass.Add("Accesorio")
    
    'Opciones clase de producto
    Dim opt_CaseType As List(Of String) = New List(Of String)
    opt_CaseType.Add("Seleccionar")
    opt_CaseType.Add("UPS TWG")
    opt_CaseType.Add("Completo")
    opt_CaseType.Add("Solo Envío")
    opt_CaseType.Add("Solo Recolección")
    opt_CaseType.Add("NA")
    
End Code


<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Crear una nueva orden Assurant</h2>


@Using Html.BeginForm()
    @<div id="Formulario">
        <h3>Información del Cliente</h3>
        <div class="row">
            <span class="Span-b"><span class="pcenter">Tipo de cliente</span></span>
            <span class="Span-b">
                 @Html.DropDownListFor(Function(m) m.CustomerType, New SelectList(opt_UserType))
            </span>
            <div id="CustOrderNo" style="display:none;">
                <span class="Span-b"><span class="pcenter">Número de Orden</span></span>
                <span class="Span-d">@Html.TextBoxFor(Function(m) m.CustomerReference)</span>
            </div>
        </div>
        
        <div class="row">
            <span class="Span-b"><span class="pcenter">Nombre</span></span>
            <span class="Span-k">
                @Html.TextBoxFor(Function(m) m.CustomerName)
            </span>
        </div>
         <div class="row">
            <span class="Span-b"><span class="pcenter">Razón Social</span></span>
            <span class="Span-k">
                @Html.TextBoxFor(Function(m) m.RazonSocial)
            </span>
        </div>
        <span class="row">
            <span class="Span-b"><span class="pcenter">RFC</span></span>
            <span class="Span-c"> 
                @Html.TextBoxFor(Function(m) m.RFC)
            </span>

            <span class="Span-b"><span class="pcenter">Email</span></span>
            <span class="Span-c"> 
                @Html.TextBoxFor(Function(m) m.Email)
            </span>
        </span>

        <span class="row">
            <span class="Span-b"><span class="pcenter">Calle</span></span>
            <span class="Span-c"> 
                @Html.TextBoxFor(Function(m) m.Address)
            </span>
            <span class="Span-a"><span class="pcenter">No. Exterior</span></span>
            <span class="Span-a"> 
                @Html.TextBoxFor(Function(m) m.ExternalNumber)
            </span>
            <span class="Span-a"><span class="pcenter">No. Interior</span></span>
            <span class="Span-a"> 
                @Html.TextBoxFor(Function(m) m.InternalNumber)
            </span>
        </span>

        <span class="row">
            <span class="Span-b"><span class="pcenter">Colonia</span></span>
            <span class="Span-c"> 
                @Html.TextBoxFor(Function(m) m.Address2)
            </span>
            <span class="Span-a"><span class="pcenter">Ciudad</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.City)
            </span>
            <span class ="Span-a"><span class="pcenter">Estado</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.State)
            </span>
        </span>

        <span class="row">
            <span class="Span-b"><span class="pcenter">País</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.Country)
            </span>
            <span class="Span-b"><span class="pcenter">Código Postal</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.ZipCode)
            </span>
            <span class="Span-b"><span class="pcenter">Horario Rec.</span></span>
            <span class="Span-b">
                @Html.TextBoxFor(Function(m) m.DeliveryTime)
            </span>
        </span>

        <span class="row">
            <span class="Span-b"><span class="pcenter">Tel Casa</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.Telephone)
            </span>
            <span class="Span-b"><span class="pcenter">Tel Trabajo</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.Telephone2)
            </span>
            <span class="Span-b"><span class="pcenter">Celular</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.Telephone3)
            </span>
        </span>

        <!-- INFORMACION DEL PRODUCTO -->  
        <h3>Información del Producto</h3>

        <span class="row">
            <span class="Span-b"><span class="pcenter">Clase de Producto</span></span>
            <span class="Span-b"> 
                @Html.DropDownListFor(Function(m) m.ProductClass, New SelectList(opt_prodClass))
            </span>
            <span class="Span-b"><span class="pcenter">Tipo de Producto</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.ProductType, New With {.Value = "Ej. Laptop"})
            </span>
            <span class="Span-b"><span class="pcenter">Marca Producto</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.ProductTrademark, New With {.Value = "Ej. HP"})
            </span>
        </span>

         <span class="row">
            <span class="Span-b"><span class="pcenter">Modelo</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.ProductModel, New With {.Value = "Ej. Pavilion dv4"})
            </span>
            <span class="Span-b"><span class="pcenter">Descripción</span></span>
            <span class="Span-d"> 
                @Html.TextBoxFor(Function(m) m.productDescription, New With {.Value = "Ej. Notebook de entretenimiento HP Pavilion dv4-2014la "})
            </span>
        </span>

        <span class="row">
            <span class="Span-b"><span class="pcenter">Número de Parte</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.PartNumber, New With {.Value = "Ej. VS605LA"})
            </span>
            <span class="Span-b"><span class="pcenter">No. Serie</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.SerialNumber, New With {.Value = "Ej. CND9483P88"})
                @Html.HiddenFor(Function(m) m.Revision, New with {.Value = ""})
            </span>
        </span>

        <!-- INFORMACION SOBRE LA FALLA -->
        <h3>Información de la Falla</h3>

        <span class="row">
            <span class="Span-b"><span class="pcenter">Clase de Servicio</span></span>
            <span class="Span-c"> 
                 @Html.DropDownListFor(Function(m) m.ServiceType, New SelectList(opt_servicetype))
            </span>

            <span class="Span-b"><span class="pcenter">Otra clase</span></span>
            <span class="Span-c"> 
                @Html.TextBox("OtherServiceType")
            </span>
        </span>

        <span class="row">
            <span class="Span-c"><span class="pcenter">Tipo de falla presentada: </span></span>
            <span class="Span-j">
                @Html.TextBoxFor(Function(m) m.FailureType)
            </span>
        </span>

        <span class="row">
            <span class="Span-c"><span class="pcenter">Comentarios: </span></span>
            <span class="Span-j">
                @Html.TextBoxFor(Function(m) m.Comment)
            </span>
        </span>


        <span class="row">
            <span class="Span-b"><span class="pcenter">Incluir Flete</span></span>
            <span class="Span-b">
                @Html.DropDownListFor(Function(m) m.Delivery, New SelectList(opt_CaseType))
            </span>
            <span class="Span-e">
                <input type="submit" value="Registrar Orden" class="Button" />
            </span>
        </span>   
     </div>
End Using

<script type="text/javascript">


    $().ready(function() {    
        $("#CustomerType").change(function () {
            

        });
    });
</script>

<script type="text/javascript">
    var AgioOptions = ['Seleccionar', 'Agiotruck (360)', 'Mostrador', 'Paquetería'];
    var TWGOptions = ['Seleccionar', 'Mostrador', 'UPS - 360', 'UPS - Entrega', 'UPS - TWG', 'Agiotruck (360)'];
    var Option = $("#CustomerType").val();
    var $select = $("#Delivery")


    $(document).ready(function () {
        $("input[type=text]").focus(function () {
            this.select();
        });

        $("#CustomerType").change(function () {
            var MyVal = $("#CustomerType option:selected").text();

            if (MyVal != "EndUser" && MyVal != "Seleccionar") {
                $("#CustOrderNo").css("display", "block");
            }
            else {
                $("#CustOrderNo").css("display", "none");
            }

            /*
            //Cambiar flete
            if (MyVal == 'Seleccionar') {
                $select = $("#Delivery").empty();
                var OptEmpry = $('<option/>', { value: "Seleccionar" })
                    .text("Seleccionar")
                    .prop('selected', i == 0);

                OptEmpry.appendTo($select);

            }
            else if (MyVal == 'Agiofix') {
                $select = $("#Delivery").empty();
                for (var i = 0; i < AgioOptions.length; i++) {
                    var OptEmpry = $('<option/>', { value: AgioOptions[i] })
                   .text(AgioOptions[i])
                   .prop('selected', i == 0);

                    OptEmpry.appendTo($select);
                }
            }
            else  {
                $select = $("#Delivery").empty();
                for (var i = 0; i < TWGOptions.length; i++) {
                    var OptEmpry = $('<option/>', { value: TWGOptions[i] })
                   .text(TWGOptions[i])
                   .prop('selected', i == 0);

                    OptEmpry.appendTo($select);
                }
            } */
        });
    });
</script>