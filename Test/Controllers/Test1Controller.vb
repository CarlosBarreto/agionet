Namespace Test
    Public Class Test1Controller
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Test1

        Function Index() As ActionResult
            Return View()
        End Function

        '
        ' GET: /Test1/Details/5

        Function Details(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        '
        ' GET: /Test1/Create

        Function Create() As ActionResult
            Return View()
        End Function

        '
        ' POST: /Test1/Create

        <HttpPost> _
        Function Create(ByVal collection As FormCollection) As ActionResult
            Try
                ' TODO: Add insert logic here
                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function
        
        '
        ' GET: /Test1/Edit/5

        Function Edit(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        '
        ' POST: /Test1/Edit/5

        <HttpPost> _
        Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                ' TODO: Add update logic here

                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        '
        ' GET: /Test1/Delete/5

        Function Delete(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        '
        ' POST: /Test1/Delete/5

        <HttpPost> _
        Function Delete(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                ' TODO: Add delete logic here

                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function     
    End Class
End Namespace
