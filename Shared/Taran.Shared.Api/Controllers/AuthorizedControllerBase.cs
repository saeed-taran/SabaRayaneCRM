using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taran.Shared.Application.Commands;
using Taran.Shared.Application.Queries;
using Taran.Shared.Core.User;
using Taran.Shared.Dtos.WrappedResponse;
using System.Net;

namespace Taran.Shared.Api.Controllers;


[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthorizedControllerBase : ControllerBase
{
    private readonly IMediator mediator;
    protected readonly IAppUser appUser;
    private readonly IHttpContextAccessor httpContextAccessor;

    protected AuthorizedControllerBase(IMediator mediator, IAppUser appUser, IHttpContextAccessor httpContextAccessor)
    {
        this.mediator = mediator;
        this.appUser = appUser;
        this.httpContextAccessor = httpContextAccessor;
    }

    protected async Task<SuccessfulResponse<OutPut>> SendCommand<OutPut>(ICommandWithUser<OutPut> command) 
    {
        command.SetUserId(appUser.UserID);
        var result = await mediator.Send(command);
        return new SuccessfulResponse<OutPut>(result);
    }

    protected async Task<SuccessfulResponse<OutPut>> SendQuery<OutPut>(IQueryWithUser<OutPut> query)
    {
        query.SetUserId(appUser.UserID);
        var result = await mediator.Send(query);
        return new SuccessfulResponse<OutPut>(result);
    }

    protected async Task<OutPut> SendInternalQuery<OutPut>(IQueryInternal<OutPut> query)
    {
        //if (!httpContextAccessor.HttpContext.Request.IsLocal())
        //    throw new BadHttpRequestException("forbidden", (int)HttpStatusCode.Forbidden);

        return await mediator.Send(query);
    }
}