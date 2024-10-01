using AutoMapper;
using AutoSpareParts.Application.Features.Addresses.Constants;
using AutoSpareParts.Application.Features.Addresses.DTOs;
using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetByIdDetailAddressQuery;

public class GetByIdDetailAddressQueryHandler : IRequestHandler<GetByIdDetailAddressQueryRequest, GetByIdDetailAddressQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdDetailAddressQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetByIdDetailAddressQueryResponse> Handle(GetByIdDetailAddressQueryRequest request,
        CancellationToken cancellationToken)
    {
        var address =
            await _unitOfWork.GetRepository<Address>().GetAsync(predicate:x => x.Id == request.Id && x.IsActive == true);
        if (address is not null)
        {
            DetailAddressDto detailAddressDto = _mapper.Map<DetailAddressDto>(address);
            return new GetByIdDetailAddressQueryResponse
            {
                Result = new DataResult<DetailAddressDto>(ResultStatus.Success, detailAddressDto)
            };
        }
        return new GetByIdDetailAddressQueryResponse
        {
            Result = new DataResult<DetailAddressDto>(ResultStatus.Error, Messages.AddressNotFound, null)
        };
    }
}