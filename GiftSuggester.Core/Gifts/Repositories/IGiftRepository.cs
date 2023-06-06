using GiftSuggester.Core.Gifts.Models;

namespace GiftSuggester.Core.Gifts.Repositories;

public interface IGiftRepository
{
    Task AddAsync(Gift gift, CancellationToken cancellationToken);

    Task<Gift> GetByPresenterRecipientAndGroupAsync(Guid groupId, Guid presenterId, Guid recipientId,
                                                    CancellationToken cancellationToken);

    Task<List<Gift>> GetAllByPresenterIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Gift>> GetAllByRecipientIdAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> ExistsWithNameForRecipientInGroupAsync(string name, Guid groupId, Guid recipientId,
                                                      CancellationToken cancellationToken);

    Task UpdateAsync(Gift gift, CancellationToken cancellationToken);
    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
}