using AutoMapper;
using AutoSpareParts.Application.Features.Addresses.Constants;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetNeighborhoodOrVillageListByDistrictIdQuery;

public class GetNeighborhoodOrVillageListByDistrictIdQueryHandler : IRequestHandler<GetNeighborhoodOrVillageListByDistrictIdQueryRequest, GetNeighborhoodOrVillageListByDistrictIdQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetNeighborhoodOrVillageListByDistrictIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetNeighborhoodOrVillageListByDistrictIdQueryResponse> Handle(GetNeighborhoodOrVillageListByDistrictIdQueryRequest request, CancellationToken cancellationToken)
    {
        var neighborhoodorvillageList = await _unitOfWork.NeighborhoodOrVillages.GetAllAsync(predicate: neighborhoodOrVillage => neighborhoodOrVillage.DistrictId == request.DistrictId);
        if (neighborhoodorvillageList != null)
        {
            var response = neighborhoodorvillageList.Select(neighborhoodorvillage => new SelectListItem()
            {
                Value = neighborhoodorvillage.Id.ToString(),
                Text = neighborhoodorvillage.Name
            }).ToList();
            return new GetNeighborhoodOrVillageListByDistrictIdQueryResponse
            {
                Result = new DataResult<List<SelectListItem>>(ResultStatus.Success, response)
            };
        }
        return new GetNeighborhoodOrVillageListByDistrictIdQueryResponse
        {
            Result = new DataResult<List<SelectListItem>>(ResultStatus.Error, Messages.NeighborhoodOrVillageNotFound, null)
        };
    }
}