using AutoMapper;
using AutoSpareParts.Application.Features.EmployeeImages.Constants;
using AutoSpareParts.Application.Features.EmployeeImages.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeImages.Queries.GetEmployeeProfilImageByEmployeeIdQuery;

public class GetEmployeeProfilImageByEmployeeIdQueryHandler:IRequestHandler<GetEmployeeProfilImageByEmployeeIdQueryRequest,GetEmployeeProfilImageByEmployeeIdQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEmployeeProfilImageByEmployeeIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetEmployeeProfilImageByEmployeeIdQueryResponse> Handle(GetEmployeeProfilImageByEmployeeIdQueryRequest request, CancellationToken cancellationToken)
    {
        var employeeImage = await _unitOfWork.EmployeeImages.GetAsync(predicate:x => x.EmployeeId == request.EmployeeId && x.IsActive && x.Profil);
        var employeeImageViewDto = _mapper.Map<EmployeeImageDto>(employeeImage);
        if (employeeImage != null)
        {
            return new GetEmployeeProfilImageByEmployeeIdQueryResponse{
                Result = new DataResult<EmployeeImageDto>(ResultStatus.Success,employeeImageViewDto)
            };
        }
        return new GetEmployeeProfilImageByEmployeeIdQueryResponse{
            Result = new DataResult<EmployeeImageDto>(ResultStatus.Error, Messages.EmployeeImageNotFound,null)
        };
    }
}