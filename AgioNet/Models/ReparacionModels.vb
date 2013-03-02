Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

' 2013.03.01
Public Class PendientesreparacionModel
    Private _OrderID As String
    Private _Comments As String
    Private _ApprovalDate As String
    Private _PartNumber As String
    Private _ProductDescription As String
    Private _SerialNumber As String
    Private _Failure As String
    Private _Solution As String
    Private _Source As String
    Private _Comment As String

    '--- 
    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
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

    Public Property ApprovalDate As String
        Get
            Return _ApprovalDate
        End Get
        Set(value As String)
            _ApprovalDate = value
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

    Public Property ProductDescription As String
        Get
            Return _ProductDescription
        End Get
        Set(value As String)
            _ProductDescription = value
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

End Class



