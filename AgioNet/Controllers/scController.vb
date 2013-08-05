Imports System.Data.SqlClient

Namespace AgioNet
    Public Class scController
        Inherits System.Web.Mvc.Controller

        ' Methods
        ' 2013.02.14 
        ' GET: /sc/approvalRepair
        <Authorize> _
        Public Function approvalRepair(ByVal model As ScanOrderModel) As ActionResult
            Dim FailureInfo As New CotizacionInfoModel
            Dim index As Integer = 0
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Me.Session.Item("OrderID") = model.OrderID

            Try
                Me.DR = Me.DA.ExecuteSP("sc_getCotizacionInfo", model.OrderID)
                If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                    Do While Me.DR.Read
                        FailureInfo = New CotizacionInfoModel With {.OrderID = DR(0), .LeadTime = DR(1), .Proveedor = DR(2), _
                                                                          .Comentario = DR(3)}
                    Loop
                Else
                    FailureInfo = New CotizacionInfoModel With {.OrderID = "No data", .LeadTime = "No data", .Proveedor = "No data", _
                                                                          .Comentario = "no data"}
                End If

                TempData("FailureInfo") = FailureInfo
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("Index")
            Finally
                DA.Dispose()
            End Try

            TempData("OrderID") = model.OrderID
            Return Me.View
        End Function

        ' 2013.02.27
        ' POST: /sc/approvalRepair
        <Authorize, HttpPost> _
        Public Function approvalRepair(ByVal model As ApprovalRepairModel) As ActionResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim strMail As String
            Dim OrderID As String = Mid(model.OrderID, 1, model.OrderID.Length - 3) & "-01"
            Try
                DR = DA.ExecuteSP("sc_ApprovalDiagnostic", model.OrderID, model.Approval, model.Absorvido, model.Comments, User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                '--Comprobar correo
                strMail = mailing.getAutorizacionEmail(OrderID)
                If strMail <> "Error! No se han encontrado datos" And strMail <> "No se han cotizado todas las partes" Then
                    Dim email As New agEmail
                    Dim emailAddress As String = String.Empty
                    Dim txtSubject As String = String.Empty
                    Dim txtMessage As String = String.Empty

                    '-- Enviar correo
                    email.HTMLBody = True
                    txtSubject = mailing.getTituloAutorizacion(OrderID, "APPROVAL")
                    emailAddress = mailing.getDistributionListAutorizacion(mailing._IsApproval, User.Identity.Name, OrderID)

                    txtMessage = strMail
                    email.SendEmail(emailAddress, txtSubject, txtMessage)
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                DA.Dispose()
            End Try

            Return RedirectToAction("aprobar_reparar_list", New With {.OrderID = Session("OrderID")})
        End Function

        ' 2013.02.22
        ' GET: /sc/LoadOrderInfo
        Public Function LoadOrderInfo() As PartialViewResult
            Dim info As New OrderPartInfoModel
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrderMaster_getOrderList", New Object() {"ORDERPART", Session("OrderID")})
                If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                    Do While Me.DR.Read
                        info = New OrderPartInfoModel With {.OrderID = DR(0), .OrderDate = DR(1), .ProductClass = DR(2), _
                        .ProductType = DR(3), .Trademark = DR(4), .Model = DR(5), .Description = DR(6), .PartNo = DR(7), _
                        .Commodity = DR(8), .SerialNumber = DR(9), .Revision = DR(10), .ServiceType = DR(11), .Failure = DR(12), _
                        .Comentario = DR(13), _
                        .sProductClass = DR(15), .sProductType = DR(16), .sTrademark = DR(17), .sModel = DR(18), _
                        .sDescription = DR(19), .sPartNumber = DR(20), .sALT = DR(21), .sSerialNumber = DR(22), _
                        .sRevision = DR(23), .sFailure = DR(24), .sSolution = DR(25), .sSource = DR(26), .sComment = DR(27)}
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
        Public Function aprobar_reparar(model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As AprobarRepararListModel
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrderMaster_ReqApproval", "LIST", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("index")
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New AprobarRepararListModel With {.OrderID = DR(0), .FechaIngreso = DR(1), _
                                            .EnvioCotizacion = DR(2), .LeadTime = DR(3), .PartNo = DR(4), .Description = DR(5), _
                                            .ProductType = DR(6)}

                        index += 1
                    Loop
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New AprobarRepararListModel With {.OrderID = "no data", .FechaIngreso = "no data", _
                                            .EnvioCotizacion = "no data", .LeadTime = "no data", .PartNo = "no data", _
                                            .Description = "no data", .ProductType = "no data"}
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
            Session.Remove("OrderID")
            Return Me.View
        End Function

        ' 2013.04.19
        ' GET: /sc/aprobar_reparar_list
        Public Function aprobar_reparar_list(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As AprobarRepararModel
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrderMaster_ReqApproval", "BYORDER", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("index")
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New AprobarRepararModel With {.OrderID = DR(0), .PartNumber = DR(1), .Description = DR(2), _
                                            .Aprobado = DR(3), .Failure = DR(4), .Solution = DR(5), .TipoReparacion = DR(6), .FoundBy = DR(7), .Comentario = DR(8)}

                        index += 1
                    Loop
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New AprobarRepararModel With {.OrderID = "No data", .PartNumber = "No data", .Description = "No data", _
                                            .Aprobado = "No data", .Failure = "No data", .Solution = "No data", .TipoReparacion = "No data", _
                                            .FoundBy = "No data", .Comentario = "No data"}
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
            Session("OrderID") = model.OrderID
            Return Me.View
        End Function

        ' 2013.03.01 
        ' GET: /sc/costear_diagnostico
        <Authorize> _
        Public Function costear_diagnostico(ByVal model As SearchOrderModel) As ActionResult
            Dim Cost As New AppCostModel
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Me.Session.Item("OrderID") = model.OrderID
            Dim TipoReparacion As String = String.Empty

            Try
                Me.DR = Me.DA.ExecuteSP("sc_geDiagnosticCost ", model.OrderID)
                If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                    Do While Me.DR.Read
                        Cost = New AppCostModel With {.OrderID = DR(0), .Costo = DR(1), .Flete = DR(2), .GastosImportacion = DR(3), _
                                                      .LeadTime = DR(4), .Proveedor = DR(5), .Comentario = DR(6)}
                    Loop
                Else
                    Cost = New AppCostModel With {.OrderID = "", .Costo = "", .Flete = "", .GastosImportacion = "", _
                                                  .LeadTime = "", .Proveedor = "", .Comentario = ""}
                End If

                TempData("model") = Cost
                '-- Determinar si la orden es costeable
                If Not DR.IsClosed Then
                    DR.Close()
                End If

                DR = DA.ExecuteSP("sc_getTipoReparacion", model.OrderID)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                While DR.Read
                    TipoReparacion = DR(0)
                End While
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("Index")
            Finally
                DA.Dispose()
            End Try

            TempData("TipoReparacion") = TipoReparacion
            Return Me.View
        End Function

        ' 2013.03.01 
        ' POST: /sc/costear_diagnostico
        <Authorize, HttpPost> _
        Public Function costear_diagnostico(ByVal model As AppCostModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim strMail As String
            Try
                model.Costo = Trim(model.Costo.Replace("$", ""))
                model.Costo = Trim(model.Costo.Replace(",", ""))

                model.Flete = Trim(model.Flete.Replace("$", ""))
                model.Flete = Trim(model.Flete.Replace(",", ""))

                model.GastosImportacion = Trim(model.GastosImportacion.Replace("$", ""))
                model.GastosImportacion = Trim(model.GastosImportacion.Replace(",", ""))
                model.Comentario = HttpUtility.HtmlEncode(model.Comentario)

                Me.DR = Me.DA.ExecuteSP("sc_CostearOrden ", model.OrderID, model.Costo, model.Flete, model.GastosImportacion, _
                                        model.LeadTime, model.Proveedor, model.Comentario, model.CostBy)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Not DR.IsClosed Then DR.Close()

                '--Comprobar correo
                strMail = mailing.getCotizacionEmail(model.OrderID)
                If strMail <> "Error! No se han encontrado datos" Then
                    Dim email As New agEmail
                    Dim emailAddress As String = String.Empty
                    Dim txtSubject As String = String.Empty
                    Dim txtMessage As String = String.Empty

                    '-- Enviar correo
                    email.HTMLBody = True
                    emailAddress = mailing.getDistributionListCotizacion(Me.Session.Item("OrderID"))
                    txtSubject = "La orden " & Me.Session.Item("OrderID") & " está lista para cotizar al cliente "
                    txtMessage = strMail
                    email.SendEmail(emailAddress, txtSubject, txtMessage)
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("Index")
            Finally
                DA.Dispose()
            End Try

            Return RedirectToAction("ordenes_diagnosticadas")

        End Function

        ' 2013.03.15 
        ' GET: /sc/cotizar_cliente
        <Authorize> _
        Public Function cotizar_cliente(model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As OrdenesCosteadasModel
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrdenesCosteadas", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New OrdenesCosteadasModel With {.OrderID = DR(0), .Incoming = DR(1), .StartDiagnostic = DR(2), _
                                            .CloseDiagnostic = DR(3), .CostDate = DR(4), .ProductModel = DR(5), .ProductDescription = DR(6), _
                                            .PartNumber = DR(7), .SerialNumber = DR(8)}
                        If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New OrdenesCosteadasModel With {.OrderID = "no data", .Incoming = Now, .StartDiagnostic = Now, _
                                            .CloseDiagnostic = Now, .ProductModel = "no data", .ProductDescription = "no data", _
                                            .PartNumber = "no data", .SerialNumber = "no data", .CostDate = "No data"}
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

        ' 2013.03.15
        ' GET: /sc/cotizarcliente
        Public Function cotizarcliente(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim Sumatoria As Integer = 0
            Dim eModel As New extDatosCotizacion
            Dim modelArray(100) As SubOrderListModel
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("sc_getSubOrderList", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New SubOrderListModel With {.OrderID = DR(0), .PartNumber = DR(1), .Description = DR(2), _
                                            .Commodity = DR(3), .SerialNumber = DR(4), .Costo = DR(5), .LeadTime = DR(6), _
                                            .Failure = DR(7), .Solution = DR(8), .TipoReparacion = DR(9), .Comentario = DR(10)}
                        If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New SubOrderListModel With {.OrderID = "no data", .PartNumber = "no data", .Description = "no data", _
                                            .Commodity = "no data", .SerialNumber = "no data", .Costo = "no data", .LeadTime = "no data", _
                                            .Failure = "no data", .Solution = "no data", .TipoReparacion = "no data", .Comentario = "no data"}
                    index += 1
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If

                If Not DR.IsClosed Then
                    DR.Close()
                End If

                DR = DA.ExecuteSP("sc_getDatosCotizacion", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If


                If Me.DR.HasRows Then
                    While DR.Read
                        eModel = New extDatosCotizacion With {.Failure = DR(1), .Retro = DR(2), .Resumen = DR(3), .FechaIngreso = DR(4), _
                                                              .ComentarioCliente = DR(5), .Delivery = DR(6), .ProductType = DR(7), _
                                                              .Rerepair = DR(8)}
                    End While
                Else
                    eModel = New extDatosCotizacion With {.Failure = "no data", .Retro = "no data", .Resumen = "no data", _
                                                          .FechaIngreso = "no data", .ComentarioCliente = "no data", .Delivery = "no data", _
                                                          .ProductType = "no data", .Rerepair = "No data"}
                End If

            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("index")
            Finally
                DA.Dispose()
            End Try

            Me.TempData.Item("Model") = modelArray
            Me.TempData.Item("OrderID") = model.OrderID
            Me.TempData.Item("EModel") = eModel
            Return Me.View
        End Function

        ' 2013.03.19
        ' POST: /Cotizarcliente
        <Authorize, HttpPost> _
        Public Function cotizarcliente(ByVal model As CotizarClienteModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim response As ActionResult

            Try
                model.ManoObra = Trim(model.ManoObra.Replace("$", ""))
                model.ManoObra = Trim(model.ManoObra.Replace(",", ""))
                model.Viaje = Trim(model.Viaje.Replace("$", ""))
                model.Viaje = Trim(model.Viaje.Replace(",", ""))
                model.SubTotal = Trim(model.SubTotal.Replace("$", ""))
                model.SubTotal = Trim(model.SubTotal.Replace(",", ""))
                model.IVA = Trim(model.IVA.Replace("$", ""))
                model.IVA = Trim(model.IVA.Replace(",", ""))
                model.Total = Trim(model.Total.Replace("$", ""))
                model.Total = Trim(model.Total.Replace(",", ""))

                ' If model.Total = "0.00" Then
                ' Throw New Exception("Error! Antes de guardar, debe calcular los valores")
                'End If

                '-- Guardar los registros por separado 
                Dim cont As Integer = 0
                While model.SubOrder.Length > cont
                    DR = DA.ExecuteSP("sc_saveCosteoParte", model.SubOrder(cont), model.SubCosto(cont), model.SubUtilidad(cont))
                    If DA._LastErrorMessage <> "" Then
                        Throw New Exception(DA._LastErrorMessage)
                    End If
                    If Not DR.IsClosed Then
                        DR.Close()
                    End If
                    cont += 1
                End While

                '-- Guardar el resultado de la cotización
                Me.DR = Me.DA.ExecuteSP("sc_saveCotizarCliente", model.OrderID, model.ManoObra, model.Viaje, model.SubTotal, _
                                        model.IVA, model.Total, model.LeadTime, User.Identity.Name)

                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                response = RedirectToAction("cotizar_cliente")
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                response = RedirectToAction("cotizar_cliente")
            Finally
                DA.Dispose()
            End Try

            Return response
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
            Dim str5 As String = String.Empty
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

                If (Me.DA.LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Return Me.View
                End If


                Do While Me.DR.Read
                    str3 = DR(0)
                    str5 = DR(1)
                Loop

                If Not Me.DR.IsClosed Then
                    Me.DR.Close()
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return View()
            End Try

            Try
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

            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Me.TempData.Item("ErrMsg") = str3
            Return Me.RedirectToAction("index")
        End Function

        ' 2013.07.22
        ' GET: /sc/crear_orden_assurant
        <Authorize> _
        Public Function crear_orden_assurant() As ActionResult
            Dim Response As ActionResult

            If Session("OrderID") <> "" Then
                'Response = Me.RedirectToAction("hoja_individual", "logistica", New With {.OrderID = Session("OrderID")})
                Response = Me.View
            Else
                If Not Session("File") <> "" Then Session.Clear()
                Response = Me.View
            End If


            Return Response
        End Function

        ' 2013.02.14
        ' POST: /sc/crear_orden_assurant
        <Authorize, HttpPost> _
        Public Function crear_orden_assurant(ByVal model As CreateAssurantOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str3 As String = String.Empty
            Dim str5 As String = String.Empty
            Dim email As New agEmail
            Dim emailAddress As String = String.Empty
            Dim txtSubject As String = String.Empty
            Dim txtMessage As String = String.Empty
            Dim model2 As CreateAssurantOrderModel = model

            Try

                Me.DR = Me.DA.ExecuteSP("sc_OrderMaster_AssurantInsert", model2.CustomerReference, model2.NotificacionServicio, model2.CustomerName, _
                        model2.Email, model2.RazonSocial, model2.Address2, model2.RFC, model2.City, model2.Address, model2.State, model2.ExternalNumber, _
                        model2.Country, model2.InternalNumber, model2.ZipCode, model2.Telephone, model2.DeliveryTime, model2.Telephone2, model2.Telephone3, _
                        model.AddressReference, model2.ProductClass, model2.ProductType, model2.ProductModel, model2.ProductTrademark, model2.PartNumber, _
                        model2.productDescription, model2.SerialNumber, model2.Revision, model2.ServiceType, model2.Delivery, model2.FailureType, _
                        model2.Comment, model2.Cosmetic, model2.Accesories, Me.User.Identity.Name)

                If (Me.DA.LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Return Me.View
                End If


                Do While Me.DR.Read
                    str3 = DR(0)
                    str5 = DR(1)
                Loop

                If Not Me.DR.IsClosed Then
                    Me.DR.Close()
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return View()
            End Try

            Try
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

            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Me.TempData.Item("ErrMsg") = str3
            Session.Clear()
            Session.Add("OrderID", str5 & "-01")


            Return RedirectToAction("crear_orden_assurant")
        End Function

        '2013.02.14
        ' GET: /sc/Index
        <Authorize> _
        Public Function Index() As ActionResult
            Return Me.View
        End Function

        ' 2013.02.28
        ' GET: /sc/ordenes_diagnosticadas
        <Authorize> _
        Public Function ordenes_diagnosticadas(model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As OrdenesDiagnosticadas
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrdenesDiagnosticadas", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New OrdenesDiagnosticadas With {.OrderID = DR(0), .Incoming = DR(1), .FoundDate = DR(2), _
                                    .ProductModel = DR(3), .ProductDescription = DR(4), .PartNumber = DR(5), .TipoReparacion = DR(6)}
                        If index + 1 = modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New OrdenesDiagnosticadas With {.OrderID = "no data", .Incoming = "no data", .FoundDate = "no data", _
                                    .ProductModel = "no data", .ProductDescription = "no data", .PartNumber = "no data", .TipoReparacion = "no data"}
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
                'Me.DR = Me.DA.ExecuteSP("sc_OrderMaster_getOrderList", "OrderList", md.OrderID)
                If md.Nombre Is Nothing Or md.Nombre = "" Then md.Nombre = ""
                DR = DA.ExecuteSP("dbo.sc_OrderMaster_byName", md.Nombre, md.SerialNo)
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

        '------------------
        ' 2013.03.06 
        ' GET: /sc/cancel_orden
        <Authorize> _
        Public Function cancel_order() As ActionResult
            Return Me.View
        End Function

        ' 2013.03.06
        ' POST: /sc/cancel_orden
        <Authorize, HttpPost> _
        Public Function cancel_order(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("sc_CancelOrder", model.OrderID, model.Comment, Me.User.Identity.Name)
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Return Me.View
                End If

                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = DR(0)
                Loop

            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                'If Not Me.DR.IsClosed Then
                ' Me.DR.Close()
                ' End If
                Me.DA.Dispose()
            End Try

            Return Me.RedirectToAction("cancel_order")
        End Function

        '-------------------
        ' 2013.04.04 
        ' GET: /sc/envio_cotizacion
        <Authorize> _
        Public Function envio_cotizacion() As ActionResult
            Return Me.View
        End Function

        ' 2013.04.04 
        ' POST: /sc/saveCostPart
        <Authorize, HttpPost> _
        Public Function saveCostPart(ByVal costo As String, pUtilidad As String, Utilidad As String) As ActionResult
            Return Json(New With {.success = "true"})
        End Function

        ' 2013.04.09
        ' GET: /sc/detalle_orden
        <Authorize> _
        Public Function detalle_orden(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim CreateOrder As New CreateAssurantOrderModel
            Dim OrderID As String = String.Empty

            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrderMaster_GetOrderInfo", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        OrderID = DR(0)
                        CreateOrder = New CreateAssurantOrderModel With {.CustomerName = DR(2), .RazonSocial = DR(3), .CustomerReference = DR(4), _
                                     .RFC = DR(5), .Email = DR(6), .Address = DR(7), .ExternalNumber = DR(8), .InternalNumber = DR(9), _
                                     .Address2 = DR(10), .City = DR(11), .State = DR(12), .Country = DR(13), .ZipCode = DR(14), _
                                     .Telephone = DR(15), .Telephone2 = DR(16), .Telephone3 = DR(17), .Delivery = DR(18), .DeliveryTime = DR(19), _
                                     .ProductClass = DR(21), .ProductType = DR(22), .ProductTrademark = DR(23), .ProductModel = DR(24), _
                                     .productDescription = DR(25), .PartNumber = DR(26), .SerialNumber = DR(27), .Revision = DR(28), _
                                     .ServiceType = DR(29), .FailureType = DR(30), .Comment = DR(31), .NotificacionServicio = DR(38) _
                        }
                    Loop
                    If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                        Me.DR.Close()
                    End If
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("ver_orden")
            Finally
                DA.Dispose()
            End Try

            TempData("Model") = CreateOrder
            TempData("OrderID") = OrderID

            Return Me.View
        End Function

        ' 2013.04.09 
        ' POST: /sc/detalle_orden
        <Authorize, HttpPost> _
        Public Function detalle_orden(ByVal model As CreateOrderModel) As ActionResult
            Return RedirectToAction("ver_orden")
        End Function

        ' 2013.04.18
        ' GET: /sc/enviar_cotizacion
        <Authorize> _
        Public Function enviar_cotizacion(model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As PendientesEnviarCotizacion
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("dbo.sc_PendientesEnviarCotizacion", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New PendientesEnviarCotizacion With {.OrderID = DR(0), .FechaIngreso = DR(1), .FechaDiagnostico = DR(2), _
                                            .FechaCotizacion = DR(3), .LeadTime = DR(4), .CostBy = DR(5)}
                        If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New PendientesEnviarCotizacion With {.OrderID = "No data", .FechaIngreso = "No data", .FechaDiagnostico = "No data", _
                                            .FechaCotizacion = "No data", .LeadTime = "No data", .CostBy = "No data"}
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

        '2013.04.17
        ' GET: /sc/enviarcotizacion
        <Authorize> _
        Public Function enviarcotizacion(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim Sumatoria As Integer = 0
            Dim eModel As New DatosCotizacionExtModel
            Dim modelArray(100) As PrecioCotizacionModel
            Dim index As Integer = 0
            Dim mailInfo As New mailing.DiagnosticMailInfo

            Try
                Me.DR = Me.DA.ExecuteSP("sc_getPrecioCotizacion", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New PrecioCotizacionModel With {.OrderID = DR(0), .PartNo = DR(1), .Description = DR(2), _
                                            .Commodity = DR(3), .SerialNo = DR(4), .LeadTime = DR(5), _
                                            .Falla = DR(6), .Solucion = DR(7), .Precio = DR(8), .TipoReparacion = DR(9), .Comentario = DR(10)}
                        If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New PrecioCotizacionModel With {.OrderID = "no data", .PartNo = "no data", .Description = "no data", _
                                            .Commodity = "no data", .SerialNo = "no data", .LeadTime = "no data", _
                                            .Falla = "no data", .Solucion = "no data", .Precio = "no data", .TipoReparacion = "no data", .Comentario = "no data"}
                    index += 1
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If

                If Not DR.IsClosed Then
                    DR.Close()
                End If

                DR = DA.ExecuteSP("sc_getDatosCotizacionExt", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If


                If Me.DR.HasRows Then
                    While DR.Read
                        eModel = New DatosCotizacionExtModel With {.OrderID = DR(0), .Failure = DR(1), .Retro = DR(2), _
                                .Comments = DR(3), .InDate = DR(4), .ManoObra = DR(5), .Viaje = DR(6), .Subtotal = DR(7), _
                                .IVA = DR(8), .Total = DR(9), .leadTime = DR(10), .Comment = DR(11), .Delivery = DR(12)}
                    End While
                Else
                    eModel = New DatosCotizacionExtModel With {.OrderID = "no data", .Failure = "no data", .Retro = "no data", _
                                .Comments = "no data", .InDate = "no data", .ManoObra = "no data", .Viaje = "no data", _
                                .Subtotal = "no data", .IVA = "no data", .Total = "no data", .leadTime = "no data", .Comment = "no data", .Delivery = "no data"}
                End If

                If Not DR.IsClosed Then
                    DR.Close()
                End If


                DR = DA.ExecuteSP("dg_getDiagnosticMailInfo", model.OrderID)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If DR.HasRows Then
                    While DR.Read
                        With mailInfo
                            .Marca = DR(1)
                            .Modelo = DR(2)
                            .SKUNO = DR(3)
                            .Descripcion = DR(4)
                            .Serie = DR(5)
                            .FallaReportada = DR(6)
                            .RetroAlimentacion = DR(7)
                            .ComentarioTecnico = DR(8)
                        End With
                    End While
                Else
                    With mailInfo
                        .Marca = "No data"
                        .Modelo = "No data"
                        .SKUNO = "No data"
                        .Descripcion = "No data"
                        .Serie = "No data"
                        .FallaReportada = "No data"
                        .RetroAlimentacion = "No data"
                        .ComentarioTecnico = "No data"
                    End With
                End If

            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("index")
            Finally
                DA.Dispose()
            End Try

            Me.TempData.Item("Model") = modelArray
            Me.TempData.Item("OrderID") = model.OrderID
            Me.TempData.Item("EModel") = eModel
            Me.TempData.Item("DatosEquipo") = mailInfo
            Return Me.View
        End Function

        ' 2013.04.18
        ' POST: /sc/enviarcotizacione
        <Authorize, HttpPost> _
        Public Function enviarcotizacion(ByVal model As EnviarCotizacionModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("sc_EnviarCotizacionSP", model.OrderID, model.Comentario, Me.User.Identity.Name)
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Return Me.View
                End If

                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = DR(0)
                Loop

            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Return Me.RedirectToAction("enviar_cotizacion")
        End Function


        ' 2013.04.23
        ' GET: /sc/comprar_parte
        <Authorize> _
        Public Function comprar_parte(model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As ComprarPartesListModel
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrdenesCotizadas", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New ComprarPartesListModel With {.OrderID = DR(0), .Incoming = DR(1), .PartNo = DR(2), _
                                    .ProductDescription = DR(3), .Comprado = DR(4), .ETAParte = DR(5), .FechaCompromiso = DR(6), _
                                    .ApprovalDate = DR(7), .TipoReparacion = DR(8), .FuenteSuministro = CambiaLink(DR(9))}
                        If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New ComprarPartesListModel With {.OrderID = "no data", .Incoming = "no data", .PartNo = "no data", _
                                    .ProductDescription = "no data", .Comprado = "no data", .ETAParte = "no data", .FechaCompromiso = "no data", _
                                    .ApprovalDate = "no data", .TipoReparacion = "no data", .FuenteSuministro = "no data"}
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

        ' 2013.04.23
        ' GET: /sc/comprarparte 'sc_CostearOrden()
        <Authorize> _
        Public Function comprarparte(ByVal model As ScanOrderModel) As ActionResult
            Dim Cost As New AppCostModel
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Me.Session.Item("OrderID") = model.OrderID
            Dim TipoReparacion As String = String.Empty

            Try
                Me.DR = Me.DA.ExecuteSP("sc_geDiagnosticCost ", model.OrderID)
                If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                    Do While Me.DR.Read
                        Cost = New AppCostModel With {.OrderID = DR(0), .Costo = DR(1), .Flete = DR(2), .GastosImportacion = DR(3), _
                                                      .LeadTime = DR(4), .Proveedor = DR(5), .Comentario = DR(6)}
                    Loop
                Else
                    Cost = New AppCostModel With {.OrderID = "", .Costo = "", .Flete = "", .GastosImportacion = "", _
                                                  .LeadTime = "", .Proveedor = "", .Comentario = ""}
                End If

                TempData("model") = Cost
                '-- Determinar si la orden es costeable
                If Not DR.IsClosed Then
                    DR.Close()
                End If

                DR = DA.ExecuteSP("sc_getTipoReparacion", model.OrderID)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                While DR.Read
                    TipoReparacion = DR(0)
                End While
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("Index")
            Finally
                DA.Dispose()
            End Try

            TempData("TipoReparacion") = TipoReparacion
            Return Me.View

        End Function

        ' 2013.04.23
        ' POST: /sc/comprarparte
        <Authorize, HttpPost> _
        Public Function comprarparte(ByVal model As AppCostModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim strMail As String
            Try
                model.Costo = Trim(model.Costo.Replace("$", ""))
                model.Costo = Trim(model.Costo.Replace(",", ""))

                model.Flete = Trim(model.Flete.Replace("$", ""))
                model.Flete = Trim(model.Flete.Replace(",", ""))

                model.GastosImportacion = Trim(model.GastosImportacion.Replace("$", ""))
                model.GastosImportacion = Trim(model.GastosImportacion.Replace(",", ""))
                model.Comentario = HttpUtility.HtmlEncode(model.Comentario)

                Me.DR = Me.DA.ExecuteSP("sc_ComprarParte ", model.OrderID, model.Costo, model.Flete, model.GastosImportacion, _
                                        model.LeadTime, model.Proveedor, model.Comentario, model.CostBy)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                '-----------
                '--Comprobar correo
                Dim OrderID As String = Mid(model.OrderID, 1, model.OrderID.Length - 3) & "-01"
                strMail = mailing.getComprasEmail(OrderID)
                If strMail <> "Error! No se han encontrado datos" And strMail <> "No se han cotizado todas las partes" Then
                    Dim email As New agEmail
                    Dim emailAddress As String = String.Empty
                    Dim txtSubject As String = String.Empty
                    Dim txtMessage As String = String.Empty

                    '-- Enviar correo
                    email.HTMLBody = True
                    txtSubject = OrderID & ": AUTORIZADA Para su reparación"
                    emailAddress = mailing.getDistributionListCompras(User.Identity.Name, OrderID)

                    txtMessage = strMail
                    email.SendEmail(emailAddress, txtSubject, txtMessage)
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("Index")
            Finally
                DA.Dispose()
            End Try

            Return RedirectToAction("comprar_parte")
        End Function
        ' Fields

        '2013.04.23 
        'GET: /sc/cargar_guia
        <Authorize> _
        Public Function cargar_guia(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As OrdenesDiagnosticadas
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrdenesCompradas", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New OrdenesDiagnosticadas With {.OrderID = DR(0), .Incoming = DR(1), .FoundDate = DR(2), _
                                    .ProductModel = DR(3), .ProductDescription = DR(4), .PartNumber = DR(5), .SerialNumber = DR(6),
                                    .Comprada = DR(7), .ETA = DR(8)}
                        If index + 1 >= modelArray.Length Then ReDim Preserve modelArray(index + 10)
                        index += 1
                    Loop
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New OrdenesDiagnosticadas With {.OrderID = "no data", .Incoming = "no data", .FoundDate = "no data", _
                                    .ProductModel = "no data", .ProductDescription = "no data", .PartNumber = "no data", .SerialNumber = "no data", _
                                    .Comprada = "no data", .ETA = Now}
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

        ' 2013.04.23
        ' GET: /sc/cargarguia 'sc_CostearOrden()
        Public Function cargarguia(ByVal model As ScanOrderModel) As ActionResult
            Dim Cost As New AppCostModel
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Me.Session.Item("OrderID") = model.OrderID
            Dim TipoReparacion As String = String.Empty

            Try
                Me.DR = Me.DA.ExecuteSP("sc_geDiagnosticCost ", model.OrderID)
                If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                    Do While Me.DR.Read
                        Cost = New AppCostModel With {.OrderID = DR(0), .Costo = DR(1), .Flete = DR(2), .GastosImportacion = DR(3), _
                                                      .LeadTime = DR(4), .Proveedor = DR(5), .Comentario = DR(6)}
                    Loop
                Else
                    Cost = New AppCostModel With {.OrderID = "", .Costo = "", .Flete = "", .GastosImportacion = "", _
                                                  .LeadTime = "", .Proveedor = "", .Comentario = ""}
                End If

                TempData("model") = Cost
                '-- Determinar si la orden es costeable
                If Not DR.IsClosed Then
                    DR.Close()
                End If

                DR = DA.ExecuteSP("sc_getTipoReparacion", model.OrderID)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                While DR.Read
                    TipoReparacion = DR(0)
                End While
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("Index")
            Finally
                DA.Dispose()
            End Try

            TempData("TipoReparacion") = TipoReparacion
            Return Me.View

        End Function

        ' 2013.04.23
        ' POST: /sc/cargarguia
        <Authorize, HttpPost> _
        Public Function cargarguia(ByVal model As GuiaCompraModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)

            Try
                Me.DR = Me.DA.ExecuteSP("sc_CargarGuiaSP ", model.OrderID, model.TrackNo)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("Index")
            Finally
                DA.Dispose()
            End Try

            Return RedirectToAction("cargar_guia")
        End Function
        ' Fields

        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader

    End Class

End Namespace
