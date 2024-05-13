using Backend.Core.Models.Devices.Requests;
using FluentValidation;

namespace Backend.Core.Validators;

public class AddDeviceValidator : AbstractValidator<DeviceRequest>
{
    public AddDeviceValidator()
    {
        RuleFor(d => d.Name).NotEmpty().NotNull().WithMessage("Дайте имя девайсу.");
        RuleFor(d => d.Type).NotEmpty().NotNull().WithMessage("Выберите тип девайса.");
    }
}
