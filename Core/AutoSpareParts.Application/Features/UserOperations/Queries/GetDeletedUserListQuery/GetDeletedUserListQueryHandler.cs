using AutoMapper;
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.UserOperations.DTOs;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Application.Features.UserOperations.Queries.GetDeletedUserListQuery;

public class GetDeletedUserListQueryHandler:IRequestHandler<GetDeletedUserListQueryRequest,GetDeletedUserListQueryResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public GetDeletedUserListQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public Task<GetDeletedUserListQueryResponse> Handle(GetDeletedUserListQueryRequest request, CancellationToken cancellationToken)
    {
        var userData = _userManager.Users.Where(u=>u.IsDeleted==true).AsQueryable();
        int pageSize = request.DatatableRequestDto.Length == -1 ? userData.Count() :  request.DatatableRequestDto.Length;
        int skip =  request.DatatableRequestDto.Start;
        var sortColumn = request.DatatableRequestDto.Columns[request.DatatableRequestDto.Order[0].Column].Data;
        var sortColumnDirection = request.DatatableRequestDto.Order[0].Dir.ToString();
        if (!string.IsNullOrEmpty(request.DatatableRequestDto.Search.Value))
        {
            userData = userData.Where(m => m.FirstName.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower())
                                           || m.LastName.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower())
                                           || m.UserName.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower())
                                           || m.Email.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower()));
        }
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            Func<AppUser, string> orderingFunction = (c => sortColumn == nameof(c.FirstName) ? c.FirstName :
                sortColumn  == nameof(c.LastName) ? c.LastName :
                sortColumn  == nameof(c.UserName) ? c.UserName :
                sortColumn  == nameof(c.Email) ? c.Email : c.Id.ToString());

            if (sortColumnDirection == OrderDirType.Desc.ToString())
            {
                userData = userData.OrderByDescending(orderingFunction).AsQueryable();
            }
            else
            {
                userData = userData.OrderBy(orderingFunction).AsQueryable();
            }
        }
        int recordsTotal = userData.Count();
        var data =  userData.Skip(skip).Take(pageSize).ToList();
        List<UserSummaryDto> deletedUser = _mapper.Map<List<UserSummaryDto>>(data);
        var response = new DatatableResponseDto<UserSummaryDto>
        {
            Draw = request.DatatableRequestDto.Draw,
            RecordsTotal = recordsTotal,
            RecordsFiltered = recordsTotal,
            Data = deletedUser
        };
        return Task.FromResult(new GetDeletedUserListQueryResponse{
            Result = new DataResult<DatatableResponseDto<UserSummaryDto>>(ResultStatus.Success, response)
        });
    }
}