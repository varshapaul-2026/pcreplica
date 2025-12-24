# 🚀 Quick Start Guide - PcReplica

A comprehensive construction management platform inspired by Procore.

## 🎯 What's Included

### Backend (.NET Core 10.0)
- ✅ RESTful API with Swagger documentation
- ✅ JWT authentication with BCrypt
- ✅ Role-based authorization
- ✅ Entity Framework Core with PostgreSQL
- ✅ Clean architecture (Core, Infrastructure, API layers)
- ✅ Repository and Unit of Work patterns

### Frontend (React + Vite)
- ✅ Modern React 18 with Vite
- ✅ Material-UI components
- ✅ React Router v6 with protected routes
- ✅ Axios for API calls with interceptors
- ✅ Context API for state management

### Features Implemented
- ✅ User authentication (Login/Register)
- ✅ Project management (CRUD)
- ✅ Dashboard with statistics
- ✅ Role-based access control
- ✅ Responsive navigation

## 📦 Prerequisites

- .NET 10.0 SDK
- Node.js 18+ & npm
- PostgreSQL 14+

## ⚡ Quick Setup (5 minutes)

### 1. Clone & Setup Database

```bash
# Clone the repository
git clone <your-repo-url>
cd pcreplica

# Install PostgreSQL (if not installed)
# On macOS: brew install postgresql
# On Ubuntu: sudo apt-get install postgresql

# Start PostgreSQL service
# On macOS: brew services start postgresql
# On Ubuntu: sudo service postgresql start

# Create database
createdb pcreplica
```

### 2. Configure Backend

```bash
# Edit connection string in server/API/appsettings.json
# Update "DefaultConnection" with your PostgreSQL credentials

cd server/API
dotnet run
```

The API will start at `https://localhost:5001` (or similar port)
- Swagger UI: `https://localhost:5001/swagger`

### 3. Setup Frontend

```bash
# In a new terminal
cd client
npm install
npm run dev
```

The app will start at `http://localhost:5173`

## 🔐 Test Credentials

The application auto-seeds with sample data:

| Email | Password | Role |
|-------|----------|------|
| admin@abcconstruction.com | Admin123! | Admin |
| pm@abcconstruction.com | Admin123! | Project Manager |
| contractor@buildtech.com | Admin123! | Contractor |

## 📚 Detailed Documentation

- **README.md** - Complete project overview
- **DATABASE_SETUP.md** - Detailed database setup and migrations guide

## 🌟 Project Structure

```
pcreplica/
├── server/              # .NET Core Backend
│   ├── API/            # Controllers, DTOs, Program.cs
│   ├── Core/           # Entities, Enums, Interfaces
│   └── Infrastructure/ # DbContext, Repositories, Services
├── client/             # React Frontend
│   └── src/
│       ├── components/ # React components
│       ├── contexts/   # Auth context
│       ├── pages/      # Page components
│       └── services/   # API service layer
├── README.md          # Full documentation
└── DATABASE_SETUP.md  # Database guide
```

## 🛠️ Key Technologies

### Backend
- ASP.NET Core 10.0
- Entity Framework Core
- PostgreSQL (Npgsql)
- JWT Bearer Authentication
- BCrypt.Net for password hashing
- Swashbuckle for API documentation

### Frontend
- React 18
- Vite
- Material-UI (MUI)
- React Router DOM
- Axios
- React Hook Form

## 🎓 Core Features Overview

### 1. Authentication
- User registration with role selection
- JWT-based login
- Protected routes
- Automatic token refresh

### 2. Project Management
- Create, view, update, delete projects
- Project status tracking
- Budget management
- Role-based permissions

### 3. Dashboard
- Project statistics
- Quick access to key metrics
- Activity overview

## 🔧 Development Tips

### Backend

```bash
# Build solution
cd server
dotnet build

# Run with hot reload
cd server/API
dotnet watch run

# Create new migration
cd server/Infrastructure
dotnet ef migrations add MigrationName --startup-project ../API
dotnet ef database update --startup-project ../API
```

### Frontend

```bash
# Run dev server with hot reload
cd client
npm run dev

# Build for production
npm run build

# Preview production build
npm run preview
```

## 📋 Next Steps

To extend the application:

1. **Add More Controllers**: Documents, Budget, RFIs, Daily Logs
2. **Enhance Frontend**: Add corresponding pages for new features
3. **File Upload**: Implement document management
4. **Real-time Features**: Add SignalR for notifications
5. **Advanced Reporting**: Charts and analytics
6. **Mobile Support**: Responsive design improvements

## 🐛 Troubleshooting

### Backend Issues
- **Connection Error**: Check PostgreSQL is running and connection string is correct
- **Build Error**: Run `dotnet restore` in server directory
- **Port Conflict**: Change port in `launchSettings.json`

### Frontend Issues
- **Module Not Found**: Run `npm install` in client directory
- **API Connection Error**: Verify API_BASE_URL in `.env` file
- **CORS Error**: Check CORS configuration in backend

## 📝 API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login and get token

### Projects
- `GET /api/projects` - List all projects
- `GET /api/projects/{id}` - Get project details
- `POST /api/projects` - Create project (Admin/PM only)
- `PUT /api/projects/{id}` - Update project (Admin/PM only)
- `DELETE /api/projects/{id}` - Delete project (Admin only)

## 🎨 UI Pages

- **/login** - Login page
- **/register** - Registration page
- **/dashboard** - Main dashboard
- **/projects** - Projects list and management

## 💡 Sample Data

The application seeds with:
- 2 Companies
- 3 Users (various roles)
- 3 Projects (different statuses)
- Project assignments
- Milestones
- Budget items
- RFIs

## 🤝 Contributing

This is an educational project demonstrating full-stack development with modern technologies.

## 📄 License

Educational purposes.

---

**Ready to start? Run the backend and frontend, then login with test credentials!** 🚀
