Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

Public Class ProcesoCalidadModels
    Private _OrderID As String
    Private _PartNumber As String
    Private _SerialNumber As String
    Private _FailureType As String
    Private _Comment As String
    Private _DateComp As String
    '--
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

    Public Property DateComp As String
        Get
            Return _DateComp
        End Get
        Set(value As String)
            _DateComp = value
        End Set
    End Property

End Class

Public Class InspeccionCalidadModel
    Private _OrderID As String
    Private _Result As String
    Private _Comment As String
    Private _Acepto As Boolean

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
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

    Public Property Comment As String
        Get
            Return _Comment
        End Get
        Set(value As String)
            _Comment = value
        End Set
    End Property

    <Display(Name:="Aceptar")> _
    Public Property Acepto As Boolean
        Get
            Return _Acepto
        End Get
        Set(value As Boolean)
            _Acepto = value
        End Set
    End Property

End Class

Public Class DatosCalidadModel
    Private _OrderID As String
    Private _PartNumber As String
    Private _Description As String
    Private _Aprobado As String
    Private _Failure As String
    Private _Solution As String
    Private _TipoReparacion As String
    Private _Comentario As String
    Private _ReparadoPor As String
    Private _RetroAC As String

    '2013.07.17
    Private _NewPart As String
    Private _NewSerialNo As String
    Private _OldPartNo As String
    Private _OldSerialNo As String
    Private _Process As String

    '-----------------
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

    Public Property Aprobado As String
        Get
            Return _Aprobado
        End Get
        Set(value As String)
            _Aprobado = value
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

    Public Property TipoReparacion As String
        Get
            Return _TipoReparacion
        End Get
        Set(value As String)
            _TipoReparacion = value
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

    Public Property ReparadoPor As String
        Get
            Return _ReparadoPor
        End Get
        Set(value As String)
            _ReparadoPor = value
        End Set
    End Property

    Public Property RetroAC As String
        Get
            Return _RetroAC
        End Get
        Set(value As String)
            _RetroAC = value
        End Set
    End Property

    '2013.07.17
    Public Property NewPart As String
        Get
            Return _NewPart
        End Get
        Set(value As String)
            _NewPart = value
        End Set
    End Property

    Public Property NewSerialNo As String
        Get
            Return _NewSerialNo
        End Get
        Set(value As String)
            _NewSerialNo = value
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

    Public Property OldSerialNo As String
        Get
            Return _OldSerialNo
        End Get
        Set(value As String)
            _OldSerialNo = value
        End Set
    End Property

    Public Property Process As String
        Get
            Return _Process
        End Get
        Set(value As String)
            _Process = value
        End Set
    End Property
End Class

' 2013.05.08
Public Class DatosFallaModel
    Private _OrderID As String
    Private _Falla As String
    Private _Retro As String
    Private _Comment As String
    Private _TComment As String
    Private _ComentarioCierre As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property Falla As String
        Get
            Return _Falla
        End Get
        Set(value As String)
            _Falla = value
        End Set
    End Property

    Public Property Retro As String
        Get
            Return _Retro
        End Get
        Set(value As String)
            _Retro = value
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

    Public Property TComment As String
        Get
            Return _TComment
        End Get
        Set(value As String)
            _TComment = value
        End Set
    End Property

    Public Property ComentarioCierre As String
        Get
            Return _ComentarioCierre
        End Get
        Set(value As String)
            _ComentarioCierre = value
        End Set
    End Property
End Class