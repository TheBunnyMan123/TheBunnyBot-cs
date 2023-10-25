using System;
using System.Text.Json.Serialization;

public sealed record class ApiVars(
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("nsfw")] bool Nsfw)
    // [property: JsonPropertyName("html_url")] Uri GitHubHomeUrl,
    // [property: JsonPropertyName("homepage")] Uri Homepage,
    // [property: JsonPropertyName("watchers")] int Watchers,
    // [property: JsonPropertyName("pushed_at")] DateTime LastPushUtc)
{
    // public DateTime LastPush => LastPushUtc.ToLocalTime();
}