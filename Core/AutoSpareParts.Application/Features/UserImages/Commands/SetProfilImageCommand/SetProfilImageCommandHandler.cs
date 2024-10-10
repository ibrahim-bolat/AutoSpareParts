using AutoSpareParts.Application.Features.UserImages.Constants;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;

namespace AutoSpareParts.Application.Features.UserImages.Commands.SetProfilImageCommand;

public class SetProfilImageCommandHandler:IRequestHandler<SetProfilImageCommandRequest,SetProfilImageCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
  
    public SetProfilImageCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }

    public async Task<SetProfilImageCommandResponse> Handle(SetProfilImageCommandRequest request, CancellationToken cancellationToken)
    {
        var userImages = await _unitOfWork.UserImages.GetAllAsync(predicate:ui=>ui.UserId==request.UserId && ui.IsActive);
        if (userImages !=null)
        {
            foreach (var userImage in userImages)
            {
                if (userImage.Id == request.Id && userImage.Profil==false)
                {
                    userImage.Profil = true;
                    userImage.ModifiedByName = request.ModifiedByName;
                    userImage.ModifiedTime = DateTime.Now;
                    await _unitOfWork.UserImages.UpdateAsync(userImage);
                }
                if(userImage.Id != request.Id && userImage.Profil)
                {
                    userImage.Profil = false;
                    userImage.ModifiedByName = request.ModifiedByName;
                    userImage.ModifiedTime = DateTime.Now;
                    await _unitOfWork.UserImages.UpdateAsync(userImage);
                    
                }
            }
            await _unitOfWork.SaveAsync();
            return new SetProfilImageCommandResponse
            {
                Result = new Result(ResultStatus.Success, Messages.UserImageSetProfil)
            };
        }
        return new SetProfilImageCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.UserImageNotFound)
        };
    }
}