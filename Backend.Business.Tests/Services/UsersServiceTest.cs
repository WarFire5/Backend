using AutoMapper;
using Backend.Business.Services;
using Backend.Core.DTOs;
using Backend.Core.Exceptions;
using Backend.Core.Models.Users;
using Backend.Core.Models.Users.Requests;
using Backend.Core.Models.Users.Responses;
using Backend.Core.Validators;
using Backend.DataLayer.Repositories;
using FluentAssertions;
using FluentValidation;
using Moq;
using ValidationException = Backend.Core.Exceptions.ValidationException;

namespace Backend.Business.Tests.Services;

public class UsersServiceTest
{
    private const string _guidPattern = "^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$";
    private readonly Mock<IUsersRepository> _usersRepositoryMock;
    private readonly IMapper _mapper;
    private readonly IValidator<AddUserRequest> _addUserValidator;

    public UsersServiceTest()
    {
        _usersRepositoryMock = new Mock<IUsersRepository>();
        var mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile<UsersMappingProfile>(); });
        _mapper = mapperConfiguration.CreateMapper();
        _addUserValidator = new AddUserValidator();
    }

    [Fact]
    public void AddUser_ValidAddUserRequestSent_GuidReceived() // название метода_что воспроизводим_что получаем
    {
        // arrange настройка
        var validAddUserRequest = new AddUserRequest()
        {
            Login = "Test",
            Password = "passwordP1!",
            Email = "qq@qq.qq",
            Age = 21
        };
        var expectedGuid = Guid.NewGuid();
        _usersRepositoryMock.Setup(x => x.AddUser(It.IsAny<UserDto>())).Returns(expectedGuid);
        var sut = new UsersService(_usersRepositoryMock.Object, _mapper, _addUserValidator); // system under test тестируемая система

        // act выполнение
        var actual = sut.AddUser(validAddUserRequest);

        // assert проверка
        Assert.Equal(expectedGuid, actual);
        _usersRepositoryMock.Verify(x => x.AddUser(It.IsAny<UserDto>()), Times.Once);
    }

    [Fact]
    public void AddUser_RequestWithInvalidAgeSent_AgeErrorReceived()
    {
        // arrange
        var invalidAddUserRequest = new AddUserRequest()
        {
            Login = "Test",
            Password = "passwordP1!",
            Email = "qq@qq.qq",
            Age = 2
        };
        var expectedGuid = Guid.NewGuid();
        _usersRepositoryMock.Setup(x => x.AddUser(It.IsAny<UserDto>())).Returns(expectedGuid);
        var sut = new UsersService(_usersRepositoryMock.Object, _mapper, _addUserValidator);

        // act, assert
        Assert.Throws<ValidationException>(() => sut.AddUser(invalidAddUserRequest));
        _usersRepositoryMock.Verify(x => x.AddUser(It.IsAny<UserDto>()), Times.Never);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("password")]
    [InlineData("")]
    public void AddUser_RequestWithInvalidPasswordSent_PasswordErrorReceived(string password)
    {
        // arrange
        var invalidAddUserRequest = new AddUserRequest()
        {
            Login = "Test",
            Password = password,
            Email = "qq@qq.qq",
            Age = 21
        };
        var expectedGuid = Guid.NewGuid();
        _usersRepositoryMock.Setup(x => x.AddUser(It.IsAny<UserDto>())).Returns(expectedGuid);
        var sut = new UsersService(_usersRepositoryMock.Object, _mapper, _addUserValidator);

        // act, assert
        Assert.Throws<ValidationException>(() => sut.AddUser(invalidAddUserRequest));
        _usersRepositoryMock.Verify(x => x.AddUser(It.IsAny<UserDto>()), Times.Never);
    }

    [Fact]
    public void GetUsers_Called_UsersReceived()
    {
        // arrange
        //var validUserResponse = new UserResponse
        //{
        //    Id = Guid.NewGuid(),
        //    Login = "Test",
        //    Email = "qq@qq.qq",
        //    Age = 21
        //};
        var dto = new List<UserDto>() { new UserDto() { Login = "Test", Email = "qq@qq.qq", Age = 21 } };
        var expected = new List<UserResponse>() { new UserResponse() { Login = "Test", Email = "qq@qq.qq", Age = 21 } };
        _usersRepositoryMock.Setup(x => x.GetUsers()).Returns(dto);
        var sut = new UsersService(_usersRepositoryMock.Object, _mapper, _addUserValidator);

        // act
        var actual = sut.GetUsers();

        // assert
        actual.Should().BeEquivalentTo(expected);
        //Assert.Equal(expected, actual);
        _usersRepositoryMock.Verify(x => x.GetUsers(), Times.Once);
    }

    //[Fact]
    //public void DeleteUserById_ValidGuidSent_NoErrorsReceived()
    //{
    //    // arrange
    //    var userId = Guid.NewGuid();
    //    _usersRepositoryMock.Setup(x => x.GetUserById(userId)).Returns(new UserDto());
    //    var sut = new UsersService(_usersRepositoryMock.Object, _mapper, _addUserValidator);

    //    // act
    //    sut.DeleteUserById(userId);

    //    // assert
    //    _usersRepositoryMock.Verify(x => x.GetUserById(userId), Times.Once);
    //    _usersRepositoryMock.Verify(x => x.DeleteUserById(userId), Times.Once);
    //}

    //[Fact]
    //public void DeleteUserById_EmptyGuidSent_UserNotFoundErrorReceived()
    //{
    //    // arrange
    //    var userId = Guid.Empty;
    //    _usersRepositoryMock.Setup(x => x.GetUserById(userId)).Returns((UserDto)null);
    //    var sut = new UsersService(_usersRepositoryMock.Object, _mapper, _addUserValidator);

    //    // act, assert
    //    Assert.Throws<NotFoundException>(() => sut.DeleteUserById(userId));

    //    _usersRepositoryMock.Verify(x => x.GetUserById(userId), Times.Once);
    //    _usersRepositoryMock.Verify(x => x.DeleteUserById(userId), Times.Never);
    //}
}