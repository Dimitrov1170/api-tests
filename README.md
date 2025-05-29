# Reqres API Automation Tests

This project is a beginner-friendly API automation suite built using C#, NUnit, and RestSharp. It tests the public [Reqres API](https://reqres.in/) and demonstrates clean, maintainable structure for junior QA engineering roles.

## 📌 Tech Stack

- **Language:** C#
- **Test Framework:** NUnit
- **HTTP Client:** RestSharp
- **JSON Parsing:** Newtonsoft.Json
- **Structure:** POM-like API client layer (`UserClient.cs`)

---

## 🚀 How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/Dimitrov1170/api-test.git
2. Open the solution in Visual Studio.

3. Run all tests via Test Explorer or using dotnet test:

---

✅ Features Covered

| Area  | Endpoint                 | Scenario                        |
| ----- | ------------------------ | ------------------------------- |
| Auth  | `POST /api/login`        | Valid login returns token       |
| Auth  | `POST /api/login`        | Missing password returns error  |
| Users | `GET /api/users?page=2`  | Returns paginated list of users |
| Users | `POST /api/users`        | Creates user with name & job    |
| Users | `PUT /api/users/{id}`    | Updates user info               |
| Users | `DELETE /api/users/{id}` | Deletes user                    |

---

👤 Author
**Georgi Dimitrov**
**QA Automation Engineer**

---

💡 Notes
Public test API used: reqres.in

Requires no authentication except: x-api-key: reqres-free-v1

Tests are fully isolated and stateless
