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

    
End Code
<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/diag.css")' rel="stylesheet" type="text/css" />

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Aquí se muestran los formularios -->
<div id="main-ContIzquierda">
        <!-- Inicia diseño del formulario -->
        <h2 class="TituloFormulario--dg">Diagnostico - Programar Prueba </h2>
        @Using Html.BeginForm()'"programar_prueba", "diagnostico", FormMethod.Post)
            @<div id="form">
                <span class="TestShedule_Title">Programar una prueba para la  Orden [ @Session("OrderID") ]</span>
                <div class="row">
                    <span class="Span-b"><span class="pcenter">OrderID: </span></span>
                    <span class="Span-d"> @Html.TextBoxFor(Function(m) m.OrderID, New With {.Value = Session("OrderID")})
                    </span>
                </div>
                <div class="row">
                    <span class="Span-b"><span class="pcenter">Prueba</span></span>
                    <span class="Span-d">@Html.DropDownListFor(Function(m) m.Prueba, OptionList)</span>
                </div>

                <!-- Datos del componente -->  
                <div class="row">
                    <span class="Span-b"><span class="pcenter">Marca</span></span>
                    <span class="Span-d">@Html.TextBoxFor(Function(m) m.Marca) </span>
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
                <!-- Comentarios -->
                <div class="form-SpArea--dgt">
                    <span class="Span-b"><span class="pcenter">Comentario:</span> </span>
                    <span class="Span-e">
                        @Html.TextAreaFor(Function(m) m.Comentarios, New With {.rows = 10, .cols = 60})
                    </span>
                </div>

                <div class="row">
                    <span class="Span-d">&nbsp;</span>
                    <span class="Span-c">&nbsp;</span>
                    <span class="Span-a"><input type="submit" value="Guardar" class="Button" /></span>                    
                </div>
                
            </div>
        End Using
  
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