using AutoSpareParts.Application.DTOs.Base;
using AutoSpareParts.Application.Features.Addresses.DTOs;

namespace AutoSpareParts.Application.Features.Accounts.DTOs;

public record UserDetailDto:BaseDto
{
    public UserDto UserDto { get; init; }
    public List<AddressSummaryDto> UserAddressSummaryDtos { get; init; }
}
