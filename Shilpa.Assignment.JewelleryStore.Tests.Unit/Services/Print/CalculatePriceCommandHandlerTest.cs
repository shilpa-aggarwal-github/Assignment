using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shilpa.Assignment.JewelleryStore.Model;
using Shilpa.Assignment.JewelleryStore.Services.Print;
using System.Threading.Tasks;

namespace Shilpa.Assignment.JewelleryStore.Tests.Unit.Services.Print
{
    [TestClass]
   public class CalculatePriceCommandHandlerTest
    {
        private CalculatePriceCommandHandler _calculatePriceCommandHandler;
        public CalculatePriceCommandHandlerTest()
        {
            _calculatePriceCommandHandler = new CalculatePriceCommandHandler();
        }
        [TestMethod, TestCategory("Print")]
        public async Task Handle_should_Retuen_sixty()
        {
            //Arrange
            var query = new CalculatePriceCommand()
            {
             GoldPrice="20",
             Weight="3",
             Discount="",
             TenantName="Siemens"
            };

            //ACT
            var response = await _calculatePriceCommandHandler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.IsInstanceOfType(response, typeof(SuccessResponseDTO<string>));
            Assert.AreEqual(response.StatusInfo.Status, "price calculated sucessfully");
            Assert.AreEqual(response.Response, "60");

        }
        [TestMethod, TestCategory("Print")]
        public async Task Handle_should_Retuen_zero()
        {
            //Arrange
            var query = new CalculatePriceCommand()
            {
                GoldPrice = "20",
                Weight = "0",
                Discount = "30",
                TenantName = "Siemens"
            };

            //ACT
            var response = await _calculatePriceCommandHandler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.IsInstanceOfType(response, typeof(SuccessResponseDTO<string>));
            Assert.AreEqual(response.StatusInfo.Status, "price calculated sucessfully");
            Assert.AreEqual(response.Response, "0");

        }
        [TestMethod, TestCategory("Print")]
        public async Task Handle_should_Retuen_ThreeHundred()
        {
            //Arrange
            var query = new CalculatePriceCommand()
            {
                GoldPrice = "20",
                Weight = "30",
                Discount = "2",
                TenantName = "Siemens"
            };

            //ACT
            var response = await _calculatePriceCommandHandler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.IsInstanceOfType(response, typeof(SuccessResponseDTO<string>));
            Assert.AreEqual(response.StatusInfo.Status, "price calculated sucessfully");
            Assert.AreEqual(response.Response, "300");

        }
        public async Task Handle_should_Retuen_zero_by_sending_empty_Goldprice_and_weight()
        {
            //Arrange
            var query = new CalculatePriceCommand()
            {
                GoldPrice = "",
                Weight = "",
                Discount = "2",
                TenantName = "Siemens"
            };

            //ACT
            var response = await _calculatePriceCommandHandler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.IsInstanceOfType(response, typeof(SuccessResponseDTO<string>));
            Assert.AreEqual(response.StatusInfo.Status, "price calculated sucessfully");
            Assert.AreEqual(response.Response, "0");

        }
    }
}

