using FirstBotDiscord.Entities.B3;
using Microsoft.AspNetCore.SignalR.Client;

namespace FirstBotDiscord.B3Api
{
    public class B3Api
    {
        public static void B3(string[] args)
        {
            HubConnection connection;

            connection = new HubConnectionBuilder().WithUrl(new System.Uri("https://b3api.webmat.com.br/HubConnection?Token=" + Configurations.Parameters.CredentialCode)).WithAutomaticReconnect().Build();
            connection.On<string>("LogOut", (msg) => LogOut(msg));

            connection.On<string>("UpdateList", (ticker) => UpdateList(ticker));

            connection.StartAsync();
        }

        public static void LogOut(string msg)
        {
            System.Console.WriteLine(msg);
        }

        public static object UpdateList(string ticker)
        {
            Customers customers = new();
            ticker = customers.Symbol;

            return ticker;
        }
    }
}