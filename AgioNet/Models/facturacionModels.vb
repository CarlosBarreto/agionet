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