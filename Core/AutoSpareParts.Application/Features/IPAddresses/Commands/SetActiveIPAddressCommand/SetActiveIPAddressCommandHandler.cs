using AutoMapper;
using AutoSpareParts.Application.Features.IPAddresses.Constants;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutoSpareParts.Application.Features.IPAddresses.Commands.SetActiveIPAddressCommand;

public class SetIPAddressActiveCommandCommandHandler : IRequestHandler<SetActiveIPAddressCommandRequest, SetActiveIPAddressCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper; 
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SetIPAddressActiveCommandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SetActiveIPAddressCommandResponse> Handle(SetActiveIPAddressCommandRequest request,
        CancellationToken cancellationToken)
    {
        IPAddress IP = await _unitOfWork.IPAddresses.GetByIdAsync(request.Id);
        if (IP != null)
        {
            if (IP.IsDeleted)
            {
                IP.IsActive = true;
                IP.IsDeleted = false;
                IP.ModifiedTime = DateTime.Now;
                IP.ModifiedByName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                await _unitOfWork.IPAddresses.UpdateAsync(IP);
                int result = await _unitOfWork.SaveAsync();
                if (result > 0)
                {
                    return new SetActiveIPAddressCommandResponse
                    {
                        Result = new  Result(ResultStatus.Success, Messages.IPUpdated)
                    };
                }
                return new SetActiveIPAddressCommandResponse{
                    Result = new Result(ResultStatus.Error, Messages.IPNotUpdated)
                };
            }
            return new SetActiveIPAddressCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.IPActive)
            };
        }
        return new SetActiveIPAddressCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.IPNotFound)
        };
    }
}