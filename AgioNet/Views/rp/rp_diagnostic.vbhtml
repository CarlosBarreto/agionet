@code
    Dim mi As AgioNet.rp_DiagnosticModel = TempData("diagnostic")
    Dim mi2() As AgioNet.rp_TestListModel = TempData("testList")
    Dim mi3() As AgioNet.rp_FailureListModel = TempData("failureList")
    Dim mi4 As AgioNet.rp_dgCloseModel = TempData("dgClose")
End Code

<div class="rp_left-contenedor">
    <span class="title"> Información del Diagnostico de la Orden</span>

    <div class="form-row">&nbsp; </div>
    <div class="form-row">
        <span class="Span-b"><strong>Orden: </strong></span>
        <span class="Span-c">@mi.OrderID </span> 
    </div>
    
    <div class="form-row">
        <span class="Span-a"><strong>Iniciado</strong></span>
        <span class="Span-h">@mi.StartDate</span>
        <span class="Span-a"><strong>Terminado</strong></span>
        <span class="Span-h">@mi.EndDate</span>
        <span class="Span-a"><strong>Creado Por:</strong></span>
        <span class="Span-h">@mi.CreateBy</span>
    </div>

    <div class="form-row">
        <span class="Span-b"><strong>Comentarios</strong></span>
        <span class="Span-j">@mi.Comments</span>
   
    </div>

    <div class="form-row">&nbsp; </div>
    <div class="form-row">&nbsp; </div>
</div>

<div class="form-row">&nbsp; </div>

<div class="rp_left-contenedor">
    <span class="title"> Pruebas Realizadas a la Orden</span>
    <div class="form-row">&nbsp; </div>

    @For Each m As AgioNet.rp_TestListModel In mi2
        @<div class="form-row">
            <span class="Span-b"><strong>Orden: </strong></span>
            <span class="Span-c">@m.OrderID </span> 
            <span class="Span-b"><strong>ID de la Prueba: </strong></span>
            <span class="Span-c">@m.TestID </span> 
        </div>
        
        @<div class="form-row">
            <span class="Span-b"><strong>Nombre: </strong></span>
            <span class="Span-b">@m.TestName </span> 
            <span class="Span-b"><strong>Descripción: </strong></span>
            <span class="Span-d">@m.TestDescription </span>
        </div>
        
         @<div class="form-row">
            <span class="Span-a"><strong>Resultado: </strong></span>
            <span class="Span-h">@m.TestResult </span> 
            <span class="Span-a"><strong>Inicio: </strong></span>
            <span class="Span-h">@m.TestStart </span>
            <span class="Span-a"><strong>Fin: </strong></span>
            <span class="Span-h">@m.TestEnd </span>
        </div>
        
        @<div class="form-row">
            <span class="Span-b"><strong>Creado Por: </strong></span>
            <span class="Span-b">@m.CreateBy </span> 
            <span class="Span-b"><strong>Fecha Log: </strong></span>
            <span class="Span-d">@m.LogDate </span>
        </div>
        
        @<div class="form-row">&nbsp; </div>
       @<div class="form">
            <span class="Span-b"><strong>Log: </strong></span>
            <span class="Span-k" style="display:block; height:100px; overflow-y:auto;">@m.LogText </span> 
        </div>
        
        @<div class="form-row">&nbsp; </div>
        @<div class="form-row">&nbsp; </div>
    Next

</div>

<div class="form-row">&nbsp; </div>

<div class="rp_left-contenedor">
    <span class="title"> Fallas Encontradas en la Orden</span>
    <div class="form-row">&nbsp; </div>

    @For Each mx As AgioNet.rp_FailureListModel In mi3
        @<div class="form-row">
            <span class="Span-b"><strong>Orden: </strong></span>
            <span class="Span-c">@mx.OrderID </span> 
            <span class="Span-b"><strong>ID Falla: </strong></span>
            <span class="Span-c">@mx.FailureID </span> 
        </div>
        
        @<div class="form-row">
            <span class="Span-b"><strong>Reparación: </strong></span>
            <span class="Span-c">@mx.TipoReparacion </span> 
        </div>
        
        @<div class="form-row">&nbsp;</div>
        @<strong>Falla: </strong> @mx.Failure @<br />@<br /> 
        
        
        @<strong>Solución: </strong> @mx.Solution @<br />@<br /> 
        
        @<strong>Fuente Suministro: </strong> @mx.Source @<br />@<br /> 
  
        @<strong>Comentario: </strong> @mx.Comment @<br />@<br /> 
        
        @<div class="form-row">
            <span class="Span-b"><strong>Detectada Por: </strong></span>
            <span class="Span-c">@mx.FoundBy </span> 
            <span class="Span-b"><strong>Detectada El: </strong></span>
            <span class="Span-c">@mx.FoundDate </span> 
        </div>
        
        @<div class="form-row">&nbsp; </div>
        @<div class="form-row">&nbsp; </div>
    Next

</div>

<div class="form-row">&nbsp; </div>

<div class="rp_left-contenedor">
    <span class="title"> Información del Cierre de Diagnostico de la Orden</span>

    <div class="form-row">&nbsp; </div>
    <div class="form-row">
        <span class="Span-b"><strong>Orden: </strong></span>
        <span class="Span-c">@mi4.OrderID </span> 
    </div>
    <div class="form-row">&nbsp; </div>
    
    <strong>Retroalimentación: </strong> @mi4.Retro <br /><br /> 
    
    <strong>Comentario Tecnico: </strong> @mi4.TComment <br /><br /> 
    
    <strong>Comentarios: </strong> @mi4.Comments <br /><br /> 
   
    <div class="form-row">&nbsp; </div>
    <div class="form-row">
        <span class="Span-b"><strong>Cerrado Por: </strong></span>
        <span class="Span-c">@mi4.RequestBy </span> 
        <span class="Span-b"><strong>Cerrado El : </strong></span>
        <span class="Span-c">@mi4.RequestDate </span> 
    </div>
    <div class="form-row">&nbsp; </div>
    <div class="form-row">&nbsp; </div>
</div>
