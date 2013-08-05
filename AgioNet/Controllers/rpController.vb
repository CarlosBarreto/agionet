Imports System.Data.SqlClient

Namespace AgioNet
    Public Class rpController
        Inherits System.Web.Mvc.Controller

        ' 2013.06.17
        ' GET: /rp/min_info
        <Authorize> _
        Public Function min_info(ByVal OrderID As String) As PartialViewResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim MinInfo As New mailing.MinOrderInfo
            Try
                DR = DA.ExecuteSP("dbo.sys_getMinOrderInfo", OrderID)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If DR.HasRows Then
                    While DR.Read
                        MinInfo.OrderID = DR(0)
                        MinInfo.Model = DR(1)
                        MinInfo.PartNo = DR(2)
                        MinInfo.Description = DR(3)
                        MinInfo.SerialNo = DR(4)
                        MinInfo.Failure = DR(5)
                        MinInfo.Status = DR(6)
                    End While
                End If
            Catch ex As Exception
                TempData("ErrMsg") = DA._LastErrorMessage
            Finally
                DA.Dispose()
            End Try

            TempData("MinInfo") = MinInfo
            Return Me.PartialView("min_info")
        End Function

        ' 2013.06.17
        ' GET : /rp/rp_recibo
        <Authorize> _
        Public Function rp_recibo(ByVal OrderID As String) As PartialViewResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model As New rp_CheckinModel
            Try
                DR = DA.ExecuteSP("dbo.srp_getCheckinInfo", OrderID)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If DR.HasRows Then
                    While DR.Read
                        model = New rp_CheckinModel With {.OrderID = DR(0), .Comment = DR(1), .Guia = DR(2), .CheckinDate = DR(3), _
                                .lenght = DR(4), .Width = DR(5), .Height = DR(6), .Weight = DR(7), .Dimweight = DR(8), _
                                .Shipweight = DR(9), .User = DR(10)}
                    End While
                Else
                    model = New rp_CheckinModel With {.OrderID = "no data", .Comment = "no data", .Guia = "no data", .CheckinDate = "no data", _
                                .lenght = "no data", .Width = "no data", .Height = "no data", .Weight = "no data", .Dimweight = "no data", _
                                .Shipweight = "no data", .User = "no data"}
                End If
            Catch ex As Exception
                TempData("ErrMsg") = DA._LastErrorMessage
            Finally
                DA.Dispose()
            End Try

            TempData("Model") = model
            Return Me.PartialView("rp_recibo")
        End Function

        '2013.06.20
        'GET : /rp/rp_ingreso
        <Authorize> _
        Public Function rp_ingreso(ByVal OrderID As String) As PartialViewResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model As New rp_ReciboModel
            Try
                DR = DA.ExecuteSP("dbo.srp_getReceiveInfo", OrderID)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If DR.HasRows Then
                    While DR.Read
                        model = New rp_ReciboModel With {.OrderID = DR(0), .SerialNo = DR(1), .SKU = DR(2), .PartNo = DR(3), _
                                .Model = DR(4), .TrackNo = DR(5), .ScanDate = DR(6), .ScanBy = DR(7), .PackType = DR(8), _
                                .PackDamage = DR(9), .NonDocPack = DR(10), .CorrectPack = DR(11), .Accesories = DR(12), .Cosmetic = DR(13), _
                                .Warranty = DR(14), .rerepair = DR(15), .Comment = DR(16)}
                    End While
                Else
                    model = New rp_ReciboModel With {.OrderID = "no data", .SerialNo = "no data", .SKU = "no data", .PartNo = "no data", _
                                .Model = "no data", .TrackNo = "no data", .ScanDate = "no data", .ScanBy = "no data", .PackType = "no data", _
                                .PackDamage = "no data", .NonDocPack = "no data", .CorrectPack = "no data", .Accesories = "no data", .Cosmetic = "no data", _
                                .Warranty = "no data", .rerepair = "no data", .Comment = "no data"}
                End If
            Catch ex As Exception
                TempData("ErrMsg") = DA._LastErrorMessage
            Finally
                DA.Dispose()
            End Try

            TempData("Model") = model
            Return Me.PartialView("rp_ingreso")
        End Function

        ' 2013.06.20
        ' GET: /rp/rp_diagnostic
        <Authorize> _
        Public Function rp_diagnostic(ByVal OrderID As String) As PartialViewResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim md_diagnostic As New rp_DiagnosticModel
            Dim md_test(100) As rp_TestListModel
            Dim md_failure(100) As rp_FailureListModel
            Dim md_dgClose As New rp_dgCloseModel

            Dim index As Integer = 0

            ' -- Información del diagnostico
            Try
                DR = DA.ExecuteSP("dbo.srp_getDiagnosticInfo", OrderID)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If DR.HasRows Then
                    While DR.Read
                        md_diagnostic = New rp_DiagnosticModel With {.OrderID = DR(0), .StartDate = DR(1), .EndDate = DR(2), .CreateBy = DR(3), .Comments = DR(4)}
                    End While
                Else
                    md_diagnostic = New rp_DiagnosticModel With {.OrderID = "No data", .StartDate = "No data", .EndDate = "No data", .CreateBy = "No data", .Comments = "No data"}
                End If

                '-- Información de las fallas
                If Not DR.IsClosed Then
                    DR.Close()
                End If

                DR = DA.ExecuteSP("dbo.srp_getTestListInfo", OrderID)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If DR.HasRows Then
                    While DR.Read
                        md_test(index) = New rp_TestListModel With {.OrderID = DR(0), .TestID = DR(1), .TestName = DR(2), _
                                        .TestDescription = DR(3), .TestResult = DR(4), .TestStart = DR(5), .TestEnd = DR(6), _
                                        .CreateBy = DR(7), .LogDate = DR(8), .LogText = DR(9)}
                        index += 1
                    End While
                    ReDim Preserve md_test(index - 1)
                Else
                    index = 0
                    md_test(index) = New rp_TestListModel With {.OrderID = "No data", .TestID = "No data", .TestName = "No data", _
                                        .TestDescription = "No data", .TestResult = "No data", .TestStart = "No data", .TestEnd = "No data", _
                                        .CreateBy = "No data", .LogDate = "No data", .LogText = "No data"}
                    index += 1
                    ReDim Preserve md_test(index - 1)
                End If

                ' -- Información de las fallas
                If Not DR.IsClosed Then
                    DR.Close()
                End If

                DR = DA.ExecuteSP("dbo.srp_getFailureList", OrderID)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                index = 0
                If DR.HasRows Then
                    While DR.Read
                        md_failure(index) = New rp_FailureListModel With {.OrderID = DR(0), .FailureID = DR(1), .TipoReparacion = DR(2), _
                                        .Failure = DR(3), .Solution = DR(4), .Source = DR(5), .Comment = DR(6), _
                                        .FoundBy = DR(7), .FoundDate = DR(8)}
                        index += 1
                    End While
                    ReDim Preserve md_failure(index - 1)
                Else
                    index = 0
                    md_failure(index) = New rp_FailureListModel With {.OrderID = "No data", .FailureID = "No data", .TipoReparacion = "No data", _
                                        .Failure = "No data", .Solution = "No data", .Source = "No data", .Comment = "No data", _
                                        .FoundBy = "No data", .FoundDate = "No data"}
                    index += 1
                    ReDim Preserve md_failure(index - 1)
                End If

                ' -- Cierre de diagnostico
                If Not DR.IsClosed Then
                    DR.Close()
                End If

                DR = DA.ExecuteSP("dbo.srp_getdgClose", OrderID)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                If DR.HasRows Then
                    While DR.Read
                        md_dgClose = New rp_dgCloseModel With {.OrderID = DR(0), .Retro = DR(1), .TComment = DR(2), _
                                        .Comments = DR(3), .RequestBy = DR(4), .RequestDate = DR(5)}
                    End While
                Else
                    index = 0
                    md_dgClose = New rp_dgCloseModel With {.OrderID = "No data", .Retro = "No data", .TComment = "No data", _
                                        .Comments = "No data", .RequestBy = "No data", .RequestDate = "No data"}
                End If
            Catch ex As Exception
                TempData("ErrMsg") = DA._LastErrorMessage
            Finally
                DA.Dispose()
            End Try

            TempData("diagnostic") = md_diagnostic
            TempData("testList") = md_test
            TempData("failureList") = md_failure
            TempData("dgClose") = md_dgClose

            Return Me.PartialView("rp_diagnostic")
        End Function

        '2013.06.21
        ' GET: /rp/rp_costpart
        <Authorize> _
        Public Function rp_costpart(ByVal OrderID As String) As PartialViewResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model(100) As rp_CostPartModel
            Dim model2(100) As rp_CotizacionPartesModel
            Dim model3 As New rp_CotizacionClienteModel
            Dim index As Integer = 0
            Try
                DR = DA.ExecuteSP("dbo.srp_getCostInfo", OrderID, User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If DR.HasRows Then
                    While DR.Read
                        model(index) = New rp_CostPartModel With {.OrderID = DR(0), .Costo = DR(1), .Flete = DR(2), .GastosImportacion = DR(3), _
                                .SubTotal = DR(4), .LeadTime = DR(5), .Proveedor = DR(6), .Comentario = DR(7), .CostDate = DR(8), _
                                .CostBy = DR(9)}
                        index += 1
                    End While
                    ReDim Preserve model(index - 1)
                Else
                    index = 0
                    model(index) = New rp_CostPartModel With {.OrderID = "no data", .Costo = "no data", .Flete = "no data", _
                                .GastosImportacion = "no data", .SubTotal = "no data", .LeadTime = "no data", .Proveedor = "no data", _
                                .Comentario = "no data", .CostDate = "no data", .CostBy = "no data"}
                    ReDim Preserve model(index)
                End If

                '--------------
                If Not DR.IsClosed Then
                    DR.Close()
                End If

                index = 0
                DR = DA.ExecuteSP("dbo.srp_getCotizacionPartesInfo", OrderID, User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If DR.HasRows Then
                    While DR.Read
                        model2(index) = New rp_CotizacionPartesModel With {.OrderID = DR(0), .PartNo = DR(1), .Description = DR(2), _
                                        .Costo = DR(3), .Utilidad = DR(4), .UtilidadNeta = DR(5), .Precio = DR(6)}
                        index += 1
                    End While
                    ReDim Preserve model2(index - 1)
                Else
                    index = 0
                    model2(index) = New rp_CotizacionPartesModel With {.OrderID = "no data", .PartNo = "no data", .Description = "no data", _
                                                .Costo = "no data", .Utilidad = "no data", .UtilidadNeta = "no data", .Precio = "no data"}
                    ReDim Preserve model2(index)
                End If

                '--------------
                If Not DR.IsClosed Then
                    DR.Close()
                End If

                index = 0
                DR = DA.ExecuteSP("dbo.srp_getCotizacionClienteInfo", OrderID, User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If DR.HasRows Then
                    While DR.Read
                        model3 = New rp_CotizacionClienteModel With {.OrderID = DR(0), .Sumatoria = DR(1), .ManoObra = DR(2), .Viaje = DR(3), _
                            .SubTotal = DR(4), .IVA = DR(5), .Total = DR(6), .LeadTime = DR(7), .CostBy = DR(8), .CostDate = DR(9)}
                        'index += 1
                    End While
                    'ReDim Preserve model3(index - 1)
                Else
                    'index = 0
                    model3 = New rp_CotizacionClienteModel With {.OrderID = "no data", .Sumatoria = "no data", .ManoObra = "no data", .Viaje = "no data", _
                                                .SubTotal = "no data", .IVA = "no data", .Total = "no data", .LeadTime = "no data", .CostBy = "no data", .CostDate = "no data"}
                    'ReDim Preserve model3(index)
                End If

            Catch ex As Exception
                TempData("ErrMsg") = DA._LastErrorMessage
            Finally
                DA.Dispose()
            End Try

            TempData("Model") = model
            TempData("CotizacionPartes") = model2
            TempData("CotizacionCliente") = model3
            Return Me.PartialView("rp_costpart")
        End Function

        '2013.07.11
        'GET: /rp/rp_compras
        <Authorize> _
        Public Function rp_compras(ByVal OrderID As String) As PartialViewResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model(100) As rp_CostPartModel
            Dim index As Integer = 0
            Try
                DR = DA.ExecuteSP("dbo.srp_getComprasInfo", OrderID, User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If DR.HasRows Then
                    While DR.Read
                        model(index) = New rp_CostPartModel With {.OrderID = DR(0), .PartNo = DR(1), .Description = DR(2), .Costo = DR(3), _
                                .Flete = DR(4), .GastosImportacion = DR(5), .SubTotal = DR(6), .LeadTime = DR(7), .Proveedor = DR(8), _
                                .Comentario = DR(9), .CostDate = DR(10), .CostBy = DR(11)}
                        index += 1
                    End While
                    ReDim Preserve model(index - 1)
                Else
                    index = 0
                    model(index) = New rp_CostPartModel With {.OrderID = "no data", .PartNo = "no data", .Description = "no data",
                                .Costo = "no data", .Flete = "no data", .GastosImportacion = "no data", .SubTotal = "no data",
                                .LeadTime = "no data", .Proveedor = "no data", .Comentario = "no data", .CostDate = "no data", .CostBy = "no data"}
                    ReDim Preserve model(index)
                End If
            Catch ex As Exception
                TempData("ErrMsg") = DA._LastErrorMessage
            Finally
                DA.Dispose()
            End Try

            TempData("Model") = model
            Return Me.PartialView("rp_compras")
        End Function
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader
    End Class
End Namespace
