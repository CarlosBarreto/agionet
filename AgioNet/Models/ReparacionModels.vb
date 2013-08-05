Imports AgioNet.DataAccess
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Data.SqlClient
Imports System.Globalization

' 2013.03.01
Public Class PendientesReparacionModel
    Private _OrderID As String
    Private _Comments As String
    Private _ApprovalDate As String
    Private _PartNumber As String
    Private _ProductDescription As String
    Private _SerialNumber As String
    Private _Failure As String
    Private _Solution As String
    Private _Source As String
    Private _Comment As String
    Private _RepairStatus As String
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

    Public Property Comments As String
        Get
            Return _Comments
        End Get
        Set(value As String)
            _Comments = value
        End Set
    End Property

    Public Property ApprovalDate As String
        Get
            Return _ApprovalDate
        End Get
        Set(value As String)
            _ApprovalDate = value
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

    Public Property ProductDescription As String
        Get
            Return _ProductDescription
        End Get
        Set(value As String)
            _ProductDescription = value
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

    Public Property RepairStatus As String
        Get
            Return _RepairStatus
        End Get
        Set(value As String)
            _RepairStatus = value
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

' 2013.03.04
Public Class ReplaceHistoryModel
    Private _OrderID As String
    Private _OldPartNo As String
    Private _NewPartNo As String
    Private _Failure As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
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

    Public Property NewPartNo As String
        Get
            Return _NewPartNo
        End Get
        Set(value As String)
            _NewPartNo = value
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
End Class

' 2013.03.04
Public Class ReplacePartModel
    Private _OrderID As String
    Private _PartNo As String
    Private _SerialNo As String
    Private _NewPartNo As String
    Private _NewSerialNo As String
    Private _Failure As String
    Private _Comment As String

    <Required()> _
    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    <Required()> _
    Public Property PartNo As String
        Get
            Return _PartNo
        End Get
        Set(value As String)
            _PartNo = value
        End Set
    End Property

    <Required()> _
    Public Property SerialNo As String
        Get
            Return _SerialNo
        End Get
        Set(value As String)
            _SerialNo = value
        End Set
    End Property

    <Required()> _
    Public Property NewPartNo As String
        Get
            Return _NewPartNo
        End Get
        Set(value As String)
            _NewPartNo = value
        End Set
    End Property

    <Required()> _
    Public Property NewSerialNo As String
        Get
            Return _NewSerialNo
        End Get
        Set(value As String)
            _NewSerialNo = value
        End Set
    End Property

    <Required()> _
    Public Property Failure As String
        Get
            Return _Failure
        End Get
        Set(value As String)
            _Failure = value
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

Public Class FailureFormInfoModel
    Private _CaseType As String
    Private _OrderID As String
    Private _PartNo As String
    Private _SerialNo As String
    Private _OldPartNo As String
    Private _OldSerialNo As String
    Private _Failure As String
    Private _Comment As String
    Private _ErrorMsg As String

    Public Property CaseType As String
        Get
            Return _CaseType
        End Get
        Set(value As String)
            _CaseType = value
        End Set
    End Property

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
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

    Public Property Failure As String
        Get
            Return _Failure
        End Get
        Set(value As String)
            _Failure = value
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

    Public Property ErrorMsg As String
        Get
            Return _ErrorMsg
        End Get
        Set(value As String)
            _ErrorMsg = value
        End Set
    End Property

    'metodos 
    Public Sub getFailureFormInfo(ByVal _OrderID_ As String)
        Dim DA As New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
        Dim DR As SqlDataReader
        Try
            DR = DA.ExecuteSP("rp_getFailureFormInfo", _OrderID_)
            If DA._LastErrorMessage <> "" Then Throw New Exception(DA._LastErrorMessage)
            If DR.HasRows Then
                Do While DR.Read
                    _CaseType = DR(0)
                    _OrderID = DR(1)
                    _PartNo = DR(2)
                    _SerialNo = DR(3)
                    _OldPartNo = DR(4)
                    _OldSerialNo = DR(5)
                    _Failure = DR(6)
                    _Comment = DR(7)
                Loop
            Else
                Throw New Exception("Error: No se han encontrado datos")
            End If
            _ErrorMsg = ""
        Catch ex As Exception
            _ErrorMsg = ex.Message
        Finally
            DA.Dispose()
        End Try
    End Sub
End Class

' 2013.03.05
Public Class AddNewPartModel
    Private _OrderID As String
    Private _PartNo As String
    Private _SerialNo As String
    Private _Failure As String
    Private _Comment As String
    Private _ErrorMsg As String
    '--- 
    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
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

    Public Property Failure As String
        Get
            Return _Failure
        End Get
        Set(value As String)
            _Failure = value
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

    Public Property ErrorMsg As String
        Get
            Return _ErrorMsg
        End Get
        Set(value As String)
            _ErrorMsg = value
        End Set
    End Property
    '----- 
End Class

' 2013.03.05
Public Class RemplazoComponenteModel
    Private _OrderID As String
    Private _PartNo As String
    Private _SerialNo As String
    Private _Componente As String
    Private _Failure As String
    Private _Process As String
    Private _Comment As String

    Private _Command As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
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

    Public Property Componente As String
        Get
            Return _Componente
        End Get
        Set(value As String)
            _Componente = value
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

    Public Property Process As String
        Get
            Return _Process
        End Get
        Set(value As String)
            _Process = value
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

    Public Property Command As String
        Get
            Return _Command
        End Get
        Set(value As String)
            _Command = value
        End Set
    End Property
End Class

' 2013.03.06
Public Class ReplaceCompHistoryModel
    Private _OrderID As String
    Private _Component As String
    Private _Failure As String
    Private _Process As String

    Public Property OrderID As String
        Get
            Return _OrderID
        End Get
        Set(value As String)
            _OrderID = value
        End Set
    End Property

    Public Property Component As String
        Get
            Return _Component
        End Get
        Set(value As String)
            _Component = value
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

    Public Property Process As String
        Get
            Return _Process
        End Get
        Set(value As String)
            _Process = value
        End Set
    End Property
End Class

' 2013.05.20
Public Class IniciarReparacionModel
    Inherits ScanOrderModel

    Private _User As String
    Public Property User As String
        Get
            Return _User
        End Get
        Set(value As String)
            _User = value
        End Set
    End Property

End Class