using Moq;
using BLL.Layers;
using BLL.Interfaces;
using BLL.Models;

namespace TestProject;

public class UserLogicTests
{
    private Mock<IUserService> _mockUserService;
    private UserLogic _userLogic;

    [SetUp]
    public void Setup()
    {
        _mockUserService = new Mock<IUserService>();
        _userLogic = new UserLogic(_mockUserService.Object);
    }

    [Test]
    public void CheckIfUserEmailExists_WhenEmailExists_ReturnsSuccess()
    {
        // Arrange
        _mockUserService.Setup(x => x.GetUser(It.IsAny<string>()))
            .Returns(new User
            (1, "Test User", "sample@gmail.com", "sample123"));

        // Act
        var result = _userLogic.CheckIfUserEmailExists(email: "sample@gmail.com");

        // Assert
        Assert.IsTrue(result.IsSuccess, result.Message);
    }

    [Test]
    public void SaveUser_WhenPasswordIsShort_ReturnsFailure()
    {
        // Arrange
        var testUser = new User
            (1, "Test User", "sample@gmail.com", "short");

        // Act
        var result = _userLogic.SaveUser(testUser);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [Test]
    public void SaveUser_WhenPasswordIsLong_ReturnsSuccess()
    {
        // Arrange
        User testUser = new User(1, "Test User", "sample@gmail.com", "sample123");

        // Act
        var result = _userLogic.SaveUser(testUser);

        // Assert
        Assert.IsTrue(result.IsSuccess, result.Message);
    }

    [Test]
    public void Login_WhenCredentialsAreValid_ReturnsSuccess()
    {
        // Arrange
        _mockUserService.Setup(x => x.GetUser(It.IsAny<string>())).Returns(new User(1, "Test User", "sample@gmail.com",
            BCrypt.Net.BCrypt.HashPassword("sample123")));

        // Act
        var result = _userLogic.Login(email: "sample@gmail.com", password: "sample123");

        // Assert
        Assert.IsTrue(result.IsSuccess, result.Message);
    }
}