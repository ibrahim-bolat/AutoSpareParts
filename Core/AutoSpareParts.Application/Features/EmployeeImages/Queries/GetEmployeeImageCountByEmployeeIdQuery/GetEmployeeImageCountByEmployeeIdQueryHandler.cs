using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Enums;
using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeImageCountByEmployeeIdQuery;

public class GetEmployeeImageCountByEmployeeIdQueryHandler:IRequestHandler<GetEmployeeImageCountByEmployeeIdQueryRequest,GetEmployeeImageCountByEmployeeIdQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetEmployeeImageCountByEmployeeIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetEmployeeImageCountByEmployeeIdQueryResponse> Handle(GetEmployeeImageCountByEmployeeIdQueryRequest request, CancellationToken cancellationToken)
    {
        var count = await _unitOfWork.EmployeeImages.CountAsync(predicate:x => x.EmployeeId == request.EmployeeId && x.IsActive);
        return new GetEmployeeImageCountByEmployeeIdQueryResponse{
            Result = new DataResult<int>(ResultStatus.Success,count)
        };
    }
}