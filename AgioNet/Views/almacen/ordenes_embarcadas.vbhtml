@ModelType Agionet.OrdenesEmbarcadasModel
    
@Code
    ViewData("Title") = "Almacen - Ordenes Embarcadas"
    Session("Section") = "almacen"
    
    Dim Read() As AgioNet.OrdenesEmbarcadasModel = TempData("Model")

    Dim grid As WebGrid = New WebGrid(Read, canPage:=False)
    Dim date1 As String = Format(Now, "dd/MM/yyyy")
    Dim date2 As String = Format(DateAdd(DateInterval.Day, 1, Now), "dd/MM/yyyy")
End Code

<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<div id="xx" style="display:block; height: 80%; width:100%; overflow-y:auto; overflow-x:hidden;">
<h2 class="TituloFormulario">Listado de Ordenes Embarcadas</h2>
<div id="Formulario">       
    <div class="row">
        @Using Html.BeginForm("ordenes_embarcadas", "almacen", FormMethod.Get) '"material_master", "ingenieria", FormMethod.Get, new AjaxOptions { .InsertionMode=InsertionMode.Replace, .UpdateTargetId="resultados" } )
            @<span class="Span-a"><span class="pcenter">Desde</span></span>
            @<span class="Span-i">@Html.TextBoxFor(Function(m) m.dateStart, New With {.Value = date1})</span>
            @<span class="Span-a"><span class="pcenter">Hasta</span></span>
            @<span class="Span-i">@Html.TextBoxFor(Function(m) m.dateEnd, New With {.Value = date2})</span>

            @<span class="Span-a"><span class="pcenter">Buscar Por Orden: </span></span>
            @<span class="Span-i">@Html.TextBoxFor(Function(m) m.OrderID)</span>
            @<span class="Span-a"><span class="pcenter">Buscar Por Guía: </span></span>
            @<span class="Span-i">@Html.TextBoxFor(Function(m) m.TrackNo)</span>
            @<span class="Span-a">
                <span class="FRaight"> <input type="submit" value="Buscar" class="Button" /> </span>
             </span>
        End Using
    </div>        
    <div class="row"> &nbsp; </div>
</div>
    @grid.GetHtml(columns:=grid.Columns( _
            grid.Column("OrderID", "Orden"), _
            grid.Column("TrackNo", "Guía"), _
            grid.Column("Comment", "Comentario"), _
            grid.Column("ShipDate", "Fecha de Embarque"), _
            grid.Column("ShipBy", "Embarcado Por") _
    ))

</div>
 
<script type="text/javascript">
    $(document).ready(function() {
        $("#StartDate").datepicker({ dateFormat: "dd/mm/yy" });
        $("#EndDate").datepicker({ dateFormat: "dd/mm/yy" });
    });
</script>