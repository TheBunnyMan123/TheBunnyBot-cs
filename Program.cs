using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace TheBunnyBot {
    class Program {
        static string prefix = "bb!";
        public static string catapitoken;
        public static string dogapitoken;

        static async Task Main(string[] args) {

            if (args.Length < 2) {
                Console.WriteLine("First argument must be your bot's token");
                Console.WriteLine("Second argument must be your key for thecatapi.com");
                // Console.WriteLine("Third argument must be your key for thedogapi.com");
                return;
            }
            Console.WriteLine("Logging in with token (sha256 hashed): " + sha256hash(args[0]));
            Console.WriteLine("---");
            
            var discord = new DiscordClient(new DiscordConfiguration() {
                Token = args[0],
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents,
                MinimumLogLevel = LogLevel.Debug,
                LogTimestampFormat = "MMM dd yyyy - hh:mm:ss tt"
            });
            var commands = discord.UseCommandsNext(new CommandsNextConfiguration() {
                StringPrefixes = new[] { "!", "bb!" }
            });

            commands.RegisterCommands(Assembly.GetExecutingAssembly());

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

        public static string sha256hash(string toHash) {
            HashAlgorithm algorithm = SHA256.Create();
            var sha256bytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(toHash));
            var sha256string1 = BitConverter.ToString(sha256bytes);
            var sha256string1split = sha256string1.Split("-");
            var sha256string2 = "";
            for (int i = 0; i < sha256string1split.Length; i++) {
                sha256string2 = sha256string2 + sha256string1split[i];
            }
            return sha256string2;
        }
    }
}