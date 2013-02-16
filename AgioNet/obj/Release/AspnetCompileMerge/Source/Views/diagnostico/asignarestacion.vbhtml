@ModelType AgioNet.AsignarEstacionModel
    
@Code
    ViewData("Title") = "Asignación de una estación para la prueba"
    Session("Section") = "diagnostico"

    Dim Read() As AgioNet.EstacionesModel = TempData("Model")
    Dim TestID As String = TempData("TestID")
    TempData.Keep("TestID")
    
    Dim OptionList As List(Of SelectListItem) = New List(Of SelectListItem)
    
    Dim Count As Integer = 0
    Dim _Len As Integer
    _Len = Read.Length - 1
    
    Dim _opt As New SelectListItem() 
    While Count <= _Len
        _opt = New SelectListItem()
        _opt.Text = Read(Count).Nombre
        _opt.Value = Read(Count).Nombre
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

    <h2 class="TituloFormulario--dg">Asignación de una estación para la prueba</h2>
    @Using Html.BeginForm("asignarestacion", "diagnostico") ', New AjaxOptions With {.UpdateTargetId = "tab-content"})
        @<div id="Formulario">       
            <div class="row">
                <span class="Span-b"><span class="pcenter">ID de la prueba: </span></span>
                <span class="Span-d"> @Html.TextBoxFor(Function(m) m.TestID, New With {.Value = TestID})</span>
            </div>

            <div class="row">
                <span class="Span-b"><span class="pcenter">Estación: </span></span>
                <span class="Span-d"> 
                    @Html.DropDownListFor(Function(m) m.Estacion, OptionList)
                    @Html.HiddenFor(Function(m) m.User, New With {.Value = User.Identity.Name})
                </span>
            </div>

            <div class="row">
                <span class="Span-b">&nbsp;</span>
                <span class="Span-c">&nbsp;</span>
                <span class="Span-a"> <input type="submit" value="Asignar" class="Button" /> </span>
            </div>
        </div>
    end Using
</div>

<!-- Aquí va la información de la orden -->
<div id="main-ContDerecha" class="bg-fondoborder">
    @Html.Action("LoadTestInfo")
</div>


