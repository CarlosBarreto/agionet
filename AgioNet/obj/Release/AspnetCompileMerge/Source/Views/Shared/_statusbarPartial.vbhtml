@Code
    Dim Message As String = TempData("StatusMsg")
End Code

<div id="StatusBar" title="Status Bar">
    <span class="statusbar-design">
        @Message
    </span>
</div>