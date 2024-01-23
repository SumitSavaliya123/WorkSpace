using Common.Constants;
using Common.Utils;
using Entities.Abstract;
using Entities.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static Common.Enums.EntityStatus;

namespace DataAccessLayer.Data
{
    public class AppDbContext: DbContext
    {
        #region Constructor

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion Constructor

        #region Methods
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry> entries = ChangeTracker
                    .Entries()
                    .Where(e => (IsAuditableEntity(e.Entity.GetType()) || IsTimestampedEntity(e.Entity.GetType())) &&
                        (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (EntityEntry entityEntry in entries)
            {
                if (IsAuditableEntity(entityEntry.Entity.GetType()))
                {
                    dynamic? baseEntity = (dynamic)entityEntry.Entity;

                    long userId = GetUserId();

                    if (entityEntry.State == EntityState.Added)
                    {
                        baseEntity.CreatedOn = DateUtil.UtcNow;
                        baseEntity.CreatedBy = userId;
                    }
                    if (entityEntry.State == EntityState.Modified)
                    {
                        baseEntity.UpdatedOn = DateUtil.UtcNow;
                        baseEntity.UpdatedBy = userId;
                    }
                }
                else if (IsTimestampedEntity(entityEntry.Entity.GetType()))
                {
                    dynamic? timeStampedEntity = (dynamic)entityEntry.Entity;

                    if (entityEntry.State == EntityState.Added)
                    {
                        timeStampedEntity.CreatedOn = DateUtil.UtcNow;
                    }
                    if (entityEntry.State == EntityState.Modified)
                    {
                        timeStampedEntity.UpdatedOn = DateUtil.UtcNow;
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        private long GetUserId()
        {
            System.Security.Claims.Claim? userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("UserId");
            if (userIdClaim != null && long.TryParse(userIdClaim.Value, out long userId))
            {
                return userId;
            }
            return 1; // Default value if the claim is not present or not parseable
        }


        private static bool IsTimestampedEntity(Type entityType)
        {
            Type? baseType = entityType.BaseType;
            while (baseType != null)
            {
                if (baseType.IsGenericType &&
                    baseType.GetGenericTypeDefinition() == typeof(TimestampedEntity<>))
                {
                    return true;
                }
                baseType = baseType.BaseType;
            }
            return false;
        }

        private static bool IsAuditableEntity(Type entityType)
        {
            Type? baseType = entityType.BaseType;
            while (baseType != null)
            {
                if (baseType.IsGenericType &&
                    baseType.GetGenericTypeDefinition() == typeof(AuditableEntity<>))
                {
                    return true;
                }
                baseType = baseType.BaseType;
            }
            return false;
        }

        #endregion

        #region DbSets

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Gender> Genders { get; set; }

        public virtual DbSet<UserRefreshTokens> UserRefreshTokens { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("gender");
                entity.Property(e => e.Title).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.Title).IsUnique();
                entity.HasKey(e => e.Id).HasName("PK_Gender_Id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.CreatedOn).HasDefaultValueSql(SystemConstants.DEFAULT_DATETIME);
                entity.Property(e => e.Status).HasDefaultValue(UserStatusTypes.ACTIVE);

                entity
                    .HasOne(a => a.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                entity
                    .HasOne(a => a.UpdatedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.UpdatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<UserRefreshTokens>(entity =>
            {
                entity.ToTable("userRefreshToken");
                entity.HasIndex(e => e.Id);
            });



            #region Seed_Data

            modelBuilder.Entity<Gender>().HasData(
                 new Gender { Id = 1, Title = "Male" },
                 new Gender { Id = 2, Title = "Female" }
            );
            #endregion

        }

    }
}
