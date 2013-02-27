@ModelType Agionet.RegFailureModel
    
@Code
    Dim isTested As AgioNet.isTestedModel = Session("isTested")
    Dim read As AgioNet.RegFailureModel = TempData("model")
    
End Code
<script type="text/javascript"> 
    $(document).ready(function () {
        var data = "@read.HasFailure";
        if (data == "1") {
            $("#Solution").html("@Replace(read.Solution, vbNewLine, "\n")");
            $("#Source").html("@Replace(read.Source, vbNewLine, "\n")");
            $("#Comment").html("@Replace(read.Comment, vbNewLine, "\n")");
            $(".TituloFormulario--dg").html("La prueba [@isTested.TestID] ya ha sido realizada.");
            $(".Button").css("visibility", "hidden");
        }
    }); 
</script>

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario--dg">Registrar Una falla</h2>
@Using Ajax.BeginForm("AddFailure", "diagnostico", New AjaxOptions With {.UpdateTargetId = "tab-ContIzquierda"}, New With {.Method = "POST"})
    @<div id="Formulario">       
        <div class="row">
            <span class="Span-b">OrderID</span>
            <span class="Span-c">
                @Html.TextBoxFor(Function(m) m.OrderID, New With {.Value = Session("OrderID")})
            </span>
        </div>

        <div class="row">
            <span class="Span-b">Diagnóstico / Falla Detectada</span>
            <span class="Span-e">@Html.TextBoxFor(Function(m) m.Failure, New With {.Value = read.Failure})</span>
        </div>
        
        <div class="form-SPArea--dgl">
            <span class="Span-b">Reparación a Realizar</span>
            <span class="Span-e">
                 @Html.TextAreaFor(Function(m) m.Solution, New With {.rows = 4, .cols = 55})
            </span>
        </div>

        <div class="form-SPArea--dgl">
            <span class="Span-b">Fuente de Suministro</span>
            <span class="Span-e">
                 @Html.TextAreaFor(Function(m) m.Source, New With {.rows = 4, .cols = 55})
            </span>
        </div>

        <div class="form-SPArea--dgl">
            <span class="Span-b">Comentarios</span>
            <span class="Span-e">
                 @Html.TextAreaFor(Function(m) m.Comment, New With {.rows = 4, .cols = 55})
            </span>
        </div>
        
        <div class="row">
            <span class="Span-b">Found By</span>
            <span class="Span-b">@Html.TextBoxFor(Function(m) m.User, New With {.Value = User.Identity.Name}) </span>
            <span class="Span-c">&nbsp;</span>
            <span class="Span-b"><input type="submit" value="Guardar" class="Button" /></span>
        </div>

    </div>
End Using
