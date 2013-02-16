@ModelType Agionet.PendingOrdersModel
    
@Code
    ViewData("Title") = "Recibo - Impresión de hoja viajera"
    Session("Section") = "recibo"
    
    Dim Read() As AgioNet.PendingOrdersModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read)
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<h2 class="TituloFormulario">Impresión de hoja viajera</h2>

<div id="ContenedorOrderIDForm">
@Using Html.BeginForm()
    @<span class="row">
        <span class="Span-b">@Html.LabelFor(Function(m) m.OrderID) </span>
        <span class="Span-j">@Html.TextBoxFor(Function(m) m.OrderID, New With {.class = "text"})</span>
        <span class="Span-a"> <input type="submit" value="SCAN" class="Button" /></span>
    </span>
End Using
</div>