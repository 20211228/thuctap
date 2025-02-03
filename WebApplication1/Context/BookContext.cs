using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class BookContext : IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        Guid,
        IdentityUserClaim<Guid>,
        ApplicationUserRole,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookContext(DbContextOptions<BookContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).HasMaxLength(150).IsRequired();
                entity.Property(e => e.Description);
                entity.Property(e => e.Price).HasDefaultValue(0);

                entity.Property(e => e.Quantity).HasDefaultValue(0);
            });


            // Cấu hình ApplicationUserRole
            modelBuilder.Entity<ApplicationUserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId }); // Khóa chính kết hợp
                entity.Property(ur => ur.Created).HasDefaultValueSql("GETDATE()");
            });

            // Cấu hình ApplicationUser
            modelBuilder.Entity<ApplicationUser>().Property(u => u.FirstName).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<ApplicationUser>().Property(u => u.LastName).HasMaxLength(100);

            // Cấu hình ApplicationRole
            modelBuilder.Entity<ApplicationRole>().Property(r => r.Description).HasMaxLength(255);
            
        }

        public override int SaveChanges()
        {
            ApplyBaseEntityRules();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyBaseEntityRules();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyBaseEntityRules()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            var currentUserId = GetCurrentUserId();

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedOn = now;
                    entity.CreatedBy = currentUserId;
                }
            }
        }

        private Guid? GetCurrentUserId()
        {
            // Lấy UserId từ Claims trong token
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return string.IsNullOrEmpty(userIdClaim) ? (Guid?)null : Guid.Parse(userIdClaim);
        }

    }
}
