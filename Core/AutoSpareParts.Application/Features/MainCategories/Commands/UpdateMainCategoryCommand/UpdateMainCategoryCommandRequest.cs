using AutoSpareParts.Application.Features.MainCategories.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.MainCategories.Commands.UpdateMainCategoryCommand;

public class UpdateMainCategoryCommandRequest : IRequest<UpdateMainCategoryCommandResponse>
{
    public MainCategoryListDto MainCategoryListDto{ get; set; }
}