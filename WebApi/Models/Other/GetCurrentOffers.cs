namespace WebApi.Models.Other;

public class GetCurrentOffers
{
    public string BookTitle { get; set; } = string.Empty;
    public int BookId { get; set; }
    public int CustomerID { get; set; }
    public string CustomerName { get; set; } = string.Empty;
}
