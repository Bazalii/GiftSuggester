﻿using GiftSuggester.Core.Groups.Models;

namespace GiftSuggester.Core.Groups.Services;

public interface IGroupService
{
    Task AddAsync(GroupCreationModel creationModel, CancellationToken cancellationToken);
    Task AddUserToGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken);
    Task RemoveUserFromGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken);
    Task<Group> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
}