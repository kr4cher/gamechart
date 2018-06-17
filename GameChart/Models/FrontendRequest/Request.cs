namespace GameChart.Models.FrontendRequest
{
    public class Request
    {
        string Endpoint { get; set; } 
        string[] Fields { get; set; }
        Filter Filter { get; set; }
        Order Order { get; set; }
        string Search { get; set; }
    }
}