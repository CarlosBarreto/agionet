Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization


Public Class rp_CheckinModel
    Inherits setCheckinModel

    Private _CheckinDate As String
    Private _User As String

    Public Property CheckinDate As String
        Get
            Return _CheckinDate
        End Get
        Set(value As String)
            _CheckinDate = value
        End Set
    End Property

    Public Property User As String
        Get
            Return _User
        End Get
        Set(value As String)
            _User = value
        End Set
    End Property
End Class

'2013-06-20
Public Class rp_ReciboModel
    Inherits ReceiveIndfoModel

    ' Properties
    Public Property Model As String
        Get
            Return Me._Model
        End Get
        Set(ByVal value As String)
            Me._Model = value
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

    Public Property TrackNo As String
        Get
            Return _trackno
        End Get
        Set(value As String)
            _trackno = value
        End Set
    End Property
    ' Fields
    Private _Model As String
    Private _SKU As String
    Private _PartNo As String
    Private _ScanBy As String
    Private _ScanDate As String
    Private _SerialNo As String
    Private _TrackNo As String
End Class

'2013.06.20
Public Class rp_DiagnosticModel
    Private _OrderID As String
    Private _StartDate As String
    Private _EndDate As String
    Private _CreateBy As String
    Private _Comments As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property StartDate As String
        Get
            Return _StartDate
        End Get
        Set(value As String)
            _StartDate = value
        End Set
    End Property

    Public Property EndDate As String
        Get
            Return _EndDate
        End Get
        Set(value As String)
            _EndDate = Value
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

    Public Property Comments As String
        Get
            Return _Comments
        End Get
        Set(value As String)
            _Comments = value
        End Set
    End Property

End Class

'2013.06.20
Public Class rp_TestListModel
    Private _OrderID As String
    Private _TestID As String
    Private _TestName As String
    Private _TestDescription As String
    Private _TestResult As String
    Private _TestStart As String
    Private _TestEnd As String
    Private _CreateBy As String
    Private _LogDate As String
    Private _LogText As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property TestID As String
        Get
            Return _TestID
        End Get
        Set(value As String)
            _TestID = value
        End Set
    End Property

    Public Property TestName As String
        Get
            Return _TestName
        End Get
        Set(value As String)
            _TestName = value
        End Set
    End Property

    Public Property TestDescription As String
        Get
            Return _TestDescription
        End Get
        Set(value As String)
            _TestDescription = value
        End Set
    End Property

    Public Property TestResult As String
        Get
            Return _TestResult
        End Get
        Set(value As String)
            _TestResult = value
        End Set
    End Property

    Public Property TestStart As String
        Get
            Return _TestStart
        End Get
        Set(value As String)
            _TestStart = value
        End Set
    End Property

    Public Property TestEnd As String
        Get
            Return _TestEnd
        End Get
        Set(value As String)
            _TestEnd = value
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

    Public Property LogDate As String
        Get
            Return _LogDate
        End Get
        Set(value As String)
            _LogDate = value
        End Set
    End Property

    Public Property LogText As String
        Get
            Return _LogText
        End Get
        Set(value As String)
            _LogText = value
        End Set
    End Property

End Class

'2013.06.20
Public Class rp_FailureListModel
    Private _OrderID As String
    Private _FailureID As String
    Private _TipoReparacion As String
    Private _Failure As String
    Private _Solution As String
    Private _Source As String
    Private _Comment As String
    Private _FoundBy As String
    Private _FoundDate As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property FailureID As String
        Get
            Return _FailureID
        End Get
        Set(value As String)
            _FailureID = value
        End Set
    End Property

    Public Property TipoReparacion As String
        Get
            Return _TipoReparacion
        End Get
        Set(value As String)
            _TipoReparacion = value
        End Set
    End Property

    Public Property Failure As String
        Get
            Return _Failure
        End Get
        Set(value As String)
            _Failure = value
        End Set
    End Property

    Public Property Solution As String
        Get
            Return _Solution
        End Get
        Set(value As String)
            _Solution = value
        End Set
    End Property

    Public Property Source As String
        Get
            Return _Source
        End Get
        Set(value As String)
            _Source = value
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

    Public Property FoundBy As String
        Get
            Return _FoundBy
        End Get
        Set(value As String)
            _FoundBy = value
        End Set
    End Property

    Public Property FoundDate As String
        Get
            Return _FoundDate
        End Get
        Set(value As String)
            _FoundDate = value
        End Set
    End Property

End Class

