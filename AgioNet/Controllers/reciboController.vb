Imports Root.Reports
Imports System.Data.SqlClient

Namespace AgioNet
    Public Class reciboController
        Inherits System.Web.Mvc.Controller

        ' Methods
        ' 2013.02.14
        ' GET: /recibo/formulario_recibo
        <HttpGet, Authorize> _
        Public Function formulario_recibo() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Dim str As String = Me.getReRepair(Me.TempData.Item("OrderID"), Me.DA)
                Dim str2 As String = Me.getWarranty(Me.TempData.Item("OrderID"), Me.DA)

                Me.TempData.Keep("OrderID")
                Me.TempData.Item("ReRepair") = str
                Me.TempData.Item("Warranty") = str2

            Catch ex As Exception
                Me.TempData.Item("MsgErr") = ex.Message
                Return Me.View
            End Try

            Return Me.View
        End Function

        ' 2013.02.14
        ' POST: /recibo/formulario_recibo
        <Authorize, HttpPost> _
        Public Function formulario_recibo(ByVal model As ReceiveIndfoModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = String.Empty
            Dim model2 As ReceiveIndfoModel = model

            Try
                Me.DR = Me.DA.ExecuteSP("rc_regReceiveInfo", model2.OrderID, model2.PackType, model2.PackDamage, model2.NonDocPack, _
                                        model2.CorrectPack, model2.Accesories, model2.Cosmetic, model2.Warranty, model2.rerepair, _
                                        model2.Comment, Me.User.Identity.Name)

                If (Me.DA.LastErrorMessage = "") Then
                    Do While Me.DR.Read
                        str = DR(0)
                    Loop
                Else
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Return Me.View
                End If

                If Not Me.DR.IsClosed Then
                    Me.DR.Close()
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.View
            Finally
                Me.DA.Dispose()
            End Try

            Me.TempData.Item("ErrMsg") = str
            Me.TempData.Item("OrderID") = model.OrderID
            Return Me.RedirectToAction("print_chkpage")
        End Function

        ' 2013.02.14
        ''' <summary>
        '''  Obtiene el valor de Rerepair de una Orden
        ''' </summary>
        ''' <param name="OrderID"></param>
        ''' <param name="_DA_"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getReRepair(ByVal OrderID As String, ByRef _DA_ As DataAccess) As String
            If (_DA_ Is Nothing) Then
                _DA_ = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            End If

            Dim str2 As String = String.Empty
            Dim reader As SqlDataReader = _DA_.ExecuteSP("rc_getReRepair", OrderID)
            If reader.HasRows Then
                Do While reader.Read
                    str2 = reader(0)
                Loop
            End If

            If Not reader.IsClosed Then
                reader.Close()
            End If

            Return str2
        End Function

        ' 2013.02.14
        ''' <summary>
        ''' Obtiene la cantidad de dias desde el ultimo ingreso para saber si está en garantía
        ''' </summary>
        ''' <param name="OrderID"></param>
        ''' <param name="_DA_"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getWarranty(ByVal OrderID As String, ByRef _DA_ As DataAccess) As String
            If (_DA_ Is Nothing) Then
                _DA_ = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            End If
            Dim str2 As String = String.Empty
            Dim reader As SqlDataReader = _DA_.ExecuteSP("rc_getWarranty", OrderID)

            If reader.HasRows Then
                Do While reader.Read
                    str2 = reader(0)
                Loop
            End If
            If Not reader.IsClosed Then
                reader.Close()
            End If
            Return str2
        End Function

        ' 2013.02.14
        ' GET: /recibo/imprimir_hoja
        <Authorize, HttpGet> _
        Public Function imprimir_hoja() As ActionResult
            Return Me.View
        End Function

        ' 2013.01.14
        ' POST: /recibo/imprimir_hoja
        <HttpPost, Authorize> _
        Public Function imprimir_hoja(ByVal model As ScanOrderModel) As ActionResult
            Me.TempData.Item("OrderID") = model.OrderID
            Return Me.RedirectToAction("print_chkpage")
        End Function

        '2013.01.14
        ' GET: /recibo/Index
        <Authorize> _
        Public Function Index() As ActionResult
            Return Me.View
        End Function

        ' 2013.01.14
        ' GET: /recibo/ingresar_orden
        <Authorize> _
        Public Function ingresar_orden() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As PendingOrdersModel
            Dim index As Integer = 0

            Try
                Me.DR = Me.DA.ExecuteSP("rc_getPendingOrders")
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("index")
                End If
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        Dim model As New PendingOrdersModel With { _
                            .OrderID = DR(0), _
                            .Customer = DR(1), _
                            .ProductType = DR(2), _
                            .SerialNo = DR(3), _
                            .Model = DR(4), _
                            .Description = DR(5) _
                        }
                        modelArray(index) = model
                        index += 1
                    Loop
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New PendingOrdersModel With { _
                        .OrderID = "No Data", _
                        .Customer = "No Data", _
                        .ProductType = "No Data", _
                        .SerialNo = "No Data", _
                        .Model = "No Data", _
                        .Description = "No Data" _
                    }
                    index += 1
                    ' -- Actualizado por Carlos Barreto
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

            Me.TempData.Item("Opt") = False
            Me.TempData.Item("Model") = modelArray
            Return Me.View
        End Function

        ' 2013.01.14
        ' POST: /recibo/ingresar_orden
        <HttpPost, Authorize> _
        Public Function ingresar_orden(ByVal model As ScanOrderModel) As Object
            Me.TempData.Item("Opt") = True
            Me.TempData.Item("OrderID") = model.OrderID
            Return Me.RedirectToAction("scan_info")
        End Function

        ' 2013.02.14
        ' GET: /recibo/LoadOrderIn
        <Authorize> _
        Public Function LoadOrderInfo() As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model As New OrderInfoModel
            Dim num As Integer = 0

            Try
                Dim model2 As OrderInfoModel
                Me.DR = Me.DA.ExecuteSP("sc_getOrderInfo", Me.TempData.Item("OrderID"))
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()

                    model2 = New OrderInfoModel With { _
                        .OrderID = "No Data", _
                        .OrderDate = "No Data", _
                        .Flete = "No Data", _
                        .CustomerType = "No Data", _
                        .CustomerName = "No Data", _
                        .RazonSocial = "No Data", _
                        .Reference = "No Data", _
                        .RFC = "No Data", _
                        .Email = "No Data", _
                        .Address = "No Data", _
                        .INumber = "No Data", _
                        .ENumber = "No Data", _
                        .Address2 = "No Data", _
                        .City = "No Data", _
                        .State = "No Data", _
                        .Country = "No Data", _
                        .ZipCode = "No Data", _
                        .Tel = "No Data", _
                        .Tel2 = "No Data", _
                        .Tel3 = "No Data", _
                        .Delivery = "No Data", _
                        .DeliveryTime = "No Data", _
                        .ProductClass = "No Data", _
                        .ProductType = "No Data", _
                        .Trademark = "No Data", _
                        .Model = "No Data", _
                        .Description = "No Data", _
                        .PartNo = "No Data", _
                        .SerialNo = "No Data", _
                        .Revision = "No Data", _
                        .ServiceType = "No Data", _
                        .FailureType = "No Data", _
                        .Comment = "No Data" _
                    }
                    model = model2
                    Me.TempData.Item("Model") = model
                    Return Me.PartialView("_OrderInfoPartial")
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        model2 = New OrderInfoModel With { _
                            .OrderID = DR(0), _
                            .OrderDate = DR(1), _
                            .Flete = DR(2), _
                            .CustomerType = DR(3), _
                            .CustomerName = DR(4), _
                            .RazonSocial = DR(5), _
                            .Reference = DR(6), _
                            .RFC = DR(7), _
                            .Email = DR(8), _
                            .Address = DR(9), _
                            .INumber = DR(10), _
                            .ENumber = DR(11), _
                            .Address2 = DR(12), _
                            .City = DR(13), _
                            .State = DR(14), _
                            .Country = DR(15), _
                            .ZipCode = DR(16), _
                            .Tel = DR(17), _
                            .Tel2 = DR(18), _
                            .Tel3 = DR(19), _
                            .Delivery = DR(20), _
                            .DeliveryTime = DR(21), _
                            .ProductClass = DR(22), _
                            .ProductType = DR(23), _
                            .Trademark = DR(24), _
                            .Model = DR(25), _
                            .Description = DR(26), _
                            .PartNo = DR(27), _
                            .SerialNo = DR(28), _
                            .Revision = DR(29), _
                            .ServiceType = DR(30), _
                            .FailureType = DR(31), _
                            .Comment = DR(32) _
                        }
                        model = model2
                    Loop

                    If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                        Me.DR.Close()
                    End If
                    Me.DA.Dispose()
                Else
                    model2 = New OrderInfoModel With { _
                        .OrderID = "No Data", _
                        .OrderDate = "No Data", _
                        .Flete = "No Data", _
                        .CustomerType = "No Data", _
                        .CustomerName = "No Data", _
                        .RazonSocial = "No Data", _
                        .Reference = "No Data", _
                        .RFC = "No Data", _
                        .Email = "No Data", _
                        .Address = "No Data", _
                        .INumber = "No Data", _
                        .ENumber = "No Data", _
                        .Address2 = "No Data", _
                        .City = "No Data", _
                        .State = "No Data", _
                        .Country = "No Data", _
                        .ZipCode = "No Data", _
                        .Tel = "No Data", _
                        .Tel2 = "No Data", _
                        .Tel3 = "No Data", _
                        .Delivery = "No Data", _
                        .DeliveryTime = "No Data", _
                        .ProductClass = "No Data", _
                        .ProductType = "No Data", _
                        .Trademark = "No Data", _
                        .Model = "No Data", _
                        .Description = "No Data", _
                        .PartNo = "No Data", _
                        .SerialNo = "No Data", _
                        .Revision = "No Data", _
                        .ServiceType = "No Data", _
                        .FailureType = "No Data", _
                        .Comment = "No Data" _
                    }
                    model = model2
                    num += 1
                End If

            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Me.DA.Dispose()
            End Try

            Me.TempData.Item("Model") = model
            Return Me.PartialView("_OrderInfoPartial")
        End Function

        '2013.02.14
        ' GET: /recibo/print_chkpage
        <HttpGet, Authorize> _
        Public Function print_chkpage() As ActionResult
            Dim code As String = Me.TempData.Item("OrderID")
            Dim data As New PrintData
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)

            Try
                Me.DR = Me.DA.ExecuteSP("rc_getPrintData", code)
                If (Me.DA.LastErrorMessage = "") Then
                    Do While Me.DR.Read
                        data.ReportedFailure = DR(0)
                        data.OrderId = DR(1)
                        data.SerialNo = DR(2)
                        data.PartNo = DR(3)
                        data.Model = DR(4)
                        data.TrackNo = DR(5)
                        data.ScanDate = DR(6)
                        data.ScanBy = DR(7)
                        data.PackType = DR(9)
                        data.PackDamage = DR(10)
                        data.NonDoucumentDamage = DR(11)
                        data.CorrectPack = DR(12)
                        data.Accesories = DR(13)
                        data.Cosmetic = DR(14)
                        data.Warranty = DR(15)
                        data.ReRepair = DR(16)
                        data.Comment = DR(17)
                    Loop

                    Dim path As String = (Me.Server.MapPath("~/Content/temp/") & code & ".jpg")
                    System.IO.File.WriteAllBytes(path, GenerarCodigo(Me.Server, code, "", 350, 40, 60))

                    Dim report As New PDFHojaViajera With { _
                        .RutaLogo = Me.Server.MapPath("~/Content/images/logo-01.jpg"), _
                        .RutaBarCode = path, _
                        .Model = data _
                    }

                    'RT.PrintPDF(report)
                    report = Nothing
                Else
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Return Me.RedirectToAction("imprimir_hoja")
                End If

                If Not Me.DR.IsClosed Then
                    Me.DR.Close()
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("imprimir_hoja")
            Finally
                DA.Dispose()
            End Try

            Me.TempData.Item("PrintData") = data
            Me.TempData.Keep("ErrMsg")
            Me.TempData.Item("OrderID") = code

            Return Me.RedirectToAction("imprimir_hoja")
        End Function

        '2013.02.14
        ' GET: /recibo/recibo_almacen
        <Authorize> _
        Public Function recibo_almacen() As ActionResult
            Return Me.View
        End Function

        ' 2013.02.14
        ' POST: /recibo/recibo_almacen
        <Authorize, HttpPost> _
        Public Function recibo_almacen(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("rc_setCheckIn", model.OrderID, Me.User.Identity.Name, model.Comment)
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
        ' POST: /recibo/scan_info
        <HttpPost, Authorize> _
        Public Function scan_info(ByVal mdl As ScanInfoModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = String.Empty
            Dim model As ScanInfoModel = mdl

            Try
                Me.DR = Me.DA.ExecuteSP("rc_regScanInfo", model.OrderID, model.SerialNo, model.PartNo, model.Model, model.TrackNo, _
                                        Me.User.Identity.Name)
                If (Me.DA.LastErrorMessage = "") Then
                    Do While Me.DR.Read
                        str = DR(0)
                    Loop
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

            Return Me.RedirectToAction("formulario_recibo")
        End Function

        ' 2013.02.14
        ' GET: /recibo/scan_info
        <Authorize, HttpGet> _
        Public Function scan_info(ByVal model As ScanOrderModel) As ActionResult
            Try
                'If Me.TempData.Item("Opt") <> "" Then
                'Me.TempData.Keep("OrderID")
                'Else
                Me.TempData.Item("OrderID") = model.OrderID
                'End If
            Catch ex As Exception
                TempData("ErrMsg") = ex.Message
            End Try
            

            Return Me.View
        End Function

        ' 2013.02.14
        ' GET: /recibo/transfer_diagnostico
        <Authorize> _
        Public Function transfer_diagnostic() As ActionResult
            Return Me.View
        End Function

        ' 2013.02.14
        ' POST: /recibo/transfer_diagnostic
        <HttpPost, Authorize> _
        Public Function transfer_diagnostic(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("rc_setTransferDiagnostic", model.OrderID, Me.User.Identity.Name, model.Comment)
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
            Return Me.RedirectToAction("transfer_diagnostic")
        End Function

        ' 2013.02.14
        ' GET: /recibo/trasnfer_incoming
        <Authorize> _
        Public Function transfer_incoming() As ActionResult
            Return Me.View
        End Function

        ' 2013.02.14
        ' POST: /recibo/transfer_incoming
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

            Return Me.RedirectToAction("recibo_almacen")
        End Function


        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader
    End Class


End Namespace
