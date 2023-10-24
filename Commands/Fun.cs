using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using CatApiWrapper.Responses;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using TheBunnyBot.Program;

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
        await ctx.RespondAsync("Hi there, " + name + ", How are you doing today?");
    }
    [Command("greet")]
    [Description("Greet a user")]
    public async Task PingCommand(CommandContext ctx) {
        await ctx.RespondAsync("Arguments required, please do `bb!help greet`");
    }

    [Command("roll")]
    [Description("Rick Roll Everyone")]
    public async Task PingCommand(CommandContext ctx) {
        foreach (var channel in await ctx.Guild.GetChannelsAsync()) {
            channel.SendMessageAsync("Rick Roll, Courtesy of " + Ctx.Member.Mention + ": ( https://www.youtube.com/watch?v=dQw4w9WgXcQ )");
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

        var response = await TheBunnyBot.Program.ApiRequest(client, "https://api.thecatapi.com/v1/images/search?api_key="+TheBunnyBot.Program.catapitoken);
        await ctx.RespondAsync(response);

    }
}