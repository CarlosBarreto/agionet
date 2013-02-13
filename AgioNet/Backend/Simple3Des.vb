Imports System.Security.Cryptography
Imports System.IO

Public Class Simple3Des
    '-- Fields
    Private TripleDes As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider()

    ' -- Methods
    Public Sub New(ByVal key As String)
        TripleDes.Key = Me.TruncateHash(key, TripleDes.KeySize / 8)
        TripleDes.IV = Me.truncateHash("", TripleDes.BlockSize / 8)
    End Sub

    Public Function DecryptData(ByVal encryptedtext As String) As String
        Dim buffer() As Byte = Convert.FromBase64String(encryptedtext)
        Dim stream2 As MemoryStream = New MemoryStream()
        Dim stream As CryptoStream = New CryptoStream(stream2, TripleDes.CreateDecryptor(), CryptoStreamMode.Write)

        stream.Write(buffer, 0, buffer.Length)
        stream.FlushFinalBlock()

        Return Encoding.Unicode.GetString(stream2.ToArray())
    End Function

    Public Function EncryptData(plaintext As String)
        Dim bytes() As Byte = Encoding.Unicode.GetBytes(plaintext)
        Dim stream2 As MemoryStream = New MemoryStream()
        Dim stream As CryptoStream = New CryptoStream(stream2, TripleDes.CreateEncryptor(), CryptoStreamMode.Write)

        stream.Write(bytes, 0, bytes.Length)
        stream.FlushFinalBlock()

        Return Convert.ToBase64String(stream2.ToArray())
    End Function

    Private Function TruncateHash(key As String, Optional lenght As Integer = 20) As Byte()
        Dim provider As SHA1CryptoServiceProvider = New SHA1CryptoServiceProvider()
        Dim bytes() As Byte = Encoding.Unicode.GetBytes(key)
        ReDim Preserve bytes(lenght - 1)
        Return bytes
    End Function

End Class
