# PatientDocs (Balanced Plan with Medium RBAC)

This middle-ground project plan bridges the gap between the beginner-friendly notes app and the full-blown SaaS. It introduces multi-user login, RBAC, Patient Documents uploads (replacing Claims), and richer frontend/backend patterns while staying approachable for someone new to .NET, React, and Azure.

## Goal
Build a small healthcare-style app where:
- Users can log in with JWT (roles: Admin, Doctor).
- Add patients and notes.
- Upload patient documents (medical history, lab reports, insurance card, ID, referrals, copay receipts, etc.).
- Call an AI service (Azure OpenAI) to summarize notes.
- Enforce medium-level RBAC (roles + one resource-based rule).

## Project Structure
```
PatientDocs/
├── api/                    # ASP.NET Core backend
│   ├── Controllers/
│   ├── Models/
│   ├── Data/
│   ├── Auth/
│   └── Program.cs
└── web/                    # React frontend
    ├── src/
    │   ├── components/
    │   ├── pages/
    │   └── lib/
    ├── api-client.ts
    ├── auth.ts
    └── vite.config.ts
```

## Backend (ASP.NET Core API)
- ASP.NET Core Web API project.
- Entities: User, Patient, Note, Document.
- JWT-based authentication with simple role checks.
- EF Core with SQL Server (local dev) / Azure SQL (prod).

### Endpoints:
- `POST /auth/login` → returns JWT with role claim.
- `GET/POST /patients` → manage patients.
- `POST /patients/{id}/notes` → attach notes.
- `GET/POST /patients/{id}/documents` → list/upload patient docs (filter by type).
- `GET /documents/{documentId}/download` → short‑lived SAS or streaming.
- `DELETE /documents/{documentId}` → uploader-or-admin can delete.
- `POST /ai/summarize` → summarize note via Azure OpenAI.
- Swagger enabled.

## Frontend (React + TypeScript)
- React app with Vite + TypeScript.
- Pages:
  - Login: store JWT in session.
  - Patients List: fetch via React Query.
  - Patient Form: React Hook Form for validation.
  - Notes: add/list notes, call Summarize.
  - Documents: upload/list/download/delete (with type filter).
- Use React Query for API data fetching & cache.
- Error/loading states (spinners, retries).

## Database
### Schema (minimal):
- **User**: Id, Email, PasswordHash (dev), Role.
- **Patient**: Id, Name, DOB, MRN.
- **Note**: Id, PatientId, AuthorUserId, Text, CreatedAt.
- **Document**: Id, PatientId, Type, FileName

### Type enum:
MedicalHistory, LabReport, InsuranceCard, GovernmentID, Referral, CopayReceipt, Other.

### Relationships:
- Patient has many Notes and Documents.
- Run EF Core migrations to set up.

## Azure Basics
- Deploy API to Azure App Service.
- Deploy React frontend to Azure Static Web Apps.
- Use Azure SQL for database.
- Use Azure Blob Storage for patient-docs container (private access).
- (Optional later) Add Key Vault and Application Insights.

## AI Integration
- Endpoint: `/api/ai/summarize`.
- Uses Azure OpenAI with a JSON schema response.
- Handles errors by retrying or returning a fallback.
- HIPAA hygiene: avoid sending unnecessary PHI; don't log prompts/outputs.

### Example Response:
```json
{
  "summary": "Patient is recovering well.",
  "followUpNeeded": true
}
```

## Medium-Level RBAC Add-On

### Overview
- **Roles**: Admin, Doctor (stored on User table).
- **JWT**: includes sub, email, role claims.

### Policies:
- `CanViewPatients` → Admin, Doctor
- `CanCreatePatient` → Admin, Doctor
- `CanWriteNotes` → Admin, Doctor
- `CanViewDocuments` → Admin, Doctor
- `CanUploadDocuments` → Admin, Doctor
- `AdminOnly` → Admin

