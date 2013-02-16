@ModelType AgioNet.EstacionesModel
    
@Code
    ViewData("Title") = "Ingeniería - Listado de estaciones"
    Session("Section") = "ingenieria"

    Dim Read As AgioNet.EstacionesModel = TempData("Model")
    Dim myModel() As AgioNet.UserListModel = TempData("UserModel")
    

    Dim OptionList As List(Of SelectListItem) = New List(Of SelectListItem)
    
    Dim Count As Integer = 0
    Dim _Len As Integer
    _Len = myModel.Length - 1
    Dim _opt As New SelectListItem()
    _opt.Text = Read.Usuario
    _opt.Value = Read.Usuario
    OptionList.Add(_opt)
    
    While Count <= _Len
        _opt = New SelectListItem()
        _opt.Text = myModel(Count).Nombre
        _opt.Value = myModel(Count).Usuario
        OptionList.Add(_opt)
        
        Count = Count + 1
    End While
    
End Code
<script type="text/javascript">
    $(function () {
        $("#Descripcion").val("@Html.Raw(Read.Descripcion)");
    });
</script>
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<h2 class="TituloFormulario">Ingenieria - Asignación de un responsable para la Estación</h2>
@Using Ajax.BeginForm("asignarresponsable", "ingenieria", New AjaxOptions With {.UpdateTargetId = "tab-content"})
    @<div id="Formulario">       
        <div class="row">
            <span class="Span-b"><span class="pcenter">Nombre: </span></span>
            <span class="Span-d"> @Html.TextBoxFor(Function(m) m.Nombre, New With {.Value = Read.Nombre })</span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="pcenter">Responsable: </span></span>
            <span class="Span-d"> 
                @Html.DropDownListFor(Function(m) m.Usuario, OptionList)
            </span>
        </div>

        <div class="row">
            <span class="Span-b">&nbsp;</span>
            <span class="Span-c">&nbsp;</span>
            <span class="Span-a"> <input type="submit" value="Guardar" class="Button" /> </span>
        </div>
    </div>
end Using



