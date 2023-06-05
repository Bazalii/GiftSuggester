using GiftSuggester.Core.Users.Models;

namespace GiftSuggester.Core.Users.Services;

public interface IUserService
{
    Task AddAsync(UserCreationModel creationModel, CancellationToken cancellationToken);
    Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User> GetByLoginAsync(string login, CancellationToken cancellationToken);
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
    Task UpdatePasswordAsync(Guid id, string oldPassword, string newPassword, CancellationToken cancellationToken);
    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
}