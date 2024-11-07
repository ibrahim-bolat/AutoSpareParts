using AutoSpareParts.Application.Features.EmployeeImages.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeProfilImageByEmployeeIdQuery;

public class GetEmployeeProfilImageByEmployeeIdQueryResponse
{
    public IDataResult<EmployeeImageDto> Result { get; set; }
}