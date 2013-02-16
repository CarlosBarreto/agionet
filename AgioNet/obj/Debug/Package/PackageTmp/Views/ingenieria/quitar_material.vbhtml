@ModelType AgioNet.MaterialMasterModel

@Code

    ViewData("Title") = "ingenieria - Quitar Material"
    Session("Section") = "ingenieria"

    Dim Read() As AgioNet.MaterialMasterListModel = TempData("Model")
    Dim grid As WebGrid = New WebGrid(source:=Read)
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Quitar Material</h2>
@Using Html.BeginForm() '"material_master", "ingenieria", FormMethod.Get, new AjaxOptions { .InsertionMode=InsertionMode.Replace, .UpdateTargetId="resultados" } )
    @<div id="Formulario">       
        <div class="row">
            <span class="Span-a"><span class="pcenter">@Html.LabelFor(Function(m) m.SKUNo)</span></span>
            <span class="Span-i">@Html.TextBoxFor(Function(m) m.SKUNo)</span>

            <span class="Span-a"><span class="pcenter">@Html.LabelFor(Function(m) m.PartNo)</span></span>
            <span class="Span-i">@Html.TextBoxFor(Function(m) m.PartNo)</span>

             <span class="Span-a">
                 <span class="FRaight">
                    <input type="submit" value="Buscar" class="Button" />
                </span>
             </span>
        </div>
        <div class="row"> &nbsp; </div>
    </div>
End Using

<div id="gridd">
    @grid.GetHtml(columns:=grid.Columns( _
                    grid.Column("SKUNo", "Equipo"), _
                    grid.Column("PartNo", "Número de Parte"), _
                    grid.Column("Description", "Descripción"), _
                    grid.Column("Alt", "Alterno") _
    ))
</div>
