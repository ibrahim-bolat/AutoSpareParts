using AutoSpareParts.Application.CustomAttributes;
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.MainCategories.Commands.CreateMainCategoryCommand;
using AutoSpareParts.Application.Features.MainCategories.Commands.UpdateMainCategoryCommand;
using AutoSpareParts.Application.Features.MainCategories.Queries.GetMainCategoryByIdQuery;
using AutoSpareParts.Application.Features.MainCategories.Queries.GetMainCategoryListQuery;
using AutoSpareParts.Application.Features.MainCategories.DTOs;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.Features.MainCategories.Constants;

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
        public async Task<IActionResult> GetMainCategoryList(DatatableRequestDto datatableRequestDto)
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
        [Endpoint(Menu = MenuDefinition.MainCategory, EndpointType = EndpointType.Writing, Decription = EndpointDecription.PostCreateMainCategory)]
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
        [Endpoint(Menu = MenuDefinition.MainCategory, EndpointType = EndpointType.Updating, Decription = EndpointDecription.PostUpdateMainCategory)]
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
                ModelState.AddModelError(nameof(Messages.MainCategoryNotFound), Messages.MainCategoryNotFound);
                return PartialView("PartialViews/_UpdateMainCategoryModalPartial", mainCategoryListDto);
            }

            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpGet]
        [Endpoint(Menu = MenuDefinition.MainCategory, EndpointType = EndpointType.Reading, Decription = EndpointDecription.GetMainCategoryById)]
        public async Task<IActionResult> GetMainCategoryById(int id)
        {
            var dresult = await _mediator.Send(new GetMainCategoryByIdQueryRequest()
            {
                Id = id
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true, mainCategory = dresult.Result.Data });
            }

            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.MainCategoryNotFound))
            {
                ModelState.AddModelError(nameof(Messages.MainCategoryNotFound), Messages.MainCategoryNotFound);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }

            return Json(new { success = false });
        }
    }
}
