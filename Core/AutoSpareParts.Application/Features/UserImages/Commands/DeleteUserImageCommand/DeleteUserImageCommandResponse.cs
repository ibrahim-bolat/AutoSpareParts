
using AutoSpareParts.Application.Features.UserImages.DTOs;
using AutoSpareParts.Application.Wrappers.Abstract;

namespace AutoSpareParts.Application.Features.UserImages.Commands.DeleteUserImageCommand;

public class DeleteUserImageCommandResponse
{
    public IDataResult<UserImageDto> Result { get; set; }
}