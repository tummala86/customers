using Microsoft.AspNetCore.Mvc;
using Customers.API.Constants;
using System.Net;
using Customers.API.Util;
using Customers.API.Models;
using Customers.Domain.Handlers;
using Customers.API.Validation;
using Customers.Domain.Validators;
using Customers.Domain.Models;
using Customers.API.Extensions;

namespace Customers.API.Controllers;

[ApiController]
[Route(ApiRoutes.Base)]
public class CustomersController : ControllerBase
{
    private readonly ICustomerCommandHandler _customerCommandHandler;
    private readonly IGetCustomersQueryHandler _getCustomersQueryHandler;

    public CustomersController(
        ICustomerCommandHandler customerCommandHandler,
        IGetCustomersQueryHandler getCustomersQueryHandler)
    {
        _customerCommandHandler = customerCommandHandler;
        _getCustomersQueryHandler = getCustomersQueryHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        var result = await _getCustomersQueryHandler.GetAllCustomers();

        return result.Match(
            success => Ok(success.Customers),
            internalError => StatusCode((int)HttpStatusCode.InternalServerError,
                ProblemDetailsHelper.InternalServerError(ApiRoutes.Base))
            );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById([FromRoute] string? id)
    {
        var validationResult = GetCustomerRequestValidator.Validate(id);
        if (validationResult.IsFailure)
        {
            var errors = validationResult.GetGroupErrors();
            return StatusCode((int)HttpStatusCode.BadRequest, ProblemDetailsHelper.InvalidParameters(errors, ApiRoutes.Base));
        }

        var domainGetCustomerRequest = new GetCustomerRequest(Guid.Parse(id!));

        var result = await _getCustomersQueryHandler.GetCustomerById(domainGetCustomerRequest);

        return result.Match(
            success => Ok(success.Customer),
            notFound => StatusCode((int)HttpStatusCode.NotFound, ProblemDetailsHelper.CustomerNotFound(ApiRoutes.Base)),
            internalError => StatusCode((int)HttpStatusCode.InternalServerError,
                ProblemDetailsHelper.InternalServerError(ApiRoutes.Base))
            );
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddCustomer([FromBody] CustomerRequest customerRequest)
    {
        var validationResult = CustomerRequestValidator.ValidateAddCustomerRequest(customerRequest);
        if (validationResult.IsFailure)
        {
            var errors = validationResult.GetGroupErrors();
            return StatusCode((int)HttpStatusCode.BadRequest, ProblemDetailsHelper.InvalidParameters(errors, ApiRoutes.Base));
        }

        var saveCustomerResults = await _customerCommandHandler.SaveCustomer(customerRequest.ToDomain());

        return saveCustomerResults.Match(
            success => StatusCode((int)HttpStatusCode.Created, success.Customer),
            internalError => StatusCode(
                (int)HttpStatusCode.InternalServerError,
                ProblemDetailsHelper.InternalServerError(ApiRoutes.Base))
        );
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateCustomer([FromBody] CustomerRequest customerRequest)
    {
        var validationResult = CustomerRequestValidator.ValidateUpdateCustomerRequest(customerRequest);
        if (validationResult.IsFailure)
        {
            var errors = validationResult.GetGroupErrors();
            return StatusCode((int)HttpStatusCode.BadRequest, ProblemDetailsHelper.InvalidParameters(errors, ApiRoutes.Base));
        }

        var updateCustomerResult = await _customerCommandHandler.UpdateCustomer(customerRequest.ToDomain());

        return updateCustomerResult.Match<IActionResult>(
            success => Ok(success.Customer),
            notFound => StatusCode((int)HttpStatusCode.NotFound, ProblemDetailsHelper.CustomerNotFound(ApiRoutes.Base)),
            internalError => StatusCode(
                (int)HttpStatusCode.InternalServerError,
                ProblemDetailsHelper.InternalServerError(ApiRoutes.Base))
            );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer([FromRoute] string? id)
    {
        var validationResult = GetCustomerRequestValidator.Validate(id);
        if (validationResult.IsFailure)
        {
            var errors = validationResult.GetGroupErrors();
            return StatusCode((int)HttpStatusCode.BadRequest, ProblemDetailsHelper.InvalidParameters(errors, ApiRoutes.Base));
        }

        var customerId = Guid.Parse(id!);

        var updateCustomerResult = await _customerCommandHandler.DeleteCustomer(customerId);

        return updateCustomerResult.Match<IActionResult>(
            success => Ok(success.Customer),
            notFound => StatusCode((int)HttpStatusCode.NotFound, ProblemDetailsHelper.CustomerNotFound(ApiRoutes.Base)),
            internalError => StatusCode(
                (int)HttpStatusCode.InternalServerError,
                ProblemDetailsHelper.InternalServerError(ApiRoutes.Base))
            );
    }

}
