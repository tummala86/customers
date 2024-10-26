using Customers.Infrastructure;
using Customers.Infrastructure.Database.Entities;
using Customers.Infrastructure.Ports;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using DomainCustomer = Customers.Domain.Models.Customer;

namespace Customers.API.Test.Unit.Infrastructure
{
    public class CustomersCommandTests
    {
        private readonly CustomersCommand _sut;
        private readonly Mock<ICustomerRepository> _customerRepository = new();
        private readonly Mock<ILogger<CustomersCommand>> _mockLogger = new();
        public CustomersCommandTests()
        {
            _sut = new CustomersCommand(_customerRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task SaveCustomer_Should_Retrun_Internal_Error()
        {
            // Arrange
            _customerRepository.Setup(x => x.InsertAsync(It.IsAny<Customer>()))
                .ReturnsAsync(() => throw new Exception("Internal error"));

            // Act
            var result = await _sut.SaveCustomer(CreateCustomerRequest());

            // Assert
            result.IsInternalError.Should().BeTrue();
            _customerRepository.Verify(d => d.InsertAsync(It.IsAny<Customer>()), Times.Once());
        }

        [Fact]
        public async Task SaveCustomer_Should_Retrun_Success()
        {
            // Arrange
            var customerRequest = CreateCustomerRequest();
            var customerDetails = new Customer()
            {
                Id = customerRequest.CustomerId,
                FirstName = "John",
                LastName = "Doe",
                Email = "John@gmail.com"
            };

            _customerRepository.Setup(x => x.InsertAsync(It.IsAny<Customer>()))
                .ReturnsAsync(customerDetails);

            // Act
            var result = await _sut.SaveCustomer(customerRequest);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _customerRepository.Verify(d => d.InsertAsync(It.IsAny<Customer>()), Times.Once());
        }

        [Fact]
        public async Task UpdateCustomer_Should_Retrun_Internal_Error()
        {
            // Arrange
            _customerRepository.Setup(x => x.UpdateAsync(It.IsAny<Customer>()))
                .ReturnsAsync(() => throw new Exception("Internal error"));

            // Act
            var result = await _sut.UpdateCustomer(CreateCustomerRequest());

            // Assert
            result.IsInternalError.Should().BeTrue();
            _customerRepository.Verify(d => d.UpdateAsync(It.IsAny<Customer>()), Times.Once());
        }

        [Fact]
        public async Task UpdateCustomer_Should_Retrun_Customer_Not_Found_Error()
        {
            // Arrange
            Customer? customerDetails = null;
            _customerRepository.Setup(x => x.UpdateAsync(It.IsAny<Customer>()))
                .ReturnsAsync(customerDetails);

            // Act
            var result = await _sut.UpdateCustomer(CreateCustomerRequest());

            // Assert
            result.IsNotFound.Should().BeTrue();
            _customerRepository.Verify(d => d.UpdateAsync(It.IsAny<Customer>()), Times.Once());
        }

        [Fact]
        public async Task UpdateCustomer_Should_Retrun_Success()
        {
            // Arrange
            var customerRequest = CreateCustomerRequest();

            var customerDetails = new Customer()
            {
                Id = customerRequest.CustomerId,
                FirstName = "John",
                LastName = "Doe",
                Email = "John@gmail.com"
            };

            _customerRepository.Setup(x => x.UpdateAsync(It.IsAny<Customer>()))
                .ReturnsAsync(customerDetails);

            // Act
            var result = await _sut.UpdateCustomer(customerRequest);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _customerRepository.Verify(d => d.UpdateAsync(It.IsAny<Customer>()), Times.Once());
        }

        [Fact]
        public async Task DeleteCustomer_Should_Retrun_Internal_Error()
        {
            // Arrange
            _customerRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => throw new Exception("Internal error"));

            var customerId = Guid.NewGuid();

            // Act
            var result = await _sut.DeleteCustomer(customerId);

            // Assert
            result.IsInternalError.Should().BeTrue();
            _customerRepository.Verify(d => d.DeleteAsync(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public async Task DeleteCustomer_Should_Retrun_Customer_NotFound_Error()
        {
            // Arrange
            Customer? customer = null;
            _customerRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(customer);

            var customerId = Guid.NewGuid();

            // Act
            var result = await _sut.DeleteCustomer(customerId);

            // Assert
            result.IsNotFound.Should().BeTrue();
            _customerRepository.Verify(d => d.DeleteAsync(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public async Task DeleteCustomer_Should_Retrun_Success()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            var customerDetails = new Customer()
            {
                Id = customerId,
                FirstName = "John",
                LastName = "Doe",
                Email = "John@gmail.com"
            };

            _customerRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(customerDetails);
            

            // Act
            var result = await _sut.DeleteCustomer(customerId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _customerRepository.Verify(d => d.DeleteAsync(It.IsAny<Guid>()), Times.Once());
        }

        private DomainCustomer CreateCustomerRequest()
        {
            return new DomainCustomer(Guid.NewGuid(), "John", "Doe", "John@gmail.com");
        }
    }
}
