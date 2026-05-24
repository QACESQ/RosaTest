using CertificateRequests.Application.DTOs;
using FluentValidation;

namespace CertificateRequests.Application.Validators;

public class CreateRequestValidator
    : AbstractValidator<CreateRequestDto>
{
    public CreateRequestValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty();

        RuleFor(x => x.CopiesCount)
            .GreaterThan(0);

        RuleFor(x => x.Reason)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Type)
            .IsInEnum();
    }
}