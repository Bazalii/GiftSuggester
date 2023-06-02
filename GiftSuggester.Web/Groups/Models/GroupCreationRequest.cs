namespace GiftSuggester.Web.Groups.Models;

public class GroupCreationRequest
{
    public string Name { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
}