using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

public class FunCommands : BaseCommandModule {
    [Command("greet")]
    [Description("Greet a user")]
    public async Task GreetCommand(CommandContext ctx, 
                                   [Description("User to ping")] DiscordMember user) {
        await ctx.RespondAsync("Hi there, " + user.Mention + ", How are you doing today?");
    }
    [Command("greet")]
    public async Task GreetCommand(CommandContext ctx, 
                                   [Description("Name to greet")] [RemainingText] String name) {
        await ctx.RespondAsync("Hi there, " + name + ", How are you doing today? (Courtesy of " + ctx.Member.Mention + ")");
    }
    [Command("greet")]
    [Description("Greet a user")]
    public async Task PingCommand(CommandContext ctx) {
        await ctx.RespondAsync("Arguments required, please do `bb!help greet`");
    }

    [Command("roll")]
    [Description("Rick Roll Everyone")]
    public async Task RollCommand(CommandContext ctx) {
        var channels = ctx.Guild.Channels;
        // Console.WriteLine(channels[0].Type);
        // Console.WriteLine(1);
        // await ctx.Channel.SendMessageAsync("Rick Roll, Courtesy of " + ctx.Member.Mention + ": ( https://www.youtube.com/watch?v=dQw4w9WgXcQ )").ConfigureAwait(false);
        // Console.WriteLine(1);
        foreach (var channel in channels) {
            if (channel.Value.Type.ToString() == "Category") {} else {
                await channel.Value.SendMessageAsync("Rick Roll, Courtesy of " + ctx.Member.Mention + ": ( https://www.youtube.com/watch?v=dQw4w9WgXcQ )").ConfigureAwait(false);
            }
        }
    }

    [Command("cat")]
    [Description("Get a cat picture from thecatapi.com")]
    public async Task CatCommand(CommandContext ctx) {
        using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/TheBunnyBot"));
            client.DefaultRequestHeaders.Add("User-Agent", "TheBunnyBot");
            client.DefaultRequestHeaders.Add("x-api-key", TheBunnyBot.Program.catapitoken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        await ctx.RespondAsync(TheBunnyBot.Program.ImageApiRequest(client, "https://api.thecatapi.com/v1/images/search?api_key="+TheBunnyBot.Program.catapitoken).Result);
    }

    [Command("dog")]
    [Description("Get a cat picture from thedogapi.com")]
    public async Task DogCommand(CommandContext ctx) {
        using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/TheBunnyBot"));
            client.DefaultRequestHeaders.Add("User-Agent", "TheBunnyBot");
            client.DefaultRequestHeaders.Add("x-api-key", TheBunnyBot.Program.dogapitoken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        await ctx.RespondAsync(TheBunnyBot.Program.ImageApiRequest(client, "https://api.thedogapi.com/v1/images/search?api_key="+TheBunnyBot.Program.dogapitoken).Result);
    }
}