Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

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


    ' Fields
    Private _Customer As String
    Private _Description As String
    Private _Model As String
    Private _OrderID As String
    Private _ProductType As String
    Private _SerialNo As String
End Class



