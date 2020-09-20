using CreditManagementSystem.Common;
using CreditManagementSystem.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditManagementSystem.Data.EntityFramework.Configuration
{
    public sealed class CreditEntityTypeBuilder : CreditManagementSystemEntityTypeBuilder<Credit>
    {
        public override void Configure(EntityTypeBuilder<Credit> builder)
        {
            base.Configure(builder);

            builder.HasKey(k => k.ID);

            builder.Property(p => p.ID).HasConversion(Utils.ConvertGuidToString()).HasColumnType("char(36)");
            builder.Property(p => p.ClientID).HasConversion(Utils.ConvertGuidToString()).HasColumnType("char(36)");

            builder.HasOne(p => p.CreditStatus).WithMany().HasForeignKey(k => k.CreditStatusID).IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
