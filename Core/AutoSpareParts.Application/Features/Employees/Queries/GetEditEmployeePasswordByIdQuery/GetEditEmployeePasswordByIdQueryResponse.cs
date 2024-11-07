using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.Employees.Queries.GetEditEmployeePasswordByIdQuery;

public class GetEditEmployeePasswordByIdQueryResponse
{
    public IDataResult<EditEmployeePasswordDto> Result { get; set; }
}