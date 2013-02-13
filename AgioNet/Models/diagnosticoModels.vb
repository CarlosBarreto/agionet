Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

'2013.02.13
Public Class ExecTestModel
    ' Properties
    Public Property OrderID As String
        Get
            Return Me._OrderID
        End Get
        Set(ByVal value As String)
            Me._OrderID = value
        End Set
    End Property

    Public Property Result As String
        Get
            Return Me._Result
        End Get
        Set(ByVal value As String)
            Me._Result = value
        End Set
    End Property

    Public Property TestID As String
        Get
            Return Me._TestID
        End Get
        Set(ByVal value As String)
            Me._TestID = value
        End Set
    End Property

    Public Property TextLog As String
        Get
            Return Me._TextLog
        End Get
        Set(ByVal value As String)
            Me._TextLog = value
        End Set
    End Property


    ' Fields
    Private _OrderID As String
    Private _Result As String
    Private _TestID As String
    Private _TextLog As String
End Class

'2013.02.13
Public Class RegFailureModel
    ' Properties
    Public Property Failure As String
        Get
            Return Me._FailureDescription
        End Get
        Set(ByVal value As String)
            Me._FailureDescription = value
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

    Public Property PSolution As String
        Get
            Return Me._Psol
        End Get
        Set(ByVal value As String)
            Me._Psol = value
        End Set
    End Property

    Public Property TestID As String
        Get
            Return Me._TestID
        End Get
        Set(ByVal value As String)
            Me._TestID = value
        End Set
    End Property

    Public Property User As String
        Get
            Return Me._User
        End Get
        Set(ByVal value As String)
            Me._User = value
        End Set
    End Property


    ' Fields
    Private _FailureDescription As String
    Private _OrderID As String
    Private _Psol As String
    Private _TestID As String
    Private _User As String
End Class


