using CodePractice.Core.Admin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodePractice.Core.Admin.Domain.Context;

public class AdminContext : DbContext
{
    public AdminContext()
    {
        
    }
    public AdminContext(DbContextOptions<AdminContext> options) : base(options)
    {

    }

    public DbSet<Menu> Menus { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserMenu> UserMenus { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(DefaultConnection.ConnectionString).EnableSensitiveDataLogging();
    }
}