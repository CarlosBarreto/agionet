@Code
    Dim OrderID As String = Html.Encode(Session("OrderID"))
    Dim DiagnosticID As String = Html.Encode(Session("DiagnosticID"))
    Dim OtherTitle As String = Html.Encode(Session("OtherTitle"))
    Dim OtherContent As String = Html.Encode(Session("OtherContent"))
    
End Code
<script type="text/javascript">
    $(document).ready(function () {
        var wdContenedor = $("#main").width() - 200;
        $("#ContFormulario").css("width", wdContenedor);
    });

    $(".Closebt").click(function () {
        alert("Se va a cerrar");
        $("#OrderInfo").css("visibility", "hidden");
    });

    function close() {
        $("#OrderInfo").css("visibility", "hidden");
    }
</script>

<div id="OrderInfo">
    <span class="OrderInfo_Title">Información de la orden <a href="javascript:close(); return false" class="OrderInfo_Closebt">X</a></span>
    <span class="Content">
        <span class="OrderInfo_bold">OrderID: </span> @OrderID <br />
        @If DiagnosticID <> "" And Not DiagnosticID Is Nothing Then
            @<span class="OrderInfo_bold">Diagnostic</span> 
            @DiagnosticID
            @<br />
        End If
        @If OtherTitle <> "" And Not OtherTitle Is Nothing Then
            @<span class="OrderInfo_bold">@OtherTitle</span>
            @OtherContent
            @<br />
        End If
    </span>
</div>