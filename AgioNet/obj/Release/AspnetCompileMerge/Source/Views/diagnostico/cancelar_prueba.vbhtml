@ModelType AgioNet.ExecTestModel

@Code
    ViewData("Title") = "Diagnostico - Cancelación de pruebas"
    Session("Section") = "diagnostico"
    
    Dim Read() As AgioNet.TestListModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=True, rowsPerPage:=20)
End Code

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Cancelar una prueba</h2>

@grid.GetHtml(columns:=grid.Columns( _
                   grid.Column("TESTID", "TESTID", style:="TestID_"), _
                   grid.Column("ORDERID", "ORDERID", style:="OrderID_"), _
                   grid.Column("TESTNAME", "TESTNAME", style:="TestName_"), _
                   grid.Column("TESTDESCRIPTION", "TESTDESCRIPTION", style:="TestDescription_"), _
                   grid.Column("TESTRESULT", "TESTRESULT", style:="TestResult_"), _
                   grid.Column("TESTSTART", "TESTSTART", style:="TestStart_"), _
                   grid.Column("TESTEND", "TESTEND", style:="TestEnd_"), _
                   grid.Column("CREATEBY", "CREATEBY", style:="CreateBy_"), _
                   grid.Column(format:=Function(item) If(item.TESTRESULT = "", Html.ActionLink("Cancelar Prueba", "CancelTest", "diagnostico", New With {.TESTID = item.TESTID}, vbNull), ""), style:="Link_") _
))

@Html.Action("LoadWnOrderInfo")
