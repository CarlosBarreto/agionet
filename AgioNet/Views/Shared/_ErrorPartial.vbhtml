@Code
    Dim ErrorMessage As String = TempData("ErrMsg")
End Code
<script type="text/javascript">
    $(function () {
        $("#dialog-message").dialog({
            modal: true,
            title: 'AgioNet',
            width: 550,
            buttons: {
                OK: function () {
                    $(this).dialog("close");
                }
            }
        });
    });
</script>

<div id="dialog-message" title="Basic modal dialog">
    <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        @ErrorMessage
    </p>
</div>
