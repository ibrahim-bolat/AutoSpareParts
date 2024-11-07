using AutoSpareParts.Application.Features.EmployeeImages.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeImageListByEmployeeIdQuery;

public class GetEmployeeImageListByEmployeeIdQueryResponse
{
    public IDataResult<IList<EmployeeImageDto>> Result { get; set; }
}