using FileManager.Api.Contract.File.Common;
using FluentValidation;

namespace FileManager.Api.Contract.File;

public class UploadManyFileRequestValidation : AbstractValidator<UploadManyFileRequest>
{
    public UploadManyFileRequestValidation()
    {
        RuleForEach(c => c.Files.Files)
            .SetValidator(new MaxFIleSizeValidation())
            .SetValidator(new BlockedSignatureValidation())
            .SetValidator(new FileNameValidation());
    }
}
