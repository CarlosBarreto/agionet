Imports System.Data.SqlClient

Namespace AgioNet
    Public Class scController
        Inherits System.Web.Mvc.Controller

        ' Methods
        ' 2013.02.14 
        ' GET: /sc/approvalRepair
        <Authorize> _
        Public Function approvalRepair(ByVal model As ScanOrderModel) As ActionResult
            Dim modelArray(100) As AppFailureInfoModel
            Dim index As Integer = 0
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Me.Session.Item("OrderID") = model.OrderID

            Try
                Dim info As New AllOrderInfo
                Me.DR = Me.DA.ExecuteSP("sc_OrderMaster_getOrderList", New Object() {"ByOrder", model.OrderID})
                If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                    Do While Me.DR.Read
                        info.OrderID = DR(0)
                        info.OrderDate = DR(1)
                        info.CustomerType = DR(2)
                        info.RFC = DR(3)
                        info.Email = DR(4)
                        info.Address = DR(5)
                        info.ExternalNumber = DR(6)
                        info.InternalNumber = DR(7)
                        info.Address2 = DR(8)
                        info.City = DR(9)
                        info.State = DR(10)
                        info.Country = DR(11)
                        info.ZipCode = DR(12)
                        info.Telephone = DR(13)
                        info.Telephone2 = DR(14)
                        info.Telephone3 = DR(15)
                        info.Delivery = DR(15)
                        info.DeliveryTime = DR(17)
                        info.ProductClass = DR(18)
                        info.ProductType = DR(19)
                        info.ProductTrademark = DR(20)
                        info.ProductModel = DR(21)
                        info.ProductDescription = DR(22)
                        info.PartNumber = DR(23)
                        info.SerialNumber = DR(24)
                        info.Revision = DR(25)
                        info.ServiceType = DR(26)
                        info.FailureType = DR(27)
                        info.Comment = DR(28)
                    Loop
                End If

                If Not Me.DR.IsClosed Then
                    Me.DR.Close()
                End If

                Me.DR = Me.DA.ExecuteSP("sc_getFailureInfo", model.OrderID)
                If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                    Do While Me.DR.Read
                        modelArray(index) = New AppFailureInfoModel With { _
                            .OrderID = DR(0), _
                            .TestID = DR(1), _
                            .TestDescription = DR(2), _
                            .FailureID = DR(3), _
                            .FailureDescription = DR(4), _
                            .PossibleSolution = DR(5), _
                            .Log = DR(6), _
                            .TestResult = DR(7), _
                            .FoundBy = DR(8), _
                            .FoundDate = DR(9) _
                        }
                        index += 1
                    Loop
                    ' -- actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If

                Me.TempData.Item("OrderInfo") = info
                Me.TempData.Item("FailureInfo") = modelArray
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.RedirectToAction("Index")
            End Try

            Return Me.View
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
                    Dim str5 As String
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

        ' 2013.02.14
        ' GET: /sc/ver_orden
        <Authorize> _
        Public Function ver_orden() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As OrderListModel
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrderMaster_getOrderList", "OrderList")
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
                            .SerialNo = DR(10), _
                            .Status = DR(11) _
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
                        .SerialNo = "No Data", _
                        .Status = "No Data" _
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

        '2013.02.14
        ' GET: /sc/ver_ordenes
        <Authorize> _
        Public Function ver_ordenes() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As OrderListModel
            Dim index As Integer = 0

            Try
                Me.DR = Me.DA.ExecuteSP("sc_OrderMaster_getOrderList", "OrderList")
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
                    ' -- Actualizado por CarlosB
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
                    ' -- Actualizado por CarlosB
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


        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader

    End Class

End Namespace
