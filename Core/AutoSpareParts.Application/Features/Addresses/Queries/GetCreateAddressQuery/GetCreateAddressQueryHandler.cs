using AutoMapper;
using AutoSpareParts.Application.Features.Addresses.Constants;
using AutoSpareParts.Application.Features.Addresses.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetCreateAddressQuery;

public class GetCreateAddressQueryHandler:IRequestHandler<GetCreateAddressQueryRequest,GetCreateAddressQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCreateAddressQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetCreateAddressQueryResponse> Handle(GetCreateAddressQueryRequest request,
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
            AddressDto addressDto = new AddressDto()
            {
                UserId = request.UserId,
                Cities = citySelectListItems,
            };
            return new GetCreateAddressQueryResponse
            {
                Result = new DataResult<AddressDto>(ResultStatus.Success, addressDto)
            };
        }
        return new GetCreateAddressQueryResponse
        {
            Result = new DataResult<AddressDto>(ResultStatus.Error, Messages.AddressNotFound,null)
        };
    }
}