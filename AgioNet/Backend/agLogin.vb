Imports System.Data.SqlClient

Public Class agLogin
    Inherits DataAccess

    '-- Fields
    Private _errorFlag As Boolean
    Private _logOnMessage As String
    Private _permissionList() As String
    Private _PermissionSubList() As String
    Protected DR As SqlDataReader

    '-- Methods
    Public Sub New()
        MyBase.New(__SERVER__, __DATABASE__, __USER__, __PASS__)
    End Sub

    Public Sub ChangePassword(OldPassword As String, NewPassword As String, CNewPassword As String)
        DR = ExecuteSP("euserChangePassword", TextEncoding(OldPassword), TextEncoding(NewPassword), TextEncoding(CNewPassword))
        If _LastErrorMessage = "" Then
            _logOnMessage = "Success"
            _errorFlag = False
            If Not DR.IsClosed Then
                DR.Close()
            End If
        Else
            _logOnMessage = _LastErrorMessage
            _errorFlag = True
        End If
    End Sub

    Public Sub getPermissionList(Type As String, User As String, Optional _Module As String = "") '(string Type, string User, string _Module = "")
        Dim index As Integer = 0
        Me.DR = Me.ExecuteSP("epermissionGetPermission", Type, User, _Module)
        Me._permissionList = New String(11 - 1) {}
        Me._PermissionSubList = New String(11 - 1) {}

        If (Not Me.DR Is Nothing And DR.HasRows) Then
            Do While Me.DR.Read
                Me._permissionList(index) = DR(1)
                Me._PermissionSubList(index) = DR(0)
                index += 1
            Loop

            ReDim Preserve _permissionList(index - 1)
            ReDim Preserve _PermissionSubList(index - 1)

            If Not Me.DR.IsClosed Then
                Me.DR.Close()
            End If
        Else
            _permissionList = Nothing
            _PermissionSubList = Nothing
        End If
    End Sub

    Public Function LogOn(_User As String, _Password As String) As String
        Dim str2 As String = String.Empty
        If _User = "" Then
            _logOnMessage = "El campo Usuario no puede quedar en blanco"
            str2 = "Error"
        End If

        If _Password = "" Then
            _logOnMessage = "El campo Contraseña no puede quedar en Blanco"
            str2 = "Error"
        End If

        Try
            Dim textcode As String = TextEncoding(_Password)
            DR = ExecuteSP("sys_UserLogin ", _User, textcode)
            If _LastErrorMessage <> "" Then
                _logOnMessage = _LastErrorMessage
                _errorFlag = True
                Return "Error"
            End If

            _logOnMessage = "Success"
            _errorFlag = False
            str2 = _User
            If Not DR.IsClosed Then
                DR.Close()
            End If
        Catch ex As Exception
            _logOnMessage = ex.Message
            _errorFlag = True
            str2 = "Error"
        End Try

        Return _User
    End Function

    '-- Porperties
    Public ReadOnly Property ErrorFlag As Boolean
        Get
            Return _errorFlag
        End Get
    End Property

    Public ReadOnly Property LogOnMessage As String
        Get
            Return _logOnMessage
        End Get
    End Property

    Public ReadOnly Property PermissionList As String()
        Get
            Return _permissionList
        End Get
    End Property

    Public ReadOnly Property PermissionSubList As String()
        Get
            Return _PermissionSubList
        End Get
    End Property
End Class
