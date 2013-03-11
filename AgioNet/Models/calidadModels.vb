Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

Public Class ProcesoCalidadModels
    Private _OrderID As String
    Private _PartNumber As String
    Private _SerialNumber As String
    Private _FailureType As String
    Private _Comment As String

    '--
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

    Public Property Comment As String
        Get
            Return _Comment
        End Get
        Set(value As String)
            _Comment = value
        End Set
    End Property

End Class

Public Class InspeccionCalidadModel
    Private _OrderID As String
    Private _Result As String
    Private _Comment As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property Result As String
        Get
            Return _Result
        End Get
        Set(value As String)
            _Result = value
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