using GiftSuggester.Core.Groups.Models;

namespace GiftSuggester.Core.Groups.Repositories;

public interface IGroupRepository
{
    Task<Group> AddAsync(Group group, CancellationToken cancellationToken);
    Task<Group> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddUserToGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken);
    Task RemoveUserFromGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken);
    Task PromoteToAdminAsync(Guid groupId, Guid userId, CancellationToken cancellationToken);
    Task RemoveAdminRightsAsync(Guid groupId, Guid userId, CancellationToken cancellationToken);
    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ContainsUserAsync(Guid groupId, Guid userId, CancellationToken cancellationToken);
    Task<bool> ContainsUserAsAdminAsync(Guid groupId, Guid userId, CancellationToken cancellationToken);
}