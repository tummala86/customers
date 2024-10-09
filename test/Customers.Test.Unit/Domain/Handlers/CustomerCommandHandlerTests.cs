using Customers.Domain.Handlers;
using Customers.Domain.Models;
using Customers.Domain.Ports;
using FluentAssertions;
using Moq;
using Xunit;

namespace Customers.API.Test.Unit.Domain.Handlers
{
    public class CustomerCommandHandlerTests
    {
        private readonly CustomerCommandHandler _sut;
        private readonly Mock<ICustomerCommand> _customerCommand = new();

        public CustomerCommandHandlerTests()
        {
            _sut = new CustomerCommandHandler(_customerCommand.Object);
        }

        [Fact]
        public async Task SaveCustomer_Should_Retrun_Internal_Error()
        {
            // Arrange
            _customerCommand.Setup(x => x.SaveCustomer(It.IsAny<Customer>()))
                .ReturnsAsync(() => new CreateCustomerResponse.InternalError("Internal error"));

            // Act
            var result = await _sut.SaveCustomer(CreateCustomer());

            // Assert
            result.IsInternalError.Should().BeTrue();
            _customerCommand.Verify(d => d.SaveCustomer(It.IsAny<Customer>()), Times.Once());
        }

        [Fact]
        public async Task SaveCustomer_Should_Retrun_Success()
        {
            // Arrange
            var customer = CreateCustomer();
            _customerCommand.Setup(x => x.SaveCustomer(It.IsAny<Customer>()))
                .ReturnsAsync(() => new CreateCustomerResponse.Success(customer));

            // Act
            var result = await _sut.SaveCustomer(customer);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _customerCommand.Verify(d => d.SaveCustomer(It.IsAny<Customer>()), Times.Once());
        }

        [Fact]
        public async Task UpdateCustomer_Should_Retrun_Internal_Error()
        {
            // Arrange
            _customerCommand.Setup(x => x.UpdateCustomer(It.IsAny<Customer>()))
                .ReturnsAsync(() => new UpdateCustomerResponse.InternalError("Internal error"));

            // Act
            var result = await _sut.UpdateCustomer(CreateCustomer());

            // Assert
            result.IsInternalError.Should().BeTrue();
            _customerCommand.Verify(d => d.UpdateCustomer(It.IsAny<Customer>()), Times.Once());
        }

        [Fact]
        public async Task UpdateCustomer_Should_Retrun_Customer_Not_Found_Error()
        {
            // Arrange
            _customerCommand.Setup(x => x.UpdateCustomer(It.IsAny<Customer>()))
                .ReturnsAsync(() => new UpdateCustomerResponse.NotFound());

            // Act
            var result = await _sut.UpdateCustomer(CreateCustomer());

            // Assert
            result.IsNotFound.Should().BeTrue();
            _customerCommand.Verify(d => d.UpdateCustomer(It.IsAny<Customer>()), Times.Once());
        }

        [Fact]
        public async Task UpdateCustomer_Should_Retrun_Success()
        {
            // Arrange
            var customer = CreateCustomer();
            _customerCommand.Setup(x => x.UpdateCustomer(It.IsAny<Customer>()))
                .ReturnsAsync(() => new UpdateCustomerResponse.Success(customer));

            // Act
            var result = await _sut.UpdateCustomer(customer);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _customerCommand.Verify(d => d.UpdateCustomer(It.IsAny<Customer>()), Times.Once());
        }

        [Fact]
        public async Task DeleteCustomer_Should_Retrun_Internal_Error()
        {
            // Arrange
            _customerCommand.Setup(x => x.DeleteCustomer(It.IsAny<Guid>()))
                .ReturnsAsync(() => new UpdateCustomerResponse.InternalError("Internal error"));

            var customerId = Guid.NewGuid();

            // Act
            var result = await _sut.DeleteCustomer(customerId);

            // Assert
            result.IsInternalError.Should().BeTrue();
            _customerCommand.Verify(d => d.DeleteCustomer(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public async Task DeleteCustomer_Should_Retrun_Customer_NotFound_Error()
        {
            // Arrange
            _customerCommand.Setup(x => x.DeleteCustomer(It.IsAny<Guid>()))
                .ReturnsAsync(() => new UpdateCustomerResponse.NotFound());

            var customerId = Guid.NewGuid();

            // Act
            var result = await _sut.DeleteCustomer(customerId);

            // Assert
            result.IsNotFound.Should().BeTrue();
            _customerCommand.Verify(d => d.DeleteCustomer(It.IsAny<Guid>()),Times.Once());
        }

        [Fact]
        public async Task DeleteCustomer_Should_Retrun_Success()
        {
            // Arrange
            var customer = CreateCustomer();
            _customerCommand.Setup(x => x.DeleteCustomer(It.IsAny<Guid>()))
                .ReturnsAsync(() => new UpdateCustomerResponse.Success(customer));

            var customerId = Guid.NewGuid();

            // Act
            var result = await _sut.DeleteCustomer(customerId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _customerCommand.Verify(d => d.DeleteCustomer(It.IsAny<Guid>()), Times.Once());
        }

        private Customer CreateCustomer()
        {
            return new Customer(Guid.NewGuid(),"John", "Doe", "John@gmail.com");
        }
    }
}
