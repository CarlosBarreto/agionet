@Modeltype AgioNet.TestListModel
    
@Code
    ViewData("Title") = "Diagnostico - Asignar a una estación"
    Session("Section") = "diagnostico"
        
    Dim Read() As AgioNet.TestListModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=True, rowsPerPage:=20)
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
    <h2 class="TituloFormulario--dg">Diagnostico - Pruebas disponibles</h2>
    @grid.GetHtml(columns:=grid.Columns( _
        grid.Column("TESTID", "TESTID", style:="TestID_"), _
        grid.Column("ORDERID", "ORDERID", style:="OrderID_"), _
        grid.Column("TESTNAME", "TESTNAME", style:="TestName_"), _
        grid.Column("TESTDESCRIPTION", "TESTDESCRIPTION", style:="TestDescription_"), _
        grid.Column("CREATEBY", "CREATEBY", style:="CreateBy_"), _
        grid.Column(format:=Function(item) Html.ActionLink("Asignar", "asignarestacion", "diagnostico", New With {.TESTID = item.TESTID}, vbNull), style:="Link_") _
    ))
       

    </div>
</div>

<!-- Aquí va la información de la orden -->
<div id="main-ContDerecha" class="bg-fondoborder">
    @Html.Action("LoadOrderInfo")
</div>