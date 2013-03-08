Imports System.Data.SqlClient

Namespace AgioNet
    Public Class calidadController
        Inherits System.Web.Mvc.Controller

        ' 2013.03.08
        ' GET: /calidad
        Function Index() As ActionResult
            Return View()
        End Function

        ' 2013.03.08
        ' POST: /calidad/Index
        <HttpPost, Authorize> _
        Public Function Index(ByVal model As ScanInfoModel) As ActionResult
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

        ' 2013.03.08
        ' GET: /calidad/proceso_calidad
        <Authorize> _
        Public Function proceso_calidad() As ActionResult
            DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As ProcesoCalidadModels
            Dim index As Integer = 0
            Try
                DR = DA.ExecuteSP("qa_getProcesoCalidad")
                If (Me.DR.HasRows And (Me.DA._LastErrorMessage = "")) Then
                    Do While Me.DR.Read
                        modelArray(index) = New ProcesoCalidadModels With {.OrderID = DR(1), .PartNumber = DR(2), .SerialNumber = DR(3), _
                                                                           .FailureType = DR(4), .Comment = DR(5)}
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


        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader
    End Class


End Namespace
