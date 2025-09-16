using MediatR;
using Microsoft.AspNetCore.Mvc;
using SabaRayane.Contract.Application.Commands.s.Customers;
using SabaRayane.Contract.Application.Queries.s.Customers;
using Taran.Identity.Access;
using Taran.Shared.Api.Attributes;
using Taran.Shared.Api.Controllers;
using Taran.Shared.Core.User;

namespace SabaRayane.Contract.Api.Controllers
{
    public class CustomerController : AuthorizedControllerBase
    {
        private readonly ILogger<CustomerController> logger;
        private readonly IMediator mediator;
        private readonly IAppUser appUser;

        public CustomerController(IMediator mediator, ILogger<CustomerController> logger, IAppUser appUser, IHttpContextAccessor httpContextAccessor)
            : base(mediator, appUser, httpContextAccessor)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.appUser = appUser;
        }


        #region Customer actions
        [HttpGet]
        [CustomeAuthorize((int)AccessNames.Customer_Get)]
        public async Task<ActionResult> Get([FromQuery] SearchCustomerQuery searchCustomerQuery)
        {
            return Ok(await SendQuery(searchCustomerQuery));
        }

        [HttpPost]
        [CustomeAuthorize((int)AccessNames.Customer_Create)]
        public async Task<ActionResult> Create([FromBody] CreateCustomerCommand createCustomerCommand)
        {
            return Ok(await SendCommand(createCustomerCommand));
        }

        [HttpPut]
        [CustomeAuthorize((int)AccessNames.Customer_Update)]
        public async Task<ActionResult> Update([FromBody] UpdateCustomerCommand updateCustomerCommand)
        {
            return Ok(await SendCommand(updateCustomerCommand));
        }

        [HttpDelete("{Id}")]
        [CustomeAuthorize((int)AccessNames.Customer_Delete)]
        public async Task<ActionResult> Delete([FromRoute] DeleteCustomerCommand deleteCustomerCommand)
        {
            return Ok(await SendCommand(deleteCustomerCommand));
        }
        #endregion

    }
}
