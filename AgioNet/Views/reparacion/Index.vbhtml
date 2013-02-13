@ModelType Agionet_v1.RepairMainModel
    
@Code
    ViewData("Title") = "Pagina Principal Reparación"
    Session("Section") = "REPAIR"
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">REPAIR </h2>
@Using Html.BeginForm()
    @<div id="Order_Cont">
        <div id="CIzq">
            <span class="SLabel">OrderID</span>
        </div>
        <div id="CCent">
            <div id="CIzq2">
                <div class="Control">
                    @Html.TextBoxFor(Function(m) m.OrderID)                                  
                    @Html.ValidationMessageFor(Function(m) m.OrderID)
                </div>
            </div>
            <div id="CDer">
                <input type="submit" value="SCAN" class="Button" />
            </div>
        </div>
     </div>
End Using