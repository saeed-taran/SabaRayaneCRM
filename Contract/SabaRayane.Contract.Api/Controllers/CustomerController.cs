using MediatR;
using Microsoft.AspNetCore.Mvc;
using SabaRayane.Contract.Application.Commands.Customers.Agreements;
using SabaRayane.Contract.Application.Commands.Customers.Customers;
using SabaRayane.Contract.Application.Commands.s.Customers;
using SabaRayane.Contract.Application.Queries.Customers.Agreements;
using SabaRayane.Contract.Application.Queries.Customers.CustomerAggregate;
using SabaRayane.Contract.Application.Queries.s.Customers;
using Taran.Identity.Access;
using Taran.Shared.Api.Attributes;
using Taran.Shared.Api.Controllers;
using Taran.Shared.Application.Commands.IO;
using Taran.Shared.Application.Queries.IO;
using Taran.Shared.Core.User;
using Taran.Shared.Dtos.Languages;

namespace SabaRayane.Contract.Api.Controllers;

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

    [HttpGet("importTemplate")]
    [CustomeAuthorize((int)AccessNames.Customer_Create)]
    public async Task<ActionResult> GetCustomerImportTemplate()
    {
        GetImportTemplateQuery createExcelTemplateCommand = new(typeof(ImportCustomerCommand), "Customer_Import_Template");

        var response = await SendQuery(createExcelTemplateCommand);
        return File(response.Data!.ResultStream, response.Data.ContentType, response.Data.FileName);
    }

    [HttpPost("import")]
    [CustomeAuthorize((int)AccessNames.Customer_Create)]
    public async Task<ActionResult> Import([FromForm] IFormFile file)
    {
        ImportDataCommand<ImportCustomerCommand>
        importDataCommand = new(
            nameof(KeyWords.ImportingCustomer),
            notifyProgress: true,
            file.OpenReadStream(),
            (rowDictionary) =>
            {
                return new()
                {
                    CustomerId = rowDictionary[nameof(ImportCustomerCommand.CustomerId)]?.ToString(),
                    FirstName = rowDictionary[nameof(ImportCustomerCommand.FirstName)]?.ToString(),
                    LastName = rowDictionary[nameof(ImportCustomerCommand.LastName)]?.ToString(),
                    MobileNumber = rowDictionary[nameof(ImportCustomerCommand.MobileNumber)]?.ToString(),
                    StoreName = rowDictionary[nameof(ImportCustomerCommand.StoreName)]?.ToString(),
                    Description = rowDictionary[nameof(ImportCustomerCommand.Description)]?.ToString()
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

    #region Agreement actions
    [HttpGet("Agreement")]
    [CustomeAuthorize((int)AccessNames.Agreement_Get)]
    public async Task<ActionResult> Get([FromQuery] SearchAgreementQuery searchAgreementQuery)
    {
        return Ok(await SendQuery(searchAgreementQuery));
    }

    [HttpPost("Agreement")]
    [CustomeAuthorize((int)AccessNames.Agreement_Create)]
    public async Task<ActionResult> Create([FromBody] CreateAgreementCommand createAgreementCommand)
    {
        return Ok(await SendCommand(createAgreementCommand));
    }

    [HttpPut("Agreement")]
    [CustomeAuthorize((int)AccessNames.Agreement_Update)]
    public async Task<ActionResult> Update([FromBody] UpdateAgreementCommand updateAgreementCommand)
    {
        return Ok(await SendCommand(updateAgreementCommand));
    }

    [HttpDelete("Agreement/{Id}")]
    [CustomeAuthorize((int)AccessNames.Agreement_Delete)]
    public async Task<ActionResult> Delete([FromRoute] DeleteAgreementCommand deleteAgreementCommand)
    {
        return Ok(await SendCommand(deleteAgreementCommand));
    }
    #endregion

    #region Notification actions
    [HttpGet("Notification")]
    [CustomeAuthorize((int)AccessNames.Notification_Get)]
    public async Task<ActionResult> Get([FromQuery] SearchNotificationQuery searchNotificationQuery)
    {
        return Ok(await SendQuery(searchNotificationQuery));
    }
    #endregion

}
