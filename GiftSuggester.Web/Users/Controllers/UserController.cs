using System.Net.Mime;
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
    private readonly UserWebModelsMapper _mapper;
    private readonly IUserService _userService;

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

    [HttpGet("byLogin/{login}")]
    public async Task<UserResponse> GetByLoginAsync(string login, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByLoginAsync(login, cancellationToken);

        return _mapper.MapUserToResponse(user);
    }

    [HttpGet("byEmail/{email}")]
    public async Task<UserResponse> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByEmailAsync(email, cancellationToken);

        return _mapper.MapUserToResponse(user);
    }

    [HttpPut]
    public Task UpdateAsync(UserUpdateRequest updateRequest, CancellationToken cancellationToken)
    {
        return _userService.UpdateAsync(_mapper.MapUpdateRequestToUser(updateRequest), cancellationToken);
    }

    [HttpPut("password")]
    public Task UpdatePasswordAsync(UpdateUserPasswordRequest updateRequest, CancellationToken cancellationToken)
    {
        return _userService.UpdatePasswordAsync(
            updateRequest.UserId,
            updateRequest.OldPassword,
            updateRequest.NewPassword,
            cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _userService.RemoveByIdAsync(id, cancellationToken);
    }
}