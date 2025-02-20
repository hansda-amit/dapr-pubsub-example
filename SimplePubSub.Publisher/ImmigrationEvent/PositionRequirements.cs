using System.Text.Json.Serialization;
using Newtonsoft.Json;

public record PositionRequirements
{
    //two attributes are used. one for newtonsoft which is used by UI application and another for dapr to serialize
    [property: JsonProperty("jobDetails")]
    [JsonPropertyName("jobDetailsRequirements")]
    public JobDetailsRequirement jobDetailsRequirements { get; set; }

    [JsonPropertyName("educationAndExperienceRequirements")]
    public EducationAndExperienceRequirement educationAndExperienceRequirements { get; set; }

    [JsonPropertyName("jobSkillsRequirements")]
    public JobSkillsRequirement jobSkillsRequirements { get; set; }

    [JsonPropertyName("workAddress")]
    public WorkAddressRequirement workAddress { get; set; }
}