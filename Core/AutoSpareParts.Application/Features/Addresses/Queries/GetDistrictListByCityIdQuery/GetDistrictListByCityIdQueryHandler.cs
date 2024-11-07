using AutoMapper;
using AutoSpareParts.Application.Features.Addresses.Constants;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetDistrictListByCityIdQuery;

public class GetDistrictListByCityIdQueryHandler : IRequestHandler<GetDistrictListByCityIdQueryRequest, GetDistrictListByCityIdQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDistrictListByCityIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetDistrictListByCityIdQueryResponse> Handle(GetDistrictListByCityIdQueryRequest request, CancellationToken cancellationToken)
    {
        var districtList = await _unitOfWork.Districts.GetAllAsync(predicate: district => district.CityId == request.CityId);
        if (districtList != null)
        {
            var response = districtList.Select(district => new SelectListItem()
            {
                Value = district.Id.ToString(),
                Text = district.Name
            }).ToList();
            return new GetDistrictListByCityIdQueryResponse
            {
                Result = new DataResult<List<SelectListItem>>(ResultStatus.Success, response)
            };
        }
        return new GetDistrictListByCityIdQueryResponse
        {
            Result = new DataResult<List<SelectListItem>>(ResultStatus.Error, Messages.DistrictNotFound, null)
        };
    }
}