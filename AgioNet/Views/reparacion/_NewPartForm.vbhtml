@ModelType AgioNet.AddNewPartModel
    
@code
    Dim OptionList As List(Of SelectListItem) = New List(Of SelectListItem)
    Dim FailureFormInfo As New AgioNet.FailureFormInfoModel
    FailureFormInfo.getFailureFormInfo(Session("OrderID"))
   
    Dim _optFail As New SelectListItem
    _optFail.Text = FailureFormInfo.Failure
    _optFail.Value = "FAIL"
    _optFail.Selected = True
    OptionList.Add(_optFail)
    
    Dim _optPP As New SelectListItem()
    _optPP.Text = "Agregar Nueva Parte"
    _optPP.Value = "NEW"
    OptionList.Add(_optPP)
     
 End Code

<script type="text/javascript">
    $(document).ready(function () {
        var data = "@FailureFormInfo.CaseType";
        if (data == "TRUE") {
            $("#Comment").html("@Replace(FailureFormInfo.Comment, vbNewLine, "\n")");
            $(".TituloFormulario--dg").html("Se ha realizado el remplazo para la Orden [@FailureFormInfo.OrderID]");
            $(".Button").css("visibility", "hidden");
        }
    });
</script>
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario--dg">Agregar una nueva una parte / Completar</h2>

@Using Ajax.BeginForm("reparacion", "reparacion", New AjaxOptions With {.UpdateTargetId = "tab-ContIzquierda"}, New With {.Method = "POST"})
    @<div class="Content">
       <div id="Formulario">
        <div class="row">
            <span class="Span-g">New Part No</span>
            <span class="Span-c">
                @Html.TextBoxFor(Function(m) m.PartNo, New With {.Value = FailureFormInfo.PartNo})
                @Html.HiddenFor(Function(m) m.OrderID, New With {.Value = Session("OrderID")})
            </span>
        </div>

        <div class="row">
            <span class="Span-g">New Serial No</span>
            <span class="Span-c">
                @Html.TextBoxFor(Function(m) m.SerialNo, New With {.Value = FailureFormInfo.SerialNo})
            </span>
        </div>
        
        <div class="row">
            <span class="Span-b">Failure</span>
            <span class="Span-d">
               @Html.DropDownListFor(Function(m) m.Failure, OptionList)
               <!-- Html.TextBoxFor(Function(m) m.Failure, New With {.Value = ""}) -->
            </span>
        </div>

        <div class="row">
            <span class="Span-b">Comment</span>
            <span class="Span-d">
                @Html.TextAreaFor(Function(m) m.Comment, New With {.Cols = "55", .Rows = "4" })
            </span>
        </div>

        <div class="row">&nbsp;</div>
        <div class="row">&nbsp;</div>
    
        <div class="row">
            <span class="Span-c">&nbsp;</span>
            <span class="Span-c">&nbsp;</span>
            <span class="Span-b">&nbsp;</span>
            <span class="Span-a">
                <input type="submit" value="SCAN" class="Button" />             
            </span>
        </div>
    </div>
</div>
End Using

<div id="Failures">
    <!-- Html.Action("LoadFoundedFailures") -->
    @Html.Action("LoadReplaceHistory")
</div>
