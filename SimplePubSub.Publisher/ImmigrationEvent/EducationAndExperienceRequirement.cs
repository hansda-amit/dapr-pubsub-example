using System.Text.Json.Serialization;

public record EducationAndExperienceRequirement
{
    /*
        "minEducation": "other",
        "altEducation": "Some education diploma",
        "fieldOfStudy": "Software",
        "isLicenseRequired": "yes",
        "licenseDetails": "React",
        "minWorkExperienceM": "gt60",
        "businessReasons": "Some reason",
        "broadFieldOfExperience": "Software development"
    */
    [JsonPropertyName("minEducation")]
    public string minEducation { get; set; }

    [JsonPropertyName("altEducation")]
    public string altEducation { get; set; }

    [JsonPropertyName("fieldOfStudy")]
    public string fieldOfStudy { get; set; }

    [JsonPropertyName("isLicenseRequired")]
    public string isLicenseRequired { get; set; }

    [JsonPropertyName("licenseDetails")]
    public string licenseDetails { get; set; }

    [JsonPropertyName("minWorkExperienceM")]
    public string minWorkExperienceM { get; set; }

    [JsonPropertyName("businessReasons")]
    public string businessReasons { get; set; }

    [JsonPropertyName("broadFieldOfExperience")]
    public string broadFieldOfExperience { get; set; }

    [JsonPropertyName("altEducationAndExp")]
    public List<AltEducationAndExperience> altEducationAndExp { get; set; } = new();

    // Todo: Remove altWorkExperienceReqM and altEducationReq when alternative stories are good for testing.
    [JsonPropertyName("altWorkExperienceReqM")]
    public string altWorkExperienceReqM { get; set; }

    [JsonPropertyName("altEducationReq")]
    public string altEducationReq { get; set; }

}

public record AltEducationAndExperience
{
    public string altMinEducation { get; set; }
    public string altMinWorkExperienceM { get; set; }
    public string otherAltEducation { get; set; }
}


public record EmploymentPositionDetails
{
    public int Id { get; set; }

    public string Name { get; set; }

    public bool isArchived { get; set; }

    public PositionRequirements PositionRequirements { get; set; }
}