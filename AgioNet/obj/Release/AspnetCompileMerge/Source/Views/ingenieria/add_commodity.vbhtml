@ModelType AgioNet.AddCommodityModel

@Code

    ViewData("Title") = "ingenieria - Editar Material"
    Session("Section") = "ingenieria"

    Dim Read() As AgioNet.AddCommodityModel = TempData("Model")
    Dim grid As WebGrid = New WebGrid(source:=Read, canPage:=False)
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/diag.css")' rel="stylesheet" type="text/css" />

<!-- Aquí se muestran los formularios -->
<div id="main-ContIzquierda">
    <!-- Inicia diseño del formulario -->
    <h2 class="TituloFormulario">Editar Material</h2>
    @Using Html.BeginForm() '"material_master", "ingenieria", FormMethod.Get, new AjaxOptions { .InsertionMode=InsertionMode.Replace, .UpdateTargetId="resultados" } )
        @<div id="Formulario">       
            <div class="row">
                <span class="Span-a"><span class="pcenter"><label>Nombre: </label></span></span>
                <span class="Span-g">@Html.TextBoxFor(Function(m) m.CommodityName)</span>

                <span class="Span-g"><span class="pcenter"><label>Descripción: </label> </span></span>
                <span class="Span-l">@Html.TextBoxFor(Function(m) m.CommodityDescription)</span>

                    <span class="Span-a">
                        <span class="FRaight">
                        <input type="submit" value="Agregar" class="Button" />
                    </span>
                    </span>
            </div>
            <div class="row"> &nbsp; </div>
        </div>
    End Using

    <div id="gridd">
        @grid.GetHtml(columns:=grid.Columns( _
                        grid.Column("CommodityID", "ID"), _
                        grid.Column("CommodityName", "Nombre"), _
                        grid.Column("CommodityDescription", "Descripción") _              
        ))
    </div>
</div>

<!-- grid.Column(format:=Function(item) Html.ActionLink("Editar", "editarmaterial", "ingenieria", _
                                New With {.SKUNo = item.SKUNo, .PartNo = item.PartNo, .Description = item.Description, .Alt = item.Alt}, _
                                vbNull)) _
-->