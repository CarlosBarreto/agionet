Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization

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

