using AutoMapper;
using AutoSpareParts.Application.Features.Accounts.DTOs;
using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSpareParts.Application.Features.Accounts.Queries.GetByIdUserQuery;

public class GetByIdUserQueryHandler:IRequestHandler<GetByIdUserQueryRequest,GetByIdUserQueryResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public GetByIdUserQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<GetByIdUserQueryResponse> Handle(GetByIdUserQueryRequest request, CancellationToken cancellationToken)
    {
        AppUser user = await _userManager.FindByIdAsync(request.Id);
        if (user != null)
        {
            if (user.IsActive)
            {
                UserDto userDto = _mapper.Map<UserDto>(user);
                return new GetByIdUserQueryResponse
                {
                    Result = new DataResult<UserDto>(ResultStatus.Success,userDto)
                };
            }
            return new GetByIdUserQueryResponse{
                Result = new DataResult<UserDto>(ResultStatus.Error, Messages.UserNotActive,null)
            };
        }
        return new GetByIdUserQueryResponse{
            Result = new DataResult<UserDto>(ResultStatus.Error, Messages.UserNotFound,null)
        };
    }
}