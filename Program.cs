﻿using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace TheBunnyBot {
    class Program {
        static string prefix = "bb!";
        // public static string catapitoken;
        // public static string dogapitoken;

        static async Task Main(string[] args) {

            if (args.Length < 1) {
                Console.WriteLine("First argument must be your bot's token");
                // Console.WriteLine("Second argument must be your key for thecatapi.com");
                // Console.WriteLine("Third argument must be your key for thedogapi.com");
                return;
            }
            Console.WriteLine("Logging in with token (sha256 hashed): " + sha256hash(args[0]));
            Console.WriteLine("Type !s to stop the bot \n---");
            
            var discord = new DiscordClient(new DiscordConfiguration() {
                Token = args[0],
                TokenType = TokenType.Bot,
                // Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents,
                Intents = DiscordIntents.All,
                MinimumLogLevel = LogLevel.Debug,
                LogTimestampFormat = "MMM dd yyyy - hh:mm:ss tt"
            });
            var commands = discord.UseCommandsNext(new CommandsNextConfiguration() {
                StringPrefixes = new[] { "!", "bb!" }
            });

            commands.RegisterCommands(Assembly.GetExecutingAssembly());

            await discord.ConnectAsync();

            var activity = new DiscordActivity("bb!help", ActivityType.Streaming);
            activity.StreamUrl = "https://twitch.tv/thebunnyman12/";

            discord.Ready += async (client, readyEventArgs) => 
                await discord.UpdateStatusAsync(activity, UserStatus.Online, null);

            while (true) {
                if (Console.ReadLine() == "!s") {
                    await discord.UpdateStatusAsync(new DiscordActivity("Bot stopping, no commands available", ActivityType.Playing), UserStatus.DoNotDisturb, null); 
                    return;
                }
            }
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