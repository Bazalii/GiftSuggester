using GiftSuggester.Core.Users.Models;

namespace GiftSuggester.Core.Users.Services;

public interface IUserService
{
    Task AddAsync(UserCreationModel creationModel, CancellationToken cancellationToken);
    Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
}