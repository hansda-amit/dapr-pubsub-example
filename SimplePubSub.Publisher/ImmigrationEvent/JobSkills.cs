using System.Text.Json.Serialization;

public record JobSkills
{
    [JsonPropertyName("skill")]
    public SkillSet? skill { get; set; }

    [JsonPropertyName("skillsMinExpM")]
    public string? skillsMinExpM { get; set; }
}
