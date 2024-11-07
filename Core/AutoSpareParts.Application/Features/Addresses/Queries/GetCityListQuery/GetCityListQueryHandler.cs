using AutoMapper;
using AutoSpareParts.Application.Features.Addresses.Constants;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetCityListQuery;

public class GetCityListQueryHandler : IRequestHandler<GetCityListQueryRequest, GetCityListQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCityListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetCityListQueryResponse> Handle(GetCityListQueryRequest request, CancellationToken cancellationToken)
    {
        var cityList = await _unitOfWork.Cities.GetAllAsync();
        if (cityList != null)
        {
            var response = cityList.Select(city => new SelectListItem()
            {
                Value = city.Id.ToString(),
                Text = city.Name
            }).ToList();
            return new GetCityListQueryResponse
            {
                Result = new DataResult<List<SelectListItem>>(ResultStatus.Success, response)
            };
        }
        return new GetCityListQueryResponse
        {
            Result = new DataResult<List<SelectListItem>>(ResultStatus.Error, Messages.CityNotFound, null)
        };
    }
}