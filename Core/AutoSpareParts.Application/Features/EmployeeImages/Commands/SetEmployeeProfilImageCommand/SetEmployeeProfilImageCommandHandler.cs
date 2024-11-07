using AutoSpareParts.Application.Features.EmployeeImages.Constants;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Enums;
using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeImages.Commands.SetEmployeeProfilImageCommand;

public class SetEmployeeProfilImageCommandHandler:IRequestHandler<SetEmployeeProfilImageCommandRequest,SetEmployeeProfilImageCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
  
    public SetEmployeeProfilImageCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }

    public async Task<SetEmployeeProfilImageCommandResponse> Handle(SetEmployeeProfilImageCommandRequest request, CancellationToken cancellationToken)
    {
        var employeeImages = await _unitOfWork.EmployeeImages.GetAllAsync(predicate:ui=>ui.EmployeeId==request.EmployeeId && ui.IsActive);
        if (employeeImages !=null)
        {
            foreach (var employeeImage in employeeImages)
            {
                if (employeeImage.Id == request.Id && employeeImage.Profil==false)
                {
                    employeeImage.Profil = true;
                    employeeImage.ModifiedByName = request.ModifiedByName;
                    employeeImage.ModifiedTime = DateTime.Now;
                    await _unitOfWork.EmployeeImages.UpdateAsync(employeeImage);
                }
                if(employeeImage.Id != request.Id && employeeImage.Profil)
                {
                    employeeImage.Profil = false;
                    employeeImage.ModifiedByName = request.ModifiedByName;
                    employeeImage.ModifiedTime = DateTime.Now;
                    await _unitOfWork.EmployeeImages.UpdateAsync(employeeImage);
                    
                }
            }
            await _unitOfWork.SaveAsync();
            return new SetEmployeeProfilImageCommandResponse
            {
                Result = new Result(ResultStatus.Success, Messages.EmployeeImageSetProfil)
            };
        }
        return new SetEmployeeProfilImageCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.EmployeeImageNotFound)
        };
    }
}