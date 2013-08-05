@ModelType Agionet.RegFailureModel
    
@Code
    Dim isTested As New AgioNet.isTestedModel '= Session("isTested")
    isTested.IsTested(Session("OrderID")) ' = Session("isTested")
    
    Dim read As AgioNet.RegFailureModel = TempData("model")
    
    Dim OptionList As List(Of SelectListItem) = New List(Of SelectListItem)
    Dim _opt As SelectListItem
    
    Dim opRemplazo As Boolean = False, opReparacion As Boolean = False, opNuevaParte As Boolean = False
    Dim opComponente As Boolean = False, opNTF As Boolean = False
    
    Select Case read.TipoReparacion
        Case "Reemplazo"
            opRemplazo = True
        Case "Reparación"
            opReparacion = True
        Case "Nueva Parte"
            opNuevaParte = True
        Case "Reparación Componente"
            opComponente = True
        Case "NTF"
            opNTF = True
    End Select
    _opt = New SelectListItem With {.Text = "Reemplazo", .Value = "Reemplazo", .Selected = opRemplazo}
    OptionList.Add(_opt)
    
    _opt = New SelectListItem With {.Text = "Reparación", .Value = "Reparación", .Selected = opReparacion}
    OptionList.Add(_opt)
    
    _opt = New SelectListItem With {.Text = "Nueva Parte", .Value = "Nueva Parte", .Selected = opNuevaParte}
    OptionList.Add(_opt)

    _opt = New SelectListItem With {.Text = "Reparación Componente", .Value = "Reparación Componente", .Selected = opComponente}
    OptionList.Add(_opt)
    
    _opt = New SelectListItem With {.Text = "NTF", .Value = "NTF", .Selected = opNTF}
    OptionList.Add(_opt)
    
    Dim Failure As String = TempData("Failure")
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

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario--dg">Registrar Falla</h2>
@Using Ajax.BeginForm("AddFailure", "diagnostico", New AjaxOptions With {.UpdateTargetId = "tab-ContIzquierda"}, New With {.Method = "POST"})
    @<div id="Formulario">       
        <div class="row">
            <span class="Span-i">OrderID</span>
            <span class="Span-c">
                @Html.TextBoxFor(Function(m) m.OrderID, New With {.Value = Session("OrderID")})
            </span>
        </div>

        <div class="row">
            <span class="Span-i">Tipor Reparación</span>
            <span class="Span-c">@Html.DropDownListFor(Function(m) m.TipoReparacion, OptionList)</span>
        </div>

        <div class="row">
            <span class="Span-i">Diagnóstico / Falla Detectada</span>
            <span class="Span-e">@Html.TextBoxFor(Function(m) m.Failure, New With {.Value = Failure})</span>
        </div>
        <div class="row"></div>

        <div class="form-SPArea--dgl">
            <span class="Span-i">Reparación a Realizar</span>
            <span class="Span-e">
                 @Html.TextAreaFor(Function(m) m.Solution, New With {.rows = 4, .cols = 55})
            </span>
        </div>

        <div class="form-SPArea--dgl">
            <span class="Span-i">Fuente de Suministro</span>
            <span class="Span-e">
                 @Html.TextAreaFor(Function(m) m.Source, New With {.rows = 4, .cols = 55})
            </span>
        </div>

        <div class="form-SPArea--dgl">
            <span class="Span-i">Comentarios</span>
            <span class="Span-e">
                 @Html.TextAreaFor(Function(m) m.Comment, New With {.rows = 4, .cols = 55})
            </span>
        </div>
        
        <div class="row">
            <span class="Span-i">Registrada Por:</span>
            <span class="Span-b">@Html.TextBoxFor(Function(m) m.User, New With {.Value = User.Identity.Name}) </span>
            <span class="Span-i">&nbsp;</span>
            <span class="Span-b"><input type="submit" value="Guardar" class="Button" /></span>
        </div>

    </div>
End Using
