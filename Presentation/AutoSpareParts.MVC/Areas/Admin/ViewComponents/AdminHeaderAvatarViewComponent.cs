using AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeImageListByUserNameQuery;
using AutoSpareParts.Application.Features.Employees.Queries.GetEmployeeSummaryByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoSpareParts.MVC.Areas.Admin.ViewComponents;

[ViewComponent]
public class AdminHeaderAvatarViewComponent : ViewComponent
{
    private readonly IMediator _mediator;

    public AdminHeaderAvatarViewComponent(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var imageResult = await _mediator.Send(new GetEmployeeImageListByUserNameQueryRequest()
        {
            UserName = User.Identity?.Name,
        });
        ViewBag.EmployeeId = imageResult.Result.Data.Select(i => i.EmployeeId).FirstOrDefault();
        ViewBag.ProfilPhoto = "/admin/images/avatar/unspecifieduseravatar.png";
        var employeeResult = await _mediator.Send(new GetEmployeeSummaryByIdQueryRequest()
        {
            Id = ViewBag.EmployeeId.ToString()
        });
        ViewBag.FullName = employeeResult.Result.Data.FirstName + " " + employeeResult.Result.Data.LastName;
        if (imageResult.Result.Data.Count > 0)
        {
            foreach (var employeeImage in imageResult.Result.Data)
            {
                if (employeeImage.Profil)
                    ViewBag.ProfilPhoto = employeeImage.Path;
            }
        }
        return View();
    }
}
