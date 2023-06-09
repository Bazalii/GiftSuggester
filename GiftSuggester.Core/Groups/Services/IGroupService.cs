using GiftSuggester.Core.Groups.Models;

namespace GiftSuggester.Core.Groups.Services;

public interface IGroupService
{
    Task<Group> AddAsync(GroupCreationModel creationModel, CancellationToken cancellationToken);
    Task<Group> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddUserToGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken);
    Task RemoveUserFromGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken);
    Task PromoteToAdminAsync(Guid groupId, Guid userId, CancellationToken cancellationToken);
    Task RemoveAdminRightsAsync(Guid groupId, Guid userId, CancellationToken cancellationToken);
    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
}