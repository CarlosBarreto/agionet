Imports System.Data.SqlClient

Namespace AgioNet
    Public Class reparacionController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /reparacion
        <Authorize> _
        Function Index() As ActionResult
            Me.Session.Clear()
            Return View()
        End Function

        ' 2013.02.13
        ' POST: /diagnostico/Index
        <HttpPost, Authorize> _
        Public Function Index(ByVal model As StartDiagnosticModel) As ActionResult
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
        ' GET: /reparacion/ordenes_pendientes
        <Authorize> _
        Public Function ordenes_pendientes() As ActionResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As PendientesreparacionModel
            Dim index As Integer = 0
            Try
                DR = DA.ExecuteSP("rp_PendientesReparación")
                If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                    Do While Me.DR.Read
                        modelArray(index) = New PendientesreparacionModel With {.OrderID = DR(0), .Comments = DR(1), .ApprovalDate = DR(2), _
                                            .PartNumber = DR(3), .ProductDescription = DR(4), .SerialNumber = DR(5), .Failure = DR(6), _
                                            .Solution = DR(7), .Source = DR(8), .Comment = DR(9)}
                        If index >= modelArray.Length Then ReDim Preserve modelArray(index + 1)
                        index += 1
                    Loop
                    ReDim Preserve modelArray(index - 1)
                Else
                    Throw New Exception(DA._LastErrorMessage)
                End If

                '-----
                TempData("Model") = modelArray
            Catch ex As Exception
                TempData("ErrMsg") = ex.Message
                Return RedirectToAction("Index")
            Finally
                DA.Dispose()
            End Try

            Return Me.View
        End Function

        ' 2013.03.01
        ' GET: /reparacion/reparar_orden
        <Authorize> _
        Public Function reparar_orden() As ActionResult
            If TempData("VFlag") = "TRUE" Then
                TempData.Keep("Model")
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
            Dim result As ActionResult
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
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim str As String = ""

            Try
                Me.DR = Me.DA.ExecuteSP("rp_SaveReplacePart", model.OrderID, model.PartNo, model.SerialNo, "NEW PART", "NEW PART", model.Failure, model.Comment, User.Identity.Name)
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
                Me.DR = Me.DA.ExecuteSP("rp_SaveReplaceComponent", model.OrderID, model.PartNo, model.SerialNo, model.Componente, _
                                        model.Failure, model.Process, model.Comment, User.Identity.Name)
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

        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader
    End Class
End Namespace
