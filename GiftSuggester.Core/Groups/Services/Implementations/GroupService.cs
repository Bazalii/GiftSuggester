using FluentValidation;
using GiftSuggester.Core.CommonClasses;
using GiftSuggester.Core.Exceptions;
using GiftSuggester.Core.Groups.Models;
using GiftSuggester.Core.Groups.Repositories;
using GiftSuggester.Core.Users.Repositories;

namespace GiftSuggester.Core.Groups.Services.Implementations;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly IValidator<Group> _groupValidator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public GroupService(
        IUnitOfWork unitOfWork,
        IGroupRepository groupRepository,
        IUserRepository userRepository,
        IValidator<Group> groupValidator)
    {
        _unitOfWork = unitOfWork;
        _groupRepository = groupRepository;
        _userRepository = userRepository;
        _groupValidator = groupValidator;
    }

    public async Task AddAsync(GroupCreationModel creationModel, CancellationToken cancellationToken)
    {
        var owner = await _userRepository.GetByIdAsync(creationModel.OwnerId, cancellationToken);

        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = creationModel.Name,
            Owner = owner
        };

        await _groupValidator.ValidateAndThrowAsync(group, cancellationToken);

        await _groupRepository.AddAsync(group, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public Task<Group> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _groupRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task AddUserToGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken)
    {
        if (await _groupRepository.ContainsUserAsync(groupId, userId, cancellationToken))
            throw new AlreadyContainsException($"User with id: {userId} is already in group with id: {groupId}");

        await _groupRepository.AddUserToGroupAsync(groupId, userId, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveUserFromGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken)
    {
        await _groupRepository.RemoveUserFromGroupAsync(groupId, userId, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task PromoteToAdminAsync(Guid groupId, Guid userId, CancellationToken cancellationToken)
    {
        if (await _groupRepository.ContainsUserAsAdminAsync(groupId, userId, cancellationToken))
            throw new AlreadyContainsException(
                $"User with id: {userId} is already admin of the group with id: {groupId}");

        await _groupRepository.PromoteToAdminAsync(groupId, userId, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAdminRightsAsync(Guid groupId, Guid userId, CancellationToken cancellationToken)
    {
        await _groupRepository.RemoveAdminRightsAsync(groupId, userId, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _groupRepository.RemoveByIdAsync(id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}