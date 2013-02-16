@ModelType AgioNet.TrackNoCostListModel

@Code
    ViewData("Title") = "Logistica - Modificar los TrackNo"
    Session("Section") = "logistica"
    
    Dim Read() As AgioNet.AgregarTrackNoModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Listado de TrackNo para modificar completar</h2>

@grid.GetHtml(columns:=grid.Columns( _
                   grid.Column("OrderID", "Orden"), _
                   grid.Column("CaseType", "Tipo de caso"), _
                   grid.Column("OutBound", "OutBound"), _
                   grid.Column("InBound", "InBound"), _
                   grid.Column("OutBound2", "OutBound2"), _
                   grid.Column("CarrierName", "Transportista"), _
                   grid.Column("Weight", "Peso"), _
                   grid.Column("Height", "Alto"), _
                   grid.Column("Lenght", "Longitud"), _
                   grid.Column("Width", "Ancho"), _
                   grid.Column("Field1", "Adicional1"), _
                   grid.Column("Field2", "Adicional2"), _
                   grid.Column("Reference", "Referencia"), _
                   grid.Column(format:=Function(item) Html.ActionLink("Modificar TrackNo", "modificarguia", "logistica", New With {.OrderID = item.OrderID}, vbNull)) _
))
