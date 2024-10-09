using Customers.WebApp.Domain;
using Customers.WebApp.Extensions;
using Customers.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Customers.WebApp.Controllers;

public class CustomerController : Controller
{
    private readonly ICustomerHandler _customerHandler;

    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ICustomerHandler customerHandler, 
        ILogger<CustomerController> logger)
    {
        _customerHandler = customerHandler;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var getCustomersResponse = await _customerHandler.GetAllCustomers();

            return getCustomersResponse.Match(
                success => View(success.Customers!.ToCustomersViewModel()),
                error => View("Error", new ErrorViewModel() { ErrorMessage = error.ErrorMessage }));
        }
        catch (Exception ex)
        {
            _logger.LogError($"There is an error while retrieving the customers. Error: {ex}");
            return View("Error", new ErrorViewModel() { ErrorMessage = "Something went wrong. Please try again later" });
        }
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Add(CustomerModel customer)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            var addCustomerResponse =  await _customerHandler.AddCustomer(customer.ToDomainCustomer());

            return addCustomerResponse.Match<IActionResult>(
               success => RedirectToAction("Index"),
               error => View("Error", new ErrorViewModel() { ErrorMessage = error.ErrorMessage }));
        }
        catch (Exception ex)
        {
            _logger.LogError($"There is an error while adding the customer. Error: {ex}");
            return View("Error", new ErrorViewModel() { ErrorMessage = "Something went wrong. Please try again later" });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        try
        {
            var getCustomerResponse = await _customerHandler.GetCustomerById(id);

            return getCustomerResponse.Match(
                      success => View(success.Customer.ToCustomerViewModel()),
                      error => View("Error", new ErrorViewModel() { ErrorMessage = error.ErrorMessage }));
        }
        catch (Exception ex)
        {
            _logger.LogError($"There is an error while retrieving the customer details.", ex);
            return View("Error", new ErrorViewModel() { ErrorMessage = "Something went wrong. Please try again later" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CustomerModel customer)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            var updateCustomerResponse =  await _customerHandler.UpdateCustomer(customer.ToDomainCustomer());

            return updateCustomerResponse.Match<IActionResult>(
               success => RedirectToAction("Index"),
               error => View("Error", new ErrorViewModel() { ErrorMessage = error.ErrorMessage }));
        }
        catch (Exception ex)
        {
            _logger.LogError($"There is an error while updating the customer details.", ex);
            return View("Error", new ErrorViewModel() { ErrorMessage = "Something went wrong. Please try again later" });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var getCustomerResponse = await _customerHandler.GetCustomerById(id);

            return getCustomerResponse.Match(
                     success => View(success.Customer.ToCustomerViewModel()),
                     error => View("Error", new ErrorViewModel() { ErrorMessage = error.ErrorMessage }));
        }
        catch (Exception ex)
        {
            _logger.LogError($"There is an error while retrieving the customer details.", ex);
            return View("Error", new ErrorViewModel() { ErrorMessage = "Something went wrong. Please try again later" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(CustomerModel customerModel)
    {
        try
        {
          var updateCustomerResponse = await _customerHandler.DeleteCustomer(customerModel.Id);
           
           return updateCustomerResponse.Match<IActionResult>(
                        success => RedirectToAction("Index"),
                        error => View("Error", new ErrorViewModel() { ErrorMessage = error.ErrorMessage }));
        }
        catch (Exception ex)
        {
            _logger.LogError($"There is an error while updating the customer details.", ex);
            return View("Error", new ErrorViewModel() { ErrorMessage = "Something went wrong. Please try again later" });
        }
    }
}
