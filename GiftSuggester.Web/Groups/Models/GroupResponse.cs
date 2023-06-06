using GiftSuggester.Web.Users.Models;

namespace GiftSuggester.Web.Groups.Models;

public class GroupResponse
{
    public Guid Id { get; set; }
    public UserResponse Owner { get; set; } = new();
    public List<UserResponse> Admins { get; set; } = new();
    public List<UserResponse> Members { get; set; } = new();
}