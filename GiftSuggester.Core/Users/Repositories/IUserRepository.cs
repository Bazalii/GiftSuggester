using GiftSuggester.Core.Users.Models;

namespace GiftSuggester.Core.Users.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User> GetByLoginAsync(string login, CancellationToken cancellationToken);
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistsWithLoginAsync(string login, CancellationToken cancellationToken);
    Task<bool> ExistsWithEmailAsync(string email, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
}