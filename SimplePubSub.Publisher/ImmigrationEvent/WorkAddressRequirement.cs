using System.Text.Json.Serialization;

public record WorkAddressRequirement
{
    /*
        "employerAddress1": "Address1",
        "employerAddress2": "address 2",
        "employerCountry": "IN",
        "employerState": "WB",
        "employerCity": "Kolkata",
        "employerZip": "700061"
    */
    [JsonPropertyName("employerEntity")]
    public string employerEntity { get; set; }

    [JsonPropertyName("employerAddress1")]
    public string employerAddress1 { get; set; }

    [JsonPropertyName("employerAddress2")]
    public string employerAddress2 { get; set; }

    [JsonPropertyName("employerCountry")]
    public string employerCountry { get; set; }

    [JsonPropertyName("employerState")]
    public string employerState { get; set; }

    [JsonPropertyName("employerCity")]
    public string employerCity { get; set; }

    [JsonPropertyName("employerZip")]
    public string employerZip { get; set; }
}