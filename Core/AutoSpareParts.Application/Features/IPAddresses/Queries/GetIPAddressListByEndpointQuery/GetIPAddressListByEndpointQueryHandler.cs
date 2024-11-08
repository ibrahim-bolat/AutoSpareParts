using AutoMapper;
using AutoSpareParts.Application.Extensions;
using AutoSpareParts.Application.Features.IPAddresses.Constants;
using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAddressListByEndpointQuery;

public class GetIPAddressListByEndpointQueryHandler : IRequestHandler<GetIPAddressListByEndpointQueryRequest,
    GetIPAddressListByEndpointQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetIPAddressListByEndpointQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetIPAddressListByEndpointQueryResponse> Handle(GetIPAddressListByEndpointQueryRequest request,
        CancellationToken cancellationToken)
    {
        List<Endpoint> endpointList = null;
        if (request.AreaName != null && request.EndpointId == 0)
        {
            if (request.MenuName == null)
            {
                endpointList = await _unitOfWork.Endpoints.GetAllAsync(predicate: a =>
                    a.AreaName == request.AreaName, include: e => e.Include(endpoint => endpoint.IPAddresses));
            }
            else
            {
                endpointList = await _unitOfWork.Endpoints.GetAllAsync(predicate: a =>
                    a.AreaName == request.AreaName && a.ControllerName == request.MenuName, include: e => e.Include(endpoint => endpoint.IPAddresses));
            }
            if (endpointList != null)
            {
                List<IPAddress> allActiveIpAddresses = await _unitOfWork.IPAddresses.GetAllAsync(predicate: ip => ip.IsActive);
                HashSet<AssignIPAddressDto> assignIpAddress = new HashSet<AssignIPAddressDto>();
                foreach (var activeIpAddress in allActiveIpAddresses)
                {
                    assignIpAddress.Add(new AssignIPAddressDto
                    {
                        Id = activeIpAddress.Id,
                        RangeStart = activeIpAddress.RangeStart,
                        RangeEnd = activeIpAddress.RangeEnd,
                        IPListType = activeIpAddress.IPListType.GetEnumDescription(),
                        TobeAssignedAreaName = request.AreaName,
                        TobeAssignedMenuName = request.MenuName,
                        TobeAssignedEndpointId = null,
                        HasAssign = endpointList.All(e => e.IPAddresses.Any(i => i.Id == activeIpAddress.Id))
                    });
                }

                return new GetIPAddressListByEndpointQueryResponse
                {
                    Result = new DataResult<HashSet<AssignIPAddressDto>>(ResultStatus.Success, assignIpAddress)
                };
            }

            return new GetIPAddressListByEndpointQueryResponse
            {
                Result = new DataResult<HashSet<AssignIPAddressDto>>(ResultStatus.Error, Messages.IPNotFound, null)
            };
        }

        if (request.AreaName == null && request.MenuName == null && request.EndpointId > 0)
        {
            Endpoint endpoint = await _unitOfWork.Endpoints.GetAsync(predicate: a => a.Id == request.EndpointId, include: e => e.Include(endpoint => endpoint.IPAddresses));
            if (endpoint != null)
            {
                List<IPAddress> allActiveIpAddresses = await _unitOfWork.IPAddresses.GetAllAsync(predicate: ip => ip.IsActive);
                HashSet<AssignIPAddressDto> assignIpAddress = new HashSet<AssignIPAddressDto>();
                List<IPAddress> endpointIpAddress = endpoint.IPAddresses;
                foreach (var activeIpAddress in allActiveIpAddresses)
                {
                    assignIpAddress.Add(new AssignIPAddressDto
                    {
                        Id = activeIpAddress.Id,
                        RangeStart = activeIpAddress.RangeStart,
                        RangeEnd = activeIpAddress.RangeEnd,
                        IPListType = activeIpAddress.IPListType.GetEnumDescription(),
                        TobeAssignedAreaName = null,
                        TobeAssignedMenuName = null,
                        TobeAssignedEndpointId = endpoint.Id.ToString(),
                        HasAssign = endpointIpAddress != null && endpointIpAddress.Any(e => e.Id == activeIpAddress.Id)
                    });
                }

                return new GetIPAddressListByEndpointQueryResponse
                {
                    Result = new DataResult<HashSet<AssignIPAddressDto>>(ResultStatus.Success, assignIpAddress)
                };
            }

            return new GetIPAddressListByEndpointQueryResponse
            {
                Result = new DataResult<HashSet<AssignIPAddressDto>>(ResultStatus.Error, Messages.IPNotFound, null)
            };
        }
        return new GetIPAddressListByEndpointQueryResponse
        {
            Result = new DataResult<HashSet<AssignIPAddressDto>>(ResultStatus.Error, Messages.EndpointNotFound, null)
        };
    }
}