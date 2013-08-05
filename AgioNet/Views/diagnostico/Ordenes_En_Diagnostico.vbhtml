@ModelType Agionet.StartDiagnosticModel
    
@Code
    ViewData("Title") = "Diagnostico - Iniciar diagnostico"
    Session("Section") = "diagnostico"
    
    Dim Read() As AgioNet.PendingOrdersModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=False)
End Code
<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/diag.css")' rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {
        if ($('#Contenedor').height() < $("#ContMain").height()) {
            $('#Contenedor').height($("#ContMain").height());
        }
    });
</script>


<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario--dg">Listado de Ordenes en Proceso de Diagnóstico </h2>

@grid.GetHtml(columns:=grid.Columns( _
        grid.Column("Item", format:=Function(item) item.WebGrid.Rows.IndexOf(item) + 1), _
        grid.Column("OrderID", "Orden"), _
        grid.Column("PartNo", "SKU No"), _
        grid.Column("ProductType", "Tipo de producto"), _
        grid.Column("SerialNo", "Numero de serie"), _
        grid.Column("Model", "Modelo"), _
        grid.Column("Description", "Descripcion"), _
        grid.Column("DateR", "Ingresado El"),
        grid.Column("DateD", "En Diagnostico"), _
        grid.Column("DiasTranscurridos", "Dias Transcurridos"), _
        grid.Column("CreateBy", "Creado Por") _
))



