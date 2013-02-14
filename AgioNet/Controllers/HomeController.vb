Namespace AgioNet
    Public Class HomeController
        Inherits System.Web.Mvc.Controller

        ' Methods
        Public Function About() As ActionResult
            Return Me.View
        End Function

        <Authorize> _
        Public Function Index() As ActionResult
            Me.ViewData.Item("Message") = "Welcome to ASP.NET MVC!"
            Return Me.View
        End Function

    End Class

End Namespace
