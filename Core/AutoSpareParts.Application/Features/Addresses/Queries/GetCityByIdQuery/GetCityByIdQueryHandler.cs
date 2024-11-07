using AutoMapper;
using AutoSpareParts.Application.Features.Addresses.Constants;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSpareParts.Application.Features.Addresses.Queries.GetCityByIdQuery;

public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQueryRequest, GetCityByIdQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCityByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetCityByIdQueryResponse> Handle(GetCityByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var city = await _unitOfWork.Cities.GetAsync(predicate: city => city.Id == request.Id);
        if (city != null)
        {
            var response = new SelectListItem()
            {
                Value = city.Id.ToString(),
                Text = city.Name
            };
            return new GetCityByIdQueryResponse
            {
                Result = new DataResult<SelectListItem>(ResultStatus.Success, response)
            };
        }
        return new GetCityByIdQueryResponse
        {
            Result = new DataResult<SelectListItem>(ResultStatus.Error, Messages.CityNotFound, null)
        };
    }
}