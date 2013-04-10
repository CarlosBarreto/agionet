Imports System.Net
Imports System.Net.Mail

Public Class agEmail
    'Fields
    Private _HTMLBody As Boolean
    Private _mailContact As String

    Protected mail As MailMessage
    Protected smtp As SmtpClient

    'Methods
    Public Sub New()
        _mailContact = "atencionaclientes@agiotech.com"
    End Sub

    Public Sub SendEmail(EmailAddress As String, txtSubject As String, txtMessage As String)
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
