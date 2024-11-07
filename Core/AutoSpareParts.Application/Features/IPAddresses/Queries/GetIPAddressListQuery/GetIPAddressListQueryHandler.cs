using AutoMapper;
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAddressListQuery;

public class GetIpAddressListQueryHandler:IRequestHandler<GetIpAddressListQueryRequest,GetIpAddressListQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetIpAddressListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<GetIpAddressListQueryResponse> Handle(GetIpAddressListQueryRequest request, CancellationToken cancellationToken)
    {
        var IPData = _mapper.ProjectTo<IPListDto>(await _unitOfWork.IPAddresses.GetAllQueryableAsync()).AsQueryable();
        int pageSize = request.DatatableRequestDto.Length == -1 ? IPData.Count() :  request.DatatableRequestDto.Length;
        int skip =  request.DatatableRequestDto.Start;
        var sortColumn = request.DatatableRequestDto.Columns[request.DatatableRequestDto.Order.FirstOrDefault()!.Column].Data;
        var sortColumnDirection = request.DatatableRequestDto.Order.FirstOrDefault()!.Dir.ToString();
        if (!string.IsNullOrEmpty(request.DatatableRequestDto.Search.Value))
        {
            IPData = IPData.Where(m => m.RangeStart.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower())
                                       || m.RangeEnd.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower())
                                       || m.IPListType.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower()));
        }
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            //Func<AppRole, string> orderingFunction = (c => sortColumn  == nameof(c.Name) ? c.Name : c.EmloyeeId.ToString());
            if (sortColumnDirection == OrderDirType.Desc.ToString())
            {
                IPData = IPData.OrderByDescending(c=>c.Status).ThenByDescending(c => sortColumn  == nameof(c.RangeStart) ? c.RangeStart : 
                    sortColumn  == nameof(c.RangeEnd) ? c.RangeEnd :
                    sortColumn  == nameof(c.IPListType) ? c.IPListType:c.Id.ToString()).AsQueryable();
            }
            else
            {
                IPData = IPData.OrderByDescending(c=>c.Status).ThenBy(c => sortColumn  == nameof(c.RangeStart) ? c.RangeStart : 
                    sortColumn  == nameof(c.RangeEnd) ? c.RangeEnd :
                    sortColumn  == nameof(c.IPListType) ? c.IPListType:c.Id.ToString()).AsQueryable();
            }
        }
        int recordsTotal = IPData.Count();
        var IPList = await IPData.Skip(skip).Take(pageSize).ToListAsync();
        var response = new DatatableResponseDto<IPListDto>
        {
            Draw = request.DatatableRequestDto.Draw,
            RecordsTotal = recordsTotal,
            RecordsFiltered = recordsTotal,
            Data = IPList
        };
        return new GetIpAddressListQueryResponse{
            Result = new DataResult<DatatableResponseDto<IPListDto>>(ResultStatus.Success, response)
        };
    }
}