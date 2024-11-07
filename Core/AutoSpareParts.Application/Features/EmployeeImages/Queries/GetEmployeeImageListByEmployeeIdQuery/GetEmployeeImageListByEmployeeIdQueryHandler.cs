using AutoMapper;
using AutoSpareParts.Application.Features.EmployeeImages.Constants;
using AutoSpareParts.Application.Features.EmployeeImages.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Enums;
using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeImageListByEmployeeIdQuery;

public class GetEmployeeImageListByEmployeeIdQueryHandler:IRequestHandler<GetEmployeeImageListByEmployeeIdQueryRequest,GetEmployeeImageListByEmployeeIdQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEmployeeImageListByEmployeeIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetEmployeeImageListByEmployeeIdQueryResponse> Handle(GetEmployeeImageListByEmployeeIdQueryRequest request, CancellationToken cancellationToken)
    {
        var employeeImages = await _unitOfWork.EmployeeImages.GetAllAsync(predicate:ui=>ui.EmployeeId==request.EmployeeId && ui.IsActive);
        var employeeImagesViewDtoList = _mapper.Map<IList<EmployeeImageDto>>(employeeImages);
        if (employeeImages.Count > -1)
        {
            return new GetEmployeeImageListByEmployeeIdQueryResponse{
                Result = new DataResult<IList<EmployeeImageDto>>(ResultStatus.Success,employeeImagesViewDtoList)
            };
        }
        return new GetEmployeeImageListByEmployeeIdQueryResponse{
            Result = new DataResult<IList<EmployeeImageDto>>(ResultStatus.Error, Messages.EmployeeImageNotFound,null)
        };
    }
}