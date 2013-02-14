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
                Me.DR = Me.DA.ExecuteSP("in_AddMaterial", model.SKUNo, model.PartNo, model.Lvl, model.Description, model.Alt)

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
        ' GET: /ingenieria/asignarresponsable
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

        <Authorize> _
        Public Function asignarresponsable(ByVal model As EstacionModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model2 As New EstacionesModel
            Dim modelArray As UserListModel() = New UserListModel(&H65 - 1) {}
            Try
                Dim model3 As EstacionesModel
                Me.DR = Me.DA.ExecuteSP("in_station_getInfo", New Object() {"DET", model.Nombre})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.PartialView("_estacionesMainPartial")
                End If
                If Me.DR.HasRows Then
                    Dim num As Integer
                    Do While Me.DR.Read
                        model3 = New EstacionesModel With { _
                            .Nombre = DR(0)), _
                            .Descripcion = DR(1)), _
                            .Usuario = DR(2)), _
                            .Proceso = DR(3)) _
                        }
                        model2 = model3
                    Loop
                    If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                        Me.DR.Close()
                    End If
                    Me.DR = Me.DA.ExecuteSP("sys_UserList", New Object(0 - 1) {})
                    If (Me.DA._LastErrorMessage <> "") Then
                        Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                        Me.DA.Dispose()
                        Return Me.PartialView("_estacionesMainPartial")
                    End If
                    If Me.DR.HasRows Then
                        num = 0
                        Do While Me.DR.Read
                            Dim model4 As New UserListModel With { _
                                .Nombre = DR(0)), _
                                .Usuario = DR(1)) _
                            }
                            modelArray(num) = model4
                            num += 1
                        Loop
                        modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New UserListModel(((num - 1) + 1) - 1) {}), UserListModel())
                    Else
                        modelArray(num) = New UserListModel With { _
                            .Nombre = "Sistema", _
                            .Usuario = "System" _
                        }
                        num += 1
                        modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New UserListModel(((num - 1) + 1) - 1) {}), UserListModel())
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
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsg") = exception.Message
                Dim result As PartialViewResult = Me.PartialView("_estacionesMainPartial")
                ProjectData.ClearProjectError()
                Return result
                ProjectData.ClearProjectError()
            End Try
            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If
            Me.DA.Dispose()
            Me.TempData.Item("UserModel") = modelArray
            Me.TempData.Item("Model") = model2
            Return Me.PartialView("_asignarestacionPartialForm")
        End Function

        <HttpPost, Authorize> _
        Public Function asignarusuario(ByVal model As EstacionesModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As MaterialMasterListModel() = New MaterialMasterListModel(2 - 1) {}
            Try
                Me.DR = Me.DA.ExecuteSP("in_station_AssignUser ", New Object() {model.Nombre, model.Usuario})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Return Me.PartialView("_borrarestacionPartial")
                End If
                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = RuntimeHelpers.GetObjectValue(Me.DR.Item(0))
                Loop
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsg") = exception.Message
                Dim result As PartialViewResult = Me.PartialView("_borrarestacionPartial")
                ProjectData.ClearProjectError()
                Return result
                ProjectData.ClearProjectError()
            Finally
                Me.DA.Dispose()
            End Try
            Return Me.PartialView("_estacionesMainPartial")
        End Function

        <Authorize> _
        Public Function asignarusuario(ByVal model As EstacionModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model2 As New EstacionesModel
            Dim modelArray As UserListModel() = New UserListModel(&H65 - 1) {}
            Try
                Dim model3 As EstacionesModel
                Me.DR = Me.DA.ExecuteSP("in_station_getInfo", New Object() {"DET", model.Nombre})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.PartialView("_estacionesMainPartial")
                End If
                If Me.DR.HasRows Then
                    Dim num As Integer
                    Do While Me.DR.Read
                        model3 = New EstacionesModel With { _
                            .Nombre = DR(0)), _
                            .Descripcion = DR(1)), _
                            .Usuario = DR(2)), _
                            .Proceso = DR(3)) _
                        }
                        model2 = model3
                    Loop
                    If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                        Me.DR.Close()
                    End If
                    Me.DR = Me.DA.ExecuteSP("sys_UserList", New Object() {"RESP", model.Nombre})
                    If (Me.DA._LastErrorMessage <> "") Then
                        Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                        Me.DA.Dispose()
                        Return Me.PartialView("_estacionesMainPartial")
                    End If
                    If Me.DR.HasRows Then
                        num = 0
                        Do While Me.DR.Read
                            Dim model4 As New UserListModel With { _
                                .Nombre = DR(0)), _
                                .Usuario = DR(1)) _
                            }
                            modelArray(num) = model4
                            num += 1
                        Loop
                        modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New UserListModel(((num - 1) + 1) - 1) {}), UserListModel())
                    Else
                        modelArray(num) = New UserListModel With { _
                            .Nombre = "Sistema", _
                            .Usuario = "System" _
                        }
                        num += 1
                        modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New UserListModel(((num - 1) + 1) - 1) {}), UserListModel())
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
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsg") = exception.Message
                Dim result As PartialViewResult = Me.PartialView("_estacionesMainPartial")
                ProjectData.ClearProjectError()
                Return result
                ProjectData.ClearProjectError()
            End Try
            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If
            Me.DA.Dispose()
            Me.TempData.Item("UserModel") = modelArray
            Me.TempData.Item("Model") = model2
            Return Me.PartialView("_asignarusuarioPartialForm")
        End Function

        <Authorize> _
        Public Function borrar_estacion() As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As EstacionesModel() = New EstacionesModel(&H65 - 1) {}
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("in_station_getInfo", New Object() {"ALL"})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.PartialView("_estacionesMainPartial")
                End If
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        Dim model As New EstacionesModel With { _
                            .Nombre = DR(0)), _
                            .Descripcion = DR(1)), _
                            .Usuario = DR(2)), _
                            .Proceso = DR(3)) _
                        }
                        modelArray(index) = model
                        index += 1
                    Loop
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New EstacionesModel(((index - 1) + 1) - 1) {}), EstacionesModel())
                Else
                    index = 0
                    modelArray(index) = New EstacionesModel With { _
                        .Nombre = "No Data", _
                        .Descripcion = "No Data", _
                        .Usuario = "No Data", _
                        .Proceso = "No Data" _
                    }
                    index += 1
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New EstacionesModel(((index - 1) + 1) - 1) {}), EstacionesModel())
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsg") = exception.Message
                Dim result As PartialViewResult = Me.PartialView("_estacionesMainPartial")
                ProjectData.ClearProjectError()
                Return result
                ProjectData.ClearProjectError()
            End Try
            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If
            Me.DA.Dispose()
            Me.TempData.Item("Model") = modelArray
            Return Me.PartialView("_borrarestacionPartial")
        End Function

        <HttpPost, Authorize> _
        Public Function borrarestacion(ByVal model As EstacionesModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As MaterialMasterListModel() = New MaterialMasterListModel(2 - 1) {}
            Try
                Me.DR = Me.DA.ExecuteSP("in_station_Del", New Object() {model.Nombre})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Return Me.PartialView("_borrarestacionPartial")
                End If
                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = RuntimeHelpers.GetObjectValue(Me.DR.Item(0))
                Loop
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsg") = exception.Message
                Dim result As PartialViewResult = Me.PartialView("_borrarestacionPartial")
                ProjectData.ClearProjectError()
                Return result
                ProjectData.ClearProjectError()
            Finally
                Me.DA.Dispose()
            End Try
            Return Me.PartialView("_estacionesMainPartial")
        End Function

        <HttpGet, Authorize> _
        Public Function borrarestacion(ByVal model As EstacionModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model2 As New EstacionesModel
            Try
                Dim model3 As EstacionesModel
                Me.DR = Me.DA.ExecuteSP("in_station_getInfo", New Object() {"DET", model.Nombre})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.PartialView("_estacionesMainPartial")
                End If
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        model3 = New EstacionesModel With { _
                            .Nombre = DR(0)), _
                            .Descripcion = DR(1)), _
                            .Usuario = DR(2)), _
                            .Proceso = DR(3)) _
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
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsg") = exception.Message
                Dim result As PartialViewResult = Me.PartialView("_estacionesMainPartial")
                ProjectData.ClearProjectError()
                Return result
                ProjectData.ClearProjectError()
            End Try
            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If
            Me.DA.Dispose()
            Me.TempData.Item("Model") = model2
            Return Me.PartialView("_borrarestacionPartialForm")
        End Function

        <Authorize> _
        Public Function crear_estacion() As PartialViewResult
            Return Me.PartialView("_crearestacionPartial")
        End Function

        <HttpPost, Authorize> _
        Public Function crear_estacion(ByVal model As EstacionesModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As MaterialMasterListModel() = New MaterialMasterListModel(2 - 1) {}
            Try
                Me.DR = Me.DA.ExecuteSP("in_station_Add", New Object() {model.Nombre, model.Descripcion, model.Usuario, model.Proceso})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Return Me.PartialView("_crearestacionPartial")
                End If
                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = RuntimeHelpers.GetObjectValue(Me.DR.Item(0))
                Loop
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsg") = exception.Message
                Dim result As PartialViewResult = Me.PartialView("_crearestacionPartial")
                ProjectData.ClearProjectError()
                Return result
                ProjectData.ClearProjectError()
            Finally
                Me.DA.Dispose()
            End Try
            Return Me.PartialView("_estacionesMainPartial")
        End Function

        <Authorize> _
        Public Function editar_estacion() As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As EstacionesModel() = New EstacionesModel(&H65 - 1) {}
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("in_station_getInfo", New Object() {"ALL"})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.PartialView("_estacionesMainPartial")
                End If
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        Dim model As New EstacionesModel With { _
                            .Nombre = DR(0)), _
                            .Descripcion = DR(1)), _
                            .Usuario = DR(2)), _
                            .Proceso = DR(3)) _
                        }
                        modelArray(index) = model
                        index += 1
                    Loop
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New EstacionesModel(((index - 1) + 1) - 1) {}), EstacionesModel())
                Else
                    index = 0
                    modelArray(index) = New EstacionesModel With { _
                        .Nombre = "No Data", _
                        .Descripcion = "No Data", _
                        .Usuario = "No Data", _
                        .Proceso = "No Data" _
                    }
                    index += 1
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New EstacionesModel(((index - 1) + 1) - 1) {}), EstacionesModel())
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsg") = exception.Message
                Dim result As PartialViewResult = Me.PartialView("_estacionesMainPartial")
                ProjectData.ClearProjectError()
                Return result
                ProjectData.ClearProjectError()
            End Try
            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If
            Me.DA.Dispose()
            Me.TempData.Item("Model") = modelArray
            Return Me.PartialView("_editarestacionPartial")
        End Function

        <Authorize> _
        Public Function editar_material() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As MaterialMasterListModel() = New MaterialMasterListModel(2 - 1) {}
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("in_getMaterialList", New Object(0 - 1) {})
                If (Me.DA._LastErrorMessage = "") Then
                    Dim model As MaterialMasterListModel
                    If Me.DR.HasRows Then
                        Do While Me.DR.Read
                            model = New MaterialMasterListModel With { _
                                .SKUNo = DR(0)), _
                                .PartNo = DR(1)), _
                                .Description = DR(3)), _
                                .Alt = DR(4)) _
                            }
                            modelArray(index) = model
                            index += 1
                            modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New MaterialMasterListModel(((index + 1) + 1) - 1) {}), MaterialMasterListModel())
                        Loop
                        modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New MaterialMasterListModel(((index - 1) + 1) - 1) {}), MaterialMasterListModel())
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
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsn") = exception.Message
                ProjectData.ClearProjectError()
            Finally
                Me.DA.Dispose()
            End Try
            Return Me.View
        End Function

        <Authorize, HttpPost> _
        Public Function editarestacion(ByVal model As EstacionesModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As MaterialMasterListModel() = New MaterialMasterListModel(2 - 1) {}
            Try
                Me.DR = Me.DA.ExecuteSP("in_station_Edit", New Object() {model.Nombre, model.Descripcion, model.Usuario, model.Proceso})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Return Me.PartialView("_editarestacionPartial")
                End If
                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = RuntimeHelpers.GetObjectValue(Me.DR.Item(0))
                Loop
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsg") = exception.Message
                Dim result As PartialViewResult = Me.PartialView("_editarestacionPartial")
                ProjectData.ClearProjectError()
                Return result
                ProjectData.ClearProjectError()
            Finally
                Me.DA.Dispose()
            End Try
            Return Me.PartialView("_estacionesMainPartial")
        End Function

        <HttpGet, Authorize> _
        Public Function editarestacion(ByVal model As EstacionModel) As PartialViewResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim model2 As New EstacionesModel
            Try
                Dim model3 As EstacionesModel
                Me.DR = Me.DA.ExecuteSP("in_station_getInfo", New Object() {"DET", model.Nombre})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA.LastErrorMessage
                    Me.DA.Dispose()
                    Return Me.PartialView("_estacionesMainPartial")
                End If
                If Me.DR.HasRows Then
                    Do While Me.DR.Read
                        model3 = New EstacionesModel With { _
                            .Nombre = DR(0)), _
                            .Descripcion = DR(1)), _
                            .Usuario = DR(2)), _
                            .Proceso = DR(3)) _
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
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsg") = exception.Message
                Dim result As PartialViewResult = Me.PartialView("_estacionesMainPartial")
                ProjectData.ClearProjectError()
                Return result
                ProjectData.ClearProjectError()
            End Try
            If (Not Me.DR.IsClosed And Me.DR.HasRows) Then
                Me.DR.Close()
            End If
            Me.DA.Dispose()
            Me.TempData.Item("Model") = model2
            Return Me.PartialView("_editarestacionPartialForm")
        End Function

        <Authorize> _
        Public Function editarmaterial(ByVal model As MaterialMasterListModel) As ActionResult
            Me.TempData.Item("SKUNo") = model.SKUNo
            Me.TempData.Item("PartNo") = model.PartNo
            Me.TempData.Item("Description") = model.Description
            Me.TempData.Item("Alt") = model.Alt
            Return Me.View
        End Function

        <Authorize, HttpPost> _
        Public Function editarmaterial(ByVal model As MaterialMasterModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As MaterialMasterListModel() = New MaterialMasterListModel(2 - 1) {}
            Try
                Me.DR = Me.DA.ExecuteSP("in_EditMaterial", New Object() {model.SKUNo, model.PartNo, model.Lvl, model.Description, model.Alt})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Return Me.View
                End If
                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = RuntimeHelpers.GetObjectValue(Me.DR.Item(0))
                Loop
                result = Me.RedirectToAction("editar_material")
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

        <Authorize> _
        Public Function estaciones() As ActionResult
            Return Me.View
        End Function

        Public Function Index() As ActionResult
            Return Me.View
        End Function

        <Authorize> _
        Public Function main_estacion() As PartialViewResult
            Return Me.PartialView("_estacionesMainPartial")
        End Function

        <Authorize> _
        Public Function material_master(ByVal m As MaterialMasterModel) As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As MaterialMasterListModel() = New MaterialMasterListModel(2 - 1) {}
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("in_getMaterialList", New Object() {m.SKUNo, m.PartNo, m.Description, m.Alt})
                If (Me.DA._LastErrorMessage = "") Then
                    Dim model As MaterialMasterListModel
                    If Me.DR.HasRows Then
                        Do While Me.DR.Read
                            model = New MaterialMasterListModel With { _
                                .SKUNo = DR(0)), _
                                .PartNo = DR(1)), _
                                .Description = DR(3)), _
                                .Alt = DR(4)) _
                            }
                            modelArray(index) = model
                            index += 1
                            modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New MaterialMasterListModel(((index + 1) + 1) - 1) {}), MaterialMasterListModel())
                        Loop
                        modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New MaterialMasterListModel(((index - 1) + 1) - 1) {}), MaterialMasterListModel())
                        Me.TempData.Item("model") = modelArray
                    Else
                        model = New MaterialMasterListModel With { _
                            .SKUNo = "no data", _
                            .PartNo = "no data", _
                            .Description = "no data", _
                            .Alt = "no data" _
                        }
                        modelArray(index) = model
                        index += 1
                        modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New MaterialMasterListModel(((index - 1) + 1) - 1) {}), MaterialMasterListModel())
                        Me.TempData.Item("Model") = modelArray
                    End If
                Else
                    modelArray(index) = New MaterialMasterListModel With { _
                        .SKUNo = "no data", _
                        .PartNo = "no data", _
                        .Description = "no data", _
                        .Alt = "no data" _
                    }
                    index += 1
                    modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New MaterialMasterListModel(((index - 1) + 1) - 1) {}), MaterialMasterListModel())
                    Me.TempData.Item("Model") = modelArray
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsn") = exception.Message
                ProjectData.ClearProjectError()
            Finally
                Me.DA.Dispose()
            End Try
            Return Me.View
        End Function

        <Authorize> _
        Public Function quitar_material() As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As MaterialMasterListModel() = New MaterialMasterListModel(2 - 1) {}
            Dim index As Integer = 0
            Try
                Me.DR = Me.DA.ExecuteSP("in_getMaterialList", New Object(0 - 1) {})
                If (Me.DA._LastErrorMessage = "") Then
                    Dim model As MaterialMasterListModel
                    If Me.DR.HasRows Then
                        Do While Me.DR.Read
                            model = New MaterialMasterListModel With { _
                                .SKUNo = DR(0)), _
                                .PartNo = DR(1)), _
                                .Description = DR(3)), _
                                .Alt = DR(4)) _
                            }
                            modelArray(index) = model
                            index += 1
                            modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New MaterialMasterListModel(((index + 1) + 1) - 1) {}), MaterialMasterListModel())
                        Loop
                        modelArray = DirectCast(Utils.CopyArray(DirectCast(modelArray, Array), New MaterialMasterListModel(((index - 1) + 1) - 1) {}), MaterialMasterListModel())
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
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.TempData.Item("ErrMsn") = exception.Message
                ProjectData.ClearProjectError()
            Finally
                Me.DA.Dispose()
            End Try
            Return Me.View
        End Function

        <Authorize, HttpPost> _
        Public Function quitar_material(ByVal model As MaterialMasterModel) As ActionResult
            Dim result As ActionResult
            Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
            Dim modelArray As MaterialMasterListModel() = New MaterialMasterListModel(2 - 1) {}
            Try
                Me.DR = Me.DA.ExecuteSP("in_DelMaterial", New Object() {model.SKUNo, model.PartNo})
                If (Me.DA._LastErrorMessage <> "") Then
                    Me.TempData.Item("ErrMsg") = Me.DA._LastErrorMessage
                    Return Me.View
                End If
                Do While Me.DR.Read
                    Me.TempData.Item("ErrMsg") = RuntimeHelpers.GetObjectValue(Me.DR.Item(0))
                Loop
                result = Me.RedirectToAction("quitar_material")
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


        ' Fields
        Protected Friend DA As DataAccess
        Protected Friend DR As SqlDataReader
    End Class


End Namespace
