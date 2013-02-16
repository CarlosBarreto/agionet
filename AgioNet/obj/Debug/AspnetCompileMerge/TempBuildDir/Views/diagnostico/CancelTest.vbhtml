@ModelType Agionet.ExecTestModel
    
@Code
    ViewData("Title") = "Cancel Test"
    Session("Section") = "diagnostico"
    Dim TestID As String = Html.Encode(TempData("TESTID"))
    
End Code

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">DIAGNOSTIC - Cancelar una prueba</h2>

@Html.Action("LoadWnOrderInfo")

@Using Html.BeginForm()
    @<div id="Formulario">
        <h3>Información del Order ID</h3>
        
        <div class="Spanraw">
            <span class="Span-b">Test ID</span>
            <span class="Span-b">
                @Html.TextBoxFor(Function(m) m.TestID, New With {.Value = TestID})

            </span>
        </div>
        
        <div class="SpArea">
            <span class="Span-b">Comentarios</span>
            <span class="Span-e">
                @Html.TextAreaFor(Function(m) m.TextLog)
            </span>
        </div>

        <div class="Spanraw">
            <span class="FRaight">
                <input type="submit" value="Guardar" class="Button" />
            </span>
        </div>
    </div>
End Using
