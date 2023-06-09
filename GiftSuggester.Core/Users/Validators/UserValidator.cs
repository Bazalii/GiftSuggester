using FluentValidation;
using GiftSuggester.Core.Users.Models;
using GiftSuggester.Core.Users.Repositories;

namespace GiftSuggester.Core.Users.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Name)
            .Length(3, 100)
            .WithMessage("{PropertyName} length has to be from {MinLength} characters to {MaxLength} characters!")
            .Matches("^[а-яА-Яa-zA-Z\\s]+$")
            .WithMessage("{PropertyName} has to consist only of English or Russian letters and spaces!");
        RuleFor(x => x.Login)
            .Length(5, 30)
            .WithMessage("{PropertyName} length has to be from {MinLength} characters to {MaxLength} characters!")
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage("{PropertyName} has to consist of English letters and numbers!")
            .MustAsync(
                async (login, cancellationToken) =>
                    !await userRepository.ExistsWithLoginAsync(login, cancellationToken))
            .WithMessage("{PropertyName} is already taken!");
        RuleFor(x => x.Email)
            .Length(5, 100)
            .WithMessage("{PropertyName} length has to be from {MinLength} characters to {MaxLength} characters!")
            .EmailAddress()
            .WithMessage("{PropertyName} has invalid format!")
            .MustAsync(
                async (email, cancellationToken) =>
                    !await userRepository.ExistsWithEmailAsync(email, cancellationToken))
            .WithMessage("{PropertyName} is already taken!");
        RuleFor(x => x.DateOfBirth)
            .GreaterThanOrEqualTo(new DateOnly(1900, 1, 1))
            .WithMessage("Your date of birth has to be later than 01.01.1900!");
        Include(new UserPasswordValidator());
    }
}