'2013.06.20
Public Class rp_dgCloseModel
    Private _OrderID As String
    Private _Retro As String
    Private _TComment As String
    Private _Comments As String
    Private _RequestBy As String
    Private _RequestDate As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property Retro As String
        Get
            Return _Retro
        End Get
        Set(value As String)
            _Retro = value
        End Set
    End Property

    Public Property TComment As String
        Get
            Return _TComment
        End Get
        Set(value As String)
            _TComment = value
        End Set
    End Property

    Public Property Comments As String
        Get
            Return _Comments
        End Get
        Set(value As String)
            _Comments = value
        End Set
    End Property

    Public Property RequestBy As String
        Get
            Return _RequestBy
        End Get
        Set(value As String)
            _RequestBy = value
        End Set
    End Property

    Public Property RequestDate As String
        Get
            Return _RequestDate
        End Get
        Set(value As String)
            _RequestDate = value
        End Set
    End Property

End Class

'2013.06.21 
Public Class rp_CostPartModel
    Private _OrderID As String
    Private _PartNo As String
    Private _Description As String
    Private _Costo As String
    Private _Flete As String
    Private _GastosImportacion As String
    Private _SubTotal As String
    Private _LeadTime As String
    Private _Proveedor As String
    Private _Comentario As String
    Private _CostDate As String
    Private _CostBy As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
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
            Return _Description
        End Get
        Set(value As String)
            _Description = value
        End Set
    End Property

    Public Property Costo As String
        Get
            Return _Costo
        End Get
        Set(value As String)
            _Costo = value
        End Set
    End Property

    Public Property Flete As String
        Get
            Return _Flete
        End Get
        Set(value As String)
            _Flete = value
        End Set
    End Property

    Public Property GastosImportacion As String
        Get
            Return _GastosImportacion
        End Get
        Set(value As String)
            _GastosImportacion = value
        End Set
    End Property

    Public Property SubTotal As String
        Get
            Return _SubTotal
        End Get
        Set(value As String)
            _SubTotal = value
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

    Public Property Proveedor As String
        Get
            Return _Proveedor
        End Get
        Set(value As String)
            _Proveedor = value
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

    Public Property CostDate As String
        Get
            Return _CostDate
        End Get
        Set(value As String)
            _CostDate = value
        End Set
    End Property

    Public Property CostBy As String
        Get
            Return _CostBy
        End Get
        Set(value As String)
            _CostBy = value
        End Set
    End Property

End Class

'2013.07.10 
Public Class rp_CotizacionPartesModel
    Private _OrderID As String
    Private _PartNo As String
    Private _Description As String
    Private _Costo As String
    Private _Utilidad As String
    Private _UtilidadNeta As String
    Private _Precio As String

    ' Properties
    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
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
            Return _Description
        End Get
        Set(value As String)
            _Description = value
        End Set
    End Property

    Public Property Costo As String
        Get
            Return _Costo
        End Get
        Set(value As String)
            _Costo = value
        End Set
    End Property

    Public Property Utilidad As String
        Get
            Return _Utilidad
        End Get
        Set(value As String)
            _Utilidad = value
        End Set
    End Property

    Public Property UtilidadNeta As String
        Get
            Return _UtilidadNeta
        End Get
        Set(value As String)
            _UtilidadNeta = value
        End Set
    End Property

    Public Property Precio As String
        Get
            Return _Precio
        End Get
        Set(value As String)
            _Precio = value
        End Set
    End Property

End Class

'2013.07.11
Public Class rp_CotizacionClienteModel
    Private _OrderID As String
    Private _Sumatoria As String
    Private _ManoObra As String
    Private _Viaje As String
    Private _SubTotal As String
    Private _IVA As String
    Private _Total As String
    Private _LeadTime As String
    Private _CostBy As String
    Private _CostDate As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property Sumatoria As String
        Get
            Return _Sumatoria
        End Get
        Set(value As String)
            _Sumatoria = value
        End Set
    End Property

    Public Property ManoObra As String
        Get
            Return _ManoObra
        End Get
        Set(value As String)
            _ManoObra = value
        End Set
    End Property

    Public Property Viaje As String
        Get
            Return _Viaje
        End Get
        Set(value As String)
            _Viaje = value
        End Set
    End Property

    Public Property SubTotal As String
        Get
            Return _SubTotal
        End Get
        Set(value As String)
            _SubTotal = value
        End Set
    End Property

    Public Property IVA As String
        Get
            Return _IVA
        End Get
        Set(value As String)
            _IVA = value
        End Set
    End Property

    Public Property Total As String
        Get
            Return _Total
        End Get
        Set(value As String)
            _Total = value
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

    Public Property CostBy As String
        Get
            Return _CostBy
        End Get
        Set(value As String)
            _CostBy = value
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