@ModelType AgioNet.LogOnModel

@Code
    Layout = "~/Views/Shared/_LayoutLogOn.vbhtml"
    ViewData("Title") = "Log On"
End Code
    
    <script src="@Url.Content("~/Scripts/jquery.validate.min.js")" type="text/javascript"></script>
    <script src="@Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")" type="text/javascript"></script>

@Using Html.BeginForm()
    @<div>
        <div>
            <span class="Cell">@Html.TextBoxFor(Function(m) m.UserName)</span>
            <span class="Cell">@Html.PasswordFor(Function(m) m.Password)</span>
            <span class="Cell2"><input type="submit" value="Log On" class="Button" /></span>
        </div>
        <div>
            <span class="Cell">@Html.Label("Usuario")</span>
            <span class="Cell">@Html.Label("Contraseña")</span>
            <span class="Cell2"></span> 
        </div>
        <span class="err">
            @Html.ValidationSummary(True, "Error, no se ha podido inciar una sesión")
            @If TempData("agLoginErr") <> "" Then
                @Html.ValidationSummary(True, Html.Encode("agLoginErr"))
            End If
        </span>
    </div>
End Using
