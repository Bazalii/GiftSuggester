using GiftSuggester.Core.Gifts.Models;
using Riok.Mapperly.Abstractions;

namespace GiftSuggester.Core.Gifts.Mappers;

[Mapper]
public partial class GiftCoreModelsMapper
{
    public Gift MapCreationModelToGift(GiftCreationModel creationModel)
    {
        var gift = MapCreationModelToGiftWithoutId(creationModel);

        gift.Id = Guid.NewGuid();

        return gift;
    }

    private partial Gift MapCreationModelToGiftWithoutId(GiftCreationModel creationModel);
}