@ModelType AgioNet.SearchOrderModel

@Code
    ViewData("Title") = "Pagina Principal Reparación"
    Session("Section") = "reparacion"
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
    <h2 class="TituloFormulario--dg">Proceso de Diagnostico</h2>
    <div class="form-Scann ">
        @Using Html.BeginForm()
            @<span class="row">
                <span class="Span-b"><span class="input-labelScann">@Html.LabelFor(Function(m) m.OrderID)</span></span>
                <span class="Span-e"><span class="input-textScann">@Html.TextBoxFor(Function(m) m.OrderID)</span></span>
                <span class="Span-a"> <input type="submit" value="SCAN" class="input-ButtonScann" /></span>
            </span>
        End Using
    </div>
</div>