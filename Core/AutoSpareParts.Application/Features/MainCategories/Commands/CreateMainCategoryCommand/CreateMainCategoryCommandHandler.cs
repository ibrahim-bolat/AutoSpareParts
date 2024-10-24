using AutoMapper;
using AutoSpareParts.Application.Features.MainCategories.Commands.CreateMainCategoryCommand;
using AutoSpareParts.Application.Features.MainCategories.Constants;
using AutoSpareParts.Application.Features.MainCategories.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutoSpareParts.Application.Features.MainCategories.Commands.CreateMainCategoryCommand;

public class CreateMainCategoryCommandHandler : IRequestHandler<CreateMainCategoryCommandRequest, CreateMainCategoryCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateMainCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CreateMainCategoryCommandResponse> Handle(CreateMainCategoryCommandRequest request,
        CancellationToken cancellationToken)
    {
        string createdByName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        var newMainCategory = _mapper.Map<MainCategory>(request.MainCategoryDto);
        newMainCategory.CreatedByName = createdByName;
        newMainCategory.ModifiedByName = createdByName;
        newMainCategory.CreatedTime = DateTime.Now;
        newMainCategory.ModifiedTime = DateTime.Now;
        newMainCategory.IsActive = true;
        newMainCategory.IsDeleted = false;
        await _unitOfWork.MainCategories.AddAsync(newMainCategory);
        int result = await _unitOfWork.SaveAsync();
        if (result > 0)
        {
            return new CreateMainCategoryCommandResponse
            {
                Result = new DataResult<MainCategoryDto>(ResultStatus.Success, Messages.MainCategoryAdded, request.MainCategoryDto)
            };
        }
        return new CreateMainCategoryCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.MainCategoryNotAdded)
        };
    }
}