using System.Text.Json.Serialization;

public record SkillSet
{
    [JsonPropertyName("label")]
    public string? label { get; set; }

    [JsonPropertyName("value")]
    public string? value { get; set; }
}
