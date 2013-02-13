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

'2013.02.13
Public Class TestListModel
    ' Properties
    Public Property CREATEBY As String
        Get
            Return Me._CREATEBY
        End Get
        Set(ByVal value As String)
            Me._CREATEBY = value
        End Set
    End Property

    Public Property ORDERID As String
        Get
            Return Me._ORDERID
        End Get
        Set(ByVal value As String)
            Me._ORDERID = value
        End Set
    End Property

    Public Property TESTDESCRIPTION As String
        Get
            Return Me._TESTDESCRIPTION
        End Get
        Set(ByVal value As String)
            Me._TESTDESCRIPTION = value
        End Set
    End Property

    Public Property TESTEND As String
        Get
            Return Me._TESTEND
        End Get
        Set(ByVal value As String)
            Me._TESTEND = value
        End Set
    End Property

    Public Property TESTID As String
        Get
            Return Me._TESTID
        End Get
        Set(ByVal value As String)
            Me._TESTID = value
        End Set
    End Property

    Public Property TESTNAME As String
        Get
            Return Me._TESTNAME
        End Get
        Set(ByVal value As String)
            Me._TESTNAME = value
        End Set
    End Property

    Public Property TESTRESULT As String
        Get
            Return Me._TESTRESULT
        End Get
        Set(ByVal value As String)
            Me._TESTRESULT = value
        End Set
    End Property

    Public Property TESTSTART As String
        Get
            Return Me._TESTSTART
        End Get
        Set(ByVal value As String)
            Me._TESTSTART = value
        End Set
    End Property


    ' Fields
    Private _CREATEBY As String
    Private _ORDERID As String
    Private _TESTDESCRIPTION As String
    Private _TESTEND As String
    Private _TESTID As String
    Private _TESTNAME As String
    Private _TESTRESULT As String
    Private _TESTSTART As String
End Class

'2013.02.13
Public Class AsignarEstacionModel
    ' Properties
    Public Property Estacion As String
        Get
            Return Me._Estacion
        End Get
        Set(ByVal value As String)
            Me._Estacion = value
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
    Private _Estacion As String
    Private _TestID As String
    Private _User As String
End Class

