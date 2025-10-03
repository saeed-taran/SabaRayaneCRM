using MediatR;
using Microsoft.AspNetCore.Mvc;
using SabaRayane.Contract.Application.Commands.Products.Products;
using SabaRayane.Contract.Application.Commands.s.Products;
using SabaRayane.Contract.Application.Queries.s.Products;
using Taran.Shared.Application.Commands.IO;
using Taran.Shared.Application.Queries.IO;
using Taran.Shared.Dtos.Languages;
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

    [HttpGet("importTemplate")]
    [CustomeAuthorize((int)AccessNames.Product_Create)]
    public async Task<ActionResult> GetProductImportTemplate()
    {
        GetImportTemplateQuery createExcelTemplateCommand = new(typeof(ImportProductCommand));

        var response = await SendQuery(createExcelTemplateCommand);
        return File(response.Data!.ResultStream, response.Data.ContentType, response.Data.FileName);
    }

    [HttpPost("import")]
    [CustomeAuthorize((int)AccessNames.Product_Create)]
    public async Task<ActionResult> Import([FromForm] IFormFile file)
    {
        ImportDataCommand<ImportProductCommand>
        importDataCommand = new(
            nameof(KeyWords.ImportingProduct),
            notifyProgress: true,
            file.OpenReadStream(),
            (rowDictionary) =>
            {
                return new()
                {
                    Name = rowDictionary[nameof(ImportProductCommand.Name)]?.ToString(),
                    Price = Convert.ToInt64(rowDictionary[nameof(ImportProductCommand.Price)]),
                    Description = rowDictionary[nameof(ImportProductCommand.Description)]?.ToString()
                };
            },
            async (createCommand) =>
            {
                await SendCommand(createCommand);
            },
            sendNotifEveryMilliSeconds: 200,
            errorListMaxLenght: 50
        );

        return Ok(await SendCommand(importDataCommand));
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
