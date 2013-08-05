Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

'2013.02.14 Upd 2013.05.09 -  
Public Class PendingOrdersModel
    ' Properties
    Public Property Customer As String
        Get
            Return Me._Customer
        End Get
        Set(ByVal value As String)
            Me._Customer = value
        End Set
    End Property

    Public Property PartNo As String
        Get
            Return _PartNo
        End Get
        Set(value As String)
            _PartNo = value
        End Set
    End Property

    Public Property Description As String
        Get
            Return Me._Description
        End Get
        Set(ByVal value As String)
            Me._Description = value
        End Set
    End Property

    Public Property Model As String
        Get
            Return Me._Model
        End Get
        Set(ByVal value As String)
            Me._Model = value
        End Set
    End Property

    Public Property OrderID As String
        Get
            Return Me._OrderID
        End Get
        Set(ByVal value As String)
            Me._OrderID = value
        End Set
    End Property

    Public Property ProductType As String
        Get
            Return Me._ProductType
        End Get
        Set(ByVal value As String)
            Me._ProductType = value
        End Set
    End Property

    Public Property SerialNo As String
        Get
            Return Me._SerialNo
        End Get
        Set(ByVal value As String)
            Me._SerialNo = value
        End Set
    End Property

    Public Property DateR As String
        Get
            Return _DateR
        End Get
        Set(value As String)
            _DateR = value
        End Set
    End Property

    Public Property DateD As String
        Get
            Return _DateD
        End Get
        Set(value As String)
            _DateD = value
        End Set
    End Property

    Public Property CreateBy As String
        Get
            Return _CreateBy
        End Get
        Set(value As String)
            _CreateBy = value
        End Set
    End Property

    '--- Campos adicionales
    Public Property DateI As String
        Get
            Return _DateI
        End Get
        Set(value As String)
            _DateI = value
        End Set
    End Property

    Public Property DateComp As String
        Get
            Return _DateComp
        End Get
        Set(value As String)
            _DateComp = value
        End Set
    End Property

    Public Property DiasTranscurridos As Integer
        Get
            Return _DiasTRanscurridos
        End Get
        Set(value As Integer)
            _DiasTRanscurridos = value
        End Set
    End Property

    ' Fields
    Private _Customer As String
    Private _PartNo As String
    Private _Description As String
    Private _Model As String
    Private _OrderID As String
    Private _ProductType As String
    Private _SerialNo As String
    Private _DateR As String
    Private _DateI As String
    Private _DateD As String
    Private _CreateBy As String

    Private _DateComp As String '-- Fecha compromiso
    Private _DiasTRanscurridos As String
End Class

' 2013.02.14
Public Class ReceiveIndfoModel
    ' Properties
    Public Property Accesories As String
        Get
            Return Me._Accesories
        End Get
        Set(ByVal value As String)
            Me._Accesories = value
        End Set
    End Property

    Public Property Comment As String
        Get
            Return Me._Comment
        End Get
        Set(ByVal value As String)
            Me._Comment = value
        End Set
    End Property

    Public Property CorrectPack As String
        Get
            Return Me._CorrectPack
        End Get
        Set(ByVal value As String)
            Me._CorrectPack = value
        End Set
    End Property

    Public Property Cosmetic As String
        Get
            Return Me._Cosmetic
        End Get
        Set(ByVal value As String)
            Me._Cosmetic = value
        End Set
    End Property

    Public Property NonDocPack As String
        Get
            Return Me._NonDocPack
        End Get
        Set(ByVal value As String)
            Me._NonDocPack = value
        End Set
    End Property

    Public Property OrderID As String
        Get
            Return Me._OrderID
        End Get
        Set(ByVal value As String)
            Me._OrderID = value
        End Set
    End Property

    Public Property PackDamage As String
        Get
            Return Me._PackDamage
        End Get
        Set(ByVal value As String)
            Me._PackDamage = value
        End Set
    End Property

    Public Property PackType As String
        Get
            Return Me._PackType
        End Get
        Set(ByVal value As String)
            Me._PackType = value
        End Set
    End Property

    Public Property rerepair As String
        Get
            Return Me._ReRepair
        End Get
        Set(ByVal value As String)
            Me._ReRepair = value
        End Set
    End Property

    Public Property Warranty As String
        Get
            Return Me._Warranty
        End Get
        Set(ByVal value As String)
            Me._Warranty = value
        End Set
    End Property


    ' Fields
    Private _Accesories As String
    Private _Comment As String
    Private _CorrectPack As String
    Private _Cosmetic As String
    Private _NonDocPack As String
    Private _OrderID As String
    Private _PackDamage As String
    Private _PackType As String
    Private _ReRepair As String
    Private _Warranty As String
