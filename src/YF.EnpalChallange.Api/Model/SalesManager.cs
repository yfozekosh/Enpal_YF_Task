namespace YF.EnpalChallange.Api.Model;

public class SalesManager
{
    public SalesManagerId Id { get; set; }

    public string Name { get; set; }

    public string[] Languages { get; set; }

    public string[] Products { get; set; }

    public string[] CustomerRatings { get; set; }
}