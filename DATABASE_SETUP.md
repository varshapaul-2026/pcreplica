# Database Setup Guide

## Prerequisites

1. **PostgreSQL Installation**
   - Install PostgreSQL 14 or later
   - Start the PostgreSQL service
   - Create a database user with appropriate permissions

2. **EF Core Tools**
   - Install globally if not already installed:
   ```bash
   dotnet tool install --global dotnet-ef
   ```

## Setup Steps

### Step 1: Configure Connection String

Edit `server/API/appsettings.json` and update the connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=pcreplica;Username=postgres;Password=your_password_here"
  }
}
```

Replace `your_password_here` with your PostgreSQL password.

### Step 2: Create Initial Migration

From the `server/Infrastructure` directory:

```bash
cd server/Infrastructure
dotnet ef migrations add InitialCreate --startup-project ../API --context ApplicationDbContext
```

This will create a `Migrations` folder with the initial migration files.

### Step 3: Apply Migration to Database

```bash
dotnet ef database update --startup-project ../API
```

This will:
- Create the `pcreplica` database if it doesn't exist
- Create all tables and relationships
- Apply the database schema

### Step 4: Verify Database

You can verify the database was created successfully by:

1. **Using psql:**
   ```bash
   psql -U postgres
   \c pcreplica
   \dt
   ```

2. **Using pgAdmin:**
   - Connect to your PostgreSQL server
   - Navigate to Databases > pcreplica > Schemas > public > Tables

## Seed Data

The application automatically seeds sample data when run in Development mode. The seed data includes:

- **2 Companies**: ABC Construction Corp, BuildTech Solutions
- **3 Users**: 
  - admin@abcconstruction.com (Admin role)
  - pm@abcconstruction.com (Project Manager role)
  - contractor@buildtech.com (Contractor role)
  - All passwords: `Admin123!`
- **3 Projects**: Various construction projects
- **Project assignments**: Users assigned to projects
- **Milestones, Budget Items, and RFIs**: Sample data for testing

## Database Schema

### Main Tables

1. **Users** - User accounts with authentication
   - Id, Email, PasswordHash, FirstName, LastName, PhoneNumber, Role, CompanyId

2. **Companies** - Organizations/companies
   - Id, Name, Address, PhoneNumber, Email

3. **Projects** - Construction projects
   - Id, Name, Description, Location, StartDate, EndDate, Status, Budget, CompanyId

4. **ProjectUsers** - Many-to-many relationship between Projects and Users
   - Id, ProjectId, UserId, Role

5. **Documents** - Project documents
   - Id, FileName, FilePath, FileSize, ContentType, Category, Version, ProjectId

6. **Tasks** - Project tasks
   - Id, Title, Description, DueDate, IsCompleted, ProjectId, AssignedToUserId, MilestoneId

7. **Milestones** - Project milestones
   - Id, Name, Description, DueDate, IsCompleted, ProjectId

8. **BudgetItems** - Budget line items
   - Id, CostCode, Description, BudgetedAmount, ActualAmount, ProjectId

9. **ChangeOrders** - Budget change orders
   - Id, Number, Description, Amount, BudgetItemId

10. **Rfis** - Requests for Information
    - Id, Number, Subject, Question, Answer, Status, DueDate, AnsweredAt, ProjectId, CreatedByUserId, AssignedToUserId

11. **DailyLogs** - Daily construction logs
    - Id, LogDate, WeatherConditions, Temperature, WorkCompleted, LaborHours, EquipmentUsed, SafetyIncidents, Notes, ProjectId, CreatedByUserId

12. **Comments** - Comments on RFIs and other entities
    - Id, Content, UserId, RfiId, ParentCommentId

### Key Features

- **Soft Delete**: All entities support soft delete (IsDeleted flag)
- **Audit Fields**: CreatedAt, UpdatedAt timestamps on all entities
- **Foreign Key Relationships**: Proper relationships with cascade/restrict delete behavior
- **Indexes**: Unique index on User.Email, composite index on ProjectUser
- **Query Filters**: Global query filter to exclude soft-deleted records

## Troubleshooting

### Connection Issues

If you get connection errors:
1. Verify PostgreSQL is running
2. Check the connection string in appsettings.json
3. Ensure the database user has proper permissions
4. Try connecting with psql to verify credentials

### Migration Issues

If migrations fail:
1. Delete the Migrations folder
2. Drop the database: `DROP DATABASE pcreplica;`
3. Recreate the initial migration
4. Apply the migration again

### Seeding Issues

If seed data isn't being created:
1. Check that you're running in Development environment
2. Look at application logs for errors
3. Verify the database is empty (seeding only runs on empty database)
4. Check database user has INSERT permissions

## Running Migrations in Production

For production environments:

1. **DO NOT** use `EnsureCreated()` - use migrations instead
2. Update Program.cs to use migrations:
   ```csharp
   var context = services.GetRequiredService<ApplicationDbContext>();
   await context.Database.MigrateAsync();
   ```

3. Run migrations as part of deployment pipeline
4. Consider using a separate migration tool/script
5. Always backup before running migrations

## Adding New Migrations

When you make changes to entity models:

```bash
cd server/Infrastructure
dotnet ef migrations add YourMigrationName --startup-project ../API
dotnet ef database update --startup-project ../API
```

## Reverting Migrations

To revert the last migration:

```bash
dotnet ef database update PreviousMigrationName --startup-project ../API
dotnet ef migrations remove --startup-project ../API
```
