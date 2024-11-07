using AutoSpareParts.Application.Features.EmployeeImages.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeImageListByUserNameQuery;

public class GetEmployeeImageListByUserNameQueryResponse
{
    public IDataResult<List<EmployeeImageDto>> Result { get; set; }

}