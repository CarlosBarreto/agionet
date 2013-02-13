@ModelType Agionet_v1.ViewFoundFailureModel

@Code
    ViewData("Title") = "REPAIR/ Listado de Fallas"
    Session("Section") = "REPAIR"
    
    Dim Read() As Agionet_v1.ViewFailureModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=True, rowsPerPage:=20)
End Code

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">REPAIR / Listado de Fallas</h2>

@grid.GetHtml(columns:=grid.Columns( _
                   grid.Column("FailureID", "FailureID", style:="TestID_"), _
                   grid.Column("Description", "Description", style:="OrderID_"), _
                   grid.Column("Solution", "Possible Solution", style:="TestName_"), _
                   grid.Column("FoundBy", "Found By", style:="TestDescription_"), _
                   grid.Column("Resolved", "Resolved", style:="TestResult_"), _
                   grid.Column(format:=Function(item) If(item.Resolved = "NO", Html.ActionLink("Remplazar", "ViewFailure", "DIAGNOSTIC", New With {.TestID = item.TESTID}, vbNull), ""), style:="Link_"), _
                   grid.Column(format:=Function(item) If(item.Resolved = "NO", Html.ActionLink("Reparar", "ViewFailure", "DIAGNOSTIC", New With {.TestID = item.TESTID}, vbNull), ""), style:="Link_") _
))

@Html.Action("LoadWnOrderInfo")