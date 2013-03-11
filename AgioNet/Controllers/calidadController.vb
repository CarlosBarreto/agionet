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


        ' 2013-03-10
        ' GET: /calidad/inspeccion_calidad
        <Authorize> _
        Public Function inspeccion_calidad() As ActionResult
            Return Me.View()
        End Function

        '2013.03.10
        ' POST: /calidad/inspeccion_calidad
        <Authorize, HttpPost> _
        Public Function inspeccion_calidad(ByVal model As InspeccionCalidadModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Try
                Me.DR = Me.DA.ExecuteSP("SaveInspeccionCalidad", model.OrderID, model.Result, model.Comment, User.Identity.Name)
                If DA._LastErrorMessage <> "" Then
                    Throw New Exception(DA._LastErrorMessage)
                End If
                result = RedirectToAction("inspeccion_calidad")
            Catch ex As Exception
                TempData("ErrMsg") = ex.Message
                result = RedirectToAction("Index")
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
