@ModelType AgioNet.AgregarTrackNoModel

@Code
    Dim OrderID As String = Session("OrderID")
    
    ViewData("Title") = "Asignar guias a la orden " & OrderID
    Session("Section") = "logistica"

    'Opciones clase de producto
    Dim opt_CaseType As List(Of String) = New List(Of String)
    opt_CaseType.Add("Seleccionar")
    opt_CaseType.Add("Completo")
    opt_CaseType.Add("Solo Envío")
    opt_CaseType.Add("Solo Recolección")
    
    'Opciones para los transportistas
    Dim opt_Carrier As List(Of String) = New List(Of String)
    opt_Carrier.Add("Seleccionar")
    opt_Carrier.Add("Agiofix")
    opt_Carrier.Add("Cliente")
    opt_Carrier.Add("Estafeta")
    opt_Carrier.Add("MultiPack")
    opt_Carrier.Add("Fedex")
    opt_Carrier.Add("UPS")
    opt_Carrier.Add("DHL")
    opt_Carrier.Add("TPO")
    
End Code
<script type="text/javascript">
    $(document).ready(function () {
        // $("#CaseType").val("Completo");

        $("#CarrierName").change(function () {
            var sel = $("#CarrierName").val();
            if (sel == "Agiofix" || sel == "Cliente") {
                $("#OutBound").val("n/a");
                $("#InBound").val("n/a");
                $("#OutBound2").val("n/a");
                $("#Reference").val("n/a");

                $("#Weight").val("0");
                $("#Height").val("0");
                $("#Lenght").val("0");
                $("#width").val("0");
                $("#Field1").val("0");
                $("#Field2").val("0");  
            }
        });
    });
</script>
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<h2 class="TituloFormulario">Logística - Asignar guías a la orden</h2>

@Using Html.BeginForm()
    @<div id="Formulario">       
        <div class="row">
            <span class="Span-b"><span class="pcenter">OrderID</span></span>
            <span class="Span-b">
                @Html.TextBoxFor(Function(m) m.OrderID, New With {.Value = OrderID})
            </span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="pcenter">Tipo de Caso</span></span>
            <span class="Span-d">
                @Html.DropDownListFor(Function(m) m.CaseType, New SelectList(opt_CaseType))
            </span>
        </div>
        
        <span class="row">
            <span class="Span-b"><span class="pcenter">OutBound</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.OutBound)
            </span>
            <span class="Span-b"><span class="pcenter">InBound</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.InBound)
            </span>
            <span class="Span-b"><span class="pcenter">OutBound2</span></span>
            <span class="Span-b"> 
                @Html.TextBoxFor(Function(m) m.OutBound2)
            </span>
        </span>

        <div class="row">
            <span class="Span-b"><span class="pcenter">Reference</span></span>
            <span class="Span-c">
                @Html.TextBoxFor(Function(m) m.Reference)
            </span>
        </div>

        <h3>Información de especificación de guías</h3>

        <div class="row">
            <span class="Span-b"><span class="pcenter">Transportista</span></span>
            <span class="Span-c">
                @Html.DropDownListFor(Function(m) m.CarrierName, New selectlist(opt_Carrier))
            </span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="pcenter">Peso</span></span>
            <span class="Span-c">
                @Html.TextBoxFor(Function(m) m.Weight)
            </span>

            <span class="Span-b"><span class="pcenter">Altura</span></span>
            <span class="Span-c">
                @Html.TextBoxFor(Function(m) m.Height)
            </span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="pcenter">Longitud</span></span>
            <span class="Span-c">
                @Html.TextBoxFor(Function(m) m.Lenght)
            </span>

            <span class="Span-b"><span class="pcenter">Ancho</span></span>
            <span class="Span-c">
                @Html.TextBoxFor(Function(m) m.width)
            </span>
        </div>

        <div class="row">
            <span class="Span-b"><span class="pcenter">Field1</span></span>
            <span class="Span-c">
                @Html.TextBoxFor(Function(m) m.Field1)
            </span>
            
            <span class="Span-b"><span class="pcenter">Field2</span></span>
            <span class="Span-c"> 
                @Html.TextBoxFor(Function(m) m.Field2)
            </span>
        </div>


        <div class="row">
            <span class="FRaight">
                <input type="submit" value="Guardar" class="Button" />
            </span>
        </div>
    </div>
End Using