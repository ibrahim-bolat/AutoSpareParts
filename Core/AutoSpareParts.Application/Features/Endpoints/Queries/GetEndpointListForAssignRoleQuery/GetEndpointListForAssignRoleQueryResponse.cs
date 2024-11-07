using AutoSpareParts.Application.DTOs;
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Models;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Endpoints.Queries.GetEndpointListForAssignRoleQuery;

public class GetEndpointListForAssignRoleQueryResponse
{
    public IDataResult<List<TreeViewDto>> Result { get; set; }
}