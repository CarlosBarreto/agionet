Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

' 2013.02.26
Public Class AllOrderInfoModel
    Private _OrderID As String
    Private _OrderDate As String
    Private _CustomerType As String
    Private _CustomerName As String
    Private _RFC As String
    Private _Email As String
    Private _Address As String
    Private _ExternalNumber As String
    Private _InternalNumber As String
    Private _Address2 As String
    Private _City As String
    Private _State As String
    Private _Country As String
    Private _ZipCode As String
    Private _Telephone As String
    Private _Telephone2 As String
    Private _Telephone3 As String
    Private _Delivery As String
    Private _DeliveryTime As String
    Private _ProductClass As String
    Private _ProductType As String
    Private _ProductTrademark As String
    Private _ProductModel As String
    Private _ProductDescription As String
    Private _PartNumber As String
    Private _SerialNumber As String
    Private _Revision As String
    Private _ServiceType As String
    Private _FailureType As String
    Private _Comment As String

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

    Public Property CustomerType As String
        Get
            Return _CustomerType
        End Get
        Set(value As String)
            _CustomerType = value
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
    Public Property RFC As String
        Get
            Return _RFC
        End Get
        Set(value As String)
            _RFC = value
        End Set
    End Property

    Public Property Email As String
        Get
            Return _Email
        End Get
        Set(value As String)
            _Email = value
        End Set
    End Property

    Public Property Address As String
        Get
            Return _Address
        End Get
        Set(value As String)
            _Address = value
        End Set
    End Property

    Public Property ExternalNumber As String
        Get
            Return _ExternalNumber
        End Get
        Set(value As String)
            _ExternalNumber = value
        End Set
    End Property

    Public Property InternalNumber As String
        Get
            Return _InternalNumber
        End Get
        Set(value As String)
            _InternalNumber = value
        End Set
    End Property

    Public Property Address2 As String
        Get
            Return _Address2
        End Get
        Set(value As String)
            _Address2 = value
        End Set
    End Property

    Public Property City As String
        Get
            Return _City
        End Get
        Set(value As String)
            _City = value
        End Set
    End Property

    Public Property State As String
        Get
            Return _State
        End Get
        Set(value As String)
            _State = value
        End Set
    End Property

    Public Property Country As String
        Get
            Return _Country
        End Get
        Set(value As String)
            _Country = value
        End Set
    End Property

    Public Property ZipCode As String
        Get
            Return _ZipCode
        End Get
        Set(value As String)
            _ZipCode = value
        End Set
    End Property

    Public Property Telephone As String
        Get
            Return _Telephone
        End Get
        Set(value As String)
            _Telephone = value
        End Set
    End Property

    Public Property Telephone2 As String
        Get
            Return _Telephone2
        End Get
        Set(value As String)
            _Telephone2 = value
        End Set
    End Property

    Public Property Telephone3 As String
        Get
            Return _Telephone3
        End Get
        Set(value As String)
            _Telephone3 = value
        End Set
    End Property

    Public Property Delivery As String
        Get
            Return _Delivery
        End Get
        Set(value As String)
            _Delivery = value
        End Set
    End Property

    Public Property DeliveryTime As String
        Get
            Return _DeliveryTime
        End Get
        Set(value As String)
            _DeliveryTime = value
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

    Public Property Revision As String
        Get
            Return _Revision
        End Get
        Set(value As String)
            _Revision = value
        End Set
    End Property

    Public Property ServiceType As String
        Get
            Return _ServiceType
        End Get
        Set(value As String)
            _ServiceType = value
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

'2013.02.14
Public Class AppFailureInfoModel
    ' Properties
    Public Property OrderID As String
        Get
            Return Me._OrderID
        End Get
        Set(ByVal value As String)
            Me._OrderID = value
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

    Public Property TestDescription As String
        Get
            Return Me._TestDescription
        End Get
        Set(ByVal value As String)
            Me._TestDescription = value
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

    Public Property FoundBy As String
        Get
            Return Me._FoundBy
        End Get
        Set(ByVal value As String)
            Me._FoundBy = value
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

    ' Fields
    Private _OrderID As String
    Private _TestID As String
    Private _TestDescription As String
    Private _FailureID As String
    Private _Solution As String
    Private _Source As String
    Private _FoundBy As String
    Private _FoundDate As String
End Class

' 2013.02.28
Public Class ApprovalRepairModel
    Private _OrderID As String
    Private _Approval As String
    Private _Comments As String
    Private _ApproBy As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property Approval As String
        Get
            Return _Approval
        End Get
        Set(value As String)
            _Approval = value
        End Set
    End Property

    Public Property Comments As String
        Get
            Return _Comments
        End Get
        Set(value As String)
            _Comments = value
        End Set
    End Property

    Public Property ApproBy As String
        Get
            Return _ApproBy
        End Get
        Set(value As String)
            _ApproBy = value
        End Set
    End Property
