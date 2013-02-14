﻿Imports System.Data.SqlClient

Namespace AgioNet
    Public Class diagnosticoController
        Inherits System.Web.Mvc.Controller

        ' Methods
        ' 2013.02.13
        ' GET: /diagnostico/AddFailure
        <Authorize> _
        Public Function AddFailure(ByVal model As ExecTestModel) As ActionResult
            Me.Session.Add("OtherTitle", "TestID")
            Me.Session.Add("OtherContent", model.TestID)
            If (model.TestID = "") Then
                Me.TempData.Keep("TESTID")
            Else
                Me.TempData.Item("TESTID") = model.TestID
            End If
            Return Me.View
        End Function

        ' 2013.02.13
        ' POST: /diagnostico/AddFailure
        <Authorize, HttpPost> _
        Public Function AddFailure(ByVal model As RegFailureModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("dg_RegFailure", model.TestID, model.Failure, model.PSolution, model.User)
                Me.DA.Dispose()
            Catch exception1 As Exception
                Me.DA.Dispose()
                Dim result As ActionResult = Me.View
                Return result
            End Try
            Return Me.RedirectToAction("DiagnosticMain")
        End Function

        ' 2013.02.13
        ' GET: /diagnostico/agregar_falla
        <Authorize> _
        Public Function agregar_falla() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As TestListModel() = New TestListModel(&H65 - 1) {}
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

                        '-- Actualizado por CarlosB
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
                        '-- Actualizado por CarlosB
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
                    'modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New TestListModel(((index - 1) + 1) - 1) {}), TestListModel())
                    '-- Actualizado por CarlosB
                    ReDim Preserve modelArray(index - 1)
                    Me.DA.Dispose()
                End Try
                Me.TempData.Item("Model") = modelArray
                Return Me.View
            End If
            Return Me.RedirectToAction("Index")
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
                If reader.HasRows Then
                    Do While reader.Read
                        Dim model As New TestListModel With { _
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
                    '-- Actualizado por CarlosB
                    ReDim Preserve modelArray(index - 1)
                Else
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
                    '-- Actualizado por CarlosB
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
                '-- Actualizado por CarlosB
                ReDim Preserve modelArray(index - 1)
                Me.DA.Dispose()
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

        ' 2013.02.13
        ' GET: /diagnostico/cancelar_prueba
        <Authorize> _
        Public Function cancelar_prueba() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As TestListModel
            Dim index As Integer = 0
            If Me.HasOrderID Then
                Try
                    Dim model As TestListModel
                    Dim reader As SqlDataReader = DA.ExecuteSP("dg_GetTestListByOrder", Session.Item("OrderID"), "")
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
                        ' -- Actualizado por CarlosB
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
                        ' -- Actualizado por CarlosB
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
                    ' -- Actualizado por CarlosB
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
        ' POST: /diagnostico/CancelTest
        <Authorize, HttpPost> _
        Public Function CancelTest(ByVal model As ExecTestModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("dg_CancelTest", model.TestID, model.TextLog)
                Me.DA.Dispose()
            Catch exception1 As Exception
                Me.DA.Dispose()
                Return View()
            End Try
            Return Me.RedirectToAction("cancelar_prueba")
        End Function

        ' 2013.02.13
        ' GET: /diagnostico/CancelTest
        <Authorize> _
        Public Function CancelTest(ByVal model As TestListModel) As ActionResult
            Me.Session.Add("OtherTitle", "TestID")
            Me.Session.Add("OtherContent", model.TESTID)
            Me.TempData.Item("TESTID") = model.TESTID
            Return Me.View
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
        Public Function ExecuteTest(ByVal model As ExecTestModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("dg_ExecTest", model.TestID, model.Result, model.TextLog)

                If DA._LastErrorMessage <> "" Then
                    Me.DA.Dispose()
                    If (model.Result = "FAIL") Then
                        Me.TempData.Item("TESTID") = model.TestID
                        Return Me.RedirectToAction("AddFailure")
                    End If
                End If
            Catch exception1 As Exception
                Me.DA.Dispose()
                Return View()
            End Try
            Return Me.RedirectToAction("realizar_pruebas")
        End Function

        ' 2013.02.13
        ' GET: /diagnostico/ExecuteTest
        <Authorize> _
        Public Function ExecuteTest(ByVal Model As TestListModel) As ActionResult
            Me.Session.Add("OtherTitle", "TestID")
            Me.Session.Add("OtherContent", Model.TESTID)
            Me.TempData.Item("TESTID") = Model.TESTID
            Return Me.View
        End Function

        ' 2013.02.13
        ' GET: /diagnostico/falla_reportada
        <Authorize> _
        Public Function falla_reportada() As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("dg_ViewReportedFailure", Session.Item("OrderID"))
                If (Me.DA._LastErrorMessage = "") Then
                    Me.TempData.Item("OrderID") = Session.Item("OrderID")
                    Do While Me.DR.Read
                        Me.TempData.Item("RFail") = DR(0)
                    Loop
                    Me.DA.Dispose()
                    Return Me.View
                End If

                Me.DA.Dispose()
                result = Me.RedirectToAction("DiagnosticMain")

            Catch exception1 As Exception
                Return RedirectToAction("DiagnosticMain")
            End Try

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
            If Session.Item("OrderID") Is Nothing Then
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
        ' GET: /diagnostico/inicar_diagnostico
        <Authorize()> _
        Public Function iniciar_diagnostico() As ActionResult
            Return Me.View
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

                If (Me.DA._LastErrorMessage = "") Then
                    Do While Me.DR.Read
                        Me.TempData.Item("ErrMsg") = DR(0)
                    Loop
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("DiagnosticMain")
                End If

                Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                Me.DA.Dispose()
                result = Me.View
            Catch exception1 As Exception
                TempData("ErrMsg") = exception1.Message
                result = Me.View
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
                            .Tel3 = DR(19) _
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
                    index = 0
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
                    Return Me.View
                End Try

                Me.TempData.Item("Model") = modelArray
                Return Me.View
            End If
            Return Me.RedirectToAction("Index")
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
                Me.DR = Me.DA.ExecuteSP("dg_ReqApproval", Session.Item("OrderID"), model.Comment, Me.User.Identity.Name)

                If (Me.DA._LastErrorMessage = "") Then
                    Me.TempData.Item("ErrMsg") = "Se ha mandado la solicitud de aprobación para la orden :" & Session.Item("OrderID")

                    If Not Me.DR.IsClosed Then
                        Me.DR.Close()
                    End If

                    Me.DR = Me.DA.ExecuteSP("sys_getDistibutionList", "REQAPPROV")
                    If Me.DR.HasRows Then
                        Do While Me.DR.Read
                            emailAddress = DR(0)
                        Loop

                        email.HTMLBody = True
                        txtSubject = "Solicitud de aprobación para la orden : " & Me.Session.Item("OrderID")
                        txtMessage = "<p style='font-family: Arial; font-size:12px; width:800px;'> " & _
                                     "Se ha registrado una solicitud de aprobacion de reparación para la orden: <b>" & _
                                     Session.Item("OrderID") & "</b>. Esta opción se encuentra disponible desde " & _
                                     "Atención a Clientes / Aprovación de Reparación<p>"

                        email.SendEmail(emailAddress, txtSubject, txtMessage)
                    End If
                    Me.DA.Dispose()
                Else
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Me.DA.Dispose()
                End If
                Return Me.RedirectToAction("DiagnosticMain")

            Catch exception1 As Exception
                Me.TempData.Item("ErrMsg") = exception1.Message
                Return RedirectToAction("DiagnosticMain")
            End Try
            Return Me.View
        End Function

        ' 2013.02.13
        ' GET: /diagnostico/programar_prueba
        <Authorize> _
        Public Function programar_prueba() As ActionResult
            If Me.HasOrderID Then
                Try
                    Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                    Dim modelArray(100) As TestIDListModel
                    Me.DR = Me.DA.ExecuteSP("dg_GetTestList")

                    Dim index As Integer = 0

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
                    Me.DA.Dispose()
                Catch exception1 As Exception
                    Me.TempData.Item("ErrMsg") = exception1.Message
                    Me.DA.Dispose()
                    Return Me.RedirectToAction("Index")
                End Try
                Return Me.View
            End If
            Return Me.RedirectToAction("Index")
        End Function

        ' 2013.02.13
        ' GET: /diagnostico/programar_prueba
        <HttpPost, Authorize> _
        Public Function programar_prueba(ByVal md As SheduleTestModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = Me.Session.Item("OrderID")

            If (md.TestID <> "") Then
                Me.DA.ExecuteSP("dg_AddTest", str, md.TestID, md.TestComment, Me.User.Identity.Name, md.ProductClass, md.ProductType, _
                                md.Trademark, md.Model, md.Description, md.PartNo, md.SerialNo, md.Revision, md.Failure)

            End If

            If (Me.DA._LastErrorMessage = "") Then
                Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
            Else
                Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
            End If

            Me.Session.Item("OrderID") = str
            Return Me.RedirectToAction("DiagnosticMain")
        End Function

        '2013.02.13
        ' GET: /diagnostico/realizar_pruebas
        <Authorize> _
        Public Function realizar_pruebas() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As TestListModel
            Dim index As Integer = 0
            If Me.HasOrderID Then
                Dim model As TestListModel

                Try
                    Dim reader As SqlDataReader = Me.DA.ExecuteSP("dg_GetTestListByOrder", Me.Session.Item("OrderID"), "")
                    If reader.HasRows Then
                        Do While reader.Read
                                model = New TestListModel With { _
                                    .TESTID = reader(0)), _
                                    .ORDERID = reader(1)), _
                                    .TESTNAME = reader(2)), _
                                    .TESTDESCRIPTION = reader(3)), _
                                    .TESTRESULT = reader(4)), _
                                    .TESTSTART = reader(5)), _
                                    .TESTEND = reader(6)), _
                                    .CREATEBY = reader(7)) _
                                }
                            modelArray(index) = model
                            index += 1
                        Loop

                        ' -- Actualizado por Carlos Barreto
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
                        ' -- Actualizado por Carlos Barreto
                        ReDim Preserve modelArray(index - 1)
                    End If
                Catch exception1 As Exception
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
                    modelArray(0) = model
                    index += 1
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)

                    Me.DA.Dispose()
                    Return Me.View
                End Try
                Me.TempData.Item("Model") = modelArray
                Return Me.View
            End If
            Return Me.RedirectToAction("Index")
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

        ' 2013.02.13
        ' GET: /diagnostico/ver_prueba
        <Authorize> _
        Public Function ver_prueba() As ActionResult
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

        '2013.02.13
        ' POST: /diagnostico/ViewTest
        <Authorize, HttpPost> _
        Public Function ViewTest() As ActionResult
            Return Me.RedirectToAction("ver_prueba")
        End Function

        ' 2013.02.13
        ' GET: /diagnostico/ViewTest
        <Authorize> _
        Public Function ViewTest(ByVal model As TestListModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Dim view As New TestView
                Me.DR = Me.DA.ExecuteSP("dg_ViewTest", model.TESTID)

                Do While Me.DR.Read
                    view.TESTID = DR(0)
                    view.ORDERID = DR(1)
                    view.TESTNAME = DR(2)
                    view.TESTDESCRIPTION = DR(3)
                    view.TESTRESULT = DR(4)
                    view.TESTSTART = DR(5)
                    view.TESTEND = DR(6)
                    view.TEXTLOG = DR(7)
                    view.CREATEBY = DR(8)
                Loop

                Me.TempData.Item("TestDetail") = view
                Me.DA.Dispose()
            Catch exception1 As Exception
                Return Me.RedirectToAction("ver_prueba")
            End Try
            Return Me.View
        End Function


        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader
    End Class

End Namespace
