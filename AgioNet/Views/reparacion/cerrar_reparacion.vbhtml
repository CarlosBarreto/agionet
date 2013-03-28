﻿@ModelType Agionet.ScanOrderModel
    
@Code
    ViewData("Title") = "Reparación -Terminar Reparación (Transfer)"
    Session("Section") = "reparacion"
End Code
<script type="text/javascript">
    $(document).ready(function () {
        $("#Comment").html("@User.Identity.Name : Terminado el proceso de reparación... ");
    });
</script>
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If


<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Terminar con la Reparación (Transfer)</h2>

<div id="ContenedorOrderIDForm">
@Using Html.BeginForm()
    @<span class="row">
        <span class="Span-b">@Html.LabelFor(Function(m) m.OrderID) </span>
        <span class="Span-j">@Html.TextBoxFor(Function(m) m.OrderID, New With {.class = "text"})</span>
        
    </span>
    
    @<span class="row">&nbsp;</span>
    
    @<span class="row">
        <span class="Span-b">@Html.LabelFor(Function(m) m.Comment) </span>
        <span class="Span-j">@Html.TextAreaFor(Function(m) m.Comment, New With {.rows = 6, .cols = 53})</span>
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