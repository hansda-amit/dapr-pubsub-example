internal record OrderPayload
{
    public OrderPayload()
    {

    }
    public OrderPayload(string orderId, decimal totalCost)
    {
        OrderId = orderId;
        TotalCost = totalCost;
    }
    public string OrderId { get; set; }
    public decimal TotalCost { get; set; }

}

