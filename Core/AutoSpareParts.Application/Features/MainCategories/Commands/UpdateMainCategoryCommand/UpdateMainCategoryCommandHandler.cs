using AutoMapper;
using AutoSpareParts.Application.Features.MainCategories.Commands.UpdateMainCategoryCommand;
using AutoSpareParts.Application.Features.MainCategories.DTOs;
using AutoSpareParts.Application.Features.MainCategories.Constants;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutoSpareParts.Application.Features.MainCategories.Commands.UpdateMainCategoryCommand;

public class UpdateMainCategoryCommandHandler : IRequestHandler<UpdateMainCategoryCommandRequest, UpdateMainCategoryCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public UpdateMainCategoryCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<UpdateMainCategoryCommandResponse> Handle(UpdateMainCategoryCommandRequest request,
        CancellationToken cancellationToken)
    {
        MainCategory mainCategory = await _unitOfWork.MainCategories.GetByIdAsync(request.MainCategoryListDto.Id);
        if (mainCategory != null)
        {
            mainCategory = _mapper.Map(request.MainCategoryListDto, mainCategory);
            mainCategory.ModifiedTime = DateTime.Now;
            mainCategory.ModifiedByName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
            await _unitOfWork.MainCategories.UpdateAsync(mainCategory);
            int result = await _unitOfWork.SaveAsync();
            if (result > 0)
            {
                return new UpdateMainCategoryCommandResponse
                {
                    Result = new DataResult<MainCategoryListDto>(ResultStatus.Success, Messages.MainCategoryUpdated, request.MainCategoryListDto)
                };
            }
            return new UpdateMainCategoryCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.MainCategoryNotUpdated)
            };
        }
        return new UpdateMainCategoryCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.MainCategoryNotUpdated)
        };
    }
}