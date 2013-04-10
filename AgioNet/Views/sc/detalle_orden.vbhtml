@ModelType AgioNet.CreateOrderModel 
    
@Code
    ViewData("Title") = "Servicio a Clientes"
    Session("Section") = "sc"
    
    Dim OrderID As String = TempData("OrderID")
    Dim Model As AgioNet.CreateOrderModel = TempData("Model")
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Crear una nueva orden</h2>

 
@Using Html.BeginForm()
    @<div id="Formulario">
        <h3>Información del Cliente</h3>
        <div class="row">
            <span class="Span-b"><span class="pcenter">OrderID</span></span>
            <span class="Span-c">@Html.TextBox("OrderID", New With {.Value = OrderID, .Disabled = True})</span>
            
            <span class="Span-b"><span class="pcenter">Tipo de cliente</span></span>
            <span class="Span-c">
                 @Html.TextBoxFor(Function(m) m.CustomerType, New With {.Value = Model.CustomerType, .Disabled = True})
            </span>
            
        </div>
        
        <div class="row">
            <span class="Span-b"><span class="pcenter">Nombre</span></span>
            <span class="Span-k">
                @Html.TextBoxFor(Function(m) m.CustomerName, New With {.Value = Model.CustomerName, .Disabled = True})
            </span>
        </div>
         <div class="row">
            <span class="Span-b"><span class="pcenter">Razón Social</span></span>
            <span class="Span-k">
                @Html.TextBoxFor(Function(m) m.RazonSocial, New With {.Value = Model.RazonSocial, .Disabled = True})
            </span>
        </div>
        <span class="row">
            <span class="Span-b"><span class="pcenter">RFC</span></span>
            <span class="Span-c"> 
                @Html.TextBoxFor(Function(m) m.RFC, New With {.Value = Model.RFC, .Disabled = True})
            </span>

            <span class="Span-b"><span class="pcenter">Email</span></span>
            <span class="Span-c"> 
                @Html.TextBoxFor(Function(m) m.Email, New With {.Value = Model.Email, .Disabled = True})
            </span>
        </span>

        <span class="row">
            <span class="Span-b"><span class="pcenter">Calle</span></span>
            <span class="Span-c"> 
                @Html.TextBoxFor(Function(m) m.Address, New With {.Value = Model.Address, .Disabled = True})
            </span>
            <span class="Span-a"><span class="pcenter">No. Exterior</span></span>
            <span class="Span-a"> 
                @Html.TextBoxFor(Function(m) m.ExternalNumber, New With {.Value = Model.ExternalNumber, .Disabled = True})
            </span>
            <span class="Span-a"><span class="pcenter">No. Interior</span></span>
            <span class="Span-a"> 
                @Html.TextBoxFor(Function(m) m.InternalNumber, New With {.Value = Model.InternalNumber, .Disabled = True})
            </span>
        </span>

        <span class="row">
            <span class="Span-b"><span class="pcenter">Colonia</span></span>
            <span class="Span-c"> 
                @Html.TextBoxFor(Function(m) m.Address2, New With {.Value = Model.Address2, .Disabled = True})
            </span>
            <span class="Span-a"><span class="pcenter">Ciudad</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.City, New With {.Value = Model.City, .Disabled = True})
            </span>
            <span class ="Span-a"><span class="pcenter">Estado</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.State, New With {.Value = Model.State, .Disabled = True})
            </span>
        </span>

        <span class="row">
            <span class="Span-b"><span class="pcenter">País</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.Country, New With {.Value = Model.Country, .Disabled = True})
            </span>
            <span class="Span-b"><span class="pcenter">Código Postal</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.ZipCode, New With {.Value = Model.ZipCode, .Disabled = True})
            </span>
            <span class="Span-b"><span class="pcenter">Horario Rec.</span></span>
            <span class="Span-b">
                @Html.TextBoxFor(Function(m) m.DeliveryTime, New With {.Value = Model.DeliveryTime, .Disabled = True})
            </span>
        </span>

        <span class="row">
            <span class="Span-b"><span class="pcenter">Tel Casa</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.Telephone, New With {.Value = Model.Telephone, .Disabled = True})
            </span>
            <span class="Span-b"><span class="pcenter">Tel Trabajo</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.Telephone2, New With {.Value = Model.Telephone2, .Disabled = True})
            </span>
            <span class="Span-b"><span class="pcenter">Celular</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.Telephone3, New With {.Value = Model.Telephone3, .Disabled = True})
            </span>
        </span>

        <!-- INFORMACION DEL PRODUCTO -->  
        <h3>Información del Producto</h3>

        <span class="row">
            <span class="Span-b"><span class="pcenter">Clase de Producto</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.ProductClass, New With {.Value = Model.ProductClass, .Disabled = True})
            </span>
            <span class="Span-b"><span class="pcenter">Tipo de Producto</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.ProductType, New With {.Value = Model.ProductType, .Disabled = True})
            </span>
            <span class="Span-b"><span class="pcenter">Marca Producto</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.ProductTrademark, New With {.Value = Model.ProductTrademark, .Disabled = True})
            </span>
        </span>

         <span class="row">
            <span class="Span-b"><span class="pcenter">Modelo</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.ProductModel, New With {.Value = Model.ProductModel, .Disabled = True})
            </span>
            <span class="Span-b"><span class="pcenter">Descripción</span></span>
            <span class="Span-d"> 
                @Html.TextBoxFor(Function(m) m.productDescription, New With {.Value = Model.productDescription, .Disabled = True})
            </span>
        </span>

        <span class="row">
            <span class="Span-b"><span class="pcenter">Número de Parte</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.PartNumber, New With {.Value = Model.PartNumber, .Disabled = True})
            </span>
            <span class="Span-b"><span class="pcenter">No. Serie</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.SerialNumber, New With {.Value = Model.SerialNumber, .Disabled = True})
            </span>
        </span>

        <!-- INFORMACION SOBRE LA FALLA -->
        <h3>Información de la Falla</h3>

        <span class="row">
            <span class="Span-b"><span class="pcenter">Clase de Servicio</span></span>
            <span class="Span-c"> 
                 @Html.TextBoxFor(Function(m) m.ServiceType, New With {.Value = Model.ServiceType, .Disabled = True})
            </span>

            <span class="Span-b"><span class="pcenter">Otra clase</span></span>
            <span class="Span-c"> 
                @Html.TextBox("OtherServiceType")
            </span>
        </span>

        <span class="row">
            <span class="Span-c"><span class="pcenter">Tipo de falla presentada: </span></span>
            <span class="Span-j">
                @Html.TextBoxFor(Function(m) m.FailureType, New With {.Value = Model.FailureType, .Disabled = True})
            </span>
        </span>

        <span class="row">
            <span class="Span-c"><span class="pcenter">Comentarios: </span></span>
            <span class="Span-j">
                @Html.TextBoxFor(Function(m) m.Comment, New With {.Value = Model.Comment, .Disabled = True})
            </span>
        </span>
        <span class="row">
            <span class="Span-b"><span class="pcenter">Incluir Flete</span></span>
            <span class="Span-b">
                @Html.TextBoxFor(Function(m) m.Delivery, New With {.Value = Model.Delivery, .Disabled = True})
            </span>
            <span class="Span-e">
                <input type="submit" value="Regresar" class="Button" />
            </span>
        </span>   

     </div>
End Using
