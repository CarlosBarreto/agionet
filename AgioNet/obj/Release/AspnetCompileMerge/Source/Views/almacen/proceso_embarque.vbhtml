@ModelType AgioNet.CheckinReportOptModel
    
@Code
    ViewData("Title") = "Almacen - Ordenes en Proceso de Embarque"
    Session("Section") = "almacen"
    
    Dim Read() As AgioNet.PendingProcessWarehouseModel = TempData("Model")
    Dim opt As AgioNet.CheckinReportOptModel = TempData("OptModel")
    
    Dim grid As WebGrid = New WebGrid(Read, canPage:=False)
End Code



<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->

<div id="xx" style="display:block; height: 80%; width:100%; overflow-y:auto; overflow-x:hidden;">
    <h2 class="TituloFormulario">Listado de Ordenes en Proceso de Embarque</h2>     
            <!-- Busqueda por nombre del cliente -->
            @Using Html.BeginForm("proceso_embarque", "almacen", FormMethod.Get) '"material_master", "ingenieria", FormMethod.Get, new AjaxOptions { .InsertionMode=InsertionMode.Replace, .UpdateTargetId="resultados" } )
               @<div id="Formulario">       
                    <div class="row">
                        <span class="Span-b"><span class="pcenter"><label>Fecha Inicio: </label></span></span>
                        <span class="Span-i">@Html.TextBoxFor(Function(m) m.StartDate, New With {.Value = opt.StartDate})</span>

                        <span class="Span-b"><span class="pcenter"><label>Fecha Final: </label></span></span>
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

    @grid.GetHtml(columns:=grid.Columns( _
            grid.Column("Item", format:=Function(item) item.WebGrid.Rows.IndexOf(item) + 1), _
            grid.Column("OrderID", "Orden"), _
            grid.Column("ProductType", "Tipo de producto"), _
            grid.Column("SerialNumber", "Numero de serie"), _
            grid.Column("ProductModel", "Modelo"), _
            grid.Column("ProductDescription", "Descripcion"), _
            grid.Column("Ingresado", "Ingresado"), _
            grid.Column("Procesado", "Recibo PT"), _
            grid.Column("DateComp", "Fecha Compromiso") _
    ))

</div>
 
<script type="text/javascript">
    $(document).ready(function () {
        $("#Comment").html("@User.Identity.Name : Procesar orden... ");
        $("#StartDate").datepicker({ dateFormat: "dd/mm/yy" });
        $("#EndDate").datepicker({ dateFormat: "dd/mm/yy" });
    });
</script>