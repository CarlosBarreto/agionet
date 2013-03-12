Imports Root.Reports
Imports Root.Reports.FontDef
Imports System.Drawing
Imports System.IO

Public Class PDFInTransitPage
    Inherits Report
    ' Methods
    Protected Overrides Sub Create()

        Dim fontDef As New FontDef(Me, StandardFont.TimesRoman)
        Dim def As New FontDef(Me, StandardFont.Helvetica)
        Dim fontProp As New FontProp(fontDef, 16, Color.Gray)
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
        page.AddRT_MM(200, 20, New RepString(fontProp, "Hoja de ingreso"))
        Dim stream As Stream = New FileStream(Me._rutaLogo, FileMode.Open, FileAccess.Read)
        page.AddMM(20, 30, New RepImageMM(stream, 40, Double.NaN))
        stream = New FileStream(Me._rutaBarCode, FileMode.Open, FileAccess.Read)

        page.AddMM(100, 55, New RepImageMM(stream, 100, Double.NaN))
        page.AddLT_MM(20, 45, New RepString(prop2, "Número de Orden:"))
        page.AddLT_MM(55, 45, New RepString(prop, Model.Orderid))
        page.AddLT_MM(150, 55, New RepString(prop, Model.Orderid))
        page.AddLT_MM(20, 65, New RepString(prop2, "Número de Serie:"))
        page.AddLT_MM(55, 65, New RepString(prop, Model.SerialNo))
        page.AddLT_MM(20, 72, New RepString(prop2, "Número de Parte:"))
        page.AddLT_MM(55, 72, New RepString(prop, Model.PartNo))
        page.AddLT_MM(20, 79, New RepString(prop2, "Modelo:"))
        page.AddLT_MM(55, 79, New RepString(prop, Model.Model))
        page.AddLT_MM(20, 86, New RepString(prop2, "Track No:"))
        page.AddLT_MM(55, 86, New RepString(prop, Model.TrackNo))
        page.AddLT_MM(20, 93, New RepString(prop2, "Fecha Ingreso: "))
        page.AddLT_MM(55, 93, New RepString(prop, Model.ScanDate))
        page.AddLT_MM(110, 93, New RepString(prop2, "Ingresado por: "))
        page.AddLT_MM(145, 93, New RepString(prop, Model.ScanBy))
        page.AddLT_MM(20, 100, New RepString(prop2, "Tipo Empaque: "))
        page.AddLT_MM(55, 100, New RepString(prop, Model.PackType))
        page.AddLT_MM(110, 100, New RepString(prop2, "Empaque Dañado: "))
        page.AddLT_MM(145, 100, New RepString(prop, Model.PackDamage))
        page.AddLT_MM(20, 107, New RepString(prop2, "Daño no documentado: "))
        page.AddLT_MM(65, 107, New RepString(prop, Model.NonDoucumentDamage))
        page.AddLT_MM(110, 107, New RepString(prop2, "Empacado Correctamente "))
        page.AddLT_MM(165, 107, New RepString(prop, Model.CorrectPack))
        page.AddLT_MM(20, 114, New RepString(prop2, "Accesorios: "))
        page.AddLT_MM(55, 114, New RepString(prop, Model.Accesories))
        page.AddLT_MM(20, 135, New RepString(prop2, "Cosmetico / Observaciones: "))
        page.AddLT_MM(55, 142, New RepString(prop, Model.Cosmetic))
        page.AddLT_MM(20, 156, New RepString(prop2, "Garantía: "))
        page.AddLT_MM(55, 156, New RepString(prop, Model.Warranty))
        page.AddLT_MM(110, 156, New RepString(prop2, "Re repair: "))
        page.AddLT_MM(145, 156, New RepString(prop, Model.ReRepair))
        page.AddLT_MM(20, 163, New RepString(prop2, "Comentarios: "))
        page.AddLT_MM(55, 163, New RepString(prop, Model.Comment))
        page.AddLT_MM(20, 177, New RepString(prop2, "Falla presentada: "))
        page.AddLT_MM(55, 177, New RepString(prop, Model.ReportedFailure))
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

        Dim FontDef As New FontDef(Me, StandardFont.TimesRoman)
        Dim fp_Titulo1 As New FontProp(FontDef, 16, Color.Black) With { _
            .bBold = True _
        }


        Dim def As New FontDef(Me, StandardFont.Helvetica)
        Dim fontProp As New FontProp(fontDef, 16, Color.Gray)
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

