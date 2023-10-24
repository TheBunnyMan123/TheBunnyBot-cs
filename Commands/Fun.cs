using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using CatApiWrapper.Responses;
using System.Net.Http;

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

    // [Command("cat")]
    // [Description("Get a cat picture from thecatapi.com")]
    // public async Task CatCommand(CommandContext ctx) {
    //     // Act
    //     var response = CatClient.ReadResult<GetResponse.Response>(httpResponseMessage);
    //     var images = response.Data.Images;

    //     var sourceUrls = new[]
    //     {
    //         "http://thecatapi.com/?id=78b"
    //     };

    //     Assert.AreEqual(2, images.Length);
    //     await ctx.RespondAsync(sourceUrls[0]);
    // }
}