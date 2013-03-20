Imports System.Data.SqlClient

Namespace AgioNet
    Public Class empaqueController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /empaque

        Function Index() As ActionResult
            Return View()
        End Function

        ' 2013.03.10
        ' GET: /empaque/recibo_empaque
        <Authorize> _
        Public Function recibo_empaque() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim myModel(100) As reciboEmpaqueModel
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("pk_PendientesEmpaque")
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                If DR.HasRows Then
                    While DR.Read
                        myModel(index) = New reciboEmpaqueModel With {.OrderID = DR(0), .Customer = DR(1), .ProductType = DR(2), .SerialNo = DR(3), .Model = DR(4), .Description = DR(5)}
                        If index >= myModel.Length Then ReDim Preserve myModel(index + 1)
                        index += 1
                    End While
                    ReDim Preserve myModel(index - 1)
                Else
                    myModel(index) = New reciboEmpaqueModel With {.OrderID = "No data", .Customer = "No data", .ProductType = "No data", .SerialNo = "No data", .Model = "No data", .Description = "No data"}
                    index += 1
                    ReDim Preserve myModel(index - 1)
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                myModel(index) = New reciboEmpaqueModel With {.OrderID = "No data", .Customer = "No data", .ProductType = "No data", .SerialNo = "No data", .Model = "No data", .Description = "No data"}
                index += 1
                ReDim Preserve myModel(index - 1)
            Finally
                Me.DA.Dispose()
            End Try
            TempData("model") = myModel
            Return Me.View
        End Function

        '2013.03.10
        ' POST: /empaque/recibo_empaque
        <Authorize, HttpPost> _
        Public Function recibo_empaque(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("pk_recibo", model.OrderID, model.Comment, Me.User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                While DR.Read
                    TempData.Item("ErrMsg") = DR(0)
                End While
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Return Me.RedirectToAction("recibo_empaque")
        End Function

        ' 2013.03.10
        ' GET: /empaque/transfer_warehouse
        <Authorize> _
        Public Function transfer_warehouse() As ActionResult
            Return Me.View
        End Function

        ' 2013.03.10
        ' POST: /empaque/transfer_warehouse
        <Authorize, HttpPost> _
        Public Function transfer_warehouse(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("pk_TransferWharehouse", model.OrderID, model.Comment, Me.User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                While DR.Read
                    TempData.Item("ErrMsg") = DR(0)
                End While
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Return Me.RedirectToAction("transfer_warehouse")
        End Function

        ' 2013.03.10
        ' GET: /empaque/process_warehouse
        <Authorize> _
        Public Function process_warehouse() As ActionResult
            Return Me.View
        End Function

        ' 2013.03.10
        ' POST: /empaque/process_warehouse
        <Authorize, HttpPost> _
        Public Function process_warehouse(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("pk_ProcessWarehouse", model.OrderID, model.Comment, Me.User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                While DR.Read
                    TempData.Item("ErrMsg") = DR(0)
                End While
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Return Me.RedirectToAction("process_warehouse")
        End Function

        ' 2013.03.10 
        ' GET: /empaque/shipping
        <Authorize> _
        Public Function shipping() As ActionResult
            Return Me.View
        End Function

        ' 2013.03.10
        ' POST: /empaque/shipping
        <Authorize, HttpPost> _
        Public Function shipping(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("pk_shipping", model.OrderID, model.Comment, Me.User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                While DR.Read
                    TempData.Item("ErrMsg") = DR(0)
                End While
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Return Me.RedirectToAction("shipping")
        End Function

        ' 2013.03.10
        ' GET: /empaque/delivery
        <Authorize> _
        Public Function delivery() As ActionResult
            Return Me.View
        End Function

        ' 2013.03.10
        <Authorize, HttpPost> _
        Public Function delivery(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("pk_delivery", model.OrderID, model.Comment, Me.User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                While DR.Read
                    TempData.Item("ErrMsg") = DR(0)
                End While
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Return Me.RedirectToAction("delivery")
        End Function
            

        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader
    End Class
End Namespace
