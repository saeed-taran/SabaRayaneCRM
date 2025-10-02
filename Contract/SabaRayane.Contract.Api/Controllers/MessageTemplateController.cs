using MediatR;
using Microsoft.AspNetCore.Mvc;
using SabaRayane.Contract.Application.Commands.s.MessageTemplates;
using SabaRayane.Contract.Application.Queries.s.MessageTemplates;
using Taran.Identity.Access;
using Taran.Shared.Api.Attributes;
using Taran.Shared.Api.Controllers;
using Taran.Shared.Core.User;

namespace SabaRayane.Contract.Api.Controllers;

public class MessageTemplateController : AuthorizedControllerBase
{
    private readonly ILogger<CustomerController> logger;
    private readonly IMediator mediator;
    private readonly IAppUser appUser;

    public MessageTemplateController(IMediator mediator, ILogger<CustomerController> logger, IAppUser appUser, IHttpContextAccessor httpContextAccessor)
        : base(mediator, appUser, httpContextAccessor)
    {
        this.logger = logger;
        this.mediator = mediator;
        this.appUser = appUser;
    }


    #region MessageTemplate actions
    [HttpGet]
    [CustomeAuthorize((int)AccessNames.MessageTemplate_Get)]
    public async Task<ActionResult> Get([FromQuery] SearchMessageTemplateQuery searchMessageTemplateQuery)
    {
        return Ok(await SendQuery(searchMessageTemplateQuery));
    }

    [HttpPost]
    [CustomeAuthorize((int)AccessNames.MessageTemplate_Create)]
    public async Task<ActionResult> Create([FromBody] CreateMessageTemplateCommand createMessageTemplateCommand)
    {
        return Ok(await SendCommand(createMessageTemplateCommand));
    }

    [HttpPut]
    [CustomeAuthorize((int)AccessNames.MessageTemplate_Update)]
    public async Task<ActionResult> Update([FromBody] UpdateMessageTemplateCommand updateMessageTemplateCommand)
    {
        return Ok(await SendCommand(updateMessageTemplateCommand));
    }

    [HttpDelete("{Id}")]
    [CustomeAuthorize((int)AccessNames.MessageTemplate_Delete)]
    public async Task<ActionResult> Delete([FromRoute] DeleteMessageTemplateCommand deleteMessageTemplateCommand)
    {
        return Ok(await SendCommand(deleteMessageTemplateCommand));
    }
    #endregion

}
