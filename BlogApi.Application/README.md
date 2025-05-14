# BlogApi
## Описание
REST API для управления постами блога, написано на ASP.NET Core с EF Core и Sqlite
## Инструкция
Склонировать репозиторий, выполнить команду в корне проекта dotnet run, откройте https://localhost:7250/swagger/index.html
## Технологии
ASP.NET Core, EF Core, Sqlite, Swagger
## Пример запроса
GET `/api/posts` ответ:
`
[
    {
        "id": 1,
        "title": "The first post in the blog",
        "content": "This is my first post in the blog",
        "createAt": "2025-05-12T19:32:26.3503116"
    }
]
`