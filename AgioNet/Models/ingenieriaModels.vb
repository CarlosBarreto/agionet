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

'2013.02.14
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

    Public Property Lvl As String
        Get
            Return Me._lvl
        End Get
        Set(ByVal value As String)
            Me._lvl = value
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
    Private _lvl As String
    Private _PartNo As String
    Private _SKUNo As String

    ' -- 2013.02.22 -- Modificacion SKU
    Private _OldSKUNo As String
    Private _OldPartNo As String

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