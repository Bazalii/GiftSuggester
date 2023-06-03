using System.Net.Mime;
using GiftSuggester.Core.Gifts.Services;
using GiftSuggester.Web.Gifts.Mappers;
using GiftSuggester.Web.Gifts.Models;
using Microsoft.AspNetCore.Mvc;

namespace GiftSuggester.Web.Gifts.Controllers;

[ApiController]
[Route("gifts")]
[Produces(MediaTypeNames.Application.Json)]
public class GiftController
{
    private readonly IGiftService _giftService;
    private readonly GiftWebModelsMapper _mapper;

    public GiftController(IGiftService giftService, GiftWebModelsMapper mapper)
    {
        _giftService = giftService;
        _mapper = mapper;
    }

    [HttpPost]
    public Task AddAsync(GiftCreationRequest creationRequest, CancellationToken cancellationToken)
    {
        return _giftService.AddAsync(
            _mapper.MapCreationRequestToCreationModel(creationRequest),
            cancellationToken);
    }

    [HttpGet("getAllByPresenterId/{id:guid}")]
    public async Task<List<GiftResponse>> GetAllByPresenterIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var gifts = await _giftService.GetAllByPresenterIdAsync(id, cancellationToken);

        return gifts
            .Select(gift => _mapper.MapGiftToResponse(gift))
            .ToList();
    }

    [HttpGet("getAllByRecipientIdAsync/{id:guid}")]
    public async Task<List<GiftResponse>> GetAllByRecipientIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var gifts = await _giftService.GetAllByRecipientIdAsync(id, cancellationToken);

        return gifts
            .Select(gift => _mapper.MapGiftToResponse(gift))
            .ToList();
    }

    [HttpPut]
    public Task UpdateAsync(GiftUpdateRequest updateRequest, CancellationToken cancellationToken)
    {
        return _giftService.UpdateAsync(
            _mapper.MapUpdateRequestToGift(updateRequest),
            cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _giftService.RemoveByIdAsync(id, cancellationToken);
    }
}