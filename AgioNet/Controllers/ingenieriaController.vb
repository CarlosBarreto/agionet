Imports System.Data.SqlClient

Namespace AgioNet
    Public Class ingenieriaController
        Inherits Controller
        '--- Methods

        ' 2013.02.14 
        ' GET: /ingenieria/agregar_material
        <Authorize> _
        Public Function agregar_material() As ActionResult
            Return Me.View
        End Function

        '2013.02.14
        ' POST: /ingenieria/agregar_material
        <HttpPost, Authorize> _
        Public Function agregar_material(ByVal model As MaterialMasterModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As New MaterialMasterListModel
            Try
                Me.DR = Me.DA.ExecuteSP("in_AddMaterial", model.SKUNo, model.PartNo, model.Commodity, model.Description, model.Alt)

                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Return Me.View
                End If

                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = DR(0)
                Loop
                result = Me.RedirectToAction("agregar_material")
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return View()
            Finally
                Me.DA.Dispose()
            End Try
            Return result
        End Function

        ' 2013.02.14
        ' GET: /ingenieria/asignar_responsable
        <Authorize, HttpGet> _
        Public Function asignar_responsable() As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As EstacionesModel
            Dim index As Integer = 0

            Try
                Me.DR = Me.DA.ExecuteSP("in_station_getInfo", "ALL")

                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.PartialView("_estacionesMainPartial")
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        Dim model As New EstacionesModel With { _
                            .Nombre = DR(0), _
                            .Descripcion = DR(1), _
                            .Usuario = DR(2), _
                            .Proceso = DR(3) _
                        }
                        modelArray(index) = model
                        index += 1
                    Loop

                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New EstacionesModel With { _
                        .Nombre = "No Data", _
                        .Descripcion = "No Data", _
                        .Usuario = "No Data", _
                        .Proceso = "No Data" _
                    }
                    index += 1
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.PartialView("_estacionesMainPartial")
            End Try

            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If
            Me.DA.Dispose()

            Me.TempData.Item("Model") = modelArray
            Return Me.PartialView("_asignarestacionPartial")
        End Function

        ' 2013.02.14
        ' GET: /ingenieria/asignar_usuario
        <Authorize, HttpGet> _
        Public Function asignar_usuario() As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As EstacionesModel
            Dim index As Integer = 0

            Try
                Me.DR = Me.DA.ExecuteSP("in_station_getInfo", "ALL")

                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.PartialView("_estacionesMainPartial")
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        Dim model As New EstacionesModel With { _
                            .Nombre = DR(0), _
                            .Descripcion = DR(1), _
                            .Usuario = DR(2), _
                            .Proceso = DR(3) _
                        }
                        modelArray(index) = model
                        index += 1
                    Loop
                    ' -- Actualizar Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New EstacionesModel With { _
                        .Nombre = "No Data", _
                        .Descripcion = "No Data", _
                        .Usuario = "No Data", _
                        .Proceso = "No Data" _
                    }
                    index += 1

                    ' -- Actualizar Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.PartialView("_estacionesMainPartial")
            End Try

            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If
            Me.DA.Dispose()

            Me.TempData.Item("Model") = modelArray
            Return Me.PartialView("_asignarusuarioPartial")
        End Function

        ' 2013.02.14
        ' POST: /ingenieria/asignarresponsable
        <HttpPost, Authorize> _
        Public Function asignarresponsable(ByVal model As EstacionesModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As New MaterialMasterListModel

            Try
                Me.DR = Me.DA.ExecuteSP("in_station_Assign", model.Nombre, model.Usuario)

                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Return Me.PartialView("_borrarestacionPartial")
                End If

                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = DR(0)
                Loop

            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.PartialView("_borrarestacionPartial")
            Finally
                Me.DA.Dispose()
            End Try
            Return Me.PartialView("_estacionesMainPartial")
        End Function

        ' 2013.02.14
        ' GET: /ingenieria/asignarresponsable
        <Authorize> _
        Public Function asignarresponsable(ByVal model As EstacionModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model2 As New EstacionesModel
            Dim modelArray(100) As UserListModel

            Try
                Dim model3 As EstacionesModel
                Me.DR = Me.DA.ExecuteSP("in_station_getInfo", "DET", model.Nombre)

                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.PartialView("_estacionesMainPartial")
                End If

                If Me.DR.HasRows Then
                    Dim num As Integer
                    Do While Me.DR.Read
                        model3 = New EstacionesModel With { _
                            .Nombre = DR(0), _
                            .Descripcion = DR(1), _
                            .Usuario = DR(2), _
                            .Proceso = DR(3) _
                        }
                        model2 = model3
                    Loop

                    If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                        Me.DR.Close()
                    End If

                    Me.DR = Me.DA.ExecuteSP("sys_UserList")
                    If (Me.DA._LastErrorMessage <> "") Then
                        Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                        Me.DA.Dispose()
                        Return Me.PartialView("_estacionesMainPartial")
                    End If

                    If Me.DR.HasRows Then
                        num = 0
                        Do While Me.DR.Read
                            Dim model4 As New UserListModel With { _
                                .Nombre = DR(0), _
                                .Usuario = DR(1) _
                            }
                            modelArray(num) = model4
                            num += 1
                        Loop

                        ' -- Actualizado por Carlos Barreto
                        ReDim modelArray(num - 1)

                    Else
                        modelArray(num) = New UserListModel With { _
                            .Nombre = "Sistema", _
                            .Usuario = "System" _
                        }
                        num += 1
                        ' -- Actualizado por Carlos Barreto
                        ReDim modelArray(num - 1)
                    End If
                Else
                    model3 = New EstacionesModel With { _
                        .Nombre = "No Data", _
                        .Descripcion = "No Data", _
                        .Usuario = "No Data", _
                        .Proceso = "No Data" _
                    }
                    model2 = model3
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.PartialView("_estacionesMainPartial")
            End Try

            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If
            Me.DA.Dispose()

            Me.TempData.Item("UserModel") = modelArray
            Me.TempData.Item("Model") = model2
            Return Me.PartialView("_asignarestacionPartialForm")
        End Function

        ' 2013.02.14
        ' POST: /ingenieria/asignarusuario
        <HttpPost, Authorize> _
        Public Function asignarusuario(ByVal model As EstacionesModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As New MaterialMasterListModel

            Try
                Me.DR = Me.DA.ExecuteSP("in_station_AssignUser ", model.Nombre, model.Usuario)

                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Return Me.PartialView("_borrarestacionPartial")
                End If

                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = DR(0)
                Loop
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.PartialView("_borrarestacionPartial")
            Finally
                Me.DA.Dispose()
            End Try

            Return Me.PartialView("_estacionesMainPartial")
        End Function

        ' 2013.02.14
        ' GET: /ingenieria/asignarusuario
        <Authorize> _
        Public Function asignarusuario(ByVal model As EstacionModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model2 As New EstacionesModel
            Dim modelArray(100) As UserListModel
            Try
                Dim model3 As EstacionesModel

                Me.DR = Me.DA.ExecuteSP("in_station_getInfo", "DET", model.Nombre)
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.PartialView("_estacionesMainPartial")
                End If

                If Me.DR.HasRows Then
                    Dim num As Integer
                    Do While Me.DR.Read
                        model3 = New EstacionesModel With { _
                            .Nombre = DR(0), _
                            .Descripcion = DR(1), _
                            .Usuario = DR(2), _
                            .Proceso = DR(3) _
                        }
                        model2 = model3
                    Loop

                    If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                        Me.DR.Close()
                    End If

                    Me.DR = Me.DA.ExecuteSP("sys_UserList", "RESP", model.Nombre)
                    If (Me.DA._LastErrorMessage <> "") Then
                        Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                        Me.DA.Dispose()
                        Return Me.PartialView("_estacionesMainPartial")
                    End If

                    If Me.DR.HasRows Then
                        num = 0
                        Do While Me.DR.Read
                            Dim model4 As New UserListModel With { _
                                .Nombre = DR(0), _
                                .Usuario = DR(1) _
                            }
                            modelArray(num) = model4
                            num += 1
                        Loop
                        '--- Actualizado por Carlos Barreto
                        ReDim Preserve modelArray(num - 1)
                    Else
                        modelArray(num) = New UserListModel With { _
                            .Nombre = "Sistema", _
                            .Usuario = "System" _
                        }
                        num += 1
                        '--- Actualizado por Carlos Barreto
                        ReDim Preserve modelArray(num - 1)
                    End If
                Else
                    model3 = New EstacionesModel With { _
                        .Nombre = "No Data", _
                        .Descripcion = "No Data", _
                        .Usuario = "No Data", _
                        .Proceso = "No Data" _
                    }
                    model2 = model3
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.PartialView("_estacionesMainPartial")
            End Try

            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If
            Me.DA.Dispose()

            Me.TempData.Item("UserModel") = modelArray
            Me.TempData.Item("Model") = model2
            Return Me.PartialView("_asignarusuarioPartialForm")
        End Function

        ' 2013.02.14
        ' GET: /ingenieria/borrar_estacion
        <Authorize> _
        Public Function borrar_estacion() As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As EstacionesModel
            Dim index As Integer = 0

            Try
                Me.DR = Me.DA.ExecuteSP("in_station_getInfo", "ALL")
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.PartialView("_estacionesMainPartial")
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        Dim model As New EstacionesModel With { _
                            .Nombre = DR(0), _
                            .Descripcion = DR(1), _
                            .Usuario = DR(2), _
                            .Proceso = DR(3) _
                        }
                        modelArray(index) = model
                        index += 1
                    Loop
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New EstacionesModel With { _
                        .Nombre = "No Data", _
                        .Descripcion = "No Data", _
                        .Usuario = "No Data", _
                        .Proceso = "No Data" _
                    }
                    index += 1
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If

            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.PartialView("_estacionesMainPartial")
            End Try

            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If
            Me.DA.Dispose()

            Me.TempData.Item("Model") = modelArray
            Return Me.PartialView("_borrarestacionPartial")
        End Function

        ' 2013.02.14
        ' POST: /ingenieria/borrarestacion
        <HttpPost, Authorize> _
        Public Function borrarestacion(ByVal model As EstacionesModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As New MaterialMasterListModel

            Try
                Me.DR = Me.DA.ExecuteSP("in_station_Del", model.Nombre)

                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Return Me.PartialView("_borrarestacionPartial")
                End If

                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = DR(0)
                Loop

            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.PartialView("_borrarestacionPartial")
            Finally
                Me.DA.Dispose()
            End Try

            Return Me.PartialView("_estacionesMainPartial")
        End Function

        ' 2013.02.14
        ' GET: /ingenieria/borrarestacion
        <HttpGet, Authorize> _
        Public Function borrarestacion(ByVal model As EstacionModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model2 As New EstacionesModel

            Try
                Dim model3 As EstacionesModel
                Me.DR = Me.DA.ExecuteSP("in_station_getInfo", "DET", model.Nombre)
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.PartialView("_estacionesMainPartial")
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        model3 = New EstacionesModel With { _
                            .Nombre = DR(0), _
                            .Descripcion = DR(1), _
                            .Usuario = DR(2), _
                            .Proceso = DR(3) _
                        }
                        model2 = model3
                    Loop
                Else
                    model3 = New EstacionesModel With { _
                        .Nombre = "No Data", _
                        .Descripcion = "No Data", _
                        .Usuario = "No Data", _
                        .Proceso = "No Data" _
                    }
                    model2 = model3
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.PartialView("_estacionesMainPartial")
            End Try

            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If
            Me.DA.Dispose()

            Me.TempData.Item("Model") = model2
            Return Me.PartialView("_borrarestacionPartialForm")
        End Function

        ' 2013.02.14
        ' GET: /ingenieria/crear_estacion
        <Authorize> _
        Public Function crear_estacion() As PartialViewResult
            Return Me.PartialView("_crearestacionPartial")
        End Function

        ' 2013.02.14
        ' POST: /ingenieria/crear_estacion
        <HttpPost, Authorize> _
        Public Function crear_estacion(ByVal model As EstacionesModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As New MaterialMasterListModel
            Try
                Me.DR = Me.DA.ExecuteSP("in_station_Add", model.Nombre, model.Descripcion, model.Usuario, model.Proceso)
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Return Me.PartialView("_crearestacionPartial")
                End If

                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = DR(0)
                Loop
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.PartialView("_crearestacionPartial")
            Finally
                Me.DA.Dispose()
            End Try

            Return Me.PartialView("_estacionesMainPartial")
        End Function

        ' 2013.02.14
        ' GET: /ingenieria/editar_estacion
        <Authorize> _
        Public Function editar_estacion() As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As EstacionesModel
            Dim index As Integer = 0

            Try
                Me.DR = Me.DA.ExecuteSP("in_station_getInfo", "ALL")
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.PartialView("_estacionesMainPartial")
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        Dim model As New EstacionesModel With { _
                            .Nombre = DR(0), _
                            .Descripcion = DR(1), _
                            .Usuario = DR(2), _
                            .Proceso = DR(3) _
                        }
                        modelArray(index) = model
                        index += 1
                    Loop
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                Else
                    index = 0
                    modelArray(index) = New EstacionesModel With { _
                        .Nombre = "No Data", _
                        .Descripcion = "No Data", _
                        .Usuario = "No Data", _
                        .Proceso = "No Data" _
                    }
                    index += 1
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.PartialView("_estacionesMainPartial")
            End Try

            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If
            Me.DA.Dispose()

            Me.TempData.Item("Model") = modelArray
            Return Me.PartialView("_editarestacionPartial")
        End Function

        ' 2013.02.14
        ' GET: /ingenieria/editar_material
        <Authorize> _
        Public Function editar_material(ByVal m As MaterialMasterModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As MaterialMasterListModel
            Dim index As Integer = 0
            Dim response As ActionResult = Me.View

            Try
                If Not DR Is Nothing Then If Not DR.IsClosed Then DR.Close()

                DR = Me.DA.ExecuteSP("in_getMaterialList", m.SKUNo, m.PartNo, m.Description, m.Alt)
                If DA._LastErrorMessage = "" Then
                    If Me.DR.HasRows Then
                        Do While DR.Read
                            modelArray(index) = New MaterialMasterListModel With { _
                                .SKUNo = DR(0), .PartNo = DR(1), .Description = DR(3), .Alt = DR(4)}
                            index += 1
                            If index >= modelArray.Length Then
                                ReDim Preserve modelArray(index + 10)
                            End If
                        Loop
                        ' -- Actualizado por Carlos Barreto
                        ReDim Preserve modelArray(index - 1)
                        TempData.Item("model") = modelArray
                        response = Me.View
                    Else
                        Throw New Exception("Error: No se encontraron registros")
                    End If
                Else
                    Throw New Exception(DA._LastErrorMessage)
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                response = Me.RedirectToAction("Index")
            Finally
                Me.DA.Dispose()
            End Try

            Return response
        End Function

        ' 2013.02.14
        ' POST: /ingenieria/editarestacion
        <Authorize, HttpPost> _
        Public Function editarestacion(ByVal model As EstacionesModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As New MaterialMasterListModel

            Try
                Me.DR = Me.DA.ExecuteSP("in_station_Edit", model.Nombre, model.Descripcion, model.Usuario, model.Proceso)
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Return Me.PartialView("_editarestacionPartial")
                End If

                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = DR(0)
                Loop
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.PartialView("_editarestacionPartial")
            Finally
                Me.DA.Dispose()
            End Try

            Return Me.PartialView("_estacionesMainPartial")
        End Function

        ' 2013.02.14
        ' GET: /ingenieria/editarestacion
        <HttpGet, Authorize> _
        Public Function editarestacion(ByVal model As EstacionModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model2 As New EstacionesModel
            Try
                Dim model3 As EstacionesModel
                Me.DR = Me.DA.ExecuteSP("in_station_getInfo", "DET", model.Nombre)
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.PartialView("_estacionesMainPartial")
                End If

                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        model3 = New EstacionesModel With { _
                            .Nombre = DR(0), _
                            .Descripcion = DR(1), _
                            .Usuario = DR(2), _
                            .Proceso = DR(3) _
                        }
                        model2 = model3
                    Loop
                Else
                    model3 = New EstacionesModel With { _
                        .Nombre = "No Data", _
                        .Descripcion = "No Data", _
                        .Usuario = "No Data", _
                        .Proceso = "No Data" _
                    }
                    model2 = model3
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.PartialView("_estacionesMainPartial")
            End Try

            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If

            Me.DA.Dispose()
            Me.TempData.Item("Model") = model2
            Return Me.PartialView("_editarestacionPartialForm")
        End Function

        ' 2013.02.14
        ' GET: /ingenieria/editarestacion
        <Authorize> _
        Public Function editarmaterial(ByVal model As MaterialMasterListModel) As ActionResult
            Me.TempData.Item("SKUNo") = model.SKUNo
            Me.TempData.Item("PartNo") = model.PartNo
            Me.TempData.Item("Description") = model.Description
            Me.TempData.Item("Alt") = model.Alt
            Return Me.View
        End Function

        ' 2013.02.14
        ' POST: /ingenieria/editarmaterial
        <Authorize, HttpPost> _
        Public Function editarmaterial(ByVal model As MaterialMasterModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As New MaterialMasterListModel

            Try
                Me.DR = Me.DA.ExecuteSP("in_EditMaterial", model.SKUNo, model.PartNo, model.Commodity, model.Description, model.Alt, model.OldSKUNo, model.OldPartNo)
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Return Me.View
                End If

                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = DR(0)
                Loop
                result = Me.RedirectToAction("editar_material")
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return Me.View
            Finally
                Me.DA.Dispose()
            End Try

            Return result
        End Function

        ' 2013.02.14
        ' GET: /ingenieria/estaciones
        <Authorize> _
        Public Function estaciones() As ActionResult
            Return Me.View
        End Function

        ' 2013.02.14
        ' GET: /ingenieria/Index
        <HttpGet, Authorize> _
        Public Function Index() As ActionResult
            Return Me.View
        End Function

        ' 2013.02.14
        ' GET: /ingenieria/main_estacion
        <Authorize> _
        Public Function main_estacion() As PartialViewResult
            Return Me.PartialView("_estacionesMainPartial")
        End Function

        ' 2013.02.14
        ' GET: /ingenieria/material_master
        <Authorize> _
        Public Function material_master(ByVal m As MaterialMasterModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As MaterialMasterListModel
            Dim index As Integer = 0

            Try
                Me.DR = Me.DA.ExecuteSP("in_getMaterialList", m.SKUNo, m.PartNo, m.Description, m.Alt)
                If (Me.DA._LastErrorMessage = "") Then
                    Dim model As MaterialMasterListModel

                    If Me.DR.HasRows Then
                        Do While Me.DR.Read
                            model = New MaterialMasterListModel With { _
                                .SKUNo = DR(0), _
                                .PartNo = DR(1), _
                                .Commodity = DR(2), _
                                .Description = DR(3), _
                                .Alt = DR(4) _
                            }
                            modelArray(index) = model
                            index += 1
                        Loop
                        ' -- Actualizado por Carlos Barreto
                        ReDim Preserve modelArray(index - 1)
                    Else
                        model = New MaterialMasterListModel With { _
                            .SKUNo = "no data", _
                            .PartNo = "no data", _
                            .Commodity = "no data", _
                            .Description = "no data", _
                            .Alt = "no data" _
                        }
                        modelArray(index) = model
                        index += 1
                        ' -- Actualizado por Carlos Barreto
                        ReDim Preserve modelArray(index - 1)
                    End If
                Else
                    modelArray(index) = New MaterialMasterListModel With { _
                        .SKUNo = "no data", _
                        .PartNo = "no data", _
                        .Commodity = "no data", _
                        .Description = "no data", _
                        .Alt = "no data" _
                    }
                    index += 1
                    ' -- Actualizado por Carlos Barreto
                    ReDim Preserve modelArray(index - 1)
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsn") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Me.TempData.Item("Model") = modelArray
            Return Me.View
        End Function

        ' 2013.02.14
        ' GET: /ingenieria/quitar_material
        <Authorize> _
        Public Function quitar_material() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray(100) As MaterialMasterListModel
            Dim index As Integer = 0

            Try
                Me.DR = Me.DA.ExecuteSP("in_getMaterialList")
                If (Me.DA._LastErrorMessage = "") Then
                    Dim model As MaterialMasterListModel

                    If Me.DR.HasRows Then
                        Do While Me.DR.Read
                            model = New MaterialMasterListModel With { _
                                .SKUNo = DR(0), _
                                .PartNo = DR(1), _
                                .Description = DR(3), _
                                .Alt = DR(4) _
                            }
                            modelArray(index) = model
                            If modelArray.Length <= index + 1 Then ReDim Preserve modelArray(index + 1)
                            index += 1
                        Loop

                        ' -- Actualizado por Carlos Barreto
                        ReDim Preserve modelArray(index - 1)
                        Me.TempData.Item("model") = modelArray
                    Else
                        model = New MaterialMasterListModel With { _
                            .SKUNo = "no data", _
                            .PartNo = "no data", _
                            .Description = "no data", _
                            .Alt = "no data" _
                        }
                        modelArray(index) = model
                        Me.TempData.Item("Model") = modelArray
                    End If
                Else
                    modelArray(index) = New MaterialMasterListModel With { _
                        .SKUNo = "no data", _
                        .PartNo = "no data", _
                        .Description = "no data", _
                        .Alt = "no data" _
                    }
                    Me.TempData.Item("Model") = modelArray
                End If
            Catch ex As Exception
                Me.TempData.Item("ErrMsn") = ex.Message
            Finally
                Me.DA.Dispose()
            End Try

            Return Me.View
        End Function

        ' 2013.02.14
        ' POST: /ingenieria/quitar_material
        <Authorize, HttpPost> _
        Public Function quitar_material(ByVal model As MaterialMasterModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)

            Dim modelArray As New MaterialMasterListModel

            Try
                Me.DR = Me.DA.ExecuteSP("in_DelMaterial", model.SKUNo, model.PartNo)
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Return Me.RedirectToAction("quitar_material")
                End If

                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = DR(0)
                Loop
                result = Me.RedirectToAction("quitar_material")
            Catch ex As Exception
                Me.TempData.Item("ErrMsg") = ex.Message
                Return View()
            Finally
                Me.DA.Dispose()
            End Try

            Return result
        End Function


        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader
    End Class


End Namespace
