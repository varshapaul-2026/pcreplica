# Procore Construction Management Platform Replica

A comprehensive construction management platform built with React, .NET Core, and PostgreSQL.

## Features

### Core Features
- **Project Management**: Create, view, update, and delete construction projects
- **Document Management**: Upload and organize project documents with categorization
- **Budget & Cost Tracking**: Manage project budgets and track expenses
- **RFIs (Requests for Information)**: Create and manage RFIs with workflow
- **Daily Logs**: Track daily construction activities, weather, and labor
- **Team Collaboration**: User management with role-based access control
- **Dashboard & Analytics**: View project KPIs and metrics

### Technical Stack
- **Frontend**: React with Vite, Material-UI, React Router, Axios
- **Backend**: .NET Core 10.0 with Entity Framework Core
- **Database**: PostgreSQL
- **Authentication**: JWT-based authentication with BCrypt password hashing

## Project Structure

```
/
├── server/                  # .NET Core Backend
│   ├── API/                # API Controllers and DTOs
│   ├── Core/               # Domain entities and interfaces
│   └── Infrastructure/     # Data access and services
├── client/                 # React Frontend
│   └── src/
│       ├── components/     # React components
│       ├── services/       # API services
│       ├── pages/          # Page components
│       └── contexts/       # React contexts
└── Readme.md              # This file
```

## Prerequisites

- .NET 10.0 SDK or later
- Node.js 18+ and npm
- PostgreSQL 14+ database server

## Backend Setup

### 1. Install PostgreSQL

Make sure PostgreSQL is installed and running on your system.

### 2. Configure Database Connection

Edit `server/API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=pcreplica;Username=postgres;Password=your_password"
  }
}
```

### 3. Create Database and Run Migrations

```bash
cd server/Infrastructure

# Install EF Core tools if not already installed
dotnet tool install --global dotnet-ef

# Create initial migration
dotnet ef migrations add InitialCreate --startup-project ../API --context ApplicationDbContext

# Apply migrations to create database
dotnet ef database update --startup-project ../API
```

### 4. Run the API

```bash
cd server/API
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5054`
- HTTPS: `https://localhost:7036`
- Swagger UI: `https://localhost:7036/swagger`

## Frontend Setup

### 1. Install Dependencies

```bash
cd client
npm install
```

### 2. Configure API URL

Create `client/src/services/api.js` and update the base URL if needed:

```javascript
const API_BASE_URL = 'http://localhost:5054/api';
```

### 3. Run the Development Server

```bash
npm run dev
```

The app will be available at `http://localhost:5173`

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Login and get JWT token

### Projects
- `GET /api/projects` - Get all projects
- `GET /api/projects/{id}` - Get project by ID
- `POST /api/projects` - Create new project (Admin/PM only)
- `PUT /api/projects/{id}` - Update project (Admin/PM only)
- `DELETE /api/projects/{id}` - Delete project (Admin only)

## User Roles

1. **Admin**: Full access to all features
2. **Project Manager**: Can manage projects and teams
3. **Contractor**: Can view and update assigned projects
4. **Subcontractor**: Limited access to specific tasks
5. **View Only**: Read-only access

## Database Schema

### Main Tables
- **Users**: User accounts and authentication
- **Companies**: Organization/company information
- **Projects**: Construction projects
- **ProjectUsers**: Many-to-many relationship between projects and users
- **Documents**: Project documents and files
- **Tasks**: Project tasks and assignments
- **Milestones**: Project milestones
- **BudgetItems**: Budget line items with cost codes
- **ChangeOrders**: Budget change orders
- **Rfis**: Requests for Information
- **DailyLogs**: Daily construction logs
- **Comments**: Comments on RFIs and other entities

## Development

### Backend Development

```bash
cd server
dotnet build
dotnet test  # Run tests (if available)
```

### Frontend Development

```bash
cd client
npm run dev     # Start dev server
npm run build   # Build for production
npm run preview # Preview production build
```

## API Authentication

All protected endpoints require a Bearer token in the Authorization header:

```
Authorization: Bearer <your_jwt_token>
```

To get a token:
1. Register a user via `/api/auth/register`
2. Login via `/api/auth/login`
3. Use the returned token in subsequent requests

## Testing the API

You can test the API using:
- **Swagger UI**: Navigate to `https://localhost:5001/swagger`
- **Postman**: Import the API endpoints
- **curl**: Command-line testing

Example curl request:
```bash
# Register a user
curl -X POST http://localhost:5054/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@example.com",
    "password": "Admin123!",
    "firstName": "Admin",
    "lastName": "User",
    "role": 1,
    "companyId": null
  }'

# Login
curl -X POST http://localhost:5054/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@example.com",
    "password": "Admin123!"
  }'
```

## Security Features

- JWT token-based authentication
- Password hashing with BCrypt
- Role-based authorization
- CORS configuration
- SQL injection prevention (EF Core parameterized queries)
- Soft delete for data integrity

## Contributing

This is an educational project for construction management platform development.

## License

This project is created for educational purposes.

## Future Enhancements

- Real-time notifications with SignalR
- Email notifications
- File upload for documents with validation
- Advanced reporting and export features
- Mobile responsive design improvements
- Gantt chart for project timelines
- Activity feed and audit logs
- Advanced search and filtering
- Two-factor authentication

## Support

For issues or questions, please refer to the project documentation or contact the development team.
