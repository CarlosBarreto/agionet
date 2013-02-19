<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>@ViewData("Title")</title>
    <link href='@Url.Content("~/Content/css/headerTool.css")' rel="stylesheet" type="text/css" />
    <link href='@Url.Content("~/Content/css/layout.css")' rel="stylesheet" type="text/css" />
    <link href='@Url.Content("~/Content/css/ui-lightness/jquery-ui-1.9.2.custom.css")' rel="stylesheet" />

    <script src='@Url.Content("~/Scripts/jquery-1.7.1.min.js")' type="text/javascript"></script>
    <script src='@Url.Content("~/Scripts/jquery-ui-1.8.20.min.js")' type="text/javascript"></script>
    <script src='@Url.Content("~/Scripts/modernizr-2.5.3.js")' type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            Resizex();
        });

        $(window).resize(function () {
            Resizex();
        });

        function Resizex() {
            var wdSeparador = $(document).width() - 130
            var Y = $("body").height() - 60;
            var wdContenedor = $("#ContFormulario").width() - 220;

            $("#Separador").css("width", wdSeparador);
            $("#Contenedor").css("height", Y);
            $("#main-ContIzquierda").css("height", "100%");
            $("#main-ContDerecha").css("height", "100%");
            $("#ContFormulario").css("width", wdContenedor);
        }
    </script>

</head>
<body>
    <div id="Header">
        <span class="logo"><img src='@Url.Content("~/Content/images/AgioNetLogoGray.png")' /></span>
        <span id="Separador">
            <Span id="menuPrincipal">
                @Html.Partial("_mainMenuPartial")
            </Span>
            <span class="Logoff">
                @Html.Partial("_LogOnPartial")
            </span>
        </span>
    </div>

    <div id="SubMenu">
         <span class="menu">
            @Html.Partial("_menuPartial")
        </span>
    </div>

    <div id="Contenedor">
        <img src='@Url.Content("~/Content/images/logoaqua.png")' class="ImagenFondo"  />
        <!--<img src="../Content/images/logoaqua.png" class="ImagenFondo"  />-->
        
        <div id="ContMain">
            <section id="main">
                @RenderBody()
            </section>
        </div>
    </div>
        
    <footer>
    </footer>
</body>
</html>