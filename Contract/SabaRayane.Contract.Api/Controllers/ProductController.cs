using MediatR;
using Microsoft.AspNetCore.Mvc;
using SabaRayane.Contract.Application.Commands.s.Products;
using SabaRayane.Contract.Application.Queries.s.Products;
using Taran.Identity.Access;
using Taran.Shared.Api.Attributes;
using Taran.Shared.Api.Controllers;
using Taran.Shared.Core.User;

namespace SabaRayane.Contract.Api.Controllers;

public class ProductController : AuthorizedControllerBase
{
    private readonly ILogger<CustomerController> logger;
    private readonly IMediator mediator;
    private readonly IAppUser appUser;

    public ProductController(IMediator mediator, ILogger<CustomerController> logger, IAppUser appUser, IHttpContextAccessor httpContextAccessor)
        : base(mediator, appUser, httpContextAccessor)
    {
        this.logger = logger;
        this.mediator = mediator;
        this.appUser = appUser;
    }


    #region Product actions
    [HttpGet]
    [CustomeAuthorize((int)AccessNames.Product_Get)]
    public async Task<ActionResult> Get([FromQuery] SearchProductQuery searchProductQuery)
    {
        return Ok(await SendQuery(searchProductQuery));
    }

    [HttpPost]
    [CustomeAuthorize((int)AccessNames.Product_Create)]
    public async Task<ActionResult> Create([FromBody] CreateProductCommand createProductCommand)
    {
        return Ok(await SendCommand(createProductCommand));
    }

    [HttpPut]
    [CustomeAuthorize((int)AccessNames.Product_Update)]
    public async Task<ActionResult> Update([FromBody] UpdateProductCommand updateProductCommand)
    {
        return Ok(await SendCommand(updateProductCommand));
    }

    [HttpDelete("{Id}")]
    [CustomeAuthorize((int)AccessNames.Product_Delete)]
    public async Task<ActionResult> Delete([FromRoute] DeleteProductCommand deleteProductCommand)
    {
        return Ok(await SendCommand(deleteProductCommand));
    }
    #endregion

}
