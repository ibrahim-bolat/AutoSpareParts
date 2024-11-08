using AutoMapper;
using AutoSpareParts.Application.Features.IPAddresses.Constants;
using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutoSpareParts.Application.Features.IPAddresses.Commands.UpdateIPAddressCommand;

public class UpdateIPAddressCommandHandler : IRequestHandler<UpdateIPAddressCommandRequest, UpdateIPAddressCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public UpdateIPAddressCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<UpdateIPAddressCommandResponse> Handle(UpdateIPAddressCommandRequest request,
        CancellationToken cancellationToken)
    {
        IPAddress IP = await _unitOfWork.IPAddresses.GetByIdAsync(request.IPDto.Id);
        if (IP != null)
        {
            IP= _mapper.Map(request.IPDto,IP);
            IP.ModifiedTime = DateTime.Now;
            IP.ModifiedByName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
            await _unitOfWork.IPAddresses.UpdateAsync(IP);
            int result = await _unitOfWork.SaveAsync();
            if (result > 0)
            {
                return new UpdateIPAddressCommandResponse
                {
                    Result = new DataResult<IPDto>(ResultStatus.Success, Messages.IPUpdated, request.IPDto)
                };
            }
            return new UpdateIPAddressCommandResponse{
                Result = new Result(ResultStatus.Error, Messages.IPNotUpdated)
            };
        }
        return new UpdateIPAddressCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.IPNotFound)
        };
    }
}