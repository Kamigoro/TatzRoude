using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using TatsRoude.Client.AI;

namespace TatsRoude
{

    class Program
    {
        private DiscordSocketClient _client;
        private IRobot _robot;

        public static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            await InitializeDiscord();
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        public async Task InitializeDiscord()
        {
            _client = new DiscordSocketClient();

            var token = "";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            _robot = new Robot();

            _client.MessageReceived += NewMessageReceived;
        }

        public async Task NewMessageReceived(SocketMessage message)
        {
            if (message.Author.IsBot)
                return;

            var prediction = await _robot.IsThisToxic(message.Content);
            if (prediction.Prediction)
            {
                Console.WriteLine($"{message.Content} is {prediction.Prediction}");
                await message.AddReactionAsync(new Emoji("\uD83D\uDE21"));
            }

            return;
        }
    }
}
