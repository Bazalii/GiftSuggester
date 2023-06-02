namespace GiftSuggester.Core.Groups.Models;

public class GroupCreationModel
{
    public string Name { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
}