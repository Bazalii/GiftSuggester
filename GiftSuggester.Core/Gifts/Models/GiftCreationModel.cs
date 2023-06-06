namespace GiftSuggester.Core.Gifts.Models;

public class GiftCreationModel
{
    public string Name { get; set; } = string.Empty;
    public Guid GroupId { get; set; }
    public Guid CreatorId { get; set; }
    public Guid PresenterId { get; set; }
    public Guid RecipientId { get; set; }
}