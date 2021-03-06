﻿Imports System.Data.SqlClient

Namespace AgioNet
    Public Class diagnosticoController
        Inherits System.Web.Mvc.Controller

        ' Methods
        ' 2013.02.13
        ' GET: /diagnostico/AddFailure
        <Authorize> _
        Public Function AddFailure() As PartialViewResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim myModel As New RegFailureModel

            Try
                Me.DR = Me.DA.ExecuteSP("dg_hasFailure", Session("OrderID"))
                If DR.HasRows Then
                    Do While DR.Read
                        myModel = New RegFailureModel With {.HasFailure = DR(0), .OrderID = DR(1), .Failure = DR(2), .Solution = DR(3), _
                                                            .Source = DR(4), .Comment = DR(5), .User = DR(6), .TipoReparacion = DR(7)}
                    Loop
                Else
                    myModel = New RegFailureModel With {.HasFailure = "0", .OrderID = "No data", .Failure = "No data", .Solution = "No data", _
                                                            .Source = "No data", .Comment = "No data", .User = "No data", .TipoReparacion = "no data"}
                End If
            Catch ex As Exception
                myModel = New RegFailureModel With {.HasFailure = "0", .OrderID = "No data", .Failure = "No data", .Solution = "No data", _
                                                            .Source = "No data", .Comment = "No data", .User = "No data", .TipoReparacion = "no data"}
                TempData("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try
            TempData("model") = myModel
            TempData.Keep("Failure")

            Return PartialView("_AddFailure")
        End Function

        ' 2013.02.13
        ' POST: /diagnostico/AddFailure
        <Authorize, HttpPost> _
        Public Function AddFailure(ByVal model As RegFailureModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim Result As ActionResult
            Try
                Me.DR = Me.DA.ExecuteSP("dg_RegFailure", model.OrderID, model.TipoReparacion, model.Failure, model.Solution, model.Source, model.Comment, model.User)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                Result = RedirectToAction("AddFailure")
                'Result = PartialView("realizar_pruebas")
            Catch ex As Exception
                TempData("ErrMsg") = ex.Message
                Result = RedirectToAction("AddFailure") ' PartialView("_AddFailure")
            Finally
                Me.DA.Dispose()
            End Try

            Return Result
        End Function

        ' 2013.02.13
        ' GET: /diagnostico/asignar_estacion
        <Authorize> _
        Public Function asignar_estacion() As ActionResult
            If Not Me.HasOrderID Then
                Return Me.RedirectToAction("Index")
            End If

            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As TestListModel() = New TestListModel(&H65 - 1) {}
            Dim index As Integer = 0
            Try
                Dim reader As SqlDataReader = Me.DA.ExecuteSP("dg_GetTestListByOrder", Session.Item("OrderID"), "")
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If reader.HasRows Then
                    Do While reader.Read
                        modelArray(index) = New TestListModel With {.TESTID = reader(0), .ORDERID = reader(1), .TESTNAME = reader(2), _
                                            .TESTDESCRIPTION = reader(3), .TESTRESULT = reader(4), .TESTSTART = reader(5), _
                                            .TESTEND = reader(6), .CREATEBY = reader(7)}

                        index += 1
                    Loop
                    '-- Actualizado por CarlosB
                    ReDim Preserve modelArray(index - 1)
                Else
                    modelArray(index) = New TestListModel With {.TESTID = "NO DATA", .ORDERID = "NO DATA", .TESTNAME = "NO DATA", _
                                        .TESTDESCRIPTION = "NO DATA", .TESTRESULT = "NO DATA", .TESTSTART = "NO DATA", _
                                        .TESTEND = "NO DATA", .CREATEBY = "NO DATA"}
                    index += 1
                    '-- Actualizado por CarlosB
                    ReDim Preserve modelArray(index - 1)

                End If
            Catch exception1 As Exception
                modelArray(index) = New TestListModel With {.TESTID = "NO DATA", .ORDERID = "NO DATA", .TESTNAME = "NO DATA", _
                                        .TESTDESCRIPTION = "NO DATA", .TESTRESULT = "NO DATA", .TESTSTART = "NO DATA", _
                                        .TESTEND = "NO DATA", .CREATEBY = "NO DATA"}
                index += 1
                '-- Actualizado por CarlosB
                ReDim Preserve modelArray(index - 1)
                TempData("ErrMsg") = exception1.Message
            Finally
                DA.Dispose()
            End Try

            Me.TempData.Item("Model") = modelArray
            Return Me.View
        End Function

        ' 2013.02.13
        ' POST: /diagnostico/asignarestacion
        <Authorize, HttpPost> _
        Public Function asignarestacion(ByVal model As AsignarEstacionModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As New MaterialMasterListModel
            Try
                Me.DR = Me.DA.ExecuteSP("dg_AssignStation", model.TestID, model.Estacion, model.User)
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Return Me.RedirectToAction("asignar_estacion")
                End If
                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = DR(0)
                Loop
            Catch exception1 As Exception
                Me.TempData.Item("ErrMsg") = exception1.Message
            Finally
                Me.DA.Dispose()
            End Try
            Return Me.RedirectToAction("asignar_estacion")
        End Function

        ' 2013.02.13
        ' GET: /diagnostico/asignarestacion
        <HttpGet, Authorize> _
        Public Function asignarestacion(ByVal model As TestIDModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As EstacionesModel
            Try
                Dim num As Integer
                Me.DR = Me.DA.ExecuteSP("in_station_getInfo", New Object() {"ALL"})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("asignar_estacion")
                End If

                If Me.DR.HasRows Then
                    num = 0
                    Do While Me.DR.Read
                        Dim model2 As New EstacionesModel With { _
                            .Nombre = DR(0), _
                            .Descripcion = DR(1), _
                            .Usuario = DR(2), _
                            .Proceso = DR(3) _
                        }
                        modelArray(num) = model2
                        num += 1
                    Loop
                    ' -- Reparado por CarlosB
                    ReDim Preserve modelArray(num - 1)

                    If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                        Me.DR.Close()
                    End If
                Else
                    num = 0
                    modelArray(num) = New EstacionesModel With { _
                        .Nombre = "No Data", _
                        .Descripcion = "No Data", _
                        .Usuario = "No Data", _
                        .Proceso = "No Data" _
                    }
                    num += 1
                    ' -- Reparado por CarlosB
                    ReDim Preserve modelArray(num - 1)
                End If
            Catch exception1 As Exception
                Me.TempData.Item("ErrMsg") = exception1.Message
                Return RedirectToAction("asignar_estacion")
            End Try

            Me.DA.Dispose()
            Me.TempData.Item("TestID") = model.TestID
            Me.TempData.Item("Model") = modelArray
            Return Me.View
        End Function


        '2013.02.13
        ' POST: /diagnostico/CancelTest
        <Authorize, HttpPost> _
        Public Function CancelTest(ByVal model As ExecTestModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("dg_CancelTest", model.TestID, model.TextLog)
            Catch exception1 As Exception
                TempData("ErrMsg") = exception1.Message
            Finally
                Me.DA.Dispose()
            End Try

            'Return Me.RedirectToAction("cancelar_prueba")
            Return PartialView("_ExecuteTest")
        End Function

        ' 2013.02.13
        ' GET: /diagnostico/CancelTest
        <Authorize> _
        Public Function CancelTest(ByVal model As TestListModel) As PartialViewResult
            ' Me.Session.Add("OtherTitle", "TestID")
            ' Me.Session.Add("OtherContent", model.TESTID)
            'Me.TempData.Item("TESTID") = model.TESTID
            'Return Me.View
            TempData.Keep("Failure")
            Return PartialView("_CancelTest")
        End Function

        ' 2013.02.13
        ''' <summary>
        ''' Funcion que elimina de la memoria, el error registrado en ErrMsg
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ClearErr()
            If Me.TempData.Item("ErrMsg") <> "" Then
                Me.TempData.Item("ErrMsg") = ""
            End If
        End Sub

        ' 2013.02.13
        ' GET: /diagnostico/DiagnosticMain
        <Authorize> _
        Public Function DiagnosticMain() As ActionResult
            If Me.HasOrderID Then
                Return Me.View
            End If
            Return Me.RedirectToAction("Index")
        End Function

        ' 2013.02.13
        ' POST: /diagnostico/DiagnosticMain
        <Authorize, HttpPost> _
        Public Function DiagnosticMain(ByVal model As StartDiagnosticModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = ""
            Try
                Me.ClearErr()
                Me.DR = Me.DA.ExecuteSP("dg_OrderValidation", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Return Me.View
                End If

                Do While Me.DR.Read
                    str = DR(0)
                Loop

                Me.Session.Item("DiagnosticID") = str
                Me.Session.Item("OrderID") = model.OrderID

                result = Me.RedirectToAction("DiagnosticMain")
            Catch exception1 As Exception
                Me.TempData.Item("ErrMsg") = exception1.Message
                Return Me.View
            Finally
                Me.DA.Dispose()
            End Try

            Return result
        End Function

        '2013.02.13
        ' POST: /diagnostico/ExecuteTest
        <Authorize, HttpPost> _
        Public Function ExecuteTest(ByVal model As ExecTestModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("dg_ExecTest", model.TestID, model.Result, model.TextLog, model.Failure)
                If DA._LastErrorMessage <> "" Then
                    'If (model.Result = "FAIL") Then
                    'Me.TempData.Item("TESTID") = model.TestID
                    'Return Me.RedirectToAction("AddFailure")
                    'End If
                    Throw New Exception(DA._LastErrorMessage)
                End If
            Catch exception1 As Exception
                TempData("ErrMsg") = exception1.Message
            Finally
                Me.DA.Dispose()
            End Try

            Return PartialView("_ExecuteTest") ' PartialView("_EjecuteTest") ' Me.RedirectToAction("realizar_pruebas")
        End Function

        ' 2013.02.13
        ' GET: /diagnostico/ExecuteTest
        <Authorize> _
        Public Function ExecuteTest(ByVal Model As TestListModel) As PartialViewResult
            TempData.Keep("Failure")
            Return Me.PartialView("_ExecuteTest")
        End Function

        ' 2013.02.13
        ' GET: /diagnostico/falla_reportada
        <Authorize> _
        Public Function falla_reportada() As PartialViewResult
            Dim result As New PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)

            Try
                Me.DR = Me.DA.ExecuteSP("dg_ViewReportedFailure", Session.Item("OrderID"))
                If (Me.DA._LastErrorMessage = "") Then
                    Me.TempData.Item("OrderID") = Session.Item("OrderID")
                    Do While Me.DR.Read
                        Me.TempData.Item("RFail") = DR(0)
                    Loop
                    result = PartialView("_FallaReportada")
                Else
                    Throw New Exception(DA._LastErrorMessage)
                End If
            Catch exception1 As Exception
                TempData("ErrMsg") = exception1.Message
                result = PartialView("_ExecuteTest")
            Finally
                DA.Dispose()
            End Try
            TempData.Keep("Failure")

            Return result
        End Function

        '2013.02.13
        ''' <summary>
        ''' Para determinar si hay registrada una Orden en la session
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function HasOrderID() As Boolean
            Dim flag2 As Boolean = True
            If Session("OrderID") Is Nothing Or Session("OrderID") = "" Then
                flag2 = False
            End If
            If Not Me.Session.Item("OtherTitle") Is Nothing Then
                Me.Session.Remove("OtherTitle")
                Me.Session.Remove("OtherContent")
            End If
            Return flag2
        End Function

        '2013.02.13
        ' GET: /diagnostico/Index
        <Authorize> _
        Public Function Index() As ActionResult
            Me.Session.Add("SubMenu", "DIAGNOSTIC")
            Me.Session.Add("OrderID", "")
            Return Me.View
        End Function

        ' 2013.02.13
        ' POST: /diagnostico/Index
        <HttpPost, Authorize> _
        Public Function Index(ByVal model As StartDiagnosticModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = ""
            Try
                Me.ClearErr()
                Me.DR = Me.DA.ExecuteSP("dg_OrderValidation", model.OrderID)

                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Return Me.View
                End If

                Do While Me.DR.Read
                    str = DR(0)
                Loop

                Me.Session.Item("DiagnosticID") = str
                Me.Session.Item("OrderID") = model.OrderID
                result = Me.RedirectToAction("DiagnosticMain")

            Catch exception1 As Exception
                Me.TempData.Item("ErrMsg") = exception1.Message
                Return View()
            Finally
                Me.DA.Dispose()
            End Try
            Return result
        End Function

        '2013.02.13
        ' GET: /diagnostico/iniciar_diagnostico
        <Authorize()> _
        Public Function iniciar_diagnostico() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As PendingOrdersModel
            Dim index As Integer = 0
            Dim response As ActionResult
            Dim ErrorMsg As String = String.Empty
            'ErrorMsg = TempData("ErrorMsg")
            
            ' -- Limpiar el TempData
            'TempData.Clear()
            'TempData("ErrorMsg") = ErrorMsg

            Try
                Me.DR = Me.DA.ExecuteSP("rc_getTransferDiagnostic")
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New PendingOrdersModel With {.OrderID = DR(0), .ProductType = DR(1), _
                                            .SerialNo = DR(2), .Model = DR(3), .Description = DR(4), .DateI = DR(5), _
                                            .DateR = DR(6), .DateD = DR(7)}
                        index += 1
                    Loop
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New PendingOrdersModel With {.OrderID = "No Data", .ProductType = "No Data", _
                                        .SerialNo = "No Data", .Model = "No Data", .Description = "No Data", .DateI = "no data", _
                                        .DateR = "No Data", .DateD = "no data"}
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

        '2013.02.13
        ' POST: /diagnostic/iniciar_diagnostico
        <HttpPost, Authorize> _
        Public Function iniciar_diagnostico(ByVal model As StartDiagnosticModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.ClearErr()
                Me.DR = Me.DA.ExecuteSP("dg_StartDiagnostic", model.OrderID, Me.User.Identity.Name, model.Comment)

                ' -- Si no se ha detectado un error, se trata de un mensaje de status
                If (Me.DA._LastErrorMessage = "") Then
                    Do While Me.DR.Read
                        Me.TempData.Item("StatusMsg") = DR(0)
                    Loop
                    Session.Add("OrderID", model.OrderID)
                    result = Me.RedirectToAction("DiagnosticMain")
                Else
                    ' Mensaje de error desde la Base de datos
                    Throw New Exception(Me.DA._LastErrorMessage)
                End If
            Catch exception1 As Exception
                ' -- Mensaje de error probocado en el sistema
                TempData("ErrMsg") = exception1.Message
                result = Me.RedirectToAction("iniciar_diagnostico")
            Finally
                DA.Dispose()
            End Try

            Return result
        End Function

        '
        ' 2013.02.13
        ' GET: /diagnostico/LoadOrderInfo
        <Authorize> _
        Public Function LoadOrderInfo() As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model As New OrderInfoModel
            Dim num As Integer = 0
            Try
                Dim model2 As OrderInfoModel
                Me.DR = Me.DA.ExecuteSP("sc_getOrderInfo", Session.Item("OrderID"))

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
                            .commodity = DR(28),
                            .SerialNo = DR(29), _
                            .Revision = DR(30), _
                            .ServiceType = DR(31), _
                            .FailureType = DR(32), _
                            .Comment = DR(33) _
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
                Me.TempData.Item("ErrMsg") = exception1.Message
                Me.DA.Dispose()
            End Try
            Me.TempData.Item("Model") = model
            Return Me.PartialView("_OrderInfoPartial")
        End Function

        ' 2013.02.13
        ' GET: /diagnostico/loadTestInfo
        <Authorize> _
        Public Function loadTestInfo() As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model As New TestInfoModel
            Dim num As Integer = 0
            Try
                Dim model2 As TestInfoModel
                Me.DR = Me.DA.ExecuteSP("dg_getTestDetail", Me.TempData.Item("TestID"))

                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    model2 = New TestInfoModel With { _
                        .TestID = "No Data", _
                        .OrderID = "No Data", _
                        .TestName = "No Data", _
                        .TestDescription = "No Data", _
                        .TestStart = "No Data", _
                        .TestEnd = "No Data", _
                        .TestResult = "No Data", _
                        .CreateBy = "No Data" _
                    }
                    model = model2
                    Me.TempData.Item("Model") = model
                    Return Me.PartialView("_TestInfoPartial")
                End If
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        model2 = New TestInfoModel With { _
                            .TestID = DR(0), _
                            .OrderID = DR(1), _
                            .TestName = DR(2), _
                            .TestDescription = DR(3), _
                            .TestStart = DR(4), _
                            .TestEnd = DR(5), _
                            .TestResult = DR(6), _
                            .CreateBy = DR(7) _
                        }
                        model = model2
                    Loop

                    If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                        Me.DR.Close()
                    End If
                    Me.DA.Dispose()
                Else
                    model2 = New TestInfoModel With { _
                        .TestID = "No Data", _
                        .OrderID = "No Data", _
                        .TestName = "No Data", _
                        .TestDescription = "No Data", _
                        .TestStart = "No Data", _
                        .TestEnd = "No Data", _
                        .TestResult = "No Data", _
                        .CreateBy = "No Data" _
                    }
                    model = model2
                    num += 1
                End If
            Catch exception1 As Exception
                Me.TempData.Item("ErrMsg") = exception1.Message
                Me.DA.Dispose()
            End Try

            Me.TempData.Item("Model") = model
            Me.TempData.Keep("OrderID")
            Return Me.PartialView("_TestInfoPartial")

        End Function

        '2013.02.13
        ' GET: /diagnostico/LoadWnOrderInfo
        Public Function LoadWnOrderInfo() As PartialViewResult
            Return Me.PartialView("_wnOrderInfoPartial")
        End Function

        '2013.02.13
        ' GET: /diagnostico/pedir_aprobar
        <Authorize> _
        Public Function pedir_aprobar() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As TestReportModel
            Dim index As Integer = 0
            Dim response As ActionResult = Me.View
            Dim strFailure As String
            Dim strLog As String

            If Me.HasOrderID Then
                Try
                    Dim reader As SqlDataReader = Me.DA.ExecuteSP("dg_GetTestListByOrder", Me.Session.Item("OrderID"), "")
                    If reader.HasRows Then
                        Do While reader.Read
                            If Len(reader(4)) >= 35 Then strFailure = Mid(reader(4), 1, 30) & "..." Else strFailure = reader(4)
                            If Len(reader(5)) >= 35 Then strLog = Mid(reader(5), 1, 30) & "..." Else strLog = reader(5)

                            modelArray(index) = New TestReportModel With {.OrderID = reader(0), .TestName = reader(1), _
                                                .TestResult = reader(2), .CapFailure = reader(3), .Failure = strFailure, .TextLog = strLog, _
                                                .TestStart = reader(6), .TestEnd = reader(7), .CreateBy = reader(8), .TipoReparacion = reader(9)}
                            index += 1
                        Loop

                        '-- Actualizado por Carlos Barreto
                        ReDim Preserve modelArray(index - 1)
                    Else
                        Throw New Exception("Error: No se han encontrado Datos")
                    End If
                    If Not reader.IsClosed Then
                        reader.Close()
                    End If
                    DR = DA.ExecuteSP("dg_getReportedFailure", Me.Session.Item("OrderID"))
                    If DA._LastErrorMessage <> "" Then
                        Throw New Exception(DA._LastErrorMessage)
                    End If

                    If DR.HasRows Then
                        While DR.Read
                            Me.TempData("FallaReportada") = DR(0)
                        End While
                    Else
                        Me.TempData("FallaReportada") = "No disponible"
                    End If

                    response = Me.View
                Catch exception1 As Exception
                    index = 0
                    modelArray(index) = New TestReportModel With {.OrderID = "No data", .TestName = "No data", .TestResult = "No data", _
                                        .CapFailure = "No data", .Failure = "No data", .TextLog = "No data", .TestStart = "No data", _
                                        .TestEnd = "No data", .CreateBy = "No data", .TipoReparacion = "No data"}
                    index += 1
                    '-- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                    TempData("ErrMsg") = exception1.Message
                Finally
                    DA.Dispose()
                End Try

                Me.TempData.Item("Model") = modelArray
                Return Me.View
            Else
                response = Me.RedirectToAction("Index")
            End If

            Return response 'Me.RedirectToAction("Index")
        End Function

        ' 2013.02.13
        ' POST: /diagnostico/pedir_aprobar
        <Authorize, HttpPost> _
        Public Function pedir_aprobar(ByVal model As ReqApprovalModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim email As New agEmail
            Dim emailAddress As String = String.Empty
            Dim txtSubject As String = String.Empty
            Dim txtMessage As String = String.Empty
            Try
                Me.DR = Me.DA.ExecuteSP("dg_ReqApproval", Session.Item("OrderID"), model.Retro, model.TComment, _
                                        model.Comment, Me.User.Identity.Name)

                If (Me.DA._LastErrorMessage = "") Then
                    Me.TempData.Item("ErrMsg") = "Se ha mandado la solicitud de aprobación para la orden :" _
                        & Session.Item("OrderID")

                    '-- Enviar correo
                    Dim OrderID As String = Mid(Me.Session.Item("OrderID"), 1, Me.Session.Item("OrderID").ToString.Length - 3) & "-01"
                    email.HTMLBody = True
                    emailAddress = mailing.getDistributionListReqApproval(User.Identity.Name, OrderID)
                    txtSubject = "Solicitud de cotización para la orden : " & OrderID
                    txtMessage = mailing.getDiagnosticEmail(OrderID)

                    'txtMessage = "<p style='font-family: Arial; font-size:12px; width:800px;'> " & _
                    '             "Se ha registrado una solicitud de cotización de parte para la orden: <b>" & _
                    '             Session.Item("OrderID") & "</b>. Esta opción se encuentra disponible desde " & _
                    '             "Servicio a Clientes / 4.- Cotización de Partes<p>"
                    '
                    email.SendEmail(emailAddress, txtSubject, txtMessage)
                Else
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                End If
                Return Me.RedirectToAction("DiagnosticMain")

            Catch exception1 As Exception
                Me.TempData.Item("ErrMsg") = exception1.Message
                Return RedirectToAction("DiagnosticMain")
            Finally
                Me.DA.Dispose()
            End Try
            Return Me.View
        End Function

        ' 2013.02.13
        ' GET: /diagnostico/programar_prueba
        ' Upd 2013.02.19 -- Quitar campos upd
        <Authorize> _
        Public Function programar_prueba() As ActionResult
            Dim response As ActionResult
            If Me.HasOrderID Then
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Dim modelArray(100) As TestIDListModel
                Dim index As Integer = 0

                Try
                    ' Obtener el listado de pruebas
                    DR = Me.DA.ExecuteSP("dg_GetTestList")
                    index = 0
                    If DA._LastErrorMessage = "" Then
                        Do While Me.DR.Read
                            modelArray(index) = New TestIDListModel With { _
                                .TestID = DR(0), _
                                .TestName = DR(1) _
                            }
                            index += 1
                        Loop
                        ' -- Actualizado por CarlosBarreto
                        ReDim Preserve modelArray(index - 1)

                        Me.TempData.Item("Model") = modelArray
                        response = Me.View
                    Else
                        Throw New Exception(DA._LastErrorMessage)
                    End If

                Catch ex As Exception
                    Me.TempData.Item("ErrMsg") = ex.Message
                    response = Me.RedirectToAction("DiagnosticMain")
                Finally
                    DA.Dispose()
                End Try
            Else
                response = Me.RedirectToAction("Index")
            End If

            Return response
        End Function

        ' 2013.02.13
        ' POST: /diagnostico/programar_prueba
        ' Upd 2013.02.19 -- Quitar campos upd 
        <HttpPost, Authorize> _
        Public Function programar_prueba(ByVal md As SheduleTestModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim Retorno As ActionResult = Me.View
            Dim index As Integer = 0
            Try
                DR = DA.ExecuteSP("dg_AddTest", md.OrderID, md.Prueba, User.Identity.Name, md.Marca, md.PartNo, _
                                  md.SerialNo, md.Revision, md.Comentarios)
                If DA._LastErrorMessage = "" Then
                    If DR.HasRows Then
                        While DR.Read
                            Me.TempData.Item("StatusMsg") = "Se ha registrado la prueba [" & DR(0) & "]"
                            Retorno = Me.RedirectToAction("DiagnosticMain")
                        End While
                    End If
                Else
                    Throw New Exception(DA._LastErrorMessage)
                    Retorno = Me.View
                End If
            Catch ex As Exception
                Dim modelArray(100) As TestIDListModel
                TempData("ErrMsg") = ex.Message
                DA.Dispose()
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                DR = Me.DA.ExecuteSP("dg_GetTestList")
                index = 0

                If DA._LastErrorMessage = "" Then
                    Do While Me.DR.Read
                        modelArray(Index) = New TestIDListModel With { _
                            .TestID = DR(0), _
                            .TestName = DR(1) _
                        }
                        Index += 1
                    Loop
                    ' -- Actualizado por CarlosBarreto
                    ReDim Preserve modelArray(index - 1)

                    Me.TempData.Item("Model") = modelArray
                    Retorno = Me.View
                Else
                    TempData("ErrMsg") = DA._LastErrorMessage
                    Retorno = RedirectToAction("DiagnosticMain")
                End If

                TempData("Model") = modelArray

                Retorno = Me.View
                If Not DR.IsClosed Then
                    DR.Close()
                End If
            Finally
                DA.Dispose()
            End Try
            
            Session("OrderID") = md.OrderID
            Return Retorno
        End Function

        '2013.02.13
        ' Actualizado el 2013.02.19 - Modificacion para estación de pruebas
        ' GET: /diagnostico/realizar_pruebas
        <Authorize> _
        Public Function realizar_pruebas() As ActionResult
            Dim response As ActionResult
            If Me.HasOrderID Then
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Dim modelArray(100) As TestByOrderIDModel
                Dim index As Integer = 0

                Try
                    ' Obtener el listado de pruebas disponiblles
                    Session("OrderID") = Mid(Session("OrderID"), 1, Session("OrderID").ToString.Length - 3) & "-01"
                    DR = Me.DA.ExecuteSP("dg_GetTestListByOrder", Session("OrderID"))
                    index = 0
                    If DA._LastErrorMessage = "" Then
                        If DR.HasRows Then
                            Do While Me.DR.Read
                                modelArray(index) = New TestByOrderIDModel With {.OrderID = DR(0), .TestName = DR(1), .TestResult = DR(2), _
                                                    .CapFailure = DR(3), .Failure = DR(4), .TextLog = DR(5), .TestStart = DR(6), .TestEnd = DR(7),
                                                    .CreateBy = DR(8)}
                                index += 1
                            Loop
                            ' -- Actualizado por CarlosBarreto
                            ReDim Preserve modelArray(index - 1)

                            Me.TempData.Item("Model") = modelArray
                            response = Me.View
                        Else
                            Throw New Exception("Error! No se han encontrado pruebas para esta orden")
                        End If
                    Else
                        Throw New Exception(DA._LastErrorMessage)
                    End If

                Catch ex As Exception
                    Me.TempData.Item("ErrMsg") = ex.Message
                    response = Me.RedirectToAction("DiagnosticMain")
                Finally
                    DA.Dispose()
                End Try
            Else
                response = Me.RedirectToAction("Index")
            End If

            Return response
        End Function

        ' 2013.02.19 
        ' 2013.03.13 Upd para mostrar listado de pruebas
        ' POST: /diagnostico/realizar_pruebas
        <Authorize, HttpPost> _
        Public Function realizar_pruebas(ByVal model As StationByUser) As ActionResult
            Return RedirectToAction("Index") '  Me.View
        End Function

        ' 2013.02.19 
        ' GET: /diagnostico/pruebasMaster
        Public Function pruebasMaster(ByVal model As ScanOrderModel) As ActionResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim Retorno As ActionResult
            Dim myModel As New TestOrderInfoModel

            Try
                ' al llegar aqui, borrar la sesion 

                DR = DA.ExecuteSP("dbo.dg_getTestInfo", model.OrderID)
                If DA._LastErrorMessage = "" Then
                    '-- Asignar los datos de la orden
                    If DR.HasRows Then
                        Do While DR.Read
                            myModel = New TestOrderInfoModel With {.OrderID = DR(0), .OrderDate = DR(1), .TestID = DR(2), .ProductClass = DR(3), _
                                      .ProductType = DR(4), .ProductTrademark = DR(5), .ProductModel = DR(6), .ProductDescription = DR(7), _
                                      .PartNumber = DR(8), .SerialNumber = DR(9), .FailureType = DR(10), .Comment = DR(11), _
                                      .DateLog = DR(12), .TextLog = DR(13)
                                }
                            TempData("Failure") = DR(10)
                        Loop
                    Else
                        'Si no hay datos, mostrar error
                        Throw New Exception("Error, no se han encontrado datos")
                    End If

                    '-- La información de la suborder, se guarda en la sesion
                    Session.Add("OrderID", model.OrderId)
                    'Session.Add("Station", model.stName)
                    Session.Add("OrderInfo", myModel)

                    '-- Si no hay errores, comprobar valores de is tested
                    Retorno = Me.View 'Me.RedirectToAction("pruebasMaster")
                Else
                    Throw New Exception(DA._LastErrorMessage)
                End If
            Catch ex As Exception
                '-- En caso de error, limpiar las variables y mostrar el error
                Session.Clear() 'Limpiar toda la sesion (El login se guarda en cookie)
                TempData("ErrMsg") = ex.Message
                TempData.Keep("Model")
                TempData.Keep("Failure")
                Retorno = Me.RedirectToAction("realizar_pruebas")
            Finally
                DA.Dispose()
            End Try

            Return Retorno
        End Function


        ' 2013.02.13
        ' GET: /diagnostico/ver_falla
        <Authorize> _
        Public Function ver_falla() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As TestListModel
            Dim index As Integer = 0
            If Me.HasOrderID Then
                Try
                    Dim model As TestListModel
                    Dim reader As SqlDataReader = Me.DA.ExecuteSP("dg_GetTestListByOrder", Me.Session.Item("OrderID"), "")

                    If reader.HasRows Then
                        Do While reader.Read
                            model = New TestListModel With { _
                                .TESTID = reader(0), _
                                .ORDERID = reader(1), _
                                .TESTNAME = reader(2), _
                                .TESTDESCRIPTION = reader(3), _
                                .TESTRESULT = reader(4), _
                                .TESTSTART = reader(5), _
                                .TESTEND = reader(6), _
                                .CREATEBY = reader(7) _
                            }
                            modelArray(index) = model
                            index += 1
                        Loop

                        '-- Actualizado por Carlos Barreto
                        ReDim Preserve modelArray(index - 1)
                    Else
                        model = New TestListModel With { _
                            .TESTID = "NO DATA", _
                            .ORDERID = "NO DATA", _
                            .TESTNAME = "NO DATA", _
                            .TESTDESCRIPTION = "NO DATA", _
                            .TESTRESULT = "NO DATA", _
                            .TESTSTART = "NO DATA", _
                            .TESTEND = "NO DATA", _
                            .CREATEBY = "NO DATA" _
                        }
                        modelArray(index) = model
                        index += 1

                        '-- Actualizado por Carlos Barreto
                        ReDim Preserve modelArray(index - 1)

                    End If
                Catch exception1 As Exception
                    modelArray(index) = New TestListModel With { _
                        .TESTID = "NO DATA", _
                        .ORDERID = "NO DATA", _
                        .TESTNAME = "NO DATA", _
                        .TESTDESCRIPTION = "NO DATA", _
                        .TESTRESULT = "NO DATA", _
                        .TESTSTART = "NO DATA", _
                        .TESTEND = "NO DATA", _
                        .CREATEBY = "NO DATA" _
                    }
                    index += 1
                    '-- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)

                    Me.DA.Dispose()
                    Return View()
                End Try
                Me.TempData.Item("Model") = modelArray
                Return Me.View
            End If
            Return Me.RedirectToAction("Index")
        End Function

        
        '2013.02.13
        ' POST: /diagnostico/ViewFailure
        <HttpPost, Authorize> _
        Public Function ViewFailure() As ActionResult
            Return Me.RedirectToAction("ver_falla")
        End Function

        ' 2013.02.13
        ' GET: /diagnostico/ViewFailure
        <Authorize> _
        Public Function ViewFailure(ByVal model As ViewFoundFailureModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Dim _view As New FailureView
                Me.DR = Me.DA.ExecuteSP("dg_ViewFoundedFailures", New Object() {model.TestID})
                Do While Me.DR.Read
                    _view.FAILUREID = DR(0)
                    _view.TESTID = DR(1)
                    _view.DESCRIPTION = DR(2)
                    _view.POSSIBLESOLUTION = DR(3)
                    _view.FOUNDBY = DR(4)
                    _view.FOUNDDATE = DR(5)
                    _view.RESOLVED = DR(6)
                Loop
                Me.TempData.Item("TestDetail") = _view
                Me.DA.Dispose()
            Catch exception1 As Exception
                Return Me.RedirectToAction("ver_falla")
            End Try
            Return Me.View
        End Function


        ' 2013.02.13
        ' GET: /diagnostico/ViewTest
        <Authorize> _
        Public Function ViewTest() As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim Retorno As New PartialViewResult
            Try
                Dim isTested As New isTestedModel '= Me.Session("isTested")
                isTested.IsTested(Session("OrderID"))

                Dim view As New TestView
                Me.DR = Me.DA.ExecuteSP("dg_ViewTest", isTested.TestID)

                If DR.HasRows Then

                    Do While Me.DR.Read
                        view.TESTID = DR(0)
                        view.ORDERID = DR(1)
                        view.TESTNAME = DR(2)
                        view.TESTDESCRIPTION = DR(3)
                        view.TESTRESULT = DR(4)
                        view.TESTSTART = DR(5)
                        view.TESTEND = DR(6)
                        view.FAILURE = DR(7)
                        view.TEXTLOG = DR(8)
                        view.CREATEBY = DR(9)
                    Loop

                    Me.TempData.Item("TestDetail") = view
                    Retorno = PartialView("_ViewTest")
                Else
                    Throw New Exception("No se han encontrado datos")
                End If
            Catch exception1 As Exception
                TempData("ErrMsg") = exception1.Message
                Retorno = PartialView("_ExecuteTest")
            Finally
                DA.Dispose()
            End Try
            TempData.Keep("Failure")
            Return Retorno
        End Function


        '2013.04.15
        ' GET: /diagnostico/Ordenes_En_Diagnostico
        Public Function Ordenes_En_Diagnostico() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As PendingOrdersModel
            Dim index As Integer = 0
            Dim response As ActionResult
            Dim ErrorMsg As String = String.Empty
            'ErrorMsg = TempData("ErrorMsg")

            ' -- Limpiar el TempData
            'TempData.Clear()
            'TempData("ErrorMsg") = ErrorMsg

            Try
                Me.DR = Me.DA.ExecuteSP("rc_getOnDiagnosticOrders")
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New PendingOrdersModel With {.OrderID = DR(0), .PartNo = DR(1), .ProductType = DR(2), _
                                            .SerialNo = DR(3), .Model = DR(4), .Description = DR(5), .DateR = DR(6), .DateD = DR(7), _
                                            .CreateBy = DR(8), .DiasTranscurridos = DR(9)}
                        If index + 1 = modelArray.Length Then ReDim Preserve modelArray(index + 10)
                        index += 1
                    Loop
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New PendingOrdersModel With {.OrderID = "No Data", .PartNo = "No Data", .ProductType = "No Data", _
                        .SerialNo = "No Data", .Model = "No Data", .Description = "No Data"}
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

        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader
    End Class

End Namespace
