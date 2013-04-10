@Code
    ViewData("Title") = "Impresión de hoja viejera"
    
    Dim OrderID As String = Html.Encode(TempData("OrderID"))
    Dim PrintData As Object = TempData("PrintData")
    TempData.Keep("MsgErr")
End Code

<script type="text/javascript">
    //$(this).ready(function () {
        $(location).attr('href', "@Url.Action("ingresar_orden","recibo")");

        //alert("Pasa por aqui");
        //window.location.href = "@Url.Action("ingresar_orden","recibo")";
    //});
</script>

