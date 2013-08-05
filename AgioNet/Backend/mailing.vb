Imports System.Data
Imports System.Data.SqlClient

Public Class mailing
    Public Shared _ErrorMess As String
    Public Shared _IsApproval As String

    Public Property ErrorMess As String
        Get
            Return _ErrorMess
        End Get
        Set(value As String)
            _ErrorMess = value
        End Set
    End Property

    Public Property IsApproval As String
        Get
            Return _IsApproval
        End Get
        Set(value As String)
            _IsApproval = value
        End Set
    End Property

    '--------------
    Public Structure DiagnosticMailInfo
        Public Marca As String
        Public Modelo As String
        Public SKUNO As String
        Public Descripcion As String
        Public Serie As String
        Public FallaReportada As String
        Public RetroAlimentacion As String
        Public ComentarioTecnico As String
        Public Comentario As String
        Public FechaCompromiso As String
    End Structure

    Public Shared Function getDiagnosticEmail(ByVal OrderID As String) As String
        'Iniciar una conexión a la Base de datos para obtener la información requerida
        Dim DA As New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
        Dim DR As SqlDataReader
        Dim strMail As New StringBuilder
        Dim mailInfo As New DiagnosticMailInfo

        Try
            DR = DA.ExecuteSP("dg_getDiagnosticMailInfo", OrderID)
            If DA._LastErrorMessage <> "" Then
                Throw New Exception(DA._LastErrorMessage)
            End If

            If DR.HasRows Then
                While DR.Read
                    With mailInfo
                        .Marca = DR(1)
                        .Modelo = DR(2)
                        .SKUNO = DR(3)
                        .Descripcion = DR(4)
                        .Serie = DR(5)
                        .FallaReportada = DR(6)
                        .RetroAlimentacion = DR(7)
                        .ComentarioTecnico = DR(8)
                    End With
                End While
            Else
                Throw New Exception("Error! No se han encontrado datos")
            End If


            strMail.Append("<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml2/DTD/xhtml1-strict.dtd"">")
            strMail.Append("<html> <head>")
            strMail.Append("<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />")
            strMail.Append("<meta http-equiv=""Author"" content=""Agiotech-Agiofix"" />")
            strMail.Append("<title>Solicitud de cotización para la orden " & OrderID & "</title>")
            strMail.Append("<style type=""text/css"">")
            strMail.Append("body { color:#333; font-family: Arial, Helvetica, sans-serif; font-size: 12px; }")
            strMail.Append(".titulo-mensaje { font-size: 1.4em; font-weight:bold; }")
            strMail.Append("</style>")
            strMail.Append("</head> <body>")
            strMail.Append("<table width=""650"" height=""auto"" border=""0"" cellpadding=""0"" cellspacing=""0""> ")
            strMail.Append("<tr height=""20""> <td width=""20""> &nbsp;  </td> <td width=""20""> &nbsp;  </td> <td width=""20""> &nbsp;  </td> <td width=""100"">&nbsp;  </td> <td width=""20""> &nbsp;  </td> <td width=""450"">&nbsp; </td> <td width=""20""> &nbsp;  </td> </tr>")
            strMail.Append("<tr height=""100"">")
            strMail.Append("<td>&nbsp;  </td>")
            strMail.Append("<td colspan=""3""> <img src=""http://www.agiotech.com/_/img/menu/logo-01.png"" alt=""Logo Agiotech"" /> </td>")
            strMail.Append("<td>&nbsp;  </td>")
            strMail.Append("<td class=""titulo-mensaje"">Solicitud de cotización para la orden " & OrderID & "</td>")
            strMail.Append("<td>&nbsp;  </td>")
            strMail.Append("</tr>")
            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("<tr height=""60"">")
            strMail.Append("    <td width=""20"">&nbsp;  </td>")
            strMail.Append("    <td colspan=""5"">Se ha registrado una solicitud de cotización de parte para la orden: <b> " & OrderID & "</b>. Esta opción se encuentra disponible desde <i><b>Servicio a Clientes / 3.- Cotización de Partes</b></i>")
            strMail.Append("        <br /><br />")
            strMail.Append("        Los datos de la Orden son:")
            strMail.Append("    </td>")
            strMail.Append("    <td width=""20"">&nbsp;  </td>")
            strMail.Append("</tr>")

            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("<tr>")
            strMail.Append("    <td colspan=""3""> &nbsp; </td>")
            strMail.Append("    <td><b>MARCA:</b> </td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("    <td>" & mailInfo.Marca & "</td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("</tr>")

            strMail.Append("<tr>")
            strMail.Append("    <td colspan=""3""> &nbsp; </td>")
            strMail.Append("    <td><b>MODELO: </b></td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("    <td>" & mailInfo.Modelo & "</td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("</tr>")

            strMail.Append("<tr>")
            strMail.Append("    <td colspan=""3""> &nbsp; </td>")
            strMail.Append("    <td><b>SKU:</b></td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("    <td>" & mailInfo.SKUNO & "</td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("</tr>")

            strMail.Append("<tr>")
            strMail.Append("    <td colspan=""3""> &nbsp; </td>")
            strMail.Append("    <td><b>DESCRIPCIÓN: </b>: </td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("    <td>" & mailInfo.Descripcion & "</td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("</tr>")

            strMail.Append("<tr>")
            strMail.Append("    <td colspan=""3""> &nbsp; </td>")
            strMail.Append("    <td><b>No SERIE:</b>: </td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("    <td>" & mailInfo.Serie & "</td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("</tr>")

            strMail.Append("<tr>")
            strMail.Append("    <td colspan=""3""> &nbsp; </td>")
            strMail.Append("    <td><b>FALLA REPORTADA</b>: </td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("    <td>" & mailInfo.FallaReportada & "</td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("</tr>")

            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("<tr>")
            strMail.Append("    <td colspan=""2""> &nbsp; </td>")
            strMail.Append("    <td colspan=""2""><b>Retroalimentación a Falla </b></td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("    <td>" & mailInfo.RetroAlimentacion & "</td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("</tr>")

            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("<tr>")
            strMail.Append("    <td colspan=""2""> &nbsp; </td>")
            strMail.Append("    <td colspan=""2""><b>Retro Atención a Clientes</b></td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("    <td>" & mailInfo.ComentarioTecnico & "</td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("</tr>")

            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("</table> ")
            strMail.Append("</body>")
            strMail.Append("</html>")


        Catch ex As Exception
            strMail.Clear()
            strMail.Append(ex.Message)
            _ErrorMess = ex.Message
        Finally
            DA.Dispose()
        End Try

        Return strMail.ToString
    End Function

    Public Shared Function getDistributionListReqApproval(ByVal User As String, ByVal OrderID As String) As String
        Dim strList As New StringBuilder
        Dim DA As New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
        Dim DR As SqlDataReader
        Try
            DR = DA.ExecuteSP("sys_getEmailUser", User)
            If DR.HasRows Then
                While DR.Read
                    strList.Append(DR(0) & ", ")
                End While
            End If
            If Not DR.IsClosed Then DR.Close()

            DR = DA.ExecuteSP("sys_getEmailSection", "Supervisor")
            If DR.HasRows Then
                While DR.Read
                    strList.Append(DR(0) & ", ")
                End While
            End If
            If Not DR.IsClosed Then DR.Close()

            DR = DA.ExecuteSP("sys_getEmailSection", "Empaque")
            If DR.HasRows Then
                While DR.Read
                    strList.Append(DR(0) & ", ")
                End While
            End If
            If Not DR.IsClosed Then DR.Close()

            DR = DA.ExecuteSP("sys_getEmailSection", "Compras")
            If DR.HasRows Then
                While DR.Read
                    strList.Append(DR(0) & ", ")
                End While
            End If
            If Not DR.IsClosed Then DR.Close()

            DR = DA.ExecuteSP("sys_getEmailSection", "Administración")
            If DR.HasRows Then
                While DR.Read
                    strList.Append(DR(0) & ", ")
                End While
            End If
            If Not DR.IsClosed Then DR.Close()

            DR = DA.ExecuteSP("sys_getEmailSection", "Atención a Clientes", OrderID)
            If DR.HasRows Then
                While DR.Read
                    strList.Append(DR(0))
                End While
            End If
            If Not DR.IsClosed Then DR.Close()

        Catch ex As Exception
            strList.Clear()
            strList.Append(ex.Message)
            _ErrorMess = ex.Message
        Finally
            DA.Dispose()
        End Try

        Return strList.ToString
    End Function

    '----- Notificación de cotización
    Public Structure CotizacionMailInfo
        Public OrderID As String
        Public PartNo As String
    End Structure

    Public Shared Function getCotizacionEmail(ByVal OrderID As String) As String
        'Iniciar una conexión a la Base de datos para obtener la información requerida
        Dim DA As New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
        Dim DR As SqlDataReader
        Dim strMail As New StringBuilder
        Dim CotizacionInfo(100) As CotizacionMailInfo
        Dim index As Integer = 0
        Dim MinInfo As New MinOrderInfo

        Try
            DR = DA.ExecuteSP("sc_getCostedParts", OrderID)
            If DA._LastErrorMessage <> "" Then
                Throw New Exception(DA._LastErrorMessage)
            End If

            If DR.HasRows Then
                While DR.Read
                    CotizacionInfo(index).OrderID = DR(0)
                    CotizacionInfo(index).PartNo = DR(1)
                    index += 1
                End While
                ReDim Preserve CotizacionInfo(index - 1)
            Else
                Throw New Exception("Error! No se han encontrado datos")
            End If


            strMail.Append("<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml2/DTD/xhtml1-strict.dtd"">")
            strMail.Append("<html> <head>")
            strMail.Append("<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />")
            strMail.Append("<meta http-equiv=""Author"" content=""Agiotech-Agiofix"" />")
            strMail.Append("<title>Solicitud de cotización para la orden " & OrderID & "</title>")
            strMail.Append("<style type=""text/css"">")
            strMail.Append("body { color:#333; font-family: Arial, Helvetica, sans-serif; font-size: 12px; }")
            strMail.Append(".titulo-mensaje { font-size: 1.4em; font-weight:bold; }")
            strMail.Append("</style>")
            strMail.Append("</head> <body>")
            strMail.Append("<table width=""650"" height=""auto"" border=""0"" cellpadding=""0"" cellspacing=""0""> ")
            strMail.Append("<tr height=""20""> <td width=""20""> &nbsp;  </td> <td width=""20""> &nbsp;  </td> <td width=""20""> &nbsp;  </td> <td width=""100"">&nbsp;  </td> <td width=""20""> &nbsp;  </td> <td width=""450"">&nbsp; </td> <td width=""20""> &nbsp;  </td> </tr>")
            strMail.Append("<tr height=""100"">")
            strMail.Append("<td>&nbsp;  </td>")
            strMail.Append("<td colspan=""3""> <img src=""http://www.agiotech.com/_/img/menu/logo-01.png"" alt=""Logo Agiotech"" /> </td>")
            strMail.Append("<td>&nbsp;  </td>")
            strMail.Append("<td class=""titulo-mensaje"">La orden " & OrderID & " está lista para cotizar al cliente</td>")
            strMail.Append("<td>&nbsp;  </td>")
            strMail.Append("</tr>")
            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("<tr height=""60"">")
            strMail.Append("    <td width=""20"">&nbsp;  </td>")
            strMail.Append("    <td colspan=""5"">La orden <b>" & OrderID & " </b>está lista para cotizar al cliente. Esta opción se encuentra disponible desde <i><b>Servicio a Clientes / 4.- Cotización Cliente</b></i>")
            strMail.Append("        <br /><br />")
            strMail.Append("        Las partes costeadas son:")
            strMail.Append("    </td>")
            strMail.Append("    <td width=""20"">&nbsp;  </td>")
            strMail.Append("</tr>")
            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")

            If Not DR.IsClosed Then DR.Close()
            DR = DA.ExecuteSP("dbo.sys_getMinOrderInfo", Mid(OrderID, 1, OrderID.Length - 3) & "-01")
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
                    MinInfo.LeadTime = DR(7)
                End While
            End If

            strMail.Append("")
            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("<tr>")
            strMail.Append("    <td  colspan=""1""> &nbsp; </td>")
            strMail.Append("    <td  colspan=""4"">")
            strMail.Append("        <Table>")
            strMail.Append("            <tr>")
            strMail.Append("                <td width=""30%""><b>Orden: </b></th>")
            strMail.Append("                <td>" & MinInfo.OrderID & "</td>")
            strMail.Append("            </tr>")
            strMail.Append("            <tr>")
            strMail.Append("                <td><b>Modelo: </b></th>")
            strMail.Append("                <td>" & MinInfo.Model & "</td>")
            strMail.Append("            </tr>")
            strMail.Append("            <tr>")
            strMail.Append("                <td><b>SKU No: </b></th>")
            strMail.Append("                <td>" & MinInfo.PartNo & "</td>")
            strMail.Append("            </tr>")
            strMail.Append("            <tr>")
            strMail.Append("                <td><b>Descripción: </b></th>")
            strMail.Append("                <td>" & MinInfo.Description & "</td>")
            strMail.Append("            </tr>")
            strMail.Append("            <tr>")
            strMail.Append("                <td><b>Número de serie: </b></th>")
            strMail.Append("                <td>" & MinInfo.SerialNo & "</td>")
            strMail.Append("            </tr>")
            strMail.Append("            <tr>")
            strMail.Append("                <td><b>Falla Reportada: </b></th>")
            strMail.Append("                <td>" & MinInfo.Failure & "</td>")
            strMail.Append("            </tr>")
            'strMail.Append("            <tr>")
            'strMail.Append("                <td><b>Fecha Compromiso: </b></th>")
            'strMail.Append("                <td>" & MinInfo.LeadTime & "</td>")
            'strMail.Append("            </tr>")
            strMail.Append("            <tr>")
            strMail.Append("                <td width=""30%"">&nbsp;</th>")
            strMail.Append("                <td>&nbsp;</td>")
            strMail.Append("            </tr>")
            strMail.Append("        </Table>")
            strMail.Append("    </td>")
            strMail.Append("    <td colspan=""2"">&nbsp;</td>")
            strMail.Append("</tr>")

            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("<tr>")
            strMail.Append("    <td colspan=""3""> &nbsp; </td>")
            strMail.Append("    <td><b>ORDEN</b></td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("    <td><b>NUMERO PARTE</b></td>")
            strMail.Append("    <td>&nbsp;</td>")
            strMail.Append("</tr>")

            For Each x As CotizacionMailInfo In CotizacionInfo
                strMail.Append("<tr>")
                strMail.Append("    <td colspan=""3""> &nbsp; </td>")
                strMail.Append("    <td>" & x.OrderID & " </td>")
                strMail.Append("    <td>&nbsp;</td>")
                strMail.Append("    <td>" & x.PartNo & "</td>")
                strMail.Append("    <td>&nbsp;</td>")
                strMail.Append("</tr>")
            Next

            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("</table> ")
            strMail.Append("</body>")
            strMail.Append("</html>")


        Catch ex As Exception
            strMail.Clear()
            strMail.Append(ex.Message)
            _ErrorMess = ex.Message
        Finally
            DA.Dispose()
        End Try

        Return strMail.ToString
    End Function

    Public Shared Function getDistributionListCotizacion(ByVal OrderID As String) As String
        Dim strList As New StringBuilder
        Dim DA As New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
        Dim DR As SqlDataReader
        Try
            DR = DA.ExecuteSP("sys_getEmailSection", "Compras")
            If DR.HasRows Then
                While DR.Read
                    strList.Append(DR(0) & ", ")
                End While
            End If
            If Not DR.IsClosed Then DR.Close()

            DR = DA.ExecuteSP("sys_getEmailSection", "Administración")
            If DR.HasRows Then
                While DR.Read
                    strList.Append(DR(0) & ", ")
                End While
            End If
            If Not DR.IsClosed Then DR.Close()

            DR = DA.ExecuteSP("sys_getEmailSection", "Atención a Clientes", OrderID)
            If DR.HasRows Then
                While DR.Read
                    strList.Append(DR(0))
                End While
            End If
            If Not DR.IsClosed Then DR.Close()

        Catch ex As Exception
            strList.Clear()
            strList.Append(ex.Message)
            _ErrorMess = ex.Message
        Finally
            DA.Dispose()
        End Try

        Return strList.ToString
    End Function

    '-- Notificación de Autorización
    Public Structure AutorizacionMailInfo
        Public OrderID As String
        Public PartNo As String
        Public Descripcion As String
        Public Approval As String
        Public ApprovalBy As String
        Public ApprovalDate As String
        Public LeadTime As String
        Public TipoReparacion As String
    End Structure

    Public Structure MinOrderInfo
        Public OrderID As String
        Public Model As String
        Public PartNo As String
        Public Description As String
        Public SerialNo As String
        Public Failure As String
        Public Status As String
        Public LeadTime As String
    End Structure

    Public Shared Sub setNotificationEmailStatus(OrderID As String, Notification As String)
        Dim DA As New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
        Dim DR As SqlDataReader
        Try
            DR = DA.ExecuteSP("sys_updNotificationStatus", OrderID, Notification)
            If DA._LastErrorMessage <> "" Then
                Throw New Exception(DA._LastErrorMessage)
            End If
            _ErrorMess = ""
        Catch ex As Exception
            _ErrorMess = ex.Message
        Finally
            DA.Dispose()
        End Try
    End Sub

    Public Shared Function getAutorizacionEmail(ByVal OrderID As String) As String
        'Iniciar una conexión a la Base de datos para obtener la información requerida
        Dim DA As New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
        Dim DR As SqlDataReader
        Dim strMail As New StringBuilder
        Dim AppInfo(100) As AutorizacionMailInfo
        Dim MinInfo As New MinOrderInfo

        Dim index As Integer = 0
        Dim OrderApproval As String = String.Empty
        Try
            DR = DA.ExecuteSP("sc_getApprovalOrders", OrderID)
            If DA._LastErrorMessage <> "" Then
                Throw New Exception(DA._LastErrorMessage)
            End If

            If DR.HasRows Then
                While DR.Read
                    AppInfo(index).OrderID = DR(0)
                    AppInfo(index).PartNo = DR(1)
                    AppInfo(index).Descripcion = DR(2)
                    AppInfo(index).Approval = DR(3)
                    AppInfo(index).ApprovalBy = DR(4)
                    AppInfo(index).ApprovalDate = DR(5)
                    OrderApproval = DR(6)
                    AppInfo(index).LeadTime = DR(7)
                    AppInfo(index).TipoReparacion = DR(8)

                    index += 1
                End While
                ReDim Preserve AppInfo(index - 1)
            End If

            setNotificationEmailStatus(OrderID, "APPROVAL")
            If _ErrorMess <> "" Then
                Throw New Exception("Error! A ocurrido un error al cambiar el estatus de la notificación")
            End If

            strMail.Append("<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml2/DTD/xhtml1-strict.dtd"">")
            strMail.Append("<html> <head>")
            strMail.Append("<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />")
            strMail.Append("<meta http-equiv=""Author"" content=""Agiotech-Agiofix"" />")
            If OrderApproval = "APPROVAL" Then
                strMail.Append("<title>" & OrderID & ": Listado de partes autorizadas para comprar</title>")
            Else
                strMail.Append("<title>" & OrderID & ": NO AUTORIZADA para reparación!</title>")
            End If
            strMail.Append("<style type=""text/css"">")
            strMail.Append("body { color:#333; font-family: Arial, Helvetica, sans-serif; font-size: 12px; }")
            strMail.Append(".titulo-mensaje { font-size: 1.4em; font-weight:bold; }")
            strMail.Append(".red { color:#f80; font-size: 1em; font-weight:bold;")
            strMail.Append("</style>")
            strMail.Append("</head> <body>")
            strMail.Append("<table width=""650"" height=""auto"" border=""0"" cellpadding=""0"" cellspacing=""0""> ")
            strMail.Append("<tr height=""20""> <td width=""20""> &nbsp;  </td> <td width=""20""> &nbsp;  </td> <td width=""20""> &nbsp;  </td> <td width=""100"">&nbsp;  </td> <td width=""20""> &nbsp;  </td> <td width=""450"">&nbsp; </td> <td width=""20""> &nbsp;  </td> </tr>")
            strMail.Append("<tr height=""100"">")
            strMail.Append("<td>&nbsp;  </td>")
            strMail.Append("<td colspan=""3""> <img src=""http://www.agiotech.com/_/img/menu/logo-01.png"" alt=""Logo Agiotech"" /> </td>")
            strMail.Append("<td>&nbsp;  </td>")
            If OrderApproval = "APPROVAL" Then
                strMail.Append("<td class=""titulo-mensaje"">" & OrderID & ": Listado de partes autorizadas para comprar</td>")
            Else
                strMail.Append("<td class=""titulo-mensaje"">" & OrderID & ": NO AUTORIZADA para reparación!</td>")
            End If
            strMail.Append("<td>&nbsp;  </td>")
            strMail.Append("</tr>")
            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")

            If OrderApproval = "APPROVAL" Then
                strMail.Append("<tr height=""60"">")
                strMail.Append("    <td width=""20"">&nbsp;  </td>")
                strMail.Append("    <td colspan=""5"">La orden <b>" & OrderID & " </b> ha sido autorizada para su reparación y está lista para el proceso de compras. Ahora, está disponible en <i><b>Servicio a Clientes / 7.- Comprar Parte</b></i><br /></td>")
                strMail.Append("    <td width=""20"">&nbsp;  </td>")
                strMail.Append("</tr>")

                strMail.Append("<tr height=""60"">")
                strMail.Append("    <td width=""20"">&nbsp;  </td>")
                strMail.Append("    <td colspan=""5""><br />  Las partes autorizadas son:</td>")
                strMail.Append("    <td width=""20"">&nbsp;  </td>")
                strMail.Append("</tr>")

                strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
                strMail.Append("<tr>")
                strMail.Append("    <td  colspan=""7"">")
                strMail.Append("        <Table>")
                strMail.Append("            <tr>")
                strMail.Append("                <th>Orden</th>")
                strMail.Append("                <th>Número Parte</th>")
                strMail.Append("                <th>Descripción</th>")
                strMail.Append("                <th>Aprobación</th>")
                strMail.Append("                <th>Aprobado Por</th>")
                strMail.Append("                <th>Aprobado El</th>")
                strMail.Append("                <th>Fecha Compromiso</th>")
                strMail.Append("                <th>Tipo Reparación</th>")
                strMail.Append("            </tr>")

                For Each x As AutorizacionMailInfo In AppInfo
                    strMail.Append("            <tr>")
                    strMail.Append("                <td>" & x.OrderID & "</td>")
                    strMail.Append("                <td>" & x.PartNo & "</td>")
                    strMail.Append("                <td>" & x.Descripcion & "</td>")
                    strMail.Append("                <td>" & x.Approval & "</td>")
                    strMail.Append("                <td>" & x.ApprovalBy & "</td>")
                    strMail.Append("                <td>" & x.ApprovalDate & "</td>")
                    strMail.Append("                <td>" & x.LeadTime & "</td>")
                    strMail.Append("                <td>" & x.TipoReparacion & "</td>")
                    strMail.Append("            </tr>")
                Next

                strMail.Append("        </Table>")
                strMail.Append("    </td>")
                strMail.Append("</tr>")

                strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            Else
                strMail.Append("<tr height=""60"">")
                strMail.Append("    <td width=""20"">&nbsp;  </td>")
                strMail.Append("    <td colspan=""5"">La orden <b>" & OrderID & " </b> <span class> NO HA SIDO AUTORIZADA</span> para su reparación. Ha pasado a Calidad para continuar con su proceso. Ahora, está disponible en <i><b>Calidad / 1.- Proceso de Calidad</b></i>")
                strMail.Append("        <br /><br />")
                strMail.Append("        Los datos de la orden son:")
                strMail.Append("    </td>")
                strMail.Append("    <td width=""20"">&nbsp;  </td>")
                strMail.Append("</tr>")


                If Not DR.IsClosed Then DR.Close()
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
                        MinInfo.LeadTime = DR(7)
                    End While
                End If

                strMail.Append("")
                strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
                strMail.Append("<tr>")
                strMail.Append("    <td  colspan=""1""> &nbsp; </td>")
                strMail.Append("    <td  colspan=""4"">")
                strMail.Append("        <Table>")
                strMail.Append("            <tr>")
                strMail.Append("                <td width=""30%""><b>Orden: </b></th>")
                strMail.Append("                <td>" & MinInfo.OrderID & "</td>")
                strMail.Append("            </tr>")
                strMail.Append("            <tr>")
                strMail.Append("                <td><b>Modelo: </b></th>")
                strMail.Append("                <td>" & MinInfo.Model & "</td>")
                strMail.Append("            </tr>")
                strMail.Append("            <tr>")
                strMail.Append("                <td><b>SKU No: </b></th>")
                strMail.Append("                <td>" & MinInfo.PartNo & "</td>")
                strMail.Append("            </tr>")
                strMail.Append("            <tr>")
                strMail.Append("                <td><b>Descripción: </b></th>")
                strMail.Append("                <td>" & MinInfo.Description & "</td>")
                strMail.Append("            </tr>")
                strMail.Append("            <tr>")
                strMail.Append("                <td><b>Número de serie: </b></th>")
                strMail.Append("                <td>" & MinInfo.SerialNo & "</td>")
                strMail.Append("            </tr>")
                strMail.Append("            <tr>")
                strMail.Append("                <td><b>Falla Reportada: </b></th>")
                strMail.Append("                <td>" & MinInfo.Failure & "</td>")
                strMail.Append("            </tr>")
                strMail.Append("            <tr>")
                strMail.Append("                <td><b>Fecha Compromiso: </b></th>")
                strMail.Append("                <td>" & MinInfo.LeadTime & "</td>")
                strMail.Append("            </tr>")
                strMail.Append("            <tr>")
                strMail.Append("                <td width=""30%"">&nbsp;</th>")
                strMail.Append("                <td>&nbsp;</td>")
                strMail.Append("            </tr>")
                strMail.Append("        </Table>")
                strMail.Append("    </td>")
                strMail.Append("    <td colspan=""2"">&nbsp;</td>")
                strMail.Append("</tr>")
            End If

            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("</table> ")
            strMail.Append("</body>")
            strMail.Append("</html>")


        Catch ex As Exception
            strMail.Clear()
            strMail.Append(ex.Message)
            _ErrorMess = ex.Message
        Finally
            DA.Dispose()
        End Try

        Return strMail.ToString
    End Function

    Public Shared Function getDistributionListAutorizacion(ByVal txt As String, user As String, OrderID As String) As String
        Dim strList As New StringBuilder
        Dim DA As New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
        Dim DR As SqlDataReader
        Try
            If txt = "APPROVAL" Then
                DR = DA.ExecuteSP("sys_getEmailSection", "Compras")
                If DR.HasRows Then
                    While DR.Read
                        strList.Append(DR(0) & ", ")
                    End While
                End If
                If Not DR.IsClosed Then DR.Close()

            Else
                DR = DA.ExecuteSP("sys_getEmailSection", "Tecnico", OrderID)
                If DR.HasRows Then
                    While DR.Read
                        strList.Append(DR(0) & ", ")
                    End While
                End If
                If Not DR.IsClosed Then DR.Close()

                DR = DA.ExecuteSP("sys_getEmailSection", "Supervisor")
                If DR.HasRows Then
                    While DR.Read
                        strList.Append(DR(0) & ", ")
                    End While
                End If
                If Not DR.IsClosed Then DR.Close()

                DR = DA.ExecuteSP("sys_getEmailUser", user)
                If DR.HasRows Then
                    While DR.Read
                        strList.Append(DR(0) & ", ")
                    End While
                End If
                If Not DR.IsClosed Then DR.Close()
            End If

            DR = DA.ExecuteSP("sys_getEmailSection", "Atención a Clientes", OrderID)
            If DR.HasRows Then
                While DR.Read
                    strList.Append(DR(0) & ", ")
                End While
            End If
            If Not DR.IsClosed Then DR.Close()

            DR = DA.ExecuteSP("sys_getEmailSection", "Administración")
            If DR.HasRows Then
                While DR.Read
                    strList.Append(DR(0) & ", ")
                End While
            End If
            If Not DR.IsClosed Then DR.Close()

            ' si es el ultimo registro, quitar la coma
            strList.Remove(strList.Length - 2, 2)
        Catch ex As Exception
            strList.Clear()
            strList.Append(ex.Message)
            _ErrorMess = ex.Message
        Finally
            DA.Dispose()
        End Try

        Return strList.ToString
    End Function

    Public Shared Function getTituloAutorizacion(OrderID As String, Notification As String) As String
        Dim DA As New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
        Dim DR As SqlDataReader
        Try
            DR = DA.ExecuteSP("sys_getNotificationTitle", OrderID, Notification)
            If DA._LastErrorMessage <> "" Then
                Throw New Exception(DA._LastErrorMessage)
            Else
                While DR.Read
                    _ErrorMess = DR(0)
                    _IsApproval = DR(1)
                End While
            End If

            '_ErrorMess = ""
        Catch ex As Exception
            _ErrorMess = ex.Message
        Finally
            DA.Dispose()
        End Try

        Return _ErrorMess
    End Function

    '--- Notificación para compras
    Public Structure ComprasMailInfo
        Public OrderID As String
        Public PartNo As String
        Public Descripcion As String
        Public LeadTime As String
        Public Comentario As String
        Public FechaCompromiso As String
        Public TipoReparacion As String

    End Structure

    Public Shared Function getComprasEmail(ByVal OrderID As String) As String
        'Iniciar una conexión a la Base de datos para obtener la información requerida
        Dim DA As New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
        Dim DR As SqlDataReader
        Dim strMail As New StringBuilder
        Dim AppInfo(100) As ComprasMailInfo
        Dim MinInfo As New MinOrderInfo

        Dim index As Integer = 0
        Dim OrderApproval As String = String.Empty
        Try
            DR = DA.ExecuteSP("sc_getPurchasedOrders", OrderID)
            If DA._LastErrorMessage <> "" Then
                Throw New Exception(DA._LastErrorMessage)
            End If

            If DR.HasRows Then
                While DR.Read
                    AppInfo(index).OrderID = DR(0)
                    AppInfo(index).PartNo = DR(1)
                    AppInfo(index).Descripcion = DR(2)
                    AppInfo(index).LeadTime = DR(3)
                    AppInfo(index).Comentario = DR(4)
                    AppInfo(index).FechaCompromiso = DR(5)
                    AppInfo(index).TipoReparacion = DR(6)
                    index += 1
                End While
                ReDim Preserve AppInfo(index - 1)

                setNotificationEmailStatus(OrderID, "PURCHASED")
                If _ErrorMess <> "" Then
                    Throw New Exception("Error! A ocurrido un error al cambiar el estatus de la notificación")
                End If
            Else
                Throw New Exception("Error! No se han encontrado datos")
            End If

            strMail.Append("<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml2/DTD/xhtml1-strict.dtd"">")
            strMail.Append("<html> <head>")
            strMail.Append("<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />")
            strMail.Append("<meta http-equiv=""Author"" content=""Agiotech-Agiofix"" />")
            strMail.Append("<title>" & OrderID & ": AUTORIZADA Para su reparación</title>")
            strMail.Append("<style type=""text/css"">")
            strMail.Append("body { color:#333; font-family: Arial, Helvetica, sans-serif; font-size: 12px; }")
            strMail.Append(".titulo-mensaje { font-size: 1.4em; font-weight:bold; }")
            strMail.Append(".red { color:#f80; font-size: 1em; font-weight:bold; }")
            strMail.Append("</style>")
            strMail.Append("</head> <body>")
            strMail.Append("<table width=""650"" height=""auto"" border=""0"" cellpadding=""0"" cellspacing=""0""> ")
            strMail.Append("<tr height=""20""> <td width=""20""> &nbsp;  </td> <td width=""20""> &nbsp;  </td> <td width=""20""> &nbsp;  </td> <td width=""100"">&nbsp;  </td> <td width=""20""> &nbsp;  </td> <td width=""450"">&nbsp; </td> <td width=""20""> &nbsp;  </td> </tr>")
            strMail.Append("<tr height=""100"">")
            strMail.Append("<td>&nbsp;  </td>")
            strMail.Append("<td colspan=""3""> <img src=""http://www.agiotech.com/_/img/menu/logo-01.png"" alt=""Logo Agiotech"" /> </td>")
            strMail.Append("<td width=""20"">&nbsp;  </td>")
            strMail.Append("<td class=""titulo-mensaje"">" & OrderID & ": AUTORIZADA Para su reparación</td>")
            strMail.Append("<td>&nbsp;  </td>")
            strMail.Append("</tr>")
            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")

            strMail.Append("<tr height=""60"">")
            strMail.Append("    <td width=""20"">&nbsp;  </td>")
            strMail.Append("    <td colspan=""5"">La orden <b>" & OrderID & " </b> ha concluido el proceso de autorización y está disponible para su reparación.")
            strMail.Append("        <br /><br />")
            strMail.Append("        Los datos de la orden son:")
            strMail.Append("    </td>")
            strMail.Append("    <td width=""20"">&nbsp;  </td>")
            strMail.Append("</tr>")

            If Not DR.IsClosed Then DR.Close()
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
                End While
            End If

            strMail.Append("")
            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>" & vbNewLine)
            strMail.Append("<tr>" & vbNewLine)
            strMail.Append("    <td  colspan=""1""> &nbsp; </td>" & vbNewLine)
            strMail.Append("    <td  colspan=""4"">" & vbNewLine)
            strMail.Append("        <Table>" & vbNewLine)
            strMail.Append("            <tr>" & vbNewLine)
            strMail.Append("                <td width=""30%""><b>Orden: </b></td>" & vbNewLine)
            strMail.Append("                <td>" & MinInfo.OrderID & "</td>" & vbNewLine)
            strMail.Append("            </tr>" & vbNewLine)
            strMail.Append("            <tr>" & vbNewLine)
            strMail.Append("                <td><b>Modelo: </b></td>" & vbNewLine)
            strMail.Append("                <td>" & MinInfo.Model & "</td>" & vbNewLine)
            strMail.Append("            </tr>" & vbNewLine)
            strMail.Append("            <tr>" & vbNewLine)
            strMail.Append("                <td><b>Numero de Parte: </b></td>" & vbNewLine)
            strMail.Append("                <td>" & MinInfo.PartNo & "</td>" & vbNewLine)
            strMail.Append("            </tr>" & vbNewLine)
            strMail.Append("            <tr>" & vbNewLine)
            strMail.Append("                <td><b>Descripción: </b></td>" & vbNewLine)
            strMail.Append("                <td>" & MinInfo.Description & "</td>" & vbNewLine)
            strMail.Append("            </tr>" & vbNewLine)
            strMail.Append("            <tr>" & vbNewLine)
            strMail.Append("                <td><b>Número de serie: </b></td>" & vbNewLine)
            strMail.Append("                <td>" & MinInfo.SerialNo & "</td>" & vbNewLine)
            strMail.Append("            </tr>" & vbNewLine)
            strMail.Append("            <tr>" & vbNewLine)
            strMail.Append("                <td><b>Falla Reportada: </b></td>" & vbNewLine)
            strMail.Append("                <td>" & MinInfo.Failure & "</td>" & vbNewLine)
            strMail.Append("            </tr>" & vbNewLine)
            strMail.Append("            <tr>" & vbNewLine)
            strMail.Append("                <td>&nbsp;</td>" & vbNewLine)
            strMail.Append("                <td>&nbsp;</td>" & vbNewLine)
            strMail.Append("            </tr>" & vbNewLine)
            strMail.Append("        </Table>" & vbNewLine)
            strMail.Append("    </td>" & vbNewLine)
            strMail.Append("    <td colspan=""2"">&nbsp;</td>" & vbNewLine)
            strMail.Append("</tr>" & vbNewLine)

            strMail.Append("<tr height=""60"">")
            strMail.Append("    <td width=""20"">&nbsp;  </td>")
            strMail.Append("    <td colspan=""5""><br />  Las partes autorizadas son:</td>")
            strMail.Append("    <td width=""20"">&nbsp;  </td>")
            strMail.Append("</tr>")

            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("<tr>")
            strMail.Append("    <td  colspan=""7"">")
            strMail.Append("        <Table>")
            strMail.Append("            <tr>")
            strMail.Append("                <th>Orden</th>")
            strMail.Append("                <th>Número Parte</th>")
            strMail.Append("                <th>Descripción</th>")
            strMail.Append("                <th>Fecha de entrega</th>")
            strMail.Append("                <th>Comentario Compras</th>")
            strMail.Append("                <th>Fecha Compromiso</th>")
            strMail.Append("                <th>Tipo Reparación</th>")
            strMail.Append("            </tr>")

            For Each x As ComprasMailInfo In AppInfo
                strMail.Append("            <tr>")
                strMail.Append("                <td>" & x.OrderID & "</td>")
                strMail.Append("                <td>" & x.PartNo & "</td>")
                strMail.Append("                <td>" & x.Descripcion & "</td>")
                strMail.Append("                <td>" & x.LeadTime & "</td>")
                strMail.Append("                <td>" & x.Comentario & "</td>")
                strMail.Append("                <td>" & x.FechaCompromiso & "</td>")
                strMail.Append("                <td>" & x.TipoReparacion & "</td>")
                strMail.Append("            </tr>")
            Next

            strMail.Append("        </Table>")
            strMail.Append("    </td>")
            strMail.Append("</tr>")


            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("</table> ")
            strMail.Append("</body>")
            strMail.Append("</html>")


        Catch ex As Exception
            strMail.Clear()
            strMail.Append(ex.Message)
            _ErrorMess = ex.Message
        Finally
            DA.Dispose()
        End Try

        Return strMail.ToString
    End Function

    Public Shared Function getDistributionListCompras(ByVal user As String, OrderID As String) As String
        Dim strList As New StringBuilder
        Dim DA As New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
        Dim DR As SqlDataReader
        Try
            DR = DA.ExecuteSP("sys_getEmailSection", "Tecnico", OrderID)
            If DR.HasRows Then
                While DR.Read
                    strList.Append(DR(0) & ", ")
                End While
            End If
            If Not DR.IsClosed Then DR.Close()

            DR = DA.ExecuteSP("sys_getEmailSection", "Supervisor")
            If DR.HasRows Then
                While DR.Read
                    strList.Append(DR(0) & ", ")
                End While
            End If
            If Not DR.IsClosed Then DR.Close()

            DR = DA.ExecuteSP("sys_getEmailUser", user)
            If DR.HasRows Then
                While DR.Read
                    strList.Append(DR(0) & ", ")
                End While
            End If
            If Not DR.IsClosed Then DR.Close()

            DR = DA.ExecuteSP("sys_getEmailSection", "Atención a Clientes", OrderID)
            If DR.HasRows Then
                While DR.Read
                    strList.Append(DR(0) & ", ")
                End While
            End If
            If Not DR.IsClosed Then DR.Close()

            DR = DA.ExecuteSP("sys_getEmailSection", "Administración")
            If DR.HasRows Then
                While DR.Read
                    strList.Append(DR(0) & ", ")
                End While
            End If
            If Not DR.IsClosed Then DR.Close()

            ' si es el ultimo registro, quitar la coma
            strList.Remove(strList.Length - 2, 2)
        Catch ex As Exception
            strList.Clear()
            strList.Append(ex.Message)
            _ErrorMess = ex.Message
        Finally
            DA.Dispose()
        End Try

        Return strList.ToString
    End Function

    '--- Notificación para recibo producto terminado
    Public Shared Function getReciboEmail(ByVal OrderID As String) As String
        'Iniciar una conexión a la Base de datos para obtener la información requerida
        Dim DA As New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
        Dim DR As SqlDataReader
        Dim strMail As New StringBuilder
        Dim MinInfo As New MinOrderInfo

        Dim index As Integer = 0
        Dim OrderApproval As String = String.Empty
        Try
            DR = DA.ExecuteSP("pk_getReceivedOrders", OrderID)
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
                End While
            Else
                Throw New Exception("Error! No se han encontrado datos")
            End If

            strMail.Append("<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml2/DTD/xhtml1-strict.dtd"">")
            strMail.Append("<html> <head>")
            strMail.Append("<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />")
            strMail.Append("<meta http-equiv=""Author"" content=""Agiotech-Agiofix"" />")
            strMail.Append("<title>" & OrderID & ": AUTORIZADA Para su reparación</title>")
            strMail.Append("<style type=""text/css"">")
            strMail.Append("body { color:#333; font-family: Arial, Helvetica, sans-serif; font-size: 12px; }")
            strMail.Append(".titulo-mensaje { font-size: 1.4em; font-weight:bold; }")
            strMail.Append(".red { color:#f80; font-size: 1em; font-weight:bold; }")
            strMail.Append("</style>")
            strMail.Append("</head> <body>")
            strMail.Append("<table width=""650"" height=""auto"" border=""0"" cellpadding=""0"" cellspacing=""0""> ")
            strMail.Append("<tr height=""20""> <td width=""20""> &nbsp;  </td> <td width=""20""> &nbsp;  </td> <td width=""20""> &nbsp;  </td> <td width=""100"">&nbsp;  </td> <td width=""20""> &nbsp;  </td> <td width=""450"">&nbsp; </td> <td width=""20""> &nbsp;  </td> </tr>")
            strMail.Append("<tr height=""100"">")
            strMail.Append("<td>&nbsp;  </td>")
            strMail.Append("<td colspan=""3""> <img src=""http://www.agiotech.com/_/img/menu/logo-01.png"" alt=""Logo Agiotech"" /> </td>")
            strMail.Append("<td width=""20"">&nbsp;  </td>")
            strMail.Append("<td class=""titulo-mensaje"">" & OrderID & ": AUTORIZADA Para su reparación</td>")
            strMail.Append("<td>&nbsp;  </td>")
            strMail.Append("</tr>")
            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")

            strMail.Append("<tr height=""60"">")
            strMail.Append("    <td width=""20"">&nbsp;  </td>")
            strMail.Append("    <td colspan=""5"">La orden <b>" & OrderID & " </b> ha concluido el proceso de autorización y está disponible para su reparación.")
            strMail.Append("        <br /><br />")
            strMail.Append("        Los datos de la orden son:")
            strMail.Append("    </td>")
            strMail.Append("    <td width=""20"">&nbsp;  </td>")
            strMail.Append("</tr>")

            strMail.Append("")
            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>" & vbNewLine)
            strMail.Append("<tr>" & vbNewLine)
            strMail.Append("    <td  colspan=""1""> &nbsp; </td>" & vbNewLine)
            strMail.Append("    <td  colspan=""4"">" & vbNewLine)
            strMail.Append("        <Table>" & vbNewLine)
            strMail.Append("            <tr>" & vbNewLine)
            strMail.Append("                <td width=""30%""><b>Orden: </b></td>" & vbNewLine)
            strMail.Append("                <td>" & MinInfo.OrderID & "</td>" & vbNewLine)
            strMail.Append("            </tr>" & vbNewLine)
            strMail.Append("            <tr>" & vbNewLine)
            strMail.Append("                <td><b>Modelo: </b></td>" & vbNewLine)
            strMail.Append("                <td>" & MinInfo.Model & "</td>" & vbNewLine)
            strMail.Append("            </tr>" & vbNewLine)
            strMail.Append("            <tr>" & vbNewLine)
            strMail.Append("                <td><b>Numero de Parte: </b></td>" & vbNewLine)
            strMail.Append("                <td>" & MinInfo.PartNo & "</td>" & vbNewLine)
            strMail.Append("            </tr>" & vbNewLine)
            strMail.Append("            <tr>" & vbNewLine)
            strMail.Append("                <td><b>Descripción: </b></td>" & vbNewLine)
            strMail.Append("                <td>" & MinInfo.Description & "</td>" & vbNewLine)
            strMail.Append("            </tr>" & vbNewLine)
            strMail.Append("            <tr>" & vbNewLine)
            strMail.Append("                <td><b>Número de serie: </b></td>" & vbNewLine)
            strMail.Append("                <td>" & MinInfo.SerialNo & "</td>" & vbNewLine)
            strMail.Append("            </tr>" & vbNewLine)
            strMail.Append("            <tr>" & vbNewLine)
            strMail.Append("                <td><b>Falla Reportada: </b></td>" & vbNewLine)
            strMail.Append("                <td>" & MinInfo.Failure & "</td>" & vbNewLine)
            strMail.Append("            </tr>" & vbNewLine)
            strMail.Append("            <tr>" & vbNewLine)
            strMail.Append("                <td>&nbsp;</td>" & vbNewLine)
            strMail.Append("                <td>&nbsp;</td>" & vbNewLine)
            strMail.Append("            </tr>" & vbNewLine)
            strMail.Append("        </Table>" & vbNewLine)
            strMail.Append("    </td>" & vbNewLine)
            strMail.Append("    <td colspan=""2"">&nbsp;</td>" & vbNewLine)
            strMail.Append("</tr>" & vbNewLine)

            'NOT USED --            strMail.Append("<tr height=""60"">")
            'NOT USED --            strMail.Append("    <td width=""20"">&nbsp;  </td>")
            'NOT USED --            strMail.Append("    <td colspan=""5""><br />  Las partes autorizadas son:</td>")
            'NOT USED --            strMail.Append("    <td width=""20"">&nbsp;  </td>")
            'NOT USED --            strMail.Append("</tr>")

            'NOT USED --            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            'NOT USED --            strMail.Append("<tr>")
            'NOT USED --            strMail.Append("    <td  colspan=""7"">")
            'NOT USED --            strMail.Append("        <Table>")
            'NOT USED --            strMail.Append("            <tr>")
            'NOT USED --            strMail.Append("                <th>Orden</th>")
            'NOT USED --            strMail.Append("                <th>Número Parte</th>")
            'NOT USED --            strMail.Append("                <th>Descripción</th>")
            'NOT USED --            strMail.Append("                <th>Fecha de entrega</th>")
            'NOT USED --            strMail.Append("                <th>Comentario Compras</th>")
            'NOT USED --            strMail.Append("                <th>Fecha Compromiso</th>")
            'NOT USED --            strMail.Append("                <th>Tipo Reparación</th>")
            'NOT USED --            strMail.Append("            </tr>")
            'NOT USED --
            'NOT USED --            For Each x As ComprasMailInfo In AppInfo
            'NOT USED --            strMail.Append("            <tr>")
            'NOT USED --            strMail.Append("                <td>" & x.OrderID & "</td>")
            'NOT USED --            strMail.Append("                <td>" & x.PartNo & "</td>")
            'NOT USED --            strMail.Append("                <td>" & x.Descripcion & "</td>")
            'NOT USED --            strMail.Append("                <td>" & x.LeadTime & "</td>")
            'NOT USED --            strMail.Append("                <td>" & x.Comentario & "</td>")
            'NOT USED --            strMail.Append("                <td>" & x.FechaCompromiso & "</td>")
            'NOT USED --            strMail.Append("                <td>" & x.TipoReparacion & "</td>")
            'NOT USED --            strMail.Append("            </tr>")
            'NOT USED --            Next

            'NOT USED --            strMail.Append("        </Table>")
            'NOT USED --            strMail.Append("    </td>")
            'NOT USED --            strMail.Append("</tr>")


            strMail.Append("<tr height=""20""><td colspan=""7"">&nbsp; </td></tr>")
            strMail.Append("</table> ")
            strMail.Append("</body>")
            strMail.Append("</html>")


        Catch ex As Exception
            strMail.Clear()
            strMail.Append(ex.Message)
            _ErrorMess = ex.Message
        Finally
            DA.Dispose()
        End Try

        Return strMail.ToString
    End Function
End Class
