using FileManager.Api.Contract.File.Common;
using FileManager.Api.Setting;
using FluentValidation;

namespace FileManager.Api.Contract.File;

public class UploadFileRequestValidation :AbstractValidator<UploadFileRequest>
{
    public UploadFileRequestValidation()
    {
        RuleFor(c => c.File)
            //.SetValidator(new MaxFIleSizeValidation())
            .SetValidator(new BlockedSignatureValidation())
            .SetValidator(new FileNameValidation());

     


      
        
    }
}
