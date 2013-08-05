Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail

Public Class agEmail
    'Fields
    Private _HTMLBody As Boolean
    Private _mailContact As String

    Protected mail As MailMessage
    Protected smtp As SmtpClient

    ' Fields
    Protected Friend DA As DataAccess
    Protected Friend DR As SqlDataReader

    'Methods
    Public Sub New()
        _mailContact = "agionet@agiotech.com"
    End Sub

    ''' <summary>
    ''' Envia un correo, tambien registra el correo en la base de datos en caso de que el correo no pueda ser enviado
    ''' </summary>
    ''' <param name="EmailAddress"></param>
    ''' <param name="txtSubject"></param>
    ''' <param name="txtMessage"></param>
    ''' <remarks></remarks>
    Public Sub SendEmail(EmailAddress As String, txtSubject As String, txtMessage As String)
        Me.DA = New DataAccess(__SERVER__, __DATABASE__, __USER__, __PASS__)
        Dim MailID As String = String.Empty

        Try
            'Guardar el correo en la Base de datos
            DR = DA.ExecuteSP("sys_saveEmail", EmailAddress, _mailContact, txtSubject, txtMessage)
            If DA._LastErrorMessage <> "" Then
                Throw New Exception(DA._LastErrorMessage)
            Else
                While DR.Read
                    MailID = DR(0)
                End While
            End If

            If Not DR.IsClosed Then
                DR.Close()
            End If

            mail = New MailMessage()
            mail.To.Add(EmailAddress)
            mail.From = New MailAddress(_mailContact)
            mail.Subject = txtSubject
            mail.Body = txtMessage
            mail.IsBodyHtml = _HTMLBody
            smtp = New SmtpClient()
            smtp.Host = "mail.agiotech.com"
            smtp.Port = 587
            smtp.Credentials = New NetworkCredential("carlos.barreto@agiotech.com", "Agiotech01")
            smtp.Send(mail)


            ' Si no ocurre error, cambiar el status de new a DONE
            DR = DA.ExecuteSP("sys_mailStatusCh", MailID, "DONE")
        Catch ex As Exception
            'En caso de error, cambiar el estatus
            If MailID <> "" Then
                DR = DA.ExecuteSP("sys_mailStatusCh", MailID, "FAIL")
            End If
        End Try

    End Sub

    'Properties
    Public Property HTMLBody As Boolean
        Get
            Return _HTMLBody
        End Get
        Set(value As Boolean)
            _HTMLBody = value
        End Set
    End Property

    Public Property mailContact As String
        Get
            Return _mailContact
        End Get
        Set(value As String)
            _mailContact = value
        End Set
    End Property
End Class
