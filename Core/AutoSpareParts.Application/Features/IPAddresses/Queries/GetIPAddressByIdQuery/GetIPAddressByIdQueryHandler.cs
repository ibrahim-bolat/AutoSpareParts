using AutoMapper;
using AutoSpareParts.Application.Features.IPAddresses.Constants;
using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Enums;
using MediatR;

namespace AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAddressByIdQuery;

public class GetIPAddressByIdQueryHandler:IRequestHandler<GetIPAddressByIdQueryRequest,GetIPAddressByIdQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetIPAddressByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetIPAddressByIdQueryResponse> Handle(GetIPAddressByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var IP = await _unitOfWork.IPAddresses.GetByIdAsync(request.Id);
        if (IP != null)
        {
            IPDto IPDto = _mapper.Map<IPDto>(IP);
            return new GetIPAddressByIdQueryResponse{
                Result = new DataResult<IPDto>(ResultStatus.Success, IPDto)
            };
        }
        return new GetIPAddressByIdQueryResponse{
            Result = new DataResult<IPDto>(ResultStatus.Error, Messages.IPNotFound,null)
        };
    }
}