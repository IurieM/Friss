using Document.Common;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Identity.Api.Validators
{
    public class FileValidator : AbstractValidator<IFormFile>
    {
        public FileValidator()
        {
            RuleFor(file => file.FileName).NotEmpty().WithErrorCode(Constants.ErrorCodes.Required);
            RuleFor(file => file.Length).GreaterThan(0).WithErrorCode(Constants.ErrorCodes.Required);
        }
    }
}
