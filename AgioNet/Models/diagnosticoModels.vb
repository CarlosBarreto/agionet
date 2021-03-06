﻿Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization
Imports System.Data.SqlClient

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

    <AllowHtml> _
    Public Property TextLog As String
        Get
            Return Me._TextLog
        End Get
        Set(ByVal value As String)
            Me._TextLog = value
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

    ' Fields
    Private _OrderID As String
    Private _Result As String
    Private _TestID As String
    Private _TextLog As String
    '- 2013.02.22 - Upd Asignar Error   
    Private _Failure As String
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

    '---- Public
    Public Property Commodity As String
        Get
            Return _Commodity
        End Get
        Set(value As String)
            _Commodity = value
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
    Private _Commodity As String
End Class

'2013.02.13
'--- Upd By CarlosB on 2013.02.22
'--- Upd By CarlosB on 2013.04.05
Public Class RegFailureModel
    ' Properties
    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
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

    Public Property User As String
        Get
            Return _User
        End Get
        Set(value As String)
            _User = value
        End Set
    End Property

    Public Property HasFailure As String
        Get
            Return _HasFailure
        End Get
        Set(value As String)
            _HasFailure = value
        End Set
    End Property

    ' Fields
    Private _OrderID As String
    Private _TipoReparacion As String
    Private _Failure As String
    Private _Solution As String
    Private _Source As String
    Private _Comment As String
    Private _User As String
    Private _HasFailure As String
End Class

'2013.02.15
Public Class ReportedFailureModel
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

' 2013.02.13
Public Class SheduleTestModel
    Private _OrderID As String
    Private _Prueba As String

    'Detalles del componente
    Private _Marca As String
    Private _PartNo As String
    Private _SerialNo As String
    Private _Revision As String
    Private _Comentarios As String

    ' -- Propiedades --
    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property Prueba As String
        Get
            Return _Prueba
        End Get
        Set(value As String)
            _Prueba = value
        End Set
    End Property

    'Detalles del componente
    Public Property Marca As String
        Get
            Return _Marca
        End Get
        Set(value As String)
            _Marca = value
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

    Public Property SerialNo As String
        Get
            Return _SerialNo
        End Get
        Set(value As String)
            _SerialNo = value
        End Set
    End Property

    Public Property Revision As String
        Get
            Return _Revision
        End Get
        Set(value As String)
            _Revision = value
        End Set
    End Property

    <AllowHtml> _
    Public Property Comentarios As String
        Get
            Return _Comentarios
        End Get
        Set(value As String)
            _Comentarios = value
        End Set
    End Property

End Class

'2013.02.13
Public Class StartDiagnosticModel
    ' Properties
    Public Property Comment As String
        Get
            If ((Me._Comment = "") Or (Me._Comment Is Nothing)) Then
                Me._Comment = ("Start order at " & Now.ToString)
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

' 2013.02.19
Public Class StationByUser
    Private _OrderID As String
    Private _stDescription As String
    Private _stName As String

    Public Property OrderId As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property srDescription As String
        Get
            Return _stDescription
        End Get
        Set(value As String)
            _stDescription = value
        End Set
    End Property

    Public Property stName As String
        Get
            Return _stName
        End Get
        Set(value As String)
            _stName = value
        End Set
    End Property

End Class

'2013.02.20
Public Class TestOrderInfoModel
    Private _OrderID As String
    Private _OrderDate As String
    Private _TestID As String
    Private _ProductClass As String
    Private _ProductType As String
    Private _ProductTrademark As String
    Private _ProductModel As String
    Private _ProductDescription As String
    Private _PartNumber As String
    Private _SerialNumber As String
    Private _FailureType As String
    Private _Comment As String
    Private _DateLog As String
    Private _TextLog As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property OrderDate As String
        Get
            Return _OrderDate
        End Get
        Set(value As String)
            _OrderDate = value
        End Set
    End Property

    Public Property TestID As String
        Get
            Return _TestID
        End Get
        Set(value As String)
            _TestID = value
        End Set
    End Property

    Public Property ProductClass As String
        Get
            Return _ProductClass
        End Get
        Set(value As String)
            _ProductClass = value
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

    Public Property ProductTrademark As String
        Get
            Return _ProductTrademark
        End Get
        Set(value As String)
            _ProductTrademark = value
        End Set
    End Property

    Public Property ProductModel As String
        Get
            Return _ProductModel
        End Get
        Set(value As String)
            _ProductModel = value
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

    Public Property DateLog As String
        Get
            Return _DateLog
        End Get
        Set(value As String)
            _DateLog = value
        End Set
    End Property

    Public Property TextLog As String
        Get
            Return _TextLog
        End Get
        Set(value As String)
            _TextLog = value
        End Set
    End Property
