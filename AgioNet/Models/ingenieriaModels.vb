Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

' 2013.02.14
Public Class EstacionModel
    ' Properties
    Public Property Nombre As String
        Get
            Return Me._Nombre
        End Get
        Set(ByVal value As String)
            Me._Nombre = value
        End Set
    End Property


    ' Fields
    Private _Nombre As String
End Class

' 2013.02.13
Public Class EstacionesModel
    ' Properties
    Public Property Descripcion As String
        Get
            Return Me._Descripcion
        End Get
        Set(ByVal value As String)
            Me._Descripcion = value
        End Set
    End Property

    Public Property Nombre As String
        Get
            Return Me._Nombre
        End Get
        Set(ByVal value As String)
            Me._Nombre = value
        End Set
    End Property

    Public Property Proceso As String
        Get
            Return Me._Proceso
        End Get
        Set(ByVal value As String)
            Me._Proceso = value
        End Set
    End Property

    Public Property Usuario As String
        Get
            Return Me._Usuario
        End Get
        Set(ByVal value As String)
            Me._Usuario = value
        End Set
    End Property


    ' Fields
    Private _Descripcion As String
    Private _Nombre As String
    Private _Proceso As String
    Private _Usuario As String
End Class

'2013.03.13 
' 2013.03.14 - Upd cambiar lvl por Commodity
Public Class MaterialMasterModel
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

    Public Property Commodity As String
        Get
            Return Me._Commodity
        End Get
        Set(ByVal value As String)
            Me._Commodity = value
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

    Public Property OldSKUNo As String
        Get
            Return _OldSKUNo
        End Get
        Set(value As String)
            _OldSKUNo = value
        End Set
    End Property

    Public Property OldPartNo As String
        Get
            Return _OldPartNo
        End Get
        Set(value As String)
            _OldPartNo = value
        End Set
    End Property

    ' Fields
    Private _Alt As String
    Private _Description As String
    'Private _lvl As String '-- Not Used 2013.03.14
    Private _PartNo As String
    Private _SKUNo As String

    ' -- 2013.02.22 -- Modificacion SKU
    Private _OldSKUNo As String
    Private _OldPartNo As String

    '-- 2013.03.14 -- Modificacion para agregar Commodity
    Private _Commodity As String

End Class

'2013.02.13
'2013.03.14 - Upd Agregar Commodity
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

    Public Property Commodity As String
        Get
            Return _Commodity
        End Get
        Set(value As String)
            _Commodity = value
        End Set
    End Property

    ' Fields
    Private _Alt As String
    Private _Description As String
    Private _PartNo As String
    Private _SKUNo As String
    '-- 2013.03.14 -- Modificacion para agregar Commodity
    Private _Commodity As String
End Class

Public Class UPDMaterialMasterModel
    Inherits MaterialMasterListModel
    Private _OldPartNo As String
    Public Property OldPartNo As String
        Get
            Return _OldPartNo
        End Get
        Set(value As String)
            _OldPartNo = value
        End Set
    End Property
End Class
' 2014.02.13
Public Class UserListModel
    ' Properties
    Public Property Nombre As String
        Get
            Return Me._Nombre
        End Get
        Set(ByVal value As String)
            Me._Nombre = value
        End Set
    End Property

    Public Property Usuario As String
        Get
            Return Me._Usuario
        End Get
        Set(ByVal value As String)
            Me._Usuario = value
        End Set
    End Property


    ' Fields
    Private _Nombre As String
    Private _Usuario As String
End Class

' 2013.05.13
<System.AttributeUsage(AttributeTargets.Method, AllowMultiple:=False, Inherited:=True)>
Public Class MultiButtonAttribute
    Inherits ActionNameSelectorAttribute

    Private _MatchFormKey As String
    Private _MatchFormValue As String

    Public Property MatchFormKey As String
        Get
            Return _MatchFormKey
        End Get
        Set(value As String)
            _MatchFormKey = value
        End Set
    End Property

    Public Property MatchFormValue As String
        Get
            Return _MatchFormValue
        End Get
        Set(value As String)
            _MatchFormValue = value
        End Set
    End Property


    Public Overrides Function IsValidName(controllerContext As ControllerContext, actionName As String, methodInfo As Reflection.MethodInfo) As Boolean
        Dim Response As Boolean
        Response = Not controllerContext.HttpContext.Request(MatchFormKey) Is Nothing And _
                   controllerContext.HttpContext.Request(MatchFormKey) = MatchFormValue
        Return Response
    End Function
End Class

Public Class CommodityListModel
    Private _CommodityName As String
    Private _CommodityDescription As String

    Public Property CommodityName As String
        Get
            Return _CommodityName
        End Get
        Set(value As String)
            _CommodityName = value
        End Set
    End Property

    Public Property CommodityDescription As String
        Get
            Return _CommodityDescription
        End Get
        Set(value As String)
            _CommodityDescription = value
        End Set
    End Property
End Class

'2013.05.27
Public Class AddCommodityModel
    Inherits CommodityListModel
    Private _CommodityID As String
    Public Property CommodityID As String
        Get
            Return _CommodityID
        End Get
        Set(value As String)
            _CommodityID = value
        End Set
    End Property
End Class