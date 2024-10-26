﻿using Customers.Domain.Models;
using Customers.Test.Integration.Fixtures;
using Customers.Test.Integration.Utils;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace Customers.Test.Integration.UpdateCustomer
{
    public class GetCustomerSuccessTest : TestServerFixture
    {
        [Fact]
        public async Task GetCustomer_Should_Return_Success()
        {
            // Arrange
            var client = Server.CreateClient();
            var customerRequest=
                new
                {
                    first_name = "John",
                    last_name = "Doe",
                    email = "test@gmail.com",
                };

            var response = await client.PostAsJsonAsync("v3/customers/add", customerRequest);
            var body = await response.Content.ReadAsStringAsync();
            var customerDetails = JsonSerializer.Deserialize<Customer>(body, Serilization.JsonSerializerOptions);

            // Act
            var results = await client.GetAsync($"v3/customers/{customerDetails.CustomerId}");

            // Assert
            results.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
