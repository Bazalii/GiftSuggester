using System.Text.RegularExpressions;
using FluentValidation;
using GiftSuggester.Core.Users.Repositories;
using Group = GiftSuggester.Core.Groups.Models.Group;

namespace GiftSuggester.Core.Groups.Validators;

public class GroupValidator : AbstractValidator<Group>
{
    public GroupValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Name)
            .Length(3, 100)
            .WithMessage("{PropertyName} length has to be from {MinLength} characters to {MaxLength} characters!")
            .Matches(new Regex("^[а-яА-Яa-zA-Z0-9\\s]*$"))
            .WithMessage("{PropertyName} has to consist only of English or Russian letters, numbers and spaces!");
        RuleFor(x => x.OwnerId)
            .MustAsync(async (ownerId, cancellationToken) =>
                           await userRepository.ExistsWithIdAsync(ownerId, cancellationToken))
            .WithMessage("Owner with this id: {PropertyValue} doesn't exist!");
    }
}