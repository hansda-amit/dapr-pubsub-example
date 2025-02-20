using System.Text.Json.Serialization;

public record SkillsGroup
{
    [JsonPropertyName("skills")]
    public List<SkillSet>? skills { get; set; }
    [JsonPropertyName("minSkillsReqdInGroup")]
    public string? minSkillsReqdInGroup { get; set; }
    [JsonPropertyName("skillsMinExpGroupM")]
    public string? skillsMinExpGroupM { get; set; }
    [JsonPropertyName("skillsGroupGate")]
    public string? skillsGroupGate { get; set; }
}