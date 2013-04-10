@ModelType AgioNet.CheckinReportOptModel

@Code

    ViewData("Title") = "Almacen - Equipos recibidos en almacén"
    Session("Section") = "almacen"

    Dim Read() As AgioNet.CheckinReportModel = TempData("Model")
    Dim opt As AgioNet.CheckinReportOptModel = TempData("OptModel")
    Dim grid As WebGrid = New WebGrid(source:=Read, canPage:=False)
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<script type="text/javascript">
    $(document).ready(function() {
        $("#StartDate").datepicker({ dateFormat: "dd/mm/yy" });
        $("#EndDate").datepicker({ dateFormat: "dd/mm/yy" });
    });
</script>
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Reporte de Recibo Almacén</h2>
@Using Html.BeginForm() '"material_master", "ingenieria", FormMethod.Get, new AjaxOptions { .InsertionMode=InsertionMode.Replace, .UpdateTargetId="resultados" } )
    @<div id="Formulario">       
        <div class="row">
            <span class="Span-a"><span class="pcenter">@Html.LabelFor(Function(m) m.StartDate)</span></span>
            <span class="Span-i">@Html.TextBoxFor(Function(m) m.StartDate, New With {.Value = opt.StartDate})</span>

            <span class="Span-a"><span class="pcenter">@Html.LabelFor(Function(m) m.EndDate)</span></span>
            <span class="Span-i">@Html.TextBoxFor(Function(m) m.EndDate, New With {.Value = opt.EndDate})</span>

             <span class="Span-a">
                 <span class="FRaight">
                    <input type="submit" value="Buscar" class="Button" />
                </span>
             </span>
        </div>
        <div class="row"> &nbsp; </div>
    </div>
End Using

<!-- Ventana modal de error -->
@If TempData("ErrMsg") = "" Then
    @<div id="gridd">
        @grid.GetHtml(columns:=grid.Columns( _
            grid.Column("OrderID", "Orden"), _
            grid.Column("OrderDate", "Fecha de Alta"), _
            grid.Column("FechaIngreso", "Fecha de ingreso"), _
            grid.Column("Guia", "Guia"), _
            grid.Column("Longitud", "Longitud"), _
            grid.Column("Ancho", "Ancho"), _
            grid.Column("Altura", "Altura"), _
            grid.Column("PesoEmbarque", "Peso Embarque"), _
            grid.Column("Comment", "Comentarios"), _
            grid.Column("CreateBy", "Creado Por") _
    ))
    </div>
End If