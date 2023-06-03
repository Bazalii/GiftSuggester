using GiftSuggester.Core.Gifts.Models;
using GiftSuggester.Data.Gifts.Models;
using Riok.Mapperly.Abstractions;

namespace GiftSuggester.Data.Gifts.Mappers;

[Mapper]
public partial class GiftDbModelsMapper
{
    public partial GiftDbModel MapGiftToDbModel(Gift gift);
    public partial Gift MapDbModelToGift(GiftDbModel dbModel);
}