using AutoSpareParts.Application.Features.MainCategories.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.MainCategories.Commands.CreateMainCategoryCommand;

public class CreateMainCategoryCommandRequest : IRequest<CreateMainCategoryCommandResponse>
{
    public MainCategoryDto MainCategoryDto { get; set; }
}