using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shilpa.Assignment.JewelleryStore.Features.Account.Model;
using Shilpa.Assignment.JewelleryStore.Features.Print;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Shilpa.Assignment.JewelleryStore.Tests.Unit.Features.Print
{
    [TestClass]
    public class PrintControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;
        private HttpClient _client;
        private HttpResponseMessage _response;
        private string _token;
        private const string ServiceBaseURL = "http://localhost:50875/";

        private readonly PrintController _printController;
        public PrintControllerTest()
        {
            _mockMediator = new Mock<IMediator>();
            _client = new HttpClient { BaseAddress = new Uri(ServiceBaseURL) };
            _token = new MockJwtTokens().GenerateToken(new LoginResponseModel() { UserId=Guid.Parse("164531AC-E888-411B-BD77-25DC205A5E43"),Role= "Regular" } , DateTime.UtcNow);
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
            _printController = new PrintController(_mockMediator.Object);
            _printController.ControllerContext.HttpContext = new DefaultHttpContext();
        }


        [TestMethod]
        public void CalculatePriceAsyncTest()
        {
            //Arrange
            string goldPrice = "20";
            string weight = "30";
            string discount = "70";
            string tenantName = "Siemens";
            //Act
            var response = _printController.CalculatePriceAsync(goldPrice,weight,discount,tenantName)?.Result;
            //Assert
            Assert.AreEqual(response.GetType().Name, "OkObjectResult");
        }
    }
}
