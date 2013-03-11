Imports System.Data.SqlClient

Namespace AgioNet
    Public Class facturacionController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /facturacion

        Function Index() As ActionResult
            Return View()
        End Function

        ' 2013.03.10 
        ' GET: /facturacion/OnBilling
        <Authorize> _
        Public Function OnBilling() As ActionResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As OnBillingModel
            Dim index As Integer = 0
            Try
                DR = DA.ExecuteSP("bl_OnBillling")
                If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                    Do While Me.DR.Read
                        modelArray(index) = New OnBillingModel With {.OrderID = DR(0), .PartNumber = DR(1), .SerialNumber = DR(2), _
                                                                           .FailureType = DR(3), .Costo = DR(4), .LeadTime = DR(5)}
                        If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ReDim Preserve modelArray(index - 1)
                Else
                    Throw New Exception(DA._LastErrorMessage)
                End If

                '-----
                TempData("Model") = modelArray
            Catch ex As Exception
                TempData("ErrMsg") = ex.Message
                Return RedirectToAction("Index")
            Finally
                DA.Dispose()
            End Try

            Return Me.View
        End Function

        ' 2013.03.11
        ' GET: /facturacion/invoiced
        <Authorize> _
        Public Function invoiced() As ActionResult
            Return Me.View
        End Function

        ' 2013.03.11
        ' POST: /facturacion/OnBilling
        <Authorize, HttpPost> _
        Public Function invoiced(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("bl_invoiced", model.OrderID, model.Comment, Me.User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Return Me.RedirectToAction("invoiced")
        End Function

        ' 2013.03.11
        ' GET: /facturacion/paid
        <Authorize> _
        Public Function paid() As ActionResult
            Return Me.View
        End Function

        ' 2013.03.11 
        ' POST: /facturacion/paid
        <Authorize, HttpPost> _
        Public Function paid(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("bl_paid ", model.OrderID, model.Comment, Me.User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Return Me.RedirectToAction("paid")
        End Function

        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader
    End Class
End Namespace
