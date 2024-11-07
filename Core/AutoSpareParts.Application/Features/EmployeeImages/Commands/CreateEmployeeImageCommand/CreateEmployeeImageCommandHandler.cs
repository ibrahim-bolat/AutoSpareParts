using AutoMapper;
using AutoSpareParts.Application.Features.EmployeeImages.Constants;
using AutoSpareParts.Application.Features.EmployeeImages.DTOs;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Application.Wrappers.Concrete;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using MediatR;

namespace AutoSpareParts.Application.Features.EmployeeImages.Commands.CreateEmployeeImageCommand;

public class CreateEmployeeImageCommandHandler:IRequestHandler<CreateEmployeeImageCommandRequest,CreateEmployeeImageCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateEmployeeImageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateEmployeeImageCommandResponse> Handle(CreateEmployeeImageCommandRequest request, CancellationToken cancellationToken)
    {
        var count = await _unitOfWork.EmployeeImages.CountAsync(predicate:x => x.EmployeeId == request.CreateEmployeeImageDto.EmployeeId && x.IsActive);
        if (count >= 4)
        {
            return new CreateEmployeeImageCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.EmployeeImageCountMoreThan4)
            };
        }
        EmployeeImage employeeImage = _mapper.Map<EmployeeImage>(request.CreateEmployeeImageDto);
        employeeImage.Path = await UploadImage(request.CreateEmployeeImageDto); //Resmi kaydet wwwroot/admin/images/employeeimages/
        employeeImage.CreatedByName = request.CreatedByName;
        employeeImage.ModifiedByName = request.CreatedByName;
        employeeImage.CreatedTime=DateTime.Now;
        employeeImage.ModifiedTime=DateTime.Now;
        employeeImage.IsActive = true;
        employeeImage.IsDeleted = false;
        await _unitOfWork.EmployeeImages.AddAsync(employeeImage);
        if (count > 0  && employeeImage.Profil)
        {
            var employeeImages =await _unitOfWork.EmployeeImages.GetAllAsync(predicate:ui =>
                    ui.EmployeeId == request.CreateEmployeeImageDto.EmployeeId && ui.IsActive);
            if (employeeImages != null)
            {
                foreach (var eImage in employeeImages)
                {
                    if (eImage.Profil)
                    {
                        eImage.Profil = false;
                        eImage.ModifiedByName = request.CreatedByName;
                        eImage.ModifiedTime = DateTime.Now;
                        await _unitOfWork.EmployeeImages.UpdateAsync(eImage);
                    }
                }
            }
        }
        int result = await _unitOfWork.SaveAsync();
        if (result > 0)
        {
            return new CreateEmployeeImageCommandResponse
            {
                Result = new DataResult<CreateEmployeeImageDto>(ResultStatus.Success, Messages.EmployeeImageAdded, request.CreateEmployeeImageDto)
            };
        }
        return new CreateEmployeeImageCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.EmployeeImageNotAdded)
        };
    }
    private async Task<string> UploadImage(CreateEmployeeImageDto employeeImageAddDto)
    {
        var imageFileName = Path.GetFileNameWithoutExtension(employeeImageAddDto.ImageFile.FileName);

        var localImageFileDir = $"wwwroot/admin/images/userimages/{employeeImageAddDto.EmployeeId}";// wwwroot/admin/images/employeeimages/1
        var extension = Path.GetExtension(employeeImageAddDto.ImageFile.FileName).ToLower();

        //local employeeimages/employeeId klasörü yoksa oluştur.
        if (!Directory.Exists(Path.Combine(localImageFileDir)))
        {
            Directory.CreateDirectory(Path.Combine(localImageFileDir));
        }
        
        var localImageFilePath = $"{localImageFileDir}/{imageFileName}{extension}"; // wwwroot/admin/images/employeeimages/1/profil.jpg

        int count = 1;
        var tempFileName = imageFileName;
        while (File.Exists(localImageFilePath))
        {
            imageFileName = string.Format("{0}{1}", tempFileName, count++);
            localImageFilePath = $"{localImageFileDir}/{imageFileName}{extension}";
        }

        var path = $"/admin/images/employeeimages/{employeeImageAddDto.EmployeeId}/{imageFileName}{extension}";// /admin/images/employeeimages/1/profil.jpg

        using (Stream fileStream = new FileStream(localImageFilePath, FileMode.Create))
        {
            await employeeImageAddDto.ImageFile.CopyToAsync(fileStream);
        }
        return path;
    }
}