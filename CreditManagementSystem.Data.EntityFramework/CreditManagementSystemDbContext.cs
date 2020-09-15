using CreditManagementSystem.Common.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CreditManagementSystem.Data.EntityFramework
{
    public class CreditManagementSystemDbContext : EFDbContext
    {
        public CreditManagementSystemDbContext(DbContextOptions<CreditManagementSystemDbContext> options)
            : base(options)
        {
        }
    }
}