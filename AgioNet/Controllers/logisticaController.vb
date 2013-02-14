Namespace AgioNet
    Public Class logisticaController
        Inherits System.Web.Mvc.Controller
        '-- Methods

        ' 2013.02.14
        ' POST: /logistica/AgregarTrackNoModel
        <HttpPost, Authorize> _
        Public Function agregar_esp_trackno(ByVal model As AgregarTrackNoModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess("localhost", "AgioNet v1.3", "aguser", "Agiotech01")
            Dim str As String = String.Empty
            Dim model2 As AgregarTrackNoModel = model
            Try
                Me.DR = Me.DA.ExecuteSP("lg_TrackNo_AssignTrackNo", New Object() {model2.OrderID, model2.CaseType, model2.OutBound, model2.InBound, model2.OutBound2, model2.CarrierName, model2.Weight, model2.Height, model2.Lenght, model2.width, model2.Field1, model2.Field2, model2.Reference, Me.User.Identity.Name})
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
            Me.TempData.Item("ErrMsg") = str
            Me.Session.Remove("OrderID")
            Return Me.RedirectToAction("ordenes_cflete")
            model2 = Nothing
            Return result
        End Function

        <Authorize> _
        Public Function agregar_esp_trackno(ByVal model As OrderLogisticaModel) As ActionResult
            Me.Session.Add("OrderID", model.OrderID)
            Return Me.View
        End Function

        <HttpPost, Authorize> _
        Public Function agregar_trackno(ByVal model As AgregarTrackNoModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess("localhost", "AgioNet v1.3", "aguser", "Agiotech01")
            Dim str As String = String.Empty
            Dim model2 As AgregarTrackNoModel = model
            Try
                Me.DR = Me.DA.ExecuteSP("lg_TrackNo_AssignTrackNo", New Object() {model2.OrderID, model2.CaseType, model2.OutBound, model2.InBound, model2.OutBound2, model2.CarrierName, model2.Weight, model2.Height, model2.Lenght, model2.width, model2.Field1, model2.Field2, model2.Reference, Me.User.Identity.Name})
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
            Me.TempData.Item("ErrMsg") = str
            Me.Session.Remove("OrderID")
            Return Me.RedirectToAction("ordenes_cflete")
            model2 = Nothing
            Return result
        End Function

        <Authorize> _
        Public Function agregar_trackno(ByVal model As OrderLogisticaModel) As ActionResult
            Me.Session.Add("OrderID", model.OrderID)
            Return Me.View
        End Function

        <Authorize> _
        Public Function GenerateExcel() As ActionResult
            Me.DA = New DataAccess("localhost", "AgioNet v1.3", "aguser", "Agiotech01")
            Dim modelArray As OrderListModel() = New OrderListModel(&H65 - 1) {}
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("lg_getPendientesRecoleccion", New Object(0 - 1) {})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("index")
                End If
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        Dim model As New OrderListModel With { _
                            .OrderID = Conversions.ToString(Me.DR.Item(0)), _
                            .OrderDate = Conversions.ToString(Me.DR.Item(1)), _
                            .CustomerName = Conversions.ToString(Me.DR.Item(2)), _
                            .Email = Conversions.ToString(Me.DR.Item(3)), _
                            .Delivery = Conversions.ToString(Me.DR.Item(4)), _
                            .DeliveryTime = Conversions.ToString(Me.DR.Item(5)), _
                            .ProductClass = Conversions.ToString(Me.DR.Item(6)), _
                            .ProductType = Conversions.ToString(Me.DR.Item(7)), _
                            .ProductModel = Conversions.ToString(Me.DR.Item(8)), _
                            .PartNo = Conversions.ToString(Me.DR.Item(9)), _
                            .SerialNo = Conversions.ToString(Me.DR.Item(10)) _
                        }
                        modelArray(index) = model
                        index += 1
                    Loop
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New OrderListModel(((index - 1) + 1) - 1) {}), OrderListModel())
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
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New OrderListModel(((index - 1) + 1) - 1) {}), OrderListModel())
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
            Me.TempData.Item("Model") = modelArray
            Return Me.View
        End Function

        <HttpGet, Authorize> _
        Public Function hoja_individual(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess("localhost", "AgioNet v1.3", "aguser", "Agiotech01")
            Dim model2 As New CreateOrderModel
            Dim code As String = String.Empty
            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrderMaster_GetOrderInfo", New Object() {model.OrderID})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("index")
                End If
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        code = Conversions.ToString(Me.DR.Item(0))
                        model2 = New CreateOrderModel With { _
                            .CustomerName = Conversions.ToString(Me.DR.Item(2)), _
                            .RazonSocial = Conversions.ToString(Me.DR.Item(3)), _
                            .CustomerReference = Conversions.ToString(Me.DR.Item(4)), _
                            .RFC = Conversions.ToString(Me.DR.Item(5)), _
                            .Email = Conversions.ToString(Me.DR.Item(6)), _
                            .Address = Conversions.ToString(Me.DR.Item(7)), _
                            .ExternalNumber = Conversions.ToString(Me.DR.Item(8)), _
                            .InternalNumber = Conversions.ToString(Me.DR.Item(9)), _
                            .Address2 = Conversions.ToString(Me.DR.Item(10)), _
                            .City = Conversions.ToString(Me.DR.Item(11)), _
                            .State = Conversions.ToString(Me.DR.Item(12)), _
                            .Country = Conversions.ToString(Me.DR.Item(13)), _
                            .ZipCode = Conversions.ToString(Me.DR.Item(14)), _
                            .Telephone = Conversions.ToString(Me.DR.Item(15)), _
                            .Telephone2 = Conversions.ToString(Me.DR.Item(&H10)), _
                            .Telephone3 = Conversions.ToString(Me.DR.Item(&H11)), _
                            .Delivery = Conversions.ToString(Me.DR.Item(&H12)), _
                            .DeliveryTime = Conversions.ToString(Me.DR.Item(&H13)), _
                            .ProductClass = Conversions.ToString(Me.DR.Item(&H15)), _
                            .ProductType = Conversions.ToString(Me.DR.Item(&H16)), _
                            .ProductTrademark = Conversions.ToString(Me.DR.Item(&H17)), _
                            .ProductModel = Conversions.ToString(Me.DR.Item(&H18)), _
                            .productDescription = Conversions.ToString(Me.DR.Item(&H19)), _
                            .PartNumber = Conversions.ToString(Me.DR.Item(&H1A)), _
                            .SerialNumber = Conversions.ToString(Me.DR.Item(&H1B)), _
                            .Revision = Conversions.ToString(Me.DR.Item(&H1C)), _
                            .ServiceType = Conversions.ToString(Me.DR.Item(&H1D)), _
                            .FailureType = Conversions.ToString(Me.DR.Item(30)), _
                            .Comment = Conversions.ToString(Me.DR.Item(&H1F)) _
                        }
                    Loop
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
            Try
                Dim path As String = (Me.Server.MapPath("~/Content/temp/") & code & ".jpg")
                File.WriteAllBytes(path, agGlobals.GenerarCodigo(Me.Server, code, "", 350, 40, 60))
                Dim report As New PDFInTransitPage With { _
                    .RutaLogo = Me.Server.MapPath("~/Content/images/logo-01.jpg"), _
                    .RutaBarCode = path, _
                    .RutaComments = Me.Server.MapPath("~/Content/images/comments.jpg"), _
                    .Orderid = code, _
                    .Model = model2 _
                }
                RT.PrintPDF(report)
                report = Nothing
                Dim page2 As New PDFInTransitPage With { _
                    .RutaLogo = Me.Server.MapPath("~/Content/images/logo-01.jpg"), _
                    .RutaBarCode = path, _
                    .RutaComments = Me.Server.MapPath("~/Content/images/comments.jpg"), _
                    .Orderid = code, _
                    .Model = model2, _
                    .IsCopy = True _
                }
                RT.PrintPDF(page2)
                page2 = Nothing
                Me.DA = New DataAccess("localhost", "AgioNet v1.3", "aguser", "Agiotech01")
                Me.DR = Me.DA.ExecuteSP("sys_AddOrderStatus", New Object() {model.OrderID, "STOCK", Me.User.Identity.Name, ("Auto: " & model.OrderID & ", formato de recolección")})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMesage") = Me.DA._LastErrorMessage
                End If
                Me.DA.Dispose()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                Me.TempData.Item("ErrMsg") = exception2.Message
                ProjectData.ClearProjectError()
            End Try
            Return Me.RedirectToAction("pendiente_recoleccion")
        End Function

        <Authorize> _
        Public Function Index() As ActionResult
            Return Me.View
        End Function

        <Authorize> _
        Public Function intransit() As ActionResult
            Return Me.View
        End Function

        <Authorize, HttpPost> _
        Public Function intransit(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess("localhost", "AgioNet v1.3", "aguser", "Agiotech01")
            Try
                Me.DR = Me.DA.ExecuteSP("lg_setInTransit", New Object() {model.OrderID, Me.User.Identity.Name, model.Comment})
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
            Return Me.View
        End Function

        Public Function modificar_guia() As ActionResult
            Me.DA = New DataAccess("localhost", "AgioNet v1.3", "aguser", "Agiotech01")
            Dim modelArray As AgregarTrackNoModel() = New AgregarTrackNoModel(&H65 - 1) {}
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("lg_getTrackNoOrders", New Object(0 - 1) {})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("index")
                End If
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        Dim model As New AgregarTrackNoModel With { _
                            .OrderID = Conversions.ToString(Me.DR.Item(0)), _
                            .CaseType = Conversions.ToString(Me.DR.Item(1)), _
                            .OutBound = Conversions.ToString(Me.DR.Item(2)), _
                            .InBound = Conversions.ToString(Me.DR.Item(3)), _
                            .OutBound2 = Conversions.ToString(Me.DR.Item(4)), _
                            .CarrierName = Conversions.ToString(Me.DR.Item(5)), _
                            .Weight = Conversions.ToString(Me.DR.Item(6)), _
                            .Height = Conversions.ToString(Me.DR.Item(7)), _
                            .Lenght = Conversions.ToString(Me.DR.Item(8)), _
                            .width = Conversions.ToString(Me.DR.Item(9)), _
                            .Field1 = Conversions.ToString(Me.DR.Item(10)), _
                            .Field2 = Conversions.ToString(Me.DR.Item(11)), _
                            .Reference = Conversions.ToString(Me.DR.Item(12)) _
                        }
                        modelArray(index) = model
                        index += 1
                    Loop
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New AgregarTrackNoModel(((index - 1) + 1) - 1) {}), AgregarTrackNoModel())
                Else
                    index = 0
                    modelArray(index) = New AgregarTrackNoModel With { _
                        .OrderID = "no data", _
                        .CaseType = "no data", _
                        .OutBound = "no data", _
                        .InBound = "no data", _
                        .OutBound2 = "no data", _
                        .CarrierName = "no data", _
                        .Weight = "no data", _
                        .Height = "no data", _
                        .Lenght = "no data", _
                        .width = "no data", _
                        .Field1 = "no data", _
                        .Field2 = "no data", _
                        .Reference = "no data" _
                    }
                    index += 1
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New AgregarTrackNoModel(((index - 1) + 1) - 1) {}), AgregarTrackNoModel())
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
            Me.TempData.Item("Model") = modelArray
            Return Me.View
        End Function

        <Authorize, HttpPost> _
        Public Function modificarguia(ByVal model As AgregarTrackNoModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess("localhost", "AgioNet v1.3", "aguser", "Agiotech01")
            Dim str As String = String.Empty
            Dim model2 As AgregarTrackNoModel = model
            Try
                Me.DR = Me.DA.ExecuteSP("lg_updTrackNo", New Object() {model2.OrderID, model2.CaseType, model2.OutBound, model2.InBound, model2.OutBound2, model2.CarrierName, model2.Weight, model2.Height, model2.Lenght, model2.width, model2.Field1, model2.Field2, model2.Reference, Me.User.Identity.Name})
                If (Me.DA.LastErrorMessage = "") Then
                    Do While Me.DR.Read
                        str = Conversions.ToString(Me.DR.Item(0))
                    Loop
                Else
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Return Me.RedirectToAction("modificar_guia")
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
                result = Me.RedirectToAction("modificar_guia")
                ProjectData.ClearProjectError()
                Return result
                ProjectData.ClearProjectError()
            End Try
            Me.TempData.Item("ErrMsg") = str
            Me.Session.Remove("OrderID")
            Return Me.RedirectToAction("modificar_guia")
            model2 = Nothing
            Return result
        End Function

        Public Function modificarguia(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess("localhost", "AgioNet v1.3", "aguser", "Agiotech01")
            Dim model2 As New AgregarTrackNoModel
            Try
                Dim model3 As AgregarTrackNoModel
                Me.DR = Me.DA.ExecuteSP("lg_getTrackNoOrders", New Object() {model.OrderID})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("index")
                End If
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        model3 = New AgregarTrackNoModel With { _
                            .OrderID = Conversions.ToString(Me.DR.Item(0)), _
                            .CaseType = Conversions.ToString(Me.DR.Item(1)), _
                            .OutBound = Conversions.ToString(Me.DR.Item(2)), _
                            .InBound = Conversions.ToString(Me.DR.Item(3)), _
                            .OutBound2 = Conversions.ToString(Me.DR.Item(4)), _
                            .CarrierName = Conversions.ToString(Me.DR.Item(5)), _
                            .Weight = Conversions.ToString(Me.DR.Item(6)), _
                            .Height = Conversions.ToString(Me.DR.Item(7)), _
                            .Lenght = Conversions.ToString(Me.DR.Item(8)), _
                            .width = Conversions.ToString(Me.DR.Item(9)), _
                            .Field1 = Conversions.ToString(Me.DR.Item(10)), _
                            .Field2 = Conversions.ToString(Me.DR.Item(11)), _
                            .Reference = Conversions.ToString(Me.DR.Item(12)) _
                        }
                        model2 = model3
                    Loop
                Else
                    model3 = New AgregarTrackNoModel With { _
                        .OrderID = "no data", _
                        .CaseType = "no data", _
                        .OutBound = "no data", _
                        .InBound = "no data", _
                        .OutBound2 = "no data", _
                        .CarrierName = "no data", _
                        .Weight = "no data", _
                        .Height = "no data", _
                        .Lenght = "no data", _
                        .width = "no data", _
                        .Field1 = "no data", _
                        .Field2 = "no data", _
                        .Reference = "no data" _
                    }
                    model2 = model3
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
            Me.TempData.Item("Model") = model2
            Return Me.View
        End Function

        <Authorize> _
        Public Function ordenes_cflete() As ActionResult
            Me.DA = New DataAccess("localhost", "AgioNet v1.3", "aguser", "Agiotech01")
            Dim modelArray As OrderListModel() = New OrderListModel(&H65 - 1) {}
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("lg_getOrdenesFletesSinTrackNo", New Object(0 - 1) {})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("index")
                End If
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        Dim model As New OrderListModel With { _
                            .OrderID = Conversions.ToString(Me.DR.Item(0)), _
                            .OrderDate = Conversions.ToString(Me.DR.Item(1)), _
                            .CustomerName = Conversions.ToString(Me.DR.Item(2)), _
                            .Email = Conversions.ToString(Me.DR.Item(3)), _
                            .Delivery = Conversions.ToString(Me.DR.Item(4)), _
                            .DeliveryTime = Conversions.ToString(Me.DR.Item(5)), _
                            .ProductClass = Conversions.ToString(Me.DR.Item(6)), _
                            .ProductType = Conversions.ToString(Me.DR.Item(7)), _
                            .ProductModel = Conversions.ToString(Me.DR.Item(8)), _
                            .PartNo = Conversions.ToString(Me.DR.Item(9)), _
                            .SerialNo = Conversions.ToString(Me.DR.Item(10)) _
                        }
                        modelArray(index) = model
                        index += 1
                    Loop
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New OrderListModel(((index - 1) + 1) - 1) {}), OrderListModel())
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
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New OrderListModel(((index - 1) + 1) - 1) {}), OrderListModel())
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
            Me.TempData.Item("Model") = modelArray
            Return Me.View
        End Function

        <Authorize> _
        Public Function ordenes_sflete() As ActionResult
            Me.DA = New DataAccess("localhost", "AgioNet v1.3", "aguser", "Agiotech01")
            Dim modelArray As OrderListModel() = New OrderListModel(&H65 - 1) {}
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("lg_getOrdenesNoFletesSinTrackNo", New Object(0 - 1) {})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("index")
                End If
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        Dim model As New OrderListModel With { _
                            .OrderID = Conversions.ToString(Me.DR.Item(0)), _
                            .OrderDate = Conversions.ToString(Me.DR.Item(1)), _
                            .CustomerName = Conversions.ToString(Me.DR.Item(2)), _
                            .Email = Conversions.ToString(Me.DR.Item(3)), _
                            .Delivery = Conversions.ToString(Me.DR.Item(4)), _
                            .DeliveryTime = Conversions.ToString(Me.DR.Item(5)), _
                            .ProductClass = Conversions.ToString(Me.DR.Item(6)), _
                            .ProductType = Conversions.ToString(Me.DR.Item(7)), _
                            .ProductModel = Conversions.ToString(Me.DR.Item(8)), _
                            .PartNo = Conversions.ToString(Me.DR.Item(9)), _
                            .SerialNo = Conversions.ToString(Me.DR.Item(10)) _
                        }
                        modelArray(index) = model
                        index += 1
                    Loop
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New OrderListModel(((index - 1) + 1) - 1) {}), OrderListModel())
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
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New OrderListModel(((index - 1) + 1) - 1) {}), OrderListModel())
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
            Me.TempData.Item("Model") = modelArray
            Return Me.View
        End Function

        <Authorize> _
        Public Function pendiente_recoleccion() As ActionResult
            Me.DA = New DataAccess("localhost", "AgioNet v1.3", "aguser", "Agiotech01")
            Dim modelArray As OrderListModel() = New OrderListModel(&H65 - 1) {}
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("lg_getPendientesRecoleccion", New Object(0 - 1) {})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("index")
                End If
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        Dim model As New OrderListModel With { _
                            .OrderID = Conversions.ToString(Me.DR.Item(0)), _
                            .OrderDate = Conversions.ToString(Me.DR.Item(1)), _
                            .CustomerName = Conversions.ToString(Me.DR.Item(2)), _
                            .Email = Conversions.ToString(Me.DR.Item(3)), _
                            .Delivery = Conversions.ToString(Me.DR.Item(4)), _
                            .DeliveryTime = Conversions.ToString(Me.DR.Item(5)), _
                            .ProductClass = Conversions.ToString(Me.DR.Item(6)), _
                            .ProductType = Conversions.ToString(Me.DR.Item(7)), _
                            .ProductModel = Conversions.ToString(Me.DR.Item(8)), _
                            .PartNo = Conversions.ToString(Me.DR.Item(9)), _
                            .SerialNo = Conversions.ToString(Me.DR.Item(10)) _
                        }
                        modelArray(index) = model
                        index += 1
                    Loop
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New OrderListModel(((index - 1) + 1) - 1) {}), OrderListModel())
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
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New OrderListModel(((index - 1) + 1) - 1) {}), OrderListModel())
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
            Me.TempData.Item("Model") = modelArray
            Return Me.View
        End Function

        Public Function reimprimir_formato() As ActionResult
            Me.DA = New DataAccess("localhost", "AgioNet v1.3", "aguser", "Agiotech01")
            Dim modelArray As OrderListModel() = New OrderListModel(&H65 - 1) {}
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("lg_getTrackNoOrders", New Object(0 - 1) {})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("index")
                End If
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        Dim model As New OrderListModel With { _
                            .OrderID = Conversions.ToString(Me.DR.Item(0)), _
                            .OrderDate = Conversions.ToString(Me.DR.Item(1)), _
                            .CustomerName = Conversions.ToString(Me.DR.Item(2)), _
                            .Email = Conversions.ToString(Me.DR.Item(3)), _
                            .Delivery = Conversions.ToString(Me.DR.Item(4)), _
                            .DeliveryTime = Conversions.ToString(Me.DR.Item(5)), _
                            .ProductClass = Conversions.ToString(Me.DR.Item(6)), _
                            .ProductType = Conversions.ToString(Me.DR.Item(7)), _
                            .ProductModel = Conversions.ToString(Me.DR.Item(8)), _
                            .PartNo = Conversions.ToString(Me.DR.Item(9)), _
                            .SerialNo = Conversions.ToString(Me.DR.Item(10)) _
                        }
                        modelArray(index) = model
                        index += 1
                    Loop
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New OrderListModel(((index - 1) + 1) - 1) {}), OrderListModel())
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
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New OrderListModel(((index - 1) + 1) - 1) {}), OrderListModel())
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
            Me.TempData.Item("Model") = modelArray
            Return Me.View
        End Function


        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader

    End Class
End Namespace
