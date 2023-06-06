using GiftSuggester.Core.Exceptions;
using GiftSuggester.Core.Gifts.Models;
using GiftSuggester.Core.Gifts.Repositories;
using GiftSuggester.Data.Gifts.Mappers;
using Microsoft.EntityFrameworkCore;

namespace GiftSuggester.Data.Gifts.Repositories;

public class GiftRepository : IGiftRepository
{
    private readonly GiftSuggesterContext _context;
    private readonly GiftDbModelsMapper _mapper;

    public GiftRepository(GiftSuggesterContext context, GiftDbModelsMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task AddAsync(Gift gift, CancellationToken cancellationToken)
    {
        return _context.Gifts
            .AddAsync(_mapper.MapGiftToDbModel(gift), cancellationToken)
            .AsTask();
    }

    public async Task<Gift> GetByPresenterRecipientAndGroupAsync(Guid groupId, Guid presenterId, Guid recipientId,
                                                                 CancellationToken cancellationToken)
    {
        var dbModel = await _context.Gifts
                          .FirstOrDefaultAsync(
                              dbModel => dbModel.GroupId == groupId &&
                                         dbModel.PresenterId == presenterId &&
                                         dbModel.RecipientId == recipientId,
                              cancellationToken: cancellationToken) ??
                      throw new EntityNotFoundException("Gift is not found!");

        return _mapper.MapDbModelToGift(dbModel);
    }

    public Task<List<Gift>> GetAllByPresenterIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Gifts
            .Where(gift => gift.PresenterId == id)
            .Select(dbModel => _mapper.MapDbModelToGift(dbModel))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public Task<List<Gift>> GetAllByRecipientIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Gifts
            .Where(gift => gift.RecipientId == id)
            .Select(dbModel => _mapper.MapDbModelToGift(dbModel))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsWithNameForRecipientInGroupAsync(string name, Guid groupId, Guid recipientId,
                                                                   CancellationToken cancellationToken)
    {
        var dbModel = await _context.Gifts.FirstOrDefaultAsync(
            model => model.Name == name && model.GroupId == groupId && model.RecipientId == recipientId,
            cancellationToken);

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