﻿@ModelType Agionet.ScanOrderModel
    
@Code
    ViewData("Title") = "Logística - Salida InTransit"
    Session("Section") = "logistica"
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If


<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Proceso Salida InTransit</h2>

<div id="ContenedorOrderIDForm">
@Using Html.BeginForm()
    @<span class="row">
        <span class="Span-b">@Html.LabelFor(Function(m) m.OrderID) </span>
        <span class="Span-j">@Html.TextBoxFor(Function(m) m.OrderID, New With {.class = "text"})</span>
        
    </span>
    
    @<span class="row">&nbsp;</span>
    
    @<span class="row">
        <span class="Span-b">@Html.LabelFor(Function(m) m.Comment) </span>
        <span class="Span-j">@Html.TextAreaFor(Function(m) m.Comment, New With {.Value = "@" & User.Identity.Name & ": En proceso de recolección... ", .rows = 6, .cols = 53})</span>
     </span>
    @<span class="row">&nbsp;</span>
    @<span class="row">&nbsp;</span>
    @<span class="row">
        <span class="Span-b">&nbsp;</span>
        <span class="Span-j">&nbsp;</span>
        <span class="Span-a"> <input type="submit" value="SCAN" class="Button" /></span>
     </span>
End Using
</div>
