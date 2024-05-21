using Backend.Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Backend.DataLayer.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    private readonly ILogger _logger = Log.ForContext<UsersRepository>();

    public UsersRepository(MainerWomanContext context) : base(context)
    {
    }

    public Guid AddUser(UserDto user)
    {
        _ctx.Users.Add(user);
        _ctx.SaveChanges();

        _logger.Information($"Возвращаем Id {user.Id} добавленного пользователя.");
        return user.Id;
    }

    public UserDto GetUserById(Guid id)
    {
        _logger.Information($"Идём в базу данных искать пользователя с Id {id}.");
        return _ctx.Users.FirstOrDefault(u => u.Id == id);
    }

    public UserDto GetUserByLogin(string login)
    {
        var user = _ctx.Users.FirstOrDefault(u => u.Login == login);

        _logger.Information($"Возвращаем пользователя с логином {login}.");
        return user;
    }

    public void UpdateUser(UserDto user)
    {
        _logger.Information($"Обновляем данные пользователя.");
        _ctx.Users.Update(user);
        _ctx.SaveChanges();
    }

    public void DeleteUserById(UserDto user)
    {
        _logger.Information($"Удаляем пользователя.");
        _ctx.Users.Remove(user);
        _ctx.SaveChanges();
    }

    public List<UserDto> GetUsers()
    {
        _logger.Information($"Получаем список пользователей.");
        return _ctx.Users.ToList();
    }

    public List<OperationWithCoinsDto> GetOperationWithCoinsByDeviceId(Guid deviceId)
    {
        _logger.Information($"Ищем операции с коинами по Id девайса {deviceId}.");
        var coins = _ctx.OperationsWithCoins.Include(o => o.Device.Owner).Where(o => o.Device.Id == deviceId).ToList();
        if (coins == null)
        {
            coins = new List<OperationWithCoinsDto>();
            _logger.Information($"У девайса с Id {deviceId} нет операций. Создаём пустой список.");
        }

        _logger.Information($"Отправляем список операций для девайса с Id {deviceId}.");
        return coins;
    }
}
