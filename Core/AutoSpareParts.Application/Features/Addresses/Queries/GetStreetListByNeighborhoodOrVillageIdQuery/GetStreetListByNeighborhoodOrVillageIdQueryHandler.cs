using AutoMapper;
using AutoSpareParts.Application.Features.Addresses.Constants;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetStreetListByNeighborhoodOrVillageIdQuery;

public class GetStreetListByNeighborhoodOrVillageIdQueryHandler : IRequestHandler<GetStreetListByNeighborhoodOrVillageIdQueryRequest, GetStreetListByNeighborhoodOrVillageIdQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetStreetListByNeighborhoodOrVillageIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetStreetListByNeighborhoodOrVillageIdQueryResponse> Handle(GetStreetListByNeighborhoodOrVillageIdQueryRequest request, CancellationToken cancellationToken)
    {
        var streetList = await _unitOfWork.Streets.GetAllAsync(predicate: street => street.NeighborhoodOrVillageId == request.NeighborhoodOrVillageId);
        if (streetList != null)
        {
            var response = streetList.Select(street => new SelectListItem()
            {
                Value = street.Id.ToString(),
                Text = street.Name
            }).ToList();
            return new GetStreetListByNeighborhoodOrVillageIdQueryResponse
            {
                Result = new DataResult<List<SelectListItem>>(ResultStatus.Success, response)
            };
        }
        return new GetStreetListByNeighborhoodOrVillageIdQueryResponse
        {
            Result = new DataResult<List<SelectListItem>>(ResultStatus.Error, Messages.StreetNotFound, null)
        };
    }
}