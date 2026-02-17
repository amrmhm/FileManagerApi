using FileManager.Api.Contract.File.Common;
using FileManager.Api.Setting;
using FluentValidation;

namespace FileManager.Api.Contract.File;

public class UploadImageRequestValidation :AbstractValidator<UploadImageRequest>
{
    public UploadImageRequestValidation()
    {
        RuleFor(c => c.Image)
            .SetValidator(new MaxFIleSizeValidation())
            .SetValidator(new BlockedSignatureValidation());

        RuleFor(c => c.Image)
            .Must((request, context) =>
            {
                var extension = Path.GetExtension(request.Image.FileName);
                return FileSetting.AlowExtenison.Contains(extension);
            })
            .WithMessage("Can Not Allow Extension")
            .When(c => c.Image is not null);
            

           

     


      
        
    }
}
