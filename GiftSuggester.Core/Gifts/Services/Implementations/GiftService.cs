using FluentValidation;
using GiftSuggester.Core.CommonClasses;
using GiftSuggester.Core.Gifts.Mappers;
using GiftSuggester.Core.Gifts.Models;
using GiftSuggester.Core.Gifts.Repositories;

namespace GiftSuggester.Core.Gifts.Services.Implementations;

public class GiftService : IGiftService
{
    private readonly IGiftRepository _giftRepository;
    private readonly IValidator<Gift> _giftValidator;
    private readonly GiftCoreModelsMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GiftService(
        IUnitOfWork unitOfWork,
        IGiftRepository giftRepository,
        IValidator<Gift> giftValidator,
        GiftCoreModelsMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _giftRepository = giftRepository;
        _giftValidator = giftValidator;
        _mapper = mapper;
    }

    public async Task<Gift> AddAsync(GiftCreationModel creationModel, CancellationToken cancellationToken)
    {
        var gift = _mapper.MapCreationModelToGift(creationModel);

        await _giftValidator.ValidateAndThrowAsync(gift, cancellationToken);

        var addedGift = await _giftRepository.AddAsync(gift, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return addedGift;
    }

    public Task<Gift> GetByPresenterRecipientAndGroupAsync(Guid presenterId, Guid recipientId, Guid groupId,
                                                           CancellationToken cancellationToken)
    {
        return _giftRepository.GetByPresenterRecipientAndGroupAsync(
            groupId,
            presenterId,
            recipientId,
            cancellationToken);
    }

    public Task<List<Gift>> GetAllByPresenterIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _giftRepository.GetAllByPresenterIdAsync(id, cancellationToken);
    }

    public Task<List<Gift>> GetAllByRecipientIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _giftRepository.GetAllByRecipientIdAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(Gift gift, CancellationToken cancellationToken)
    {
        await _giftValidator.ValidateAndThrowAsync(gift, cancellationToken);

        await _giftRepository.UpdateAsync(gift, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _giftRepository.RemoveByIdAsync(id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}