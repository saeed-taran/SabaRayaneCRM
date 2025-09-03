using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taran.Shared.Application.Queries;
using Taran.Shared.Dtos.WrappedResponse;

namespace Taran.Shared.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class UnAuthorizedControllerBase : ControllerBase
{
    private readonly IMediator mediator;

    protected UnAuthorizedControllerBase(IMediator mediator)
    {
        this.mediator = mediator;
    }

    protected async Task<SuccessfulResponse<OutPut>> SendQuery<OutPut>(IQueryWithoutUser<OutPut> query)
    {
        var result = await mediator.Send(query);
        return new SuccessfulResponse<OutPut>(result);
    }
}