### Resource-based rules:
- **Notes**: Only the note author or an Admin can edit/delete a note.
- **Documents**: Only the document uploader or an Admin can delete a document.

### Implementation Steps
1. Add Role column to User and seed Admin + Doctor.
2. Issue JWT with role claim in `/auth/login`.
3. Define authorization policies in Program.cs.
4. Annotate patient/note/document endpoints with `[Authorize(Policy=...)]`.
5. Add resource-based requirements for note edit/delete and document delete.
6. Frontend: decode token role for UI state; hide admin nav for Doctors.
7. Add tests (integration + Cypress) to cover 401/403/200 cases.

## Step-by-Step Phases (Build Plan)

### Phase 0 — Project Setup
- Repo structure with api/ + web/.
- Init git, add .gitignore, README.
- Configure DB (SQL Server via Docker or SQLite).
- Verify `dotnet run` + `npm run dev` work.

### Phase 1 — Backend Skeleton
- Create API project, add EF Core, Swagger.
- Add DbContext + empty sets.
- Add health check, migrations.

### Phase 2 — Auth + RBAC
- Add User entity with Role.
- Seed Admin + Doctor.
- `/auth/login` → JWT with role.
- Add policies (CanViewPatients, CanUploadDocuments, AdminOnly, etc.).
- Protect endpoints.

### Phase 3 — Patients & Notes
- CRUD-lite for patients.
- Add/list notes.
- Policies applied.

### Phase 4 — Frontend Skeleton
- Vite + TS app.
- Auth context stores JWT in session.
- Pages: Login, Patients, Patient→Notes.
- React Query + React Hook Form.

### Phase 5 — AI Summarize
- `/api/ai/summarize` endpoint.
- Mock response first, then Azure OpenAI.
- Frontend: "Summarize" button shows result.

### Phase 6 — Patient Documents (minimal)
#### API:
- `POST /patients/{id}/documents` (multipart): fields file, type.
- Save to Blob path derived from `{patientId}/{type}/{guid}-{safeFileName}`.
- Create DB row with Id, PatientId, Type, FileName only.
- `GET /patients/{id}/documents` (query: type, page, size): return minimal records.
- `GET /documents/{documentId}/download`: generate short‑lived SAS URL or stream.
- `DELETE /documents/{documentId}`: resource rule (uploader or Admin).

#### UI:
- Patient Documents tab with upload + list + Download/Delete actions.
- Filter by Type.

### Phase 7 — Resource-Based RBAC
- Add NoteAuthorOrAdmin handler for notes.
- Add DocumentUploaderOrAdmin handler for documents.
- Protect `PUT/DELETE /notes/{id}` and `DELETE /documents/{id}`.

### Phase 8 — Testing
- Integration tests: 401, 403, 200 paths.
- Cypress: Doctor can CRUD patients/notes, can't access admin; upload/delete doc.

### Phase 9 — Azure Deployment
- Deploy API + frontend.
- Set up Azure SQL + Blob.
- Configure app settings.

### Phase 10 — Polish
- README with setup + demo script.
- Light audit logs (IDs only).
- Optional: App Insights, Key Vault.

## Resources
- **ASP.NET Core API**: https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api
- **React Basics**: https://react.dev/learn
- **React Query**: https://tanstack.com/query/latest
- **Azure App Service Deploy**: https://learn.microsoft.com/en-us/azure/app-service/quickstartdotnetcore
- **Azure Blob Storage Upload**: https://learn.microsoft.com/en-us/azure/storage/blobs/storagequickstart-blobs-dotnet
- **Azure OpenAI Quickstart**: https://learn.microsoft.com/en-us/azure/ai-services/openai/quickstart

## Why This Plan Works
- **Balanced**: Covers most of the job's tech stack (auth, RBAC, CRUD, AI, Azure basics) without overwhelming detail.
- **Practical**: Each phase is buildable in ~1 day or less.
- **Extendable**: Can evolve toward the full SaaS plan if time/skill grows.
