namespace GiftSuggester.Core.Gifts.Models;

public class Gift
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid GroupId { get; set; }
    public Guid PresenterId { get; set; }
    public Guid RecipientId { get; set; }
}