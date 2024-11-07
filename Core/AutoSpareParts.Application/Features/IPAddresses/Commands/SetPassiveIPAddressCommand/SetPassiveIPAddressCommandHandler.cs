using AutoMapper;
using AutoSpareParts.Application.Features.IPAddresses.Constants;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutoSpareParts.Application.Features.IPAddresses.Commands.SetPassiveIPAddressCommand;

public class SetIpAddressPassiveCommandCommandHandler : IRequestHandler<SetPassiveIpAddressCommandRequest, SetPassiveIpAddressCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper; 
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SetIpAddressPassiveCommandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SetPassiveIpAddressCommandResponse> Handle(SetPassiveIpAddressCommandRequest request,
        CancellationToken cancellationToken)
    {
        IPAddress IP = await _unitOfWork.IPAddresses.GetByIdAsync(request.Id);
        if (IP != null)
        {
            if (IP.IsActive)
            {
                IP.IsActive = false;
                IP.IsDeleted = true;
                IP.ModifiedTime = DateTime.Now;
                IP.ModifiedByName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                await _unitOfWork.IPAddresses.UpdateAsync(IP);
                int result = await _unitOfWork.SaveAsync();
                if (result > 0)
                {
                    return new SetPassiveIpAddressCommandResponse
                    {
                        Result = new  Result(ResultStatus.Success, Messages.IPUpdated)
                    };
                }
                return new SetPassiveIpAddressCommandResponse{
                    Result = new Result(ResultStatus.Error, Messages.IPNotUpdated)
                };
            }
            return new SetPassiveIpAddressCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.IPNotActive)
            };
        }
        return new SetPassiveIpAddressCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.IPNotFound)
        };
    }
}