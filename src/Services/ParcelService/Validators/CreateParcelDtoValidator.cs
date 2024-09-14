using FluentValidation;
using ParcelService.DTOs;

namespace ParcelService.Validators;

public class CreateParcelDtoValidator: AbstractValidator<CreateParcelDto>
{
    public CreateParcelDtoValidator()
    {
        RuleFor(p => p.Weight).GreaterThan(0).WithMessage("Weight must be greater than zero."); 
        RuleFor(p => p.SenderEmail).EmailAddress().WithMessage("Invalid sender email format."); 
        RuleFor(p => p.ReceiverEmail).EmailAddress().WithMessage("Invalid receiver email format.");

        RuleFor(p => p).Must(p => p.SenderEmail != p.ReceiverEmail)
            .WithMessage("Sender email cannot be the same as receiver email.");

        RuleFor(p => p.FromCity).NotEmpty().WithMessage("Fill in the field 'FromCity'");

        RuleFor(p => p.ToCity).NotEmpty().WithMessage("Fill in the field 'ToCity'");

        RuleFor(p => p).Must(p => p.FromCity != p.ToCity)
            .WithMessage("FromCity cannot be the same as ToCity.");

        RuleFor(p => p.Country).NotEmpty().WithMessage("Fill in the field 'Country'");

    }
}
