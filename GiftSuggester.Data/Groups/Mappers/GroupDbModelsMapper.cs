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

    private UserDbModel UserToUserDbModel(User user) =>
        _context.Users.First(dbModel => dbModel.Id == user.Id);

    private List<UserDbModel> UsersToUserDbModels(List<User> users) =>
        users.Select(member => _context.Users.First(user => user.Id == member.Id))
            .ToList();

    private List<User> DbModelsToUsers(List<UserDbModel> dbModels) =>
        dbModels.Select(dbModel => _userDbModelsMapper.MapDbModelToUser(dbModel))
            .ToList();
}