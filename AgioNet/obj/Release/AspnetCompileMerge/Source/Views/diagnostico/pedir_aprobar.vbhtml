@ModelType Agionet.ReqApprovalModel

@Code
    ViewData("Title") = "Diagnostico - Enviar Diagnóstico"
    Session("Section") = "diagnostico"
    
    Dim Read() As AgioNet.TestReportModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=True, rowsPerPage:=18)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Proceso de Enviar Diagnóstico</h2>

@grid.GetHtml(columns:=grid.Columns( _
                   grid.Column("OrderID", "Orden", style:="TestID_"), _
                   grid.Column("TestName", "Prueba", style:="OrderID_"), _
                   grid.Column("TestResult", "Resultado", style:="TestName_"), _
                   grid.Column("CapFailure", "Falla Capturada"), _
                   grid.Column("Failure", "Tipo de Falla", style:="TestDescription_"), _
                   grid.Column("TextLog", "TextLog", style:="TestResult_"), _
                   grid.Column("TestEnd", "Fin", style:="TestEnd_"), _
                   grid.Column("CreateBy", "Creada Por", style:="CreateBy_"), _
                   grid.Column("TipoReparacion", "Tipo Reparación") _
))
<!-- grid.Column("TestStart", "Inicio", style:="TestStart_"), _-->
<div class="row">&nbsp;</div>

@Using Html.BeginForm()
    @<div class="row">
        <span class="Span-b">Falla Reportada</span>
        <span class="Span-k">@Html.Encode(TempData("FallaReportada"))</span>
     </div>
    @<div class="row"></div>
    @<div class="row">
        <span class="Span-b">Retro-alimentación</span>
        <span class="Span-e">@Html.TextBoxFor(Function(m) m.Retro)</span>
     </div>
    
    @<div class="row"></div>
    @<div class="form-SPArea--dlg">
        <span class="Span-b">Comentario Interno (Para los Técnicos)</span>
        <span class="Span-e">
            @Html.TextAreaFor(Function(m) m.TComment, New With {.rows = 4, .cols = 55})
        </span>
     </div>
    @<div class="row">&nbsp;</div>
    @<div class="row">&nbsp;</div>
    @<div class="form-SPArea--dgl">
        <span class="Span-b">Resumen para Atención a Clientes/Compras</span>
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
