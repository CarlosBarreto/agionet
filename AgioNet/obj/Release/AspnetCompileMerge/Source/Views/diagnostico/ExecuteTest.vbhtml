@ModelType Agionet.ExecTestModel
    
@Code
    ViewData("Title") = "Diagnostico - Ejecutar pruebas"
    Session("Section") = "diagnostico"
    Dim TestID As String = Html.Encode(TempData("TESTID"))
    
    Dim OprionList As List(Of String) = New List(Of String)
    OprionList.Add("FAIL")
    OprionList.Add("PASS")
End Code

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario"> Ejecutar una prueba</h2>

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

        <div class="Spanraw">
            <span class="Span-b">Test Result</span>
            <span class="Span-b">
                 @Html.DropDownListFor(Function(m) m.Result, New SelectList(OprionList))
            </span>
        </div>
        
        <div class="SpArea">
            <span class="Span-b">Text Log</span>
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
