using FluentValidation;
using GiftSuggester.Core.Users.Models;

namespace GiftSuggester.Core.Users.Validators;

public class UserPasswordValidator : AbstractValidator<User>
{
    public UserPasswordValidator()
    {
        RuleFor(x => x.Password)
            .Length(8, 100)
            .WithMessage("{PropertyName} length has to be from {MinLength} characters to {MaxLength} characters!")
            .Matches("[A-Z]+")
            .WithMessage("{PropertyName} has to contain at least one uppercase English letter!")
            .Matches("[a-z]+")
            .WithMessage("{PropertyName} has to contain at least one lowercase English letter!")
            .Matches("[0-9]+")
            .WithMessage("{PropertyName} has to contain at least one number!")
            .Matches("[!?*.#_^]+")
            .WithMessage("{PropertyName} has to contain at least one of special symbols: \"!,?,*,.,#,_,^\".");
    }
}