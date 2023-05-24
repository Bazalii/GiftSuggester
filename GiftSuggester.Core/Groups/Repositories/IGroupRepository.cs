using GiftSuggester.Core.Groups.Models;

namespace GiftSuggester.Core.Groups.Repositories;

public interface IGroupRepository
{
    Task AddAsync(Group group, CancellationToken cancellationToken);
    Task AddUserToGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken);
    Task<Group> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
}