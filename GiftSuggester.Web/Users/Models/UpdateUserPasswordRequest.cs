namespace GiftSuggester.Web.Users.Models;

public class UpdateUserPasswordRequest
{
    public Guid UserId { get; set; }
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}