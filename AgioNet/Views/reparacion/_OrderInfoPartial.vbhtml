@code
    Dim SerialNo As String = Html.Encode(TempData("SerialNo"))
    Dim CustName As String = Html.Encode(TempData("CustName"))
End Code
<div id="Formulario">
    
    <div class="Spanraw">
        <span class="Span-b">Order ID</span>
        <span class="Span-b">
            <span class="InfoLabel">@Session("OrderID")</span>
        </span>
                
        <span class="Span-g">Serial No</span>
        <span class="Span-b">
            <span class="InfoLabel">@SerialNo</span>
        </span>
    </div>

    <div class="Spanraw">
        <span class="Span-b">Customer Name</span>
        <span class="Span-e">
            <span class="InfoLabel">@CustName</span>
        </span>
    </div>
</div>
