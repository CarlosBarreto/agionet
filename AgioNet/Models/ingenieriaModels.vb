Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

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

