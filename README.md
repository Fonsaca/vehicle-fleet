# Vehicle Fleet

This application is a solution for a vehicle fleet. It is a full-stack web application that allows management of Buses, Trucks and Cars.

The system is developed using .NET 8 (ASP.NET Web API) for the backend, Angular 17 with PrimeNG for the frontend, and it runs fully containerized using Docker. It uses an in-memory database via Entity Framework for quick setup and testing.

## 🚀 Features

- List all **vehicles**
- Add new **Bus**, **Car** and **Truck**
- Edit existing records
- Filter and search for vehicles

## 🛠 Technologies Used

- **.NET 8** (ASP.NET Core Web API)
- **Entity Framework In-Memory**
- **Angular 17**
- **PrimeNG**
- **Docker / Docker Compose**
- **Swagger**

## 📋 Requirements

- [Docker Desktop](https://www.docker.com/products/docker-desktop) installed and running

## ▶️ How to Run the System

1. Clone the repository:

```bash
   git clone https://github.com/Fonsaca/vehicle-fleet.git ./vehicle-fleet
   cd ./vehicle-fleet
```

2. Start the application with Docker Compose

```bash
   docker-compose up --build 
```


3. Access the application
```bash
   Web App (Angular UI): http://localhost:25000

   API (Swagger UI): http://localhost:25001/swagger
```
