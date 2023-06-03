using GiftSuggester.Core.Gifts.Models;
using GiftSuggester.Web.Gifts.Models;
using Riok.Mapperly.Abstractions;

namespace GiftSuggester.Web.Gifts.Mappers;

[Mapper]
public partial class GiftWebModelsMapper
{
    public partial GiftCreationModel MapCreationRequestToCreationModel(GiftCreationRequest creationRequest);
    public partial Gift MapUpdateRequestToGift(GiftUpdateRequest updateRequest);
    public partial GiftResponse MapGiftToResponse(Gift gift);
}