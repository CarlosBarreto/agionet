Imports System.Data.SqlClient

Namespace AgioNet
    Public Class reparacionController
        Inherits System.Web.Mvc.Controller

        ' 2013.04.24 - Upd By CarlosB
        ' GET: /reparacion
        <Authorize> _
        Function Index() As ActionResult
            Me.Session.Clear()
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As PendingOrdersModel
            Dim i As Integer = 0
            Try
                DR = DA.ExecuteSP("rp_PendientesReparación", "REPAIR")
                 If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(i) = New PendingOrdersModel With {.OrderID = DR(0), .Customer = DR(1), .ProductType = DR(2), _
                                            .SerialNo = DR(3), .Model = DR(4), .Description = DR(5), .DateR = DR(6), .DateComp = DR(7)}
                        i += 1
                    Loop
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(i - 1)
                Else
                    i = 0
                    modelArray(i) = New PendingOrdersModel With {.OrderID = "No Data", .Customer = "No Data", .ProductType = "No Data", _
                        .SerialNo = "No Data", .Model = "No Data", .Description = "No Data", .DateR = "No Data", .DateComp = "No data"}
                    i += 1
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(i - 1)
                End If

                TempData("Model") = modelArray
            Catch ex As Exception
                TempData("ErrMsg") = ex.Message
                i = 0
                modelArray(i) = New PendingOrdersModel With {.OrderID = "No Data", .Customer = "No Data", .ProductType = "No Data", _
                        .SerialNo = "No Data", .Model = "No Data", .Description = "No Data", .DateR = "No Data", .DateComp = "No data"}
                i += 1
                ReDim Preserve modelArray(i - 1)

                Return RedirectToAction("Index")
            Finally
                DA.Dispose()
            End Try

            Return Me.View

            Return View()
        End Function

        ' 2013.02.13
        ' POST: /reparacion/Index
        <HttpPost, Authorize> _
        Public Function Index(ByVal model As ScanOrderModel) As ActionResult
            Dim response As ActionResult

            If model.OrderID = "" Then
                response = Me.View
            Else
                TempData("OrderID") = model.OrderID
                response = Me.RedirectToAction("ordenes_pendientes")
            End If
            Return response
        End Function

        ' 2013.03.01
        ' GET: /reparacion/ordenes_pendientes
        <Authorize> _
        Public Function ordenes_pendientes() As ActionResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As PendientesreparacionModel
            Dim index As Integer = 0
            Dim mailInfo As New mailing.DiagnosticMailInfo
            Dim OrderID As String

            If TempData("OrderID") = "" Then
                OrderID = Trim(Mid(Session("OrderID"), 1, Session("OrderID").ToString.Length - 3) & "-01")
            Else
                OrderID = TempData("OrderID")
            End If

            Try
                DR = DA.ExecuteSP("rp_PendientesReparación", "ORDER", OrderID)
                If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                    Do While Me.DR.Read
                        modelArray(index) = New PendientesReparacionModel With {.OrderID = DR(0), .Comments = DR(1), .ApprovalDate = DR(2), _
                                            .PartNumber = DR(3), .ProductDescription = DR(4), .SerialNumber = DR(5), .Failure = DR(6), _
                                            .Solution = DR(7), .Source = DR(8), .Comment = DR(9), .RepairStatus = DR(10), .Comentario = DR(11)}
                        If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ReDim Preserve modelArray(index - 1)
                Else
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Not DR.IsClosed Then
                    DR.Close()
                End If

                DR = DA.ExecuteSP("dg_getDiagnosticMailInfo", OrderID)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                    While DR.Read
                        With mailInfo
                            .Marca = DR(1)
                            .Modelo = DR(2)
                            .SKUNO = DR(3)
                            .Descripcion = DR(4)
                            .Serie = DR(5)
                            .FallaReportada = DR(6)
                            .RetroAlimentacion = DR(7)
                            .Comentario = DR(8)
                            .ComentarioTecnico = DR(9)
                            .FechaCompromiso = DR(10)
                        End With
                    End While
                Else
                    Throw New Exception(DA._LastErrorMessage)
                End If
                '-----
            Catch ex As Exception
                If ex.Message <> "" Then
                    TempData("ErrMsg") = ex.Message
                    Return RedirectToAction("Index")
                Else
                    index = 0
                    modelArray(index) = New PendientesReparacionModel With {.OrderID = "no data", .Comments = "no data", _
                                        .ApprovalDate = "no data", .PartNumber = "no data", .ProductDescription = "no data", _
                                        .SerialNumber = "no data", .Failure = "no data", .Solution = "no data", _
                                        .Source = "no data", .Comment = "no data", .RepairStatus = "no data", .Comentario = "no data"}
                    index += 1
                    ReDim Preserve modelArray(index - 1)

                    mailInfo = New mailing.DiagnosticMailInfo With {.Marca = "no data", .Modelo = "no data", .SKUNO = "no data", _
                                                    .Descripcion = "no data", .Serie = "no data", .FallaReportada = "no data", _
                                                    .RetroAlimentacion = "no data", .ComentarioTecnico = "no data"}

                End If
            Finally
                DA.Dispose()
            End Try

            TempData("Model") = modelArray
            TempData("mailInfo") = mailInfo
            TempData("OrderID") = OrderID
            Return Me.View
        End Function

        '2013.04.25 
        ' GET: /reparacion/ordenespendientes
        Public Function ordenespendientes(ByVal model As ScanOrderModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = ""
            Try
                Me.DR = Me.DA.ExecuteSP("rp_OrderValidation", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                Else
                    Do While DR.Read
                        str = DR(0)
                    Loop

                    If str.ToString <> "OK" Then
                        Throw New Exception(str.ToString)
                    End If

                    If Not DR.IsClosed Then
                        DR.Close()
                    End If

                    Session.Add("OrderID", model.OrderID)
                    result = Me.RedirectToAction("reparar_orden")

                    Dim modelArray As New PendientesreparacionModel
                    DR = DA.ExecuteSP("rp_getFailureInfo", Session("OrderID"))
                    If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                        Do While Me.DR.Read
                            modelArray = New PendientesreparacionModel With {.OrderID = DR(0), .Comments = DR(1), .ApprovalDate = DR(2), _
                                                .PartNumber = DR(3), .ProductDescription = DR(4), .SerialNumber = DR(5), .Failure = DR(6), _
                                                .Solution = DR(7), .Source = DR(8), .Comment = DR(9)}
                        Loop
                    Else
                        Throw New Exception(DA._LastErrorMessage)
                    End If

                    '-----
                    TempData("Model") = modelArray
                End If


            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                result = Me.View
            Finally
                Me.DA.Dispose()
            End Try

            TempData("VFlag") = "TRUE"
            Return result
        End Function

        ' 2013.03.01
        ' GET: /diagnostico/LoadFailureInfo
        <Authorize> _
        Public Function LoadFailureInfo() As PartialViewResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As New PendientesreparacionModel
            Try
                DR = DA.ExecuteSP("rp_getFailureInfo", Session("OrderID"))
                If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                    Do While Me.DR.Read
                        modelArray = New PendientesreparacionModel With {.OrderID = DR(0), .Comments = DR(1), .ApprovalDate = DR(2), _
                                            .PartNumber = DR(3), .ProductDescription = DR(4), .SerialNumber = DR(5), .Failure = DR(6), _
                                            .Solution = DR(7), .Source = DR(8), .Comment = DR(9)}
                    Loop
                Else
                    Throw New Exception(DA._LastErrorMessage)
                End If

                '-----
                TempData("Model") = modelArray
            Catch ex As Exception
                TempData("ErrMsg") = ex.Message
            Finally
                DA.Dispose()
            End Try

            Return PartialView("_FailureInfoPartial")
        End Function

        ' 2013.03.04
        ' GET: /REPLACE/LoadReplaceHistory 
        Function LoadReplaceHistory() As PartialViewResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim myModel(100) As ReplaceHistoryModel
            Dim Cont As Integer = 0
            Try
                DR = DA.ExecuteSP("rp_getReplaceHistory", Session("OrderID"))
                If DA._LastErrorMessage <> "" Then Throw New Exception(DA._LastErrorMessage)

                If DR.HasRows Then
                    While DR.Read
                        myModel(Cont) = New ReplaceHistoryModel With {.OrderID = DR(0), .OldPartNo = DR(1), .NewPartNo = DR(2), .Failure = DR(3)}
                        Cont = Cont + 1
                    End While
                    ReDim Preserve myModel(Cont - 1)
                Else
                    myModel(0) = New ReplaceHistoryModel With {.OrderID = "No data", .OldPartNo = "No Data", .NewPartNo = "No Data", .Failure = "No Data"}
                    Cont = Cont + 1
                    ReDim Preserve myModel(Cont - 1)
                End If
            Catch ex As Exception
                myModel(Cont) = New ReplaceHistoryModel With {.OrderID = "No data", .OldPartNo = "No Data", .NewPartNo = "No Data", .Failure = "No Data"}
                Cont = Cont + 1
                ReDim Preserve myModel(Cont - 1)
            Finally
                DA.Dispose()
            End Try


            TempData("ModelRep") = myModel
            Return PartialView("ReplaceHistory")
        End Function



        ' 2013.03.01
        ' GET: /reparacion/reparar_orden
        <Authorize> _
        Public Function reparar_orden() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = ""

            If TempData("VFlag") = "TRUE" Then
                TempData.Keep("Model")
                Try
                    Me.DR = Me.DA.ExecuteSP("rp_getTipoReparacion", Session("OrderID"))
                    If (Me.DA._LastErrorMessage <> "") Then
                        Throw New Exception(DA._LastErrorMessage)
                    End If

                    While DR.Read
                        TempData("TipoReparacion") = DR(0)
                    End While
                Catch ex As Exception
                    TempData("ErrMsg") = ex.Message
                    Return RedirectToAction("Index")
                Finally
                    DA.Dispose()
                End Try

                Return Me.View
            Else
                Session.Clear()
                Return RedirectToAction("Index")
            End If
        End Function

        ' 2013.03.04
        ' GET: /repacion/remplazo_parte
        <Authorize> _
        Public Function remplazo_parte() As PartialViewResult
            Return PartialView("_ReplacePartForm")
        End Function

        ' 2013.03.04 
        ' POST: /reparacion/remplazo_parte
        <Authorize, HttpPost> _
        Public Function remplazo_parte(ByVal model As ReplacePartModel) As PartialViewResult
            Dim result As New PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = ""

            Try
                Me.DR = Me.DA.ExecuteSP("rp_SaveReplacePart", model.OrderID, model.NewPartNo, model.NewSerialNo, model.PartNo, model.SerialNo, _
                                        model.Failure, model.Comment, User.Identity.Name)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                Else
                    result = PartialView("_ReplacePartForm")
                    'result = RedirectToAction("ReplaceHistory")
                    'result = RedirectToAction("Index")
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                'result = Me.View
            Finally
                Me.DA.Dispose()
            End Try

            Return result
        End Function

        '2013.03.05 
        ' GET: /reparacion/nueva_parte
        <Authorize> _
        Public Function nueva_parte() As PartialViewResult
            Return PartialView("_NewPartForm")
        End Function

        '2013.03.05
        ' POST: /reparacion/nueva_parte
        <Authorize, HttpPost> _
        Public Function nueva_parte(ByVal model As AddNewPartModel) As PartialViewResult
            Dim result As New PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = ""

            Try
                Me.DR = Me.DA.ExecuteSP("rp_SaveReplacePart", model.OrderID, model.PartNo, model.SerialNo, "NEW PART", "NEW PART", model.Failure, model.Comment, User.Identity.Name)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                Else
                    result = PartialView("_NewPartForm")
                    'result = RedirectToAction("ReplaceHistory")
                    'result = RedirectToAction("Index")
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                result = PartialView("_NewPartForm")
            Finally
                Me.DA.Dispose()
            End Try

            Return result
        End Function

        ' -- Remplazo/reparacion de componente
        '2013.03.06 
        ' GET: /reparacion/reparacion
        <Authorize> _
        Public Function reparacion() As PartialViewResult
            Return PartialView("_ReplaceComponentForm")
        End Function

        '2013.03.06
        ' POST: /reparacion/reparacion
        <Authorize, HttpPost> _
        Public Function reparacion(ByVal model As RemplazoComponenteModel) As PartialViewResult
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = ""

            Try
                If model.Command = "Reemplazar" Then
                    Me.DR = Me.DA.ExecuteSP("rp_SaveReplaceComponent", model.OrderID, model.PartNo, model.SerialNo, model.Componente, _
                                            model.Failure, model.Process, model.Comment, User.Identity.Name)
                Else
                    DR = Me.DA.ExecuteSP("rp_SetReapairSubOrder", model.OrderID, model.PartNo, model.SerialNo, model.Componente, _
                                            model.Failure, model.Process, model.Comment, User.Identity.Name)
                End If

                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                'result = Me.View
            Finally
                Me.DA.Dispose()
            End Try
            result = PartialView("_ReplaceComponentForm")
            Return result

        End Function

        ' 2013.03.06
        ' GET: /reparacion/LoadReplaceCompHistory
        <Authorize> _
        Function LoadReplaceCompHistory() As PartialViewResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim myModel(100) As ReplaceCompHistoryModel
            Dim Cont As Integer = 0
            Try
                DR = DA.ExecuteSP("rp_getReplaceCompHistory", Session("OrderID"))
                If DA._LastErrorMessage <> "" Then Throw New Exception(DA._LastErrorMessage)

                If DR.HasRows Then
                    While DR.Read
                        myModel(Cont) = New ReplaceCompHistoryModel With {.OrderID = DR(0), .Component = DR(1), .Failure = DR(2), .Process = DR(3)}
                        Cont = Cont + 1
                    End While
                    ReDim Preserve myModel(Cont - 1)
                Else
                    myModel(Cont) = New ReplaceCompHistoryModel With {.OrderID = "No data", .Component = "No Data", .Failure = "No Data", .Process = "No Data"}
                    Cont = Cont + 1
                    ReDim Preserve myModel(Cont - 1)
                End If
            Catch ex As Exception
                myModel(Cont) = New ReplaceCompHistoryModel With {.OrderID = "No data", .Component = "No Data", .Failure = "No Data", .Process = "No Data"}
                Cont = Cont + 1
                ReDim Preserve myModel(Cont - 1)
            Finally
                DA.Dispose()
            End Try


            TempData("ModelRep") = myModel
            Return PartialView("_ReplaceCompHistory")
        End Function

        ' -- Terminar reparacion
        ' 2013.03.06
        ' GET: /reparacion/cerrar_reparacion
        <Authorize> _
        Public Function cerrar_reparacion() As ActionResult
            Return Me.View
        End Function

        ' 2013.03.06
        ' POST: /reparacion/cerrar_reparacion
        <Authorize, HttpPost> _
        Public Function cerrar_reparacion(ByVal model As ScanOrderModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = ""

            Try
                Me.DR = Me.DA.ExecuteSP("rp_CloseRepair", model.OrderID, model.Comment, User.Identity.Name)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Return RedirectToAction("Index")
        End Function


        ' 2013.05.20
        ' GET: /reparacion/iniciar_reparacion
        <Authorize> _
        Public Function iniciar_reparacion() As ActionResult
            Me.Session.Clear()
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As PendingOrdersModel
            Dim i As Integer = 0
            Try
                DR = DA.ExecuteSP("rp_PendientesReparación", "LIST")
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(i) = New PendingOrdersModel With {.OrderID = DR(0), .Customer = DR(1), .ProductType = DR(2), _
                                            .SerialNo = DR(3), .Model = DR(4), .Description = DR(5), .DateR = DR(6), .DateComp = DR(7)}
                        i += 1
                    Loop
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(i - 1)
                Else
                    i = 0
                    modelArray(i) = New PendingOrdersModel With {.OrderID = "No Data", .Customer = "No Data", .ProductType = "No Data", _
                        .SerialNo = "No Data", .Model = "No Data", .Description = "No Data", .DateR = "No Data", .DateComp = "No data"}
                    i += 1
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(i - 1)
                End If

                TempData("Model") = modelArray
            Catch ex As Exception
                TempData("ErrMsg") = ex.Message
                i = 0
                modelArray(i) = New PendingOrdersModel With {.OrderID = "No Data", .Customer = "No Data", .ProductType = "No Data", _
                        .SerialNo = "No Data", .Model = "No Data", .Description = "No Data", .DateR = "No Data", .DateComp = "No data"}
                i += 1
                ReDim Preserve modelArray(i - 1)

                Return RedirectToAction("Index")
            Finally
                DA.Dispose()
            End Try

            Return Me.View
        End Function

        ' 2013.05.20
        ' POST: /reparacion/iniciar_reparacion
        <Authorize, HttpPost> _
        Public Function iniciar_reparacion(ByVal model As IniciarReparacionModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = ""

            Try
                Me.DR = Me.DA.ExecuteSP("rp_StartRepairSP", model.OrderID, model.Comment, User.Identity.Name)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Return RedirectToAction("Index")
        End Function

        '2013.04.15
        ' GET: /reparacion/ordenes_en_reparacion
        Public Function ordenes_en_reparacion() As ActionResult
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
                Me.DR = Me.DA.ExecuteSP("rp_getInRepairProcess")
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New PendingOrdersModel With {.OrderID = DR(0), .Customer = DR(1), .ProductType = DR(2), _
                                            .SerialNo = DR(3), .Model = DR(4), .Description = DR(5), .DateR = DR(6), .DateD = DR(7), .CreateBy = DR(8)}
                        If index + 1 = modelArray.Length Then ReDim Preserve modelArray(index + 10)
                        index += 1
                    Loop
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New PendingOrdersModel With {.OrderID = "No data", .Customer = "No data", .ProductType = "No data", _
                                            .SerialNo = "No data", .Model = "No data", .Description = "No data", .DateR = "No data", .DateD = "No data", .CreateBy = "No data"}
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
