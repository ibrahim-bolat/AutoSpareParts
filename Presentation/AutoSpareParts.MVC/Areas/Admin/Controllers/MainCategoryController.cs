
using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.CustomAttributes;
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.IpOperations.Queries.GetByIdIpAddressQuery;
using AutoSpareParts.Application.Features.MainCategories.Commands.CreateMainCategoryCommand;
using AutoSpareParts.Application.Features.MainCategories.Commands.UpdateMainCategoryCommand;
using AutoSpareParts.Application.Features.MainCategories.DTOs;
using AutoSpareParts.Application.Features.MainCategories.Queries.GetByIdMainCategoryQuery;
using AutoSpareParts.Application.Features.MainCategories.Queries.GetMainCategoryListQuery;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Messages = AutoSpareParts.Application.Features.MainCategories.Constants.Messages;

namespace AutoSpareParts.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MainCategoryController : Controller
    {

        private readonly IMediator _mediator;

        public MainCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAllMainCategories(DatatableRequestDto datatableRequestDto)
        {
            var dresult = await _mediator.Send(new GetMainCategoryListQueryRequest()
            {
                DatatableRequestDto = datatableRequestDto
            });

            var jsonData = new
            {
                draw = dresult.Result.Data.Draw,
                recordsFiltered = dresult.Result.Data.RecordsFiltered,
                recordsTotal = dresult.Result.Data.RecordsTotal,
                data = dresult.Result.Data.Data,
                isSusccess = true
            };
            return Ok(jsonData);
        }

        [HttpPost]
        [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.MainCategory, EndpointType = EndpointType.Writing, Definition = "Create MainCategory")]
        public async Task<IActionResult> CreateMainCategory(MainCategoryDto mainCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("PartialViews/_CreateMainCategoryModalPartial", mainCategoryDto);
            }

            var dresult = await _mediator.Send(new CreateMainCategoryCommandRequest()
            {
                MainCategoryDto = mainCategoryDto
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.MainCategory, EndpointType = EndpointType.Updating, Definition = "Update MainCategory")]
        public async Task<IActionResult> UpdateMainCategory(MainCategoryListDto mainCategoryListDto)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("PartialViews/_UpdateMainCategoryModalPartial", mainCategoryListDto);
            }

            var dresult = await _mediator.Send(new UpdateMainCategoryCommandRequest()
            {
                MainCategoryListDto = mainCategoryListDto
            });
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.MainCategoryNotFound))
            {
                ModelState.AddModelError("MainCategoryNotFound", Messages.MainCategoryNotFound);
                return PartialView("PartialViews/_UpdateMainCategoryModalPartial", mainCategoryListDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpGet]
        [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.MainCategory, EndpointType = EndpointType.Reading, Definition = "Get By Id MainCategory Details")]
        public async Task<IActionResult> GetMainCategoryById(int id)
        {
            var dresult = await _mediator.Send(new GetByIdMainCategoryQueryRequest()
            {
                Id = id
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true, mainCategory = dresult.Result.Data });
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.MainCategoryNotFound))
            {
                ModelState.AddModelError("MainCategoryNotFound", Messages.MainCategoryNotFound);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }

            return Json(new { success = false });
        }
    }
}
