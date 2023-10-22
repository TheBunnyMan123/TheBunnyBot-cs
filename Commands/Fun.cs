using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;

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
}