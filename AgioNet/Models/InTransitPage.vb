Imports Root.Reports
Imports Root.Reports.FontDef
Imports System.Drawing
Imports System.IO
Imports Root.Reports.FlowLayoutManager

Public Class PDFInTransitPage
    Inherits Report
    ' Methods
    Protected Overrides Sub Create()
        Dim FontDef As New FontDef(Me, StandardFont.TimesRoman)
        Dim fp_Titulo1 As New FontProp(FontDef, 16, Color.Black) With { _
            .bBold = True _
        }


        Dim def As New FontDef(Me, StandardFont.Helvetica)
        Dim fontProp As New FontProp(FontDef, 16, Color.Gray)
        Dim prop2 As New FontProp(def, 8, Color.Black) With { _
            .bBold = True _
        }
        Dim prop As New FontProp(def, 8, Color.Black)

        page_Cur = New Page(Me)
        page_Cur.AddRT_MM(200, 20, New RepString(fontProp, "Formato de Recolección"))
        Dim stream As Stream = New FileStream(Me._rutaLogo, FileMode.Open, FileAccess.Read)
        page_Cur.AddMM(20, 30, New RepImageMM(stream, 40, Double.NaN))

        stream = New FileStream(Me._rutaBarCode, FileMode.Open, FileAccess.Read)
        page_Cur.AddMM(100, 45, New RepImageMM(stream, 100, Double.NaN))

        stream = New FileStream(Me._rutaComments, FileMode.Open, FileAccess.Read)
        page_Cur.AddMM(5, 270, New RepImageMM(stream, 204.5, Double.NaN))
        page_Cur.AddLT_MM(20, 40, New RepString(prop2, "Número de Orden:"))
        page_Cur.AddLT_MM(55, 40, New RepString(prop, Me.Orderid))
        page_Cur.AddLT_MM(150, 45, New RepString(prop, Me.Orderid))
        page_Cur.AddLT_MM(20, 65, New RepString(prop2, "Nombre:"))
        page_Cur.AddLT_MM(55, 65, New RepString(prop, Me.Model.CustomerName))
        page_Cur.AddLT_MM(20, 72, New RepString(prop2, "Razon Social:"))
        page_Cur.AddLT_MM(55, 72, New RepString(prop, Me.Model.RazonSocial))
        page_Cur.AddLT_MM(20, 79, New RepString(prop2, "RFC:"))
        page_Cur.AddLT_MM(55, 79, New RepString(prop, Me.Model.RFC))
        page_Cur.AddLT_MM(110, 79, New RepString(prop2, "Email: "))
        page_Cur.AddLT_MM(145, 79, New RepString(prop, Me.Model.Email))
        page_Cur.AddLT_MM(20, 86, New RepString(prop2, "Dirección:"))
        page_Cur.AddLT_MM(55, 86, New RepString(prop, String.Concat(New String() {Me.Model.Address, " No. ", Me.Model.ExternalNumber, " Int - ", Me.Model.InternalNumber})))
        page_Cur.AddLT_MM(20, 93, New RepString(prop2, "Colonia: "))
        page_Cur.AddLT_MM(55, 93, New RepString(prop, Me.Model.Address2))
        page_Cur.AddLT_MM(110, 93, New RepString(prop2, "Ciudad: "))
        page_Cur.AddLT_MM(145, 93, New RepString(prop, Me.Model.City))
        page_Cur.AddLT_MM(20, 100, New RepString(prop2, "Estado: "))
        page_Cur.AddLT_MM(55, 100, New RepString(prop, Me.Model.State))
        page_Cur.AddLT_MM(110, 100, New RepString(prop2, "País: "))
        page_Cur.AddLT_MM(145, 100, New RepString(prop, Me.Model.Country))
        page_Cur.AddLT_MM(20, 107, New RepString(prop2, "C.P. "))
        page_Cur.AddLT_MM(55, 107, New RepString(prop, Me.Model.ZipCode))
        page_Cur.AddLT_MM(110, 107, New RepString(prop2, "Horario Rec. "))
        page_Cur.AddLT_MM(145, 107, New RepString(prop, Me.Model.DeliveryTime))
        page_Cur.AddLT_MM(20, 114, New RepString(prop2, "Tel. Casa: "))
        page_Cur.AddLT_MM(55, 114, New RepString(prop, Me.Model.Telephone))
        page_Cur.AddLT_MM(110, 114, New RepString(prop2, "Tel. Trabajo "))
        page_Cur.AddLT_MM(145, 114, New RepString(prop, Me.Model.Telephone2))
        page_Cur.AddLT_MM(20, 121, New RepString(prop2, "Celular: "))
        page_Cur.AddLT_MM(55, 121, New RepString(prop, Me.Model.Telephone3))
        page_Cur.AddLT_MM(20, 135, New RepString(prop2, "Clase Producto: "))
        page_Cur.AddLT_MM(55, 135, New RepString(prop, Me.Model.ProductClass))
        page_Cur.AddLT_MM(110, 135, New RepString(prop2, "Tipo Producto: "))
        page_Cur.AddLT_MM(145, 135, New RepString(prop, Me.Model.ProductType))
        page_Cur.AddLT_MM(20, 142, New RepString(prop2, "Marca: "))
        page_Cur.AddLT_MM(55, 142, New RepString(prop, Me.Model.ProductTrademark))
        page_Cur.AddLT_MM(110, 142, New RepString(prop2, "Modelo: "))
        page_Cur.AddLT_MM(145, 142, New RepString(prop, Me.Model.ProductModel))
        page_Cur.AddLT_MM(20, 149, New RepString(prop2, "Descripción: "))
        page_Cur.AddLT_MM(55, 149, New RepString(prop, Me.Model.productDescription))
        page_Cur.AddLT_MM(20, 156, New RepString(prop2, "Numero de parte: "))
        page_Cur.AddLT_MM(55, 156, New RepString(prop, Me.Model.PartNumber))
        page_Cur.AddLT_MM(110, 156, New RepString(prop2, "Numero de serie: "))
        page_Cur.AddLT_MM(145, 156, New RepString(prop, Me.Model.SerialNumber))
        page_Cur.AddLT_MM(20, 170, New RepString(prop2, "Clase servicio: "))
        page_Cur.AddLT_MM(55, 170, New RepString(prop, Me.Model.ServiceType))
        page_Cur.AddLT_MM(20, 177, New RepString(prop2, "Falla presentada: "))
        page_Cur.AddLT_MM(55, 177, New RepString(prop, Me.Model.FailureType))
        page_Cur.AddRT_MM(200, 270, New RepString(prop, "Copia para el cliente"))

        '-- Segunda página

        page_Cur = New Root.Reports.Page(Me)
        page_Cur.AddRT_MM(200, 20, New RepString(fontProp, "Formato de Recolección"))
        stream = New FileStream(Me._rutaLogo, FileMode.Open, FileAccess.Read)
        page_Cur.AddMM(20, 30, New RepImageMM(stream, 40, Double.NaN))
        stream = New FileStream(Me._rutaBarCode, FileMode.Open, FileAccess.Read)
        page_Cur.AddMM(100, 45, New RepImageMM(stream, 100, Double.NaN))
        stream = New FileStream(Me._rutaComments, FileMode.Open, FileAccess.Read)
        page_Cur.AddMM(5, 270, New RepImageMM(stream, 204.5, Double.NaN))

        page_Cur.AddLT_MM(20, 40, New RepString(prop2, "Número de Orden:"))
        page_Cur.AddLT_MM(55, 40, New RepString(prop, Me.Orderid))
        page_Cur.AddLT_MM(150, 45, New RepString(prop, Me.Orderid))
        page_Cur.AddLT_MM(20, 65, New RepString(prop2, "Nombre:"))
        page_Cur.AddLT_MM(55, 65, New RepString(prop, Me.Model.CustomerName))
        page_Cur.AddLT_MM(20, 72, New RepString(prop2, "Razon Social:"))
        page_Cur.AddLT_MM(55, 72, New RepString(prop, Me.Model.RazonSocial))
        page_Cur.AddLT_MM(20, 79, New RepString(prop2, "RFC:"))
        page_Cur.AddLT_MM(55, 79, New RepString(prop, Me.Model.RFC))
        page_Cur.AddLT_MM(110, 79, New RepString(prop2, "Email: "))
        page_Cur.AddLT_MM(145, 79, New RepString(prop, Me.Model.Email))
        page_Cur.AddLT_MM(20, 86, New RepString(prop2, "Dirección:"))
        page_Cur.AddLT_MM(55, 86, New RepString(prop, String.Concat(New String() {Me.Model.Address, " No. ", Me.Model.ExternalNumber, " Int - ", Me.Model.InternalNumber})))
        page_Cur.AddLT_MM(20, 93, New RepString(prop2, "Colonia: "))
        page_Cur.AddLT_MM(55, 93, New RepString(prop, Me.Model.Address2))
        page_Cur.AddLT_MM(110, 93, New RepString(prop2, "Ciudad: "))
        page_Cur.AddLT_MM(145, 93, New RepString(prop, Me.Model.City))
        page_Cur.AddLT_MM(20, 100, New RepString(prop2, "Estado: "))
        page_Cur.AddLT_MM(55, 100, New RepString(prop, Me.Model.State))
        page_Cur.AddLT_MM(110, 100, New RepString(prop2, "País: "))
        page_Cur.AddLT_MM(145, 100, New RepString(prop, Me.Model.Country))
        page_Cur.AddLT_MM(20, 107, New RepString(prop2, "C.P. "))
        page_Cur.AddLT_MM(55, 107, New RepString(prop, Me.Model.ZipCode))
        page_Cur.AddLT_MM(110, 107, New RepString(prop2, "Horario Rec. "))
        page_Cur.AddLT_MM(145, 107, New RepString(prop, Me.Model.DeliveryTime))
        page_Cur.AddLT_MM(20, 114, New RepString(prop2, "Tel. Casa: "))
        page_Cur.AddLT_MM(55, 114, New RepString(prop, Me.Model.Telephone))
        page_Cur.AddLT_MM(110, 114, New RepString(prop2, "Tel. Trabajo "))
        page_Cur.AddLT_MM(145, 114, New RepString(prop, Me.Model.Telephone2))
        page_Cur.AddLT_MM(20, 121, New RepString(prop2, "Celular: "))
        page_Cur.AddLT_MM(55, 121, New RepString(prop, Me.Model.Telephone3))
        page_Cur.AddLT_MM(20, 135, New RepString(prop2, "Clase Producto: "))
        page_Cur.AddLT_MM(55, 135, New RepString(prop, Me.Model.ProductClass))
        page_Cur.AddLT_MM(110, 135, New RepString(prop2, "Tipo Producto: "))
        page_Cur.AddLT_MM(145, 135, New RepString(prop, Me.Model.ProductType))
        page_Cur.AddLT_MM(20, 142, New RepString(prop2, "Marca: "))
        page_Cur.AddLT_MM(55, 142, New RepString(prop, Me.Model.ProductTrademark))
        page_Cur.AddLT_MM(110, 142, New RepString(prop2, "Modelo: "))
        page_Cur.AddLT_MM(145, 142, New RepString(prop, Me.Model.ProductModel))
        page_Cur.AddLT_MM(20, 149, New RepString(prop2, "Descripción: "))
        page_Cur.AddLT_MM(55, 149, New RepString(prop, Me.Model.productDescription))
        page_Cur.AddLT_MM(20, 156, New RepString(prop2, "Numero de parte: "))
        page_Cur.AddLT_MM(55, 156, New RepString(prop, Me.Model.PartNumber))
        page_Cur.AddLT_MM(110, 156, New RepString(prop2, "Numero de serie: "))
        page_Cur.AddLT_MM(145, 156, New RepString(prop, Me.Model.SerialNumber))
        page_Cur.AddLT_MM(20, 170, New RepString(prop2, "Clase servicio: "))
        page_Cur.AddLT_MM(55, 170, New RepString(prop, Me.Model.ServiceType))
        page_Cur.AddLT_MM(20, 177, New RepString(prop2, "Falla presentada: "))
        page_Cur.AddLT_MM(55, 177, New RepString(prop, Me.Model.FailureType))
        page_Cur.AddRT_MM(200, 270, New RepString(prop, "Copia para Mensajería"))

        '---  Tercera página

        page_Cur = New Root.Reports.Page(Me)
        page_Cur.AddRT_MM(200, 20, New RepString(fontProp, "Formato de Recolección"))
        stream = New FileStream(Me._rutaLogo, FileMode.Open, FileAccess.Read)
        page_Cur.AddMM(20, 30, New RepImageMM(stream, 40, Double.NaN))
        stream = New FileStream(Me._rutaBarCode, FileMode.Open, FileAccess.Read)
        page_Cur.AddMM(100, 45, New RepImageMM(stream, 100, Double.NaN))
        stream = New FileStream(Me._rutaComments, FileMode.Open, FileAccess.Read)
        page_Cur.AddMM(5, 270, New RepImageMM(stream, 204.5, Double.NaN))

        page_Cur.AddLT_MM(20, 40, New RepString(prop2, "Número de Orden:"))
        page_Cur.AddLT_MM(55, 40, New RepString(prop, Me.Orderid))
        page_Cur.AddLT_MM(150, 45, New RepString(prop, Me.Orderid))
        page_Cur.AddLT_MM(20, 65, New RepString(prop2, "Nombre:"))
        page_Cur.AddLT_MM(55, 65, New RepString(prop, Me.Model.CustomerName))
        page_Cur.AddLT_MM(20, 72, New RepString(prop2, "Razon Social:"))
        page_Cur.AddLT_MM(55, 72, New RepString(prop, Me.Model.RazonSocial))
        page_Cur.AddLT_MM(20, 79, New RepString(prop2, "RFC:"))
        page_Cur.AddLT_MM(55, 79, New RepString(prop, Me.Model.RFC))
        page_Cur.AddLT_MM(110, 79, New RepString(prop2, "Email: "))
        page_Cur.AddLT_MM(145, 79, New RepString(prop, Me.Model.Email))
        page_Cur.AddLT_MM(20, 86, New RepString(prop2, "Dirección:"))
        page_Cur.AddLT_MM(55, 86, New RepString(prop, String.Concat(New String() {Me.Model.Address, " No. ", Me.Model.ExternalNumber, " Int - ", Me.Model.InternalNumber})))
        page_Cur.AddLT_MM(20, 93, New RepString(prop2, "Colonia: "))
        page_Cur.AddLT_MM(55, 93, New RepString(prop, Me.Model.Address2))
        page_Cur.AddLT_MM(110, 93, New RepString(prop2, "Ciudad: "))
        page_Cur.AddLT_MM(145, 93, New RepString(prop, Me.Model.City))
        page_Cur.AddLT_MM(20, 100, New RepString(prop2, "Estado: "))
        page_Cur.AddLT_MM(55, 100, New RepString(prop, Me.Model.State))
        page_Cur.AddLT_MM(110, 100, New RepString(prop2, "País: "))
        page_Cur.AddLT_MM(145, 100, New RepString(prop, Me.Model.Country))
        page_Cur.AddLT_MM(20, 107, New RepString(prop2, "C.P. "))
        page_Cur.AddLT_MM(55, 107, New RepString(prop, Me.Model.ZipCode))
        page_Cur.AddLT_MM(110, 107, New RepString(prop2, "Horario Rec. "))
        page_Cur.AddLT_MM(145, 107, New RepString(prop, Me.Model.DeliveryTime))
        page_Cur.AddLT_MM(20, 114, New RepString(prop2, "Tel. Casa: "))
        page_Cur.AddLT_MM(55, 114, New RepString(prop, Me.Model.Telephone))
        page_Cur.AddLT_MM(110, 114, New RepString(prop2, "Tel. Trabajo "))
        page_Cur.AddLT_MM(145, 114, New RepString(prop, Me.Model.Telephone2))
        page_Cur.AddLT_MM(20, 121, New RepString(prop2, "Celular: "))
        page_Cur.AddLT_MM(55, 121, New RepString(prop, Me.Model.Telephone3))
        page_Cur.AddLT_MM(20, 135, New RepString(prop2, "Clase Producto: "))
        page_Cur.AddLT_MM(55, 135, New RepString(prop, Me.Model.ProductClass))
        page_Cur.AddLT_MM(110, 135, New RepString(prop2, "Tipo Producto: "))
        page_Cur.AddLT_MM(145, 135, New RepString(prop, Me.Model.ProductType))
        page_Cur.AddLT_MM(20, 142, New RepString(prop2, "Marca: "))
        page_Cur.AddLT_MM(55, 142, New RepString(prop, Me.Model.ProductTrademark))
        page_Cur.AddLT_MM(110, 142, New RepString(prop2, "Modelo: "))
        page_Cur.AddLT_MM(145, 142, New RepString(prop, Me.Model.ProductModel))
        page_Cur.AddLT_MM(20, 149, New RepString(prop2, "Descripción: "))
        page_Cur.AddLT_MM(55, 149, New RepString(prop, Me.Model.productDescription))
        page_Cur.AddLT_MM(20, 156, New RepString(prop2, "Numero de parte: "))
        page_Cur.AddLT_MM(55, 156, New RepString(prop, Me.Model.PartNumber))
        page_Cur.AddLT_MM(110, 156, New RepString(prop2, "Numero de serie: "))
        page_Cur.AddLT_MM(145, 156, New RepString(prop, Me.Model.SerialNumber))
        page_Cur.AddLT_MM(20, 170, New RepString(prop2, "Clase servicio: "))
        page_Cur.AddLT_MM(55, 170, New RepString(prop, Me.Model.ServiceType))
        page_Cur.AddLT_MM(20, 177, New RepString(prop2, "Falla presentada: "))
        page_Cur.AddLT_MM(55, 177, New RepString(prop, Me.Model.FailureType))

        page_Cur.AddRT_MM(200, 270, New RepString(prop, "Copia Administración"))



    End Sub


    ' Properties
    Public Property IsCopy As Boolean
        Get
            Return Me._IsCopy
        End Get
        Set(ByVal value As Boolean)
            Me._IsCopy = value
        End Set
    End Property

    Public Property Model As CreateOrderModel
        Get
            Return Me._Model
        End Get
        Set(ByVal value As CreateOrderModel)
            Me._Model = value
        End Set
    End Property

    Public Property Orderid As String
        Get
            Return Me._OrderID
        End Get
        Set(ByVal value As String)
            Me._OrderID = value
        End Set
    End Property

    Public Property RutaBarCode As String
        Get
            Return Me._rutaBarCode
        End Get
        Set(ByVal value As String)
            Me._rutaBarCode = value
        End Set
    End Property

    Public Property RutaComments As String
        Get
            Return Me._rutaComments
        End Get
        Set(ByVal value As String)
            Me._rutaComments = value
        End Set
    End Property

    Public Property RutaLogo As String
        Get
            Return Me._rutaLogo
        End Get
        Set(ByVal value As String)
            Me._rutaLogo = value
        End Set
    End Property


    ' Fields
    Private _IsCopy As Boolean
    Private _Model As CreateOrderModel
    Private _OrderID As String
    Private _rutaBarCode As String
    Private _rutaComments As String
    Private _rutaLogo As String
