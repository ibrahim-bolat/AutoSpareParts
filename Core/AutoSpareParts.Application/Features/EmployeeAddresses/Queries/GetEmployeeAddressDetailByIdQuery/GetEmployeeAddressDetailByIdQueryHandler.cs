using AutoMapper;
using AutoSpareParts.Application.Features.EmployeeAddresses.Constants;
using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using AutoSpareParts.Application.Features.EmpLoyeeAddresses.Queries.GetEmployeeAddressDetailByIdQuery;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Enums;
using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Queries.GetByIdDetailEmployeeAddressQuery;

public class GetEmployeeAddressDetailByIdQueryHandler : IRequestHandler<GetEmployeeAddressDetailByIdQueryRequest, GetEmployeeAddressDetailByIdQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEmployeeAddressDetailByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetEmployeeAddressDetailByIdQueryResponse> Handle(GetEmployeeAddressDetailByIdQueryRequest request,
        CancellationToken cancellationToken)
    {
        var address =
            await _unitOfWork.EmployeeAddresses.GetAsync(predicate:x => x.Id == request.Id && x.IsActive == true);
        if (address is not null)
        {
            EmployeeAddressDetailDto detailEmployeeAddressDto = _mapper.Map<EmployeeAddressDetailDto>(address);
            return new GetEmployeeAddressDetailByIdQueryResponse
            {
                Result = new DataResult<EmployeeAddressDetailDto>(ResultStatus.Success, detailEmployeeAddressDto)
            };
        }
        return new GetEmployeeAddressDetailByIdQueryResponse
        {
            Result = new DataResult<EmployeeAddressDetailDto>(ResultStatus.Error, Messages.EmployeeAddressNotFound, null)
        };
    }
}