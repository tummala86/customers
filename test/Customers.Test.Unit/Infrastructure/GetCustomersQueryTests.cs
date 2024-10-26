using Customers.Infrastructure;
using Customers.Infrastructure.Database.Entities;
using Customers.Infrastructure.Models;
using Customers.Infrastructure.Ports;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Customers.API.Test.Unit.Infrastructure
{
    public class GetCustomersQueryTests
    {
        private readonly GetCustomersQuery _sut;
        private readonly Mock<ICustomerRepository> _customerRepository = new();
        private readonly Mock<ILogger<GetCustomersQuery>> _logger = new();

        public GetCustomersQueryTests()
        {
            _sut = new GetCustomersQuery(_customerRepository.Object, _logger.Object);
        }

        [Fact]
        public async Task GetAllCustomers_Should_Retrun_Internal_Error()
        {
            // Arrange
            _customerRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(() => throw new Exception("Internal error"));

            // Act
            var result = await _sut.GetAllCustomers();

            // Assert
            result.IsInternalError.Should().BeTrue();
            _customerRepository.Verify(d => d.GetAllAsync(), Times.Once());
        }

        [Fact]
        public async Task GetAllCustomers_Should_Retrun_Success()
        {
            // Arrange
            _customerRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Customer>());

            // Act
            var result = await _sut.GetAllCustomers();

            // Assert
            result.IsSuccess.Should().BeTrue();
            _customerRepository.Verify(d => d.GetAllAsync(), Times.Once());
        }

        [Fact]
        public async Task GetCustomerById_Should_Retrun_Internal_Error()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            _customerRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => throw new Exception("Internal error"));

            var getCustomerRequest = new Customers.Domain.Models.GetCustomerRequest(customerId);

            // Act
            var result = await _sut.GetCustomerById(getCustomerRequest);

            // Assert
            result.IsInternalError.Should().BeTrue();
            _customerRepository.Verify(d => d.GetByIdAsync(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public async Task GetCustomerById_Should_Retrun_Customer_NotFoun_Error()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            Customer? customer = null;

            _customerRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(customer);

            var getCustomerRequest = new Customers.Domain.Models.GetCustomerRequest(customerId);

            // Act
            var result = await _sut.GetCustomerById(getCustomerRequest);

            // Assert
            result.IsNotFound.Should().BeTrue();
            _customerRepository.Verify(d => d.GetByIdAsync(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public async Task GetCustomerById_Should_Retrun_Success()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            var customerDetails = new Customer(){
                Id = customerId,
                FirstName = "John",
                LastName = "Doe",
                Email = "John@gmail.com" 
            };

            _customerRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(customerDetails);

            var getCustomerRequest = new Customers.Domain.Models.GetCustomerRequest(customerId);

            // Act
            var result = await _sut.GetCustomerById(getCustomerRequest);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _customerRepository.Verify(d => d.GetByIdAsync(It.IsAny<Guid>()), Times.Once());
        }
    }
}
