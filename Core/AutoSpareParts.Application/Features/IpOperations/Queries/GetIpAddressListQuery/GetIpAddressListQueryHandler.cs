using AutoMapper;
using AutoSpareParts.Application.DTOs.Common;
using AutoSpareParts.Application.Features.IpOperations.DTOs;
using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Application.Features.IpOperations.Queries.GetIpAddressListQuery;

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
        var ipData =
            _mapper.ProjectTo<IpListDto>(await _unitOfWork.GetRepository<IpAddress>().GetAllQueryableAsync()).AsQueryable();
        int pageSize = request.DatatableRequestDto.Length == -1 ? ipData.Count() :  request.DatatableRequestDto.Length;
        int skip =  request.DatatableRequestDto.Start;
        var sortColumn = request.DatatableRequestDto.Columns[request.DatatableRequestDto.Order.FirstOrDefault()!.Column].Data;
        var sortColumnDirection = request.DatatableRequestDto.Order.FirstOrDefault()!.Dir.ToString();
        if (!string.IsNullOrEmpty(request.DatatableRequestDto.Search.Value))
        {
            ipData = ipData.Where(m => m.RangeStart.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower())
                                       || m.RangeEnd.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower())
                                       || m.IpListType.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower()));
        }
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            //Func<AppRole, string> orderingFunction = (c => sortColumn  == nameof(c.Name) ? c.Name : c.Id.ToString());
            if (sortColumnDirection == OrderDirType.Desc.ToString())
            {
                ipData = ipData.OrderByDescending(c=>c.Status).ThenByDescending(c => sortColumn  == nameof(c.RangeStart) ? c.RangeStart : 
                    sortColumn  == nameof(c.RangeEnd) ? c.RangeEnd :
                    sortColumn  == nameof(c.IpListType) ? c.IpListType:c.Id.ToString()).AsQueryable();
            }
            else
            {
                ipData = ipData.OrderByDescending(c=>c.Status).ThenBy(c => sortColumn  == nameof(c.RangeStart) ? c.RangeStart : 
                    sortColumn  == nameof(c.RangeEnd) ? c.RangeEnd :
                    sortColumn  == nameof(c.IpListType) ? c.IpListType:c.Id.ToString()).AsQueryable();
            }
        }
        int recordsTotal = ipData.Count();
        var ipList = await ipData.Skip(skip).Take(pageSize).ToListAsync();
        var response = new DatatableResponseDto<IpListDto>
        {
            Draw = request.DatatableRequestDto.Draw,
            RecordsTotal = recordsTotal,
            RecordsFiltered = recordsTotal,
            Data = ipList
        };
        return new GetIpAddressListQueryResponse{
            Result = new DataResult<DatatableResponseDto<IpListDto>>(ResultStatus.Success, response)
        };
    }
}