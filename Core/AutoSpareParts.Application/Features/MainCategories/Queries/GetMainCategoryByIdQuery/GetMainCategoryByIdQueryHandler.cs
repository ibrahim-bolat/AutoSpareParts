using AutoMapper;
using AutoSpareParts.Application.Features.MainCategories.Constants;
using AutoSpareParts.Application.Features.MainCategories.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Enums;
using MediatR;

namespace AutoSpareParts.Application.Features.MainCategories.Queries.GetMainCategoryByIdQuery;

public class GetMainCategoryByIdQueryHandler:IRequestHandler<GetMainCategoryByIdQueryRequest,GetMainCategoryByIdQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMainCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetMainCategoryByIdQueryResponse> Handle(GetMainCategoryByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var mainCategory = await _unitOfWork.MainCategories.GetByIdAsync(request.Id);
        if (mainCategory != null)
        {
            MainCategoryListDto mainCategoryListDto = _mapper.Map<MainCategoryListDto>(mainCategory);
            return new GetMainCategoryByIdQueryResponse
            {
                Result = new DataResult<MainCategoryListDto>(ResultStatus.Success, mainCategoryListDto)
            };
        }
        return new GetMainCategoryByIdQueryResponse
        {
            Result = new DataResult<MainCategoryListDto>(ResultStatus.Error, Messages.MainCategoryNotFound,null)
        };
    }
}