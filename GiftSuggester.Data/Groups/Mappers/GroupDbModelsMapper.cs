using GiftSuggester.Core.Groups.Models;
using GiftSuggester.Core.Users.Models;
using GiftSuggester.Data.Groups.Models;
using GiftSuggester.Data.Users.Mappers;
using GiftSuggester.Data.Users.Models;
using Riok.Mapperly.Abstractions;

namespace GiftSuggester.Data.Groups.Mappers;

[Mapper]
public partial class GroupDbModelsMapper
{
    private readonly GiftSuggesterContext _context;
    private readonly UserDbModelsMapper _userDbModelsMapper;

    public GroupDbModelsMapper(GiftSuggesterContext context, UserDbModelsMapper userDbModelsMapper)
    {
        _context = context;
        _userDbModelsMapper = userDbModelsMapper;
    }

    public partial GroupDbModel MapGroupToDbModel(Group group);
    public partial Group MapDbModelToGroup(GroupDbModel dbModel);

    private List<UserDbModel> MembersToUserDbModels(List<User> members) =>
        members.Select(member => _context.Users.First(user => user.Id == member.Id))
            .ToList();

    private List<User> DbModelsToMembers(List<UserDbModel> dbModels) =>
        dbModels.Select(dbModel => _userDbModelsMapper.MapDbModelToUser(dbModel))
            .ToList();
}