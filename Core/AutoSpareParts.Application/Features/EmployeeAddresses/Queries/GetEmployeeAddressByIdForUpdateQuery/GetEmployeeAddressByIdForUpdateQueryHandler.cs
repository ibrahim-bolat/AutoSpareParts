using AutoMapper;
using AutoSpareParts.Application.Features.EmployeeAddresses.Constants;
using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities.NotDerivedFromBase;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Queries.GetEmployeeAddressByIdForUpdateQuery;

public class GetByIdUpdateEmployeeAddressQueryHandler : IRequestHandler<GetByIdUpdateEmployeeAddressQueryRequest, GetByIdUpdateEmployeeAddressQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdUpdateEmployeeAddressQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetByIdUpdateEmployeeAddressQueryResponse> Handle(GetByIdUpdateEmployeeAddressQueryRequest request,
        CancellationToken cancellationToken)
    {
        var employeeAddress = await _unitOfWork.EmployeeAddresses.GetAsync(predicate:x => x.Id == request.Id && x.IsActive == true);
        List<City> cityList;
        if (employeeAddress is not null)
        {
            if (employeeAddress.StreetName is not null)
            {
                cityList = await _unitOfWork.Cities.GetAllAsync(
                    include: c => c
                        .Include(city => city.Districts.Where(d => d.CityId == Convert.ToInt32(employeeAddress.CityId)))
                        .ThenInclude(district => district.NeighborhoodsOrVillages.Where(n =>n.DistrictId == Convert.ToInt32(employeeAddress.DistrictId)))
                        .ThenInclude(neighborhoodorvillage => neighborhoodorvillage.Streets.Where(s => s.Id == Convert.ToInt32(employeeAddress.NeighborhoodOrVillageId))));
            }
            else
            {
                cityList = await _unitOfWork.Cities.GetAllAsync(
                    include: c => c.Include(city => city.Districts.Where(d => d.CityId == Convert.ToInt32(employeeAddress.CityId)))
                        .ThenInclude(district => district.NeighborhoodsOrVillages.Where(n => n.DistrictId == Convert.ToInt32(employeeAddress.DistrictId))));
            }
            EmployeeAddressDto employeeAddressDto = new EmployeeAddressDto();
            employeeAddressDto = _mapper.Map(employeeAddress, employeeAddressDto);
            employeeAddressDto.Cities = cityList.Select(city => new SelectListItem()
            {
                Value = city.Id.ToString(),
                Text = city.Name,
            }).ToList();
            var districtList = await _unitOfWork.Districts.GetAllAsync(predicate:district => district.CityId.ToString().Equals(employeeAddressDto.CityId));
            if (districtList != null)
            {
                employeeAddressDto.Districts = districtList.Select(district => new SelectListItem()
                {
                    Value = district.Id.ToString(),
                    Text = district.Name,
                }).ToList();

                var neighborhoodorvillageList = await _unitOfWork.NeighborhoodOrVillages.GetAllAsync(predicate:neighborhoodOrVillage =>
                        neighborhoodOrVillage.DistrictId.ToString().Equals(employeeAddressDto.DistrictId));
                if (neighborhoodorvillageList != null)
                {
                    employeeAddressDto.NeighborhoodsOrVillages = neighborhoodorvillageList.Select(neighborhoodorvillage =>
                        new SelectListItem()
                        {
                            Value = neighborhoodorvillage.Id.ToString(),
                            Text = neighborhoodorvillage.Name,
                        }).ToList();

                    if (!string.IsNullOrEmpty(employeeAddressDto.StreetId))
                    {
                        var streetList = await _unitOfWork.Streets.GetAllAsync(predicate:street =>
                            street.NeighborhoodOrVillageId.ToString().Equals(employeeAddressDto.NeighborhoodOrVillageId));
                        if (streetList != null)
                        {
                            employeeAddressDto.Streets = streetList.Select(street => new SelectListItem()
                            {
                                Value = street.Id.ToString(),
                                Text = street.Name,
                            }).ToList();
                        }
                    }
                }
            }

            return new GetByIdUpdateEmployeeAddressQueryResponse
            {
                Result = new DataResult<EmployeeAddressDto>(ResultStatus.Success, employeeAddressDto)
            };
        }

        return new GetByIdUpdateEmployeeAddressQueryResponse
        {
            Result = new DataResult<EmployeeAddressDto>(ResultStatus.Error, Messages.EmployeeAddressNotFound, null)
        };
    }
}