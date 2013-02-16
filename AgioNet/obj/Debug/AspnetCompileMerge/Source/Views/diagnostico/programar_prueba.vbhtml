@Modeltype AgioNet.SheduleTestModel
    
@Code
    ViewData("Title") = "Diagnostico - Programar prueba"
    Session("Section") = "diagnostico"
    
    Dim myModel() As AgioNet.TestIDListModel = TempData("Model")
    
    Dim OptionList As List(Of SelectListItem) = New List(Of SelectListItem)
    
    Dim Count As Integer = 0
    Dim _Len As Integer
    _Len = myModel.Length - 1
    While Count <= _Len
        Dim _opt As New SelectListItem()
        _opt.Text = myModel(Count).TestName
        _opt.Value = myModel(Count).TestID
        OptionList.Add(_opt)
        
        Count = Count + 1
    End While
    
    'Opciones para tipo de producto
    Dim opt_ProductType As List(Of String) = New List(Of String)
    opt_ProductType.Add("MDB")
    opt_ProductType.Add("MEM")
    opt_ProductType.Add("HDD")
    opt_ProductType.Add("ODD")
    opt_ProductType.Add("LCD")
    
End Code
<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/diag.css")' rel="stylesheet" type="text/css" />

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<script type="text/javascript">
    $(document).ready(function () {
        $('#NewTest').click(function () {
            NewTestEnable();
        });
    });


    function NewTestEnable() {
        if ($("#NewTest").is(':checked')) {
            document.getElementById('TestName').disabled = false;
            document.getElementById('TestDescription').disabled = false;
        }
        else {
            //alert("No está activado");
            document.getElementById('TestName').disabled = true;
            document.getElementById('TestDescription').disabled = true;
            $("#TestName").val("");
            $("#TestDescription").val("");
        }
    }
</script>

<!-- Aquí se muestran los formularios -->
<div id="main-ContIzquierda">
        <!-- Inicia diseño del formulario -->
        <h2 class="TituloFormulario--dg">Diagnostico - Programar una prueba </h2>
        @Using Html.BeginForm()'"programar_prueba", "diagnostico", FormMethod.Post)
            @<div id="form">
                <span class="TestShedule_Title">Programar una prueba para la  Orden [ @Session("OrderID") ]</span>
                <div class="row">
                    <span class="Span-b"><span class="pcenter">Prueba</span></span>
                    <span class="Span-d">@Html.DropDownListFor(Function(m) m.TestID, OptionList)</span>
                </div>
                <!-- Datos del componente -->

                <div class="row">
                    <span class="Span-b"><span class="pcenter">Clase del producto</span></span>
                    <span class="Span-d">
                        @Html.TextBox("txtProductClass","Component", New With {.Value = "Componente", .disabled = True})
                        @Html.HiddenFor(Function(m) m.ProductClass, New With {.Value = "Componente"})
                    </span>
                </div>
                <div class="row">
                    <span class="Span-b"><span class="pcenter">Tipo de producto</span></span>
                    <span class="Span-d">@Html.DropDownListFor(Function(m) m.ProductType, New SelectList(opt_ProductType))</span>
                </div>
                <div class="row">
                    <span class="Span-b"><span class="pcenter">Marca</span></span>
                    <span class="Span-d">@Html.TextBoxFor(Function(m) m.Trademark) </span>
                </div>
                <div class="row">
                    <span class="Span-b"><span class="pcenter">SKU Equipo</span></span>
                    <span class="Span-d">@Html.TextBoxFor(Function(m) m.Model)</span>
                </div>
                <div class="row">
                    <span class="Span-b"><span class="pcenter">Part No</span></span>
                    <span class="Span-d">@Html.TextBoxFor(Function(m) m.PartNo)</span>
                </div>
                <div class="row">
                    <span class="Span-b"><span class="pcenter">Número de serie</span></span>
                    <span class="Span-d">@Html.TextBoxFor(Function(m) m.SerialNo)</span>
                </div>
                <div class="row">
                    <span class="Span-b"><span class="pcenter">Revisión</span></span>
                    <span class="Span-d">@Html.TextBoxFor(Function(m) m.Revision)</span>
                </div>
                <div class="row-SpArea--dg">
                    <span class="Span-b"><span class="pcenter">Descripción de la falla</span></span>
                    <span class="Span-c">@Html.TextAreaFor(Function(m) m.Failure, New With {.rows = 4, .cols = 32})</span>
                </div>

                <div class="row"> <span class="Span-c">&nbsp;</span> </div>
                <div class="row"> <span class="Span-c">&nbsp;</span> </div>

                <!-- Comentarios -->
                <div class="row-SpArea--dg">
                    <span class="Span-b"><span class="pcenter">Comentarios</span></span>
                    <span class="Span-d">@Html.TextAreaFor(Function(m) m.TestComment, New With {.rows=4, .cols = 32})  </span>
                    <span class="Span-a"><input type="submit" value="Guardar" class="Button" /></span>                    
                    <span class="Span-b">&nbsp;</span>
                </div>
            </div>
        End Using
        
        <div class="row">&nbsp;</div>
        <div class="form-aclaracion">
            <span class="form-aclaracion_Title">Nota:</span>
            <span class="form-aclaracion_Text">
                En caso de de que la <strong>Prueba</strong>, <strong>Modelo</strong>, el <strong>Número de parte</strong>, o la 
                <strong>Marca</strong> del producto que necesitas no estén disponibles en este listado, Favor de contactar a ingeniería 
                (Encargado, <a href="mailto:humberto.vega@agiotech.com">Humberto Vega</a> )
         </span>
    </div>
</div>

<!-- Aquí va la información de la orden -->
<div id="main-ContDerecha" class="bg-fondoborder">
    @Html.Action("LoadOrderInfo")
</div>