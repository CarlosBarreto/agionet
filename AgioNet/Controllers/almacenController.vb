Imports Root.Reports
Imports System.Data.SqlClient

Namespace AgioNet
    Public Class almacenController
        Inherits System.Web.Mvc.Controller

        '2013.01.14
        ' GET: /almacen/Index
        <Authorize> _
        Public Function Index() As ActionResult
            Return Me.View
        End Function

        '2013.02.14
        ' GET: /almacen/recibo_almacen
        <Authorize> _
        Public Function recibo_almacen() As ActionResult
            Return Me.View
        End Function

        ' 2013.02.14
        ' POST: /almacen/recibo_almacen
        <Authorize, HttpPost> _
        Public Function recibo_almacen(ByVal model As setCheckinModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("rc_setCheckIn", model.OrderID, model.Comment, model.Guia, model.lenght, model.Width, model.Height, _
                                        model.Weight, model.Dimweight, model.Shipweight, Me.User.Identity.Name)
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

            Return Me.RedirectToAction("recibo_almacen")
        End Function

        ' 2013.02.14
        ' GET: /almacen/trasnfer_incoming
        <Authorize> _
        Public Function transfer_incoming() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As PendingOrdersModel
            Dim index As Integer = 0
            Dim response As ActionResult

            ' -- Limpiar el TempData
            'TempData.Clear()

            Try
                Me.DR = Me.DA.ExecuteSP("rc_getTransferIncoming")
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New PendingOrdersModel With {.OrderID = DR(0), .Customer = DR(1), .ProductType = DR(2), _
                                            .SerialNo = DR(3), .Model = DR(4), .Description = DR(5), .DateR = DR(9)}
                        index += 1
                    Loop
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New PendingOrdersModel With {.OrderID = "No Data", .Customer = "No Data", .ProductType = "No Data", _
                        .SerialNo = "No Data", .Model = "No Data", .Description = "No Data", .DateR = "No Data"}
                    index += 1
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If
                response = Me.View
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                response = Me.RedirectToAction("index")
            Finally
                DA.Dispose()
            End Try

            Me.TempData.Item("Model") = modelArray
            Return response
        End Function

        ' 2013.02.14
        ' POST: /almacen/transfer_incoming
        <HttpPost, Authorize> _
        Public Function transfer_incoming(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("rc_setTransferIncoming", model.OrderID, Me.User.Identity.Name, model.Comment)
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

            Return Me.RedirectToAction("transfer_incoming") ' Me.RedirectToAction("recibo_almacen")
        End Function

        ' Methods
        '2013.02.26
        ' GET: /almacen/checkin_report
        <Authorize> _
        Function checkin_report(ByVal model As CheckinReportOptModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim mymodel(1000) As CheckinReportModel
            Dim num As Integer = 0

            Try
                DR = Me.DA.ExecuteSP("rc_CheckingReport", model.StartDate, model.EndDate, model.OrderID)
                If Me.DA._LastErrorMessage = "" Then
                    num = 0
                    Do While DR.Read
                        mymodel(num) = New CheckinReportModel With {.OrderID = DR(0), .OrderDate = DR(1), .FechaIngreso = DR(2), _
                                       .Guia = DR(3), .Longitud = DR(4), .Ancho = DR(5), .Altura = DR(6), .PesoEmbarque = DR(7), _
                                       .Comment = DR(8), .CreateBy = DR(9)}
                        num += 1
                    Loop
                    ReDim Preserve mymodel(num - 1)
                Else
                    Throw New Exception(DA._LastErrorMessage) '("Error! No se encontraron datos")
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Me.TempData("Model") = mymodel
            Me.TempData("optModel") = model
            Return Me.View
        End Function

        ' 2013.03.10
        ' GET: /almacen/process_warehouse
        <Authorize> _
        Public Function process_warehouse() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As PendingProcessWarehouseModel
            Dim index As Integer = 0
            Dim response As ActionResult

            ' -- Limpiar el TempData
            'TempData.Clear()

            Try
                Me.DR = Me.DA.ExecuteSP("pk_PendientesProcessWarehouse")
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New PendingProcessWarehouseModel With {.OrderID = DR(0), .CustomerName = DR(1), _
                                            .ProductType = DR(2), .SerialNumber = DR(3), .ProductModel = DR(4), _
                                            .ProductDescription = DR(5), .Ingresado = DR(6), .Procesado = DR(7), .DateComp = DR(8)}
                        index += 1
                    Loop
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New PendingProcessWarehouseModel With {.OrderID = "No data", .CustomerName = "No data", _
                                            .ProductType = "No data", .SerialNumber = "No data", .ProductModel = "No data", _
                                            .ProductDescription = "No data", .Ingresado = "No data", .Procesado = "No data", .DateComp = "No data"}
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If
                response = Me.View
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                response = Me.RedirectToAction("index")
            Finally
                DA.Dispose()
            End Try

            Me.TempData.Item("Model") = modelArray
            Return response
        End Function

        ' 2013.03.10
        ' POST: /almacen/process_warehouse
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
        ' GET: /almacen/shipping
        <Authorize> _
        Public Function shipping() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As PendingProcessWarehouseModel
            Dim index As Integer = 0
            Dim response As ActionResult

            ' -- Limpiar el TempData
            'TempData.Clear()

            Try
                Me.DR = Me.DA.ExecuteSP("pk_PendientesEmbarqueProdutoTerminado")
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New PendingProcessWarehouseModel With {.OrderID = DR(0), .CustomerName = DR(1), _
                                            .ProductType = DR(2), .SerialNumber = DR(3), .ProductModel = DR(4), _
                                            .ProductDescription = DR(5), .Ingresado = DR(6), .Procesado = DR(7), .DateComp = DR(8)}
                        index += 1
                    Loop
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New PendingProcessWarehouseModel With {.OrderID = "No data", .CustomerName = "No data", _
                                            .ProductType = "No data", .SerialNumber = "No data", .ProductModel = "No data", _
                                            .ProductDescription = "No data", .Ingresado = "No data", .Procesado = "No data", .DateComp = "No data"}
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If
                response = Me.View
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                response = Me.RedirectToAction("index")
            Finally
                DA.Dispose()
            End Try

            Me.TempData.Item("Model") = modelArray
            Return response
        End Function

        ' 2013.03.10
        ' POST: /almacen/shipping
        <Authorize, HttpPost> _
        Public Function shipping(ByVal model As ShippingModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)

            Try
                Me.DR = Me.DA.ExecuteSP("pk_shipping", model.OrderID, model.TRackNo, model.Comment, Me.User.Identity.Name)
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

        ' 2013.05.24
        ' GET: /almacen/partes_compradas
        <Authorize, HttpGet> _
        Public Function partes_compradas() As ActionResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As ReciboPartesCompradasModel
            Dim index As Integer = 0
            Dim response As ActionResult

            ' -- Limpiar el TempData
            'TempData.Clear()

            Try
                Me.DR = Me.DA.ExecuteSP("wh_getPartesCompradas")
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New ReciboPartesCompradasModel With {.OrderID = DR(0), .PartNo = DR(1), .Description = DR(2),
                                            .Proveedor = DR(3), .Comentario = DR(4), .LeadTime = DR(5), .CostDate = DR(6), .TrackNo = DR(7)}
                        If index + 1 >= modelArray.Length Then ReDim Preserve modelArray(index + 10)
                        index += 1
                    Loop
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New ReciboPartesCompradasModel With {.OrderID = "No data", .PartNo = "No data", .Description = "No data",
                                            .Proveedor = "No data", .Comentario = "No data", .LeadTime = "No data", .CostDate = "No data", .TrackNo = "No data"}
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If
                response = Me.View
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                response = Me.RedirectToAction("index")
            Finally
                DA.Dispose()
            End Try

            Me.TempData.Item("Model") = modelArray
            Return response
        End Function

        ' 2013.05.24
        ' POST: /almacen/partes_compradas : Buscar
        <Authorize, HttpPost> _
        <MultiButton(MatchFormKey:="action", MatchFormValue:="Buscar")> _
        Public Function partes_compradas(ByVal model As ReciboPartesCompradasModel) As ActionResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As ReciboPartesCompradasModel
            Dim index As Integer = 0
            Dim response As ActionResult

            ' -- Limpiar el TempData
            'TempData.Clear()

            Try
                Me.DR = Me.DA.ExecuteSP("wh_getPartesCompradas", model.OrderID, model.TrackNo, model.PartNo, model.Proveedor)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New ReciboPartesCompradasModel With {.OrderID = DR(0), .PartNo = DR(1), .Description = DR(2),
                                            .Proveedor = DR(3), .Comentario = DR(4), .LeadTime = DR(5), .CostDate = DR(6), .TrackNo = DR(7)}
                        index += 1
                    Loop
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New ReciboPartesCompradasModel With {.OrderID = "No data", .PartNo = "No data", .Description = "No data",
                                            .Proveedor = "No data", .Comentario = "No data", .LeadTime = "No data", .CostDate = "No data", .TrackNo = "No data"}
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If
                response = Me.View
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                response = Me.RedirectToAction("index")
            Finally
                DA.Dispose()
            End Try

            Me.TempData.Item("Model") = modelArray
            Return response
        End Function

        ' 2013.05.24
        ' POST: /almacen/partes_compradas : Recibir
        <Authorize, HttpPost> _
        <MultiButton(MatchFormKey:="action", MatchFormValue:="Recibir")> _
        Public Function partes_compradar(ByVal model As ReciboPartesCompradasModel) As ActionResult
            Dim response As ActionResult

            Try
                If model.OrderID = "" Then
                    Throw New Exception("Error! La Orden no puede quedar en blanco para esta operación")
                End If

                response = Me.RedirectToAction("partescompradas", "almacen", New With {.OrderID = model.OrderID})
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                response = Me.RedirectToAction("partes_compradas")
            End Try

            Return response
        End Function

        ' 2013.05.24
        ' GET: /almacen/partes_compradas
        <Authorize> _
        Public Function partescompradas(ByVal m As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model As New ReciboPartesCompradasModel
            Dim index As Integer = 0
            Dim response As ActionResult

            ' -- Limpiar el TempData
            'TempData.Clear()

            Try
                Me.DR = Me.DA.ExecuteSP("wh_getPartesCompradas", m.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        model = New ReciboPartesCompradasModel With {.OrderID = DR(0), .PartNo = DR(1), .Description = DR(2),
                                            .Proveedor = DR(3), .Comentario = DR(4), .LeadTime = DR(5), .CostDate = DR(6), .TrackNo = DR(7)}
                    Loop
                Else
                    model = New ReciboPartesCompradasModel With {.OrderID = "No data", .PartNo = "No data", .Description = "No data",
                                            .Proveedor = "No data", .Comentario = "No data", .LeadTime = "No data", .CostDate = "No data", .TrackNo = "No data"}
                End If
                response = Me.View
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                response = Me.RedirectToAction("index")
            Finally
                DA.Dispose()
            End Try

            Me.TempData.Item("Model") = model
            Return response
        End Function

        ' 2013.05.27
        ' POST: /almacen/partescompradas
        <Authorize, HttpPost> _
        Public Function partescompradas(ByVal model As SavePartesCompradasModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim response As ActionResult
            Try
                Me.DR = Me.DA.ExecuteSP("wh_SavePartesCompradas", model.OrderID, model.PartNo, model.TrackNo, model.Comentario, User.Identity.Name)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                response = Me.RedirectToAction("partes_compradas")
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                response = Me.View
            Finally
                DA.Dispose()
            End Try

            Me.TempData.Item("Model") = model
            Return response
        End Function

        ' 2013.06.10
        ' GET: /almacen/proceso_embarque
        <Authorize> _
        Public Function proceso_embarque(ByVal model As CheckinReportOptModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As PendingProcessWarehouseModel
            Dim index As Integer = 0
            Dim response As ActionResult

            ' -- Limpiar el TempData
            'TempData.Clear()

            Try
                Me.DR = Me.DA.ExecuteSP("pk_PendientesEmbarqueProdutoTerminado", model.StartDate, model.EndDate, model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New PendingProcessWarehouseModel With {.OrderID = DR(0), .CustomerName = DR(1), _
                                            .ProductType = DR(2), .SerialNumber = DR(3), .ProductModel = DR(4), _
                                            .ProductDescription = DR(5), .Ingresado = DR(6), .Procesado = DR(7), .DateComp = DR(8)}
                        index += 1
                    Loop
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New PendingProcessWarehouseModel With {.OrderID = "No data", .CustomerName = "No data", _
                                            .ProductType = "No data", .SerialNumber = "No data", .ProductModel = "No data", _
                                            .ProductDescription = "No data", .Ingresado = "No data", .Procesado = "No data", .DateComp = "No data"}
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If
                response = Me.View
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                response = Me.RedirectToAction("index")
            Finally
                DA.Dispose()
            End Try

            Me.TempData.Item("Model") = modelArray
            Me.TempData.Item("OptModel") = model
            Return response
        End Function

        ' 2013.06.10
        'GET: /almacen/ordenes_embarcadas
        Public Function ordenes_embarcadas(ByVal model As OrdenesEmbarcadasModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As OrdenesEmbarcadasModel
            Dim index As Integer = 0
            Dim response As ActionResult

            ' -- Limpiar el TempData
            'TempData.Clear()

            Try
                Me.DR = Me.DA.ExecuteSP("wh_shippingReport", model.OrderID, model.TrackNo, model.dateStart, model.dateEnd)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New OrdenesEmbarcadasModel With {.OrderID = DR(0), .TrackNo = DR(1), _
                                            .Comment = DR(2), .ShipDate = DR(3), .ShipBy = DR(4)}
                        If index + 1 >= modelArray.Length Then ReDim Preserve modelArray(index * 100)
                        index += 1
                    Loop
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New OrdenesEmbarcadasModel With {.OrderID = "No data", .TrackNo = "No data", _
                                            .Comment = "No data", .ShipDate = "No data", .ShipBy = "No data"}
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If
                response = Me.View
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                response = Me.RedirectToAction("index")
            Finally
                DA.Dispose()
            End Try

            Me.TempData.Item("Model") = modelArray
            Return response
        End Function
        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader
    End Class

End Namespace
