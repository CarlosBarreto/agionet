Imports Root.Reports
Imports Root.Reports.FontDef
Imports System.Drawing
Imports System.IO

Public Class PDFInTransitPage
    Inherits Report
    ' Methods
    Protected Overrides Sub Create()
        MyBase.Create()
        Dim fontDef As New FontDef(Me, StandardFont.TimesRoman)
        Dim def As New FontDef(Me, StandardFont.Helvetica)
        Dim fontProp As New FontProp(fontDef, 16, Color.Gray)
        Dim prop2 As New FontProp(def, 8, Color.Black) With { _
            .bBold = True _
        }
        Dim prop As New FontProp(def, 8, Color.Black)
        Dim page As New System.Web.UI.Page()

        MyBase.page_Cur.AddRT_MM(200, 20, New RepString(fontProp, "Formato de Recolección"))
        Dim stream As Stream = New FileStream(Me._rutaLogo, FileMode.Open, FileAccess.Read)
        MyBase.page_Cur.AddMM(20, 30, New RepImageMM(stream, 40, Double.NaN))
        stream = New FileStream(Me._rutaBarCode, FileMode.Open, FileAccess.Read)
        MyBase.page_Cur.AddMM(100, 45, New RepImageMM(stream, 100, Double.NaN))
        stream = New FileStream(Me._rutaComments, FileMode.Open, FileAccess.Read)
        MyBase.page_Cur.AddMM(5, 270, New RepImageMM(stream, 204.5, Double.NaN))
        MyBase.page_Cur.AddLT_MM(20, 40, New RepString(prop2, "Número de Orden:"))
        MyBase.page_Cur.AddLT_MM(55, 40, New RepString(prop, Me.Orderid))
        MyBase.page_Cur.AddLT_MM(150, 45, New RepString(prop, Me.Orderid))
        MyBase.page_Cur.AddLT_MM(20, 65, New RepString(prop2, "Nombre:"))
        MyBase.page_Cur.AddLT_MM(55, 65, New RepString(prop, Me.Model.CustomerName))
        MyBase.page_Cur.AddLT_MM(20, 72, New RepString(prop2, "Razon Social:"))
        MyBase.page_Cur.AddLT_MM(55, 72, New RepString(prop, Me.Model.RazonSocial))
        MyBase.page_Cur.AddLT_MM(20, 79, New RepString(prop2, "RFC:"))
        MyBase.page_Cur.AddLT_MM(55, 79, New RepString(prop, Me.Model.RFC))
        MyBase.page_Cur.AddLT_MM(110, 79, New RepString(prop2, "Email: "))
        MyBase.page_Cur.AddLT_MM(145, 79, New RepString(prop, Me.Model.Email))
        MyBase.page_Cur.AddLT_MM(20, 86, New RepString(prop2, "Dirección:"))
        MyBase.page_Cur.AddLT_MM(55, 86, New RepString(prop, String.Concat(New String() {Me.Model.Address, " No. ", Me.Model.ExternalNumber, " Int - ", Me.Model.InternalNumber})))
        MyBase.page_Cur.AddLT_MM(20, 93, New RepString(prop2, "Colonia: "))
        MyBase.page_Cur.AddLT_MM(55, 93, New RepString(prop, Me.Model.Address2))
        MyBase.page_Cur.AddLT_MM(110, 93, New RepString(prop2, "Ciudad: "))
        MyBase.page_Cur.AddLT_MM(145, 93, New RepString(prop, Me.Model.City))
        MyBase.page_Cur.AddLT_MM(20, 100, New RepString(prop2, "Estado: "))
        MyBase.page_Cur.AddLT_MM(55, 100, New RepString(prop, Me.Model.State))
        MyBase.page_Cur.AddLT_MM(110, 100, New RepString(prop2, "País: "))
        MyBase.page_Cur.AddLT_MM(145, 100, New RepString(prop, Me.Model.Country))
        MyBase.page_Cur.AddLT_MM(20, 107, New RepString(prop2, "C.P. "))
        MyBase.page_Cur.AddLT_MM(55, 107, New RepString(prop, Me.Model.ZipCode))
        MyBase.page_Cur.AddLT_MM(110, 107, New RepString(prop2, "Horario Rec. "))
        MyBase.page_Cur.AddLT_MM(145, 107, New RepString(prop, Me.Model.DeliveryTime))
        MyBase.page_Cur.AddLT_MM(20, 114, New RepString(prop2, "Tel. Casa: "))
        MyBase.page_Cur.AddLT_MM(55, 114, New RepString(prop, Me.Model.Telephone))
        MyBase.page_Cur.AddLT_MM(110, 114, New RepString(prop2, "Tel. Trabajo "))
        MyBase.page_Cur.AddLT_MM(145, 114, New RepString(prop, Me.Model.Telephone2))
        MyBase.page_Cur.AddLT_MM(20, 121, New RepString(prop2, "Celular: "))
        MyBase.page_Cur.AddLT_MM(55, 121, New RepString(prop, Me.Model.Telephone3))
        MyBase.page_Cur.AddLT_MM(20, 135, New RepString(prop2, "Clase Producto: "))
        MyBase.page_Cur.AddLT_MM(55, 135, New RepString(prop, Me.Model.ProductClass))
        MyBase.page_Cur.AddLT_MM(110, 135, New RepString(prop2, "Tipo Producto: "))
        MyBase.page_Cur.AddLT_MM(145, 135, New RepString(prop, Me.Model.ProductType))
        MyBase.page_Cur.AddLT_MM(20, 142, New RepString(prop2, "Marca: "))
        MyBase.page_Cur.AddLT_MM(55, 142, New RepString(prop, Me.Model.ProductTrademark))
        MyBase.page_Cur.AddLT_MM(110, 142, New RepString(prop2, "Modelo: "))
        MyBase.page_Cur.AddLT_MM(145, 142, New RepString(prop, Me.Model.ProductModel))
        MyBase.page_Cur.AddLT_MM(20, 149, New RepString(prop2, "Descripción: "))
        MyBase.page_Cur.AddLT_MM(55, 149, New RepString(prop, Me.Model.productDescription))
        MyBase.page_Cur.AddLT_MM(20, 156, New RepString(prop2, "Numero de parte: "))
        MyBase.page_Cur.AddLT_MM(55, 156, New RepString(prop, Me.Model.PartNumber))
        MyBase.page_Cur.AddLT_MM(110, 156, New RepString(prop2, "Numero de serie: "))
        MyBase.page_Cur.AddLT_MM(145, 156, New RepString(prop, Me.Model.SerialNumber))
        MyBase.page_Cur.AddLT_MM(20, 170, New RepString(prop2, "Clase servicio: "))
        MyBase.page_Cur.AddLT_MM(55, 170, New RepString(prop, Me.Model.ServiceType))
        MyBase.page_Cur.AddLT_MM(20, 177, New RepString(prop2, "Falla presentada: "))
        MyBase.page_Cur.AddLT_MM(55, 177, New RepString(prop, Me.Model.FailureType))
        If Me._IsCopy Then
            MyBase.page_Cur.AddRT_MM(200, 270, New RepString(prop, "Copia para el cliente"))
        Else
            MyBase.page_Cur.AddRT_MM(200, 270, New RepString(prop, "Copia para Mensajería"))
        End If
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
        MyBase.Create()
        Dim fontDef As New FontDef(Me, StandardFont.TimesRoman)
        Dim def As New FontDef(Me, StandardFont.Helvetica)
        Dim fontProp As New FontProp(fontDef, 16, Color.Gray)
        Dim prop2 As New FontProp(def, 8, Color.Black) With { _
            .bBold = True _
        }

        Dim prop As New FontProp(def, 8, Color.Black)
        Dim page As New Page(Me)
        MyBase.page_Cur.AddRT_MM(200, 20, New RepString(fontProp, "Hoja de ingreso"))
        Dim stream As Stream = New FileStream(Me._rutaLogo, FileMode.Open, FileAccess.Read)
        MyBase.page_Cur.AddMM(20, 30, New RepImageMM(stream, 40, Double.NaN))
        stream = New FileStream(Me._rutaBarCode, FileMode.Open, FileAccess.Read)

        MyBase.page_Cur.AddMM(100, 55, New RepImageMM(stream, 100, Double.NaN))
        MyBase.page_Cur.AddLT_MM(20, 45, New RepString(prop2, "Número de Orden:"))
        MyBase.page_Cur.AddLT_MM(55, 45, New RepString(prop, Model.Orderid))
        MyBase.page_Cur.AddLT_MM(150, 55, New RepString(prop, Model.Orderid))
        MyBase.page_Cur.AddLT_MM(20, 65, New RepString(prop2, "Número de Serie:"))
        MyBase.page_Cur.AddLT_MM(55, 65, New RepString(prop, Model.SerialNo))
        MyBase.page_Cur.AddLT_MM(20, 72, New RepString(prop2, "Número de Parte:"))
        MyBase.page_Cur.AddLT_MM(55, 72, New RepString(prop, Model.PartNo))
        MyBase.page_Cur.AddLT_MM(20, 79, New RepString(prop2, "Modelo:"))
        MyBase.page_Cur.AddLT_MM(55, 79, New RepString(prop, Model.Model))
        MyBase.page_Cur.AddLT_MM(20, 86, New RepString(prop2, "Track No:"))
        MyBase.page_Cur.AddLT_MM(55, 86, New RepString(prop, Model.TrackNo))
        MyBase.page_Cur.AddLT_MM(20, 93, New RepString(prop2, "Fecha Ingreso: "))
        MyBase.page_Cur.AddLT_MM(55, 93, New RepString(prop, Model.ScanDate))
        MyBase.page_Cur.AddLT_MM(110, 93, New RepString(prop2, "Ingresado por: "))
        MyBase.page_Cur.AddLT_MM(145, 93, New RepString(prop, Model.ScanBy))
        MyBase.page_Cur.AddLT_MM(20, 100, New RepString(prop2, "Tipo Empaque: "))
        MyBase.page_Cur.AddLT_MM(55, 100, New RepString(prop, Model.PackType))
        MyBase.page_Cur.AddLT_MM(110, 100, New RepString(prop2, "Empaque Dañado: "))
        MyBase.page_Cur.AddLT_MM(145, 100, New RepString(prop, Model.PackDamage))
        MyBase.page_Cur.AddLT_MM(20, 107, New RepString(prop2, "Daño no documentado: "))
        MyBase.page_Cur.AddLT_MM(65, 107, New RepString(prop, Model.NonDoucumentDamage))
        MyBase.page_Cur.AddLT_MM(110, 107, New RepString(prop2, "Empacado Correctamente "))
        MyBase.page_Cur.AddLT_MM(165, 107, New RepString(prop, Model.CorrectPack))
        MyBase.page_Cur.AddLT_MM(20, 114, New RepString(prop2, "Accesorios: "))
        MyBase.page_Cur.AddLT_MM(55, 114, New RepString(prop, Model.Accesories))
        MyBase.page_Cur.AddLT_MM(20, 135, New RepString(prop2, "Cosmetico / Observaciones: "))
        MyBase.page_Cur.AddLT_MM(55, 142, New RepString(prop, Model.Cosmetic))
        MyBase.page_Cur.AddLT_MM(20, 156, New RepString(prop2, "Garantía: "))
        MyBase.page_Cur.AddLT_MM(55, 156, New RepString(prop, Model.Warranty))
        MyBase.page_Cur.AddLT_MM(110, 156, New RepString(prop2, "Re repair: "))
        MyBase.page_Cur.AddLT_MM(145, 156, New RepString(prop, Model.ReRepair))
        MyBase.page_Cur.AddLT_MM(20, 163, New RepString(prop2, "Comentarios: "))
        MyBase.page_Cur.AddLT_MM(55, 163, New RepString(prop, Model.Comment))
        MyBase.page_Cur.AddLT_MM(20, 177, New RepString(prop2, "Falla presentada: "))
        MyBase.page_Cur.AddLT_MM(55, 177, New RepString(prop, Model.ReportedFailure))
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



