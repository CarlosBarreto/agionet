Imports System.Data.SqlClient

Namespace AgioNet
    Public Class calidadController
        Inherits System.Web.Mvc.Controller

        ' 2013.03.08
        ' GET: /calidad
        Function Index() As ActionResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As ProcesoCalidadModels
            Dim i As Integer = 0
            Try
                DR = DA.ExecuteSP("qa_getProcesoCalidad")
                If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                    Do While Me.DR.Read
                        modelArray(i) = New ProcesoCalidadModels With {.OrderID = DR(1), .PartNumber = DR(2), .SerialNumber = DR(3), _
                                                                           .FailureType = DR(4), .Comment = DR(5), .DateComp = DR(6)}
                        If i >= modelArray.Length Then ReDim Preserve modelArray(i + 1)
                        i += 1
                    Loop
                    ReDim Preserve modelArray(i - 1)
                Else
                    Throw New Exception(DA._LastErrorMessage)
                End If


            Catch ex As Exception
                TempData("ErrMsg") = ex.Message
                i = 0
                modelArray(i) = New ProcesoCalidadModels With {.OrderID = "no data", .PartNumber = "no data", .SerialNumber = "no data", _
                                                                          .FailureType = "no data", .Comment = "no data", .DateComp = "no data"}
                i += 1
                ReDim Preserve modelArray(i - 1)


                'Return RedirectToAction("Index")
            Finally
                DA.Dispose()
            End Try

            '-----
            TempData("Model") = modelArray
            Return Me.View
        End Function

        ' POST: 
        <Authorize, HttpPost> _
        Public Function Index(ByVal model As ScanOrderModel) As ActionResult
            Return RedirectToAction("inspeccion_calidad", New With {.OrderID = model.OrderID})
        End Function

        ' 2013.03.08
        ' GET: /calidad/proceso_calidad
        <Authorize> _
        Public Function proceso_calidad() As ActionResult
            Return RedirectToAction("Index")
            'NOT USED --            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            'NOT USED --            Dim modelArray(100) As ProcesoCalidadModels
            'NOT USED --            Dim index As Integer = 0
            'NOT USED --            Try
            'NOT USED --            DR = DA.ExecuteSP("qa_getProcesoCalidad")
            'NOT USED --            If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
            'NOT USED --            Do While Me.DR.Read
            'NOT USED --            modelArray(Index) = New ProcesoCalidadModels With {.OrderID = DR(1), .PartNumber = DR(2), .SerialNumber = DR(3), _
            'NOT USED --                                                               .FailureType = DR(4), .Comment = DR(5)}
            'NOT USED --            If Index() >= modelArray.Length Then ReDim Preserve modelArray(Index() + 1)
            'NOT USED --            Index += 1
            'NOT USED --            Loop
            'NOT USED --            ReDim Preserve modelArray(Index() - 1)
            'NOT USED --            Else
            'NOT USED --            Throw New Exception(DA._LastErrorMessage)
            'NOT USED --            End If

            '-----
            'NOT USED --            TempData("Model") = modelArray
            'NOT USED --            Catch ex As Exception
            'NOT USED --            TempData("ErrMsg") = ex.Message
            'NOT USED --            Return RedirectToAction("Index")
            'NOT USED --            Finally
            'NOT USED --            DA.Dispose()
            'NOT USED --            End Try

            'NOT USED --            Return Me.View
        End Function


        ' 2013-03-10
        ' GET: /calidad/inspeccion_calidad
        <Authorize> _
        Public Function inspeccion_calidad(ByVal model As ScanOrderModel) As ActionResult
            Dim result As ActionResult

            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As DatosCalidadModel
            Dim DatosFalla As New DatosFallaModel

            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("qa_getDatosCalidad", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(Me.DA.LastErrorMessage)
                End If
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        modelArray(index) = New DatosCalidadModel With {.OrderID = DR(0), .PartNumber = DR(1), .Description = DR(2), _
                                            .Aprobado = DR(3), .Failure = DR(4), .Solution = DR(5), .TipoReparacion = DR(6), _
                                            .Comentario = DR(7), .ReparadoPor = DR(8), .RetroAC = DR(9), .NewPart = DR(10), _
                                            .NewSerialNo = DR(11), .OldPartNo = DR(12), .OldSerialNo = DR(13), .Process = DR(14)}

                        index += 1
                    Loop
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New DatosCalidadModel With {.OrderID = "No data", .PartNumber = "No data", .Description = "No data", _
                                            .Aprobado = "No data", .Failure = "No data", .Solution = "No data", .TipoReparacion = "No data", _
                                             .Comentario = "No data", .ReparadoPor = "No data", .RetroAC = "no data", .NewPart = "no data", _
                                            .NewSerialNo = "no data", .OldPartNo = "no data", .OldSerialNo = "no data", .Process = "no data"}
                    index += 1
                    ' -- Reparado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If

                'Agregar los datos de la falla [qa_getDatosFalla]
                If Not DR.IsClosed Then
                    DR.Close()
                End If

                Me.DR = Me.DA.ExecuteSP("qa_getDatosFalla", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(Me.DA.LastErrorMessage)
                End If
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        DatosFalla = New DatosFallaModel With {.OrderID = DR(0), .Falla = DR(1), .Retro = DR(2), .Comment = DR(3), .TComment = DR(4), .ComentarioCierre = DR(5)}
                    Loop
                Else
                    DatosFalla = New DatosFallaModel With {.OrderID = "no data", .Falla = "no data", .Retro = "no data", .Comment = "no data", .TComment = "no data", .ComentarioCierre = "no data"}
                End If


                result = Me.View
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                result = Me.RedirectToAction("index")
            Finally
                DA.Dispose()
            End Try

            Me.TempData.Item("Model") = modelArray
            Me.TempData.Item("Falla") = DatosFalla
            Me.TempData.Item("OrderID") = model.OrderID

            Return result
        End Function

        '2013.03.10
        ' POST: /calidad/inspeccion_calidad
        <Authorize, HttpPost> _
        Public Function inspeccion_calidad(ByVal model As InspeccionCalidadModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                If model.Acepto <> True Then
                    Throw New Exception("Debe confirmar que ha Comprobado la información de Calidad")
                End If

                Me.DR = Me.DA.ExecuteSP("SaveInspeccionCalidad", model.OrderID, model.Result, model.Comment, User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                Else
                    While DR.Read
                        TempData("ErrMsg") = DR(0)
                    End While
                End If

                result = RedirectToAction("Index")
            Catch ex As Exception
                TempData("ErrMsg") = ex.Message
                result = RedirectToAction("inspeccion_calidad", "calidad", New With {.OrderID = model.OrderID})
            Finally
                DA.Dispose()
            End Try

            Return result
        End Function


        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader
    End Class


End Namespace
