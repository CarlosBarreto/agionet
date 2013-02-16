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
    });
</script>
@Code
    If Request.IsAuthenticated Then
        @<div class="menuser">
            <ul>
                <li>Bienvenido @User.Identity.Name !
                    <ul>
                        <li>@Html.ActionLink("Cerrar Sesión", "LogOff", "Account") </li>
                        <li>@Html.ActionLink("Cambiar Contraseña", "ChangePassword", "Account") </li>
                    </ul>
                </li>
            </ul>
         </div>
    Else
        @:[ @Html.ActionLink("Iniciar Sesión", "LogOn", "Account") ]
    End If
End Code