End Class

' 2013.02.14
Public Class ScanInfoModel
    ' Properties
    Public Property Model As String
        Get
            Return Me._Model
        End Get
        Set(ByVal value As String)
            Me._Model = value
        End Set
    End Property

    <Required> _
    Public Property OrderID As String
        Get
            Return Me._OrderID
        End Get
        Set(ByVal value As String)
            Me._OrderID = value
        End Set
    End Property

    Public Property SKU As String
        Get
            Return _SKU
        End Get
        Set(value As String)
            _SKU = value
        End Set
    End Property

    Public Property PartNo As String
        Get
            Return Me._PartNo
        End Get
        Set(ByVal value As String)
            Me._PartNo = value
        End Set
    End Property

    Public Property ScanBy As String
        Get
            Return Me._ScanBy
        End Get
        Set(ByVal value As String)
            Me._ScanBy = value
        End Set
    End Property

    Public Property ScanDate As String
        Get
            Return Me._ScanDate
        End Get
        Set(ByVal value As String)
            Me._ScanDate = value
        End Set
    End Property

    Public Property SerialNo As String
        Get
            Return Me._SerialNo
        End Get
        Set(ByVal value As String)
            Me._SerialNo = value
        End Set
    End Property

    Public Property Marca As String
        Get
            Return _Marca
        End Get
        Set(value As String)
            _Marca = value
        End Set
    End Property

    ' Fields
    Private _Model As String
    Private _OrderID As String
    Private _SKU As String
    Private _PartNo As String
    Private _ScanBy As String
    Private _ScanDate As String
    Private _SerialNo As String
    Private _Marca As String
End Class

'2013.02.26
Public Class CheckinReportOptModel
    Private _OrderID As String
    Private _StartDate As String
    Private _EndDate As String

    Public Property OrderID As String
        Get
            If IsNothing(_OrderID) Then _OrderID = ""
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property StartDate As String
        Get
            If _StartDate = "" Then _StartDate = Format(Now, "dd/MM/yyyy")
            Return _StartDate
        End Get
        Set(value As String)
            _StartDate = value
        End Set
    End Property

    Public Property EndDate As String
        Get
            If _EndDate = "" Then _EndDate = Format(Now, "dd/MM/yyyy")
            Return _EndDate
        End Get
        Set(value As String)
            _EndDate = value
        End Set
    End Property
End Class

'2013.02.26
Public Class CheckinReportModel
    Private _OrderID As String
    Private _OrderDate As String
    Private _FechaIngreso As String
    Private _Guia As String
    Private _Longitud As String
    Private _Ancho As String
    Private _Altura As String
    Private _PesoEmbarque As String
    Private _Comment As String
    Private _CreateBy As String

    '--- properties
    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property OrderDate As String
        Get
            Return _OrderDate
        End Get
        Set(value As String)
            _OrderDate = value
        End Set
    End Property

    Public Property FechaIngreso As String
        Get
            Return _FechaIngreso
        End Get
        Set(value As String)
            _FechaIngreso = value
        End Set
    End Property

    Public Property Guia As String
        Get
            Return _Guia
        End Get
        Set(value As String)
            _Guia = value
        End Set
    End Property

    Public Property Longitud As String
        Get
            Return _Longitud
        End Get
        Set(value As String)
            _Longitud = value
        End Set
    End Property

    Public Property Ancho As String
        Get
            Return _Ancho
        End Get
        Set(value As String)
            _Ancho = value
        End Set
    End Property

    Public Property Altura As String
        Get
            Return _Altura
        End Get
        Set(value As String)
            _Altura = value
        End Set
    End Property

    Public Property PesoEmbarque As String
        Get
            Return _PesoEmbarque
        End Get
        Set(value As String)
            _PesoEmbarque = value
        End Set
    End Property

    Public Property Comment As String
        Get
            Return _Comment
        End Get
        Set(value As String)
            _Comment = value
        End Set
    End Property

    Public Property CreateBy As String
        Get
            Return _CreateBy
        End Get
        Set(value As String)
            _CreateBy = value
        End Set
    End Property
End Class

