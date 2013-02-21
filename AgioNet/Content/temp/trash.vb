Imports System.Data.SqlClient

Module trash
    ' 2013.02.13
    ' GET: /diagnostico/cancelar_prueba
    '<Authorize> _
    'Public Function cancelar_prueba() As ActionResult
    '    Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
    'Dim modelArray(100) As TestListModel
    'Dim index As Integer = 0
    'If Me.HasOrderID Then
    '    Try
    'Dim model As TestListModel
    '   Dim reader As SqlDataReader = DA.ExecuteSP("dg_GetTestListByOrder", Session.Item("OrderID"), "")
    '        If reader.HasRows Then
    '           Do While reader.Read
    '              model = New TestListModel With { _
    '                 .TESTID = reader(0), _
    '                .ORDERID = reader(1), _
    '               .TESTNAME = reader(2), _
    '              .TESTDESCRIPTION = reader(3), _
    '             .TESTRESULT = reader(4), _
    '            .TESTSTART = reader(5), _
    '           .TESTEND = reader(6), _
    '          .CREATEBY = reader(7) _
    '     }
    '    modelArray(index) = model
    '   index += 1
    'Loop
    ' -- Actualizado por CarlosB
    'ReDim Preserve modelArray(index - 1)
    'Else
    '   model = New TestListModel With { _
    '                    .TESTID = "NO DATA", _
    '                   .ORDERID = "NO DATA", _
    '                  .TESTNAME = "NO DATA", _
    '                 .TESTDESCRIPTION = "NO DATA", _
    '                .TESTRESULT = "NO DATA", _
    '               .TESTSTART = "NO DATA", _
    '              .TESTEND = "NO DATA", _
    '             .CREATEBY = "NO DATA" _
    '        }
    '             modelArray(index) = model
    '            index += 1
    ' -- Actualizado por CarlosB
    '            ReDim Preserve modelArray(index - 1)
    '        End If
    '    Catch exception1 As Exception
    '        modelArray(index) = New TestListModel With { _
    '            .TESTID = "NO DATA", _
    '            .ORDERID = "NO DATA", _
    '           .TESTNAME = "NO DATA", _
    '            .TESTDESCRIPTION = "NO DATA", _
    '            .TESTRESULT = "NO DATA", _
    '            .TESTSTART = "NO DATA", _
    '            .TESTEND = "NO DATA", _
    '            .CREATEBY = "NO DATA" _
    '        }
    '        index += 1
    ' -- Actualizado por CarlosB
    '       ReDim Preserve modelArray(index - 1)
    '    Me.DA.Dispose()
    '     Return View()
    '   End Try
    '  Me.TempData.Item("Model") = modelArray
    '   Return Me.View
    '     End If
    'Return Me.RedirectToAction("Index")
    '    End Function

    '----------------------------------------------------------------------------------------------------------------------------
    ' NOT USED -- ' NOT USED -- ' NOT USED -- ' NOT USED -- ' NOT USED -- ' NOT USED -- ' NOT USED -- ' NOT USED -- ' NOT USED -- ' NOT USED -- ' NOT USED -- 
    '----------------------------------------------------------------------------------------------------------------------------
    ' NOT USED --     ' 2013.02.13
    ' NOT USED --     ' GET: /diagnostico/ver_prueba
    ' NOT USED --     <Authorize> _
    ' NOT USED --     Public Function ver_prueba() As ActionResult
    ' NOT USED --         Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
    ' NOT USED --     Dim modelArray(100) As TestListModel
    ' NOT USED --     Dim index As Integer = 0
    ' NOT USED -- 
    ' NOT USED --         If Me.HasOrderID Then
    ' NOT USED --             Try
    ' NOT USED --     Dim model As TestListModel
    ' NOT USED --     Dim reader As SqlDataReader = Me.DA.ExecuteSP("dg_GetTestListByOrder", Me.Session.Item("OrderID"), "")
    ' NOT USED --                 If reader.HasRows Then
    ' NOT USED --                     Do While reader.Read
    ' NOT USED --                         model = New TestListModel With { _
    ' NOT USED --                             .TESTID = reader(0), _
    ' NOT USED --                             .ORDERID = reader(1), _
    ' NOT USED --                             .TESTNAME = reader(2), _
    ' NOT USED --                             .TESTDESCRIPTION = reader(3), _
    ' NOT USED --                             .TESTRESULT = reader(4), _
    ' NOT USED --                             .TESTSTART = reader(5), _
    ' NOT USED --                             .TESTEND = reader(6), _
    ' NOT USED --                             .CREATEBY = reader(7) _
    ' NOT USED --                         }
    ' NOT USED --                         modelArray(index) = model
    ' NOT USED --                         index += 1
    ' NOT USED --                     Loop
    ' NOT USED --     '-- Actualizado por Carlos Barreto
    ' NOT USED --                     ReDim Preserve modelArray(index - 1)
    ' NOT USED --                 Else
    ' NOT USED --                     model = New TestListModel With { _
    ' NOT USED --                         .TESTID = "NO DATA", _
    ' NOT USED --                         .ORDERID = "NO DATA", _
    ' NOT USED --                         .TESTNAME = "NO DATA", _
    ' NOT USED --                         .TESTDESCRIPTION = "NO DATA", _
    ' NOT USED --                         .TESTRESULT = "NO DATA", _
    ' NOT USED --                         .TESTSTART = "NO DATA", _
    ' NOT USED --                         .TESTEND = "NO DATA", _
    ' NOT USED --                         .CREATEBY = "NO DATA" _
    ' NOT USED --                     }
    ' NOT USED --                     modelArray(index) = model
    ' NOT USED --                     index += 1
    ' NOT USED --     '-- Actualizado por Carlos Barreto
    ' NOT USED --                     ReDim Preserve modelArray(index - 1)
    ' NOT USED --                 End If
    ' NOT USED --             Catch exception1 As Exception
    ' NOT USED -- 
    ' NOT USED --                 modelArray(index) = New TestListModel With { _
    ' NOT USED --                     .TESTID = "NO DATA", _
    ' NOT USED --                     .ORDERID = "NO DATA", _
    ' NOT USED --                     .TESTNAME = "NO DATA", _
    ' NOT USED --                     .TESTDESCRIPTION = "NO DATA", _
    ' NOT USED --                     .TESTRESULT = "NO DATA", _
    ' NOT USED --                     .TESTSTART = "NO DATA", _
    ' NOT USED --                     .TESTEND = "NO DATA", _
    ' NOT USED --                     .CREATEBY = "NO DATA" _
    ' NOT USED --                 }
    ' NOT USED --                 index += 1
    ' NOT USED --     '-- Actualizado por Carlos Barreto
    ' NOT USED --                 ReDim Preserve modelArray(index - 1)
    ' NOT USED --                 Me.DA.Dispose()
    ' NOT USED --                 Return View()
    ' NOT USED --             End Try
    ' NOT USED --             Me.TempData.Item("Model") = modelArray
    ' NOT USED --             Return Me.View
    ' NOT USED --         End If
    ' NOT USED --         Return Me.RedirectToAction("Index")
    ' NOT USED --     End Function

End Module 'Module