End Class

' 2013.02.20
Public Class isTestedModel
    Private _Response As String
    Private _TestID As String
    Private _Result As String
    Private _TextLog As String
    Private _Failure As String
    Private _ErrorMsg As String

    Public Property Response As String
        Get
            Return _Response
        End Get
        Set(value As String)
            _Response = value
        End Set
    End Property

    Public Property TestID As String
        Get
            Return _TestID
        End Get
        Set(value As String)
            _TestID = value
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

    Public Property TextLog As String
        Get
            Return _TextLog
        End Get
        Set(value As String)
            _TextLog = value
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

    Public Property ErrorMsg As String
        Get
            Return _ErrorMsg
        End Get
        Set(value As String)
            _ErrorMsg = value
        End Set
    End Property

    ' metodos
    Public Sub IsTested(ByVal _OrderID_ As String)
        Dim DA As New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
        Dim DR As SqlDataReader
        Try
            DR = DA.ExecuteSP("dg_isTested", _OrderID_)
            If DR.HasRows Then
                Do While DR.Read
                    _Response = DR(0)
                    _TestID = DR(1)
                    _Result = DR(2)
                    _TextLog = DR(3)
                    _Failure = DR(4)
                Loop
            Else
                'Si no hay datos, mostrar error
                Throw New Exception("Error, no se han encontrado datos")
            End If
            _ErrorMsg = ""
        Catch ex As Exception
            _ErrorMsg = ex.Message
        Finally
            DA.Dispose()
        End Try
    End Sub

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
Public Class TestIDListModel
    ' Properties
    Public Property TestID As String
        Get
            Return Me._TestID
        End Get
        Set(ByVal value As String)
            Me._TestID = value
        End Set
    End Property

    Public Property TestName As String
        Get
            Return Me._TestName
        End Get
        Set(ByVal value As String)
            Me._TestName = value
        End Set
    End Property


    ' Fields
    Private _TestID As String
    Private _TestName As String
End Class

'2013.02.13
Public Class TestInfoModel
    ' Properties
    Public Property CreateBy As String
        Get
            Return Me._CreateBy
        End Get
        Set(ByVal value As String)
            Me._CreateBy = value
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

    Public Property TestDescription As String
        Get
            Return Me._TestDescription
        End Get
        Set(ByVal value As String)
            Me._TestDescription = value
        End Set
    End Property

    Public Property TestEnd As String
        Get
            Return Me._TestEnd
        End Get
        Set(ByVal value As String)
            Me._TestEnd = value
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

    Public Property TestName As String
        Get
            Return Me._TestName
        End Get
        Set(ByVal value As String)
            Me._TestName = value
        End Set
    End Property

    Public Property TestResult As String
        Get
            Return Me._TestResult
        End Get
        Set(ByVal value As String)
            Me._TestResult = value
        End Set
    End Property

    Public Property TestStart As String
        Get
            Return Me._TestStart
        End Get
        Set(ByVal value As String)
            Me._TestStart = value
        End Set
    End Property


    ' Fields
    Private _CreateBy As String
    Private _OrderID As String
    Private _TestDescription As String
    Private _TestEnd As String
    Private _TestID As String
    Private _TestName As String
    Private _TestResult As String
    Private _TestStart As String
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

