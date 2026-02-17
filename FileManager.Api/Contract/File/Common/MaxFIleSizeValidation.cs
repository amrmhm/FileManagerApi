using FileManager.Api.Setting;
using FluentValidation;

namespace FileManager.Api.Contract.File.Common;

//We Can Not Use Domain Model Upload File Request 
public class MaxFIleSizeValidation : AbstractValidator<IFormFile>
{
    public MaxFIleSizeValidation()
    {
        // C : تشير الي الفايل نفسه
        RuleFor(c => c)
            .Must((request, context) => request.Length <= FileSetting.MaxFileSizeInByte)
            .WithMessage($"Max Size File Is {FileSetting.MaxFileSizeInMb}MB .")
            .When(c => c is not null);
    }
}
