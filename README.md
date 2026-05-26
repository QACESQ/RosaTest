# Сервис заявок на справки

Backend-сервис для обработки заявок сотрудников на получение бухгалтерских справок (2-НДФЛ, о месте работы, о доходах и др.).

Сотрудники создают заявки и отслеживают их статус, бухгалтер обрабатывает заявки и фиксирует изменения статусов с историей.

---

## Архитектура

Проект разделён на слои:

- **API** — контроллеры, middleware, Swagger
- **Application** — бизнес-логика, DTO, сервисы, валидация
- **Domain** — сущности и enums
- **Infrastructure** — EF Core, база данных, конфигурации

Подход: упрощённая Clean Architecture.

---

## Технологии

- ASP.NET Core Web API (.NET 10)
- Entity Framework Core
- SQLite
- FluentValidation
- Swagger / OpenAPI

---

## Функциональность

### Сотрудник
- создание заявки на справку
- просмотр своих заявок
- отслеживание статуса

### Бухгалтер
- просмотр всех заявок
- просмотр деталей заявки
- изменение статуса заявки

---

## Бизнес-логика

### Защита от дублей
Запрещено создавать одинаковые заявки в короткий промежуток времени.

### Статусы заявок

Created -> InProgress -> Completed

InProgress -> Rejected

Created -> Rejected


### История изменений
Каждое изменение статуса сохраняется в истории.

---

## Валидация

Используется FluentValidation:

- обязательные поля
- CopiesCount > 0
- Reason до 500 символов
- проверка enum значений

---

## Обработка ошибок

Глобальный middleware возвращает ошибки:

```json
{
  "error": "Сообщение об ошибке",
  "timestamp": "2026-05-24T12:00:00Z"
}
```

Коды:
- `400` — бизнес-ошибки
- `404` — не найдено
- `500` — внутренняя ошибка сервера

---

## API Endpoints

### Сотрудник

- `POST /api/requests` — создать заявку  
- `GET /api/employees/{employeeId}/requests` — список заявок сотрудника  

### Бухгалтер

- `GET /api/accountant/requests` — все заявки  
- `GET /api/accountant/requests/{id}` — детали заявки  
- `PATCH /api/accountant/requests/{id}/status` — изменить статус заявки  

---

## База данных

- SQLite  
- создаётся автоматически через миграции EF Core  
- файл: `certificates.db`

---

## Запуск проекта

### 1. Применить миграции

```bash
dotnet ef database update --project src/CertificateRequests.Infrastructure --startup-project src/CertificateRequests.API
```

### 2. Запустить API

```bash
dotnet run --project src/CertificateRequests.API
```

### 3. Swagger

```
http://localhost:5088/swagger
```

### 4. Тестовые сотрудники

```json
[
  {
    id : "11111111-1111-1111-1111-111111111111",
    fullName : "Ivan Ivanov"
  },
  {
    id : "22222222-2222-2222-2222-222222222222",
    fullName : "Petr Petrov"
  }
]
```
