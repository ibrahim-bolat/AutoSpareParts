using AutoSpareParts.Application.Features.IpOperations.Constants;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Endpoint = AutoSpareParts.Domain.Entities.Endpoint;

namespace AutoSpareParts.Application.Features.AuthorizeEndpoints.Commands.AssignIpListAuthorizeEndpointsCommand;

public class AssignIpListAuthorizeEndpointsCommandHandler : IRequestHandler<
    AssignIpListAuthorizeEndpointsCommandRequest, AssignIpListAuthorizeEndpointsCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignIpListAuthorizeEndpointsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AssignIpListAuthorizeEndpointsCommandResponse> Handle(
        AssignIpListAuthorizeEndpointsCommandRequest request, CancellationToken cancellationToken)
    {
        List<Endpoint> endpointList = null;
        List<IpAddress> allActiveIpAddresses =
            await _unitOfWork.IpAddresses.GetAllAsync(predicate:ip => ip.IsActive);
        if (request.IpAreaName != null)
        {
            if (request.IpMenuName == null)
            {
                endpointList = await _unitOfWork.Endpoints.GetAllAsync(predicate:e => 
                    e.AreaName == request.IpAreaName, include:e => e.Include(endpoint=>endpoint.IpAddresses));
            }
            else
            {
                endpointList = await _unitOfWork.Endpoints.GetAllAsync(
                    predicate:e => e.AreaName == request.IpAreaName && e.ControllerName == request.IpMenuName,
                    include:e => e.Include(endpoint=>endpoint.IpAddresses));
            }

            if (endpointList != null)
            {
                foreach (var endpoint in endpointList)
                {
                    endpoint.IpAddresses.RemoveAll(ip => true);
                    if (request.IpIds.Count() != 0)
                        endpoint.IpAddresses.AddRange(allActiveIpAddresses.Where(ip => request.IpIds.Contains(ip.Id)));
                }

                await _unitOfWork.SaveAsync();
                return new AssignIpListAuthorizeEndpointsCommandResponse
                {
                    Result = new Result(ResultStatus.Success, Messages.IpUpdated)
                };
            }
            return new AssignIpListAuthorizeEndpointsCommandResponse
            {
                Result = new Result(ResultStatus.Error, AuthorizeEndpoints.Constants.Messages.EndpointNotFound)
            };
        }

        if (request.IpAreaName == null && request.IpMenuName == null && request.EndpointId > 0)
        {
            Endpoint endpoint = await _unitOfWork.Endpoints
                .GetAsync(predicate:e => e.Id == request.EndpointId, include:e => e.Include(endpoint=>endpoint.IpAddresses));
            endpoint.IpAddresses.RemoveAll(ip => true);
            if (request.IpIds.Count() != 0)
                endpoint.IpAddresses.AddRange(allActiveIpAddresses.Where(ip => request.IpIds.Contains(ip.Id)));

            await _unitOfWork.SaveAsync();
            return new AssignIpListAuthorizeEndpointsCommandResponse
            {
                Result = new Result(ResultStatus.Success, Messages.IpUpdated)
            };
        }

        return new AssignIpListAuthorizeEndpointsCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.IpNotUpdated)
        };
    }
}