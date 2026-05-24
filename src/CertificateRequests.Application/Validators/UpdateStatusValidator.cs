using CertificateRequests.Application.DTOs;
using FluentValidation;

namespace CertificateRequests.Application.Validators;

public class UpdateStatusValidator
    : AbstractValidator<UpdateStatusDto>
{
    public UpdateStatusValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum();
    }
}