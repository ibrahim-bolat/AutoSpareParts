using AutoMapper;
using AutoSpareParts.Application.Features.Addresses.Constants;
using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetStreetListQuery;

public class GetStreetListQueryHandler:IRequestHandler<GetStreetListQueryRequest,GetStreetListQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetStreetListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetStreetListQueryResponse> Handle(GetStreetListQueryRequest request, CancellationToken cancellationToken)
    {
        var streetList = await _unitOfWork.GetRepository<Street>().GetAllAsync(predicate:street=>street.NeighborhoodOrVillageId==request.NeighborhoodOrVillageId);
        if (streetList != null)
        {
            var response = streetList.Select(street => new SelectListItem()
            {
                Value = street.Id.ToString(),
                Text = street.Name
            }).ToList();
            return new GetStreetListQueryResponse{
                Result = new DataResult<List<SelectListItem>>(ResultStatus.Success, response)
            };
        }
        return new GetStreetListQueryResponse{
            Result = new DataResult<List<SelectListItem>>(ResultStatus.Error, Messages.StreetNotFound,null)
        };
    }
}