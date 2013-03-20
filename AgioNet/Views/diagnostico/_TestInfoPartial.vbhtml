@code
    Dim read As AgioNet.TestInfoModel = TempData("Model")
    
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<div class="form-row">
    <span class="Span-c"><strong>TestID: </strong> @read.TestID</span>
</div>


<div class="form-row">
    <span class="Span-e"><strong>OrderID: </strong> @read.OrderID</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Prueba: </strong> @read.TestName</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Descripción: </strong> @read.TestDescription</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Inicio: </strong> @read.TestStart</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Fin: </strong> @read.TestEnd</span>
</div>
       
<div class="form-row">
    <span class="Span-e"><strong>Resultado: </strong> @read.TestResult</span>
</div>

<div class="form-row">
    <span class="Span-e"><strong>Creada por: </strong> @read.CreateBy</span>
</div>

<div class="form-row">&nbsp;</div>
<div class="form-row">&nbsp;</div>