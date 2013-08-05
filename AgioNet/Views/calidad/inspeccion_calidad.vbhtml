@ModelType Agionet.InspeccionCalidadModel
    
@Code
   
    Dim OrderID As String = TempData("OrderID")
    
    Dim OptionList As List(Of SelectListItem) = New List(Of SelectListItem)
    Dim _opt As SelectListItem
    
    _opt = New SelectListItem With {.Text = "PASS", .Value = "PASS"}
    OptionList.Add(_opt)
    
    _opt = New SelectListItem With {.Text = "FAIL", .Value = "FAIL"}
    OptionList.Add(_opt)
    
    Dim Read() As AgioNet.datoscalidadmodel = TempData("Model")
    Dim rfail As AgioNet.DatosFallaModel = TempData("Falla")
    
    Dim grid As WebGrid = New WebGrid(Read)
    
End Code
<!-- Ventana modal de error -->
@If TempData("ErrMsg") <> "" Then
    @Html.Partial("_ErrorPartial")
End If

<div id="rp_main">
    <!-- Inicia diseño del formulario -->
    <h2 class="TituloFormulario">Proceso de Calidad</h2>
    <!-- Cuerpo principal del reporte -->
        <!--div id="calidad-contenedor-form" class="calidad-contenedor-form"-->
    <div id="rp_left">
        <span class="title"> Fallas Atendidas Para la Orden</span>
        <div class="form-row">&nbsp; </div>
    @For Each m As AgioNet.DatosCalidadModel In Read
        @<div class="rp_left-contenedor">

            <div class="form-row">
                <span class="Span-c"><strong>Orden: </strong>@m.OrderID </span> 
                <span class="Span-i">&nbsp;</span>
                <span class="Span-c"><strong>Aprobado: </strong>@m.Aprobado </span>
            </div>

            <div class="form-row">
                <span class="Span-i"><strong>PartNo: </strong>@m.PartNumber</span>        
                <span class="Span-e"><strong>Descripción: </strong> @m.Description </span>
            </div>
    
            <div class="form-row">&nbsp;</div>
            <!-- Textos Largos -->
            <strong>Falla: </strong> @m.Failure <br /><br />
            <strong>Solución: </strong> @m.Solution <br /><br />
            <strong>Comentario: </strong> @m.Comentario <br /><br />
            <strong>Retro AC: </strong> @m.RetroAC <br /><br />

            <div class="form-row">
                <span class="Span-c"><strong>Tipo Reparación: </strong> @m.TipoReparacion</span>
                <span class="Span-i">&nbsp;</span>
                <span class="Span-c"><strong>Reparado Por: </strong> @m.ReparadoPor </span>
            </div>

            @If m.Process = "Replace" Then
                @<div class="form-row">
                    <strong>Nueva Parte: </strong> @m.NewPart &nbsp;&nbsp;&nbsp; <strong>Nueva Serie: </strong> @m.NewSerialNo &nbsp;&nbsp;&nbsp;
                    <strong>Parte Anterior: </strong> @m.OldPartNo &nbsp;&nbsp;&nbsp; <strong>Serie Anterior: </strong> @m.OldSerialNo
                </div>       
            Else
                @<div class="form-row">
                    <strong>Numero de Parte: </strong> @m.NewPart &nbsp;&nbsp;&nbsp; <strong>Número de Serie: </strong> @m.NewSerialNo &nbsp;&nbsp;&nbsp;
                    <strong>Componente: </strong> @m.OldPartNo &nbsp;&nbsp;&nbsp; <strong>Falla: </strong> @m.OldSerialNo
                </div> 
            
                @<div class="form-row">
                    <span class="Span-b"><strong>Proceso: </strong> @m.Process</span>
                </div>
            End If 

            <div class="form-row">&nbsp; </div>   
    </div>
    
    @<div class="form-row">&nbsp; </div>   
    Next

        <div class="rp_left-contenedor">
            <!--"inspeccion_calidad", "calidad",  FormMethod.Post, New With {.Id = "myForm"}-->
            @Using Html.BeginForm("inspeccion_calidad", "calidad", FormMethod.Post, New With {.dd = "myForm"})
                @<span class="row">
                    <span class="Span-b"><label class="Label">Orden</label> </span>
                    <span class="Span-j">@Html.TextBoxFor(Function(m) m.OrderID, New With {.Value = OrderID, .class = "text"})</span>        
                </span>
 
                @<div class="row">
                    <span class="Span-b"><label class="Label">Resultado</label></span>
                    <span class="Span-b">@Html.DropDownListFor(Function(m) m.Result, OptionList, New With {.class="text"})
                    </span>
                 </div>
    
                @<span class="row-textarea">
                    <span class="Span-b"> <label class="Label">Comentario</label> </span>
                    <span class="Span-e">@Html.TextAreaFor(Function(m) m.Comment, New With {.rows = 4, .cols = 60})</span>
                </span>
                
                @<div class="row"> &nbsp; </div>
                @<div class="row"> Confirmo que he Comprobado la información de Calidad @Html.CheckBoxfor(function(m) m.Acepto) </div>
                @<div class="row">
                    <span class="Span-b">&nbsp;</span>
                    <span class="Span-b">&nbsp;</span>
                    <span class="Span-b">&nbsp;</span>
                    <span class="Span-a"> <input type="submit" value="Procesar" class="Button" /></span>
                 </div>
            End Using
        </div>
    </div>

    <!--  id="calidad-contenedor-datos" class="calidad-contenedor-datos" -->
    <div  id="rp_right">
        <strong>Comentario Cierre Reparación:</strong> @rfail.ComentarioCierre <br /><br />
        <strong>Falla Reportada: </strong> @rfail.Falla <br /><br />
        <strong>Retroalimentación: </strong> @rfail.Retro <br /><br />
        <strong>Retroalimentación Atención a clientes: </strong> @rfail.Comment <br /><br />
        <strong>Comentario del Técnico:</strong> @rfail.TComment<br /><br />
    </div>
</div>

<!--script type="text/javascript">
    $().ready(function () {
        $("#myForm").validate({
            debug: true,
            rules: {
                Acepto: { required: true }
            }
        });
    });
</!--script-->
<!-- <div class="row"> &nbsp; </div>
<h2 class="TituloFormulario">Fallas Detectadas Para la Orden</h2>
                OrderID: { required: true },
grid.GetHtml(columns:=grid.Columns( _
    grid.Column("OrderID", "Orden"), _
    grid.Column("PartNumber", "Numero de Parte"), _
    grid.Column("Description", "Descripción"), _
    grid.Column("Aprobado", "Aprobado"), _
    grid.Column("RetroAC", "RetroAC"), _
    grid.Column("Failure", "Falla"), _
    grid.Column("Solution", "Solución"), _
    grid.Column("TipoReparacion", "Tipo Reparación"), _
    grid.Column("Comentario", "Comentario Reparacion"), _
    grid.Column("ReparadoPor", "Detectado Por") _
))
    -->
