using FluentValidation;

namespace FileManager.Api.Contract.File.Common;

public class FileNameValidation : AbstractValidator<IFormFile>
{
    public FileNameValidation()
    {
        RuleFor(c => c.FileName)
     .Matches("^[A-Za-z0-9_\\-.() ]+$")
     .WithMessage("Invalid file name")
     .When(c => c is not null);
    }
}
