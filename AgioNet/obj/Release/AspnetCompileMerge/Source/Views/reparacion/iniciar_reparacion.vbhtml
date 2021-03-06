﻿@ModelType Agionet.IniciarReparacionModel
    
@Code
    ViewData("Title") = "Reparación - Iniciar reparación"
    Session("Section") = "reparacion"
    
    Dim Read() As AgioNet.PendingOrdersModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=False)
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
    <h2 class="TituloFormulario--dg">Proceso Iniciar Reparación</h2>
    <div class="form-Scann ">
        @Using Html.BeginForm()
            @<span class="row">
                <span class="Span-b"><span class="input-labelScann">@Html.LabelFor(Function(m) m.OrderID)</span></span>
                <span class="Span-e"><span class="input-textScann">@Html.TextBoxFor(Function(m) m.OrderID)</span></span>
                <span class="Span-a"> <input type="submit" value="SCAN" class="input-ButtonScann" /></span>
            </span>
            @<span class="row"></span>
        End Using
    </div>
</div>

<div id="xx" style="display:block; height: 80%; width:100%; overflow-y:auto; overflow-x:hidden;">
    <h2 class="TituloFormulario--dg">Listado de Ordenes Pendientes de Iniciar Reparación </h2>
    @grid.GetHtml(columns:=grid.Columns( _
        grid.Column("OrderID", "Orden"), _
        grid.Column("ProductType", "Tipo de producto"), _
        grid.Column("SerialNo", "Numero de serie"), _
        grid.Column("Model", "Modelo"), _
        grid.Column("Description", "Descripcion"), _
        grid.Column("DateR", "Fecha de ingreso"), _
        grid.Column("DateComp", "Fecha Compromiso") _
    ))

</div>





