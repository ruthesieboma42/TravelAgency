# Travel Agency Backend API
# Project Overview

The Travel Agency Backend API is a structured RESTful service designed to simulate a travel booking system. The project focuses less on UI and more on backend architecture, emphasizing clean code organization, scalability, and maintainability.

A key goal of this project was to understand and apply Domain Driven Design (DDD) principles in a real-world API structure.

## Architecture Focus

This project is built around a layered architecture approach:

- API Layer – Handles HTTP requests, routing, and exposure of endpoints
- Core Layer – Contains domain logic and business rules
- Services Layer – Implements application workflows and business operations
- Data Layer – Handles database access, persistence, and repository logic

This separation ensures a clear distinction between:

- What is exposed externally (API)
- What is internal business logic (Core/Services)
- What handles data storage (Data layer)

## Key Concepts Implemented
- Domain Driven Design (DDD) principles
- Layered architecture for backend scalability
- Separation of concerns across API, Core, Services, and Data layers
- Clean and maintainable code structure for Web APIs
- Structuring backend logic to avoid tight coupling

## What I Learned
- How to design backend systems using DDD principles
- How to properly structure a Web API solution for scalability
- The importance of separating:
  - Domain logic (Core)
  - Application logic (Services)
  - Infrastructure/data concerns (Data)
  - API exposure layer
- How layered architecture improves maintainability and testability
- Thinking in terms of business domains instead of just endpoints
