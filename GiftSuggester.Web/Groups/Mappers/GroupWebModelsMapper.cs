using GiftSuggester.Core.Groups.Models;
using GiftSuggester.Core.Users.Models;
using GiftSuggester.Web.Groups.Models;
using GiftSuggester.Web.Users.Mappers;
using GiftSuggester.Web.Users.Models;
using Riok.Mapperly.Abstractions;

namespace GiftSuggester.Web.Groups.Mappers;

[Mapper]
public partial class GroupWebModelsMapper
{
    private readonly UserWebModelsMapper _userWebModelsMapper;

    public GroupWebModelsMapper(UserWebModelsMapper userWebModelsMapper)
    {
        _userWebModelsMapper = userWebModelsMapper;
    }

    public partial GroupCreationModel MapCreationRequestToCreationModel(GroupCreationRequest creationRequest);
    public partial GroupResponse MapGroupToResponse(Group group);

    private List<UserResponse> MapUsersToUserResponses(List<User> users)
    {
        return users.Select(user => _userWebModelsMapper.MapUserToResponse(user))
            .ToList();
    }
}