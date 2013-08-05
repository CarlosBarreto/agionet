@ModelType AgioNet.RemplazoComponenteModel
    
@code
    Dim OptionList As List(Of SelectListItem) = New List(Of SelectListItem)
    Dim FailureFormInfo As New AgioNet.FailureFormInfoModel
    FailureFormInfo.getFailureFormInfo(Session("OrderID"))
   
 End Code
<script type="text/javascript">
    $(document).ready(function () {
        $("#Comment").html("");
    });
</script>
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario--dg">Remplazar/Reparar un componente </h2>

@Using Ajax.BeginForm("reparacion", "reparacion", New AjaxOptions With {.UpdateTargetId = "tab-ContIzquierda"}, New With {.Method = "POST"})
    @<div class="Content">
       <div id="Formulario">
        <div class="row">
            <span class="Span-g">Part No</span>
            <span class="Span-c">
                @Html.TextBoxFor(Function(m) m.PartNo, New With {.Value = FailureFormInfo.OldPartNo})
                @Html.HiddenFor(Function(m) m.OrderID, New With {.Value = Session("OrderID")})
            </span>
            <span class="Span-g">Serial No</span>
            <span class="Span-c">
                @Html.TextBoxFor(Function(m) m.SerialNo, New With {.Value = FailureFormInfo.OldSerialNo})
            </span>
        </div>

        <div class="row">        
            <span class="Span-g">Componente</span>
            <span class="Span-c">
                @Html.TextBoxFor(Function(m) m.Componente, New With {.Value = ""})
            </span>
        </div>
        
        <div class="row">
            <span class="Span-b">Failure</span>
            <span class="Span-d">
               @Html.TextBoxFor(Function(m) m.Failure, New With {.Value = FailureFormInfo.Failure})
               <!-- Html.TextBoxFor(Function(m) m.Failure, New With {.Value = ""}) -->
            </span>
        </div>
        
        <div class="row">
            <span class="Span-b">Proceso Aplicado</span>
            <span class="Span-d">
               @Html.TextBoxFor(Function(m) m.Process, New With {.Value = ""})
               <!-- Html.TextBoxFor(Function(m) m.Failure, New With {.Value = ""}) -->
            </span>
        </div>

        <div class="row">
            <span class="Span-b">Comment</span>
            <span class="Span-d">
                @Html.TextAreaFor(Function(m) m.Comment, New With {.Cols = "55", .Rows = "4"})
            </span>
        </div>

        <div class="row">&nbsp;</div>
        <div class="row">&nbsp;</div>
    
        <div class="row">
            <span class="Span-c">&nbsp;</span>
            <span class="Span-a">&nbsp;</span>
            <span class="Span-c">
                <input name="Command" type="submit" value="Terminar" class="Button" />
                &nbsp; &nbsp; &nbsp; &nbsp;
                <input name="Command" type="submit" value="Reemplazar" class="Button" />             
            </span>
        </div>
    </div>
</div>
End Using

<div id="Failures">
    <!-- Html.Action("LoadFoundedFailures") -->
    @Html.Action("LoadReplaceCompHistory")
</div>
