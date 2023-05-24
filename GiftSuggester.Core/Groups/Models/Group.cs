using GiftSuggester.Core.Users.Models;

namespace GiftSuggester.Core.Groups.Models;

public class Group
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public List<User> Members { get; set; } = new();
}