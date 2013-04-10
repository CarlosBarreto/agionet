Imports System.IO
Imports System.Drawing
Imports System.Drawing.Text

Module agGlobals
    '// Fields
    Public Const __DATABASE__ As String = "AgioNet v1.3"
    Public Const __ENPASSWD__ As String = "00Ag10t3ch00"
    Public Const __PASS__ As String = "Agiotech01"
    Public Const __SERVER__ As String = "localhost" '"192.168.1.6" '
    Public Const __USER__ As String = "aguser"

    '// Methods
    Public Function GenerarCodigo(ByRef _Servidor_ As Object, ByVal Code As String, Optional Format As String = "", Optional Width As Integer = 210, Optional Height As Integer = 40, Optional Size As Integer = 60) As Byte()
        Dim resultado() As Byte
        If Width = 0 Then Width = 200
        If Height = 0 Then Height = 60
        If Size = 0 Then Size = 60

        If Not String.IsNullOrEmpty(Code) Then

            Using stream As New MemoryStream()
                Dim bitmap As New Bitmap(Width, Height)
                Dim grafic As Object = Graphics.FromImage(bitmap)
                'Dim fuente = CargarFuente(Format, Size)
                Dim fuente As Font = CargarFuente(_Servidor_)
                Dim point As New Point()
                Dim brush As New SolidBrush(Color.Black)

                grafic.FillRectangle(New SolidBrush(Color.White), 0, 0, Width, Height)
                grafic.DrawString(FormatBarCode(Code.ToUpper()), fuente, brush, point)
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg)
                stream.Seek(0, SeekOrigin.Begin)
                resultado = stream.ToArray()
            End Using
        Else
            resultado = Nothing
        End If

        Return resultado
    End Function

    Private Function FormatBarCode(ByVal code As String) As String
        Dim barcode As String = String.Empty
        barcode = String.Format("*{0}*", code)
        Return barcode
    End Function

    Private Function CargarFuente(ByRef Server As Object, Optional ByVal fuente As String = "E39", Optional ByVal size As Integer = 36) As Font
        Dim f As String = "FRE3OF9X.TTF"
        Select Case f
            Case "E39"
                f = "FRE3OF9X.TTF"
            Case "E13"
                f = "EAN-13.TTF"
            Case "E9"
                f = "FRE3OF9X.TTF"
        End Select

        Dim pfc As New PrivateFontCollection
        Dim Path As String = Server.MapPath("~\Content\fonts\" & f)
        'System.Configuration.ConfigurationManager.AppSettings.Get("PATH_FONTS") + """ + f
        pfc.AddFontFile(Path)
        Dim fontFamily As FontFamily = pfc.Families(0)
        Dim _Font As New Font(fontFamily, Convert.ToSingle(size))

        Return _Font
    End Function

    Public Function TextEncoding(ByVal plainText As String) As String
        Dim key As String = "00Ag10t3ch00"
        Dim des As Simple3Des = New Simple3Des(key)
        Return des.EncryptData(plainText)
    End Function

    Public Function getCreateOrderEmailFormat(OrderID As String, model As CreateOrderModel) As String
        Dim builder As StringBuilder = New StringBuilder

        builder.AppendLine("<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml2/DTD/xhtml1-strict.dtd""> ")
        builder.AppendLine("<html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en""> ")
        builder.AppendLine("<head> ")
        builder.AppendLine("     <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" /> ")
        builder.AppendLine("     <meta http-equiv=""Author"" content=""Agiotech - Agiofix"" /> ")
        builder.AppendLine("    <title>Se ha creado una nueva orden de reparación</title> ")
        builder.AppendLine("</head> ")
        builder.AppendLine("<body> ")
        builder.AppendLine("    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""> ")
        builder.AppendLine("        <tr> ")
        builder.AppendLine("            <td style=""width:10%;""></td> ")
        builder.AppendLine("            <td style=""width:80%;""><img src=""http://www.agiotech.com/_/img/menu/logo-01.png"" alt=""Agiotech Logo"" /> </td> ")
        builder.AppendLine("            <td style=""width:10%;""></td> ")
        builder.AppendLine("        </tr> ")
        builder.AppendLine("        <tr> ")
        builder.AppendLine("            <td style=""width:10%;""></td> ")
        builder.AppendLine("            <td style=""width:80%;""></td> ")
        builder.AppendLine("            <td style=""width:10%;""></td> ")
        builder.AppendLine("        </tr> ")
        builder.AppendLine("        <tr> ")
        builder.AppendLine("            <td style=""width:10%;""></td> ")
        builder.AppendLine("            <td style=""width:80%;""> ")
        builder.AppendLine("                <h4 style=""font-family:Arial, Verdana; margin: auto; width:90%;"">Hola, " + model.CustomerName + " </h4><br /> ")
        builder.AppendLine("                <p style=""font-family:Arial, Verdana; margin: auto; width:90%;""> ")
        builder.AppendLine("                    Este correo es para informarte que se ha creado la Orden de reparación <b>[ " + OrderID + "-01 ]</b> y será atendida de inmediato. Por lo pronto, aquí tienes los datos que se han registrado en nuestro sistema ")
        builder.AppendLine("                </p> ")
        builder.AppendLine("                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0""> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td style=""width:5%;"">&nbsp;</td> ")
        builder.AppendLine("                        <td style=""width:15%;"">&nbsp;</td> ")
        builder.AppendLine("                        <td style=""width:30%;"">&nbsp;</td> ")
        builder.AppendLine("                        <td style=""width:15%;"">&nbsp;</td> ")
        builder.AppendLine("                        <td style=""width:30%;"">&nbsp;</td> ")
        builder.AppendLine("                        <td style=""width:5%;"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                        <td><b>Número de Orden:</b></td> ")
        builder.AppendLine("                        <td>" + OrderID + "-01</td> ")
        builder.AppendLine("                        <td colspan=""3"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1""></td> ")
        builder.AppendLine("                        <td><b>Nombre:</b></td> ")
        builder.AppendLine("                        <td colspan=""2"">" + model.CustomerName + "</td> ")
        builder.AppendLine("                        <td colspan=""2""></td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1""></td> ")
        builder.AppendLine("                        <td><b>Razon Social:</b></td> ")
        builder.AppendLine("                        <td colspan=""2"">" + model.RazonSocial + "</td> ")
        builder.AppendLine("                        <td colspan=""2""></td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1""></td> ")
        builder.AppendLine("                        <td><b>RFC:</b></td> ")
        builder.AppendLine("                        <td>" + model.RFC + "</td> ")
        builder.AppendLine("                        <td><b>Email:</b></td> ")
        builder.AppendLine("                        <td>" + model.Email + "</td> ")
        builder.AppendLine("                        <td colspan=""1""></td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1""></td> ")
        builder.AppendLine("                        <td><b>Dirección:</b></td> ")
        builder.AppendLine("                        <td colspan=""3"">" + model.Address + "No. " + model.ExternalNumber + "Int - " + model.InternalNumber + "</td> ")
        builder.AppendLine("                        <td colspan=""1""></td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                        <td><b>Colonia:</b></td> ")
        builder.AppendLine("                        <td>" + model.Address2 + "</td> ")
        builder.AppendLine("                        <td><b>Ciudad:</b></td> ")
        builder.AppendLine("                        <td>" + model.City + "</td> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                        <td><b>Estado:</b></td> ")
        builder.AppendLine("                        <td>" + model.State + "</td> ")
        builder.AppendLine("                        <td><b>País:</b></td> ")
        builder.AppendLine("                        <td>" + model.Country + "</td> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                        <td><b>C.P.</b></td> ")
        builder.AppendLine("                        <td>" + model.ZipCode + "</td> ")
        builder.AppendLine("                        <td><b>Horario Rec.</b></td> ")
        builder.AppendLine("                        <td>" + model.DeliveryTime + "</td> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                        <td><b>Tel. Casa:</b></td> ")
        builder.AppendLine("                        <td>" + model.Telephone + "</td> ")
        builder.AppendLine("                        <td><b>Tel. Trabajo</b></td> ")
        builder.AppendLine("                        <td>" + model.Telephone2 + "</td> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                        <td><b>Celular:</b></td> ")
        builder.AppendLine("                        <td>" + model.Telephone3 + "</td> ")
        builder.AppendLine("                        <td colspan=""2"">&nbsp;</td> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""6"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                        <td><b>Clase Producto:</b></td> ")
        builder.AppendLine("                        <td>" + model.ProductClass + "</td> ")
        builder.AppendLine("                        <td><b>Tipo Producto:</b></td> ")
        builder.AppendLine("                        <td>" + model.ProductType + "</td> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                        <td><b>Marca:</b></td> ")
        builder.AppendLine("                        <td>" + model.ProductTrademark + "</td> ")
        builder.AppendLine("                        <td><b>Modelo:</b></td> ")
        builder.AppendLine("                        <td>" + model.ProductModel + "</td> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                        <td><b>Descripción:</b></td> ")
        builder.AppendLine("                        <td colspan=""3"">" + model.productDescription + "</td> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                        <td><b>Numero de parte:</b></td> ")
        builder.AppendLine("                        <td>" + model.PartNumber + "</td> ")
        builder.AppendLine("                        <td><b>Numero de serie:</b></td> ")
        builder.AppendLine("                        <td>" + model.SerialNumber + "</td> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""6"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                        <td><b>Clase servicio:</b></td> ")
        builder.AppendLine("                        <td colspan=""3"">" + model.ServiceType + "</td> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                        <td><b>Falla presentada:</b></td> ")
        builder.AppendLine("                        <td colspan=""3"">" + model.FailureType + "</td> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                        <td><b>Comentarios: </b></td> ")
        builder.AppendLine("                        <td colspan=""3"">" + model.Comment + "</td> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                        <td><b>Incluir Flete: </b></td> ")
        builder.AppendLine("                        <td colspan=""3"">" + model.Delivery + "</td> ")
        builder.AppendLine("                        <td colspan=""1"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                    <tr> ")
        builder.AppendLine("                        <td colspan=""6"">&nbsp;</td> ")
        builder.AppendLine("                    </tr> ")
        builder.AppendLine("                </table> ")
        builder.AppendLine("            </td> ")
        builder.AppendLine("            <td style=""width:10%;""></td> ")
        builder.AppendLine("        </tr> ")
        builder.AppendLine("        <tr> ")
        builder.AppendLine("            <td style=""width:10%;""></td> ")
        builder.AppendLine("            <td style=""width:80%;""></td> ")
        builder.AppendLine("            <td style=""width:10%;""></td> ")
        builder.AppendLine("        </tr> ")
        builder.AppendLine("    </table> ")
        builder.AppendLine("</body> ")
        builder.AppendLine("</html> ")
        Return builder.ToString()

    End Function

    '// Nested Types
    Public Structure FailureView
        Public FAILUREID As String
        Public TESTID As String
        Public DESCRIPTION As String
        Public POSSIBLESOLUTION As String
        Public FOUNDBY As String
        Public FOUNDDATE As String
        Public RESOLVED As String
    End Structure

    Public Structure PrintData
        Public ReportedFailure As String
        Public OrderId As String
        Public SerialNo As String
        Public PartNo As String
        Public Model As String
        Public Descripcion As String
        Public Trademark As String
        Public TrackNo As String
        Public ScanDate As String
        Public checkinDate As String
        Public ScanBy As String
        Public PackType As String
        Public PackDamage As String
        Public NonDoucumentDamage As String
        Public CorrectPack As String
        Public Accesories As String
        Public Cosmetic As String
        Public Warranty As String
        Public ReRepair As String
        Public Comment As String



    End Structure

    'Upd by CarlosB 2013.04.09 -- 
    Public Structure TestView
        Public TESTID As String
        Public ORDERID As String
        Public TESTNAME As String
        Public TESTDESCRIPTION As String
        Public TESTRESULT As String
        Public TESTSTART As String
        Public TESTEND As String
        Public FAILURE As String
        Public TEXTLOG As String
        Public CREATEBY As String
    End Structure
End Module
