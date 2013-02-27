@ModelType Agionet.ReqApprovalModel

@Code
    ViewData("Title") = "Diagnostico - Solicitar aprobación"
    Session("Section") = "diagnostico"
    
    Dim Read() As AgioNet.TestReportModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=True, rowsPerPage:=18)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Solicitar Aprobación del diagnostico</h2>

@grid.GetHtml(columns:=grid.Columns( _
                   grid.Column("OrderID", "Orden", style:="TestID_"), _
                   grid.Column("TestName", "Prueba", style:="OrderID_"), _
                   grid.Column("TestResult", "Resultado", style:="TestName_"), _
                   grid.Column("Failure", "Falla", style:="TestDescription_"), _
                   grid.Column("TextLog", "TextLog", style:="TestResult_"), _
                   grid.Column("TestStart", "Inicio", style:="TestStart_"), _
                   grid.Column("TestEnd", "Fin", style:="TestEnd_"), _
                   grid.Column("CreateBy", "Creada Por", style:="CreateBy_") _
))

<div class="row">&nbsp;</div>

@Using Html.BeginForm()
    @<div class="form-SPArea--dgl">
        <span class="Span-b">Comentarios</span>
        <span class="Span-e">
            @Html.HiddenFor(Function(m) m.OrderID, New With {.Value = Session("OrderID")}) 
            @Html.TextAreaFor(Function(m) m.Comment, New With {.rows = 4, .cols = 55})
        </span>
    </div>
   
   @<div class="row">&nbsp;</div>
   @<div class="row">&nbsp;</div>
    
    @<span class="row">
        <span class="Span-a">&nbsp;</span>
        <span class="Span-c">&nbsp;</span>
        <span class="Span-c">&nbsp;</span>
        <span class="Span-a"> 
            <input type="submit" value="SCAN" class="Button" />
        </span>
    </span>
End Using
