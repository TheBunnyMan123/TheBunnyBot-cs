using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Net.Models;
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

    [Command("clone")]
    [Description("Clone current channel")]
    public async Task CloneCommand(CommandContext ctx) {
        var channel = ctx.Channel;
        await channel.CloneAsync(ctx.Member + " ran bb!clone");
        await ctx.RespondAsync("Done!");
    }

    [Command("rename")]
    [Description("Rename current channel")]
    public async Task RenameCommand(CommandContext ctx,
                                    [Description("New name")] params String[] name) {
        string renameTo = String.Join("-", name);
        var channel = ctx.Channel;
        Action<ChannelEditModel> action = new(x => x.Name = renameTo);
        await channel.ModifyAsync(action);
        await ctx.RespondAsync("Done! Renamed to " + renameTo);
    }
    [Command("rename")]
    [Description("Rename current channel")]
    public async Task RenameCommand(CommandContext ctx) {
        await ctx.RespondAsync("Arguments required, please do `bb!help rename`");
    }

    [Command("timeout")]
    [Description("Time someone out")]
    public async Task TimeoutCommand(CommandContext ctx, [Description("User to time out")] DiscordMember user) {
        await ctx.Member.TimeoutAsync(DateTime.Now + TimeSpan.FromSeconds((60*5)), "imagine trying to time someone out");
        await ctx.RespondAsync("@everyone " + ctx.Member.Mention  + " tried to time " + user.Mention + " out like an idiot");
    }
    [Command("timeout")]
    [Description("Time someone out")]
    public async Task TimeoutCommand(CommandContext ctx) {
        await ctx.RespondAsync("arguments required. try `bb!help timeout`");
    }
    [Command("timeout")]
    [Description("Time someone out")]
    public async Task TimeoutCommand(CommandContext ctx, [Description("User ID")] string[] UserID) {
        if (String.Join(" ", UserID) == "random") {
            await ctx.Member.TimeoutAsync(DateTime.Now + TimeSpan.FromSeconds((60*60*24)), "timeout roulette");
        }else {
            await ctx.RespondAsync("Nah fam");
        }
    }
}