using AutoMapper;
using AutoSpareParts.Application.Features.EmployeeAddresses.Constants;
using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using AutoSpareParts.Application.Features.EmployeeAddresses.Queries.GetEmployeeAddressByEmployeeIdForCreateQuery;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Queries.GetCreateEmployeeAddressQuery;

public class GetEmployeeAddressByEmployeeIdForCreateQueryHandler:IRequestHandler<GetEmployeeAddressByEmployeeIdForCreateQueryRequest,GetEmployeeAddressByEmployeeIdForCreateQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetEmployeeAddressByEmployeeIdForCreateQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetEmployeeAddressByEmployeeIdForCreateQueryResponse> Handle(GetEmployeeAddressByEmployeeIdForCreateQueryRequest request,
        CancellationToken cancellationToken)
    {
        var cityList = await _unitOfWork.Cities.GetAllAsync();
        List<SelectListItem> citySelectListItems = null;
        if (cityList != null)
        {
            citySelectListItems = cityList.Select(city => new SelectListItem()
            {
                Value = city.Id.ToString(),
                Text = city.Name
            }).ToList();
            EmployeeAddressDto EmployeeAddressDto = new EmployeeAddressDto()
            {
                EmployeeId = request.EmployeeId,
                Cities = citySelectListItems,
            };
            return new GetEmployeeAddressByEmployeeIdForCreateQueryResponse
            {
                Result = new DataResult<EmployeeAddressDto>(ResultStatus.Success, EmployeeAddressDto)
            };
        }
        return new GetEmployeeAddressByEmployeeIdForCreateQueryResponse
        {
            Result = new DataResult<EmployeeAddressDto>(ResultStatus.Error, Messages.EmployeeAddressNotFound, null)
        };
    }
}