using AutoMapper;
using AutoSpareParts.Application.Features.EmployeeAddresses.Constants;
using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities.NotDerivedFromBase;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Commands.UpdateEmployeeAddressCommand;

public class UpdateEmployeeAddressCommandHandler:IRequestHandler<UpdateEmployeeAddressCommandRequest, UpdateEmployeeAddressCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateEmployeeAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UpdateEmployeeAddressCommandResponse> Handle(UpdateEmployeeAddressCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.EmployeeAddressDto.DefaultAddress)
        {
            var employeeAddresses = await _unitOfWork.EmployeeAddresses.GetAllAsync(predicate:a=>a.EmployeeId==request.EmployeeAddressDto.EmployeeId);
            for (int i = 0; i < employeeAddresses.Count; i++)
            {
                if (employeeAddresses[i].Id != request.EmployeeAddressDto.Id)
                {
                    employeeAddresses[i].DefaultAddress = false;
                    employeeAddresses[i].ModifiedByName = request.ModifiedByName;
                    employeeAddresses[i].ModifiedTime = DateTime.Now;
                    await _unitOfWork.EmployeeAddresses.UpdateAsync(employeeAddresses[i]);
                }
                else
                {
                    GetNames(request.EmployeeAddressDto, out string cityName,out string districtName,out string neighborhoodOrVillageName,out string streetName);
                    employeeAddresses[i] = _mapper.Map(request.EmployeeAddressDto, employeeAddresses[i]);
                    employeeAddresses[i].DefaultAddress = true;
                    employeeAddresses[i].ModifiedByName = request.ModifiedByName;
                    employeeAddresses[i].ModifiedTime = DateTime.Now;
                    employeeAddresses[i].CityId = request.EmployeeAddressDto.CityId;
                    employeeAddresses[i].CityName = cityName;    
                    employeeAddresses[i].DistrictId = request.EmployeeAddressDto.DistrictId;
                    employeeAddresses[i].DistrictName = districtName;            
                    employeeAddresses[i].NeighborhoodOrVillageId = request.EmployeeAddressDto.NeighborhoodOrVillageId;
                    employeeAddresses[i].NeighborhoodOrVillageName = neighborhoodOrVillageName;                    
                    employeeAddresses[i].StreetId = request.EmployeeAddressDto.StreetId;
                    employeeAddresses[i].StreetName = streetName;
                    await _unitOfWork.EmployeeAddresses.UpdateAsync(employeeAddresses[i]);
                }
            }
        }
        else
        {
            var employeeAddress = await _unitOfWork.EmployeeAddresses.GetAsync(predicate:x => x.Id == request.EmployeeAddressDto.Id);
            if (employeeAddress != null)
            {
                GetNames(request.EmployeeAddressDto, out string cityName,out string districtName,out string neighborhoodOrVillageName,out string streetName);
                employeeAddress = _mapper.Map(request.EmployeeAddressDto, employeeAddress);
                employeeAddress.DefaultAddress = false;
                employeeAddress.ModifiedByName = request.ModifiedByName;
                employeeAddress.ModifiedTime = DateTime.Now;
                employeeAddress.CityId = request.EmployeeAddressDto.CityId;
                employeeAddress.CityName = cityName;    
                employeeAddress.DistrictId = request.EmployeeAddressDto.DistrictId;
                employeeAddress.DistrictName = districtName;            
                employeeAddress.NeighborhoodOrVillageId = request.EmployeeAddressDto.NeighborhoodOrVillageId;
                employeeAddress.NeighborhoodOrVillageName = neighborhoodOrVillageName;                    
                employeeAddress.StreetId = request.EmployeeAddressDto.StreetId;
                employeeAddress.StreetName = streetName;
                await _unitOfWork.EmployeeAddresses.UpdateAsync(employeeAddress);
            }
        }
        var result = await _unitOfWork.SaveAsync();
        if (result > 0)
        {
            return new UpdateEmployeeAddressCommandResponse
            {
                Result = new DataResult<EmployeeAddressDto>(ResultStatus.Success, Messages.EmployeeAddressUpdated, request.EmployeeAddressDto)
            };
        }
        return new UpdateEmployeeAddressCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.EmployeeAddressNotUpdated)
        };
    }

    private void GetNames(EmployeeAddressDto employeeAddressDto, out string cityName,out string districtName,out string neighborhoodOrVillageName,out string streetName)
    {
        int cityId = Convert.ToInt32(employeeAddressDto.CityId);
        int districtId = Convert.ToInt32(employeeAddressDto.DistrictId);
        int neighborhoodorvillageId = Convert.ToInt32(employeeAddressDto.NeighborhoodOrVillageId);
        City city = null;
        if (int.TryParse(employeeAddressDto.StreetId, out int streetId))
        {
            city =  _unitOfWork.Cities.GetAsync(predicate:c => c.Id == cityId,
                include: c => c.Include(city=>city.Districts.Where(d=>d.Id==districtId))
                    .ThenInclude(district=>district.NeighborhoodsOrVillages.Where(n=>n.Id==neighborhoodorvillageId))
                    .ThenInclude(neighborhoodorvillage=>neighborhoodorvillage.Streets.Where(s=>s.Id==streetId))).Result;
            cityName = city.Name;
            districtName = city.Districts.FirstOrDefault()?.Name;
            neighborhoodOrVillageName = city.Districts.FirstOrDefault()?.NeighborhoodsOrVillages.FirstOrDefault()?.Name;
            streetName = city.Districts.FirstOrDefault()?.NeighborhoodsOrVillages.FirstOrDefault()
                ?.Streets.FirstOrDefault()?.Name;
        }
        else
        {
            city =  _unitOfWork.Cities.GetAsync(predicate:c => c.Id == cityId,
                include: c => c.Include(city => city.Districts.Where(d => d.Id == districtId))
                    .ThenInclude(district => district.NeighborhoodsOrVillages.Where(n => n.Id == neighborhoodorvillageId))).Result;
            cityName = city.Name;
            districtName = city.Districts.FirstOrDefault()?.Name;
            neighborhoodOrVillageName = city.Districts.FirstOrDefault()?.NeighborhoodsOrVillages.FirstOrDefault()?.Name;
            streetName = null;
        }
    }
}