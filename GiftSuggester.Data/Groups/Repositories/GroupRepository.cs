using GiftSuggester.Core.Exceptions;
using GiftSuggester.Core.Groups.Models;
using GiftSuggester.Core.Groups.Repositories;
using GiftSuggester.Core.Users.Models;
using GiftSuggester.Data.Groups.Mappers;
using GiftSuggester.Data.Groups.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftSuggester.Data.Groups.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly GiftSuggesterContext _context;
    private readonly GroupDbModelsMapper _mapper;

    public GroupRepository(GiftSuggesterContext context, GroupDbModelsMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task AddAsync(Group group, CancellationToken cancellationToken)
    {
        return _context.Groups
            .AddAsync(_mapper.MapGroupToDbModel(group), cancellationToken)
            .AsTask();
    }

    public async Task AddUserToGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken)
    {
        var userDbModel =
            await _context.Users.FirstOrDefaultAsync(dbModel => dbModel.Id == userId, cancellationToken) ??
            throw new EntityNotFoundException($"User with id: {userId} is not found!");

        var groupDbModel =
            await _context.Groups.FirstOrDefaultAsync(dbModel => dbModel.Id == groupId, cancellationToken) ??
            throw new EntityNotFoundException($"Group with id: {groupId} is not found!");

        groupDbModel.Members.Add(userDbModel);
    }

    public async Task RemoveUserFromGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken)
    {
        var groupDbModel =
            await _context.Groups.FirstOrDefaultAsync(dbModel => dbModel.Id == groupId, cancellationToken) ??
            throw new EntityNotFoundException($"Group with id: {groupId} is not found!");

        var userDbModel =
            groupDbModel.Members.FirstOrDefault(dbModel => dbModel.Id == userId) ??
            throw new EntityNotFoundException($"User with id: {userId} is not in the group with id: {groupId}!");

        groupDbModel.Members.Remove(userDbModel);
    }

    public async Task<Group> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dbModel = await _context.Groups.FirstOrDefaultAsync(dbModel => dbModel.Id == id, cancellationToken) ??
                      throw new EntityNotFoundException($"Group with id: {id} is not found!");

        return _mapper.MapDbModelToGroup(dbModel);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        var dbModel = await _context.Groups.FirstOrDefaultAsync(model => model.Id == id, cancellationToken);

        return dbModel is not null;
    }

    public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dbModel = await _context.Groups.FirstOrDefaultAsync(dbModel => dbModel.Id == id, cancellationToken) ??
                      throw new EntityNotFoundException($"Group with id: {id} is not found!");

        _context.Groups.Remove(dbModel);
    }
}