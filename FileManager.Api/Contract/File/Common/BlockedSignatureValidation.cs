using FileManager.Api.Setting;
using FluentValidation;

namespace FileManager.Api.Contract.File.Common;

public class BlockedSignatureValidation : AbstractValidator<IFormFile>
{
    public BlockedSignatureValidation()
    {
        RuleFor(c => c)
         .Must((request, content) =>
         {
             BinaryReader binary = new(request.OpenReadStream());
             var bytes = binary.ReadBytes(2);
             var fileSequenceHex = BitConverter.ToString(bytes);
             foreach (var signature in FileSetting.BlockSignature)
             {
                 if (signature.Equals(fileSequenceHex))
                     return false;
             }
             return true;
         })
         .WithMessage("Can Not Allowed Content File")
         .When(c => c is not null);
    }
}
