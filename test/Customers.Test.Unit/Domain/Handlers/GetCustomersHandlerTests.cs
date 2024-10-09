using Customers.Domain.Handlers;
using Customers.Domain.Models;
using Customers.Domain.Ports;
using FluentAssertions;
using Moq;
using Xunit;

namespace Customers.API.Test.Unit.Domain.Handlers
{
    public class GetCustomersHandlerTests
    {
        private readonly GetCustomersHandler _sut;
        private readonly Mock<IGetCustomersQuery> _getCustomersQuery = new();

        public GetCustomersHandlerTests()
        {
            _sut = new GetCustomersHandler(_getCustomersQuery.Object);
        }

        [Fact]
        public async Task GetAllCustomers_Should_Retrun_Internal_Error()
        {
            // Arrange
            _getCustomersQuery.Setup(x => x.GetAllCustomers())
                .ReturnsAsync(() => new GetCustomersResponse.InternalError("Internal error"));

            // Act
            var result = await _sut.GetAllCustomers();

            // Assert
            result.IsInternalError.Should().BeTrue();
            _getCustomersQuery.Verify(d => d.GetAllCustomers(), Times.Once());
        }

        [Fact]
        public async Task GetAllCustomers_Should_Retrun_Success()
        {
            // Arrange
            _getCustomersQuery.Setup(x => x.GetAllCustomers())
                .ReturnsAsync(() => new GetCustomersResponse.Success(new List<Customer>()));

            // Act
            var result = await _sut.GetAllCustomers();

            // Assert
            result.IsSuccess.Should().BeTrue();
            _getCustomersQuery.Verify(d => d.GetAllCustomers(), Times.Once());
        }

        [Fact]
        public async Task GetCustomerById_Should_Retrun_Internal_Error()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            _getCustomersQuery.Setup(x => x.GetCustomerById(It.IsAny<GetCustomerRequest>()))
                .ReturnsAsync(() => new GetCustomerResponse.InternalError("Internal error"));

            var getCustomerRequest = new GetCustomerRequest(customerId);

            // Act
            var result = await _sut.GetCustomerById(getCustomerRequest);

            // Assert
            result.IsInternalError.Should().BeTrue();
            _getCustomersQuery.Verify(d => d.GetCustomerById(It.IsAny<GetCustomerRequest>()), Times.Once());
        }

        [Fact]
        public async Task GetCustomerById_Should_Retrun_Customer_NotFoun_Error()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            _getCustomersQuery.Setup(x => x.GetCustomerById(It.IsAny<GetCustomerRequest>()))
                .ReturnsAsync(() => new GetCustomerResponse.NotFound());
            var getCustomerRequest = new GetCustomerRequest(customerId);

            // Act
            var result = await _sut.GetCustomerById(getCustomerRequest);

            // Assert
            result.IsNotFound.Should().BeTrue();
            _getCustomersQuery.Verify(d => d.GetCustomerById(It.IsAny<GetCustomerRequest>()), Times.Once());
        }

        [Fact]
        public async Task GetCustomerById_Should_Retrun_Success()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            var customerDetails = new Customer(customerId, "John", "Doe", "John@gmail.com");

            _getCustomersQuery.Setup(x => x.GetCustomerById(It.IsAny<GetCustomerRequest>()))
                .ReturnsAsync(() => new GetCustomerResponse.Success(customerDetails));

            var getCustomerRequest = new GetCustomerRequest(customerId);

            // Act
            var result = await _sut.GetCustomerById(getCustomerRequest);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _getCustomersQuery.Verify(d => d.GetCustomerById(It.IsAny<GetCustomerRequest>()), Times.Once());
        }
    }
}
