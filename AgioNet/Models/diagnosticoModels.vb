Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization
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
Public Class MaterialMasterListModel
    ' Properties
    Public Property Alt As String
        Get
            Return Me._Alt
        End Get
        Set(ByVal value As String)
            Me._Alt = value
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

    Public Property PartNo As String
        Get
            Return Me._PartNo
        End Get
        Set(ByVal value As String)
            Me._PartNo = value
        End Set
    End Property

    Public Property SKUNo As String
        Get
            Return Me._SKUNo
        End Get
        Set(ByVal value As String)
            Me._SKUNo = value
        End Set
    End Property


    ' Fields
    Private _Alt As String
    Private _Description As String
    Private _PartNo As String
    Private _SKUNo As String
End Class

'2013.02.13
Public Class OrderInfoModel
    ' Properties
    Public Property Address As String
        Get
            Return Me._Address
        End Get
        Set(ByVal value As String)
            Me._Address = value
        End Set
    End Property

    Public Property Address2 As String
        Get
            Return Me._Address2
        End Get
        Set(ByVal value As String)
            Me._Address2 = value
        End Set
    End Property

    Public Property City As String
        Get
            Return Me._City
        End Get
        Set(ByVal value As String)
            Me._City = value
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

    Public Property Country As String
        Get
            Return Me._Country
        End Get
        Set(ByVal value As String)
            Me._Country = value
        End Set
    End Property

    Public Property CustomerName As String
        Get
            Return Me._CustomerName
        End Get
        Set(ByVal value As String)
            Me._CustomerName = value
        End Set
    End Property

    Public Property CustomerType As String
        Get
            Return Me._CustomerType
        End Get
        Set(ByVal value As String)
            Me._CustomerName = value
        End Set
    End Property

    Public Property Delivery As String
        Get
            Return Me._Delivery
        End Get
        Set(ByVal value As String)
            Me._Delivery = value
        End Set
    End Property

    Public Property DeliveryTime As String
        Get
            Return Me._DeliveryTime
        End Get
        Set(ByVal value As String)
            Me._DeliveryTime = value
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

    Public Property Email As String
        Get
            Return Me._Email
        End Get
        Set(ByVal value As String)
            Me._Email = value
        End Set
    End Property

    Public Property ENumber As String
        Get
            Return Me._ENumber
        End Get
        Set(ByVal value As String)
            Me._ENumber = value
        End Set
    End Property

    Public Property FailureType As String
        Get
            Return Me._FailureType
        End Get
        Set(ByVal value As String)
            Me._FailureType = value
        End Set
    End Property

    Public Property Flete As String
        Get
            Return Me._Flete
        End Get
        Set(ByVal value As String)
            Me._Flete = value
        End Set
    End Property

    Public Property INumber As String
        Get
            Return Me._INumber
        End Get
        Set(ByVal value As String)
            Me._INumber = value
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

    Public Property OrderDate As String
        Get
            Return Me._OrderDate
        End Get
        Set(ByVal value As String)
            Me._OrderDate = value
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

    Public Property PartNo As String
        Get
            Return Me._PartNo
        End Get
        Set(ByVal value As String)
            Me._PartNo = value
        End Set
    End Property

    Public Property ProductClass As String
        Get
            Return Me._ProductClass
        End Get
        Set(ByVal value As String)
            Me._ProductClass = value
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

    Public Property RazonSocial As String
        Get
            Return Me._RazonSocial
        End Get
        Set(ByVal value As String)
            Me._RazonSocial = value
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

    Public Property Revision As String
        Get
            Return Me._Revision
        End Get
        Set(ByVal value As String)
            Me._Revision = value
        End Set
    End Property

    Public Property RFC As String
        Get
            Return Me._RFC
        End Get
        Set(ByVal value As String)
            Me._RFC = value
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

    Public Property ServiceType As String
        Get
            Return Me._ServiceType
        End Get
        Set(ByVal value As String)
            Me._ServiceType = value
        End Set
    End Property

    Public Property State As String
        Get
            Return Me._State
        End Get
        Set(ByVal value As String)
            Me._State = value
        End Set
    End Property

    Public Property Tel As String
        Get
            Return Me._Tel
        End Get
        Set(ByVal value As String)
            Me._Tel = value
        End Set
    End Property

    Public Property Tel2 As String
        Get
            Return Me._Tel2
        End Get
        Set(ByVal value As String)
            Me._Tel2 = value
        End Set
    End Property

    Public Property Tel3 As String
        Get
            Return Me._Tel3
        End Get
        Set(ByVal value As String)
            Me._Tel3 = value
        End Set
    End Property

    Public Property Trademark As String
        Get
            Return Me._Trademark
        End Get
        Set(ByVal value As String)
            Me._Trademark = value
        End Set
    End Property

    Public Property ZipCode As String
        Get
            Return Me._ZipCode
        End Get
        Set(ByVal value As String)
            Me._ZipCode = value
        End Set
    End Property


    ' Fields
    Private _Address As String
    Private _Address2 As String
    Private _City As String
    Private _Comment As String
    Private _Country As String
    Private _CustomerName As String
    Private _CustomerType As String
    Private _Delivery As String
    Private _DeliveryTime As String
    Private _Description As String
    Private _Email As String
    Private _ENumber As String
    Private _FailureType As String
    Private _Flete As String
    Private _INumber As String
    Private _Model As String
    Private _OrderDate As String
    Private _OrderID As String
    Private _PartNo As String
    Private _ProductClass As String
    Private _ProductType As String
    Private _RazonSocial As String
    Private _Reference As String
    Private _Revision As String
    Private _RFC As String
    Private _SerialNo As String
    Private _ServiceType As String
    Private _State As String
    Private _Tel As String
    Private _Tel2 As String
    Private _Tel3 As String
    Private _Trademark As String
    Private _ZipCode As String
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
Public Class StartDiagnosticModel
    ' Properties
    Public Property Comment As String
        Get
            If ((Me._Comment = "") Or (Me._Comment Is Nothing)) Then
                Me._Comment = ("Start order at " & Conversions.ToString(DateAndTime.Now))
            End If
            Return Me._Comment
        End Get
        Set(ByVal value As String)
            Me._Comment = value
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


    ' Fields
    Private _Comment As String
    Private _OrderID As String
End Class

'2013.02.13
Public Class TestIDModel
    ' Properties
    Public Property TestID As String
        Get
            Return Me._TestID
        End Get
        Set(ByVal value As String)
            Me._TestID = value
        End Set
    End Property


    ' Fields
    Private _TestID As String
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










