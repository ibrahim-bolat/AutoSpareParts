using AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeImageListByEmployeeIdQuery;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoSpareParts.MVC.Areas.Admin.ViewComponents;

[ViewComponent]
public class EmployeeImageCardViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public EmployeeImageCardViewComponent(IMediator mediator)
        {
             _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int employeeId)
        {
            if (employeeId > 0)
            {
                var dresult = await _mediator.Send(new GetEmployeeImageListByEmployeeIdQueryRequest()
                {
                    EmployeeId = employeeId,
                });
                if (dresult.Result.ResultStatus == ResultStatus.Success)
                {
                    ViewBag.EmployeeId = employeeId;
                    return View(dresult.Result.Data.OrderBy(i=>i.Id).ToList());
                }
            }
            return View();
        }
    }
