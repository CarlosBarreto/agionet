Imports Root.Reports
Imports System.IO
Imports System.Data.SqlClient

Namespace AgioNet
    Public Class logisticaController
        Inherits System.Web.Mvc.Controller
        '-- Methods

        ' 2013.02.14
        ' POST: /logistica/agregar_esp_trackno
        <HttpPost, Authorize> _
        Public Function agregar_esp_trackno(ByVal model As AgregarTrackNoModel) As ActionResult
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

        ' 2013.02.14
        ' GET: /logistica/hoja_individual
        <HttpGet, Authorize> _
        Public Function hoja_individual(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model2 As New CreateOrderModel
            Dim code As String = String.Empty
            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrderMaster_GetOrderInfo", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("index")
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        code = DR(0)
                        model2 = New CreateOrderModel With { _
                            .CustomerName = DR(2), _
                            .RazonSocial = DR(3), _
                            .CustomerReference = DR(4), _
                            .RFC = DR(5), _
                            .Email = DR(6), _
                            .Address = DR(7), _
                            .ExternalNumber = DR(8), _
                            .InternalNumber = DR(9), _
                            .Address2 = DR(10), _
                            .City = DR(11), _
                            .State = DR(12), _
                            .Country = DR(13), _
                            .ZipCode = DR(14), _
                            .Telephone = DR(15), _
                            .Telephone2 = DR(16), _
                            .Telephone3 = DR(17), _
                            .Delivery = DR(18), _
                            .DeliveryTime = DR(19), _
                            .ProductClass = DR(21), _
                            .ProductType = DR(22), _
                            .ProductTrademark = DR(23), _
                            .ProductModel = DR(24), _
                            .productDescription = DR(25), _
                            .PartNumber = DR(26), _
                            .SerialNumber = DR(27), _
                            .Revision = DR(28), _
                            .ServiceType = DR(29), _
                            .FailureType = DR(30), _
                            .Comment = DR(31) _
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
                If Not System.IO.File.Exists(path) Then
                    System.IO.File.WriteAllBytes(path, GenerarCodigo(Me.Server, code, "", 350, 40, 60))
                End If

                Dim report As New PDFIntransit
                report.RutaLogo = Me.Server.MapPath("~/Content/images/logo-01.jpg")
                report.RutaBarCode = path
                report.RutaComments = Me.Server.MapPath("~/Content/images/comments.jpg")
                report.Orderid = code
                report.Model = model2

                'RT.PrintPDF(report)
                RT.ViewPDF(report, Me.Server.MapPath("~/Content/temp/") & code & ".pdf")

                report = Nothing

                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Me.DR = Me.DA.ExecuteSP("sys_AddOrderStatus", model.OrderID, "STOCK", Me.User.Identity.Name, ("Auto: " & model.OrderID & ", formato de recolección"))
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMesage") = Me.DA._LastErrorMessage
                End If
                Me.DA.Dispose()

                Dim filename As String = Me.Server.MapPath("~/Content/temp/") & code & ".pdf"
                Return File(filename, "application/pdf", Server.HtmlEncode(filename))
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("index")
            End Try


            '
        End Function

        ' 2013.02.14
        ' GET: /logistica/Index
        <Authorize> _
        Public Function Index() As ActionResult
            Return Me.View
        End Function

        ' 2013.02.14
        ' GET: /logistica/intransit
        <Authorize> _
        Public Function intransit() As ActionResult
            Return Me.View
        End Function

        ' 2013.02.14 
        ' POST: /logistica/intransit
        <Authorize, HttpPost> _
        Public Function intransit(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("lg_setInTransit", model.OrderID, Me.User.Identity.Name, model.Comment)
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
            Return Me.View
        End Function

        ' 2013.02.14
        ' GET: /logistica/modificar_guia
        <Authorize, HttpGet> _
        Public Function modificar_guia() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As AgregarTrackNoModel
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("lg_getTrackNoOrders")
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("index")
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        Dim model As New AgregarTrackNoModel With { _
                            .OrderID = DR(0), _
                            .CaseType = DR(1), _
                            .OutBound = DR(2), _
                            .InBound = DR(3), _
                            .OutBound2 = DR(4), _
                            .CarrierName = DR(5), _
                            .Weight = DR(6), _
                            .Height = DR(7), _
                            .Lenght = DR(8), _
                            .width = DR(9), _
                            .Field1 = DR(10), _
                            .Field2 = DR(11), _
                            .Reference = DR(12) _
                        }
                        modelArray(index) = model
                        If modelArray.Length <= index + 1 Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
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

        ' 2013.02.14
        ' POST: /logistica/modificarguia
        <Authorize, HttpPost> _
        Public Function modificarguia(ByVal model As AgregarTrackNoModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = String.Empty
            Dim model2 As AgregarTrackNoModel = model

            Try
                Me.DR = Me.DA.ExecuteSP("lg_updTrackNo", model2.OrderID, model2.CaseType, model2.OutBound, model2.InBound, _
                                        model2.OutBound2, model2.CarrierName, model2.Weight, model2.Height, model2.Lenght, _
                                        model2.width, model2.Field1, model2.Field2, model2.Reference, Me.User.Identity.Name)

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
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("modificar_guia")
            Finally
                Me.DA.Dispose()
            End Try

            Me.TempData.Item("ErrMsg") = str
            Me.Session.Remove("OrderID")
            Return Me.RedirectToAction("modificar_guia")
        End Function

        ' 2013.02.14
        ' GET: /logistica/modificarguia
        <Authorize, HttpGet> _
        Public Function modificarguia(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model2 As New AgregarTrackNoModel

            Try
                Dim model3 As AgregarTrackNoModel
                Me.DR = Me.DA.ExecuteSP("lg_getTrackNoOrders", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("index")
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        model3 = New AgregarTrackNoModel With { _
                            .OrderID = DR(0), _
                            .CaseType = DR(1), _
                            .OutBound = DR(2), _
                            .InBound = DR(3), _
                            .OutBound2 = DR(4), _
                            .CarrierName = DR(5), _
                            .Weight = DR(6), _
                            .Height = DR(7), _
                            .Lenght = DR(8), _
                            .width = DR(9), _
                            .Field1 = DR(10), _
                            .Field2 = DR(11), _
                            .Reference = DR(12) _
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
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("index")
            End Try

            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If
            Me.DA.Dispose()

            Me.TempData.Item("Model") = model2
            Return Me.View
        End Function

        ' 2013.02.14
        ' GET: /logistica/ordenes_cflete
        <Authorize> _
        Public Function ordenes_cflete() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As OrderListModel
            Dim index As Integer = 0

            Try
                Me.DR = Me.DA.ExecuteSP("lg_getOrdenesFletesSinTrackNo")
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
                        If modelArray.Length <= index + 1 Then ReDim Preserve modelArray(index + 1)
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

        ' 2013.02.14
        ' GET: /logistica/ordenes_sflete
        <Authorize> _
        Public Function ordenes_sflete() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As OrderListModel
            Dim index As Integer = 0

            Try
                Me.DR = Me.DA.ExecuteSP("lg_getOrdenesNoFletesSinTrackNo")
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
                        If modelArray.Length <= index + 1 Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ' -- Actualizdo por Carlos Barreto
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
                    ' -- Actualizdo por Carlos Barreto
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

        ' 2013.02.14
        ' GET: /logistica/pendiente_recoleccion
        <Authorize> _
        Public Function pendiente_recoleccion() As ActionResult
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
                        If modelArray.Length <= index + 1 Then ReDim Preserve modelArray(index + 1)
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

        ' 2013.02.14
        ' GET: /logistica/reimprimir_formato
        Public Function reimprimir_formato() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As OrderListModel
            Dim index As Integer = 0

            Try
                Me.DR = Me.DA.ExecuteSP("lg_getTrackNoOrders")
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
                        If modelArray.Length <= index + 1 Then ReDim Preserve modelArray(index + 1)
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

        ' 2013.03.10
        ' GET: /almacen/delivery
        <Authorize> _
        Public Function delivery() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As PendingProcessWarehouseModel
            Dim index As Integer = 0
            Dim response As ActionResult

            ' -- Limpiar el TempData
            'TempData.Clear()

            Try
                Me.DR = Me.DA.ExecuteSP("pk_PendientesDelivery")
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
        ' POST: /almacen/delivery
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

        ' 2013.02.14
        ' GET: /almacen/LoadOrderIn
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

        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader
    End Class
End Namespace
