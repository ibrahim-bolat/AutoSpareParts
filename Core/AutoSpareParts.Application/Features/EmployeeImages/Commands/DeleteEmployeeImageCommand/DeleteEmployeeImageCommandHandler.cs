using AutoMapper;
using AutoSpareParts.Application.Features.EmployeeImages.Constants;
using AutoSpareParts.Application.Features.EmployeeImages.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace AutoSpareParts.Application.Features.EmployeeImages.Commands.DeleteEmployeeImageCommand;

public class DeleteEmployeeImageCommandHandler:IRequestHandler<DeleteEmployeeImageCommandRequest,DeleteEmployeeImageCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _hostEnvironment;

    public DeleteEmployeeImageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _hostEnvironment = hostEnvironment;
    }
    
    public async Task<DeleteEmployeeImageCommandResponse> Handle(DeleteEmployeeImageCommandRequest request, CancellationToken cancellationToken)
    {
        var employeeImage = await _unitOfWork.EmployeeImages.GetAsync(predicate:x => x.Id == request.Id && x.IsActive==true);
        if (employeeImage != null)
        {
            var imagePath = _hostEnvironment.WebRootPath + employeeImage.Path;
            var employeeImagePath = $"{_hostEnvironment.WebRootPath}/admin/images/employeeimages/{employeeImage.EmployeeId}";
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
                if(Directory.Exists(employeeImagePath) && Directory.GetFiles(employeeImagePath).Length==0)
                    Directory.Delete(employeeImagePath);

                employeeImage.IsActive = false;
                employeeImage.IsDeleted = true;
                employeeImage.ModifiedByName = request.ModifiedByName;
                employeeImage.ModifiedTime = DateTime.Now;
                await _unitOfWork.EmployeeImages.UpdateAsync(employeeImage);
                var result = await _unitOfWork.SaveAsync();
                var employeeImageDto = _mapper.Map<EmployeeImageDto>(employeeImage);
                if (result > 0)
                {
                    return new DeleteEmployeeImageCommandResponse
                    {
                        Result = new DataResult<EmployeeImageDto>(ResultStatus.Success, employeeImageDto)
                    };
                }
                return new DeleteEmployeeImageCommandResponse
                {
                    Result = new DataResult<EmployeeImageDto>(ResultStatus.Error, Messages.EmployeeImageNotDeleted, null)
                };
            }
        }
        return new DeleteEmployeeImageCommandResponse
        {
            Result = new DataResult<EmployeeImageDto>(ResultStatus.Error, Messages.EmployeeImageNotFound, null)
        };
    }

}