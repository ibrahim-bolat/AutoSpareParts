using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using AutoSpareParts.Application.Features.Employees.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.IPAddresses.Queries.GetIPAddressByIdQuery
{
    public class GetIPAddressByIdQueryResponse
    {
        public IDataResult<IPDto> Result { get; set; }
    }
}