@ModelType Agionet.ReqApprovalModel

@Code
    ViewData("Title") = "Diagnostico - Solicitar aprobación"
    Session("Section") = "diagnostico"
    
    Dim Read() As AgioNet.TestListModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=True, rowsPerPage:=20)
End Code

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Solicitar Aprobación del diagnostico</h2>

@grid.GetHtml(columns:=grid.Columns( _
                   grid.Column("TESTID", "TESTID", style:="TestID_"), _
                   grid.Column("ORDERID", "ORDERID", style:="OrderID_"), _
                   grid.Column("TESTNAME", "TESTNAME", style:="TestName_"), _
                   grid.Column("TESTDESCRIPTION", "TESTDESCRIPTION", style:="TestDescription_"), _
                   grid.Column("TESTRESULT", "TESTRESULT", style:="TestResult_"), _
                   grid.Column("TESTSTART", "TESTSTART", style:="TestStart_"), _
                   grid.Column("TESTEND", "TESTEND", style:="TestEnd_"), _
                   grid.Column("CREATEBY", "CREATEBY", style:="CreateBy_") _
))

@Html.Action("LoadWnOrderInfo")

@Using Html.BeginForm()
    @<span class="row">
        <span class="Span-b">@Html.HiddenFor(Function(m) m.OrderID, New With {.Value = Session("OrderID")})</span>
        <span class="Span-j">@Html.TextBoxFor(Function(m) m.Comment) </span>
        <span class="Span-a"> <input type="submit" value="SCAN" class="Button" /></span>
    </span>
End Using
