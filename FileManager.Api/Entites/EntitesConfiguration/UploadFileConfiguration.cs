using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileManager.Api.Entites.EntitesConfiguration;

public class UploadFileConfiguration : IEntityTypeConfiguration<UploadFile>
{
    public void Configure(EntityTypeBuilder<UploadFile> builder)
    {
        builder.Property(c => c.FileName)
            .IsRequired()
            .HasMaxLength(250);
        builder.Property(c => c.StoredFileName)
            .IsRequired()
            .HasMaxLength(250);
        builder.Property(c => c.ContentType)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(c => c.FileExtension)
            .IsRequired()
            .HasMaxLength(10);
    }
}
