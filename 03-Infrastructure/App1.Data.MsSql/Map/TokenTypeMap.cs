using App1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App1.Data.MsSql.Map
{
    internal class TokenTypeMap : IEntityTypeConfiguration<TokenType>
    {
        public void Configure(EntityTypeBuilder<TokenType> builder)
        {
            builder.ToTable("TokenTypes");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id)
                .HasColumnName("TypeId")
                .IsRequired();

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        }
    }
}
