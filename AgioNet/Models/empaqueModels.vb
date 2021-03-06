﻿Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

Public Class reciboEmpaqueModel
    Private _OrderID As String
    Private _Customer As String
    Private _ProductType As String
    Private _SerialNo As String
    Private _Model As String
    Private _Description As String
    Private _Entrega As String

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

    Public Property Entrega As String
        Get
            Return _Entrega
        End Get
        Set(value As String)
            _Entrega = value
        End Set
    End Property
End Class

Public Class DeliveryPageModel
    ' Properties
    <Display(Name:="Dirección"), Required> _
    Public Property Address As String
        Get
            Return Me._Address
        End Get
        Set(ByVal value As String)
            Me._Address = value
        End Set
    End Property

    <Display(Name:="Dirección 2 (Colonia)")> _
    Public Property Address2 As String
        Get
            Return Me._Address2
        End Get
        Set(ByVal value As String)
            Me._Address2 = value
        End Set
    End Property

    <Display(Name:="Ciudad")> _
    Public Property City As String
        Get
            Return Me._City
        End Get
        Set(ByVal value As String)
            Me._City = value
        End Set
    End Property

    <Display(Name:="Comentarios")> _
    Public Property Comment As String
        Get
            Return Me._Comment
        End Get
        Set(ByVal value As String)
            Me._Comment = value
        End Set
    End Property

    <Display(Name:="País")> _
    Public Property Country As String
        Get
            Return Me._Country
        End Get
        Set(ByVal value As String)
            Me._Country = value
        End Set
    End Property

    <Display(Name:="ID de cliente")> _
    Public ReadOnly Property CustomerID As String
        Get
            Return Me._customerID
        End Get
    End Property

    <Required, Display(Name:="Nombre del cliente")> _
    Public Property CustomerName As String
        Get
            Return Me._customerName
        End Get
        Set(ByVal value As String)
            Me._customerName = value
        End Set
    End Property

    <Display(Name:="Numero de cliente")> _
    Public Property CustomerReference As String
        Get
            Return Me._customerReference
        End Get
        Set(ByVal value As String)
            Me._customerReference = value
        End Set
    End Property

    <Display(Name:="Tipo de Cliente ")> _
    Public Property CustomerType As String
        Get
            Return Me._customerType
        End Get
        Set(ByVal value As String)
            Me._customerType = value
        End Set
    End Property

    <Display(Name:="Flete")> _
    Public Property Delivery As String
        Get
            Return Me._Delivery
        End Get
        Set(ByVal value As String)
            Me._Delivery = value
        End Set
    End Property

    <Display(Name:="Horario de recolección")> _
    Public Property DeliveryTime As String
        Get
            Return Me._DeliveryTime
        End Get
        Set(ByVal value As String)
            Me._DeliveryTime = value
        End Set
    End Property

    <Required, Display(Name:="Correo Electrónico")> _
    Public Property Email As String
        Get
            Return Me._email
        End Get
        Set(ByVal value As String)
            Me._email = value
        End Set
    End Property

    <Display(Name:="Número Exterior")> _
    Public Property ExternalNumber As String
        Get
            Return Me._ExternalNumber
        End Get
        Set(ByVal value As String)
            Me._ExternalNumber = value
        End Set
    End Property

    <Display(Name:="Falla presentada")> _
    Public Property FailureType As String
        Get
            Return Me._failureType
        End Get
        Set(ByVal value As String)
            Me._failureType = value
        End Set
    End Property

    <Display(Name:="Numero Interior")> _
    Public Property InternalNumber As String
        Get
            Return Me._InternalNumber
        End Get
        Set(ByVal value As String)
            Me._InternalNumber = value
        End Set
    End Property

    <Display(Name:="Número de parte")> _
    Public Property PartNumber As String
        Get
            Return Me._partNumber
        End Get
        Set(ByVal value As String)
            Me._partNumber = value
        End Set
    End Property

    <Display(Name:="Clase de producto")> _
    Public Property ProductClass As String
        Get
            Return Me._productClass
        End Get
        Set(ByVal value As String)
            Me._productClass = value
        End Set
    End Property

    <Display(Name:="Descripción")> _
    Public Property productDescription As String
        Get
            Return Me._productDescription
        End Get
        Set(ByVal value As String)
            Me._productDescription = value
        End Set
    End Property

    <Display(Name:="ID del producto")> _
    Public ReadOnly Property ProductID As String
        Get
            Return Me._productID
        End Get
    End Property

    <Display(Name:="Modelo")> _
    Public Property ProductModel As String
        Get
            Return Me._productModel
        End Get
        Set(ByVal value As String)
            Me._productModel = value
        End Set
    End Property

    <Display(Name:="Marca")> _
    Public Property ProductTrademark As String
        Get
            Return Me._productTrademark
        End Get
        Set(ByVal value As String)
            Me._productTrademark = value
        End Set
    End Property

    <Display(Name:="Tipo de producto")> _
    Public Property ProductType As String
        Get
            Return Me._productType
        End Get
        Set(ByVal value As String)
            Me._productType = value
        End Set
    End Property

    <Display(Name:="Razón Social")> _
    Public Property RazonSocial As String
        Get
            Return Me._RazonSocial
        End Get
        Set(ByVal value As String)
            Me._RazonSocial = value
        End Set
    End Property

    <Display(Name:="Revisión")> _
    Public Property Revision As String
        Get
            Return Me._revision
        End Get
        Set(ByVal value As String)
            Me._revision = value
        End Set
    End Property

    <Display(Name:="RFC")> _
    Public Property RFC As String
        Get
            Return Me._RFC
        End Get
        Set(ByVal value As String)
            Me._RFC = value
        End Set
    End Property

    <Display(Name:="Número de serie")> _
    Public Property SerialNumber As String
        Get
            Return Me._serialNumber
        End Get
        Set(ByVal value As String)
            Me._serialNumber = value
        End Set
    End Property

    <Display(Name:="Tipo de servicio")> _
    Public Property ServiceType As String
        Get
            Return Me._serviceType
        End Get
        Set(ByVal value As String)
            Me._serviceType = value
        End Set
    End Property

    <Display(Name:="Estado")> _
    Public Property State As String
        Get
            Return Me._State
        End Get
        Set(ByVal value As String)
            Me._State = value
        End Set
    End Property

    <Display(Name:="Teléfono"), Required> _
    Public Property Telephone As String
        Get
            Return Me._Telephone
        End Get
        Set(ByVal value As String)
            Me._Telephone = value
        End Set
    End Property

    <Display(Name:="Teléfono 2")> _
    Public Property Telephone2 As String
        Get
            Return Me._Telephone2
        End Get
        Set(ByVal value As String)
            Me._Telephone2 = value
        End Set
    End Property

    <Display(Name:="Teléfono 3")> _
    Public Property Telephone3 As String
        Get
            Return Me._Telephone3
        End Get
        Set(ByVal value As String)
            Me._Telephone3 = value
        End Set
    End Property

    <Display(Name:="Código Postal")> _
    Public Property ZipCode As String
        Get
            Return Me._zipCode
        End Get
        Set(ByVal value As String)
            Me._zipCode = value
        End Set
    End Property

    Public Property Reparacion As String
        Get
            Return _Reparacion
        End Get
        Set(value As String)
            _Reparacion = value
        End Set
    End Property

    Public Property Comentarios As String
        Get
            Return _Commentarios
        End Get
        Set(value As String)
            _Commentarios = value
        End Set
    End Property

    ' Fields
    Private _Address As String
    Private _Address2 As String
    Private _City As String
    Private _Comment As String
    Private _Country As String
    Private _customerID As String
    Private _customerName As String
    Private _customerReference As String
    Private _customerType As String
    Private _Delivery As String
    Private _DeliveryTime As String
    Private _email As String
    Private _ExternalNumber As String
    Private _failureType As String
    Private _InternalNumber As String
    Private _partNumber As String
    Private _productClass As String
    Private _productDescription As String
    Private _productID As String
    Private _productModel As String
    Private _productTrademark As String
    Private _productType As String
    Private _RazonSocial As String
    Private _revision As String
    Private _RFC As String
    Private _serialNumber As String
    Private _serviceType As String
    Private _State As String
    Private _Telephone As String
    Private _Telephone2 As String
    Private _Telephone3 As String
    Private _zipCode As String
    '--- Agregar campo, reparacion a realizar y Comentarios
    Private _Reparacion As String
    Private _Commentarios As String
End Class

Public Class ScanPackModel
    Private _OrderID As String
    Private _Reparacion As String
    Private _Comentario As String

    '--- 
    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property Reparacion As String
        Get
            Return _Reparacion
        End Get
        Set(value As String)
            _Reparacion = value
        End Set
    End Property

    Public Property Comentario As String
        Get
            Return _Comentario
        End Get
        Set(value As String)
            _Comentario = value
        End Set
    End Property
End Class

Public Class ScannedOrderModel
    Inherits ScanOrderModel
End Class