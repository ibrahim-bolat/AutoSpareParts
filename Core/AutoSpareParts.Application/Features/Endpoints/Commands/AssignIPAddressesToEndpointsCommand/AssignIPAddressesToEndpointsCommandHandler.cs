using AutoSpareParts.Application.Features.Endpoints.Constants;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Endpoint = AutoSpareParts.Domain.Entities.Endpoint;

namespace AutoSpareParts.Application.Features.Endpoints.Commands.AssignIPAddressesToEndpointsCommand;

public class AssignIpAddressesToEndpointsCommandHandler : IRequestHandler<
    AssignIpAddressesToEndpointsCommandRequest, AssignIpAddressesToEndpointsCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignIpAddressesToEndpointsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AssignIpAddressesToEndpointsCommandResponse> Handle(
        AssignIpAddressesToEndpointsCommandRequest request, CancellationToken cancellationToken)
    {
        List<Endpoint> endpointList = null;
        List<IPAddress> allActiveIPAddresses = await _unitOfWork.IPAddresses.GetAllAsync(predicate:ip => ip.IsActive);
        if (request.IPAreaName != null)
        {
            if (request.IPMenuName == null)
            {
                endpointList = await _unitOfWork.Endpoints.GetAllAsync(predicate:e => 
                    e.AreaName == request.IPAreaName, include:e => e.Include(endpoint=>endpoint.IPAddresses));
            }
            else
            {
                endpointList = await _unitOfWork.Endpoints.GetAllAsync(
                    predicate:e => e.AreaName == request.IPAreaName && e.ControllerName == request.IPMenuName,
                    include:e => e.Include(endpoint=>endpoint.IPAddresses));
            }

            if (endpointList != null)
            {
                foreach (var endpoint in endpointList)
                {
                    endpoint.IPAddresses.RemoveAll(ip => true);
                    if (request.IPIds.Count() != 0)
                        endpoint.IPAddresses.AddRange(allActiveIPAddresses.Where(ip => request.IPIds.Contains(ip.Id)));
                }

                await _unitOfWork.SaveAsync();
                return new AssignIpAddressesToEndpointsCommandResponse
                {
                    Result = new Result(ResultStatus.Success, Messages.IPUpdated)
                };
            }
            return new AssignIpAddressesToEndpointsCommandResponse
            {
                Result = new Result(ResultStatus.Error, Endpoints.Constants.Messages.EndpointNotFound)
            };
        }

        if (request.IPAreaName == null && request.IPMenuName == null && request.EndpointId > 0)
        {
            Endpoint endpoint = await _unitOfWork.Endpoints.GetAsync(predicate:e => e.Id == request.EndpointId, include:e => e.Include(endpoint=>endpoint.IPAddresses));
            endpoint.IPAddresses.RemoveAll(ip => true);
            if (request.IPIds.Count() != 0)
                endpoint.IPAddresses.AddRange(allActiveIPAddresses.Where(ip => request.IPIds.Contains(ip.Id)));

            await _unitOfWork.SaveAsync();
            return new AssignIpAddressesToEndpointsCommandResponse
            {
                Result = new Result(ResultStatus.Success, Messages.IPUpdated)
            };
        }

        return new AssignIpAddressesToEndpointsCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.IPNotUpdated)
        };
    }
}