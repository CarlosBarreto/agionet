Namespace AgioNet
    Public Class logisticaController
        Inherits System.Web.Mvc.Controller
        '-- Methods

        ' 2013.02.14
        ' POST: /logistica/agregar_esp_trackno
        <HttpPost, Authorize> _
        Public Function agregar_esp_trackno(ByVal model As AgregarTrackNoModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = String.Empty
            Dim model2 As AgregarTrackNoModel = model

            Try
                Me.DR = Me.DA.ExecuteSP("lg_TrackNo_AssignTrackNo", model2.OrderID, model2.CaseType, model2.OutBound, model2.InBound, _
                                        model2.OutBound2, model2.CarrierName, model2.Weight, model2.Height, model2.Lenght, model2.width, _
                                        model2.Field1, model2.Field2, model2.Reference, Me.User.Identity.Name)
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

            Me.TempData.Item("ErrMsg") = str
            Me.Session.Remove("OrderID")

            Return Me.RedirectToAction("ordenes_cflete")
        End Function

        ' 2013.02.14
        ' GET: /logistica/agregar_esp_trackno
        <Authorize> _
        Public Function agregar_esp_trackno(ByVal model As OrderLogisticaModel) As ActionResult
            Me.Session.Add("OrderID", model.OrderID)
            Return Me.View
        End Function

        ' 2014.02.14
        ' POST: /logistica/agregar_trackno
        <HttpPost, Authorize> _
        Public Function agregar_trackno(ByVal model As AgregarTrackNoModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = String.Empty
            Dim model2 As AgregarTrackNoModel = model
            Try
                Me.DR = Me.DA.ExecuteSP("lg_TrackNo_AssignTrackNo", model2.OrderID, model2.CaseType, model2.OutBound, model2.InBound, _
                                        model2.OutBound2, model2.CarrierName, model2.Weight, model2.Height, model2.Lenght, model2.width, _
                                        model2.Field1, model2.Field2, model2.Reference, Me.User.Identity.Name)
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
                Return View()
            Finally
                Me.DA.Dispose()
            End Try

            Me.TempData.Item("ErrMsg") = str
            Me.Session.Remove("OrderID")
            Return Me.RedirectToAction("ordenes_cflete")
        End Function

        ' 2013.02.14
        ' GET: /logistica/agregar_trackno
        <Authorize> _
        Public Function agregar_trackno(ByVal model As OrderLogisticaModel) As ActionResult
            Me.Session.Add("OrderID", model.OrderID)
            Return Me.View
        End Function

        ' 2013.02.14
        ' GET: /logistica/GenerateExcel
        <Authorize> _
        Public Function GenerateExcel() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As OrderListModel
            Dim index As Integer = 0

            Try
                Me.DR = Me.DA.ExecuteSP("lg_getPendientesRecoleccion")
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
                    ' -- Actualizado por Carlos Barreto 
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
            Me.TempData.Item("Model") = modelArray
            Return Me.View
        End Function

        <HttpGet, Authorize> _
        Public Function hoja_individual(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
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
                        code = DR(0)
                        model2 = New CreateOrderModel With { _
                            .CustomerName = DR(2)), _
                            .RazonSocial = DR(3)), _
                            .CustomerReference = DR(4)), _
                            .RFC = DR(5)), _
                            .Email = DR(6)), _
                            .Address = DR(7)), _
                            .ExternalNumber = DR(8)), _
                            .InternalNumber = DR(9)), _
                            .Address2 = DR(10)), _
                            .City = DR(11)), _
                            .State = DR(12)), _
                            .Country = DR(13)), _
                            .ZipCode = DR(14)), _
                            .Telephone = DR(15)), _
                            .Telephone2 = DR(&H10)), _
                            .Telephone3 = DR(&H11)), _
                            .Delivery = DR(&H12)), _
                            .DeliveryTime = DR(&H13)), _
                            .ProductClass = DR(&H15)), _
                            .ProductType = DR(&H16)), _
                            .ProductTrademark = DR(&H17)), _
                            .ProductModel = DR(&H18)), _
                            .productDescription = DR(&H19)), _
                            .PartNumber = DR(&H1A)), _
                            .SerialNumber = DR(&H1B)), _
                            .Revision = DR(&H1C)), _
                            .ServiceType = DR(&H1D)), _
                            .FailureType = DR(30)), _
                            .Comment = DR(&H1F)) _
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
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
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
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
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
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
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
                            .OrderID = DR(0), _
                            .CaseType = DR(1)), _
                            .OutBound = DR(2)), _
                            .InBound = DR(3)), _
                            .OutBound2 = DR(4)), _
                            .CarrierName = DR(5)), _
                            .Weight = DR(6)), _
                            .Height = DR(7)), _
                            .Lenght = DR(8)), _
                            .width = DR(9)), _
                            .Field1 = DR(10)), _
                            .Field2 = DR(11)), _
                            .Reference = DR(12)) _
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
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = String.Empty
            Dim model2 As AgregarTrackNoModel = model
            Try
                Me.DR = Me.DA.ExecuteSP("lg_updTrackNo", New Object() {model2.OrderID, model2.CaseType, model2.OutBound, model2.InBound, model2.OutBound2, model2.CarrierName, model2.Weight, model2.Height, model2.Lenght, model2.width, model2.Field1, model2.Field2, model2.Reference, Me.User.Identity.Name})
                If (Me.DA.LastErrorMessage = "") Then
                    Do While Me.DR.Read
                        str = DR(0)
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
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
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
                            .OrderID = DR(0), _
                            .CaseType = DR(1)), _
                            .OutBound = DR(2)), _
                            .InBound = DR(3)), _
                            .OutBound2 = DR(4)), _
                            .CarrierName = DR(5)), _
                            .Weight = DR(6)), _
                            .Height = DR(7)), _
                            .Lenght = DR(8)), _
                            .width = DR(9)), _
                            .Field1 = DR(10)), _
                            .Field2 = DR(11)), _
                            .Reference = DR(12)) _
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
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
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
                            .OrderID = DR(0), _
                            .OrderDate = DR(1)), _
                            .CustomerName = DR(2)), _
                            .Email = DR(3)), _
                            .Delivery = DR(4)), _
                            .DeliveryTime = DR(5)), _
                            .ProductClass = DR(6)), _
                            .ProductType = DR(7)), _
                            .ProductModel = DR(8)), _
                            .PartNo = DR(9)), _
                            .SerialNo = DR(10)) _
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
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
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
                            .OrderID = DR(0), _
                            .OrderDate = DR(1)), _
                            .CustomerName = DR(2)), _
                            .Email = DR(3)), _
                            .Delivery = DR(4)), _
                            .DeliveryTime = DR(5)), _
                            .ProductClass = DR(6)), _
                            .ProductType = DR(7)), _
                            .ProductModel = DR(8)), _
                            .PartNo = DR(9)), _
                            .SerialNo = DR(10)) _
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
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
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
                            .OrderID = DR(0), _
                            .OrderDate = DR(1)), _
                            .CustomerName = DR(2)), _
                            .Email = DR(3)), _
                            .Delivery = DR(4)), _
                            .DeliveryTime = DR(5)), _
                            .ProductClass = DR(6)), _
                            .ProductType = DR(7)), _
                            .ProductModel = DR(8)), _
                            .PartNo = DR(9)), _
                            .SerialNo = DR(10)) _
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
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
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
                            .OrderID = DR(0), _
                            .OrderDate = DR(1)), _
                            .CustomerName = DR(2)), _
                            .Email = DR(3)), _
                            .Delivery = DR(4)), _
                            .DeliveryTime = DR(5)), _
                            .ProductClass = DR(6)), _
                            .ProductType = DR(7)), _
                            .ProductModel = DR(8)), _
                            .PartNo = DR(9)), _
                            .SerialNo = DR(10)) _
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
