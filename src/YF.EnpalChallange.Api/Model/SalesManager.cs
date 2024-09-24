using System.ComponentModel.DataAnnotations.Schema;
using YF.EnpalChallange.Api.Model.Contract;

namespace YF.EnpalChallange.Api.Model;

public class SalesManager
{
    public SalesManagerId Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string[] Languages { get; set; } = null!;

    public string[] Products { get; set; } = null!;

    public string[] CustomerRatings { get; set; } = null!;
}