using AutoSpareParts.Application.Features.Employees.Queries.GetEmployeeSummaryCardByIdQuery;
using AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeImageListByEmployeeIdQuery;
using AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeProfilImageByEmployeeIdQuery;
using AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeImageCountByEmployeeIdQuery;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace AutoSpareParts.MVC.Areas.Admin.ViewComponents;

[ViewComponent]
public class UserSummaryCardViewComponent : ViewComponent
{

    private readonly IMediator _mediator;

    public UserSummaryCardViewComponent(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IViewComponentResult> InvokeAsync(int userId)
    {
        var countResult = await _mediator.Send(new GetEmployeeImageCountByEmployeeIdQueryRequest()
        {
            EmployeeId = userId
        });
        if (countResult.Result.ResultStatus == ResultStatus.Success)
            ViewBag.UserImageCount = countResult.Result.Data;
        
        var profilResult = await _mediator.Send(new GetEmployeeProfilImageByEmployeeIdQueryRequest()
        {
            EmployeeId = userId
        });
        if (profilResult.Result.ResultStatus == ResultStatus.Success)
        {
            if (profilResult.Result.Data != null)
                ViewBag.UserImageProfilImagePath = profilResult.Result.Data.Path;

            if (countResult.Result.Data > 0)
            {
                var allImageResult = await _mediator.Send(new GetEmployeeImageListByEmployeeIdQueryRequest()
                {
                    EmployeeId = userId
                });
                if (allImageResult.Result.ResultStatus == ResultStatus.Success)
                {
                    ViewBag.UserImageList = allImageResult.Result.Data;
                }
            }
        }
        else
        {
            if (countResult.Result.Data > 0)
            {
                var allImageResult = await _mediator.Send(new GetEmployeeImageListByEmployeeIdQueryRequest()
                {
                    EmployeeId = userId
                });
                if (allImageResult.Result.ResultStatus == ResultStatus.Success)
                {
                    ViewBag.UserImageList = allImageResult.Result.Data;
                }
            }
        }
        var userCardSummaryResult = await _mediator.Send(new GetByIdForUserSummaryCardQueryRequest()
        {
            Id = userId
        });
        return View(userCardSummaryResult.Result.Data);
    }
}