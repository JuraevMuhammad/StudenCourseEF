# Лоиҳаи курси: Системаи идоракунии маркази таълимӣ (LMS)

## 1. Тавсифи лоиҳа

Ҳадафи ин лоиҳа сохтани **RESTful API** барои идоракунии маркази таълимӣ (Learning Management System) мебошад. Система бояд имконияти идоракунии раванди таълим, аз ҷумла омӯзгорон, курсҳо, гурӯҳҳо ва донишҷӯёнро дошта бошад.

### Талаботи техникӣ

1. **Технологияҳо:** C#, .NET Core (Web API), Entity Framework Core, PostgreSQL.
2. **Soft Delete:** Ҳамаи ҷадвалҳо бояд майдони `IsDeleted` дошта бошанд. Маълумот аз пойгоҳи додаҳо (Database) **ҳеҷ гоҳ** бо `DELETE` нест карда намешавад. Ба ҷои ин, майдони `IsDeleted` ба `true` иваз карда мешавад.
3. **Data Consistency:** Ҳангоми гирифтани маълумот (GET requests), система бояд танҳо маълумотеро баргардонад, ки `IsDeleted == false` аст.


---

## 2. Сохтори Пойгоҳи Додаҳо (Database Schema)

Дар лоиҳа 5 ҷадвали асосӣ (Entities) вуҷуд дорад.
*Эзоҳ: Ҳамаи ҷадвалҳо бояд майдонҳои стандартии `Id` (PK), `CreatedAt` (DateTime) ва `IsDeleted` (bool)-ро дошта бошанд.*

### 1. Ҷадвали `Teachers` (Омӯзгорон)

Ин ҷадвал маълумоти омӯзгоронро нигоҳ медорад.

| Майдон | Тип | Тавсиф |
| --- | --- | --- |
| FirstName | string | Номи омӯзгор |
| LastName | string | Насаби омӯзгор |
| Email | string | Почтаи электронӣ (Unique) |
| Phone | string | Рақами телефон |
| Specialization | string | Ихтисос (мас: Backend, Frontend, Design) |
| ExperienceYears | int | Таҷрибаи корӣ (бо сол) |

### 2. Ҷадвали `Courses` (Курсҳо)

Курсҳои таълимӣ, ки марказ пешниҳод мекунад. Ҳар як курс ба як омӯзгор вобаста аст.

| Майдон | Тип | Тавсиф |
| --- | --- | --- |
| Title | string | Номи курс (мас: ".NET Bootcamp") |
| Description | string | Маълумот дар бораи курс |
| Price | decimal | Нархи курс |
| TeacherId | int | Калиди хориҷӣ (FK) ба Teachers |

### 3. Ҷадвали `Groups` (Гурӯҳҳо)

Гурӯҳҳои таълимӣ, ки аз рӯи курсҳо ташкил мешаванд.

| Майдон | Тип | Тавсиф |
| --- | --- | --- |
| Name | string | Номи гурӯҳ (мас: "C#-G1") |
| CourseId | int | Калиди хориҷӣ (FK) ба Courses |
| StartDate | DateTime | Санаи оғози дарс |
| EndDate | DateTime | Санаи тахминии хатм |
| IsStarted | bool | Статуси оғоз шудан |

### 4. Ҷадвали `Students` (Донишҷӯён)

Рӯйхати умумии донишҷӯёни марказ.

| Майдон | Тип | Тавсиф |
| --- | --- | --- |
| FirstName | string | Номи донишҷӯ |
| LastName | string | Насаби донишҷӯ |
| BirthDate | DateTime | Санаи таваллуд |
| Email | string | Почтаи электронӣ |
| Phone | string | Рақами телефон |

### 5. Ҷадвали `StudentGroups` (Ҷадвали пайвасткунанда)

Ин ҷадвал барои амалӣ кардани робитаи Many-to-Many байни Донишҷӯён ва Гурӯҳҳо хизмат мекунад (як донишҷӯ метавонад дар чанд гурӯҳ бошад).

| Майдон | Тип | Тавсиф |
| --- | --- | --- |
| StudentId | int | Калиди хориҷӣ (FK) ба Students |
| GroupId | int | Калиди хориҷӣ (FK) ба Groups |
| EnrolledAt | DateTime | Санаи қабул ба гурӯҳ |
| Status | enum | Ҳолат (Active, Completed, Dropped) |
| Grade | int? | Баҳо (агар курсро хатм карда бошад) |

