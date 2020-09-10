using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MandadosExpress.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HubConnection _connection;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void connectButton_Click(object sender, RoutedEventArgs e)
        {
            await CreateConnection();
        }

        private async void sendButton_Click(object sender, RoutedEventArgs e)
        {

            ListBoxItem item = new ListBoxItem();
            item.HorizontalAlignment = HorizontalAlignment.Right;
            item.Content = messageTextBox.Text;
            item.Padding = new Thickness(2, 5, 0, 5);
            messageList.Items.Add(item);
            await _connection.InvokeAsync("SendMessage", messageTextBox.Text);
            messageTextBox.Text = string.Empty;
        }

        private async Task CreateConnection()
        {

            var baseUri = new Uri(urlTextBox.Text);
            try
            {
                _connection = new HubConnectionBuilder()
                        .WithUrl(new Uri(baseUri, "chathub"))
                        .WithAutomaticReconnect(Enumerable.Repeat(TimeSpan.FromSeconds(5), 4).ToArray())
                        .Build();

                _connection.On<string>("ReceiveMessage", ReceiveMessage);

                await _connection.StartAsync();

                CheckConnection();
            }
            catch (Exception e)
            {

            }
        }

        public async Task ReceiveMessage(string message)
        {
            ListBoxItem item = new ListBoxItem();
            item.HorizontalAlignment = HorizontalAlignment.Left;
            item.Content = message;
            item.Background = new SolidColorBrush(Colors.AliceBlue);
            item.Padding = new Thickness(2, 5, 0, 5);
            messageList.Items.Add(item);
        }

        private void CheckConnection()
        {

            if (_connection?.State == HubConnectionState.Connected)
            {

                statusRectangle.Fill = new SolidColorBrush(Colors.Green);

            }
            else if (_connection?.State == HubConnectionState.Disconnected)
            {
                statusRectangle.Fill = new SolidColorBrush(Colors.Red);

            }
        }
    }
}
