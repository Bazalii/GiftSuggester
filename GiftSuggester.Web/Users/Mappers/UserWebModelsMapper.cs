using GiftSuggester.Core.Users.Models;
using GiftSuggester.Web.Users.Models;
using Riok.Mapperly.Abstractions;

namespace GiftSuggester.Web.Users.Mappers;

[Mapper]
public partial class UserWebModelsMapper
{
    public partial UserCreationModel MapCreationRequestToCreationModel(UserCreationRequest creationRequest);
    public partial User MapUpdateRequestToUser(UserUpdateRequest updateRequest);
    public partial UserResponse MapUserToResponse(User user);
}