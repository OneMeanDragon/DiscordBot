Imports System.ComponentModel

Imports Discord             ' NuGet Discord.Net
Imports Discord.WebSocket   ' NuGet Discord.Net

Public Class Form1

    Dim WithEvents discordEv As New DiscordSocketClient

    Private Async Function Connect(tt As TokenType, tstr As String) As Task
        Await discordEv.LoginAsync(tt, tstr)
        Await discordEv.StartAsync()

        Await Task.Delay(-1)
    End Function

    Private Async Function Disconnect() As Task
        Await discordEv.LogoutAsync()
    End Function

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddHandler discordEv.Ready, AddressOf onReady
        AddHandler discordEv.MessageReceived, AddressOf onMessage
        
        ' Connect Discord
        Try
            Await Connect(TokenType.Bot, "############### YOUR oAuthToken ###############")
            Await discordEv.StartAsync()
        Catch ex As Exception
            Dim result As Integer = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Async Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        ' Disconnect Discord
        If discordEv.LoginState = LoginState.LoggedIn Then
            Await Disconnect()
        End If
        Await discordEv.StopAsync()
    End Sub
    
    Private Delegate Sub mUpdateCaption(ByVal sCaption As String)
    Private Sub UpdateCaption(ByVal sCaption As String)
        If Me.InvokeRequired() Then
            Dim udcap As New mUpdateCaption(AddressOf UpdateCaption)
            Me.Invoke(udcap, sCaption)
        Else
            Me.Text = sCaption
        End If
    End Sub

    Private Function onReady() As Task
        UpdateCaption("Logged on as: " + discordEv.CurrentUser.Username) 'LoggedIn event still dosent capture CurrentUser info, so stuck with this.

        Return Task.CompletedTask
    End Function
    
    Private Async Function onMessage(message As SocketMessage) As Task
        'MsgBox(message.Author.Username & vbCrLf & message.Content) ' MsgBox is just an example do what you want

        If message.Author.Id = discordEv.CurrentUser.Id Then 'Check if self message
            'The reason this if statement is here, is because you get sent back the messages that you send.
            'Ignore
        Else
            Await message.Channel.SendMessageAsync("Test") 'Bot must have user permissions in the channel to send message, unless it was a private DM
        End If
    End Function

End Class
