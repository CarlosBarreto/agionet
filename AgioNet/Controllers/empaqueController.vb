Imports System.Data.SqlClient
Imports Root.Reports

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
        Public Function shipping(ByVal model As ScanPackModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)

            Try
                Me.DR = Me.DA.ExecuteSP("pk_shipping", model.OrderID, model.Comentario, Me.User.Identity.Name)
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

        ' 2013.03.22 
        ' GET: /empaque/pagina_entrega
        <Authorize> _
        Public Function pagina_entrega() As ActionResult
            Return Me.View
        End Function

        ' 2013.03.10
        ' POST: /empaque/pagina_entrega
        <Authorize, HttpPost> _
        Public Function pagina_entrega(ByVal model As ScanPackModel) As ActionResult
            Dim response As ActionResult

            Try
                '-- Si no hay error, imprimir la pagina
                TempData("OrderID") = model.OrderID
                TempData("Reparacion") = model.Reparacion
                TempData("Comentarios") = model.Comentario

                response = Me.RedirectToAction("print_chkpage")
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                response = Me.RedirectToAction("shipping")
            End Try

            Return response
        End Function


        ' 2013.03.22
        '2013.02.14
        ' GET: /empaque/print_chkpage
        <HttpGet, Authorize> _
        Public Function print_chkpage() As ActionResult
            Dim code As String = Me.TempData.Item("OrderID")
            Dim model As New DeliveryPageModel

            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrderMaster_GetOrderInfo", code)
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("index")
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        model = New DeliveryPageModel With {.CustomerName = DR(2), .RazonSocial = DR(3), .CustomerReference = DR(4), _
                                .RFC = DR(5), .Email = DR(6), .Address = DR(7), .ExternalNumber = DR(8), .InternalNumber = DR(9), _
                                .Address2 = DR(10), .City = DR(11), .State = DR(12), .Country = DR(13), .ZipCode = DR(14), _
                                .Telephone = DR(15), .Telephone2 = DR(16), .Telephone3 = DR(17), .Delivery = DR(18), .DeliveryTime = DR(19), _
                                .ProductClass = DR(21), .ProductType = DR(22), .ProductTrademark = DR(23), .ProductModel = DR(24), _
                                .productDescription = DR(25), .PartNumber = DR(26), .SerialNumber = DR(27), .Revision = DR(28), _
                                .ServiceType = DR(29), .FailureType = DR(30), .Comment = DR(31), .Reparacion = TempData("Reparacion"), _
                                .Comentarios = TempData("Comentarios") _
                        }
                    Loop
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("index")
            End Try

            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If

            Me.DA.Dispose()

            Try
                'Crear la imagen del código de barra
                Dim path As String = (Me.Server.MapPath("~/Content/temp/") & code & ".jpg")
                System.IO.File.WriteAllBytes(path, GenerarCodigo(Me.Server, code, "", 350, 40, 60))

                Dim report As New PDFFormatoEntrega 'PDFInTransitPage
                report.RutaLogo = Me.Server.MapPath("~/Content/images/logo-01.jpg")
                report.RutaBarCode = path
                report.RutaComments = Me.Server.MapPath("~/Content/images/comments.jpg")
                report.Orderid = code
                report.Model = model

                'RT.PrintPDF(report)
                RT.ViewPDF(report, Me.Server.MapPath("~/Content/temp/emp/") & code & ".pdf")

                report = Nothing

                Dim filename As String = Me.Server.MapPath("~/Content/temp/emp/") & code & ".pdf"
                Return File(filename, "application/pdf", Server.HtmlEncode(filename))
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("index")
            End Try

            Return RedirectToAction("shipping")
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
