Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

' 2013.02.14
Public Class AgregarTrackNoModel
    ' Properties
    Public Property CarrierName As String
        Get
            Return Me._CarrierName
        End Get
        Set(ByVal value As String)
            Me._CarrierName = value
        End Set
    End Property

    Public Property CaseType As String
        Get
            Return Me._CaseType
        End Get
        Set(ByVal value As String)
            Me._CaseType = value
        End Set
    End Property

    Public Property Field1 As String
        Get
            Return Me._Field1
        End Get
        Set(ByVal value As String)
            Me._Field1 = value
        End Set
    End Property

    Public Property Field2 As String
        Get
            Return Me._Field2
        End Get
        Set(ByVal value As String)
            Me._Field2 = value
        End Set
    End Property

    Public Property Height As String
        Get
            Return Me._Height
        End Get
        Set(ByVal value As String)
            Me._Height = value
        End Set
    End Property

    Public Property InBound As String
        Get
            Return Me._InBound
        End Get
        Set(ByVal value As String)
            Me._InBound = value
        End Set
    End Property

    Public Property Lenght As String
        Get
            Return Me._Lenght
        End Get
        Set(ByVal value As String)
            Me._Lenght = value
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

    Public Property OutBound As String
        Get
            Return Me._OutBound
        End Get
        Set(ByVal value As String)
            Me._OutBound = value
        End Set
    End Property

    Public Property OutBound2 As String
        Get
            Return Me._OutBound2
        End Get
        Set(ByVal value As String)
            Me._OutBound2 = value
        End Set
    End Property

    Public Property Reference As String
        Get
            Return Me._Reference
        End Get
        Set(ByVal value As String)
            Me._Reference = value
        End Set
    End Property

    Public Property Weight As String
        Get
            Return Me._Weight
        End Get
        Set(ByVal value As String)
            Me._Weight = value
        End Set
    End Property

    Public Property width As String
        Get
            Return Me._Width
        End Get
        Set(ByVal value As String)
            Me._Width = value
        End Set
    End Property


    ' Fields
    Private _CarrierName As String
    Private _CaseType As String
    Private _Field1 As String
    Private _Field2 As String
    Private _Height As String
    Private _InBound As String
    Private _Lenght As String
    Private _OrderID As String
    Private _OutBound As String
    Private _OutBound2 As String
    Private _Reference As String
    Private _Weight As String
    Private _Width As String
End Class

' 2013.02.14
Public Class OrderLogisticaModel
    ' Properties
    Public Property OrderID As String
        Get
            Return Me._OrderID
        End Get
        Set(ByVal value As String)
            Me._OrderID = value
        End Set
    End Property


    ' Fields
    Private _OrderID As String
End Class

