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
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New OnBillingModel With {.OrderID = DR(0), .PartNumber = DR(1), .SerialNumber = DR(2), _
                                                                           .FailureType = DR(3), .Costo = DR(4), .LeadTime = DR(5)}
                        If index + 1 >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New OnBillingModel With {.OrderID = "No data", .PartNumber = "No data", .SerialNumber = "No data", _
                                                                 .FailureType = "No data", .Costo = "No data", .LeadTime = "No data"}
                    If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                    index += 1
                    ReDim Preserve modelArray(index - 1)
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

        ' 2013.05.13
        ' POST: /facturacion/OnBilling
        <Authorize, HttpPost> _
        Public Function OnBilling(ByVal model As AprobarFacturacionModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("bl_AprobarFacturacion", model.OrderID, Me.User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                While DR.Read
                    TempData("ErrMsg") = DR(0)
                End While
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Return Me.RedirectToAction("OnBilling")
        End Function

        ' 2013.05.13
        ' GET: /facturacion/aprobar_facturacion
        <Authorize, HttpGet> _
        Public Function aprobar_facturacion(ByVal model As AprobarFacturacionModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("bl_AprobarFacturacion", model.OrderID, Me.User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                While DR.Read
                    TempData("ErrMsg") = DR(0)
                End While
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Return Me.RedirectToAction("OnBilling")
        End Function

        
        ' 2013.03.11
        ' GET: /facturacion/to_invoiced
        <Authorize> _
        Public Function to_invoice() As ActionResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As OnBillingModel
            Dim index As Integer = 0
            Try
                DR = DA.ExecuteSP("bl_AprobadosFacturacion")
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New OnBillingModel With {.OrderID = DR(0), .PartNumber = DR(1), .SerialNumber = DR(2), _
                                                                           .FailureType = DR(3), .Costo = DR(4), .LeadTime = DR(5)}
                        If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New OnBillingModel With {.OrderID = "No data", .PartNumber = "No data", .SerialNumber = "No data", _
                                                                 .FailureType = "No data", .Costo = "No data", .LeadTime = "No data"}
                    If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                    index += 1
                    ReDim Preserve modelArray(index - 1)
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
        ' POST: /facturacion/to_invoice
        <Authorize, HttpPost> _
        Public Function to_invoice(ByVal model As ScanOrderModel) As ActionResult
            Return RedirectToAction("invoice", New With {.OrderID = model.OrderID})
            'Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            'Try
            'Me.DR = Me.DA.ExecuteSP("bl_invoiced", model.OrderID, model.Comment, Me.User.Identity.Name)
            'If DA._LastErrorMessage <> "" Then
            ' Throw New Exception(DA._LastErrorMessage)
            ' End If
            'While DR.Read
            'TempData("ErrMsg") = DR(0)
            'End While
            'Catch ex As Exception
            ' Me.TempData.Item("ErrMsg") = ex.Message
            'Finally
            ' Me.DA.Dispose()
            ' End Try

            'Return Me.RedirectToAction("invoiced")
        End Function


        ' 2013.05.14
        ' GET: /facturacion/invoice
        Public Function invoice(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim Sumatoria As Integer = 0
            Dim eModel As New DatosCotizacionExtModel
            Dim modelArray(100) As PrecioFacturacionModel
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("bl_getPrecioCotizacion", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New PrecioFacturacionModel With {.OrderID = DR(0), .PartNo = DR(1), .Description = DR(2), _
                                            .Precio = DR(3), .Commodity = DR(4), .TipoReparacion = DR(5)}
                        If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New PrecioFacturacionModel With {.OrderID = "no data", .PartNo = "no data", .Description = "no data", _
                                            .Commodity = "no data", .Precio = "no data", .TipoReparacion = "no data"}
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

        '2013.05.15
        ' POST: /facturacion/invoice
        <Authorize, HttpPost> _
        Public Function invoice(ByVal model As facturarModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim response As ActionResult
            Dim ItemID As String = String.Empty

            Try
                model.SubTotal = Trim(model.SubTotal.Replace("$", ""))
                model.SubTotal = Trim(model.SubTotal.Replace(",", ""))
                model.IVA = Trim(model.IVA.Replace("$", ""))
                model.IVA = Trim(model.IVA.Replace(",", ""))
                model.Total = Trim(model.Total.Replace("$", ""))
                model.Total = Trim(model.Total.Replace(",", ""))

                If model.Total = "0.00" Then
                    Throw New Exception("Error! Antes de guardar, debe calcular los valores")
                End If

                '-- Guardar el item de la factura
                DR = DA.ExecuteSP("bl_AddFacturaItem", model.NoFactura, model.OrderID, model.SubTotal, model.IVA, _
                                                       model.Total, model.Comentario, User.Identity.Name)

                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                'Obtener el ItemID
                While DR.Read
                    ItemID = DR(0)
                End While

                ' Limpiar el DR
                If Not DR.IsClosed Then
                    DR.Close()
                End If

                '-- Guardar los registros por separado 
                Dim cont As Integer = 0
                Dim PrecioInt As String
                While model.SubOrder.Length > cont
                    PrecioInt = Trim(model.Precio(cont).Replace("$", ""))
                    PrecioInt = Trim(PrecioInt.Replace(",", ""))
                    DR = DA.ExecuteSP("bl_AddDesgloseItem", ItemID, model.SubOrder(cont), model.PartNo(cont), PrecioInt)
                    If DA._LastErrorMessage <> "" Then
                        Throw New Exception(DA._LastErrorMessage)
                    End If
                    If Not DR.IsClosed Then
                        DR.Close()
                    End If
                    cont += 1
                End While

                response = RedirectToAction("to_invoice")
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                response = RedirectToAction("to_invoice")
            Finally
                DA.Dispose()
            End Try

            Return response
        End Function

        '--------------------------------------------------------------------
        ' 2013.03.11
        ' GET: /facturacion/to_invoice_alt
        <Authorize> _
        Public Function to_invoice_alt() As ActionResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As OnBillingModel
            Dim index As Integer = 0
            Try
                DR = DA.ExecuteSP("bl_AprobadosFacturacionTerceros")
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New OnBillingModel With {.OrderID = DR(0), .PartNumber = DR(1), .SerialNumber = DR(2), _
                                                                           .FailureType = DR(3), .Costo = DR(4), .LeadTime = DR(5)}
                        If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New OnBillingModel With {.OrderID = "No data", .PartNumber = "No data", .SerialNumber = "No data", _
                                                                 .FailureType = "No data", .Costo = "No data", .LeadTime = "No data"}
                    If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                    index += 1
                    ReDim Preserve modelArray(index - 1)
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
        ' POST: /facturacion/to_invoice_alt
        <Authorize, HttpPost> _
        Public Function to_invoice_alt(ByVal model As ScanOrderModel) As ActionResult
            Return RedirectToAction("invoice_alt", New With {.OrderID = model.OrderID})
        End Function

        ' 2013.05.30
        ' GET: /facturacion/invoice_alt
        Public Function invoice_alt(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim Sumatoria As Integer = 0
            Dim eModel As New DatosCotizacionExtModel
            Dim modelArray(100) As PrecioFacturacionModel
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("bl_getPrecioCotizaciónTerceros", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New PrecioFacturacionModel With {.OrderID = DR(0), .PartNo = DR(1), .Description = DR(2), _
                                            .Precio = DR(3), .Commodity = DR(4), .TipoReparacion = DR(5)}
                        If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New PrecioFacturacionModel With {.OrderID = "no data", .PartNo = "no data", .Description = "no data", _
                                            .Commodity = "no data", .Precio = "no data", .TipoReparacion = "no data"}
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

        '2013.05.15
        ' POST: /facturacion/invoice_alt
        <Authorize, HttpPost> _
        Public Function invoice_alt(ByVal model As facturarModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim response As ActionResult
            Dim ItemID As String = String.Empty

            Try
                model.SubTotal = Trim(model.SubTotal.Replace("$", ""))
                model.SubTotal = Trim(model.SubTotal.Replace(",", ""))
                model.IVA = Trim(model.IVA.Replace("$", ""))
                model.IVA = Trim(model.IVA.Replace(",", ""))
                model.Total = Trim(model.Total.Replace("$", ""))
                model.Total = Trim(model.Total.Replace(",", ""))

                If model.Total = "0.00" Then
                    Throw New Exception("Error! Antes de guardar, debe calcular los valores")
                End If

                '-- Guardar el item de la factura
                DR = DA.ExecuteSP("bl_AddFacturaItem", model.NoFactura, model.OrderID, model.SubTotal, model.IVA, _
                                                       model.Total, model.Comentario, User.Identity.Name)

                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                'Obtener el ItemID
                While DR.Read
                    ItemID = DR(0)
                End While

                ' Limpiar el DR
                If Not DR.IsClosed Then
                    DR.Close()
                End If

                '-- Guardar los registros por separado 
                Dim cont As Integer = 0
                Dim PrecioInt As String
                While model.SubOrder.Length > cont
                    PrecioInt = Trim(model.Precio(cont).Replace("$", ""))
                    PrecioInt = Trim(PrecioInt.Replace(",", ""))
                    DR = DA.ExecuteSP("bl_AddDesgloseItem", ItemID, model.SubOrder(cont), model.PartNo(cont), PrecioInt)
                    If DA._LastErrorMessage <> "" Then
                        Throw New Exception(DA._LastErrorMessage)
                    End If
                    If Not DR.IsClosed Then
                        DR.Close()
                    End If
                    cont += 1
                End While

                response = RedirectToAction("to_invoice")
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                response = RedirectToAction("to_invoice")
            Finally
                DA.Dispose()
            End Try

            Return response
        End Function
        '--------------------------------------------------------------------

        ' 2013.05.15 
        ' GET: /facturación/send_invoice
        <Authorize> _
        Public Function send_invoice() As ActionResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As facturaListModel
            Dim index As Integer = 0
            Try
                DR = DA.ExecuteSP("bl_EnviarFacturaList")
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New facturaListModel With {.NoFactura = DR(0), .SubTotal = DR(1), .IVA = DR(2), .Total = DR(3)}
                        If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New facturaListModel With {.NoFactura = "No data", .SubTotal = "No data", .IVA = "No data", .Total = "No data"}
                    If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                    index += 1
                    ReDim Preserve modelArray(index - 1)
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

        ' 2013.05.16 
        ' GET: /facturacion/sendinvoice
        <Authorize> _
        Public Function sendinvoice(ByVal model As ScanFacturaModel) As ActionResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As facturaDetailModel
            Dim Suma As New facturaListModel

            Dim index As Integer = 0
            Try
                DR = DA.ExecuteSP("bl_EnviarFacturaDetail", model.NoFactura, "DETAIL")
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New facturaDetailModel With {.NoFactura = DR(0), .OrderID = DR(1), .SubTotal = DR(2), .IVA = DR(3), .Total = DR(4)}
                        If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New facturaDetailModel With {.NoFactura = "No data", .OrderID = "No data", .SubTotal = "No data", .IVA = "No data", .Total = "No data"}
                    If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                    index += 1
                    ReDim Preserve modelArray(index - 1)
                End If

                '-- Limpiar el DR
                If Not DR.IsClosed Then
                    DR.Close()
                End If

                DR = DA.ExecuteSP("bl_EnviarFacturaDetail", model.NoFactura)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        Suma = New facturaListModel With {.NoFactura = DR(0), .SubTotal = DR(1), .IVA = DR(2), .Total = DR(3)}
                    Loop
                Else
                    Suma = New facturaListModel With {.NoFactura = "No data", .SubTotal = "No data", .IVA = "No data", .Total = "No data"}
                End If

                '-----
                TempData("Model") = modelArray
                TempData("Suma") = Suma
                TempData("NoFactura") = model.NoFactura

            Catch ex As Exception
                TempData("ErrMsg") = ex.Message
                Return RedirectToAction("Index")
            Finally
                DA.Dispose()
            End Try

            Return Me.View
        End Function

        ' 2013.05.16
        ' POST: /facturacion/sendinvoice
        <Authorize, HttpPost> _
        Public Function sendinvoice(ByVal model As ScanFacturaModel, Optional ByVal Reference As String = "") As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("bl_EnviarFactura ", model.NoFactura, Me.User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                While DR.Read
                    TempData("ErrMsg") = DR(0)
                End While
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Return Me.RedirectToAction("send_invoice")

        End Function


        ' 2013.03.11
        ' GET: /facturacion/paid
        <Authorize> _
        Public Function paid() As ActionResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As facturaListModel
            Dim index As Integer = 0
            Try
                DR = DA.ExecuteSP("bl_PendientesPagar")
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New facturaListModel With {.NoFactura = DR(0), .SubTotal = DR(1), .IVA = DR(2), .Total = DR(3)}
                        If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New facturaListModel With {.NoFactura = "No data", .SubTotal = "No data", .IVA = "No data", .Total = "No data"}
                    If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                    index += 1
                    ReDim Preserve modelArray(index - 1)
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
        ' POST: /facturacion/paid
        <Authorize, HttpPost> _
        Public Function paid(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("bl_paid ", model.OrderID, model.Comment, Me.User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                While DR.Read
                    TempData("ErrMsg") = DR(0)
                End While
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Return Me.RedirectToAction("paid")
        End Function

        ' 2013.07.09
        ' GET: /facturacion/pagadas
        <Authorize> _
        Public Function pagadas() As ActionResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As facturaListModel
            Dim index As Integer = 0
            Try
                DR = DA.ExecuteSP("bl_OrdenesPagadas")
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New facturaListModel With {.NoFactura = DR(0), .SubTotal = DR(1), .IVA = DR(2), .Total = DR(3)}
                        index += 1
                        If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                    Loop
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New facturaListModel With {.NoFactura = "No data", .SubTotal = "No data", .IVA = "No data", .Total = "No data"}
                    If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                    index += 1
                    ReDim Preserve modelArray(index - 1)
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
        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader
    End Class
End Namespace
