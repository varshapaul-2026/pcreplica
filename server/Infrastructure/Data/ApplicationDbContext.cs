using Microsoft.EntityFrameworkCore;
using PcReplica.Core.Entities;

namespace PcReplica.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectUser> ProjectUsers => Set<ProjectUser>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<Core.Entities.Task> Tasks => Set<Core.Entities.Task>();
    public DbSet<Milestone> Milestones => Set<Milestone>();
    public DbSet<BudgetItem> BudgetItems => Set<BudgetItem>();
    public DbSet<ChangeOrder> ChangeOrders => Set<ChangeOrder>();
    public DbSet<Rfi> Rfis => Set<Rfi>();
    public DbSet<DailyLog> DailyLogs => Set<DailyLog>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            
            entity.HasOne(e => e.Company)
                .WithMany(c => c.Users)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Company configuration
        modelBuilder.Entity<Company>(entity =>
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
        });

        // Project configuration
        modelBuilder.Entity<Project>(entity =>
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Budget).HasPrecision(18, 2);
            
            entity.HasOne(e => e.Company)
                .WithMany(c => c.Projects)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ProjectUser configuration
        modelBuilder.Entity<ProjectUser>(entity =>
        {
            entity.HasOne(e => e.Project)
                .WithMany(p => p.ProjectUsers)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.User)
                .WithMany(u => u.ProjectUsers)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasIndex(e => new { e.ProjectId, e.UserId }).IsUnique();
        });

        // Document configuration
        modelBuilder.Entity<Document>(entity =>
        {
            entity.Property(e => e.FileName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.FilePath).IsRequired();
            
            entity.HasOne(e => e.Project)
                .WithMany(p => p.Documents)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Task configuration
        modelBuilder.Entity<Core.Entities.Task>(entity =>
        {
            entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
            
            entity.HasOne(e => e.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Milestone)
                .WithMany(m => m.Tasks)
                .HasForeignKey(e => e.MilestoneId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Milestone configuration
        modelBuilder.Entity<Milestone>(entity =>
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            
            entity.HasOne(e => e.Project)
                .WithMany(p => p.Milestones)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // BudgetItem configuration
        modelBuilder.Entity<BudgetItem>(entity =>
        {
            entity.Property(e => e.CostCode).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            entity.Property(e => e.BudgetedAmount).HasPrecision(18, 2);
            entity.Property(e => e.ActualAmount).HasPrecision(18, 2);
            
            entity.HasOne(e => e.Project)
                .WithMany(p => p.BudgetItems)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ChangeOrder configuration
        modelBuilder.Entity<ChangeOrder>(entity =>
        {
            entity.Property(e => e.Number).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            
            entity.HasOne(e => e.BudgetItem)
                .WithMany(b => b.ChangeOrders)
                .HasForeignKey(e => e.BudgetItemId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // RFI configuration
        modelBuilder.Entity<Rfi>(entity =>
        {
            entity.Property(e => e.Number).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Subject).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Question).IsRequired();
            
            entity.HasOne(e => e.Project)
                .WithMany(p => p.Rfis)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.CreatedByUser)
                .WithMany(u => u.CreatedRfis)
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.AssignedToUser)
                .WithMany(u => u.AssignedRfis)
                .HasForeignKey(e => e.AssignedToUserId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // DailyLog configuration
        modelBuilder.Entity<DailyLog>(entity =>
        {
            entity.Property(e => e.Temperature).HasPrecision(5, 2);
            
            entity.HasOne(e => e.Project)
                .WithMany(p => p.DailyLogs)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.CreatedByUser)
                .WithMany(u => u.DailyLogs)
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Comment configuration
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.Content).IsRequired();
            
            entity.HasOne(e => e.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.Rfi)
                .WithMany(r => r.Comments)
                .HasForeignKey(e => e.RfiId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(e => e.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Apply global query filter for soft delete
        modelBuilder.Entity<User>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Company>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Project>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Document>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Core.Entities.Task>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Milestone>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<BudgetItem>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<ChangeOrder>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Rfi>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<DailyLog>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Comment>().HasQueryFilter(e => !e.IsDeleted);
    }
}
