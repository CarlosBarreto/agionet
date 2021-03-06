﻿Imports System.ComponentModel
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

'2013.02.14
Public Class OrderListModel
    ' Properties
    Public Property CustomerName As String
        Get
            Return Me._CustomerName
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

    Public Property Email As String
        Get
            Return Me._Email
        End Get
        Set(ByVal value As String)
            Me._Email = value
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

    Public Property ProductModel As String
        Get
            Return Me._ProductModel
        End Get
        Set(ByVal value As String)
            Me._ProductModel = value
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

    Public Property Status As String
        Get
            Return Me._Status
        End Get
        Set(ByVal value As String)
            Me._Status = value
        End Set
    End Property


    ' Fields
    Private _CustomerName As String
    Private _Delivery As String
    Private _DeliveryTime As String
    Private _Email As String
    Private _OrderDate As String
    Private _OrderID As String
    Private _PartNo As String
    Private _ProductClass As String
    Private _ProductModel As String
    Private _ProductType As String
    Private _SerialNo As String
    Private _Status As String
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

' 2013.02.14
Public Class ScanOrderModel
    ' Properties
    Public Property Comment As String
        Get
            Return Me._Comment
        End Get
        Set(ByVal value As String)
            Me._Comment = value
        End Set
    End Property

    Public Property IsSerialNumber As Boolean
        Get
            Return Me._IsSerialNumber
        End Get
        Set(ByVal value As Boolean)
            Me._IsSerialNumber = value
        End Set
    End Property

    <Required> _
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
    Private _IsSerialNumber As Boolean
    Private _OrderID As String
End Class

' 2015.02.15 
Public Class TrackNoCostListModel
    ' Properties
    Public Property CustomerName As String
        Get
            Return Me._CustomerName
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

    Public Property Email As String
        Get
            Return Me._Email
        End Get
        Set(ByVal value As String)
            Me._Email = value
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

    Public Property TrackNo As String
        Get
            Return Me._TrackNo
        End Get
        Set(ByVal value As String)
            Me._TrackNo = value
        End Set
    End Property

    Public Property TrackNoType As String
        Get
            Return Me._TrackNoType
        End Get
        Set(ByVal value As String)
            Me._TrackNoType = value
        End Set
    End Property


    ' Fields
    Private _CustomerName As String
    Private _Delivery As String
    Private _DeliveryTime As String
    Private _Email As String
    Private _OrderDate As String
    Private _OrderID As String
    Private _ProductClass As String
    Private _ProductType As String
    Private _TrackNo As String
    Private _TrackNoType As String
End Class

' 2013.04.10  Upd 2013.05.09
Public Class PendingProcessWarehouseModel
    Private _OrderID As String
    Private _ProductType As String
    Private _ProductModel As String
    Private _ProductDescription As String
    Private _FechaEmbarque As String
    Private _TrackNo As String

    Private _CustomerName As String
    Private _SerialNumber As String
    Private _Ingresado As String
    Private _Procesado As String

    Private _DateComp As String

    Private _FechaIngreso As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
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

    Public Property FechaEmbarque As String
        Get
            Return _FechaEmbarque
        End Get
        Set(value As String)
            _FechaEmbarque = value
        End Set
    End Property

    Public Property TrackNo As String
        Get
            Return _TrackNo
        End Get
        Set(value As String)
            _TrackNo = value
        End Set
    End Property


    Public Property CustomerName As String
        Get
            Return _CustomerName
        End Get
        Set(value As String)
            _CustomerName = value
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

    Public Property Ingresado As String
        Get
            Return _Ingresado
        End Get
        Set(value As String)
            _Ingresado = value
        End Set
    End Property

    Public Property Procesado As String
        Get
            Return _Procesado
        End Get
        Set(value As String)
            _Procesado = value
        End Set
    End Property

    Public Property DateComp As String
        Get
            Return _DateComp
        End Get
        Set(value As String)
            _DateComp = value
        End Set
    End Property

    Public Property FechaIngreso As String
        Get
            Return _FechaIngreso
        End Get
        Set(value As String)
            _FechaIngreso = value
        End Set
    End Property
End Class


