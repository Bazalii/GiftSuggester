using GiftSuggester.Core.Users.Models;
using GiftSuggester.Data.Users.Models;
using Riok.Mapperly.Abstractions;

namespace GiftSuggester.Data.Users.Mappers;

[Mapper]
public partial class UserDbModelsMapper
{
    public partial UserDbModel MapUserToDbModel(User user);

    public partial User MapDbModelToUser(UserDbModel dbModel);
}