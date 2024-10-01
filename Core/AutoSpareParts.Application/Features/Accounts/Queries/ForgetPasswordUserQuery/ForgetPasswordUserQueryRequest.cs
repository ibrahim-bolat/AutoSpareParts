using AutoSpareParts.Application.Features.Accounts.DTOs;
using MediatR;

namespace AutoSpareParts.Application.Features.Accounts.Queries.ForgetPasswordUserQuery;

public class ForgetPasswordUserQueryRequest:IRequest<ForgetPasswordUserQueryResponse>
{
    public ForgetPassDto ForgetPassDto { get; set; }
}