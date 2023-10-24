using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;

public class UtilityCommands : BaseCommandModule {
    [Command("ping")]
    [Description("Replies with \"Pong!\"")]
    public async Task PingCommand(CommandContext ctx) {
        await ctx.RespondAsync("Pong!");
    }

    [Command("random")]
    [Description("Random Number between X and Y")]
    public async Task RandomCommand(CommandContext ctx, 
                                    [Description("Minimum Number")] int min, 
                                    [Description("Maximum Number")] int max) {
        var random = new Random();
        await ctx.RespondAsync($"Your number is: {random.Next(min, max)}");
    }
    [Command("random")]
    [Description("Random Number between X and Y")]
    public async Task RandomCommand(CommandContext ctx) {
        await ctx.RespondAsync("Arguments required, please do `bb!help random`");
    }

    [Command("poll")]
    [Description("Create a poll")]
    public async Task PollCommand(CommandContext ctx,  
                                  [Description("Poll text")] [RemainingText] String msg) {
        var interactivity = ctx.Client.GetInteractivity();
        DiscordEmoji[] emojiOptions = new DiscordEmoji[] { DiscordEmoji.FromName(ctx.Client, ":white_check_mark:", false), DiscordEmoji.FromName(ctx.Client, ":x:", false) };
        var options = emojiOptions.Select(x => x.ToString());

        #pragma warning disable CS8602
        var pollEmbed = new DiscordEmbedBuilder {
            Title = "Poll by " + ctx.Member.Username,
            Description = msg
        };

        var pollMessage = await ctx.Channel.SendMessageAsync(embed: pollEmbed).ConfigureAwait(false);

        foreach(var option in emojiOptions) {
            await pollMessage.CreateReactionAsync(option);
        }
    }

    [Command("poll")]
    [Description("Create a poll")]
    public async Task PollCommand(CommandContext ctx) {
        await ctx.RespondAsync("Arguments required, please do `bb!help poll`");
    }
}