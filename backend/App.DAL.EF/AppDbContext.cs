﻿using App.Domain;
using App.Domain.Identity;
using App.Domain.Logic;
using Base.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Action = System.Action;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>,AppUserRole,
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public DbSet<ActionEntity> Actions { get; set; }
    public DbSet<ActionTypeEntity> ActionTypes { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<CurrentStock> CurrentStocks { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Reason> Reasons { get; set; }
    public DbSet<StockAudit> StockAudits { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }
    public DbSet<StorageRoom> StorageRooms { get; set; }
    public DbSet<StorageRoomInInventory> StorageRoomInInventories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    
    public DbSet<Person> Persons { get; set; }

    public DbSet<AppRefreshToken> RefreshTokens { get; set; } = default!;
    
    private readonly IUserNameResolver _userNameResolver;
    private readonly ILogger<AppDbContext> _logger;
    
    public AppDbContext(DbContextOptions<AppDbContext> options, IUserNameResolver userNameResolver,
        ILogger<AppDbContext> logger)
        : base(options)
    {
        _userNameResolver = userNameResolver;
        _logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // remove cascade delete
        foreach (var relationship in modelBuilder.Model
                     .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        /*
        // TODO - adding primary key to AppUserRole causes issues with RoleManager
        // We have custom UserRole - with separate PK and navigation for Role and User
        // override default Identity EF config
        builder.Entity<AppUserRole>().HasKey(a => new { a.UserId, a.RoleId });
        builder.Entity<AppUserRole>().HasAlternateKey(a => a.Id);
        builder.Entity<AppUserRole>().HasIndex(a => new { a.UserId, a.RoleId }).IsUnique();
        */

        modelBuilder.Entity<AppUserRole>()
            .HasOne(a => a.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<AppUserRole>()
            .HasOne(a => a.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(a => a.RoleId);
        
        modelBuilder.Entity<Inventory>()
            .Property(i => i.AllowedRoles)
            .HasColumnType("jsonb");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var addedEntries = ChangeTracker.Entries()
            .Where(e => e is { Entity: IDomainMeta });
        foreach (var entry in addedEntries)
        {
            if (entry is { Entity: IDomainMeta })
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        (entry.Entity as IDomainMeta)!.CreatedAt = DateTime.UtcNow;
                        (entry.Entity as IDomainMeta)!.CreatedBy = _userNameResolver.CurrentUserName;
                        break;
                    case EntityState.Modified:
                        entry.Property("ChangedAt").IsModified = true;
                        entry.Property("ChangedBy").IsModified = true;
                        (entry.Entity as IDomainMeta)!.ChangedAt = DateTime.UtcNow;
                        (entry.Entity as IDomainMeta)!.ChangedBy = _userNameResolver.CurrentUserName;

                        // Prevent overwriting CreatedBy/CreatedAt on update
                        entry.Property("CreatedAt").IsModified = false;
                        entry.Property("CreatedBy").IsModified = false;
                        break;
                }
            }
            
            if (entry is { Entity: IDomainUserId, State: EntityState.Modified })
            {
                // do not allow userid modification
                entry.Property("UserId").IsModified = false;
                _logger.LogWarning("UserId modification attempt. Denied!");
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}