   
@Code
    ViewData("Title") = "DIAGNOSTIC - TESTS / View Test"
    Session("Section") = "DIAGNOSTIC"
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
        <span class="Span-b">Failure ID</span>
        <span class="Span-b">
            <span class="InfoLabel">@test.FAILUREID </span>
        </span>

        <span class="Span-b">Test ID</span>
        <span class="Span-b">
            <span class="InfoLabel">@test.TESTID</span>
        </span>
    </div>

    <div class="Spanraw">
        <span class="Span-b">Failure Description</span>
        <span class="Span-e">
            <span class="InfoLabel">@test.DESCRIPTION</span>
        </span>
    </div>

    <div class="Spanraw">
        <span class="Span-b">Posible Solution</span>
        <span class="Span-e">
            <span class="InfoLabel">@test.POSSIBLESOLUTION</span>
        </span>
    </div>

    <div class="Spanraw">
        <span class="Span-b">Found By</span>
        <span class="Span-b">
            <span class="InfoLabel">@test.FOUNDBY</span>
        </span>

        <span class="Span-b">Found Date</span>
        <span class="Span-c">
            <span class="InfoLabel">@test.FOUNDDATE</span>
        </span>
    </div>

    <div class="Spanraw">
        <span class="Span-b">Resolved</span>
        <span class="Span-b">
            <span class="InfoLabel">@test.RESOLVED</span>
        </span>
    </div>

    <div class="Spanraw">

        @Using Html.BeginForm()
        @<div id="CDer">
            <input type="submit" value="Return" class="Button" />
        </div>
        End Using
    </div>
</div>



