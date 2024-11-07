using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeImageCountByEmployeeIdQuery;

public class GetEmployeeImageCountByEmployeeIdQueryResponse
{
    public IDataResult<int> Result { get; set; }
}