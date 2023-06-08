using FluentValidation;
using GiftSuggester.Core.Groups.Models;
using GiftSuggester.Core.Users.Repositories;

namespace GiftSuggester.Core.Groups.Validators;

public class GroupValidator : AbstractValidator<Group>
{
    public GroupValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Name)
            .Length(3, 100)
            .WithMessage("{PropertyName} length has to be from {MinLength} characters to {MaxLength} characters!")
            .Matches("^[а-яА-Яa-zA-Z0-9\\s]*$")
            .WithMessage("{PropertyName} has to consist only of English or Russian letters, numbers and spaces!");
    }
}