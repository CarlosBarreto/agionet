@ModelType Agionet.RegFailureModel
    
@Code
    ViewData("Title") = "Registrar Falla"
    Session("Section") = "diagnostico"
    Dim TestID As String = Html.Encode(TempData("TESTID"))
    
End Code

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">DIAGNOSTIC - Registrar Una falla</h2>

@Html.Action("LoadWnOrderInfo")

@Using Html.BeginForm()
    @<div id="Formulario">
        <h3>Información del Order ID</h3>
        
        <div class="Spanraw">
            <span class="Span-b">Test ID</span>
            <span class="Span-b">
                @Html.TextBoxFor(Function(m) m.TestID, New With {.Value = TestID})
                @Html.HiddenFor(Function(m) m.OrderID, New With {.Value = Session("OrderID")})
            </span>
        </div>

        <div class="Spanraw">
            <span class="Span-b">Possible Solution</span>
            <span class="Span-e">@Html.TextBoxFor(Function(m) m.PSolution)</span>
        </div>
        
        <div class="SpArea">
            <span class="Span-b">Failure Description</span>
            <span class="Span-e">
                 @Html.TextAreaFor(Function(m) m.Failure)
            </span>
        </div>
        
        <div class="Spanraw">
            <span class="Span-b">Found By</span>
            <span class="Span-b">@Html.TextBoxFor(Function(m) m.User, New With {.Value = User.Identity.Name}) </span>
        </div>

        <div class="Spanraw">
            <span class="FRaight">
                <input type="submit" value="Guardar" class="Button" />
            </span>
        </div>
    </div>
End Using
