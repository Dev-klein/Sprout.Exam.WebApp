using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sprout.Exam.WebApp.Models;

namespace Sprout.Exam.WebApp.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<HrEmployee>().HasIndex(
                hrEmployee => new { hrEmployee.Name, hrEmployee.BirthDate, hrEmployee.TIN, hrEmployee.EmployeeType, hrEmployee.IsDeleted }).IsUnique(false);
        }

        public DbSet<HrEmployee> HrEmployees { get; set; }
    }
}
