@ModelType AgioNet.OrderListModel

@Code
    ViewData("Title") = "Servicio a clientes - Autorización de Reparación"
    Session("Section") = "sc"
    
    Dim Read As AgioNet.AppFailureInfoModel = TempData("FailureInfo")
    'Dim Order As Object = TempData("OrderInfo")
    
    'Dim grid As WebGrid = New WebGrid(Read)
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
    <h2 class="TituloFormulario">Información de la Orden</h2>
    <div id="Formulario">
           
    </div>

    <h2 class="TituloFormulario">Listado de fallas para autorizar reparación</h2>
    @Using Html.BeginForm()
        @<div id="Formulario2">
            <div class="row">
                <span class="Span-c"></span>
            </div>
            <div class="row"></div>
        </div>
    End Using
</div>
<!-- Aquí va la información de la orden -->
<div id="main-ContDerecha" class="bg-fondoborder">
    @Html.Action("LoadOrderInfo")
</div>
<!--
    'grid.GetHtml(columns:=grid.Columns( _
    '              grid.Column("OrderID", "Orden"), _
    '                   grid.Column("TestID", "ID de prueba"), _
    '                   grid.Column("TestDescription", "Descripción"), _
    '                   grid.Column("FailureID", "ID de Falla"), _
    '                   grid.Column("FailureDescription", "Descripción de falla"), _
    '             grid.Column("PossibleSolution", "Posible Solución"), _
    '             grid.Column("Log", "Reporte de Prueba"), _
    '             grid.Column("TestResult", "Resultado de Prueba"), _
    '             grid.Column("FoundBy", "Reportado Por"), _
    '             grid.Column("FoundDate", "Fecha de reporte") _
))-->

