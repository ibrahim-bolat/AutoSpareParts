using AutoSpareParts.Application.Features.Endpoints.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using AutoSpareParts.Application.Repositories.Common;

namespace AutoSpareParts.Application.Features.Endpoints.Queries.GetEndpointListForAssignRoleQuery;

public class
    GetAuthorizeEndpointsforAssignRoleQueryHandler : IRequestHandler<GetEndpointListForAssignRoleQueryRequest,
        GetEndpointListForAssignRoleQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorizeEndpointsforAssignRoleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetEndpointListForAssignRoleQueryResponse> Handle(
        GetEndpointListForAssignRoleQueryRequest request, CancellationToken cancellationToken)
    {
        List<Endpoint> endpoints = await _unitOfWork.Endpoints.GetAllAsync(predicate:e => e.IsActive);
        HashSet<string> menus = new();
        endpoints.ForEach(e => menus.Add(e.ControllerName));
        List<TreeViewDto> treeViewDtos = new();
        int menuCount = 1;
        if (endpoints != null && menus != null)
        {
            foreach (var menu in menus)
            {
                treeViewDtos.Add(
                    new TreeViewDto()
                    {
                        id = (menuCount++).ToString(),
                        text = $"{menu} <span class='badge badge-pill badge-success' style = 'font-size: 16px;'>{endpoints.Count(e => e.ControllerName == menu)}</span>" ,
                        children = GetEndpoints(endpoints.Where(e => e.ControllerName == menu).ToList()),
                    }
                );
            }
            menuCount = 1;
            return new GetEndpointListForAssignRoleQueryResponse
            {
                Result = new DataResult<List<TreeViewDto>>(ResultStatus.Success, treeViewDtos)
            };
        }
        return new GetEndpointListForAssignRoleQueryResponse
        {
            Result = new DataResult<List<TreeViewDto>>(ResultStatus.Error, Messages.EndpointNotFound, null,null)
        };

    }

    private List<TreeViewDto> GetEndpoints(List<Endpoint> endpoints)
    {
        List<TreeViewDto> treeViewDtos = new List<TreeViewDto>();
        foreach (var endpoint in endpoints)
        {
            treeViewDtos.Add(new TreeViewDto()
            {
                id = endpoint.Id.ToString(),
                text = $"<a class='btn btn-info mr-2' onclick='return getRolesByEndpointId({endpoint.Id.ToString()})'>" +
                       $"<i class='fa fa-tasks'>Rol Ata</i></a> {endpoint.Definition}",
                children = null
            });
        }
        return treeViewDtos;
    }
}