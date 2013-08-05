@ModelType AgioNet.MaterialMasterModel

@Code

    ViewData("Title") = "ingenieria - Agregar Material Al BOM"
    Session("Section") = "ingenieria"

    Dim Read() As AgioNet.MaterialMasterListModel = TempData("Model")
    Dim grid As WebGrid = New WebGrid(source:=Read, canPage:=False)
    Dim SKUNo As String = TempData("SKUNo")
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Agregar Material Al BOM</h2>
@Using Html.BeginForm("agregar_material_bom", "ingenieria", FormMethod.Post)
    @<div id="Formulario">       
        <div class="row">
            <span class="Span-b"><span class="pcenter">@Html.LabelFor(Function(m) m.SKUNo)</span></span>
            <span class="Span-i">@Html.TextBoxFor(Function(m) m.SKUNo)</span>
            <span class="Span-a">&nbsp;</span>
            <span class="Span-a"><input type="submit" value="Buscar" class="Button" name="action"/></span>
        </div>
        <div class="row">
            <span class="Span-b"><span class="pcenter"><label>Número de Parte</label></span></span>
            <span class="Span-i"> 
                @Html.TextBoxFor(Function(m) m.PartNo)
            </span>
            <span class="Span-a">&nbsp;</span>
            <span class="Span-a"><input type="submit" value="Agregar" class="Button" name="action" /></span>
        </div>
        <div class="row"> &nbsp; </div>
    </div>
End Using



<h2 class="TituloFormulario">Números de Parte Que Componen al BOM</h2>
<div id="gridd">
    @grid.GetHtml(columns:=grid.Columns( _
                    grid.Column("SKUNo", "Equipo"), _
                    grid.Column("PartNo", "Número de Parte"), _
                    grid.Column("Commodity", "Commodity"), _
                    grid.Column("Description", "Descripción"), _
                    grid.Column("Alt", "Alterno") _
    ))
</div>
