using GiftSuggester.Web.Gifts.Mappers;
using GiftSuggester.Web.Groups.Mappers;
using GiftSuggester.Web.HostedServices;
using GiftSuggester.Web.Users.Mappers;
using Microsoft.OpenApi.Models;

namespace GiftSuggester.Web;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWeb(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<GiftWebModelsMapper, GiftWebModelsMapper>();
        serviceCollection.AddScoped<GroupWebModelsMapper, GroupWebModelsMapper>();
        serviceCollection.AddScoped<UserWebModelsMapper, UserWebModelsMapper>();

        serviceCollection.AddHostedService<MigrationHostedService>();

        serviceCollection.AddControllers();

        serviceCollection.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                "v1", new OpenApiInfo { Title = "GiftSuggester", Version = "v1" });
        });

        return serviceCollection;
    }
}