using AutoMapper;
using AutoSpareParts.Application.Features.EmployeeAddresses.Constants;
using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Enums;
using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Commands.DeleteEmployeeAddressCommand;

public class DeleteEmployeeAddressCommandHandler : IRequestHandler<DeleteEmployeeAddressCommandRequest, DeleteEmployeeAddressCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteEmployeeAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DeleteEmployeeAddressCommandResponse> Handle(DeleteEmployeeAddressCommandRequest request, CancellationToken cancellationToken)
    {
        var employeeAddress = await _unitOfWork.EmployeeAddresses.GetAsync(predicate:x => x.Id == request.Id);
        if (employeeAddress != null)
        {
            employeeAddress.IsActive = false;
            employeeAddress.IsDeleted = true;
            employeeAddress.ModifiedByName = request.ModifiedByName;
            employeeAddress.ModifiedTime = DateTime.Now;
            var deletedAddress = _unitOfWork.EmployeeAddresses.UpdateAsync(employeeAddress);
            var result = await _unitOfWork.SaveAsync();
            var addressDto = _mapper.Map<EmployeeAddressDetailDto>(employeeAddress);
            if (result > 0)
            {
                return new DeleteEmployeeAddressCommandResponse
                {
                    Result = new DataResult<EmployeeAddressDetailDto>(ResultStatus.Success, Messages.EmployeeAddressDeleted, addressDto)
                };
            }
            return new DeleteEmployeeAddressCommandResponse
            {
                Result = new DataResult<EmployeeAddressDetailDto>(ResultStatus.Error, Messages.EmployeeAddressNotDeleted, null)
            };
        }
        return new DeleteEmployeeAddressCommandResponse
        {
            Result = new DataResult<EmployeeAddressDetailDto>(ResultStatus.Error, Messages.EmployeeAddressNotFound, null)
        };
    }
}