Imports System.Data.SqlClient

Namespace AgioNet
    Public Class reparacionController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /reparacion
        <Authorize> _
        Function Index() As ActionResult
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
                Me.DR = Me.DA.ExecuteSP("dg_OrderValidation", model.OrderID)
                If (Me.DA._LastErrorMessage <> "") Then
                    Throw New Exception(DA._LastErrorMessage)
                Else
                    Do While DR.Read
                        str = DR(0)
                    Loop

                    If str.ToString <> "TRUE" Then
                        Throw New Exception(str.ToString)
                    End If
                End If

                result = Me.RedirectToAction("reparar_orden")
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                result = Me.View
            Finally
                Me.DA.Dispose()
            End Try

            Return result
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
            Return Me.View
        End Function

        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader
    End Class
End Namespace
