using System.Net.Mime;
using GiftSuggester.Core.Users.Models;
using GiftSuggester.Core.Users.Services;
using GiftSuggester.Web.Users.Mappers;
using GiftSuggester.Web.Users.Models;
using Microsoft.AspNetCore.Mvc;

namespace GiftSuggester.Web.Users.Controllers;

[ApiController]
[Route("users")]
[Produces(MediaTypeNames.Application.Json)]
public class UserController
{
    private readonly IUserService _userService;
    private readonly UserWebModelsMapper _mapper;

    public UserController(IUserService userService, UserWebModelsMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost]
    public Task AddAsync(UserCreationRequest creationRequest, CancellationToken cancellationToken)
    {
        return _userService.AddAsync(
            _mapper.MapCreationRequestToCreationModel(creationRequest),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<UserResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(id, cancellationToken);

        return _mapper.MapUserToResponse(user);
    }

    [HttpPut]
    public Task UpdateAsync(UserUpdateRequest updateRequest, CancellationToken cancellationToken)
    {
        return _userService.UpdateAsync(_mapper.MapUpdateRequestToUser(updateRequest), cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _userService.RemoveByIdAsync(id, cancellationToken);
    }
}