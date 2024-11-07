using AutoMapper;
using AutoSpareParts.Application.Features.EmployeeAddresses.Constants;
using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.NotDerivedFromBase;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Commands.CreateAddressCommand;

public class CreateEmployeeAddressCommandHandler:IRequestHandler<CreateEmployeeAddressCommandRequest, CreateEmployeeAddressCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateEmployeeAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CreateEmployeeAddressCommandResponse> Handle(CreateEmployeeAddressCommandRequest request, CancellationToken cancellationToken)
    {
       var count = await _unitOfWork.EmployeeAddresses.CountAsync(predicate:x => x.EmployeeId == request.EmployeeAddressDto.EmployeeId && x.IsActive);
       string createdByName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        if (count >= 4)
        {
            return new CreateEmployeeAddressCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.EmployeeAddressCountMoreThan4)
            };
        }
        int cityId = Convert.ToInt32(request.EmployeeAddressDto.CityId);
        int districtId = Convert.ToInt32(request.EmployeeAddressDto.DistrictId);
        int neighborhoodorvillageId = Convert.ToInt32(request.EmployeeAddressDto.NeighborhoodOrVillageId);
        string cityName = null;
        string districtName = null;
        string neighborhoodOrVillageName = null;
        string streetName = null;
        City city = null;
        if (int.TryParse(request.EmployeeAddressDto.StreetId, out int streetId))
        {
            city = await _unitOfWork.Cities.GetAsync(predicate:c => c.Id == cityId,
                include: c => c.Include(city=>city.Districts.Where(d=>d.Id==districtId))
                    .ThenInclude(district=>district.NeighborhoodsOrVillages.Where(n=>n.Id==neighborhoodorvillageId))
                    .ThenInclude(neighborhoodorvillage=>neighborhoodorvillage.Streets.Where(s=>s.Id==streetId)));
             cityName = city.Name;
             districtName = city.Districts.FirstOrDefault()?.Name;
             neighborhoodOrVillageName = city.Districts.FirstOrDefault()?.NeighborhoodsOrVillages.FirstOrDefault()?.Name;
             streetName = city.Districts.FirstOrDefault()?.NeighborhoodsOrVillages.FirstOrDefault()
                ?.Streets.FirstOrDefault()?.Name;
        }
        else
        {
            city = await _unitOfWork.Cities.GetAsync(predicate:c => c.Id == cityId,
                include: c => c.Include(city => city.Districts.Where(d => d.Id == districtId))
                    .ThenInclude(district => district.NeighborhoodsOrVillages.Where(n => n.Id == neighborhoodorvillageId)));
             cityName = city.Name;
             districtName = city.Districts.FirstOrDefault()?.Name;
             neighborhoodOrVillageName = city.Districts.FirstOrDefault()?.NeighborhoodsOrVillages.FirstOrDefault()?.Name;
        }
        if (count > 0)
        {
            if (request.EmployeeAddressDto.DefaultAddress)
            {
                var addresses = await _unitOfWork.EmployeeAddresses.GetAllAsync(predicate:a=>a.EmployeeId == request.EmployeeAddressDto.EmployeeId);
                foreach (var address in addresses)
                {
                    address.DefaultAddress = false;
                    address.ModifiedByName = createdByName;
                    address.ModifiedTime = DateTime.Now;
                    await _unitOfWork.EmployeeAddresses.UpdateAsync(address);
                }
                var newEmployeeAddress = _mapper.Map<EmployeeAddress>(request.EmployeeAddressDto);
                newEmployeeAddress.DefaultAddress = true;
                newEmployeeAddress.CreatedByName = createdByName;
                newEmployeeAddress.ModifiedByName = createdByName;
                newEmployeeAddress.CreatedTime = DateTime.Now;
                newEmployeeAddress.ModifiedTime = DateTime.Now;
                newEmployeeAddress.IsActive = true;
                newEmployeeAddress.IsDeleted = false;
                newEmployeeAddress.CityId = request.EmployeeAddressDto.CityId;
                newEmployeeAddress.CityName = cityName;
                newEmployeeAddress.DistrictId = request.EmployeeAddressDto.DistrictId;
                newEmployeeAddress.DistrictName = districtName;
                newEmployeeAddress.NeighborhoodOrVillageId = request.EmployeeAddressDto.NeighborhoodOrVillageId;
                newEmployeeAddress.NeighborhoodOrVillageName = neighborhoodOrVillageName;
                newEmployeeAddress.StreetName = streetName;
                newEmployeeAddress.StreetId = request.EmployeeAddressDto.StreetId;
                await _unitOfWork.EmployeeAddresses.AddAsync(newEmployeeAddress);
            }
            else
            {
                var newEmployeeAddress = _mapper.Map<EmployeeAddress>(request.EmployeeAddressDto);
                newEmployeeAddress.DefaultAddress = false;
                newEmployeeAddress.CreatedByName = createdByName;
                newEmployeeAddress.ModifiedByName = createdByName;
                newEmployeeAddress.CreatedTime = DateTime.Now;
                newEmployeeAddress.ModifiedTime = DateTime.Now;
                newEmployeeAddress.IsActive = true;
                newEmployeeAddress.IsDeleted = false;
                newEmployeeAddress.CityId = request.EmployeeAddressDto.CityId;
                newEmployeeAddress.CityName = cityName;
                newEmployeeAddress.DistrictId = request.EmployeeAddressDto.DistrictId;
                newEmployeeAddress.DistrictName = districtName;
                newEmployeeAddress.NeighborhoodOrVillageId = request.EmployeeAddressDto.NeighborhoodOrVillageId;
                newEmployeeAddress.NeighborhoodOrVillageName = neighborhoodOrVillageName;
                newEmployeeAddress.StreetName = streetName;
                newEmployeeAddress.StreetId = request.EmployeeAddressDto.StreetId;
                await _unitOfWork.EmployeeAddresses.AddAsync(newEmployeeAddress);
            }
        }
        else
        {
            if (request.EmployeeAddressDto.DefaultAddress)
            {
                var newEmployeeAddress = _mapper.Map<EmployeeAddress>(request.EmployeeAddressDto);
                newEmployeeAddress.DefaultAddress = true;
                newEmployeeAddress.CreatedByName = createdByName;
                newEmployeeAddress.ModifiedByName = createdByName;
                newEmployeeAddress.CreatedTime = DateTime.Now;
                newEmployeeAddress.ModifiedTime = DateTime.Now;
                newEmployeeAddress.IsActive = true;
                newEmployeeAddress.IsDeleted = false;
                newEmployeeAddress.CityId = request.EmployeeAddressDto.CityId;
                newEmployeeAddress.CityName = cityName;
                newEmployeeAddress.DistrictId = request.EmployeeAddressDto.DistrictId;
                newEmployeeAddress.DistrictName = districtName;
                newEmployeeAddress.NeighborhoodOrVillageId = request.EmployeeAddressDto.NeighborhoodOrVillageId;
                newEmployeeAddress.NeighborhoodOrVillageName = neighborhoodOrVillageName;
                newEmployeeAddress.StreetName = streetName;
                newEmployeeAddress.StreetId = request.EmployeeAddressDto.StreetId;
                await _unitOfWork.EmployeeAddresses.AddAsync(newEmployeeAddress);
            }
            else
            {
                var newEmployeeAddress = _mapper.Map<EmployeeAddress>(request.EmployeeAddressDto);
                newEmployeeAddress.DefaultAddress = false;
                newEmployeeAddress.CreatedByName = createdByName;
                newEmployeeAddress.ModifiedByName = createdByName;
                newEmployeeAddress.CreatedTime = DateTime.Now;
                newEmployeeAddress.ModifiedTime = DateTime.Now;
                newEmployeeAddress.IsActive = true;
                newEmployeeAddress.IsDeleted = false;
                newEmployeeAddress.CityId = request.EmployeeAddressDto.CityId;
                newEmployeeAddress.CityName = cityName;
                newEmployeeAddress.DistrictId = request.EmployeeAddressDto.DistrictId;
                newEmployeeAddress.DistrictName = districtName;
                newEmployeeAddress.NeighborhoodOrVillageId = request.EmployeeAddressDto.NeighborhoodOrVillageId;
                newEmployeeAddress.NeighborhoodOrVillageName = neighborhoodOrVillageName;
                newEmployeeAddress.StreetName = streetName;
                newEmployeeAddress.StreetId = request.EmployeeAddressDto.StreetId;
                await _unitOfWork.EmployeeAddresses.AddAsync(newEmployeeAddress);
            }
        }
        int result = await _unitOfWork.SaveAsync();
        if (result > 0)
        {
            return new CreateEmployeeAddressCommandResponse
            {
                Result = new DataResult<EmployeeAddressDto>(ResultStatus.Success, Messages.EmployeeAddressAdded, request.EmployeeAddressDto)
            };
        }
        return new CreateEmployeeAddressCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.EmployeeAddressNotAdded)
        };
    }
}