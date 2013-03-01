Imports System.Data.SqlClient

Namespace AgioNet
    Public Class scController
        Inherits System.Web.Mvc.Controller

        ' Methods
        ' 2013.02.14 
        ' GET: /sc/approvalRepair
        <Authorize> _
        Public Function approvalRepair(ByVal model As ScanOrderModel) As ActionResult
            Dim FailureInfo As New AppFailureInfoModel
            Dim index As Integer = 0
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Me.Session.Item("OrderID") = model.OrderID

            Try
                Me.DR = Me.DA.ExecuteSP("sc_getFailureInfo", model.OrderID)
                If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                    Do While Me.DR.Read
                        FailureInfo = New AppFailureInfoModel With {.OrderID = DR(0), .TestID = DR(1), .TestDescription = DR(2), _
                                                                          .FailureID = DR(3), .Solution = DR(4), .Source = DR(5), _
                                                                          .FoundBy = DR(6), .FoundDate = DR(7)}
                    Loop
                Else
                    FailureInfo = New AppFailureInfoModel With {.OrderID = "No data", .TestID = "No data", .TestDescription = "No data", _
                                                                          .FailureID = "No data", .Solution = "No data", .Source = "No data", _
                                                                          .FoundBy = "No data", .FoundDate = "No data"}
                End If

                TempData("FailureInfo") = FailureInfo
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("Index")
            Finally
                DA.Dispose()
            End Try

            Return Me.View
        End Function

        ' 2013.02.27
        ' POST: /sc/approvalRepair
        <Authorize, HttpPost> _
        Public Function approvalRepair(ByVal model As ApprovalRepairModel) As ActionResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)

            Try
                DR = DA.ExecuteSP("sc_ApprovalDiagnostic", model.OrderID, model.Approval, model.Comments, User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                DA.Dispose()
            End Try

            Return RedirectToAction("aprobar_reparar")
        End Function

        ' 2013.02.22
        ' GET: /sc/LoadOrderInfo
        Public Function LoadOrderInfo() As PartialViewResult
            Dim info As New AllOrderInfoModel
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrderMaster_getOrderList", New Object() {"ByOrder", Session("OrderID")})
                If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                    Do While Me.DR.Read
                        info = New AllOrderInfoModel With {.OrderID = DR(0), .OrderDate = DR(1), .CustomerType = DR(2), .CustomerName = DR(3), _
                        .RFC = DR(4), .Email = DR(5), .Address = DR(6), .ExternalNumber = DR(7), .InternalNumber = DR(8), _
                        .Address2 = DR(9), .City = DR(10), .State = DR(11), .Country = DR(12), .ZipCode = DR(13), _
                        .Telephone = DR(14), .Telephone2 = DR(15), .Telephone3 = DR(16), .Delivery = DR(17), .DeliveryTime = DR(18), _
                        .ProductClass = DR(19), .ProductType = DR(20), .ProductTrademark = DR(21), .ProductModel = DR(22), _
                        .ProductDescription = DR(23), .PartNumber = DR(24), .SerialNumber = DR(25), .Revision = DR(26), _
                        .ServiceType = DR(27), .FailureType = DR(28), .Comment = DR(29)}
                    Loop
                    Me.TempData.Item("OrderInfo") = info
                End If
            Catch ex As Exception
                TempData("ErrMsg") = ex.Message
            Finally
                DA.Dispose()
            End Try

            Return PartialView("_OrderInfoPartial")
        End Function

        ' 2013.02.14
        ' GET: /sc/aprobar_reparar
        <Authorize> _
        Public Function aprobar_reparar() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As OrderListModel
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrderMaster_ReqApproval")
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("index")
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        Dim model As New OrderListModel With { _
                            .OrderID = DR(0), _
                            .OrderDate = DR(1), _
                            .CustomerName = DR(2), _
                            .Email = DR(3), _
                            .Delivery = DR(4), _
                            .DeliveryTime = DR(5), _
                            .ProductClass = DR(6), _
                            .ProductType = DR(7), _
                            .ProductModel = DR(8), _
                            .PartNo = DR(9), _
                            .SerialNo = DR(10) _
                        }
                        modelArray(index) = model
                        index += 1
                    Loop
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New OrderListModel With { _
                        .OrderID = "No Data", _
                        .OrderDate = "No Data", _
                        .CustomerName = "No Data", _
                        .Email = "No Data", _
                        .Delivery = "No Data", _
                        .DeliveryTime = "No Data", _
                        .ProductClass = "No Data", _
                        .ProductType = "No Data", _
                        .ProductModel = "No Data", _
                        .PartNo = "No Data", _
                        .SerialNo = "No Data" _
                    }
                    index += 1
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("index")
            End Try

            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If
            Me.DA.Dispose()

            Me.TempData.Item("Model") = modelArray
            Return Me.View
        End Function

        ' 2013.03.01 
        ' GET: /sc/costear_diagnostico
        <Authorize> _
        Public Function costear_diagnostico(ByVal model As SearchOrderModel) As ActionResult
            Dim Cost As New AppCostModel
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Me.Session.Item("OrderID") = model.OrderID

            Try
                Me.DR = Me.DA.ExecuteSP("sc_geDiagnosticCost ", model.OrderID)
                If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                    Do While Me.DR.Read
                        Cost = New AppCostModel With {.OrderID = DR(0), .Costo = DR(1), .LeadTime = DR(2), .Comentario = DR(3)}
                    Loop
                Else
                    Cost = New AppCostModel With {.OrderID = "", .Costo = "", .LeadTime = "", .Comentario = ""}
                End If

                TempData("model") = Cost
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("Index")
            Finally
                DA.Dispose()
            End Try

            Return Me.View
        End Function

        ' 2013.03.01 
        ' POST: /sc/costear_diagnostico
        <Authorize, HttpPost> _
        Public Function costear_diagnostico(ByVal model As AppCostModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)

            Try
                Me.DR = Me.DA.ExecuteSP("sc_CostearOrden ", model.OrderID, model.Costo, model.LeadTime, model.Comentario, model.CostBy)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("Index")
            Finally
                DA.Dispose()
            End Try

            Return RedirectToAction("ordenes_diagnosticadas")

        End Function

        ' 2013.02.14
        ' GET: /sc/crear_orden
        <Authorize> _
        Public Function crear_orden() As ActionResult
            Return Me.View
        End Function

        ' 2013.02.14
        ' POST: /sc/crear_orden
        <Authorize, HttpPost> _
        Public Function crear_orden(ByVal model As CreateOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str3 As String = String.Empty
            Dim email As New agEmail
            Dim emailAddress As String = String.Empty
            Dim txtSubject As String = String.Empty
            Dim txtMessage As String = String.Empty
            Dim model2 As CreateOrderModel = model

            Try
                If (Strings.Mid(model.ProductType, 1, 3) = "Ej.") Then
                    Throw New Exception("El tipo de producto no puede quedar en blanco")
                End If
                If (Strings.Mid(model.ProductTrademark, 1, 3) = "Ej.") Then
                    model.ProductTrademark = ""
                End If
                If (Strings.Mid(model.ProductModel, 1, 3) = "Ej.") Then
                    Throw New Exception("El modelo del producto no puede quedar en blanco")
                End If
                If (Strings.Mid(model.productDescription, 1, 3) = "Ej.") Then
                    model.productDescription = ""
                End If
                If (Strings.Mid(model.PartNumber, 1, 3) = "Ej.") Then
                    model.PartNumber = ""
                End If
                If (Strings.Mid(model.SerialNumber, 1, 3) = "Ej.") Then
                    Throw New Exception("El numero de serie del producto no puede quedar en blanco")
                End If

                Me.DR = Me.DA.ExecuteSP("sc_OrderMaster_Insert", model2.CustomerType, model2.CustomerName, model2.RazonSocial, _
                                        model2.CustomerReference, model2.RFC, model2.Email, model2.Address, model2.ExternalNumber, _
                                        model2.InternalNumber, model2.Address2, model2.City, model2.State, model2.Country, _
                                        model2.ZipCode, model2.Telephone, model2.Telephone2, model2.Telephone3, model2.Delivery, _
                                        model2.DeliveryTime, model2.ProductClass, model2.ProductType, model2.ProductTrademark, _
                                        model2.ProductModel, model2.productDescription, model2.PartNumber, model2.SerialNumber, _
                                        model2.Revision, model2.ServiceType, model2.FailureType, model2.Comment, Me.User.Identity.Name)

                If (Me.DA.LastErrorMessage = "") Then
                    Dim str5 As String = String.Empty
                    Do While Me.DR.Read
                        str3 = DR(0)
                        str5 = DR(1)
                    Loop

                    If Not Me.DR.IsClosed Then
                        Me.DR.Close()
                    End If

                    Me.DR = Me.DA.ExecuteSP("sys_getDistibutionList", "CREATEORDER")

                    If Me.DR.HasRows Then
                        Do While Me.DR.Read
                            emailAddress = DR(0)
                        Loop

                        email.HTMLBody = True
                        txtSubject = ("Creada nueva orden de reparación : " & str5)
                        txtMessage = agGlobals.getCreateOrderEmailFormat(str5, model)

                        If (model.CustomerType = "EndUser") Then
                            email.SendEmail(model.Email, txtSubject, txtMessage)
                        End If

                        email.SendEmail(emailAddress, txtSubject, txtMessage)
                    End If
                Else
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Return Me.View
                End If

                If Not Me.DR.IsClosed Then
                    Me.DR.Close()
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return View()
            Finally
                Me.DA.Dispose()
            End Try

            Me.TempData.Item("ErrMsg") = str3
            Return Me.RedirectToAction("index")
        End Function

        '2013.02.14
        ' GET: /sc/Index
        <Authorize> _
        Public Function Index() As ActionResult
            Return Me.View
        End Function

        ' 2013.02.28
        ' GET: /sc/ordenes_diagnosticadas
        Public Function ordenes_diagnosticadas() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As OrdenesDiagnosticadas
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrdenesDiagnosticadas")
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New OrdenesDiagnosticadas With {.OrderID = DR(0), .Incoming = DR(1), .FoundDate = DR(2), _
                                    .ProductModel = DR(3), .ProductDescription = DR(4), .PartNumber = DR(5), .SerialNumber = DR(6)}
                        If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New OrdenesDiagnosticadas With {.OrderID = "no data", .Incoming = "no data", .FoundDate = "no data", _
                                    .ProductModel = "no data", .ProductDescription = "no data", .PartNumber = "no data", .SerialNumber = "no data"}
                    index += 1
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("index")
            Finally
                DA.Dispose()
            End Try

            Me.TempData.Item("Model") = modelArray
            Return Me.View
        End Function

        ' 2013.02.14
        ' GET: /sc/ver_orden
        <Authorize> _
        Public Function ver_orden(ByVal md As SearchOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As OrderListModel
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrderMaster_getOrderList", "OrderList", md.OrderID)
                If DA._LastErrorMessage = "" Then

                    If Me.DR.HasRows Then
                        Do While Me.DR.Read
                            If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                            modelArray(index) = New OrderListModel With {.OrderID = DR(0), .OrderDate = DR(1), .CustomerName = DR(2), _
                                .Email = DR(3), .Delivery = DR(4), .DeliveryTime = DR(5), .ProductClass = DR(6), .ProductType = DR(7), _
                                .ProductModel = DR(8), .PartNo = DR(9), .SerialNo = DR(10), .Status = DR(11)}
                            index += 1
                        Loop
                        ' -- Actualizado por Carlos Barreto
                        ReDim Preserve modelArray(index - 1)

                    Else
                        index = 0
                        modelArray(index) = New OrderListModel With {.OrderID = "No Data", .OrderDate = "No Data", .CustomerName = "No Data", _
                            .Email = "No Data", .Delivery = "No Data", .DeliveryTime = "No Data", .ProductClass = "No Data", .ProductType = "No Data", _
                            .ProductModel = "No Data", .PartNo = "No Data", .SerialNo = "No Data", .Status = "No Data"}
                        index += 1
                        ' -- Actualizado por Carlos Barreto
                        ReDim Preserve modelArray(index - 1)
                    End If
                Else
                    Throw New Exception(DA._LastErrorMessage)
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("index")
            Finally
                DA.Dispose()
            End Try

            Me.TempData.Item("Model") = modelArray
            Return Me.View
        End Function

        '2013.02.14
        ' GET: /sc/ver_ordenes
        <Authorize> _
        Public Function ver_ordenes(ByVal md As SearchOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As OrderListModel
            Dim index As Integer = 0

            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrderMaster_getOrderList", "OrderList", md.OrderID)
                If DA._LastErrorMessage = "" Then

                    If Me.DR.HasRows Then
                        Do While Me.DR.Read
                            If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                            modelArray(index) = New OrderListModel With {.OrderID = DR(0), .OrderDate = DR(1), .CustomerName = DR(2), _
                                .Email = DR(3), .Delivery = DR(4), .DeliveryTime = DR(5), .ProductClass = DR(6), .ProductType = DR(7), _
                                .ProductModel = DR(8), .PartNo = DR(9), .SerialNo = DR(10), .Status = DR(11)}
                            index += 1
                        Loop
                        ' -- Actualizado por Carlos Barreto
                        ReDim Preserve modelArray(index - 1)

                    Else
                        index = 0
                        modelArray(index) = New OrderListModel With {.OrderID = "No Data", .OrderDate = "No Data", .CustomerName = "No Data", _
                            .Email = "No Data", .Delivery = "No Data", .DeliveryTime = "No Data", .ProductClass = "No Data", .ProductType = "No Data", _
                            .ProductModel = "No Data", .PartNo = "No Data", .SerialNo = "No Data", .Status = "No Data"}
                        index += 1
                        ' -- Actualizado por Carlos Barreto
                        ReDim Preserve modelArray(index - 1)
                    End If
                Else
                    Throw New Exception(DA._LastErrorMessage)
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("index")
            Finally
                DA.Dispose()
            End Try

            Me.TempData.Item("Model") = modelArray
            Return Me.View
        End Function


        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader

    End Class

End Namespace
