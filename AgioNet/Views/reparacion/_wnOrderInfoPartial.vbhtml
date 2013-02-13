@Code
    Dim OrderID As String = Html.Encode(Session("OrderID"))
    Dim DiagnosticID As String = Html.Encode(Session("DiagnosticID"))
    Dim OtherTitle As String = Html.Encode(Session("OtherTitle"))
    Dim OtherContent As String = Html.Encode(Session("OtherContent"))
    
End Code
<script type="text/javascript">
    $(".Closebt").click(function () {
        alert("Se va a cerrar");
        $("#OrderInfo").css("visibility", "hidden");
    });

    function close() {
        $("#OrderInfo").css("visibility", "hidden");
    }
</script>

<div id="OrderInfo">
    <span class="Title">Información de la orden <a href="javascript:close(); return false" class="Closebt">X</a></span>
    <span class="Content">
        <span class="bold">OrderID: </span> @OrderID <br />
        @If DiagnosticID <> "" And Not DiagnosticID Is Nothing Then
            @<span class="bold">Diagnostic</span> 
            @DiagnosticID
            @<br />
        End If
        @If OtherTitle <> "" And Not OtherTitle Is Nothing Then
            @<span class="bold">@OtherTitle</span>
            @OtherContent
            @<br />
        End If
    </span>
</div>