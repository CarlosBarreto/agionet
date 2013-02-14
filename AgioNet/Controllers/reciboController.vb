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
            Dim reader As SqlDataReader = _DA_.ExecuteSP("rc_getReRepair", New Object() {OrderID})
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

        Public Function getWarranty(ByVal OrderID As String, ByRef _DA_ As DataAccess) As String
            If (_DA_ Is Nothing) Then
                _DA_ = New DataAccess("localhost", "AgioNet v1.3", "aguser", "Agiotech01")
            End If
            Dim str2 As String = String.Empty
            Dim reader As SqlDataReader = _DA_.ExecuteSP("rc_getWarranty", New Object() {OrderID})
            If reader.HasRows Then
                Do While reader.Read
                    str2 = Conversions.ToString(reader.Item(0))
                Loop
            End If
            If Not reader.IsClosed Then
                reader.Close()
            End If
            Return str2
        End Function

        <Authorize, HttpGet> _
        Public Function imprimir_hoja() As ActionResult
            Return Me.View
        End Function

        <HttpPost, Authorize> _
        Public Function imprimir_hoja(ByVal model As ScanOrderModel) As ActionResult
            Me.TempData.Item("OrderID") = model.OrderID
            Return Me.RedirectToAction("print_chkpage")
        End Function

        <Authorize> _
        Public Function Index() As ActionResult
            Return Me.View
        End Function

        <Authorize> _
        Public Function ingresar_orden() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As PendingOrdersModel() = New PendingOrdersModel(&H65 - 1) {}
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("rc_getPendingOrders", New Object(0 - 1) {})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("index")
                End If
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        Dim model As New PendingOrdersModel With { _
                            .OrderID = Conversions.ToString(Me.DR.Item(0)), _
                            .Customer = Conversions.ToString(Me.DR.Item(1)), _
                            .ProductType = Conversions.ToString(Me.DR.Item(2)), _
                            .SerialNo = Conversions.ToString(Me.DR.Item(3)), _
                            .Model = Conversions.ToString(Me.DR.Item(4)), _
                            .Description = Conversions.ToString(Me.DR.Item(5)) _
                        }
                        modelArray(index) = model
                        index += 1
                    Loop
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New PendingOrdersModel(((index - 1) + 1) - 1) {}), PendingOrdersModel())
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
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New PendingOrdersModel(((index - 1) + 1) - 1) {}), PendingOrdersModel())
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsg") = exception.Message
                Dim result As ActionResult = Me.RedirectToAction("index")
                ProjectData.ClearProjectError()
                Return result
                ProjectData.ClearProjectError()
            End Try
            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If
            Me.DA.Dispose()
            Me.TempData.Item("Opt") = False
            Me.TempData.Item("Model") = modelArray
            Return Me.View
        End Function

        <HttpPost, Authorize> _
        Public Function ingresar_orden(ByVal model As ScanOrderModel) As Object
            Me.TempData.Item("Opt") = True
            Me.TempData.Item("OrderID") = model.OrderID
            Return Me.RedirectToAction("scan_info")
        End Function

        <Authorize> _
        Public Function LoadOrderInfo() As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model As New OrderInfoModel
            Dim num As Integer = 0
            Try
                Dim model2 As OrderInfoModel
                Me.DR = Me.DA.ExecuteSP("sc_getOrderInfo", New Object() {RuntimeHelpers.GetObjectValue(Me.TempData.Item("OrderID"))})
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
                            .OrderID = Conversions.ToString(Me.DR.Item(0)), _
                            .OrderDate = Conversions.ToString(Me.DR.Item(1)), _
                            .Flete = Conversions.ToString(Me.DR.Item(2)), _
                            .CustomerType = Conversions.ToString(Me.DR.Item(3)), _
                            .CustomerName = Conversions.ToString(Me.DR.Item(4)), _
                            .RazonSocial = Conversions.ToString(Me.DR.Item(5)), _
                            .Reference = Conversions.ToString(Me.DR.Item(6)), _
                            .RFC = Conversions.ToString(Me.DR.Item(7)), _
                            .Email = Conversions.ToString(Me.DR.Item(8)), _
                            .Address = Conversions.ToString(Me.DR.Item(9)), _
                            .INumber = Conversions.ToString(Me.DR.Item(10)), _
                            .ENumber = Conversions.ToString(Me.DR.Item(11)), _
                            .Address2 = Conversions.ToString(Me.DR.Item(12)), _
                            .City = Conversions.ToString(Me.DR.Item(13)), _
                            .State = Conversions.ToString(Me.DR.Item(14)), _
                            .Country = Conversions.ToString(Me.DR.Item(15)), _
                            .ZipCode = Conversions.ToString(Me.DR.Item(&H10)), _
                            .Tel = Conversions.ToString(Me.DR.Item(&H11)), _
                            .Tel2 = Conversions.ToString(Me.DR.Item(&H12)), _
                            .Tel3 = Conversions.ToString(Me.DR.Item(&H13)), _
                            .Delivery = Conversions.ToString(Me.DR.Item(20)), _
                            .DeliveryTime = Conversions.ToString(Me.DR.Item(&H15)), _
                            .ProductClass = Conversions.ToString(Me.DR.Item(&H16)), _
                            .ProductType = Conversions.ToString(Me.DR.Item(&H17)), _
                            .Trademark = Conversions.ToString(Me.DR.Item(&H18)), _
                            .Model = Conversions.ToString(Me.DR.Item(&H19)), _
                            .Description = Conversions.ToString(Me.DR.Item(&H1A)), _
                            .PartNo = Conversions.ToString(Me.DR.Item(&H1B)), _
                            .SerialNo = Conversions.ToString(Me.DR.Item(&H1C)), _
                            .Revision = Conversions.ToString(Me.DR.Item(&H1D)), _
                            .ServiceType = Conversions.ToString(Me.DR.Item(30)), _
                            .FailureType = Conversions.ToString(Me.DR.Item(&H1F)), _
                            .Comment = Conversions.ToString(Me.DR.Item(&H20)) _
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
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsg") = exception.Message
                Me.DA.Dispose()
                ProjectData.ClearProjectError()
            End Try
            Me.TempData.Item("Model") = model
            Return Me.PartialView("_OrderInfoPartial")
        End Function

        <HttpGet, Authorize> _
        Public Function print_chkpage() As ActionResult
            Dim code As String = Conversions.ToString(Me.TempData.Item("OrderID"))
            Dim data As New PrintData
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("rc_getPrintData", New Object() {code})
                If (Me.DA.LastErrorMessage = "") Then
                    Do While Me.DR.Read
                        data.ReportedFailure = Conversions.ToString(Me.DR.Item(0))
                        data.OrderId = Conversions.ToString(Me.DR.Item(1))
                        data.SerialNo = Conversions.ToString(Me.DR.Item(2))
                        data.PartNo = Conversions.ToString(Me.DR.Item(3))
                        data.Model = Conversions.ToString(Me.DR.Item(4))
                        data.TrackNo = Conversions.ToString(Me.DR.Item(5))
                        data.ScanDate = Conversions.ToString(Me.DR.Item(6))
                        data.ScanBy = Conversions.ToString(Me.DR.Item(7))
                        data.PackType = Conversions.ToString(Me.DR.Item(9))
                        data.PackDamage = Conversions.ToString(Me.DR.Item(10))
                        data.NonDoucumentDamage = Conversions.ToString(Me.DR.Item(11))
                        data.CorrectPack = Conversions.ToString(Me.DR.Item(12))
                        data.Accesories = Conversions.ToString(Me.DR.Item(13))
                        data.Cosmetic = Conversions.ToString(Me.DR.Item(14))
                        data.Warranty = Conversions.ToString(Me.DR.Item(15))
                        data.ReRepair = Conversions.ToString(Me.DR.Item(&H10))
                        data.Comment = Conversions.ToString(Me.DR.Item(&H11))
                    Loop
                    Dim path As String = (Me.Server.MapPath("~/Content/temp/") & code & ".jpg")
                    File.WriteAllBytes(path, agGlobals.GenerarCodigo(Me.Server, code, "", 350, 40, 60))
                    Dim report As New PDFHojaViajera With { _
                        .RutaLogo = Me.Server.MapPath("~/Content/images/logo-01.jpg"), _
                        .RutaBarCode = path, _
                        .Model = data _
                    }
                    RT.PrintPDF(report)
                    report = Nothing
                Else
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Return Me.RedirectToAction("imprimir_hoja")
                End If
                If Not Me.DR.IsClosed Then
                    Me.DR.Close()
                End If
                Me.DA.Dispose()
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsg") = exception.Message
                Me.DA.Dispose()
                Dim result As ActionResult = Me.RedirectToAction("imprimir_hoja")
                ProjectData.ClearProjectError()
                Return result
                ProjectData.ClearProjectError()
            End Try
            Me.TempData.Item("PrintData") = data
            Me.TempData.Keep("ErrMsg")
            Me.TempData.Item("OrderID") = code
            Return Me.RedirectToAction("imprimir_hoja")
        End Function

        <Authorize> _
        Public Function recibo_almacen() As ActionResult
            Return Me.View
        End Function

        <Authorize, HttpPost> _
        Public Function recibo_almacen(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("rc_setCheckIn", New Object() {model.OrderID, Me.User.Identity.Name, model.Comment})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Return Me.View
                End If
                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = RuntimeHelpers.GetObjectValue(Me.DR.Item(0))
                Loop
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsg") = exception.Message
                ProjectData.ClearProjectError()
            Finally
                If Not Me.DR.IsClosed Then
                    Me.DR.Close()
                End If
                Me.DA.Dispose()
            End Try
            Return Me.RedirectToAction("recibo_almacen")
        End Function

        <HttpPost, Authorize> _
        Public Function scan_info(ByVal mdl As ScanInfoModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = String.Empty
            Dim model As ScanInfoModel = mdl
            Try
                Me.DR = Me.DA.ExecuteSP("rc_regScanInfo", New Object() {model.OrderID, model.SerialNo, model.PartNo, model.Model, model.TrackNo, Me.User.Identity.Name})
                If (Me.DA.LastErrorMessage = "") Then
                    Do While Me.DR.Read
                        str = Conversions.ToString(Me.DR.Item(0))
                    Loop
                Else
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Return Me.View
                End If
                If Not Me.DR.IsClosed Then
                    Me.DR.Close()
                End If
                Me.DA.Dispose()
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsg") = exception.Message
                Me.DA.Dispose()
                result = Me.View
                ProjectData.ClearProjectError()
                Return result
                ProjectData.ClearProjectError()
            End Try
            Return Me.RedirectToAction("formulario_recibo")
            model = Nothing
            Return result
        End Function

        <Authorize, HttpGet> _
        Public Function scan_info(ByVal model As ScanOrderModel) As ActionResult
            If Operators.ConditionalCompareObjectEqual(Me.TempData.Item("Opt"), True, False) Then
                Me.TempData.Keep("OrderID")
            Else
                Me.TempData.Item("OrderID") = model.OrderID
            End If
            Return Me.View
        End Function

        <Authorize> _
        Public Function transfer_diagnostic() As ActionResult
            Return Me.View
        End Function

        <HttpPost, Authorize> _
        Public Function transfer_diagnostic(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("rc_setTransferDiagnostic", New Object() {model.OrderID, Me.User.Identity.Name, model.Comment})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Return Me.View
                End If
                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = RuntimeHelpers.GetObjectValue(Me.DR.Item(0))
                Loop
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsg") = exception.Message
                ProjectData.ClearProjectError()
            Finally
                Me.DA.Dispose()
            End Try
            Return Me.RedirectToAction("transfer_diagnostic")
        End Function

        <Authorize> _
        Public Function transfer_incoming() As ActionResult
            Return Me.View
        End Function

        <HttpPost, Authorize> _
        Public Function transfer_incoming(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("rc_setTransferIncoming", New Object() {model.OrderID, Me.User.Identity.Name, model.Comment})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Return Me.View
                End If
                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = RuntimeHelpers.GetObjectValue(Me.DR.Item(0))
                Loop
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsg") = exception.Message
                ProjectData.ClearProjectError()
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
