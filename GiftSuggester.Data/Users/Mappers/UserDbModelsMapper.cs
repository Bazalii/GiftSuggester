using GiftSuggester.Core.Users.Models;
using GiftSuggester.Data.Groups.Models;
using GiftSuggester.Data.Users.Models;
using Riok.Mapperly.Abstractions;

namespace GiftSuggester.Data.Users.Mappers;

[Mapper]
public partial class UserDbModelsMapper
{
    private readonly GiftSuggesterContext _context;

    public UserDbModelsMapper(GiftSuggesterContext context)
    {
        _context = context;
    }

    [MapProperty(nameof(User.GroupIds), nameof(UserDbModel.Groups))]
    public partial UserDbModel MapUserToDbModel(User user);

    [MapProperty(nameof(UserDbModel.Groups), nameof(User.GroupIds))]
    public partial User MapDbModelToUser(UserDbModel dbModel);

    private List<GroupDbModel> GroupIdsToGroupDbModels(List<Guid> ids) =>
        ids.Select(id => _context.Groups.First(group => group.Id == id))
            .ToList();

    private List<Guid> GroupDbModelsToGroupIds(List<GroupDbModel> dbModels) =>
        dbModels.Select(dbModel => dbModel.Id)
            .ToList();
}