End Class

Public Class PDFHojaViajera
    Inherits Report

    Private rPosLeft As Double = 20 ';  // millimeters
    Private rPosRight As Double = 195 ';  // millimeters
    Private rPosTop As Double = 24 ';  // millimeters
    Private rPosBottom As Double = 278 ';  // millimeters



    ' Methods
    Protected Overrides Sub Create()
        Dim fontDef As New FontDef(Me, StandardFont.TimesRoman)
        Dim def As New FontDef(Me, StandardFont.Helvetica)
        Dim fontProp As New FontProp(fontDef, 16, Color.Gray)
        Dim prop2 As New FontProp(def, 8, Color.Black) With { _
            .bBold = True _
        }

        Dim prop As New FontProp(def, 8, Color.Black)
        Dim page As New Page(Me)

        page.AddRT_MM(200, 20, New RepString(fontProp, "Formato de ingreso"))

        Dim stream As Stream = New FileStream(Me._rutaLogo, FileMode.Open, FileAccess.Read)
        page.AddMM(20, 30, New RepImageMM(stream, 40, Double.NaN))

        stream = New FileStream(Me._rutaBarCode, FileMode.Open, FileAccess.Read)
        page.AddMM(100, 45, New RepImageMM(stream, 100, Double.NaN))

        page.AddLT_MM(20, 45, New RepString(prop2, "Número de Orden:"))
        page.AddLT_MM(55, 45, New RepString(prop, Model.Orderid))
        page.AddLT_MM(150, 55, New RepString(prop, Model.Orderid))
        page.AddLT_MM(20, 65, New RepString(prop2, "Número de Serie:"))
        page.AddLT_MM(55, 65, New RepString(prop, Model.SerialNo))
        page.AddLT_MM(20, 72, New RepString(prop2, "Número de Parte / SKU:"))
        page.AddLT_MM(65, 72, New RepString(prop, Model.PartNo))
        page.AddLT_MM(20, 79, New RepString(prop2, "Modelo:"))
        page.AddLT_MM(55, 79, New RepString(prop, Model.Model))
        '-----------
        page.AddLT_MM(20, 86, New RepString(prop2, "Descripción:"))
        page.AddLT_MM(55, 86, New RepString(prop, Model.Descripcion))
        page.AddLT_MM(20, 93, New RepString(prop2, "Marca:"))
        page.AddLT_MM(55, 93, New RepString(prop, Model.Trademark))
        '-- Descripcion
        page.AddLT_MM(20, 100, New RepString(prop2, "Track No:"))
        page.AddLT_MM(55, 100, New RepString(prop, Model.TrackNo))
        '----
        page.AddLT_MM(20, 107, New RepString(prop2, "Fecha arribo:"))
        page.AddLT_MM(55, 107, New RepString(prop, Model.checkinDate))

        page.AddLT_MM(20, 114, New RepString(prop2, "Fecha Ingreso: "))
        page.AddLT_MM(55, 114, New RepString(prop, Model.ScanDate))
        page.AddLT_MM(110, 114, New RepString(prop2, "Ingresado por: "))
        page.AddLT_MM(145, 114, New RepString(prop, Model.ScanBy))

        page.AddLT_MM(20, 121, New RepString(prop2, "Tipo Empaque: "))
        page.AddLT_MM(55, 121, New RepString(prop, Model.PackType))
        page.AddLT_MM(110, 121, New RepString(prop2, "Empaque Dañado: "))
        page.AddLT_MM(145, 121, New RepString(prop, Model.PackDamage))

        page.AddLT_MM(20, 128, New RepString(prop2, "Daño no documentado: "))
        page.AddLT_MM(65, 128, New RepString(prop, Model.NonDoucumentDamage))
        page.AddLT_MM(110, 128, New RepString(prop2, "Empacado Correctamente "))
        page.AddLT_MM(165, 128, New RepString(prop, Model.CorrectPack))

        page.AddLT_MM(20, 135, New RepString(prop2, "Accesorios: "))
        page.AddLT_MM(55, 135, New RepString(prop, Model.Accesories))

        Dim stra As String
        Dim line As Integer = 0
        Dim posY As Integer = 163
        Dim lt As String

        page.AddLT_MM(20, 156, New RepString(prop2, "Cosmetico / Observaciones: "))
        While (line * 55) + 1 < Len(Model.Cosmetic)
            lt = Mid(Model.Cosmetic, (line * 55) + 1, 1)
            If lt <> " " Then lt = "_ "
            stra = Mid(Model.Cosmetic, (line * 55) + 1, 55) & lt
            page.AddLT_MM(55, posY, New RepString(prop, stra))
            line += 1
            posY += 7
        End While
        'page.AddLT_MM(55, 163, New RepString(prop, Model.Cosmetic))

        page.AddLT_MM(20, 184, New RepString(prop2, "Garantía: "))
        page.AddLT_MM(55, 184, New RepString(prop, Model.Warranty))
        page.AddLT_MM(110, 184, New RepString(prop2, "Re repair: "))
        page.AddLT_MM(145, 184, New RepString(prop, Model.ReRepair))

        page.AddLT_MM(20, 191, New RepString(prop2, "Comentarios: "))
        page.AddLT_MM(55, 191, New RepString(prop, Model.Comment))
        page.AddLT_MM(20, 212, New RepString(prop2, "Falla presentada: "))

        'If Len(Model.ReportedFailure) > 55 Then

        stra = ""
        line = 0
        posY = 212
        lt = ""
        While (line * 55) + 1 < Len(Model.ReportedFailure)
            lt = Mid(Model.ReportedFailure, (line * 55) + 1, 1)
            If lt <> " " Then lt = "_ "
            stra = Mid(Model.ReportedFailure, (line * 55) + 1, 55) & lt
            page.AddLT_MM(55, posY, New RepString(prop, stra))
            line += 1
            posY += 7
        End While

        'Upd 2013.06.25 Agregar check list By CarlosB -- Begin
        Dim cklistPath As String = Mid(Me._rutaLogo, 1, Me._rutaLogo.ToString.Length - 11)
        Select Case Model.ProductClass
            Case "LAPTOP"
                cklistPath = cklistPath & "ckl_laptop.JPG"
            Case "DESKTOP"
                cklistPath = cklistPath & "ckl_desktop.JPG"
            Case "AIO"
                cklistPath = cklistPath & "ckl_desktop.JPG"
            Case "TABLET"
                cklistPath = cklistPath & "ckl_tablet.JPG"
            Case "CONSOLA"
                cklistPath = cklistPath & "ckl_consola.JPG"
            Case "VIDEOJUEGO"
                cklistPath = cklistPath & "ckl_consola.JPG"
            Case "TV"
                cklistPath = cklistPath & "ckl_tv.JPG"
            Case "CELULAR"
                cklistPath = cklistPath & "ckl_celular.JPG"
            Case "TELEFONO"
                cklistPath = cklistPath & "ckl_celular.JPG"
            Case "IMPRESORA"
                cklistPath = cklistPath & "ckl_impresora.JPG"
            Case "MULTIFUNCIONAL"
                cklistPath = cklistPath & "ckl_impresora.JPG"
            Case Else
                cklistPath = ""
        End Select

        If cklistPath <> "" Then
            stream = New FileStream(cklistPath, FileMode.Open, FileAccess.Read)
            page.AddMM(5, 290, New RepImageMM(stream, 204.5, Double.NaN))
        End If
        'Upd 2013.06.25 Agregar check list By CarlosB -- Begin

        'Nuevo formato
        page_Cur = New Root.Reports.Page(Me)
        Dim prop3 As New FontProp(def, 7, Color.Black)

        'page_Cur = New Page(Me)
        'Colocar la cabecera
        Dim imagePath As String = Mid(Me._rutaLogo, 1, Me._rutaLogo.Length - 11) & "dg_head.jpg"
        stream = New FileStream(Me._rutaLogo, FileMode.Open, FileAccess.Read)
        page_Cur.AddMM(20, 30, New RepImageMM(stream, 40, Double.NaN))

        '-- Colocar cuadro de firmas / Deshabilitado por el momento
        stream = New FileStream(imagePath, FileMode.Open, FileAccess.Read)
        page_Cur.AddMM(5, 75, New RepImageMM(stream, 204.5, Double.NaN))


        page_Cur.AddLT_MM(65, 35.5, New RepString(prop3, Mid(Model.Orderid, 3, Model.orderid.ToString.Length - 5)))
        page_Cur.AddLT_MM(65, 39.7, New RepString(prop3, Model.Customer))
        page_Cur.AddLT_MM(65, 43.7, New RepString(prop3, Model.Trademark))
        page_Cur.AddLT_MM(65, 48, New RepString(prop3, Model.Model))
        page_Cur.AddLT_MM(65, 53, New RepString(prop3, Model.SerialNo))

        Dim imgBody As String = Mid(Me._rutaLogo, 1, Me._rutaLogo.Length - 11)
        Select Case Model.ProductClass
            Case "LAPTOP"
                imgBody = imgBody & "dg_laptop.JPG"
            Case "DESKTOP"
                imgBody = imgBody & "dg_desktop.JPG"
            Case "AIO"
                imgBody = imgBody & "dg_desktop.JPG"
            Case "TABLET"
                imgBody = imgBody & "dg_tablet.JPG"
            Case "CONSOLA"
                imgBody = imgBody & "dg_consola.JPG"
            Case "VIDEOJUEGO"
                imgBody = imgBody & "dg_consola.JPG"
            Case "TV"
                imgBody = imgBody & "dg_tv.JPG"
            Case "CELULAR"
                imgBody = imgBody & "dg_celular.JPG"
            Case "TELEFONO"
                imgBody = imgBody & "dg_celular.JPG"
            Case "IMPRESORA"
                imgBody = imgBody & "dg_impresora.JPG"
            Case "MULTIFUNCIONAL"
                imgBody = imgBody & "dg_impresora.JPG"
            Case Else
                imgBody = imgBody & "dg_clean.jpg"
        End Select

        If imgBody <> "" Then
            stream = New FileStream(imgBody, FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(5, 290, New RepImageMM(stream, 204.5, Double.NaN))
        End If
    End Sub


    ' Properties
    Public Property Model As Object
        Get
            Return Me._Model
        End Get
        Set(ByVal value As Object)
            Me._Model = DirectCast(value, PrintData)
        End Set
    End Property

    Public Property RutaBarCode As String
        Get
            Return Me._rutaBarCode
        End Get
        Set(ByVal value As String)
            Me._rutaBarCode = value
        End Set
    End Property

    Public Property RutaLogo As String
        Get
            Return Me._rutaLogo
        End Get
        Set(ByVal value As String)
            Me._rutaLogo = value
        End Set
    End Property


    ' Fields
    Private _Model As PrintData
    Private _rutaBarCode As String
    Private _rutaLogo As String
End Class

Public Class PDFIntransit
    Inherits Report
    ' Methods
    Protected Overrides Sub Create()
        Dim Counter As Integer = 0
        Dim msg As String = String.Empty

        ' -- Declarar los tipos de letra
        Dim fdTimes As New FontDef(Me, StandardFont.TimesRoman)

        '-- Declarar los estilos
        Dim fp_Titulo As New FontProp(fdTimes, 12, Color.Black) With {.bBold = True}
        Dim fp_Contacto As New FontProp(fdTimes, 6, Color.Black)
        Dim fp_subTitulo As New FontProp(fdTimes, 8, Color.Black) With {.bBold = True}
        Dim fp_Campo As New FontProp(fdTimes, 7, Color.Black) With {.bBold = True}
        Dim fp_Datos As New FontProp(fdTimes, 7, Color.Black)

        Do While Counter < 3
            '-- Crar nueva página
            page_Cur = New Page(Me)

            '-- Colocar el logo
            Dim stream As Stream = New FileStream(Me._rutaLogo, FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(20, 30, New RepImageMM(stream, 40, Double.NaN))
            '-- Colocar el código de barras
            stream = New FileStream(Me._rutaBarCode, FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(100, 50, New RepImageMM(stream, 100, Double.NaN))

            '-- Colocar cuadro de firmas / Deshabilitado por el momento
            stream = New FileStream(Me._rutaComments, FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(5, 270, New RepImageMM(stream, 204.5, Double.NaN))

            '-- Colocar el título
            page_Cur.AddLT_MM(100, 21, New RepString(fp_Titulo, "Formato de recolección de equipo"))

            '-- Colocar los datos de la empresa
            page_Cur.AddLT_MM(15, 36, New RepString(fp_Contacto, "Agiotech de México S.A. de C.V."))
            page_Cur.AddLT_MM(15, 40, New RepString(fp_Contacto, "Parque Industrial City Park"))
            page_Cur.AddLT_MM(15, 44, New RepString(fp_Contacto, "Av. Aviación 5051 Nave 8"))
            page_Cur.AddLT_MM(15, 48, New RepString(fp_Contacto, "Col. San Juan de Ocotán"))
            page_Cur.AddLT_MM(15, 52, New RepString(fp_Contacto, "Zapopan, Jalisco C.P. 45019"))

            '-- Colocar el OrderID
            page_Cur.AddLT_MM(130, 52, New RepString(fp_Datos, Me.Orderid))

            '-- Colocar información del cliente
            page_Cur.AddLT_MM(15, 65, New RepString(fp_subTitulo, "Información del cliente"))

            page_Cur.AddLT_MM(15, 75, New RepString(fp_Campo, "Nombre:"))
            page_Cur.AddLT_MM(45, 75, New RepString(fp_Datos, Me.Model.CustomerName))

            page_Cur.AddLT_MM(15, 80, New RepString(fp_Campo, "Razon Social:"))
            page_Cur.AddLT_MM(45, 80, New RepString(fp_Datos, Me.Model.RazonSocial))

            page_Cur.AddLT_MM(15, 85, New RepString(fp_Campo, "RFC:"))
            page_Cur.AddLT_MM(45, 85, New RepString(fp_Datos, Me.Model.RFC))
            page_Cur.AddLT_MM(105, 85, New RepString(fp_Campo, "Email: "))
            page_Cur.AddLT_MM(130, 85, New RepString(fp_Datos, Me.Model.Email))

            page_Cur.AddLT_MM(15, 90, New RepString(fp_Campo, "Dirección:"))
            page_Cur.AddLT_MM(45, 90, New RepString(fp_Datos, String.Concat(New String() {Me.Model.Address, " No. ", Me.Model.ExternalNumber, " Int - ", Me.Model.InternalNumber})))

            page_Cur.AddLT_MM(15, 95, New RepString(fp_Campo, "Colonia: "))
            page_Cur.AddLT_MM(45, 95, New RepString(fp_Datos, Me.Model.Address2))

            page_Cur.AddLT_MM(15, 100, New RepString(fp_Campo, "Ciudad: "))
            page_Cur.AddLT_MM(45, 100, New RepString(fp_Datos, Me.Model.City))
            page_Cur.AddLT_MM(115, 100, New RepString(fp_Campo, "Estado: "))
            page_Cur.AddLT_MM(140, 100, New RepString(fp_Datos, Me.Model.State))

            page_Cur.AddLT_MM(15, 105, New RepString(fp_Campo, "País: "))
            page_Cur.AddLT_MM(45, 105, New RepString(fp_Datos, Me.Model.Country))
            page_Cur.AddLT_MM(85, 105, New RepString(fp_Campo, "C.P. "))
            page_Cur.AddLT_MM(100, 105, New RepString(fp_Datos, Me.Model.ZipCode))
            page_Cur.AddLT_MM(140, 105, New RepString(fp_Campo, "Horario Rec. "))
            page_Cur.AddLT_MM(165, 105, New RepString(fp_Datos, Me.Model.DeliveryTime))

            page_Cur.AddLT_MM(14, 110, New RepString(fp_Campo, "Tel. Casa: "))
            page_Cur.AddLT_MM(45, 110, New RepString(fp_Datos, Me.Model.Telephone))
            page_Cur.AddLT_MM(85, 110, New RepString(fp_Campo, "Tel. Trabajo "))
            page_Cur.AddLT_MM(100, 110, New RepString(fp_Datos, Me.Model.Telephone2))
            page_Cur.AddLT_MM(140, 110, New RepString(fp_Campo, "Celular: "))
            page_Cur.AddLT_MM(165, 110, New RepString(fp_Datos, Me.Model.Telephone3))

            '-- Colocar la información del equipo
            page_Cur.AddLT_MM(15, 120, New RepString(fp_subTitulo, "Información del equipo"))

            page_Cur.AddLT_MM(15, 130, New RepString(fp_Campo, "Clase Producto: "))
            page_Cur.AddLT_MM(45, 130, New RepString(fp_Datos, Me.Model.ProductClass))
            page_Cur.AddLT_MM(105, 130, New RepString(fp_Campo, "Tipo Producto: "))
            page_Cur.AddLT_MM(140, 130, New RepString(fp_Datos, Me.Model.ProductType))

            page_Cur.AddLT_MM(15, 135, New RepString(fp_Campo, "Marca: "))
            page_Cur.AddLT_MM(45, 135, New RepString(fp_Datos, Me.Model.ProductTrademark))
            page_Cur.AddLT_MM(105, 135, New RepString(fp_Campo, "Modelo: "))
            page_Cur.AddLT_MM(140, 135, New RepString(fp_Datos, Me.Model.ProductModel))

            page_Cur.AddLT_MM(15, 140, New RepString(fp_Campo, "Descripción: "))
            page_Cur.AddLT_MM(45, 140, New RepString(fp_Datos, Me.Model.productDescription))

            page_Cur.AddLT_MM(15, 145, New RepString(fp_Campo, "Numero de parte: "))
            page_Cur.AddLT_MM(45, 145, New RepString(fp_Datos, Me.Model.PartNumber))
            page_Cur.AddLT_MM(105, 145, New RepString(fp_Campo, "Numero de serie: "))
            page_Cur.AddLT_MM(140, 145, New RepString(fp_Datos, Me.Model.SerialNumber))

            page_Cur.AddLT_MM(15, 155, New RepString(fp_Campo, "Clase servicio: "))
            page_Cur.AddLT_MM(45, 155, New RepString(fp_Datos, Me.Model.ServiceType))

            page_Cur.AddLT_MM(15, 160, New RepString(fp_Campo, "Falla presentada: "))
            '------------------------
            Dim stra As String
            Dim line As Integer = 0
            Dim posY As Integer = 160
            Dim lt As String
            Dim fr As Integer = 65

            While (line * fr) + 1 < Len(Model.FailureType)
                lt = Mid(Model.FailureType, (line * fr) + 1, 1)
                If lt <> " " Then lt = "_ "
                stra = Mid(Model.FailureType, (line * fr) + 1, fr) & lt
                page_Cur.AddLT_MM(45, posY, New RepString(fp_Datos, stra))
                line += 1
                posY += 7
            End While

            'page_Cur.AddLT_MM(45, 160, New RepString(fp_Datos, Me.Model.FailureType))
            Select Case Counter
                Case 0
                    msg = "Copia para el cliente"
                Case 1
                    msg = "Copia para Mensajería"
                Case 2
                    msg = "Copia Administración"
            End Select


            page_Cur.AddRT_MM(200, 270, New RepString(fp_Datos, msg))

            Counter += 1
        Loop

    End Sub


    ' Properties
    Public Property IsCopy As Boolean
        Get
            Return Me._IsCopy
        End Get
        Set(ByVal value As Boolean)
            Me._IsCopy = value
        End Set
    End Property

    Public Property Model As CreateOrderModel
        Get
            Return Me._Model
        End Get
        Set(ByVal value As CreateOrderModel)
            Me._Model = value
        End Set
    End Property

    Public Property Orderid As String
        Get
            Return Me._OrderID
        End Get
        Set(ByVal value As String)
            Me._OrderID = value
        End Set
    End Property

    Public Property RutaBarCode As String
        Get
            Return Me._rutaBarCode
        End Get
        Set(ByVal value As String)
            Me._rutaBarCode = value
        End Set
    End Property

    Public Property RutaComments As String
        Get
            Return Me._rutaComments
        End Get
        Set(ByVal value As String)
            Me._rutaComments = value
        End Set
    End Property

    Public Property RutaLogo As String
        Get
            Return Me._rutaLogo
        End Get
        Set(ByVal value As String)
            Me._rutaLogo = value
        End Set
    End Property


    ' Fields
    Private _IsCopy As Boolean
    Private _Model As CreateOrderModel
    Private _OrderID As String
    Private _rutaBarCode As String
    Private _rutaComments As String
    Private _rutaLogo As String
End Class

Public Class PDFFormatoEntrega
    Inherits Report
    ' Methods
    Protected Overrides Sub Create()
        Dim Counter As Integer = 0
        Dim msg As String = String.Empty

        ' -- Declarar los tipos de letra
        Dim fdTimes As New FontDef(Me, StandardFont.TimesRoman)

        '-- Declarar los estilos
        Dim fp_Titulo As New FontProp(fdTimes, 12, Color.Black) With {.bBold = True}
        Dim fp_Contacto As New FontProp(fdTimes, 6, Color.Black)
        Dim fp_subTitulo As New FontProp(fdTimes, 8, Color.Black) With {.bBold = True}
        Dim fp_Campo As New FontProp(fdTimes, 7, Color.Black) With {.bBold = True}
        Dim fp_Datos As New FontProp(fdTimes, 7, Color.Black)

        Do While Counter < 3
            '-- Crar nueva página
            page_Cur = New Page(Me)

            '-- Colocar el logo
            Dim stream As Stream = New FileStream(Me._rutaLogo, FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(20, 30, New RepImageMM(stream, 40, Double.NaN))
            '-- Colocar el código de barras
            stream = New FileStream(Me._rutaBarCode, FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(100, 50, New RepImageMM(stream, 100, Double.NaN))

            '-- Colocar cuadro de firmas / Deshabilitado por el momento
            stream = New FileStream(Me._rutaComments, FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(5, 270, New RepImageMM(stream, 204.5, Double.NaN))

            '-- Colocar el título
            page_Cur.AddLT_MM(100, 21, New RepString(fp_Titulo, "Formato de entrega de equipo"))

            '-- Colocar los datos de la empresa
            page_Cur.AddLT_MM(15, 36, New RepString(fp_Contacto, "Agiotech de México S.A. de C.V."))
            page_Cur.AddLT_MM(15, 40, New RepString(fp_Contacto, "Parque Industrial City Park"))
            page_Cur.AddLT_MM(15, 44, New RepString(fp_Contacto, "Av. Aviación 5051 Nave 8"))
            page_Cur.AddLT_MM(15, 48, New RepString(fp_Contacto, "Col. San Juan de Ocotán"))
            page_Cur.AddLT_MM(15, 52, New RepString(fp_Contacto, "Zapopan, Jalisco C.P. 45019"))

            '-- Colocar el OrderID
            page_Cur.AddLT_MM(130, 52, New RepString(fp_Datos, Me.Orderid))

            '-- Colocar información del cliente
            page_Cur.AddLT_MM(15, 65, New RepString(fp_subTitulo, "Información del cliente"))

            page_Cur.AddLT_MM(15, 75, New RepString(fp_Campo, "Nombre:"))
            page_Cur.AddLT_MM(45, 75, New RepString(fp_Datos, Me.Model.CustomerName))

            page_Cur.AddLT_MM(15, 80, New RepString(fp_Campo, "Razon Social:"))
            page_Cur.AddLT_MM(45, 80, New RepString(fp_Datos, Me.Model.RazonSocial))

            page_Cur.AddLT_MM(15, 85, New RepString(fp_Campo, "RFC:"))
            page_Cur.AddLT_MM(45, 85, New RepString(fp_Datos, Me.Model.RFC))
            page_Cur.AddLT_MM(105, 85, New RepString(fp_Campo, "Email: "))
            page_Cur.AddLT_MM(130, 85, New RepString(fp_Datos, Me.Model.Email))

            page_Cur.AddLT_MM(15, 90, New RepString(fp_Campo, "Dirección:"))
            page_Cur.AddLT_MM(45, 90, New RepString(fp_Datos, String.Concat(New String() {Me.Model.Address, " No. ", Me.Model.ExternalNumber, " Int - ", Me.Model.InternalNumber})))

            page_Cur.AddLT_MM(15, 95, New RepString(fp_Campo, "Colonia: "))
            page_Cur.AddLT_MM(45, 95, New RepString(fp_Datos, Me.Model.Address2))

            page_Cur.AddLT_MM(15, 100, New RepString(fp_Campo, "Ciudad: "))
            page_Cur.AddLT_MM(45, 100, New RepString(fp_Datos, Me.Model.City))
            page_Cur.AddLT_MM(115, 100, New RepString(fp_Campo, "Estado: "))
            page_Cur.AddLT_MM(140, 100, New RepString(fp_Datos, Me.Model.State))

            page_Cur.AddLT_MM(15, 105, New RepString(fp_Campo, "País: "))
            page_Cur.AddLT_MM(45, 105, New RepString(fp_Datos, Me.Model.Country))
            page_Cur.AddLT_MM(85, 105, New RepString(fp_Campo, "C.P. "))
            page_Cur.AddLT_MM(100, 105, New RepString(fp_Datos, Me.Model.ZipCode))
            page_Cur.AddLT_MM(140, 105, New RepString(fp_Campo, "Horario Rec. "))
            page_Cur.AddLT_MM(165, 105, New RepString(fp_Datos, Me.Model.DeliveryTime))

            page_Cur.AddLT_MM(14, 110, New RepString(fp_Campo, "Tel. Casa: "))
            page_Cur.AddLT_MM(45, 110, New RepString(fp_Datos, Me.Model.Telephone))
            page_Cur.AddLT_MM(85, 110, New RepString(fp_Campo, "Tel. Trabajo "))
            page_Cur.AddLT_MM(100, 110, New RepString(fp_Datos, Me.Model.Telephone2))
            page_Cur.AddLT_MM(140, 110, New RepString(fp_Campo, "Celular: "))
            page_Cur.AddLT_MM(165, 110, New RepString(fp_Datos, Me.Model.Telephone3))

            '-- Colocar la información del equipo
            page_Cur.AddLT_MM(15, 120, New RepString(fp_subTitulo, "Información del equipo"))

            page_Cur.AddLT_MM(15, 130, New RepString(fp_Campo, "Clase Producto: "))
            page_Cur.AddLT_MM(45, 130, New RepString(fp_Datos, Me.Model.ProductClass))
            page_Cur.AddLT_MM(105, 130, New RepString(fp_Campo, "Tipo Producto: "))
            page_Cur.AddLT_MM(140, 130, New RepString(fp_Datos, Me.Model.ProductType))

            page_Cur.AddLT_MM(15, 135, New RepString(fp_Campo, "Marca: "))
            page_Cur.AddLT_MM(45, 135, New RepString(fp_Datos, Me.Model.ProductTrademark))
            page_Cur.AddLT_MM(105, 135, New RepString(fp_Campo, "Modelo: "))
            page_Cur.AddLT_MM(140, 135, New RepString(fp_Datos, Me.Model.ProductModel))

            page_Cur.AddLT_MM(15, 140, New RepString(fp_Campo, "Descripción: "))
            page_Cur.AddLT_MM(45, 140, New RepString(fp_Datos, Me.Model.productDescription))

            page_Cur.AddLT_MM(15, 145, New RepString(fp_Campo, "Numero de parte: "))
            page_Cur.AddLT_MM(45, 145, New RepString(fp_Datos, Me.Model.PartNumber))
            page_Cur.AddLT_MM(105, 145, New RepString(fp_Campo, "Numero de serie: "))
            page_Cur.AddLT_MM(140, 145, New RepString(fp_Datos, Me.Model.SerialNumber))

            page_Cur.AddLT_MM(15, 155, New RepString(fp_Campo, "Clase servicio: "))
            page_Cur.AddLT_MM(45, 155, New RepString(fp_Datos, Me.Model.ServiceType))

            page_Cur.AddLT_MM(15, 160, New RepString(fp_Campo, "Falla presentada: "))
            '------------------------
            Dim stra As String
            Dim line As Integer = 0
            Dim posY As Integer = 160
            Dim lt As String
            Dim fr As Integer = 65

            While (line * fr) + 1 < Len(Model.FailureType)
                lt = Mid(Model.FailureType, (line * fr) + 1, 1)
                If lt <> " " Then lt = "_ "
                stra = Mid(Model.FailureType, (line * fr) + 1, fr) & lt
                page_Cur.AddLT_MM(45, posY, New RepString(fp_Datos, stra))
                line += 1
                posY += 5
            End While

            '--- Agregar Reparación
            page_Cur.AddLT_MM(15, 175, New RepString(fp_Campo, "Reparación a realizar: "))

            stra = ""
            lt = ""
            line = 0
            posY = 175
            fr = 55

            While (line * fr) + 1 < Len(Model.Reparacion)
                lt = Mid(Model.Reparacion, (line * fr) + 1, 1)
                If lt <> " " Then lt = "_ "
                stra = Mid(Model.Reparacion, (line * fr) + 1, fr) & lt
                page_Cur.AddLT_MM(60, posY, New RepString(fp_Datos, stra))
                line += 1
                posY += 5
            End While

            '-- Agregar los comentarios, (Dentro del cuadro correspondiente)
            stra = ""
            lt = ""
            line = 0
            posY = 198
            fr = 80

            While (line * fr) + 1 < Len(Model.Comentarios)
                lt = Mid(Model.Comentarios, (line * fr) + 1, 1)
                If lt <> " " Then lt = "_ "
                stra = Mid(Model.Comentarios, (line * fr) + 1, fr) & lt
                page_Cur.AddLT_MM(15, posY, New RepString(fp_Datos, stra))
                line += 1
                posY += 5
            End While

            'page_Cur.AddLT_MM(45, 160, New RepString(fp_Datos, Me.Model.FailureType))
            Select Case Counter
                Case 0
                    msg = "Copia para el cliente"
                Case 1
                    msg = "Copia Administración"
                Case 2
                    msg = "Copia para Mensajería"
            End Select


            page_Cur.AddLT_MM(160, 280, New RepString(fp_Datos, msg))
            page_Cur.AddLT_MM(15, 241, New RepString(fp_Datos, "Estoy de acuerdo en el funcionamiento y reparación de mi equipo"))
            Counter += 1
        Loop

    End Sub


    ' Properties
    Public Property IsCopy As Boolean
        Get
            Return Me._IsCopy
        End Get
        Set(ByVal value As Boolean)
            Me._IsCopy = value
        End Set
    End Property

    Public Property Model As DeliveryPageModel
        Get
            Return Me._Model
        End Get
        Set(ByVal value As DeliveryPageModel)
            Me._Model = value
        End Set
    End Property

    Public Property Orderid As String
        Get
            Return Me._OrderID
        End Get
        Set(ByVal value As String)
            Me._OrderID = value
        End Set
    End Property

    Public Property RutaBarCode As String
        Get
            Return Me._rutaBarCode
        End Get
        Set(ByVal value As String)
            Me._rutaBarCode = value
        End Set
    End Property

    Public Property RutaComments As String
        Get
            Return Me._rutaComments
        End Get
        Set(ByVal value As String)
            Me._rutaComments = value
        End Set
    End Property

    Public Property RutaLogo As String
        Get
            Return Me._rutaLogo
        End Get
        Set(ByVal value As String)
            Me._rutaLogo = value
        End Set
    End Property


    ' Fields
    Private _IsCopy As Boolean
    Private _Model As DeliveryPageModel
    Private _OrderID As String
    Private _rutaBarCode As String
    Private _rutaComments As String
    Private _rutaLogo As String
End Class


' 2013.04.18
Public Class PDFCotización
    Inherits Report
    ' Methods
    Protected Overrides Sub Create()
        Dim Counter As Integer = 0
        Dim msg As String = String.Empty

        ' -- Declarar los tipos de letra
        Dim fdTimes As New FontDef(Me, StandardFont.TimesRoman)

        '-- Declarar los estilos
        Dim fp_Titulo As New FontProp(fdTimes, 12, Color.Black) With {.bBold = True}
        Dim fp_Contacto As New FontProp(fdTimes, 6, Color.Black)
        Dim fp_subTitulo As New FontProp(fdTimes, 8, Color.Black) With {.bBold = True}
        Dim fp_Campo As New FontProp(fdTimes, 7, Color.Black) With {.bBold = True}
        Dim fp_Datos As New FontProp(fdTimes, 7, Color.Black)

        Do While Counter < 3
            '-- Crar nueva página
            page_Cur = New Page(Me)

            '-- Colocar el logo
            Dim stream As Stream = New FileStream(Me._rutaLogo, FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(20, 30, New RepImageMM(stream, 40, Double.NaN))
            '-- Colocar el código de barras
            stream = New FileStream(Me._rutaBarCode, FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(100, 50, New RepImageMM(stream, 100, Double.NaN))

            '-- Colocar cuadro de firmas / Deshabilitado por el momento
            stream = New FileStream(Me._rutaComments, FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(5, 270, New RepImageMM(stream, 204.5, Double.NaN))

            '-- Colocar el título
            page_Cur.AddLT_MM(100, 21, New RepString(fp_Titulo, "Formato de entrega de equipo"))

            '-- Colocar los datos de la empresa
            page_Cur.AddLT_MM(15, 36, New RepString(fp_Contacto, "Agiotech de México S.A. de C.V."))
            page_Cur.AddLT_MM(15, 40, New RepString(fp_Contacto, "Parque Industrial City Park"))
            page_Cur.AddLT_MM(15, 44, New RepString(fp_Contacto, "Av. Aviación 5051 Nave 8"))
            page_Cur.AddLT_MM(15, 48, New RepString(fp_Contacto, "Col. San Juan de Ocotán"))
            page_Cur.AddLT_MM(15, 52, New RepString(fp_Contacto, "Zapopan, Jalisco C.P. 45019"))

            '-- Colocar el OrderID
            page_Cur.AddLT_MM(130, 52, New RepString(fp_Datos, Me.Orderid))

            '-- Colocar información del cliente
            page_Cur.AddLT_MM(15, 65, New RepString(fp_subTitulo, "Información del cliente"))

            page_Cur.AddLT_MM(15, 75, New RepString(fp_Campo, "Nombre:"))
            page_Cur.AddLT_MM(45, 75, New RepString(fp_Datos, Me.Model.CustomerName))

            page_Cur.AddLT_MM(15, 80, New RepString(fp_Campo, "Razon Social:"))
            page_Cur.AddLT_MM(45, 80, New RepString(fp_Datos, Me.Model.RazonSocial))

            page_Cur.AddLT_MM(15, 85, New RepString(fp_Campo, "RFC:"))
            page_Cur.AddLT_MM(45, 85, New RepString(fp_Datos, Me.Model.RFC))
            page_Cur.AddLT_MM(105, 85, New RepString(fp_Campo, "Email: "))
            page_Cur.AddLT_MM(130, 85, New RepString(fp_Datos, Me.Model.Email))

            page_Cur.AddLT_MM(15, 90, New RepString(fp_Campo, "Dirección:"))
            page_Cur.AddLT_MM(45, 90, New RepString(fp_Datos, String.Concat(New String() {Me.Model.Address, " No. ", Me.Model.ExternalNumber, " Int - ", Me.Model.InternalNumber})))

            page_Cur.AddLT_MM(15, 95, New RepString(fp_Campo, "Colonia: "))
            page_Cur.AddLT_MM(45, 95, New RepString(fp_Datos, Me.Model.Address2))

            page_Cur.AddLT_MM(15, 100, New RepString(fp_Campo, "Ciudad: "))
            page_Cur.AddLT_MM(45, 100, New RepString(fp_Datos, Me.Model.City))
            page_Cur.AddLT_MM(115, 100, New RepString(fp_Campo, "Estado: "))
            page_Cur.AddLT_MM(140, 100, New RepString(fp_Datos, Me.Model.State))

            page_Cur.AddLT_MM(15, 105, New RepString(fp_Campo, "País: "))
            page_Cur.AddLT_MM(45, 105, New RepString(fp_Datos, Me.Model.Country))
            page_Cur.AddLT_MM(85, 105, New RepString(fp_Campo, "C.P. "))
            page_Cur.AddLT_MM(100, 105, New RepString(fp_Datos, Me.Model.ZipCode))
            page_Cur.AddLT_MM(140, 105, New RepString(fp_Campo, "Horario Rec. "))
            page_Cur.AddLT_MM(165, 105, New RepString(fp_Datos, Me.Model.DeliveryTime))

            page_Cur.AddLT_MM(14, 110, New RepString(fp_Campo, "Tel. Casa: "))
            page_Cur.AddLT_MM(45, 110, New RepString(fp_Datos, Me.Model.Telephone))
            page_Cur.AddLT_MM(85, 110, New RepString(fp_Campo, "Tel. Trabajo "))
            page_Cur.AddLT_MM(100, 110, New RepString(fp_Datos, Me.Model.Telephone2))
            page_Cur.AddLT_MM(140, 110, New RepString(fp_Campo, "Celular: "))
            page_Cur.AddLT_MM(165, 110, New RepString(fp_Datos, Me.Model.Telephone3))

            '-- Colocar la información del equipo
            page_Cur.AddLT_MM(15, 120, New RepString(fp_subTitulo, "Información del equipo"))

            page_Cur.AddLT_MM(15, 130, New RepString(fp_Campo, "Clase Producto: "))
            page_Cur.AddLT_MM(45, 130, New RepString(fp_Datos, Me.Model.ProductClass))
            page_Cur.AddLT_MM(105, 130, New RepString(fp_Campo, "Tipo Producto: "))
            page_Cur.AddLT_MM(140, 130, New RepString(fp_Datos, Me.Model.ProductType))

            page_Cur.AddLT_MM(15, 135, New RepString(fp_Campo, "Marca: "))
            page_Cur.AddLT_MM(45, 135, New RepString(fp_Datos, Me.Model.ProductTrademark))
            page_Cur.AddLT_MM(105, 135, New RepString(fp_Campo, "Modelo: "))
            page_Cur.AddLT_MM(140, 135, New RepString(fp_Datos, Me.Model.ProductModel))

            page_Cur.AddLT_MM(15, 140, New RepString(fp_Campo, "Descripción: "))
            page_Cur.AddLT_MM(45, 140, New RepString(fp_Datos, Me.Model.productDescription))

            page_Cur.AddLT_MM(15, 145, New RepString(fp_Campo, "Numero de parte: "))
            page_Cur.AddLT_MM(45, 145, New RepString(fp_Datos, Me.Model.PartNumber))
            page_Cur.AddLT_MM(105, 145, New RepString(fp_Campo, "Numero de serie: "))
            page_Cur.AddLT_MM(140, 145, New RepString(fp_Datos, Me.Model.SerialNumber))

            page_Cur.AddLT_MM(15, 155, New RepString(fp_Campo, "Clase servicio: "))
            page_Cur.AddLT_MM(45, 155, New RepString(fp_Datos, Me.Model.ServiceType))

            page_Cur.AddLT_MM(15, 160, New RepString(fp_Campo, "Falla presentada: "))
            '------------------------
            Dim stra As String
            Dim line As Integer = 0
            Dim posY As Integer = 160
            Dim lt As String
            Dim fr As Integer = 65

            While (line * fr) + 1 < Len(Model.FailureType)
                lt = Mid(Model.FailureType, (line * fr) + 1, 1)
                If lt <> " " Then lt = "_ "
                stra = Mid(Model.FailureType, (line * fr) + 1, fr) & lt
                page_Cur.AddLT_MM(45, posY, New RepString(fp_Datos, stra))
                line += 1
                posY += 5
            End While

            '--- Agregar Reparación
            page_Cur.AddLT_MM(15, 175, New RepString(fp_Campo, "Reparación a realizar: "))

            stra = ""
            lt = ""
            line = 0
            posY = 175
            fr = 55

            While (line * fr) + 1 < Len(Model.Reparacion)
                lt = Mid(Model.Reparacion, (line * fr) + 1, 1)
                If lt <> " " Then lt = "_ "
                stra = Mid(Model.Reparacion, (line * fr) + 1, fr) & lt
                page_Cur.AddLT_MM(60, posY, New RepString(fp_Datos, stra))
                line += 1
                posY += 5
            End While

            '-- Agregar los comentarios, (Dentro del cuadro correspondiente)
            stra = ""
            lt = ""
            line = 0
            posY = 198
            fr = 80

            While (line * fr) + 1 < Len(Model.Comentarios)
                lt = Mid(Model.Comentarios, (line * fr) + 1, 1)
                If lt <> " " Then lt = "_ "
                stra = Mid(Model.Comentarios, (line * fr) + 1, fr) & lt
                page_Cur.AddLT_MM(15, posY, New RepString(fp_Datos, stra))
                line += 1
                posY += 5
            End While

            'page_Cur.AddLT_MM(45, 160, New RepString(fp_Datos, Me.Model.FailureType))
            Select Case Counter
                Case 0
                    msg = "Copia para el cliente"
                Case 1
                    msg = "Copia Administración"
                Case 2
                    msg = "Copia para Mensajería"
            End Select


            page_Cur.AddRT_MM(200, 270, New RepString(fp_Datos, msg))

            Counter += 1
        Loop

    End Sub


    ' Properties
    Public Property IsCopy As Boolean
        Get
            Return Me._IsCopy
        End Get
        Set(ByVal value As Boolean)
            Me._IsCopy = value
        End Set
    End Property

    Public Property Model As DeliveryPageModel
        Get
            Return Me._Model
        End Get
        Set(ByVal value As DeliveryPageModel)
            Me._Model = value
        End Set
    End Property

    Public Property Orderid As String
        Get
            Return Me._OrderID
        End Get
        Set(ByVal value As String)
            Me._OrderID = value
        End Set
    End Property

    Public Property RutaBarCode As String
        Get
            Return Me._rutaBarCode
        End Get
        Set(ByVal value As String)
            Me._rutaBarCode = value
        End Set
    End Property

    Public Property RutaComments As String
        Get
            Return Me._rutaComments
        End Get
        Set(ByVal value As String)
            Me._rutaComments = value
        End Set
    End Property

    Public Property RutaLogo As String
        Get
            Return Me._rutaLogo
        End Get
        Set(ByVal value As String)
            Me._rutaLogo = value
        End Set
    End Property


    ' Fields
    Private _IsCopy As Boolean
    Private _Model As DeliveryPageModel
    Private _OrderID As String
    Private _rutaBarCode As String
    Private _rutaComments As String
    Private _rutaLogo As String
End Class

' 2013.07.15
Public Class PDFReciboEquipo
    Inherits Report
    ' Methods
    Protected Overrides Sub Create()
        Dim Counter As Integer = 0
        Dim msg As String = String.Empty

        ' -- Declarar los tipos de letra
        Dim fdTimes As New FontDef(Me, StandardFont.TimesRoman)
        Dim fdHelv As New FontDef(Me, StandardFont.Helvetica)
        Dim fdCourier As New FontDef(Me, StandardFont.Courier)

        '-- Declarar los estilos
        Dim fp_TituloFormato As New FontProp(fdHelv, 8, Color.Black) With {.bBold = True}
        Dim fp_OrderID As New FontProp(fdHelv, 14, Color.Black) With {.bBold = False}
        Dim fp_NotificacionBold As New FontProp(fdHelv, 6, Color.Black) With {.bBold = True}
        Dim fp_Notificacion As New FontProp(fdHelv, 6, Color.Black) With {.bBold = False}
        Dim fp_data As New FontProp(fdHelv, 6, Color.Black) With {.bBold = False, .bUnderline = False}

        Dim fp_Titulo As New FontProp(fdTimes, 12, Color.Black) With {.bBold = True}
        Dim fp_Contacto As New FontProp(fdTimes, 5, Color.Black)
        Dim fp_subTitulo As New FontProp(fdTimes, 8, Color.Black) With {.bBold = True}
        Dim fp_Campo As New FontProp(fdTimes, 7, Color.Black) With {.bBold = True}
        Dim fp_Datos As New FontProp(fdTimes, 7, Color.Black)

        Do While Counter < 2
            '-- Crear nueva página
            page_Cur = New Page(Me)

            '-- Colocar los logos
            'Logo Agiotech
            Dim stream As Stream = New FileStream(Me._Pathimages & "/" & Me.Logo, FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(7, 25, New RepImageMM(stream, 25, Double.NaN))

            ' Separador
            stream = New FileStream(Me._Pathimages & "/img_vline.jpg", FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(35, 25, New RepImageMM(stream, 5, Double.NaN))

            ' Logo del cliente
            stream = New FileStream(Me._Pathimages & "/" & _LogoCliente, FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(40, 25, New RepImageMM(stream, 25, Double.NaN))

            ' Recuedro, falla reportada
            stream = New FileStream(Me._Pathimages & "/" & "img_rec1.jpg", FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(10, 126, New RepImageMM(stream, 190, Double.NaN))

            'Recuadro Inspección
            stream = New FileStream(Me._Pathimages & "/" & Me._imgInspeccion, FileMode.Open, FileAccess.Read)
            If _imgInspeccion <> "img_rec3.jpg" Then
                page_Cur.AddMM(-10, 178, New RepImageMM(stream, 220, Double.NaN))
            Else
                page_Cur.AddMM(10, 176, New RepImageMM(stream, 190, Double.NaN))
            End If

            'Recuadro Estado Físico (Cosmetic)
            stream = New FileStream(Me._Pathimages & "/" & "img_rec3.jpg", FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(10, 225, New RepImageMM(stream, 190, Double.NaN))

            'Recuadro Accesorios / Documentos
            stream = New FileStream(Me._Pathimages & "/" & "img_rec3.jpg", FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(10, 268, New RepImageMM(stream, 190, Double.NaN))

            ' Lineas de firma
            stream = New FileStream(Me._Pathimages & "/" & "img_line.jpg", FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(25, 281, New RepImageMM(stream, 45, Double.NaN))

            stream = New FileStream(Me._Pathimages & "/" & "img_line.jpg", FileMode.Open, FileAccess.Read)
            page_Cur.AddMM(130, 281, New RepImageMM(stream, 45, Double.NaN))

            'Titulo del documento
            page_Cur.AddLT_MM(130, 5, New RepString(fp_TituloFormato, "Formato de Recepción-Inspección"))

            '-- Colocar el OrderID
            page_Cur.AddLT_MM(130, 11, New RepString(fp_OrderID, Me.Orderid))

            'Notificación de servicio
            page_Cur.AddLT_MM(130, 20, New RepString(fp_NotificacionBold, "Notificación de Servicio:"))
            page_Cur.AddLT_MM(165, 20, New RepString(fp_Notificacion, Model.NotificacionServicio))

            'Fecha
            page_Cur.AddLT_MM(130, 24, New RepString(fp_NotificacionBold, "Fecha:"))
            page_Cur.AddLT_MM(145, 24, New RepString(fp_Notificacion, Model.OrderDate)) 'Format(Now, "dd/MM/yyy")

            '-- Colocar información del cliente
            page_Cur.AddLT_MM(15, 30, New RepString(fp_subTitulo, "Información del cliente"))

            page_Cur.AddLT_MM(15, 36, New RepString(fp_NotificacionBold, "Tipo de Cliente:"))
            page_Cur.AddLT_MM(40, 36, New RepString(fp_data, Me.Model.CustomerType))

            page_Cur.AddLT_MM(110, 36, New RepString(fp_NotificacionBold, "Num. Cliente: "))
            page_Cur.AddLT_MM(135, 36, New RepString(fp_data, Me.Model.NumCliente))
            '------------------------------------------------------------------------------------
            page_Cur.AddLT_MM(15, 40, New RepString(fp_NotificacionBold, "Nombre:"))
            page_Cur.AddLT_MM(40, 40, New RepString(fp_data, Me.Model.CustomerName))

            page_Cur.AddLT_MM(110, 40, New RepString(fp_NotificacionBold, "Email: "))
            page_Cur.AddLT_MM(135, 40, New RepString(fp_data, Me.Model.Email))
            '------------------------------------------------------------------------------------
            page_Cur.AddLT_MM(15, 44, New RepString(fp_NotificacionBold, "Razon Social:"))
            page_Cur.AddLT_MM(40, 44, New RepString(fp_data, Me.Model.RazonSocial))

            page_Cur.AddLT_MM(110, 44, New RepString(fp_NotificacionBold, "Colonia: "))
            page_Cur.AddLT_MM(135, 44, New RepString(fp_data, Me.Model.Address2))
            '------------------------------------------------------------------------------------
            page_Cur.AddLT_MM(15, 48, New RepString(fp_NotificacionBold, "RFC:"))
            page_Cur.AddLT_MM(40, 48, New RepString(fp_data, Me.Model.RFC))

            page_Cur.AddLT_MM(110, 48, New RepString(fp_NotificacionBold, "Ciudad: "))
            page_Cur.AddLT_MM(135, 48, New RepString(fp_data, Me.Model.City))
            '------------------------------------------------------------------------------------
            page_Cur.AddLT_MM(15, 52, New RepString(fp_NotificacionBold, "Calle:"))
            page_Cur.AddLT_MM(40, 52, New RepString(fp_data, Me.Model.Address))

            page_Cur.AddLT_MM(110, 52, New RepString(fp_NotificacionBold, "Estado: "))
            page_Cur.AddLT_MM(135, 52, New RepString(fp_data, Me.Model.State))
            '------------------------------------------------------------------------------------
            page_Cur.AddLT_MM(15, 56, New RepString(fp_NotificacionBold, "Num. exterior:"))
            page_Cur.AddLT_MM(40, 56, New RepString(fp_data, Me.Model.ExternalNumber))

            page_Cur.AddLT_MM(110, 56, New RepString(fp_NotificacionBold, "País: "))
            page_Cur.AddLT_MM(135, 56, New RepString(fp_data, Me.Model.Country))
            '------------------------------------------------------------------------------------
            page_Cur.AddLT_MM(15, 60, New RepString(fp_NotificacionBold, "Num. Interior:"))
            page_Cur.AddLT_MM(40, 60, New RepString(fp_data, Me.Model.InternalNumber))

            page_Cur.AddLT_MM(110, 60, New RepString(fp_NotificacionBold, "C.P. "))
            page_Cur.AddLT_MM(135, 60, New RepString(fp_data, Me.Model.ZipCode))
            '------------------------------------------------------------------------------------
            page_Cur.AddLT_MM(15, 64, New RepString(fp_NotificacionBold, "Tel. Casa: "))
            page_Cur.AddLT_MM(40, 64, New RepString(fp_data, Me.Model.Telephone))

            page_Cur.AddLT_MM(110, 64, New RepString(fp_NotificacionBold, "Horario Rec. "))
            page_Cur.AddLT_MM(135, 64, New RepString(fp_data, Me.Model.DeliveryTime))
            '------------------------------------------------------------------------------------
            page_Cur.AddLT_MM(15, 68, New RepString(fp_NotificacionBold, "Tel. Trabajo "))
            page_Cur.AddLT_MM(40, 68, New RepString(fp_data, Me.Model.Telephone2))

            page_Cur.AddLT_MM(110, 68, New RepString(fp_NotificacionBold, "Celular: "))
            page_Cur.AddLT_MM(135, 68, New RepString(fp_data, Me.Model.Telephone3))
            '----------------------------------------------------------------------------------
            page_Cur.AddLT_MM(15, 72, New RepString(fp_NotificacionBold, "Referencias"))
            page_Cur.AddLT_MM(40, 72, New RepString(fp_data, Me.Model.AddressReference))

            '-- Colocar la información del equipo
            If Model.ReRepair Then
                page_Cur.AddLT_MM(15, 78, New RepString(fp_subTitulo, "Información del equipo [ REINCIDENCIA ]"))
            Else
                page_Cur.AddLT_MM(15, 78, New RepString(fp_subTitulo, "Información del equipo"))
            End If

            page_Cur.AddLT_MM(15, 86, New RepString(fp_NotificacionBold, "Clase Producto: "))
            page_Cur.AddLT_MM(40, 86, New RepString(fp_Notificacion, Me.Model.ProductClass))

            page_Cur.AddLT_MM(110, 86, New RepString(fp_NotificacionBold, "Tipo Producto: "))
            page_Cur.AddLT_MM(135, 86, New RepString(fp_Notificacion, Me.Model.ProductType))
            '--------------------------------------------------------------------------------
            page_Cur.AddLT_MM(15, 90, New RepString(fp_NotificacionBold, "Modelo: "))
            page_Cur.AddLT_MM(40, 90, New RepString(fp_Notificacion, Me.Model.ProductModel))

            page_Cur.AddLT_MM(110, 90, New RepString(fp_NotificacionBold, "Marca: "))
            page_Cur.AddLT_MM(135, 90, New RepString(fp_Notificacion, Me.Model.ProductTrademark))
            '--------------------------------------------------------------------------------
            page_Cur.AddLT_MM(15, 94, New RepString(fp_NotificacionBold, "Numero de parte: "))
            page_Cur.AddLT_MM(40, 94, New RepString(fp_Notificacion, Me.Model.PartNumber))

            page_Cur.AddLT_MM(110, 94, New RepString(fp_NotificacionBold, "Descripción: "))
            page_Cur.AddLT_MM(135, 94, New RepString(fp_Notificacion, Me.Model.productDescription))
            '--------------------------------------------------------------------------------
            page_Cur.AddLT_MM(15, 98, New RepString(fp_NotificacionBold, "Numero de serie: "))
            page_Cur.AddLT_MM(40, 98, New RepString(fp_Notificacion, Me.Model.SerialNumber))

            page_Cur.AddLT_MM(110, 98, New RepString(fp_NotificacionBold, "Revisión: "))
            page_Cur.AddLT_MM(135, 98, New RepString(fp_Notificacion, Me.Model.Revision))
            '-------------------------------------------------------------------------------
            page_Cur.AddLT_MM(15, 102, New RepString(fp_NotificacionBold, "Clase servicio: "))
            page_Cur.AddLT_MM(40, 102, New RepString(fp_Notificacion, Me.Model.ServiceType))

            page_Cur.AddLT_MM(15, 108, New RepString(fp_NotificacionBold, "Falla presentada: "))
            '------------------------
            Dim stra As String
            Dim line As Integer = 0
            Dim posY As Integer = 113
            Dim lt As String
            Dim fr As Integer = 103

            While (line * fr) + 1 < Len(Model.FailureType)

                lt = Mid(Model.FailureType, ((line * fr) + 1), 1)
                'Mid(Model.Cosmetic, (line * 55) + 1, 1)
                'lt2 = Mid(Model.FailureType, (line * fr) + 1, 1)
                If lt <> " " Then lt = "_ "
                stra = Mid(Model.FailureType, (line * fr) + 1, fr) & lt
                page_Cur.AddLT_MM(15, posY, New RepString(fp_Notificacion, UCase(stra)))
                line += 1
                posY += 4
            End While

            page_Cur.AddLT_MM(15, 128, New RepString(fp_NotificacionBold, "Inspección Visual: "))
            page_Cur.AddLT_MM(15, 183, New RepString(fp_NotificacionBold, "Observaciones Estado Físico: "))
            page_Cur.AddLT_MM(15, 188, New RepString(fp_Notificacion, Model.Cosmetic))

            page_Cur.AddLT_MM(15, 226, New RepString(fp_NotificacionBold, "Observaciones Accesorios / Documentos: "))
            page_Cur.AddLT_MM(15, 231, New RepString(fp_Notificacion, Model.Accesories))

            page_Cur.AddLT_MM(37, 281, New RepString(fp_NotificacionBold, "Firma Cliente: "))
            Select Case Model.CustomerType
                Case "EndUser"
                    msg = "Firma Agiofix"
                Case "Agiofix"
                    msg = "Firma Agiofix"
                Case "TWG"
                    msg = "Firma Agiotech"
                Case "ASSURANT"
                    msg = "Firma Agiotech"
                Case Else
                    msg = "Firma Agiofix"
            End Select

            page_Cur.AddLT_MM(143, 281, New RepString(fp_NotificacionBold, msg))
            ' Firmas

            'page_Cur.AddLT_MM(45, 160, New RepString(fp_Datos, Me.Model.FailureType))
            Select Case Counter
                Case 0
                    msg = "Copia para el cliente"
                Case 1
                    If Model.CustomerType = "ASSURANT" Or Model.CustomerType = "TWG" Then
                        msg = "Copia para Agiotech"
                    Else
                        msg = "Copia para AgioFix"
                    End If


                    'Case 2
                    '   msg = "Copia Administración"
            End Select


            page_Cur.AddRT_MM(200, 290, New RepString(fp_Notificacion, msg))

            ' Firma
            page_Cur.AddLT_MM(85, 290, New RepString(fp_Contacto, "Agiotech de México S.A. de C.V. "))
            page_Cur.AddLT_MM(40, 293, New RepString(fp_Contacto, "Parque Industrial City Park Av. Aviación 5051 Nave 8 Col. San Juan de Ocotán Zapopan, Jalisco C.P. 45019"))

            Counter += 1
        Loop
    End Sub


    ' Properties
    Public Property PathImages As String
        Get
            Return _Pathimages
        End Get
        Set(value As String)
            _Pathimages = value
        End Set
    End Property

    Public Property Logo As String
        Get
            Return _Logo
        End Get
        Set(value As String)
            _Logo = value
        End Set
    End Property

    Public Property LogoCliente As String
        Get
            Return _LogoCliente
        End Get
        Set(value As String)
            _LogoCliente = value
        End Set
    End Property

    Public Property imgInspeccion As String
        Get
            Return _imgInspeccion
        End Get
        Set(value As String)
            _imgInspeccion = value
        End Set
    End Property

    Public Property Reincidencia As Boolean
        Get
            Return _Reincidencia
        End Get
        Set(value As Boolean)
            _Reincidencia = value
        End Set
    End Property

    Public Property IsCopy As Boolean
        Get
            Return Me._IsCopy
        End Get
        Set(ByVal value As Boolean)
            Me._IsCopy = value
        End Set
    End Property

    Public Property Model As CreateAssurantOrderModel
        Get
            Return Me._Model
        End Get
        Set(ByVal value As CreateAssurantOrderModel)
            Me._Model = value
        End Set
    End Property

    Public Property Orderid As String
        Get
            Return Me._OrderID
        End Get
        Set(ByVal value As String)
            Me._OrderID = value
        End Set
    End Property


    Public Property RutaBarCode As String
        Get
            Return Me._rutaBarCode
        End Get
        Set(ByVal value As String)
            Me._rutaBarCode = value
        End Set
    End Property

    Public Property RutaComments As String
        Get
            Return Me._rutaComments
        End Get
        Set(ByVal value As String)
            Me._rutaComments = value
        End Set
    End Property

    Public Property RutaLogo As String
        Get
            Return Me._rutaLogo
        End Get
        Set(ByVal value As String)
            Me._rutaLogo = value
        End Set
    End Property


    ' Fields
    Private _Pathimages As String
    Private _Logo As String
    Private _LogoCliente As String
    Private _imgInspeccion As String
    Private _Reincidencia As Boolean

    Private _IsCopy As Boolean
    Private _Model As CreateOrderModel
    Private _OrderID As String

    Private _rutaBarCode As String
    Private _rutaComments As String
    Private _rutaLogo As String

End Class
