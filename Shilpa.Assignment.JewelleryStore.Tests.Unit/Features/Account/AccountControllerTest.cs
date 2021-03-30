using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shilpa.Assignment.Database.DAL;
using Shilpa.Assignment.JewelleryStore.Features.Account.Controller;
using Shilpa.Assignment.JewelleryStore.Features.Account.Model;
using Shilpa.Assignment.JewelleryStore.Model;
using Shilpa.Assignment.JewelleryStore.Services.Account;
using System.Threading;

namespace Shilpa.Assignment.JewelleryStore.Tests.Unit.Features.Account
{
    [TestClass]
    public class AccountControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<JewelleryContext> _mockJewelleryContext;
        private readonly IOptions<AppSettings> _options;
        private readonly AccountController _accountController;

        public AccountControllerTest()
        {
            _mockMediator = new Mock<IMediator>();
            _mockJewelleryContext = new Mock<JewelleryContext>();
            var mockTenantSet = new MockData().GetTestTenantSet();
            var mockRoleSet = new MockData().GetTestRoleSet();
            var mockUserSet = new MockData().GetTestUsersSet();
            _options = Options.Create(new AppSettings() { EncryptionKey = "SiemensBangalore" });
            _mockJewelleryContext.Setup(s => s.Users).Returns(mockUserSet.Object);
            _mockJewelleryContext.Setup(s => s.Tenants).Returns(mockTenantSet.Object);
            _mockJewelleryContext.Setup(s => s.Roles).Returns(mockRoleSet.Object);
            _mockMediator.Setup(m => m.Send(It.IsAny<LoginUserCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<SuccessResponseDTO<LoginResponseModel>>());

            _accountController = new AccountController(_mockMediator.Object, new MockJwtTokens(), _mockJewelleryContext.Object, _options);
            _accountController.ControllerContext.HttpContext = new DefaultHttpContext();
        }


        [TestMethod]
        public void LoginAsyncTest()
        {
            //Arrange
            LoginRequestModel loginRequestModel = new LoginRequestModel() { Email = "shilpagarg.raikot@gmail.com", Password = "WelcomeNewOppourtunities", TenantName = "Siemens" };
            var response = _accountController.LoginAsync(loginRequestModel)?.Result;
            Assert.AreEqual(response.GetType().Name, "OkObjectResult");
        }
    }
}
