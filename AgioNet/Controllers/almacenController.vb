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
                                            .ProductDescription = DR(5), .Ingresado = DR(6), .Procesado = DR(7)}
                        index += 1
                    Loop
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New PendingProcessWarehouseModel With {.OrderID = "No data", .CustomerName = "No data", _
                                            .ProductType = "No data", .SerialNumber = "No data", .ProductModel = "No data", _
                                            .ProductDescription = "No data", .Ingresado = "No data", .Procesado = "No data"}
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
            Return Me.View
        End Function

        ' 2013.03.10
        ' POST: /almacen/shipping
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

        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader
    End Class

End Namespace
