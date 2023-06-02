using GiftSuggester.Core.Exceptions;
using GiftSuggester.Core.Gifts.Models;
using GiftSuggester.Core.Gifts.Repositories;
using GiftSuggester.Data.Gifts.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftSuggester.Data.Gifts.Repositories;

public class GiftRepository : IGiftRepository
{
    private readonly GiftSuggesterContext _context;

    public GiftRepository(GiftSuggesterContext context)
    {
        _context = context;
    }

    public Task AddAsync(Gift gift, CancellationToken cancellationToken)
    {
        return _context.Gifts
            .AddAsync(
                new GiftDbModel
                {
                    Id = gift.Id,
                    Name = gift.Name,
                    GroupId = gift.GroupId,
                    PresenterId = gift.PresenterId,
                    RecipientId = gift.RecipientId
                }, cancellationToken)
            .AsTask();
    }

    public Task<List<Gift>> GetAllByPresenterIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Gifts
            .Where(gift => gift.PresenterId == id)
            .Select(
                dbModel => new Gift
                {
                    Id = dbModel.Id,
                    Name = dbModel.Name,
                    GroupId = dbModel.GroupId,
                    PresenterId = dbModel.PresenterId,
                    RecipientId = dbModel.RecipientId
                })
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public Task<List<Gift>> GetAllByRecipientIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Gifts
            .Where(gift => gift.RecipientId == id)
            .Select(
                dbModel => new Gift
                {
                    Id = dbModel.Id,
                    Name = dbModel.Name,
                    GroupId = dbModel.GroupId,
                    PresenterId = dbModel.PresenterId,
                    RecipientId = dbModel.RecipientId
                })
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsWithNameForRecipientAsync(string name, Guid recipientId,
                                                            CancellationToken cancellationToken)
    {
        var dbModel = await _context.Gifts.FirstOrDefaultAsync(
            model => model.Name == name && model.RecipientId == recipientId, cancellationToken);

        return dbModel is not null;
    }

    public async Task UpdateAsync(Gift gift, CancellationToken cancellationToken)
    {
        var dbModel = await _context.Gifts.FirstOrDefaultAsync(dbModel => dbModel.Id == gift.Id, cancellationToken) ??
                      throw new EntityNotFoundException($"Gift with id: {gift.Id} is not found!");

        dbModel.Name = gift.Name;
        dbModel.GroupId = gift.GroupId;
        dbModel.PresenterId = gift.PresenterId;
        dbModel.RecipientId = gift.RecipientId;
    }

    public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dbModel = await _context.Gifts.FirstOrDefaultAsync(dbModel => dbModel.Id == id, cancellationToken) ??
                      throw new EntityNotFoundException($"Gift with id: {id} is not found!");

        _context.Gifts.Remove(dbModel);
    }
}