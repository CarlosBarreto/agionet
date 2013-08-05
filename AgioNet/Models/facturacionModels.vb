Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

' 2013.03.10
Public Class OnBillingModel
    Private _OrderID As String
    Private _PartNumber As String
    Private _SerialNumber As String
    Private _FailureType As String
    Private _Costo As String
    Private _LeadTime As String

    '-- Propiedades
    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property PartNumber As String
        Get
            Return _PartNumber
        End Get
        Set(value As String)
            _PartNumber = value
        End Set
    End Property

    Public Property SerialNumber As String
        Get
            Return _SerialNumber
        End Get
        Set(value As String)
            _SerialNumber = value
        End Set
    End Property

    Public Property FailureType As String
        Get
            Return _FailureType
        End Get
        Set(value As String)
            _FailureType = value
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

    Public Property LeadTime As String
        Get
            Return _LeadTime
        End Get
        Set(value As String)
            _LeadTime = value
        End Set
    End Property

End Class

Public Class AprobarFacturacionModel
    Private _OrderID As String
    Private _Aprobado As Boolean
    Private _AprobadoPor As String
    Private _FechaAprobación As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property Aprobado As String
        Get
            Return _Aprobado
        End Get
        Set(value As String)
            _Aprobado = value
        End Set
    End Property

    Public Property AprobadoPor As String
        Get
            Return _AprobadoPor
        End Get
        Set(value As String)
            _AprobadoPor = value
        End Set
    End Property

    Public Property FechaAprobacion As String
        Get
            Return _FechaAprobación
        End Get
        Set(value As String)
            _FechaAprobación = value
        End Set
    End Property
End Class

' 2013.04.17 
Public Class PrecioFacturacionModel

    Private _OrderID As String
    Private _PartNo As String
    Private _Description As String
    Private _Precio As String
    Private _Commodity As String
    Private _TipoReparacion As String

    '--------------
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

    Public Property Precio As String
        Get
            Return _Precio
        End Get
        Set(value As String)
            _Precio = value
        End Set
    End Property

    Public Property Commodity As String
        Get
            Return _Commodity
        End Get
        Set(value As String)
            _Commodity = value
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
End Class

'2013.05.15 
Public Class facturarModel
    Private _OrderID As String
    Private _SubOrder() As String
    Private _PartNo() As String
    Private _Precio() As String
    Private _SubTotal As String
    Private _IVA As String
    Private _Total As String
    Private _NoFactura As String
    Private _Comentario As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property SubOrder As String()
        Get
            Return _SubOrder
        End Get
        Set(value As String())
            _SubOrder = value
        End Set
    End Property

    Public Property PartNo As String()
        Get
            Return _PartNo
        End Get
        Set(value As String())
            _PartNo = value
        End Set
    End Property

    Public Property Precio As String()
        Get
            Return _Precio
        End Get
        Set(value As String())
            _Precio = value
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

    Public Property NoFactura As String
        Get
            Return _NoFactura
        End Get
        Set(value As String)
            _NoFactura = value
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

End Class

' 2013.05.15
Public Class facturaListModel
    Private _NoFactura As String
    Private _SubTotal As String
    Private _IVA As String
    Private _Total As String

    Public Property NoFactura As String
        Get
            Return _NoFactura
        End Get
        Set(value As String)
            _NoFactura = value
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

End Class

'2013.05.16
Public Class facturaDetailModel
    Inherits facturaListModel

    Private _OrderID As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public ReadOnly Property Reference As String
        Get
            Dim OType As String
            OType = Mid(_OrderID, 1, 2)
            If OType = "AG" Or OType = "AF" Then
                Return _OrderID
            Else
                Return Mid(_OrderID, 3, _OrderID.Length - 5)
            End If

        End Get
    End Property
End Class

'2013.05.16
Public Class ScanFacturaModel
    Private _NoFactura As String
    Public Property NoFactura As String
        Get
            Return _NoFactura
        End Get
        Set(value As String)
            _NoFactura = value
        End Set
    End Property
End Class