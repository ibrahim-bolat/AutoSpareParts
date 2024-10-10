using AutoMapper;
using AutoSpareParts.Application.Features.IpOperations.Constants;
using AutoSpareParts.Application.Features.IpOperations.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutoSpareParts.Application.Features.IpOperations.Commands.SetIpAddressActiveCommand;

public class SetIpAddressActiveCommandCommandHandler : IRequestHandler<SetIpAddressActiveCommandRequest, SetIpAddressActiveCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper; 
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SetIpAddressActiveCommandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SetIpAddressActiveCommandResponse> Handle(SetIpAddressActiveCommandRequest request,
        CancellationToken cancellationToken)
    {
        IpAddress ipAddress = await _unitOfWork.IpAddresses.GetByIdAsync(request.Id);
        if (ipAddress != null)
        {
            if (ipAddress.IsDeleted)
            {
                ipAddress.IsActive = true;
                ipAddress.IsDeleted = false;
                ipAddress.ModifiedTime = DateTime.Now;
                ipAddress.ModifiedByName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                await _unitOfWork.IpAddresses.UpdateAsync(ipAddress);
                int result = await _unitOfWork.SaveAsync();
                if (result > 0)
                {
                    return new SetIpAddressActiveCommandResponse
                    {
                        Result = new  Result(ResultStatus.Success, Messages.IpUpdated)
                    };
                }
                return new SetIpAddressActiveCommandResponse{
                    Result = new Result(ResultStatus.Error, Messages.IpNotUpdated)
                };
            }
            return new SetIpAddressActiveCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.IpActive)
            };
        }
        return new SetIpAddressActiveCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.IpNotFound)
        };
    }
}