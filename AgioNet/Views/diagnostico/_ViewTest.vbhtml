@ModelType AgioNet.TestIDListModel
    
@Code
    Dim isTested As New AgioNet.isTestedModel '= Session("isTested")
    isTested.IsTested(Session("OrderID")) ' = Session("isTested")
    
    Dim test As Object
    test = TempData("TestDetail")
End Code
<script type="text/javascript"> 
    $(document).ready(function () {
        $("#TextLog").html("@Replace(isTested.TextLog, vbNewLine, "\n")");
    }); 
</script>

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Detalles de prueba para la orden [@Session("OrderID")]</h2>
<div id="Formulario">
    <h4>Información de la Prueba</h4>
    <div class="row">
        <span class="Span-c"><strong>TestID:</strong> @test.TESTID </span>
        <span class="Span-c"><strong>OrderID: </strong>@test.ORDERID</span>
    </div>

    <div class="row">
        <span class="Span-e"><strong>Nombre de la prueba: </strong>@test.TESTNAME</span>
    </div>
    <div class="row">
        <span class="Span-e"><strong>Descripción: </strong>@test.TESTDESCRIPTION </span>
    </div>

    <div class="row">
        <span class="Span-c"><strong>Resultado: </strong> @test.TESTRESULT</span>
        <span class="Span-c"><strong>Inicio: </strong>@test.TESTSTART </span>
        <span class="Span-c"><strong>Finalizada: </strong> @test.TESTEND</span>
    </div>

     <div class="form-SpArea--dgt">
        <span class="Span-b"><strong>Test Log: </strong></span>
        <span class="Span-e">
            @Html.TextArea("TextLog", New With {.rows = 10, .cols = 60})
        </span>
    </div>
    
    <div class="row">
        <span class="Span-c"><strong>Creada Por: </strong>@test.CREATEBY</span>
    </div>
</div>




