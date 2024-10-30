using AutoMapper;
using AutoSpareParts.Application.Features.MainCategories.Constants;
using AutoSpareParts.Application.Features.MainCategories.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Enums;
using MediatR;

namespace AutoSpareParts.Application.Features.MainCategories.Queries.GetByIdMainCategoryQuery;

public class GetByIdMainCategoryQueryHandler:IRequestHandler<GetByIdMainCategoryQueryRequest,GetByIdMainCategoryQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdMainCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetByIdMainCategoryQueryResponse> Handle(GetByIdMainCategoryQueryRequest request, CancellationToken cancellationToken)
    {
        var mainCategory = await _unitOfWork.MainCategories.GetByIdAsync(request.Id);
        if (mainCategory != null)
        {
            MainCategoryListDto mainCategoryListDto = _mapper.Map<MainCategoryListDto>(mainCategory);
            return new GetByIdMainCategoryQueryResponse
            {
                Result = new DataResult<MainCategoryListDto>(ResultStatus.Success, mainCategoryListDto)
            };
        }
        return new GetByIdMainCategoryQueryResponse
        {
            Result = new DataResult<MainCategoryListDto>(ResultStatus.Error, Messages.MainCategoryNotFound,null)
        };
    }
}