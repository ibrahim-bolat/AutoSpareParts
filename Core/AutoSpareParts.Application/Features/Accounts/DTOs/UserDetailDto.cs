using AutoSpareParts.Application.DTOs.Base;
using AutoSpareParts.Application.Features.Addresses.DTOs;

namespace AutoSpareParts.Application.Features.Accounts.DTOs;

public class UserDetailDto:BaseDto
{
    public UserDto UserDto { get; set; }
    public List<AddressSummaryDto> UserAddressSummaryDtos { get; set; }
}
