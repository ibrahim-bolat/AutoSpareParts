using AutoMapper;
using AutoSpareParts.Application.Features.EmployeeAddresses.Constants;
using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using AutoSpareParts.Application.Features.EmployeeAddresses.Queries.GetSelectedAddressQuery;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Queries.GetSelectedAddressQuery;

public class GetSelectedEmployeeAddressQueryHandler:IRequestHandler<GetSelectedEmployeeAddressQueryRequest, GetSelectedEmployeeAddressQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSelectedEmployeeAddressQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetSelectedEmployeeAddressQueryResponse> Handle(GetSelectedEmployeeAddressQueryRequest request,
        CancellationToken cancellationToken)
    {
        var cityList = await _unitOfWork.Cities.GetAllAsync();
        if (cityList != null)
        {
            request.EmployeeAddressDto.Cities = cityList.Select(city => new SelectListItem()
            {
                Value = city.Id.ToString(),
                Text = city.Name,
            }).ToList();
            if (!string.IsNullOrEmpty(request.EmployeeAddressDto.CityId))
            {
                var districtList = await _unitOfWork.Districts.GetAllAsync(predicate: district => district.CityId == Convert.ToInt32(request.EmployeeAddressDto.CityId));
                if (districtList != null)
                {
                    request.EmployeeAddressDto.Districts = districtList.Select(district => new SelectListItem()
                    {
                        Value = district.Id.ToString(),
                        Text = district.Name,
                    }).ToList();

                    if (!string.IsNullOrEmpty(request.EmployeeAddressDto.DistrictId))
                    {
                        var neighborhoodorvillageList = await _unitOfWork.NeighborhoodOrVillages.GetAllAsync(predicate: neighborhoodOrVillage =>
                                neighborhoodOrVillage.DistrictId == Convert.ToInt32(request.EmployeeAddressDto.DistrictId));
                        if (neighborhoodorvillageList != null)
                        {
                            request.EmployeeAddressDto.NeighborhoodsOrVillages = neighborhoodorvillageList.Select(
                                neighborhoodorvillage => new SelectListItem()
                                {
                                    Value = neighborhoodorvillage.Id.ToString(),
                                    Text = neighborhoodorvillage.Name,
                                }).ToList();

                            if (!string.IsNullOrEmpty(request.EmployeeAddressDto.NeighborhoodOrVillageId))
                            {
                                var streetList = await _unitOfWork.Streets.GetAllAsync(predicate: street =>
                                                street.NeighborhoodOrVillageId ==Convert.ToInt32(request.EmployeeAddressDto.NeighborhoodOrVillageId));
                                if (streetList != null)
                                {
                                    request.EmployeeAddressDto.Streets = streetList.Select(street => new SelectListItem()
                                    {
                                        Value = street.Id.ToString(),
                                        Text = street.Name,
                                    }).ToList();
                                }
                            }
                        }
                    }
                }
            }

            return new GetSelectedEmployeeAddressQueryResponse
            {
                Result = new DataResult<EmployeeAddressDto>(ResultStatus.Success, request.EmployeeAddressDto)
            };
        }
        return new GetSelectedEmployeeAddressQueryResponse
        {
            Result = new DataResult<EmployeeAddressDto>(ResultStatus.Error, Messages.EmployeeAddressNotFound,null)
        };
    }
}