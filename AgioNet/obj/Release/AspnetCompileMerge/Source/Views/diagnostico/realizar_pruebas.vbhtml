﻿@Modeltype AgioNet.ScanOrderModel
    
@Code
    ViewData("Title") = "Diagnostico - Asignar a una estación"
    Session("Section") = "diagnostico"
        
    Dim Read() As AgioNet.TestByOrderIDModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=True, rowsPerPage:=20)
End Code
<!-- Agregar el CSS para recibo --> 
<link href='@Url.Content("~/Content/css/diag.css")' rel="stylesheet" type="text/css" />

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<!-- Aquí se muestran los formularios -->
<div id="main-ContIzquierda">
    <!-- Inicia diseño del formulario -->
    <h2 class="TituloFormulario--dg">Diagnostico - Pruebas disponibles</h2>
    @grid.GetHtml(columns:=grid.Columns( _
        grid.Column("OrderID", "Orden", style:="TestID_"), _
        grid.Column("TestName", "Prueba", style:="OrderID_"), _
        grid.Column("TestResult", "Resultado", style:="TestName_"), _
        grid.Column("Failure", "Falla", style:="TestDescription_"), _
        grid.Column("TextLog", "Text Log", style:="CreateBy_"), _
        grid.Column("TestStart", "Inicio", style:="CreateBy_"), _
        grid.Column("TestEnd", "Final", style:="CreateBy_"), _
        grid.Column("CreateBy", "Creada por", style:="CreateBy_"), _
        grid.Column(format:=Function(item) Html.ActionLink("Realizar", "pruebasMaster", "diagnostico", New With {.OrderID = item.OrderID}, vbNull), style:="Link_") _
    ))
       

        <div class="row">&nbsp;</div>
        <div class="form-aclaracion">
            <span class="form-aclaracion_Title">Nota:</span>
            <span class="form-aclaracion_Text">
                En caso de de que la <strong>Prueba</strong>, <strong>Modelo</strong>, el <strong>Número de parte</strong>, o la 
                <strong>Marca</strong> del producto que necesitas no estén disponibles en este listado, Favor de contactar a ingeniería 
                (Encargado, <a href="mailto:humberto.vega@agiotech.com">Humberto Vega</a> )
         </span>
    </div>
</div>

<!-- Aquí va la información de la orden -->
<div id="main-ContDerecha" class="bg-fondoborder">
    @Html.Action("LoadOrderInfo")
</div>



