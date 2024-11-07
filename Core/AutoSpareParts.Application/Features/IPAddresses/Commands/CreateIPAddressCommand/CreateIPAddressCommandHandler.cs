using AutoMapper;
using AutoSpareParts.Application.Features.IPAddresses.Constants;
using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutoSpareParts.Application.Features.IPAddresses.Commands.CreatIPpAddressCommand;

public class CreateIpAddressCommandHandler : IRequestHandler<CreateIpAddressCommandRequest, CreateIpAddressCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateIpAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CreateIpAddressCommandResponse> Handle(CreateIpAddressCommandRequest request,
        CancellationToken cancellationToken)
    {
        string createdByName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        var newIPAddress = _mapper.Map<IPAddress>(request.IpDto);
        newIPAddress.CreatedByName = createdByName;
        newIPAddress.ModifiedByName = createdByName;
        newIPAddress.CreatedTime = DateTime.Now;
        newIPAddress.ModifiedTime = DateTime.Now;
        newIPAddress.IsActive = true;
        newIPAddress.IsDeleted = false;
        await _unitOfWork.IPAddresses.AddAsync(newIPAddress);
        int result = await _unitOfWork.SaveAsync();
        if (result > 0)
        {
            return new CreateIpAddressCommandResponse
            {
                Result = new DataResult<IPDto>(ResultStatus.Success, Messages.IPAdded, request.IpDto)
            };
        }
        return new CreateIpAddressCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.IPNotAdded)
        };
    }
}