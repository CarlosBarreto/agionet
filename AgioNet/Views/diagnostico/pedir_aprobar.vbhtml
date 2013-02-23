﻿@ModelType Agionet.ReqApprovalModel

@Code
    ViewData("Title") = "Diagnostico - Solicitar aprobación"
    Session("Section") = "diagnostico"
    
    Dim Read() As AgioNet.TestReportModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=True, rowsPerPage:=20)
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

@Html.Action("LoadWnOrderInfo")

@Using Html.BeginForm()
    @<span class="row">
        <span class="Span-b">@Html.HiddenFor(Function(m) m.OrderID, New With {.Value = Session("OrderID")})</span>
        <span class="Span-j">@Html.TextBoxFor(Function(m) m.Comment) </span>
        <span class="Span-a"> <input type="submit" value="SCAN" class="Button" /></span>
    </span>
End Using
