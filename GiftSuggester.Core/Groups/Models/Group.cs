using GiftSuggester.Core.Users.Models;

namespace GiftSuggester.Core.Groups.Models;

public class Group
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public User Owner { get; set; } = new();
    public List<User> Admins { get; set; } = new();
    public List<User> Members { get; set; } = new();
}