# Healthcare Mini App

A balanced healthcare practice management system built with ASP.NET Core, React, and Azure. This project demonstrates real-world development patterns including authentication, RBAC, file management, and AI integration while remaining approachable for developers learning enterprise-level development.

## 🎯 What This App Does

**For Healthcare Providers:**
- **User Management**: Secure login with JWT authentication (Admin/Doctor roles)
- **Patient Management**: Add, view, and manage patient records
- **Medical Notes**: Create and manage patient notes with AI-powered summarization
- **Document Management**: Upload, categorize, and secure patient documents
- **Role-Based Access**: Different permissions for Admins vs Doctors

**Key Features:**
- 🔐 JWT-based authentication with role-based access control
- 👥 Multi-user system with Admin and Doctor roles
- 📝 AI-powered note summarization using Azure OpenAI
- 📁 Secure document upload/download with Azure Blob Storage
- 🗄️ SQL Server database with Entity Framework Core
- 🌐 Modern React frontend with TypeScript

## 🏗️ Architecture

```
healthcare-mini-balanced/
├── api/                    # ASP.NET Core Web API backend
│   ├── Controllers/        # API endpoints
│   ├── Models/            # Data entities and DTOs
│   ├── Data/              # Database context and migrations
│   ├── Auth/              # Authentication and authorization
│   └── Program.cs         # Application entry point
└── web/                    # React + TypeScript frontend
    ├── src/
    │   ├── components/     # Reusable UI components
    │   ├── pages/         # Page components
    │   └── lib/           # Utilities and API client
    ├── package.json        # Dependencies
    └── vite.config.ts      # Build configuration
```

## 🚀 Getting Started

### Prerequisites
- **.NET 8 SDK** or later
- **Node.js 18** or later
- **SQL Server** (local or Azure)
- **Azure account** (for production deployment)

### Local Development Setup

#### 1. Clone the Repository
```bash
git clone https://github.com/MuhammadMunir1214/Patient-Docs.git
cd Patient-Docs
```

#### 2. Backend Setup
```bash
cd api
dotnet restore
dotnet run
```
The API will be available at `https://localhost:7001`

#### 3. Frontend Setup
```bash
cd web
npm install
npm run dev
```
The React app will be available at `http://localhost:5173`

#### 4. Database Setup
```bash
cd api
dotnet ef database update
```

## 🔧 Configuration

### Environment Variables
Create `.env` files in the appropriate directories:

**Backend (`api/`):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HealthcareMiniApp;Trusted_Connection=true;TrustServerCertificate=true;"
  },
  "JwtSettings": {
    "SecretKey": "your-super-secret-key-here",
    "Issuer": "HealthcareMiniApp",
    "Audience": "HealthcareMiniApp",
    "ExpirationHours": 24
  },
  "AzureOpenAI": {
    "Endpoint": "https://your-resource.openai.azure.com/",
    "ApiKey": "your-api-key",
    "DeploymentName": "your-deployment-name"
  }
}
```

**Frontend (`web/`):**
```env
VITE_API_BASE_URL=https://localhost:7001
```

## 📚 API Endpoints

### Authentication
- `POST /auth/login` - User login (returns JWT)

### Patients
- `GET /patients` - List all patients
- `POST /patients` - Create new patient
- `GET /patients/{id}` - Get patient details
- `PUT /patients/{id}` - Update patient
- `DELETE /patients/{id}` - Delete patient

### Notes
- `GET /patients/{id}/notes` - Get patient notes
- `POST /patients/{id}/notes` - Add note to patient
- `PUT /notes/{id}` - Update note
- `DELETE /notes/{id}` - Delete note

### Documents
- `GET /patients/{id}/documents` - List patient documents
- `POST /patients/{id}/documents` - Upload document
- `GET /documents/{id}/download` - Download document
- `DELETE /documents/{id}` - Delete document

### AI Services
- `POST /ai/summarize` - Summarize medical notes

## 🔐 Security Features

### Role-Based Access Control (RBAC)
- **Admin**: Full access to all features
- **Doctor**: Limited access (can't access admin features)

### Resource-Level Security
- Users can only edit/delete their own notes
- Users can only delete documents they uploaded
- Admins can access all resources

### JWT Authentication
- Secure token-based authentication
- Configurable expiration times
- Role claims embedded in tokens

## 🧪 Testing

### Backend Tests
```bash
cd api
dotnet test
```

### Frontend Tests
```bash
cd web
npm test
```

## 🚀 Deployment

### Azure Deployment
1. **Backend**: Deploy to Azure App Service
2. **Frontend**: Deploy to Azure Static Web Apps
3. **Database**: Use Azure SQL Database
4. **Storage**: Use Azure Blob Storage for documents

### Environment Configuration
Update connection strings and settings for production environment.

## 📖 Learning Path

This project is designed to be built in phases:

1. **Phase 0**: Project setup and structure
2. **Phase 1**: Backend skeleton with EF Core
3. **Phase 2**: Authentication and RBAC
4. **Phase 3**: Patient and note management
5. **Phase 4**: Frontend skeleton
6. **Phase 5**: AI integration
7. **Phase 6**: Document management
8. **Phase 7**: Advanced RBAC
9. **Phase 8**: Testing
10. **Phase 9**: Azure deployment
11. **Phase 10**: Polish and optimization

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🆘 Support

If you encounter any issues:
1. Check the [Issues](https://github.com/MuhammadMunir1214/Patient-Docs/issues) page
2. Create a new issue with detailed information
3. Include your environment details and error messages

## 🔗 Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [React Documentation](https://react.dev/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Azure Documentation](https://docs.microsoft.com/en-us/azure/)

---

**Built with ❤️ for learning enterprise-level development patterns**
