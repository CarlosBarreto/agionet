Namespace AgioNet
    Public Class AccountController
        Inherits System.Web.Mvc.Controller

        ' 2013.02.13
        ' GET: /Account/ChangePassword
        <Authorize()> _
        Public Function ChangePassword() As ActionResult
            Return View()
        End Function

        '2013.02.13
        ' POST: /Account/ChangePassword
        <Authorize()> _
        <HttpPost()> _
        Public Function ChangePassword(ByVal model As ChangePasswordModel) As ActionResult
            If ModelState.IsValid Then
                Dim login As New agLogin
                login.ChangePassword(model.OldPassword, model.NewPassword, model.ConfirmPassword)
                If login._LastErrorMessage = "" Then
                    login.Dispose()
                    Return RedirectToAction("ChangePasswordSuccess")
                End If

                ModelState.AddModelError("", login.LastErrorMessage)
                login.Dispose()
            End If

            Return View(model)
        End Function

        ' 2013.02.13
        ' GET: /Account/ChangePasswordSuccess
        <Authorize()> _
        Public Function ChangePasswordSuccess()
            Return View()
        End Function

        ' 2013.02.13
        ' GET: /Account/LogOff
        <Authorize()> _
        Public Function LogOff() As ActionResult
            FormsAuthentication.SignOut()
            Return Me.RedirectToAction("Index", "Home")
        End Function

        ' 2013.02.13
        ' GET: /Account/LogOn
        Public Function LogOn() As ActionResult
            Return Me.View
        End Function

        ' 2013.02.13
        ' POST: /Account/LogOn
        <HttpPost> _
        Public Function LogOn(ByVal model As LogOnModel, ByVal returnUrl As String) As ActionResult
            If Me.ModelState.IsValid Then
                Dim login As New agLogin
                Dim userName As String = login.LogOn(model.UserName, model.Password)
                If Not login.ErrorFlag Then
                    FormsAuthentication.SetAuthCookie(userName, False)
                    login.Dispose()
                    Return Me.RedirectToAction("Index", "Home")
                End If
                Me.TempData.Item("agLoginErr") = login._LastErrorMessage
            End If
            Return Me.View(model)
        End Function

    End Class
End Namespace
