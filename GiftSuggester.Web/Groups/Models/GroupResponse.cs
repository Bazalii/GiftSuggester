using GiftSuggester.Web.Users.Models;

namespace GiftSuggester.Web.Groups.Models;

public class GroupResponse
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public List<UserResponse> Members { get; set; } = new();
}