using FileManager.Api.Setting;
using FluentValidation;

namespace FileManager.Api.Contract.File;

public class UploadFileRequestValidation :AbstractValidator<UploadFileRequest>
{
    public UploadFileRequestValidation()
    {
        //RuleFor(c => c.File)
        //    .Must((request, context) => request.File.Length <= FileSetting.MaxFileSizeInByte)
        //    .WithMessage($"Max Size File Is {FileSetting.MaxFileSizeInMb}MB .")
        //    .When(c => c.File is not null);

        RuleFor(c => c.File)
            .Must((request,content) =>
            {
                BinaryReader binary = new(request.File.OpenReadStream());
                var bytes = binary.ReadBytes(2);
                var fileSequenceHex = BitConverter.ToString(bytes);
                foreach(var signature in FileSetting.BlockSignature)
                {
                    if (signature.Equals(fileSequenceHex))
                        return false;
                }
                return true;
            })
            .WithMessage("Can Not Allowed Content File")
            .When(c => c.File is not null);
    }
}