---

## 3. Робитаҳои байни ҷадвалҳо (Relationships)

* **Teacher ↔ Courses:** One-to-Many (Як омӯзгор – Чанд курс).
* **Course ↔ Groups:** One-to-Many (Як курс – Чанд гурӯҳ).
* **Student ↔ Groups:** Many-to-Many (Тавассути ҷадвали `StudentGroups`).

---

## 4. API Endpoints 

Донишҷӯ вазифадор аст, ки контроллерҳоро сохта, эндпоинтҳои зеринро амалӣ намояд.
*Ёдраскунӣ: Ҳамаи дархостҳои GET бояд маълумоти `IsDeleted = true`-ро филтр кунанд.*

### I. Teachers Controller

1. `GET /api/teachers` – Гирифтани рӯйхати ҳамаи омӯзгорон.
2. `GET /api/teachers/{id}` – Гирифтани омӯзгор бо ID.
3. `POST /api/teachers` – Илова кардани омӯзгори нав.
4. `PUT /api/teachers/{id}` – Таҳрири маълумоти омӯзгор.
5. `DELETE /api/teachers/{id}` – Soft delete (ғайрифаъол кардан)-и омӯзгор.
6. `GET /api/teachers/active` – Гирифтани омӯзгороне, ки ҳоло курс доранд.
7. `GET /api/teachers/search?name={text}` – Ҷустуҷӯи омӯзгор аз рӯи ном.

### II. Courses Controller

8. `GET /api/courses` – Гирифтани рӯйхати курсҳо.
9. `GET /api/courses/{id}` – Гирифтани курс бо ID.
10. `GET /api/courses/with-teachers` – Гирифтани курсҳо ҳамроҳ бо маълумоти омӯзгор (Include).
11. `POST /api/courses` – Сохтани курси нав.
12. `PUT /api/courses/{id}` – Таҳрири курс.
13. `DELETE /api/courses/{id}` – Soft delete-и курс.
14. `GET /api/courses/filter?minPrice={x}&maxPrice={y}` – Филтр аз рӯи нарх.

### III. Groups Controller

15. `GET /api/groups` – Рӯйхати ҳамаи гурӯҳҳо.
16. `GET /api/groups/{id}` – Гирифтани гурӯҳ бо ID.
17. `GET /api/groups/by-course/{courseId}` – Гурӯҳҳои як курси мушаххас.
18. `POST /api/groups` – Сохтани гурӯҳи нав.
19. `PUT /api/groups/{id}` – Таҳрири гурӯҳ.
20. `DELETE /api/groups/{id}` – Soft delete-и гурӯҳ.
21. `PUT /api/groups/{id}/start` – Ишора кардани оғози дарс (IsStarted = true).
22. `GET /api/groups/{id}/details` – Гурӯҳ бо рӯйхати донишҷӯёнаш (Include Students).

### IV. Students Controller

23. `GET /api/students` – Рӯйхати донишҷӯён.
24. `GET /api/students/{id}` – Гирифтани донишҷӯ бо ID.
25. `POST /api/students` – Бақайдгирии донишҷӯ.
26. `PUT /api/students/{id}` – Таҳрири маълумот.
27. `DELETE /api/students/{id}` – Soft delete.
28. `GET /api/students/deleted` – Рӯйхати донишҷӯёни нестшуда (барои Admin).
29. `PUT /api/students/{id}/restore` – Барқарор кардани донишҷӯи нестшуда.

### V. Enrollment Controller (StudentGroups)

30. `POST /api/enrollments/add` – Дохил кардани донишҷӯ ба гурӯҳ.
31. `DELETE /api/enrollments/remove` – Хориҷ кардани донишҷӯ аз гурӯҳ.
32. `GET /api/enrollments/student/{studentId}` – Таърихи курсҳои донишҷӯ.
33. `PUT /api/enrollments/grade` – Гузоштани баҳо ба донишҷӯ.

### VI. Analytics Controller

33. `GET /api/analytics/students-count` – Шумораи умумии донишҷӯёни фаъол.

---


* **Архитектура:** Истифодаи дурусти Domain,Infrastructure,WebApp
* **Soft Delete:** Дуруст кор кардани филтри `IsDeleted` дар ҳамаи қисматҳо.
* **LINQ:** Истифодаи оптималии `Include` ва навиштани дархостҳои мураккаб.
* **Clean Code:** Номгузории дурусти тағйирёбандаҳо ва методҳо.