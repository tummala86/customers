using Customers.Infrastructure;
using Customers.Infrastructure.Database.Entities;
using Customers.Infrastructure.Models;
using Customers.Infrastructure.Ports;
using FluentAssertions;
using Moq;
using Xunit;
using DomainCustomer = Customers.Domain.Models.Customer;

namespace Customers.API.Test.Unit.Infrastructure
{
    public class CustomersCommandTests
    {
        private readonly CustomersCommand _sut;
        private readonly Mock<ICustomerRepository> _customerRepository = new();

        public CustomersCommandTests()
        {
            _sut = new CustomersCommand(_customerRepository.Object);
        }

        [Fact]
        public async Task SaveCustomer_Should_Retrun_Internal_Error()
        {
            // Arrange
            _customerRepository.Setup(x => x.InsertAsync(It.IsAny<DomainCustomer>()))
                .ReturnsAsync(() => new CreateCustomerResponse.InternalError("Internal error"));

            // Act
            var result = await _sut.SaveCustomer(CreateCustomerRequest());

            // Assert
            result.IsInternalError.Should().BeTrue();
            _customerRepository.Verify(d => d.InsertAsync(It.IsAny<DomainCustomer>()), Times.Once());
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

            _customerRepository.Setup(x => x.InsertAsync(It.IsAny<DomainCustomer>()))
                .ReturnsAsync(() => new CreateCustomerResponse.Success(customerDetails));

            // Act
            var result = await _sut.SaveCustomer(customerRequest);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _customerRepository.Verify(d => d.InsertAsync(It.IsAny<DomainCustomer>()), Times.Once());
        }

        [Fact]
        public async Task UpdateCustomer_Should_Retrun_Internal_Error()
        {
            // Arrange
            _customerRepository.Setup(x => x.UpdateAsync(It.IsAny<DomainCustomer>()))
                .ReturnsAsync(() => new UpdateCustomerResponse.InternalError("Internal error"));

            // Act
            var result = await _sut.UpdateCustomer(CreateCustomerRequest());

            // Assert
            result.IsInternalError.Should().BeTrue();
            _customerRepository.Verify(d => d.UpdateAsync(It.IsAny<DomainCustomer>()), Times.Once());
        }

        [Fact]
        public async Task UpdateCustomer_Should_Retrun_Customer_Not_Found_Error()
        {
            // Arrange
            _customerRepository.Setup(x => x.UpdateAsync(It.IsAny<DomainCustomer>()))
                .ReturnsAsync(() => new UpdateCustomerResponse.NotFound());

            // Act
            var result = await _sut.UpdateCustomer(CreateCustomerRequest());

            // Assert
            result.IsNotFound.Should().BeTrue();
            _customerRepository.Verify(d => d.UpdateAsync(It.IsAny<DomainCustomer>()), Times.Once());
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

            _customerRepository.Setup(x => x.UpdateAsync(It.IsAny<DomainCustomer>()))
                .ReturnsAsync(() => new UpdateCustomerResponse.Success(customerDetails));

            // Act
            var result = await _sut.UpdateCustomer(customerRequest);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _customerRepository.Verify(d => d.UpdateAsync(It.IsAny<DomainCustomer>()), Times.Once());
        }

        [Fact]
        public async Task DeleteCustomer_Should_Retrun_Internal_Error()
        {
            // Arrange
            _customerRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => new UpdateCustomerResponse.InternalError("Internal error"));

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
            _customerRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => new UpdateCustomerResponse.NotFound());

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
                .ReturnsAsync(() => new UpdateCustomerResponse.Success(customerDetails));
            

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
