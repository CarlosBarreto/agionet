<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>@ViewData("Title")</title>

    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible">
    <!-- CSS Upd By CarlosB on 2013.05.03 -->
    <link href='@Url.Content("~/Content/css/normalize.min.css")' rel="stylesheet" type="text/css" />
    <link href='@Url.Content("~/Content/css/agioTheme/jquery-ui-1.10.3.custom.css")' rel="stylesheet" />
    <link href='@Url.Content("~/Content/css/agionet.css")' rel="stylesheet" type="text/css" />

    <!-- JS Upd By CarlosB on 2013.05.03 -->
    <script src='@Url.Content("~/Scripts/modernizr-2.6.2-respond-1.1.o.min.js")' type="text/javascript"></script>
    <script src='@Url.Content("~/Scripts/jquery-1.7.1.js")' type="text/javascript"></script>
    <script src='@Url.Content("~/Scripts/jquery.validate.min.js")' type="text/javascript"></script>
    <script src='@Url.Content("~/Scripts/messages_es.js")' type="text/javascript"></script>
    <script src='@Url.Content("~/Scripts/jquery-ui-1.10.3.custom.min.js")' type="text/javascript"></script>
</head>
<body>
    <!--[if lt IE 7]>
        <p class="chromeframe">You are using an <strong>outdated</strong> browser. Please <a href="http://browsehappy.com/">upgrade your browser</a> or <a href="http://www.google.com/chromeframe/?redirect=true">activate Google Chrome Frame</a> to improve your experience.</p>
    <![endif]-->
    <header>
        <div id="Header-contenedor">
            <figure id="logo">
                <img src='@Url.Content("~/Content/images/AgioNetLogoGray.png")' />
            </figure>

            <div id="Separador">
                <nav>
                    <div id="menuPrincipal">
                        @Html.Partial("_mainMenuPartial")
                    </div>   
                </nav>
                <nav>
                    <span class="Logoff">
                        @Html.Partial("_LogOnPartial")
                    </span>    
                </nav>
            </div>
        </div>

        <div id="SubMenu">
            <div class="menu">
                @Html.Partial("_menuPartial")
            </div>
        </div>
    </header>

    
    <div id="Contenedor">
        <img src='@Url.Content("~/Content/images/logoaqua.png")' class="ImagenFondo"  />
        <!--<img src="../Content/images/logoaqua.png" class="ImagenFondo"  />-->
        
        <div id="ContMain">
            <section id="main">
                @RenderBody()
            </section>
        </div>
    </div>

    
    <script type="text/javascript">
        $(document).ready(function () {
            $('.menuser ul ul').css({
                position: "absolute",
                top: "30px",
                display: "none"
            });

            $('.menuser li').hover(function () {
                $(this).find('> ul').stop(true, true).slideDown('slow');
            }, function () {
                $(this).find('ul').stop(true, true).slideUp('slow');
            });

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

        function logImplement() {
            alert("Hola");

            
        }
    </script>
</body>
</html>