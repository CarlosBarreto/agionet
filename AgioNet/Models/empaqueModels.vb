Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

Public Class reciboEmpaqueModel
    Private _OrderID As String
    Private _Customer As String
    Private _ProductType As String
    Private _SerialNo As String
    Private _Model As String
    Private _Description As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property Customer As String
        Get
            Return _Customer
        End Get
        Set(value As String)
            _Customer = value
        End Set
    End Property

    Public Property ProductType As String
        Get
            Return _ProductType
        End Get
        Set(value As String)
            _ProductType = value
        End Set
    End Property

    Public Property SerialNo As String
        Get
            Return _SerialNo
        End Get
        Set(value As String)
            _SerialNo = value
        End Set
    End Property

    Public Property Model As String
        Get
            Return _Model
        End Get
        Set(value As String)
            _Model = value
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
End Class