@ModelType AgioNet.TestIDListModel
    
@Code
    ViewData("Title") = "Diagnostico - Ver prueba"
    Session("Section") = "diagnostico"
    Dim test As Object
    test = TempData("TestDetail")
End Code

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">OM-VIEW ORDER</h2>
<p class="InfoFormulario">
    Ver la información de la orden.
</p>
<div id="Formulario">
    <h4>Información de la Prueba</h4>
    <div class="Spanraw">
        <span class="Span-b">Test ID</span>
        <span class="Span-c">
            <span class="InfoLabel">@test.TESTID </span>
        </span>

        <span class="Span-b">Order ID</span>
        <span class="Span-c">
            <span class="InfoLabel">@test.ORDERID</span>
        </span>
    </div>

    <div class="Spanraw">
        <span class="Span-b">Test Name</span>
        <span class="Span-c">
            <span class="InfoLabel">@test.TESTNAME</span>
        </span>

        <span class="Span-b">Test description</span>
        <span class="Span-e">
            <span class="InfoLabel">@test.TESTDESCRIPTION</span>
        </span>
    </div>

    <div class="Spanraw">
        <span class="Span-b">Test Result</span>
        <span class="Span-b">
            <span class="InfoLabel">@test.TESTRESULT</span>
        </span>

        <span class="Span-b">Test Start</span>
        <span class="Span-b">
            <span class="InfoLabel">@test.TESTSTART</span>
        </span>

        <span class="Span-b">Test End</span>
        <span class="Span-b">
            <span class="InfoLabel">@test.TESTEND</span>
        </span>
    </div>

    <div class="SpArea">
        <span class="Span-b">Text Log</span>
        <span class="Span-j">
            <span class="InfoArea">@test.TEXTLOG</span>
        </span>
    </div>

    <div class="Spanraw">
        <span class="Span-b"> Create By</span>
        <span class="Span-b">
            <span class="InfoLabel">@test.CREATEBY</span>
        </span>

        @Using Html.BeginForm()
        @<div id="CDer">
            <input type="submit" value="SCAN" class="Button" />
        </div>
        End Using
    </div>
</div>



