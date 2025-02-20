public record EmploymentPositionEvent
{
    public string eventType { get; set; }

    public string recordType { get; set; }

    public int recordId { get; set; }

    public string name { get; set; }

    public DateTime eventTimestamp { get; set; }

    public EmploymentPositionDetails employmentPositionDetails { get; set; }

    public Dictionary<string, object> additionalProperties { get; set; }

}