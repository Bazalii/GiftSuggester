using FluentValidation;
using GiftSuggester.Core.Gifts.Models;
using GiftSuggester.Core.Gifts.Repositories;
using GiftSuggester.Core.Groups.Repositories;
using GiftSuggester.Core.Users.Repositories;

namespace GiftSuggester.Core.Gifts.Validators;

public class GiftValidator : AbstractValidator<Gift>
{
    public GiftValidator(
        IUserRepository userRepository,
        IGroupRepository groupRepository,
        IGiftRepository giftRepository)
    {
        RuleFor(x => x.Name)
            .Length(1, 100)
            .WithMessage("{PropertyName} length has to be from {MinLength} characters to {MaxLength} characters!");
        RuleFor(x => x.GroupId)
            .MustAsync(async (groupId, cancellationToken) =>
                           await groupRepository.ExistsAsync(groupId, cancellationToken))
            .WithMessage("Group with this id: {PropertyValue} doesn't exist!");
        RuleFor(x => x.CreatorId)
            .MustAsync(async (creatorId, cancellationToken) =>
                           await userRepository.ExistsWithIdAsync(creatorId, cancellationToken))
            .WithMessage("Creator with this id: {PropertyValue} doesn't exist!");
        RuleFor(x => x.PresenterId)
            .MustAsync(async (presenterId, cancellationToken) =>
                           presenterId == Guid.Empty ||
                           await userRepository.ExistsWithIdAsync(presenterId, cancellationToken))
            .WithMessage("Presenter with this id: {PropertyValue} doesn't exist!");
        RuleFor(x => x.RecipientId)
            .MustAsync(async (recipientId, cancellationToken) =>
                           await userRepository.ExistsWithIdAsync(recipientId, cancellationToken))
            .WithMessage("recipient with this id: {PropertyValue} doesn't exist!");
        RuleFor(x => new { x.PresenterId, x.RecipientId })
            .MustAsync((validationObject, cancellationToken) =>
                           Task.FromResult(validationObject.PresenterId != validationObject.RecipientId))
            .WithMessage("Presenter and recipient cannot be the same person!");
        RuleFor(x => new { x.Name, x.GroupId, x.RecipientId })
            .MustAsync(
                async (validationObject, cancellationToken) =>
                    !await giftRepository.ExistsWithNameForRecipientInGroupAsync(
                        validationObject.Name,
                        validationObject.GroupId,
                        validationObject.RecipientId,
                        cancellationToken))
            .WithMessage(x => $"Gift with {x.Name} name is already in list for person with {x.RecipientId} id!");
    }
}