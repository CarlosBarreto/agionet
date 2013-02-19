<ul class="ul_menu">
@Code
    Dim obj As New AgioNet.agLogin
    Dim SubSec = CType(Session("Section"), String)
    Try
        obj.getPermissionList("FUNCTION", User.Identity.Name, SubSec)
    
        Dim Lista As Object = Nothing ' = TempData("Modules")
        Dim SubLista As Object = obj.PermissionList
        Dim Cont As Integer = 0
        Dim StrMenu As String = String.Empty
    
        Lista = obj.PermissionSubList() 'CType(Session("Modules"), String())
        If Not Lista Is Nothing Then
            For Each x As String In Lista
                StrMenu = SubLista(Cont)
            @<text> <li class="ul_menu_li"> @Html.ActionLink(StrMenu, "index", SubSec & "/" & x) </li></text>
                Cont = Cont + 1
            Next
        End If
    Catch ex As Exception
        TempData("ErrMsg") = ex.Message
    Finally
        ' -- Limpiar sesión 
        obj.Dispose()
    End Try
End code
</ul>