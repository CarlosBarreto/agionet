Imports AgioNet.agGlobals
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

    Public Sub getPermissionList(Type As String, User As String, _Module As String) '(string Type, string User, string _Module = "")
        Dim index As Integer = 0
        DR = ExecuteSP("epermissionGetPermission", Type, User, _Module)
        ReDim _permissionList(20)
        ReDim _PermissionSubList(20)

        If IsDBNull(DR) And DR.HasRows Then
            While DR.Read
                _permissionList(index) = RD(1).ToString
                _PermissionSubList(index) = RD(0).ToString

                index = index + 1
            End While

            ReDim Preserve _permissionList(index - 1)
            ReDim Preserve _PermissionSubList(index - 1)
        End If

        If Not DR.IsClosed Then
            DR.Close()
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
            DR = ExecuteSP("sys_UserLogin ", _User, TextEncoding(_Password))
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
            'ProjectData.SetProjectError(exception1);
            'Exception exception = exception1;
            'this._logOnMessage = exception.Message;
            'this._errorFlag = true;
            'str2 = "Error";
            'ProjectData.ClearProjectError();
        End Try
    End Function

    '-- Porperties
    Public ReadOnly Property ErrorFlag As String
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
