# MuiCharts
Small demo app on .NET/React. Job entry challenge

## Devcontainers
This repository is equipped with configuration files for Dev Containers feature which allows you to develop this project in a containerized environment. You can use VS Code with Dev Containers extension and Docker or you can use GitHub Codespaces. Read more at [Developing inside a Container](https://code.visualstudio.com/docs/remote/containers).

## Backend
Path: `/backend`

Backend is a simple ASP.NET Core Web API project with EF Core and SQLite. It provides a RESTful API for the frontend to consume and incorportaes DDD principles.

### Projects
- `MuiCharts.Api` - ASP.NET Core Web API project
- `MuiCharts.Contracts` - Shared Web API contracts that can be extracted into a separate package and shared between the client and the server
- `MuiCharts.Infrastructure` - Infrastructure layer with EF Core and SQLite
- `MuiCharts.Domain` - Domain layer with business logic and models

### Essential variables
Use these properties as environmental variables or CLI arguments to configure the backend:
#### HTTPS
If you want to use Kestrel as your primary web server (with no reverse proxy), you can use the following properties to configure HTTPS with Let's Encrypt certificate:
- `HTTPS_PORTS=443` - Listen for HTTPS requests on port 443
- `LettuceEncrypt:AcceptTermsOfService=true` - bypass interactive prompt
- `LettuceEncrypt:DomainNames:0=example.com` - domain name for the certificate (use `:1`, `:2`, `:3`, etc. to add more)
- `LettuceEncrypt:EmailAddress=eugene@xfox111.net` - email address for certificate issuer

> **Note**: you need to have either a public IP address or a domain name to use Let's Encrypt certificates. Otherwise, use `dotnet dev-certs https` to generate a self-signed certificate.

#### Data persistence
Configure these options if you want to change default paths for data persistence:
- `ConnectionStrings:DataContext=Data Source=/persistence/data.db` - SQLite DB connection string (default: `Data Source=/persistence/data.db` for `Production` and `Data Source=data.db` for `Development`)
- `LettuceEncrypt:CertificatesPath=/persistence` - path to store Let's Encrypt certificates (default: `/persistence`)

> **IMPORTANT**: default persistence paths are configured to be used in a Docker container, where the user is `root`. `/persistence` is not writtable by a non-root user, so you need either to change the paths if you want to run the app outside of a container without root privileges or run app as `sudo`.

## Frontend
Path: `/frontend`

Frontend is a simple React app with Material-UI. It consumes the RESTful API provided by the backend (or uses its emulation) and visualizes the data.

> 🚧 WIP

## Docker
Use sample `docker-compose.yml` to see how to deploy project using Docker

## GitHub Actions
Path: `.github/workflows`

There are two GitHub Actions workflows:
- `backend.yml` - CI/CD for the backend. Deploys the app to a remote server using Docker and SSH
- `frontend.yml` - CI/CD for the frontend. Deploys the app to GitHub Pages
