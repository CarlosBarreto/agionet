@ModelType Agionet.ScanPackModel
    
@Code
    ViewData("Title") = "Almacen - Embarcar equipo"
    Session("Section") = "almacen"
End Code
<script type="text/javascript">
    $(document).ready(function () {
        $("#Comment").html("@User.Identity.Name : Embarcar equipo... ");
    });
</script>
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If


<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Embarcar equipo</h2>

<div id="ContenedorOrderIDForm">
@Using Html.BeginForm()
    @<div id="Formulario">
        <span class="row">
            <span class="Span-c">@Html.LabelFor(Function(m) m.OrderID) </span>
            <span class="Span-j">@Html.TextBoxFor(Function(m) m.OrderID, New With {.class = "text"})</span>
        </span>
        
        <span class="row">&nbsp;</span>

        <span class="row">
            <span class="Span-c">@Html.LabelFor(Function(m) m.Comentario) </span>
            <span class="Span-j">@Html.TextAreaFor(Function(m) m.Comentario, New With {.rows = 6, .cols = 53})</span>
         </span>
        <span class="row">&nbsp;</span>
        <span class="row">&nbsp;</span>
        <span class="row">
            <span class="Span-b">&nbsp;</span>
            <span class="Span-j">&nbsp;</span>
            <span class="Span-a"> <input type="submit" value="SCAN" class="Button" /></span>
         </span>
    </div>
End Using
</div>
