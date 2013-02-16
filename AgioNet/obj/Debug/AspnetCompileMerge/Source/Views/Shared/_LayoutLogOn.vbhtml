<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>@ViewData("Title")</title>
    <link href="@Url.Content("~/Content/css/login.css")" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <div id="Contenedor">
        <img src="@Url.Content("~/Content/images/agionet_logo.png")" class="logo" />
        <section id="ContenedorFormulario">
            @RenderBody()
        </section>
        <footer>
        </footer>
    </div>
</body>
</html>
