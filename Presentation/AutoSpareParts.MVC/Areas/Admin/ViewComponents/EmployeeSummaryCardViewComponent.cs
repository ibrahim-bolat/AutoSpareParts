using AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeImageListByEmployeeIdQuery;
using AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeProfilImageByEmployeeIdQuery;
using AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeImageCountByEmployeeIdQuery;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoSpareParts.Application.Features.Employees.Queries.GetEmployeeSummaryByIdQuery;
using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Queries.GetEmployeeSummaryCardByIdQuery;


namespace AutoSpareParts.MVC.Areas.Admin.ViewComponents;

[ViewComponent]
public class EmployeeSummaryCardViewComponent : ViewComponent
{

    private readonly IMediator _mediator;

    public EmployeeSummaryCardViewComponent(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IViewComponentResult> InvokeAsync(int employeeId)
    {
        var countResult = await _mediator.Send(new GetEmployeeImageCountByEmployeeIdQueryRequest()
        {
            EmployeeId = employeeId
        });
        if (countResult.Result.ResultStatus == ResultStatus.Success)
            ViewBag.EmployeeImageCount = countResult.Result.Data;
        
        var profilResult = await _mediator.Send(new GetEmployeeProfilImageByEmployeeIdQueryRequest()
        {
            EmployeeId = employeeId
        });
        if (profilResult.Result.ResultStatus == ResultStatus.Success)
        {
            if (profilResult.Result.Data != null)
                ViewBag.EmployeeProfilImagePath = profilResult.Result.Data.Path;

            if (countResult.Result.Data > 0)
            {
                var allImageResult = await _mediator.Send(new GetEmployeeImageListByEmployeeIdQueryRequest()
                {
                    EmployeeId = employeeId
                });
                if (allImageResult.Result.ResultStatus == ResultStatus.Success)
                {
                    ViewBag.EmployeeImageList = allImageResult.Result.Data;
                }
            }
        }
        else
        {
            if (countResult.Result.Data > 0)
            {
                var allImageResult = await _mediator.Send(new GetEmployeeImageListByEmployeeIdQueryRequest()
                {
                    EmployeeId = employeeId
                });
                if (allImageResult.Result.ResultStatus == ResultStatus.Success)
                {
                    ViewBag.EmployeeImageList = allImageResult.Result.Data;
                }
            }
        }
        var employeeCardSummaryResult = await _mediator.Send(new GetByIdForUserSummaryCardQueryRequest()
        {
            Id = employeeId
        });
        return View(employeeCardSummaryResult.Result.Data);
    }
}