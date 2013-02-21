﻿@ModelType Agionet.ExecTestModel
    
@Code
    Dim isTested As AgioNet.isTestedModel = Session("isTested")
    
    Dim OptionList As List(Of SelectListItem) = New List(Of SelectListItem)
    Dim _opt As SelectListItem
    
    _opt = New SelectListItem With {.Text = "PASS", .Value = "PASS"}
    OptionList.Add(_opt)
    
    If isTested.Result = "FAIL" Then
        _opt = New SelectListItem With {.Text = "FAIL", .Value = "FAIL", .Selected = True}
    Else
        _opt = New SelectListItem With {.Text = "FAIL", .Value = "FAIL"}
    End If
    OptionList.Add(_opt)   
    
    If isTested.Response = "Data" Then
        Response.Write("")
    End If
    
End Code
<script type="text/javascript"> 
    $(document).ready(function () {
        var data = "@isTested.Response";
        if (data == "Data") {
            $("#TextLog").html("@isTested.TextLog");
            $(".TituloFormulario--dg").html("La prueba [@isTested.TestID] ya ha sido realizada.");
            $(".Button").css("visibility", "hidden");
        }
    }); 
</script>
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario--dg"> Realizar una prueba</h2>

@Using Ajax.BeginForm("ExecuteTest", "diagnostico", New AjaxOptions With {.UpdateTargetId = "tab-ContIzquierda"}, New With {.Method = "POST"})

    @<div id="Formulario">
        <h3>Información del Order ID</h3>
        
        <div class="row">
            <span class="Span-b">Test ID</span>
            <span class="Span-b">
                @Html.TextBoxFor(Function(m) m.TestID, New With {.Value = isTested.TestID})
            </span>
        </div>

        <div class="row">
            <span class="Span-b">Test Result</span>
            <span class="Span-b">
                 @Html.DropDownListFor(Function(m) m.Result, OptionList )
            </span>
        </div>
        
        <div class="form-SpArea--dgt">
            <span class="Span-b">Text Log</span>
            <span class="Span-e">
                @Html.TextAreaFor(Function(m) m.TextLog, New With {.rows = 10, .cols = 60})
            </span>
        </div>

        <div class="row">
            <span class="Span-c">&nbsp;</span>
            <span class="Span-e">&nbsp;</span>
            <span class="Span-a"><input type="submit" value="Guardar" class="Button" /></span>
        </div>
    </div>
                    End Using