End Class

' 2013.02.01 
Public Class AppCostModel
    Private _OrderID As String
    Private _Costo As String
    Private _Flete As String
    Private _GastosImportacion As String
    Private _LeadTime As String
    Private _Proveedor As String
    Private _Comentario As String
    Private _CostBy As String

    '----- 
    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property Costo As String
        Get
            Return _Costo
        End Get
        Set(value As String)
            _Costo = value
        End Set
    End Property

    Public Property Flete As String
        Get
            Return _Flete
        End Get
        Set(value As String)
            _Flete = value
        End Set
    End Property

    Public Property GastosImportacion As String
        Get
            Return _GastosImportacion
        End Get
        Set(value As String)
            _GastosImportacion = value
        End Set
    End Property

    Public Property LeadTime As String
        Get
            Return _LeadTime
        End Get
        Set(value As String)
            _LeadTime = value
        End Set
    End Property

    Public Property Proveedor As String
        Get
            Return _Proveedor
        End Get
        Set(value As String)
            _Proveedor = value
        End Set
    End Property

    <AllowHtml> _
    Public Property Comentario As String
        Get
            Return _Comentario
        End Get
        Set(value As String)
            _Comentario = value
        End Set
    End Property

    Public Property CostBy As String
        Get
            Return _CostBy
        End Get
        Set(value As String)
            _CostBy = value
        End Set
    End Property

End Class

' 2013.02.14
Public Class CreateOrderModel
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
End Class

' 2013.02.28
Public Class OrdenesDiagnosticadas
    Private _OrderID As String
    Private _Incoming As String
    Private _FoundDate As String
    Private _ProductModel As String
    Private _ProductDescription As String
    Private _PartNumber As String
    Private _SerialNumber As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property Incoming As String
        Get
            Return _Incoming
        End Get
        Set(value As String)
            _Incoming = value
        End Set
    End Property

    Public Property FoundDate As String
        Get
            Return _FoundDate
        End Get
        Set(value As String)
            _FoundDate = value
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

End Class

' 2013.03.15
Public Class OrdenesCosteadasModel
    Private _OrderID As String
    Private _Incoming As String
    Private _ProductModel As String
    Private _ProductDescription As String
    Private _PartNumber As String
    Private _SerialNumber As String

    '--- Propiedades
    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property Incoming As String
        Get
            Return _Incoming
        End Get
        Set(value As String)
            _Incoming = value
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
End Class
' 2013.02.13
Public Class ReqApprovalModel
    ' Properties
    Public Property Comment As String
        Get
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

' 2013-02.26
Public Class SearchOrderModel
    Private _OrderID As String

    Public Property OrderID As String
        Get
            If _OrderID = "" Or IsNothing(_OrderID) Then _OrderID = ""
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property
End Class

' 2013.03.15 
Public Class SubOrderListModel
    Private _OrderID As String
    Private _PartNumber As String
    Private _Description As String
    Private _Commodity As String
    Private _SerialNumber As String
    Private _Costo As String
    Private _LeadTime As String
    Private _Failure As String
    Private _Solution As String

    '--- Propiedades
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

    Public Property Description As String
        Get
            Return _Description
        End Get
        Set(value As String)
            _Description = value
        End Set
    End Property

    Public Property Commodity As String
        Get
            Return _Commodity
        End Get
        Set(value As String)
            _Commodity = value
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

    Public Property Costo As String
        Get
            Return _Costo
        End Get
        Set(value As String)
            _Costo = value
        End Set
    End Property

    Public Property LeadTime As String
        Get
            Return _LeadTime
        End Get
        Set(value As String)
            _LeadTime = value
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

End Class

'2013.03.19
Public Class CotizarClienteModel
    Private _OrderID As String
    Private _ManoObra As String
    Private _Viaje As String
    Private _SubTotal As String
    Private _IVA As String
    Private _Total As String
    Private _Leadtime As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property ManoObra As String
        Get
            Return _ManoObra
        End Get
        Set(value As String)
            _ManoObra = value
        End Set
    End Property

    Public Property Viaje As String
        Get
            Return _Viaje
        End Get
        Set(value As String)
            _Viaje = value
        End Set
    End Property

    Public Property SubTotal As String
        Get
            Return _SubTotal
        End Get
        Set(value As String)
            _SubTotal = value
        End Set
    End Property

    Public Property IVA As String
        Get
            Return _IVA
        End Get
        Set(value As String)
            _IVA = value
        End Set
    End Property

    Public Property Total As String
        Get
            Return _Total
        End Get
        Set(value As String)
            _Total = value
        End Set
    End Property

    Public Property LeadTime As String
        Get
            Return _Leadtime
        End Get
        Set(value As String)
            _Leadtime = value
        End Set
    End Property

End Class