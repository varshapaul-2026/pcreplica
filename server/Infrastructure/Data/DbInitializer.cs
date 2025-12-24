using Microsoft.EntityFrameworkCore;
using PcReplica.Core.Entities;
using PcReplica.Core.Enums;
using System.Threading.Tasks;

namespace PcReplica.Infrastructure.Data;

public static class DbInitializer
{
    public static async System.Threading.Tasks.Task SeedAsync(ApplicationDbContext context)
    {
        // Check if database has any data
        if (await context.Users.AnyAsync())
        {
            return; // Database has been seeded
        }

        // Create sample companies
        var companies = new List<Company>
        {
            new Company
            {
                Name = "ABC Construction Corp",
                Address = "123 Main St, New York, NY 10001",
                PhoneNumber = "(555) 123-4567",
                Email = "info@abcconstruction.com"
            },
            new Company
            {
                Name = "BuildTech Solutions",
                Address = "456 Oak Ave, Los Angeles, CA 90001",
                PhoneNumber = "(555) 987-6543",
                Email = "contact@buildtech.com"
            }
        };

        context.Companies.AddRange(companies);
        await context.SaveChangesAsync();

        // Create sample users (password is "Admin123!")
        var users = new List<User>
        {
            new User
            {
                Email = "admin@abcconstruction.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                FirstName = "John",
                LastName = "Administrator",
                PhoneNumber = "(555) 111-1111",
                Role = UserRole.Admin,
                CompanyId = companies[0].Id
            },
            new User
            {
                Email = "pm@abcconstruction.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                FirstName = "Sarah",
                LastName = "Manager",
                PhoneNumber = "(555) 222-2222",
                Role = UserRole.ProjectManager,
                CompanyId = companies[0].Id
            },
            new User
            {
                Email = "contractor@buildtech.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                FirstName = "Mike",
                LastName = "Contractor",
                PhoneNumber = "(555) 333-3333",
                Role = UserRole.Contractor,
                CompanyId = companies[1].Id
            }
        };

        context.Users.AddRange(users);
        await context.SaveChangesAsync();

        // Create sample projects
        var projects = new List<Project>
        {
            new Project
            {
                Name = "Downtown Office Complex",
                Description = "Modern 10-story office building in downtown area",
                Location = "123 Business District, New York, NY",
                StartDate = DateTime.UtcNow.AddMonths(-6),
                EndDate = DateTime.UtcNow.AddMonths(6),
                Status = ProjectStatus.Active,
                Budget = 15000000,
                CompanyId = companies[0].Id
            },
            new Project
            {
                Name = "Residential Tower",
                Description = "Luxury residential tower with 200 units",
                Location = "789 Skyline Dr, Los Angeles, CA",
                StartDate = DateTime.UtcNow.AddMonths(-3),
                EndDate = DateTime.UtcNow.AddMonths(12),
                Status = ProjectStatus.Active,
                Budget = 25000000,
                CompanyId = companies[0].Id
            },
            new Project
            {
                Name = "Shopping Mall Renovation",
                Description = "Complete renovation of existing shopping center",
                Location = "456 Commerce Blvd, San Francisco, CA",
                StartDate = DateTime.UtcNow.AddMonths(-1),
                EndDate = DateTime.UtcNow.AddMonths(8),
                Status = ProjectStatus.Planning,
                Budget = 8000000,
                CompanyId = companies[1].Id
            }
        };

        context.Projects.AddRange(projects);
        await context.SaveChangesAsync();

        // Add project users
        var projectUsers = new List<ProjectUser>
        {
            new ProjectUser { ProjectId = projects[0].Id, UserId = users[0].Id, Role = "Admin" },
            new ProjectUser { ProjectId = projects[0].Id, UserId = users[1].Id, Role = "Project Manager" },
            new ProjectUser { ProjectId = projects[1].Id, UserId = users[1].Id, Role = "Project Manager" },
            new ProjectUser { ProjectId = projects[2].Id, UserId = users[2].Id, Role = "Contractor" }
        };

        context.ProjectUsers.AddRange(projectUsers);
        await context.SaveChangesAsync();

        // Create sample milestones
        var milestones = new List<Milestone>
        {
            new Milestone
            {
                Name = "Foundation Complete",
                Description = "Complete foundation and underground work",
                DueDate = DateTime.UtcNow.AddMonths(-3),
                IsCompleted = true,
                ProjectId = projects[0].Id
            },
            new Milestone
            {
                Name = "Structure Complete",
                Description = "Complete main building structure",
                DueDate = DateTime.UtcNow.AddMonths(2),
                IsCompleted = false,
                ProjectId = projects[0].Id
            },
            new Milestone
            {
                Name = "Interior Work",
                Description = "Complete all interior finishes",
                DueDate = DateTime.UtcNow.AddMonths(5),
                IsCompleted = false,
                ProjectId = projects[0].Id
            }
        };

        context.Milestones.AddRange(milestones);
        await context.SaveChangesAsync();

        // Create sample budget items
        var budgetItems = new List<BudgetItem>
        {
            new BudgetItem
            {
                CostCode = "01-001",
                Description = "Site Preparation",
                BudgetedAmount = 500000,
                ActualAmount = 485000,
                ProjectId = projects[0].Id
            },
            new BudgetItem
            {
                CostCode = "02-001",
                Description = "Foundation Work",
                BudgetedAmount = 2000000,
                ActualAmount = 2100000,
                ProjectId = projects[0].Id
            },
            new BudgetItem
            {
                CostCode = "03-001",
                Description = "Structural Steel",
                BudgetedAmount = 3500000,
                ActualAmount = 2800000,
                ProjectId = projects[0].Id
            }
        };

        context.BudgetItems.AddRange(budgetItems);
        await context.SaveChangesAsync();

        // Create sample RFIs
        var rfis = new List<Rfi>
        {
            new Rfi
            {
                Number = "RFI-001",
                Subject = "Clarification on electrical layout",
                Question = "Can you clarify the electrical panel locations on floor 3?",
                Status = RfiStatus.Submitted,
                DueDate = DateTime.UtcNow.AddDays(7),
                ProjectId = projects[0].Id,
                CreatedByUserId = users[1].Id,
                AssignedToUserId = users[0].Id
            },
            new Rfi
            {
                Number = "RFI-002",
                Subject = "HVAC system specifications",
                Question = "What are the specific requirements for the HVAC system in the lobby area?",
                Answer = "Please refer to the updated specifications in document HVAC-001-Rev2",
                Status = RfiStatus.Answered,
                DueDate = DateTime.UtcNow.AddDays(3),
                AnsweredAt = DateTime.UtcNow.AddDays(-2),
                ProjectId = projects[0].Id,
                CreatedByUserId = users[1].Id,
                AssignedToUserId = users[0].Id
            }
        };

        context.Rfis.AddRange(rfis);
        await context.SaveChangesAsync();
    }
}