' 2013.03.11
Public Class setCheckinModel
    Private _OrderID As String
    Private _Comment As String
    ' Private _CheckinDate As String
    '2013.04.09 
    Private _Guia As String

    Private _lenght As String
    Private _Width As String
    Private _Height As String
    Private _Weight As String
    Private _Dimweight As String
    Private _Shipweight As String

    ' propiedades
    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property Comment As String
        Get
            Return _Comment
        End Get
        Set(value As String)
            _Comment = value
        End Set
    End Property

    '-- NOT USED --    Public Property CheckinDate As String
    '-- NOT USED --        Get
    '-- NOT USED --            Return _CheckinDate
    '-- NOT USED --        End Get
    '-- NOT USED --        Set(value As String)
    '-- NOT USED --            _CheckinDate = value
    '-- NOT USED --        End Set
    '-- NOT USED --    End Property

    Public Property Guia As String
        Get
            Return _Guia
        End Get
        Set(value As String)
            _Guia = value
        End Set
    End Property

    Public Property lenght As String
        Get
            Return _lenght
        End Get
        Set(value As String)
            _lenght = value
        End Set
    End Property

    Public Property Width As String
        Get
            Return _Width
        End Get
        Set(value As String)
            _Width = value
        End Set
    End Property

    Public Property Height As String
        Get
            Return _Height
        End Get
        Set(value As String)
            _Height = value
        End Set
    End Property

    Public Property Weight As String
        Get
            Return _Weight
        End Get
        Set(value As String)
            _Weight = value
        End Set
    End Property

    Public Property Dimweight As String
        Get
            Return _Dimweight
        End Get
        Set(value As String)
            _Dimweight = value
        End Set
    End Property

    Public Property Shipweight As String
        Get
            Return _Shipweight
        End Get
        Set(value As String)
            _Shipweight = value
        End Set
    End Property

End Class

'2013.05.24 - UPD 2013.05-27
Public Class ReciboPartesCompradasModel
    Inherits SavePartesCompradasModel

    Private _Action As String
    Private _Description As String
    Private _Proveedor As String
    Private _LeadTime As String
    Private _CostDate As String

    Public Property Action As String
        Get
            Return _Action
        End Get
        Set(value As String)
            _Action = value
        End Set
    End Property

    Public Property Description As String
        Get
            Return _Description
        End Get
        Set(value As String)
            _Description = value
        End Set
    End Property

    Public Property Proveedor As String
        Get
            Return _Proveedor
        End Get
        Set(value As String)
            _Proveedor = value
        End Set
    End Property

    Public Property LeadTime As String
        Get
            Return _LeadTime
        End Get
        Set(value As String)
            _LeadTime = value
        End Set
    End Property

    Public Property CostDate As String
        Get
            Return _CostDate
        End Get
        Set(value As String)
            _CostDate = value
        End Set
    End Property

End Class

Public Class SavePartesCompradasModel
    Inherits ScanOrderModel

    Private _PartNo As String
    Private _Comentario As String
    Private _TrackNo As String

    Public Property TrackNo As String
        Get
            Return _TrackNo
        End Get
        Set(value As String)
            _TrackNo = value
        End Set
    End Property

    Public Property Comentario As String
        Get
            Return _Comentario
        End Get
        Set(value As String)
            _Comentario = value
        End Set
    End Property

    Public Property PartNo As String
        Get
            Return _PartNo
        End Get
        Set(value As String)
            _PartNo = value
        End Set
    End Property
End Class

' 2013.06.06 
Public Class ShippingModel
    Inherits ScanOrderModel

    Private _TrackNo As String

    Public Property TrackNo As String
        Get
            Return _TrackNo
        End Get
        Set(value As String)
            _TrackNo = value
        End Set
    End Property
End Class

'2013.06.10
Public Class OrdenesEmbarcadasModel
    Inherits ScanOrderModel

    Private _TrackNo As String
    Private _ShipDate As String
    Private _ShipBy As String

    Private _dateStart As String
    Private _dateEnd As String

    Public Property TrackNo As String
        Get
            Return _TrackNo
        End Get
        Set(value As String)
            _TrackNo = value
        End Set
    End Property

    Public Property ShipDate As String
        Get
            Return _ShipDate
        End Get
        Set(value As String)
            _ShipDate = value
        End Set
    End Property

    Public Property ShipBy As String
        Get
            Return _ShipBy
        End Get
        Set(value As String)
            _ShipBy = value
        End Set
    End Property

    Public Property dateStart As String
        Get
            Return _dateStart
        End Get
        Set(value As String)
            _dateStart = value
        End Set
    End Property

    Public Property dateEnd As String
        Get
            Return _dateEnd
        End Get
        Set(value As String)
            _dateEnd = value
        End Set
    End Property

End Class
