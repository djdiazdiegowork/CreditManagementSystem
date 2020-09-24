using CreditManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditManagementSystem.Data.EntityFramework.Configuration
{
    public sealed class CreditStatusEntityTypeBuilder : CreditManagementSystemEntityTypeBuilder<CreditStatus>
    {
        public override void Configure(EntityTypeBuilder<CreditStatus> builder)
        {
            base.Configure(builder);

            builder.HasKey(k => k.ID);

            builder.Property(p => p.ID).ValueGeneratedNever();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
    }
}
