using AutoMapper;
using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.DTOs;
using AutoSpareParts.Application.Features.Accounts.DTOs;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.Accounts.Queries.GetEditPasswordAccountByIdQuery;

public class GetEditPasswordAccountByIdQueryHandler:IRequestHandler<GetEditPasswordAccountByIdQueryRequest,GetEditPasswordAccountByIdQueryResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetEditPasswordAccountByIdQueryHandler(UserManager<AppUser> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GetEditPasswordAccountByIdQueryResponse> Handle(GetEditPasswordAccountByIdQueryRequest request, CancellationToken cancellationToken)
    {
        AppUser user = await _userManager.FindByIdAsync(request.Id);
        string contextUserName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
        if (user.UserName.Equals(contextUserName))
        {
            if (user != null)
            {
                if (user.IsActive)
                {
                    EditPasswordAccountDto editPasswordAccountDto = _mapper.Map<EditPasswordAccountDto>(user);
                    return new GetEditPasswordAccountByIdQueryResponse
                    {
                        Result = new DataResult<EditPasswordAccountDto>(ResultStatus.Success,editPasswordAccountDto)
                    };
                }
                return new GetEditPasswordAccountByIdQueryResponse{
                    Result = new DataResult<EditPasswordAccountDto>(ResultStatus.Error, Messages.UserNotActive,null)
                };
            }
        }
        return new GetEditPasswordAccountByIdQueryResponse{
            Result = new DataResult<EditPasswordAccountDto>(ResultStatus.Error, Messages.UserNotFound,null)
        };
    }
}