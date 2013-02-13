Imports System.Data.SqlClient

Namespace AgioNet
    Public Class diagnosticoController
        Inherits System.Web.Mvc.Controller

        Public Class diagnosticoController
            Inherits Controller
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

            <Authorize, HttpPost> _
            Public Function asignarestacion(ByVal model As AsignarEstacionModel) As ActionResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Dim modelArray As MaterialMasterListModel() = New MaterialMasterListModel(2 - 1) {}
                Try
                    Me.DR = Me.DA.ExecuteSP("dg_AssignStation", New Object() {model.TestID, model.Estacion, model.User})
                    If (Me.DA._LastErrorMessage <> "") Then
                        Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                        Return Me.RedirectToAction("asignar_estacion")
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
                Return Me.RedirectToAction("asignar_estacion")
            End Function

            <HttpGet, Authorize> _
            Public Function asignarestacion(ByVal model As TestIDModel) As ActionResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Dim modelArray As EstacionesModel() = New EstacionesModel(&H65 - 1) {}
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
                                .Nombre = Conversions.ToString(Me.DR.Item(0)), _
                                .Descripcion = Conversions.ToString(Me.DR.Item(1)), _
                                .Usuario = Conversions.ToString(Me.DR.Item(2)), _
                                .Proceso = Conversions.ToString(Me.DR.Item(3)) _
                            }
                            modelArray(num) = model2
                            num += 1
                        Loop
                        modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New EstacionesModel(((num - 1) + 1) - 1) {}), EstacionesModel())
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
                        modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New EstacionesModel(((num - 1) + 1) - 1) {}), EstacionesModel())
                    End If
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Me.TempData.Item("ErrMsg") = exception.Message
                    Dim result As ActionResult = Me.RedirectToAction("asignar_estacion")
                    ProjectData.ClearProjectError()
                    Return result
                    ProjectData.ClearProjectError()
                End Try
                Me.DA.Dispose()
                Me.TempData.Item("TestID") = model.TestID
                Me.TempData.Item("Model") = modelArray
                Return Me.View
            End Function

            <Authorize> _
            Public Function cancelar_prueba() As ActionResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Dim modelArray As TestListModel() = New TestListModel(&H65 - 1) {}
                Dim index As Integer = 0
                If Me.HasOrderID Then
                    Try
                        Dim model As TestListModel
                        Dim reader As SqlDataReader = Me.DA.ExecuteSP("dg_GetTestListByOrder", New Object() {RuntimeHelpers.GetObjectValue(Me.Session.Item("OrderID")), ""})
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
                            modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New TestListModel(((index - 1) + 1) - 1) {}), TestListModel())
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
                            modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New TestListModel(((index - 1) + 1) - 1) {}), TestListModel())
                        End If
                    Catch exception1 As Exception
                        ProjectData.SetProjectError(exception1)
                        Dim exception As Exception = exception1
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
                        modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New TestListModel(((index - 1) + 1) - 1) {}), TestListModel())
                        Me.DA.Dispose()
                        Dim result As ActionResult = Me.View
                        ProjectData.ClearProjectError()
                        Return result
                        ProjectData.ClearProjectError()
                    End Try
                    Me.TempData.Item("Model") = modelArray
                    Return Me.View
                End If
                Return Me.RedirectToAction("Index")
            End Function

            <Authorize, HttpPost> _
            Public Function CancelTest(ByVal model As ExecTestModel) As ActionResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Try
                    Me.DR = Me.DA.ExecuteSP("dg_CancelTest", New Object() {model.TestID, model.TextLog})
                    Me.DA.Dispose()
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Me.DA.Dispose()
                    Dim result As ActionResult = Me.View
                    ProjectData.ClearProjectError()
                    Return result
                    ProjectData.ClearProjectError()
                End Try
                Return Me.RedirectToAction("cancelar_prueba")
            End Function

            <Authorize> _
            Public Function CancelTest(ByVal model As TestListModel) As ActionResult
                Me.Session.Add("OtherTitle", "TestID")
                Me.Session.Add("OtherContent", model.TESTID)
                Me.TempData.Item("TESTID") = model.TESTID
                Return Me.View
            End Function

            Public Sub ClearErr()
                If Operators.ConditionalCompareObjectNotEqual(Me.TempData.Item("ErrMsg"), "", False) Then
                    Me.TempData.Item("ErrMsg") = ""
                    Me.TempData.Keep("ErrMsg")
                End If
            End Sub

            <Authorize> _
            Public Function DiagnosticMain() As ActionResult
                If Me.HasOrderID Then
                    Return Me.View
                End If
                Return Me.RedirectToAction("Index")
            End Function

            <Authorize, HttpPost> _
            Public Function DiagnosticoMain(ByVal model As StartDiagnosticModel) As ActionResult
                Dim result As ActionResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Dim str As String = ""
                Try
                    Me.ClearErr()
                    Me.DR = Me.DA.ExecuteSP("dg_OrderValidation", New Object() {model.OrderID})
                    If (Me.DA._LastErrorMessage <> "") Then
                        Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                        Return Me.View
                    End If
                    Do While Me.DR.Read
                        str = Conversions.ToString(Me.DR.Item(0))
                    Loop
                    Me.Session.Item("DiagnosticID") = str
                    Me.Session.Item("OrderID") = model.OrderID
                    result = Me.RedirectToAction("DiagnosticMain")
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Me.TempData.Item("ErrMsg") = exception.Message
                    result = Me.View
                    ProjectData.ClearProjectError()
                    Return result
                    ProjectData.ClearProjectError()
                Finally
                    Me.DA.Dispose()
                End Try
                Return result
            End Function

            <Authorize, HttpPost> _
            Public Function ExecuteTest(ByVal model As ExecTestModel) As ActionResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Try
                    Me.DR = Me.DA.ExecuteSP("dg_ExecTest", New Object() {model.TestID, model.Result, model.TextLog})
                    Me.DA.Dispose()
                    If (model.Result = "FAIL") Then
                        Me.TempData.Item("TESTID") = model.TestID
                        Return Me.RedirectToAction("AddFailure")
                    End If
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Me.DA.Dispose()
                    Dim result As ActionResult = Me.View
                    ProjectData.ClearProjectError()
                    Return result
                    ProjectData.ClearProjectError()
                End Try
                Return Me.RedirectToAction("realizar_pruebas")
            End Function

            <Authorize> _
            Public Function ExecuteTest(ByVal Model As TestListModel) As ActionResult
                Me.Session.Add("OtherTitle", "TestID")
                Me.Session.Add("OtherContent", Model.TESTID)
                Me.TempData.Item("TESTID") = Model.TESTID
                Return Me.View
            End Function

            <Authorize> _
            Public Function falla_reportada() As ActionResult
                Dim result As ActionResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Try
                    Me.DR = Me.DA.ExecuteSP("dg_ViewReportedFailure", New Object() {RuntimeHelpers.GetObjectValue(Me.Session.Item("OrderID"))})
                    If (Me.DA._LastErrorMessage = "") Then
                        Me.TempData.Item("OrderID") = RuntimeHelpers.GetObjectValue(Me.Session.Item("OrderID"))
                        Do While Me.DR.Read
                            Me.TempData.Item("RFail") = RuntimeHelpers.GetObjectValue(Me.DR.Item(0))
                        Loop
                        Me.DA.Dispose()
                        Return Me.View
                    End If
                    Me.DA.Dispose()
                    result = Me.RedirectToAction("DiagnosticMain")
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    result = Me.RedirectToAction("DiagnosticMain")
                    ProjectData.ClearProjectError()
                    Return result
                    ProjectData.ClearProjectError()
                End Try
                Return result
            End Function

            Public Function HasOrderID() As Boolean
                Dim flag2 As Boolean = True
                If Conversions.ToBoolean(Operators.OrObject((Me.Session.Item("OrderID") Is Nothing), Operators.CompareObjectEqual(Me.Session.Item("OrderID"), "", False))) Then
                    flag2 = False
                End If
                If Conversions.ToBoolean(Operators.OrObject((Not Me.Session.Item("OtherTitle") Is Nothing), Operators.CompareObjectNotEqual(Me.Session.Item("OtherTitle"), "", False))) Then
                    Me.Session.Remove("OtherTitle")
                    Me.Session.Remove("OtherContent")
                End If
                Return flag2
            End Function

            <Authorize> _
            Public Function Index() As ActionResult
                Me.Session.Add("SubMenu", "DIAGNOSTIC")
                Me.Session.Add("OrderID", "")
                Return Me.View
            End Function

            <HttpPost, Authorize> _
            Public Function Index(ByVal model As StartDiagnosticModel) As ActionResult
                Dim result As ActionResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Dim str As String = ""
                Try
                    Me.ClearErr()
                    Me.DR = Me.DA.ExecuteSP("dg_OrderValidation", New Object() {model.OrderID})
                    If (Me.DA._LastErrorMessage <> "") Then
                        Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                        Return Me.View
                    End If
                    Do While Me.DR.Read
                        str = Conversions.ToString(Me.DR.Item(0))
                    Loop
                    Me.Session.Item("DiagnosticID") = str
                    Me.Session.Item("OrderID") = model.OrderID
                    result = Me.RedirectToAction("DiagnosticMain")
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Me.TempData.Item("ErrMsg") = exception.Message
                    result = Me.View
                    ProjectData.ClearProjectError()
                    Return result
                    ProjectData.ClearProjectError()
                Finally
                    Me.DA.Dispose()
                End Try
                Return result
            End Function

            Public Function iniciar_diagnostico() As ActionResult
                Return Me.View
            End Function

            <HttpPost, Authorize> _
            Public Function iniciar_diagnostico(ByVal model As StartDiagnosticModel) As ActionResult
                Dim result As ActionResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Try
                    Me.ClearErr()
                    Me.DR = Me.DA.ExecuteSP("dg_StartDiagnostic", New Object() {model.OrderID, Me.User.Identity.Name, model.Comment})
                    If (Me.DA._LastErrorMessage = "") Then
                        Do While Me.DR.Read
                            Me.TempData.Item("ErrMsg") = RuntimeHelpers.GetObjectValue(Me.DR.Item(0))
                        Loop
                        Me.DA.Dispose()
                        Return Me.RedirectToAction("DiagnosticMain")
                    End If
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Me.DA.Dispose()
                    result = Me.View
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    result = Me.View
                    ProjectData.ClearProjectError()
                    Return result
                    ProjectData.ClearProjectError()
                End Try
                Return result
            End Function

            <Authorize> _
            Public Function LoadOrderInfo() As PartialViewResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Dim model As New OrderInfoModel
                Dim num As Integer = 0
                Try
                    Dim model2 As OrderInfoModel
                    Me.DR = Me.DA.ExecuteSP("sc_getOrderInfo", New Object() {RuntimeHelpers.GetObjectValue(Me.Session.Item("OrderID"))})
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

            <Authorize> _
            Public Function loadTestInfo() As PartialViewResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Dim model As New TestInfoModel
                Dim num As Integer = 0
                Try
                    Dim model2 As TestInfoModel
                    Me.DR = Me.DA.ExecuteSP("dg_getTestDetail", New Object() {RuntimeHelpers.GetObjectValue(Me.TempData.Item("TestID"))})
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
                                .TestID = Conversions.ToString(Me.DR.Item(0)), _
                                .OrderID = Conversions.ToString(Me.DR.Item(1)), _
                                .TestName = Conversions.ToString(Me.DR.Item(2)), _
                                .TestDescription = Conversions.ToString(Me.DR.Item(3)), _
                                .TestStart = Conversions.ToString(Me.DR.Item(4)), _
                                .TestEnd = Conversions.ToString(Me.DR.Item(5)), _
                                .TestResult = Conversions.ToString(Me.DR.Item(6)), _
                                .CreateBy = Conversions.ToString(Me.DR.Item(7)) _
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
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Me.TempData.Item("ErrMsg") = exception.Message
                    Me.DA.Dispose()
                    ProjectData.ClearProjectError()
                End Try
                Me.TempData.Item("Model") = model
                Me.TempData.Keep("OrderID")
                Return Me.PartialView("_TestInfoPartial")
            End Function

            Public Function LoadWnOrderInfo() As PartialViewResult
                Return Me.PartialView("_wnOrderInfoPartial")
            End Function

            <Authorize> _
            Public Function pedir_aprobar() As ActionResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Dim modelArray As TestListModel() = New TestListModel(&H65 - 1) {}
                Dim index As Integer = 0
                If Me.HasOrderID Then
                    Try
                        Dim model As TestListModel
                        Dim reader As SqlDataReader = Me.DA.ExecuteSP("dg_GetTestListByOrder", New Object() {RuntimeHelpers.GetObjectValue(Me.Session.Item("OrderID")), ""})
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
                            modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New TestListModel(((index - 1) + 1) - 1) {}), TestListModel())
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
                            modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New TestListModel(((index - 1) + 1) - 1) {}), TestListModel())
                        End If
                    Catch exception1 As Exception
                        ProjectData.SetProjectError(exception1)
                        Dim exception As Exception = exception1
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
                        modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New TestListModel(((index - 1) + 1) - 1) {}), TestListModel())
                        Me.DA.Dispose()
                        Dim result As ActionResult = Me.View
                        ProjectData.ClearProjectError()
                        Return result
                        ProjectData.ClearProjectError()
                    End Try
                    Me.TempData.Item("Model") = modelArray
                    Return Me.View
                End If
                Return Me.RedirectToAction("Index")
            End Function

            <Authorize, HttpPost> _
            Public Function pedir_aprobar(ByVal model As ReqApprovalModel) As ActionResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Dim email As New agEmail
                Dim emailAddress As String = String.Empty
                Dim txtSubject As String = String.Empty
                Dim txtMessage As String = String.Empty
                Try
                    Me.DR = Me.DA.ExecuteSP("dg_ReqApproval", New Object() {RuntimeHelpers.GetObjectValue(Me.Session.Item("OrderID")), model.Comment, Me.User.Identity.Name})
                    If (Me.DA._LastErrorMessage = "") Then
                        Me.TempData.Item("ErrMsg") = Operators.ConcatenateObject("Se ha mandado la solicitud de aprobación para la orden :", Me.Session.Item("OrderID"))
                        If Not Me.DR.IsClosed Then
                            Me.DR.Close()
                        End If
                        Me.DR = Me.DA.ExecuteSP("sys_getDistibutionList", New Object() {"REQAPPROV"})
                        If Me.DR.HasRows Then
                            Do While Me.DR.Read
                                emailAddress = Conversions.ToString(Me.DR.Item(0))
                            Loop
                            email.HTMLBody = True
                            txtSubject = Conversions.ToString(Operators.ConcatenateObject("Solicitud de aprobación para la orden : ", Me.Session.Item("OrderID")))
                            txtMessage = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("<p style='font-family: Arial; font-size:12px; width:800px;'>Se ha registrado una solicitud de aprobacion de reparación para la orden: <b>", Me.Session.Item("OrderID")), "</b>. Esta opción se encuentra disponible desde "), "Atención a Clientes / Aprovación de Reparación<p>"))
                            email.SendEmail(emailAddress, txtSubject, txtMessage)
                        End If
                        Me.DA.Dispose()
                    Else
                        Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                        Me.DA.Dispose()
                    End If
                    Return Me.RedirectToAction("DiagnosticMain")
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Me.TempData.Item("ErrMsg") = exception.Message
                    Dim result As ActionResult = Me.RedirectToAction("DiagnosticMain")
                    ProjectData.ClearProjectError()
                    Return result
                    ProjectData.ClearProjectError()
                End Try
                Return Me.View
            End Function

            <Authorize> _
            Public Function programar_prueba() As ActionResult
                If Me.HasOrderID Then
                    Try
                        Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                        Me.DR = Me.DA.ExecuteSP("dg_GetTestList", New Object(0 - 1) {})
                        Dim modelArray As TestIDListModel() = New TestIDListModel(&H65 - 1) {}
                        Dim index As Integer = 0
                        Do While Me.DR.Read
                            modelArray(index) = New TestIDListModel With { _
                                .TestID = Conversions.ToString(Me.DR.Item(0)), _
                                .TestName = Conversions.ToString(Me.DR.Item(1)) _
                            }
                            index += 1
                        Loop
                        modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New TestIDListModel(((index - 1) + 1) - 1) {}), TestIDListModel())
                        Me.TempData.Item("Model") = modelArray
                        Me.DA.Dispose()
                    Catch exception1 As Exception
                        ProjectData.SetProjectError(exception1)
                        Dim exception As Exception = exception1
                        Me.TempData.Item("ErrMsg") = exception.Message
                        Me.DA.Dispose()
                        Dim result As ActionResult = Me.RedirectToAction("Index")
                        ProjectData.ClearProjectError()
                        Return result
                        ProjectData.ClearProjectError()
                    End Try
                    Return Me.View
                End If
                Return Me.RedirectToAction("Index")
            End Function

            <HttpPost, Authorize> _
            Public Function programar_prueba(ByVal md As SheduleTestModel) As ActionResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Dim str As String = Conversions.ToString(Me.Session.Item("OrderID"))
                If (md.TestID <> "") Then
                    Me.DA.ExecuteSP("dg_AddTest", New Object() {str, md.TestID, md.TestComment, Me.User.Identity.Name, md.ProductClass, md.ProductType, md.Trademark, md.Model, md.Description, md.PartNo, md.SerialNo, md.Revision, md.Failure})
                End If
                If (Me.DA._LastErrorMessage = "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                Else
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                End If
                Me.Session.Item("OrderID") = str
                Return Me.RedirectToAction("DiagnosticMain")
            End Function

            <Authorize> _
            Public Function realizar_pruebas() As ActionResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Dim modelArray As TestListModel() = New TestListModel(&H65 - 1) {}
                Dim index As Integer = 0
                If Me.HasOrderID Then
                    Dim model As TestListModel
                    Try
                        Dim reader As SqlDataReader = Me.DA.ExecuteSP("dg_GetTestListByOrder", New Object() {RuntimeHelpers.GetObjectValue(Me.Session.Item("OrderID")), ""})
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
                            modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New TestListModel(((index - 1) + 1) - 1) {}), TestListModel())
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
                            modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New TestListModel(((index - 1) + 1) - 1) {}), TestListModel())
                        End If
                    Catch exception1 As Exception
                        ProjectData.SetProjectError(exception1)
                        Dim exception As Exception = exception1
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
                        modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New TestListModel(((index - 1) + 1) - 1) {}), TestListModel())
                        Me.DA.Dispose()
                        Dim result As ActionResult = Me.View
                        ProjectData.ClearProjectError()
                        Return result
                        ProjectData.ClearProjectError()
                    End Try
                    Me.TempData.Item("Model") = modelArray
                    Return Me.View
                End If
                Return Me.RedirectToAction("Index")
            End Function

            <Authorize> _
            Public Function ver_falla() As ActionResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Dim modelArray As TestListModel() = New TestListModel(&H65 - 1) {}
                Dim index As Integer = 0
                If Me.HasOrderID Then
                    Try
                        Dim model As TestListModel
                        Dim reader As SqlDataReader = Me.DA.ExecuteSP("dg_GetTestListByOrder", New Object() {RuntimeHelpers.GetObjectValue(Me.Session.Item("OrderID")), ""})
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
                            modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New TestListModel(((index - 1) + 1) - 1) {}), TestListModel())
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
                            modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New TestListModel(((index - 1) + 1) - 1) {}), TestListModel())
                        End If
                    Catch exception1 As Exception
                        ProjectData.SetProjectError(exception1)
                        Dim exception As Exception = exception1
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
                        modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New TestListModel(((index - 1) + 1) - 1) {}), TestListModel())
                        Me.DA.Dispose()
                        Dim result As ActionResult = Me.View
                        ProjectData.ClearProjectError()
                        Return result
                        ProjectData.ClearProjectError()
                    End Try
                    Me.TempData.Item("Model") = modelArray
                    Return Me.View
                End If
                Return Me.RedirectToAction("Index")
            End Function

            <Authorize> _
            Public Function ver_prueba() As ActionResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Dim modelArray As TestListModel() = New TestListModel(&H65 - 1) {}
                Dim index As Integer = 0
                If Me.HasOrderID Then
                    Try
                        Dim model As TestListModel
                        Dim reader As SqlDataReader = Me.DA.ExecuteSP("dg_GetTestListByOrder", New Object() {RuntimeHelpers.GetObjectValue(Me.Session.Item("OrderID")), ""})
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
                            modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New TestListModel(((index - 1) + 1) - 1) {}), TestListModel())
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
                            modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New TestListModel(((index - 1) + 1) - 1) {}), TestListModel())
                        End If
                    Catch exception1 As Exception
                        ProjectData.SetProjectError(exception1)
                        Dim exception As Exception = exception1
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
                        modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New TestListModel(((index - 1) + 1) - 1) {}), TestListModel())
                        Me.DA.Dispose()
                        Dim result As ActionResult = Me.View
                        ProjectData.ClearProjectError()
                        Return result
                        ProjectData.ClearProjectError()
                    End Try
                    Me.TempData.Item("Model") = modelArray
                    Return Me.View
                End If
                Return Me.RedirectToAction("Index")
            End Function

            <HttpPost, Authorize> _
            Public Function ViewFailure() As ActionResult
                Return Me.RedirectToAction("ver_falla")
            End Function

            <Authorize> _
            Public Function ViewFailure(ByVal model As ViewFoundFailureModel) As ActionResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Try
                    Dim view As FailureView
                    Me.DR = Me.DA.ExecuteSP("dg_ViewFoundedFailures", New Object() {model.TestID})
                    Do While Me.DR.Read
                        view.FAILUREID = Conversions.ToString(Me.DR.Item(0))
                        view.TESTID = Conversions.ToString(Me.DR.Item(0))
                        view.DESCRIPTION = Conversions.ToString(Me.DR.Item(0))
                        view.POSSIBLESOLUTION = Conversions.ToString(Me.DR.Item(0))
                        view.FOUNDBY = Conversions.ToString(Me.DR.Item(0))
                        view.FOUNDDATE = Conversions.ToString(Me.DR.Item(0))
                        view.RESOLVED = Conversions.ToString(Me.DR.Item(0))
                    Loop
                    Me.TempData.Item("TestDetail") = view
                    Me.DA.Dispose()
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Dim result As ActionResult = Me.RedirectToAction("ver_falla")
                    ProjectData.ClearProjectError()
                    Return result
                    ProjectData.ClearProjectError()
                End Try
                Return Me.View
            End Function

            <Authorize, HttpPost> _
            Public Function ViewTest() As ActionResult
                Return Me.RedirectToAction("ver_prueba")
            End Function

            <Authorize> _
            Public Function ViewTest(ByVal model As TestListModel) As ActionResult
                Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
                Try
                    Dim view As TestView
                    Me.DR = Me.DA.ExecuteSP("dg_ViewTest", New Object() {model.TESTID})
                    Do While Me.DR.Read
                        view.TESTID = Conversions.ToString(Me.DR.Item(0))
                        view.ORDERID = Conversions.ToString(Me.DR.Item(1))
                        view.TESTNAME = Conversions.ToString(Me.DR.Item(2))
                        view.TESTDESCRIPTION = Conversions.ToString(Me.DR.Item(3))
                        view.TESTRESULT = Conversions.ToString(Me.DR.Item(4))
                        view.TESTSTART = Conversions.ToString(Me.DR.Item(5))
                        view.TESTEND = Conversions.ToString(Me.DR.Item(6))
                        view.TEXTLOG = Conversions.ToString(Me.DR.Item(7))
                        view.CREATEBY = Conversions.ToString(Me.DR.Item(8))
                    Loop
                    Me.TempData.Item("TestDetail") = view
                    Me.DA.Dispose()
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Dim result As ActionResult = Me.RedirectToAction("ver_prueba")
                    ProjectData.ClearProjectError()
                    Return result
                    ProjectData.ClearProjectError()
                End Try
                Return Me.View
            End Function


            ' Fields
            Protected Friend DA As DataAccess
            Protected Friend DR As SqlDataReader
        End Class


Collapse Methods



    End Class
End Namespace
