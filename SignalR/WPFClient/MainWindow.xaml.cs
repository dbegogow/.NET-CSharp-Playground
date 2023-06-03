using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;

namespace WPFClient;

public partial class MainWindow : Window
{
    HubConnection connection;

    public MainWindow()
    {
        InitializeComponent();

        connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7034/chathub")
            .WithAutomaticReconnect()
            .Build();

        connection.Reconnecting += (sender)
            => Task.FromResult(this.Dispatcher.InvokeAsync(() =>
                {
                    var newMessage = "Attempting to reconnect...";
                    messages.Items.Add(newMessage);
                }));

        connection.Reconnected += (sender)
            => Task.FromResult(this.Dispatcher.InvokeAsync(() =>
            {
                var newMessage = "Reconnected to the server";
                messages.Items.Clear();
                messages.Items.Add(newMessage);
            }));

        connection.Closed += (sender)
            => Task.FromResult(this.Dispatcher.InvokeAsync(() =>
            {
                var newMessage = "Connection Closed";
                messages.Items.Add(newMessage);
                openConnection.IsEnabled = true;
                sendMessage.IsEnabled = false;
            }));
    }

    private async void openConnection_Click(object sender, RoutedEventArgs e)
    {
        connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            this.Dispatcher.InvokeAsync(() =>
            {
                var newMessage = $"{user}: {message}";
                messages.Items.Add(newMessage);
            });
        });

        try
        {
            await connection.StartAsync();
            messages.Items.Add("Connection Started");
            openConnection.IsEnabled = false;
            sendMessage.IsEnabled = true;
        }
        catch (Exception ex)
        {
            messages.Items.Add(ex.Message);
        }
    }

    private async void sendMessage_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            await connection.InvokeAsync(
                "SendMessage", "WPF Client", messageInput.Text);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
