@ModelType AgioNet.OrderListModel

@Code
    ViewData("Title") = "Servicio a clientes - Pendientes de Autorización"
    Session("Section") = "sc"
    
    Dim Read() As AgioNet.AprobarRepararModel = TempData("Model")
    
    Dim grid As WebGrid = New WebGrid(Read)
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If
<!-- Inicia diseño del formulario -->
<h2 class="TituloFormulario">Autorizaciones para la orden [ @Session("OrderID") ]</h2>

@grid.GetHtml(columns:=grid.Columns( _
                   grid.Column("OrderID", "Orden"), _
                   grid.Column("PartNumber", "Numero de Parte"), _
                   grid.Column("Description", "Descripción"), _
                   grid.Column("Failure", "Falla"), _
                   grid.Column("Solution", "Solución"), _
                   grid.Column("Comentario", "Comentario Compras"), _
                   grid.Column("TipoReparacion", "Tipo Reparación"), _
                   grid.Column("FoundBy", "Detectado Por"), _
                   grid.Column("Aprobado", "Aprobado"), _
                   grid.Column(format:=Function(item) If(item.Aprobado <>  "" Or item.Aprobado = "FALSE", Html.ActionLink("Autorización", "approvalRepair", "sc", New With {.OrderID = item.OrderID}, vbNull), ""), style:="Link_") _
))