'2013.02.22
Public Class TestReportModel
    Private _OrderID As String
    Private _TestName As String
    Private _TestResult As String
    Private _CapFailure As String
    Private _Failure As String
    Private _TextLog As String
    Private _TestStart As String
    Private _TestEnd As String
    Private _CreateBy As String
    Private _TipoReparacion As String

    '-- Propiedades
    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property TestName As String
        Get
            Return _TestName
        End Get
        Set(value As String)
            _TestName = value
        End Set
    End Property

    Public Property TestResult As String
        Get
            Return _TestResult
        End Get
        Set(value As String)
            _TestResult = value
        End Set
    End Property

    Public Property CapFailure As String
        Get
            Return _CapFailure
        End Get
        Set(value As String)
            _CapFailure = value
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

    Public Property TextLog As String
        Get
            Return _TextLog
        End Get
        Set(value As String)
            _TextLog = value
        End Set
    End Property

    Public Property TestStart As String
        Get
            Return _TestStart
        End Get
        Set(value As String)
            _TestStart = value
        End Set
    End Property

    Public Property TestEnd As String
        Get
            Return _TestEnd
        End Get
        Set(value As String)
            _TestEnd = value
        End Set
    End Property

    Public Property CreateBy As String
        Get
            Return _CreateBy
        End Get
        Set(value As String)
            _CreateBy = value
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

'2013.02.13
Public Class ViewFoundFailureModel
    ' Properties
    Public Property Description As String
        Get
            Return Me._FDescription
        End Get
        Set(ByVal value As String)
            Me._FDescription = value
        End Set
    End Property

    Public Property FailureID As String
        Get
            Return Me._FailureID
        End Get
        Set(ByVal value As String)
            Me._FailureID = value
        End Set
    End Property

    Public Property FoundBy As String
        Get
            Return Me._FoundBy
        End Get
        Set(ByVal value As String)
            Me._FailureID = value
        End Set
    End Property

    Public Property FoundDate As String
        Get
            Return Me._FoundDate
        End Get
        Set(ByVal value As String)
            Me._FoundDate = value
        End Set
    End Property

    Public Property Resolved As String
        Get
            Return Me._Resolved
        End Get
        Set(ByVal value As String)
            Me._Resolved = value
        End Set
    End Property

    Public Property Solution As String
        Get
            Return Me._PSolution
        End Get
        Set(ByVal value As String)
            Me._PSolution = value
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


    ' Fields
    Private _FailureID As String
    Private _FDescription As String
    Private _FoundBy As String
    Private _FoundDate As String
    Private _PSolution As String
    Private _Resolved As String
    Private _TestID As String
End Class

' 2013.03.13
Public Class TestByOrderIDModel
    Private _OrderID As String
    Private _TestName As String
    Private _TestResult As String
    Private _Failure As String
    Private _CapFailure As String
    Private _TextLog As String
    Private _TestStart As DateTime
    Private _TestEnd As DateTime
    Private _CreateBy As String

    '-----------------------------------------------------------------
    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property TestName As String
        Get
            Return _TestName
        End Get
        Set(value As String)
            _TestName = value
        End Set
    End Property

    Public Property TestResult As String
        Get
            Return _TestResult
        End Get
        Set(value As String)
            _TestResult = value
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

    Public Property CapFailure As String
        Get
            Return _CapFailure
        End Get
        Set(value As String)
            _CapFailure = value
        End Set
    End Property

    Public Property TextLog As String
        Get
            Return _TextLog
        End Get
        Set(value As String)
            _TextLog = value
        End Set
    End Property

    Public Property TestStart As DateTime
        Get
            Return _TestStart
        End Get
        Set(value As DateTime)
            _TestStart = value
        End Set
    End Property

    Public Property TestEnd As DateTime
        Get
            Return _TestEnd
        End Get
        Set(value As DateTime)
            _TestEnd = value
        End Set
    End Property

    Public Property CreateBy As String
        Get
            Return _CreateBy
        End Get
        Set(value As String)
            _CreateBy = value
        End Set
    End Property
End Class










