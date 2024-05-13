using Moq;
using Backend.Business.Services;
using Backend.Core.DTOs;
using Backend.DataLayer.Repositories;
using System.Net.Cache;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Backend.Core.Models.Users.Requests;
using AutoMapper;
using FluentValidation;
using Backend.Core.Validators;
using Backend.Core.Models.Users;

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
    public void AddUser_ValidAddUserRequestSent_GuidReceived()
    {
        // arrange настройка
        var validAddUserRequest = new AddUserRequest()
        {
            Login = "Test",
            Password = "passwordP1!",
            Email = "qq@qq.qq",
            Age = 21,
        };
        var expectedGuid = Guid.NewGuid();
        _usersRepositoryMock.Setup(x => x.AddUser(It.IsAny<UserDto>())).Returns(expectedGuid);
        var sut = new UsersService(_usersRepositoryMock.Object,_mapper,_addUserValidator); // system under test тестируемая система

        // act выполнение
        var actual = sut.AddUser(validAddUserRequest);

        // assert проверка
        Assert.Equal(expectedGuid, actual);
        _usersRepositoryMock.Verify(x => x.AddUser(It.IsAny<UserDto>()), Times.Once);
    }

    //[Fact]
    //public void AddUser_DtoWithInvalidAgeSent_AgeErrorReceived()
    //{
    //    // arrange
    //    var invalidUserDto = new UserDto()
    //    {
    //        Age = 12,
    //        Login = "Test",
    //        Email = "qq@qq.qq",
    //        Password = "password"
    //    };
    //    var expectedGuid = Guid.NewGuid();
    //    _usersRepositoryMock.Setup(x => x.AddUser(It.IsAny<UserDto>())).Returns(expectedGuid);
    //    var sut = new UsersService(_usersRepositoryMock.Object);

    //    // act, assert
    //    Assert.Throws<ValidationException>(() => sut.AddUser(invalidUserDto));
    //    _usersRepositoryMock.Verify(x => x.AddUser(It.IsAny<UserDto>()), Times.Never);
    //}

    //[Theory]
    //[InlineData(null)]
    //[InlineData("passwor")]
    //[InlineData("")]
    //public void AddUser_DtoWithInvalidPasswordSent_PasswordErrorReceived()
    //{
    //    // arrange
    //    var invalidUserDto = new UserDto()
    //    {
    //        Age = 18,
    //        Login = "Test",
    //        Email = "qq@qq.qq",
    //        Password = password,
    //    };
    //    var expectedGuid = Guid.NewGuid();
    //    _usersRepositoryMock.Setup(x => x.AddUser(It.IsAny<UserDto>())).Returns(expectedGuid);
    //    var sut = new UsersService(_usersRepositoryMock.Object);

    //    // act, assert
    //    Assert.Throws<ValidationException>(() => sut.AddUser(invalidUserDto));
    //    _usersRepositoryMock.Verify(x => x.AddUser(It.IsAny<UserDto>()), Times.Never);
    //}

    //[Fact]
    //public void GetUsers_Called_UsersReceived()
    //{
    //    // arrange
    //    var expected = new List<UserDto>() { new UserDto() { Email = "qq@qq.qq" }, new UserDto() { Email = "ww@ww.ww" } };
    //    _usersRepositoryMock.Setup(x => x.GetUsers()).Returns(expected);
    //    var sut = new UsersService(_usersRepositoryMock.Object);

    //    // act
    //    var actual = sut.GetUsers();

    //    // assert
    //    Assert.Equal(expected, actual);
    //    _usersRepositoryMock.Verify(x => x.GetUsers(), Times.Once);
    //}

    //[Fact]
    //public void DeleteUserById_ValidGuidSent_NoErrorsReceived()
    //{
    //    // arrange
    //    var userId = Guid.NewGuid();
    //    _usersRepositoryMock.Setup(x => x.DeleteUserById(userId)).Returns(expected);
    //    var sut = new UsersService(_usersRepositoryMock.Object);

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
    //    var sut = new UsersService(_usersRepositoryMock.Object);

    //    // act, assert
    //    Assert.Throws<NotFoundException>(() => sut.DeleteUserById(userId));

    //    _usersRepositoryMock.Verify(x => x.GetUserById(userId), Times.Once);
    //    _usersRepositoryMock.Verify(x => x.DeleteUserById(userId), Times.Never);
    //}
}