using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shilpa.Assignment.Database.DAL;
using Shilpa.Assignment.JewelleryStore.Features.Account.Model;
using Shilpa.Assignment.JewelleryStore.Model;
using Shilpa.Assignment.JewelleryStore.Services.Account;
using Shilpa.Assignment.JewelleryStore.Tests.Unit.Features.Account;
using System.Threading.Tasks;

namespace Shilpa.Assignment.JewelleryStore.Tests.Unit.Services.Account
{
    [TestClass]
    public class LoginUserCommandHandlerTest
    {
        private readonly Mock<JewelleryContext> _mockJewelleryContext;
        private LoginUserCommandHandler _loginUserCommandHandler;
        public LoginUserCommandHandlerTest()
        {

            _mockJewelleryContext = new Mock<JewelleryContext>();
            var mockTenantSet = new MockData().GetTestTenantSet();
            var mockRoleSet = new MockData().GetTestRoleSet();
            var mockUserSet = new MockData().GetTestUsersSet();
            _mockJewelleryContext.Setup(s => s.Users).Returns(mockUserSet.Object);
            _mockJewelleryContext.Setup(s => s.Tenants).Returns(mockTenantSet.Object);
            _mockJewelleryContext.Setup(s => s.Roles).Returns(mockRoleSet.Object);
            _loginUserCommandHandler = new LoginUserCommandHandler(_mockJewelleryContext.Object);
        }

        [TestMethod,TestCategory("Login")]
        public async Task Handle_should_return_user_details()
        {
            //Arrange
            var query = new LoginUserCommand()
            {
                Email = "shilpagarg.raikot@gmail.com",
                SceretKey = "SiemensBangalore",
                TenantName = "Siemens",
                Password = "WelcomeNewOppourtunities",
                JWTAuth = new MockJwtTokens()
            };

            //ACT
            var response = await _loginUserCommandHandler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.IsInstanceOfType(response, typeof(SuccessResponseDTO<LoginResponseModel>));
            Assert.AreEqual(response.StatusInfo.Status, Constants.Sucessfully_logged_in);

        }
        [TestMethod, TestCategory("Login")]
        public async Task Handle_should_return_user_Email_required()
        {
            //Arrange
            var query = new LoginUserCommand()
            {
                Email = "",
                SceretKey = "SiemensBangalore",
                TenantName = "Siemens",
                Password = "WelcomeNewOppourtunities",
                JWTAuth = new MockJwtTokens()
            };

            //ACT
            var response = await _loginUserCommandHandler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.IsInstanceOfType(response, typeof(SuccessResponseDTO<LoginResponseModel>));
            Assert.AreEqual(response.StatusInfo.Status, Constants.Email_is_required);

        }
        [TestMethod, TestCategory("Login")]
        public async Task Handle_should_return_user_Password_required()
        {
            //Arrange
            var query = new LoginUserCommand()
            {
                Email = "shilpagarg.raikot@gmail.com",
                SceretKey = "SiemensBangalore",
                TenantName = "Siemens",
                Password = "",
                JWTAuth = new MockJwtTokens()
            };

            //ACT
            var response = await _loginUserCommandHandler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.IsInstanceOfType(response, typeof(SuccessResponseDTO<LoginResponseModel>));
            Assert.AreEqual(response.StatusInfo.Status, Constants.Password_is_required);

        }
        [TestMethod, TestCategory("Login")]
        public async Task Handle_should_return_Client_Doesnt_Exist()
        {
            //Arrange
            var query = new LoginUserCommand()
            {
                Email = "shilpagarg.raikot@gmail.com",
                SceretKey = "SiemensBangalore",
                TenantName = "",
                Password = "WelcomeNewOppourtunities",
                JWTAuth = new MockJwtTokens()
            };

            //ACT
            var response = await _loginUserCommandHandler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.IsInstanceOfType(response, typeof(SuccessResponseDTO<LoginResponseModel>));
            Assert.AreEqual(response.StatusInfo.Status, Constants.Client_doesn_not_exist);

        }
        [TestMethod, TestCategory("Login")]
        public async Task Handle_should_return_username_or_password_Incorrect_by_passing_invalid_username()
        {
            //Arrange
            var query = new LoginUserCommand()
            {
                Email = "shilpagarg.raikot1@gmail.com",
                SceretKey = "SiemensBangalore",
                TenantName = "Siemens",
                Password = "WelcomeNewOppourtunities",
                JWTAuth = new MockJwtTokens()
            };

            //ACT
            var response = await _loginUserCommandHandler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.IsInstanceOfType(response, typeof(SuccessResponseDTO<LoginResponseModel>));
            Assert.AreEqual(response.StatusInfo.Status, Constants.Username_or_password_is_incorrect);

        }
        [TestMethod, TestCategory("Login")]
        public async Task Handle_should_return_username_or_password_Incorrect_by_passing_invalid_Password()
        {
            //Arrange
            var query = new LoginUserCommand()
            {
                Email = "shilpagarg.raikot@gmail.com",
                SceretKey = "SiemensBangalore",
                TenantName = "Siemens",
                Password = "WelcomeNewOppourtunities1",
                JWTAuth = new MockJwtTokens()
            };

            //ACT
            var response = await _loginUserCommandHandler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.IsInstanceOfType(response, typeof(SuccessResponseDTO<LoginResponseModel>));
            Assert.AreEqual(response.StatusInfo.Status, Constants.Username_or_password_is_incorrect);

        }
        
       
    }
}
