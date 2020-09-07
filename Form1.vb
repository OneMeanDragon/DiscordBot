Imports System.ComponentModel

Imports Discord             ' NuGet Discord.Net
Imports Discord.WebSocket   ' NuGet Discord.Net

Public Class Form1

    Dim WithEvents discordEv As New DiscordSocketClient

    Private Async Function Connect(tt As TokenType, tstr As String) As Task
        AddHandler discordEv.MessageReceived, AddressOf onMessage
        Await discordEv.LoginAsync(tt, tstr)
        Await discordEv.StartAsync()

        Await Task.Delay(-1)
    End Function

    Private Async Function Disconnect() As Task
        Await discordEv.LogoutAsync()
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Connect Discord
        Try
            Dim dotask As Task = Connect(TokenType.Bot, "############### YOUR oAuthToken ###############")
        Catch ex As Exception
            Dim result As Integer = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        ' Disconnect Discord
        Dim dotask As Task = Disconnect()
    End Sub

    Private Async Function onMessage(message As SocketMessage) As Task
        MsgBox(message.Author.Username & vbCrLf & message.Content) ' MsgBox is just an example do what you want
    End Function


End Class
