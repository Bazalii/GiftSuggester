using System.Net.Mime;
using GiftSuggester.Core.Groups.Models;
using GiftSuggester.Core.Groups.Services;
using GiftSuggester.Web.Groups.Mappers;
using GiftSuggester.Web.Groups.Models;
using Microsoft.AspNetCore.Mvc;

namespace GiftSuggester.Web.Groups.Controllers;

[ApiController]
[Route("groups")]
[Produces(MediaTypeNames.Application.Json)]
public class GroupController
{
    private readonly IGroupService _groupService;
    private readonly GroupWebModelsMapper _mapper;

    public GroupController(IGroupService groupService, GroupWebModelsMapper mapper)
    {
        _groupService = groupService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<GroupResponse> AddAsync(GroupCreationRequest creationRequest, CancellationToken cancellationToken)
    {
        var addedGroup = await _groupService.AddAsync(
            _mapper.MapCreationRequestToCreationModel(creationRequest),
            cancellationToken);

        return _mapper.MapGroupToResponse(addedGroup);
    }

    [HttpGet("{id:guid}")]
    public async Task<GroupResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var group = await _groupService.GetByIdAsync(id, cancellationToken);

        return _mapper.MapGroupToResponse(group);
    }

    [HttpPut("addUserToGroup/{groupId:guid}/{userId:guid}")]
    public Task AddUserToGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken)
    {
        return _groupService.AddUserToGroupAsync(groupId, userId, cancellationToken);
    }

    [HttpPut("removeUserFromGroup/{groupId:guid}/{userId:guid}")]
    public Task RemoveUserFromGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken)
    {
        return _groupService.RemoveUserFromGroupAsync(groupId, userId, cancellationToken);
    }

    [HttpPut("promoteToAdmin/{groupId:guid}/{userId:guid}")]
    public Task PromoteToAdminAsync(Guid groupId, Guid userId, CancellationToken cancellationToken)
    {
        return _groupService.PromoteToAdminAsync(groupId, userId, cancellationToken);
    }

    [HttpPut("removeAdminRights/{groupId:guid}/{userId:guid}")]
    public Task RemoveAdminRightsAsync(Guid groupId, Guid userId, CancellationToken cancellationToken)
    {
        return _groupService.RemoveAdminRightsAsync(groupId, userId, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _groupService.RemoveByIdAsync(id, cancellationToken);
    }
}