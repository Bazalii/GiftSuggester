using FluentValidation;
using GiftSuggester.Core.Gifts.Models;
using GiftSuggester.Core.Gifts.Services;
using GiftSuggester.Core.Gifts.Services.Implementations;
using GiftSuggester.Core.Gifts.Validators;
using GiftSuggester.Core.Groups.Models;
using GiftSuggester.Core.Groups.Services;
using GiftSuggester.Core.Groups.Services.Implementations;
using GiftSuggester.Core.Groups.Validators;
using GiftSuggester.Core.Users.Models;
using GiftSuggester.Core.Users.Services;
using GiftSuggester.Core.Users.Services.Implementations;
using GiftSuggester.Core.Users.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace GiftSuggester.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IGiftService, GiftService>();
        serviceCollection.AddScoped<IGroupService, GroupService>();
        serviceCollection.AddScoped<IUserService, UserService>();

        serviceCollection.AddScoped<IValidator<Gift>, GiftValidator>();
        serviceCollection.AddScoped<IValidator<Group>, GroupValidator>();
        serviceCollection.AddScoped<UserValidator>();
        serviceCollection.AddScoped<UserPasswordValidator>();

        return serviceCollection;
    }
}