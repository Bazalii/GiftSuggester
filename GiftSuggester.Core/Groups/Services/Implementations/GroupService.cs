using FluentValidation;
using GiftSuggester.Core.CommonClasses;
using GiftSuggester.Core.Groups.Models;
using GiftSuggester.Core.Groups.Repositories;
using GiftSuggester.Core.Users.Repositories;

namespace GiftSuggester.Core.Groups.Services.Implementations;

public class GroupService : IGroupService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<Group> _groupValidator;

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
        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = creationModel.Name,
            OwnerId = creationModel.OwnerId
        };

        await _groupValidator.ValidateAndThrowAsync(group, cancellationToken);

        var owner = await _userRepository.GetByIdAsync(creationModel.OwnerId, cancellationToken);
        group.Members.Add(owner);

        await _groupRepository.AddAsync(group, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task AddUserToGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken)
    {
        await _groupRepository.AddUserToGroupAsync(groupId, userId, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public Task<Group> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _groupRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _groupRepository.RemoveByIdAsync(id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}