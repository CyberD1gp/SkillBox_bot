using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using Discord.Net;

namespace SkillBox_bot
{
    class Program
    {
        DiscordSocketClient _client;

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.MessageReceived += CommandsHandler;
            _client.SlashCommandExecuted += SlashCommandHandler;
            _client.Ready += Client_Ready;


            _client.Log += Log;

            var token = File.ReadAllText(@"C:\Users\pyatn\source\repos\SkillBox_bot\SkillBox_bot\bin\Debug\Token.txt");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            Console.ReadLine();
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private Task CommandsHandler(SocketMessage msg)
        {
            if(!msg.Author.IsBot)
                switch (msg.Content)
                {
                    case "!hi":
                        {
                            
                            msg.Channel.SendMessageAsync($"Hi {msg.Author.Mention} ");
                            break;

                        }
                    case "!rand":
                        {
                            Random rand = new Random();
                            msg.Channel.SendMessageAsync($"Random = {rand.Next(-100, 100)}");
                            break;
                        }

                    case "!play":
                        {
                            msg.Channel.SendMessageAsync("Nope!!!");
                            break;
                        }
                    case "!date":
                        {
                            msg.Channel.SendMessageAsync($"Сегодня : {DateTime.Now.ToLongDateString()}");
                            break;
                        }
                    case "!age":
                        {
                            msg.Channel.SendMessageAsync($@"Your acc created at {msg.Author.CreatedAt.DateTime.ToLongDateString}");
                            break;
                        }


                }
            string text = $"{DateTime.Now.ToLongTimeString()} {msg.Author} {msg.Content}";
            Console.WriteLine($"{text} ");
           
            return Task.CompletedTask;
        }

        private async Task SlashCommandHandler(SocketSlashCommand comand)
        {


            switch (comand.Data.Name)
            {
                case "start":
                    await comand.RespondAsync($"You executed { comand.Data.Name}");
                    break;
                case "exchange":
                    await comand.RespondAsync(@"https://www.cbr.ru/currency_base/daily/");
                    break;
              
            }
           
        }

        private async Task Client_Ready()
        {
            var globalCommand = new SlashCommandBuilder();
            globalCommand.WithName("start");
            globalCommand.WithDescription("start bot");

            globalCommand.WithName("exchange");
            globalCommand.WithDescription("Курсы валют");

         
            await _client.CreateGlobalApplicationCommandAsync(globalCommand.Build());

        }

      
       

    }

}