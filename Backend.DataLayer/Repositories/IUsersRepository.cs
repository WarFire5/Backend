﻿using Backend.Core.DTOs;

namespace Backend.DataLayer.Repositories;

public interface IUsersRepository
{
    Guid AddUser(UserDto user);
    UserDto GetUserById(Guid id);
    UserDto GetUserByLogin(string login);
    void UpdateUser(UserDto user);
    void DeleteUserById(UserDto user);
    List<UserDto> GetUsers();
}