using Backend.Core.Models.Devices.Requests;
using FluentValidation;

namespace Backend.Core.Validators;

public class AddDeviceValidator : AbstractValidator<DeviceRequest>
{
    public AddDeviceValidator()
    {
        RuleFor(d => d.DeviceName).NotEmpty().NotNull().WithMessage("Дайте имя девайсу.");
        RuleFor(d => d.DeviceType).NotEmpty().NotNull().WithMessage("Выберите тип девайса.");
    }
}