using System.Text.Json.Serialization;

public record JobDetailsRequirement
{
    /*
        "jobTitle": "Software Analyst 2",
        "jobBand": "L2",
        "businessUnit": "IT",
        "jobDescription": "<p>Desc</p>",
        "isSupervisorRole": "yes",
        "supervisionLevel": "peer",
        "isTelecommuteRole": "yes",
        "percentTelecommute": 100,
        "isTravelRole": "yes",
        "percentTravel": 100,
        "travelType": "both"
    */
    [JsonPropertyName("jobTitle")]
    public string jobTitle { get; set; }

    [JsonPropertyName("jobBand")]
    public string jobBand { get; set; }

    [JsonPropertyName("businessUnit")]
    public string businessUnit { get; set; }

    [JsonPropertyName("jobDescription")]
    public string jobDescription { get; set; }

    [JsonPropertyName("isSupervisorRole")]
    public string isSupervisorRole { get; set; }

    [JsonPropertyName("supervisionLevel")]
    public string supervisionLevel { get; set; }

    [JsonPropertyName("isTelecommuteRole")]
    public string isTelecommuteRole { get; set; }

    [JsonPropertyName("percentTelecommute")]
    public int percentTelecommute { get; set; }

    [JsonPropertyName("isTravelRole")]
    public string isTravelRole { get; set; }

    [JsonPropertyName("percentTravel")]
    public int percentTravel { get; set; }

    [JsonPropertyName("travelType")]
    public string? travelType { get; set; }
}
