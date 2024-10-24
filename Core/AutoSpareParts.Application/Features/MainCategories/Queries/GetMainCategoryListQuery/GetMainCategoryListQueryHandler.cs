using AutoMapper;
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.MainCategories.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Application.Features.MainCategories.Queries.GetMainCategoryListQuery;

public class GetMainCategoryListQueryHandler : IRequestHandler<GetMainCategoryListQueryRequest, GetMainCategoryListQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMainCategoryListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<GetMainCategoryListQueryResponse> Handle(GetMainCategoryListQueryRequest request, CancellationToken cancellationToken)
    {
        var mainCategoryData = _mapper.ProjectTo<MainCategoryListDto>(await _unitOfWork.MainCategories.GetAllQueryableAsync()).AsQueryable();
        int pageSize = request.DatatableRequestDto.Length == -1 ? mainCategoryData.Count() : request.DatatableRequestDto.Length;
        int skip = request.DatatableRequestDto.Start;
        var sortColumn = request.DatatableRequestDto.Columns[request.DatatableRequestDto.Order.FirstOrDefault()!.Column].Data;
        var sortColumnDirection = request.DatatableRequestDto.Order.FirstOrDefault()!.Dir.ToString();
        if (!string.IsNullOrEmpty(request.DatatableRequestDto.Search.Value))
        {
            mainCategoryData = mainCategoryData.Where(m => m.Name.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower()));
        }
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            //Func<AppRole, string> orderingFunction = (c => sortColumn  == nameof(c.Name) ? c.Name : c.Id.ToString());
            if (sortColumnDirection == OrderDirType.Desc.ToString())
            {
                mainCategoryData = mainCategoryData.OrderByDescending(c => c.Status).ThenByDescending(c => sortColumn == nameof(c.Name) ? c.Name : c.Id.ToString()).AsQueryable();
            }
            else
            {
                mainCategoryData = mainCategoryData.OrderByDescending(c => c.Status).ThenBy(c => sortColumn == nameof(c.Name) ? c.Name : c.Id.ToString()).AsQueryable();
            }
        }
        int recordsTotal = mainCategoryData.Count();
        var data = await mainCategoryData.Skip(skip).Take(pageSize).ToListAsync();
        List<MainCategoryListDto> roleList = _mapper.Map<List<MainCategoryListDto>>(data);
        var response = new DatatableResponseDto<MainCategoryListDto>
        {
            Draw = request.DatatableRequestDto.Draw,
            RecordsTotal = recordsTotal,
            RecordsFiltered = recordsTotal,
            Data = roleList
        };
        return new GetMainCategoryListQueryResponse
        {
            Result = new DataResult<DatatableResponseDto<MainCategoryListDto>>(ResultStatus.Success, response)
        };
    }
}