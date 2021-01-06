using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBAI.Data.Entity;
using FBAI.Data.Entity.Account;
using FBAI.Data.Entity.EmployeeAttachment;
using FBAI.Data.Entity.MasterData;
using FBAI.Models.MasterData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FBAI.Data
{
    public class BlogDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogDbContext(DbContextOptions<BlogDbContext> options, IHttpContextAccessor _httpContextAccessor) :
            base(options)
        {
            this._httpContextAccessor = _httpContextAccessor;
        }


        #region Master Data

        public DbSet<Gender> Genders { get; set; }
        public DbSet<CostingType> CostingTypes { get; set; }
        public DbSet<QtyMeasurementType> QtyMeasurementTypes { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<Year> Years { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Thana> Thanas { get; set; }
        public DbSet<PostOffice> PostOffices { get; set; }
        public DbSet<StoreUserMobileNumber> StoreUserMobileNumbers { get; set; }
        public DbSet<AccountingMaxCode> AccountingMaxCodes { get; set; }
        public DbSet<AttachmentMaxCode> AttachmentMaxCodes { get; set; }
        public DbSet<DipositeType> DipositeMoneyFors { get; set; }


        #endregion

        #region EmployeeAttachment

        public DbSet<AttachmentMaster> AttachmentMasters { get; set; }
        public DbSet<AttachmentDetails> AttachmentDetails { get; set; }

        #endregion

        #region Account

        public DbSet<RegularCostingMaster> RegularCostingMasters { get; set; }
        public DbSet<RegularCostingDetails> RegularCostingDetails { get; set; }
        public DbSet<Salary> Salarys { get; set; }
        public DbSet<BoucherMaster> BoucherMasters { get; set; }
        public DbSet<BoucherDetails> BoucherDetailses { get; set; }

        #endregion

        #region Settings Configs
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {

            var entities = ChangeTracker.Entries().Where(x => x.Entity is Base && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = !string.IsNullOrEmpty(_httpContextAccessor?.HttpContext?.User?.Identity?.Name)
                ? _httpContextAccessor.HttpContext.User.Identity.Name
                : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Base)entity.Entity).createdAt = DateTime.Now.AddHours(-12);
                    ((Base)entity.Entity).createdBy = currentUsername;
                }
                else
                {
                    entity.Property("createdAt").IsModified = false;
                    entity.Property("createdBy").IsModified = false;
                    ((Base)entity.Entity).updatedAt = DateTime.Now.AddHours(-12);
                    ((Base)entity.Entity).updatedBy = currentUsername;
                }

            }
        }
        #endregion
    }

}
