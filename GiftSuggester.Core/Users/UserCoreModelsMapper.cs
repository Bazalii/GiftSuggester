using GiftSuggester.Core.Users.Models;
using Riok.Mapperly.Abstractions;

namespace GiftSuggester.Core.Users;

[Mapper]
public partial class UserCoreModelsMapper
{
    public User MapCreationModelToUser(UserCreationModel creationModel)
    {
        var user = MapCreationModelToUserWithoutId(creationModel);

        user.Id = Guid.NewGuid();

        return user;
    }

    private partial User MapCreationModelToUserWithoutId(UserCreationModel creationModel);
}