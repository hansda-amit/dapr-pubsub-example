using System.Text.Json.Serialization;

public record JobSkillsRequirement
{
    [JsonPropertyName("skills")]
    public List<JobSkills>? skills { get; set; }
    [JsonPropertyName("skillsGroup")]
    public List<SkillsGroup>? skillsGroup { get; set; }
}
