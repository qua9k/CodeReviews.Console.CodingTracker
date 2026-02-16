# Habit Tracker

A simple habit tracker. Occurrences of a habit are stored in a database via
CRUD operations. It uses `.NET`, `ADO.NET`, and `SQLite`.

## Purpose

Exposure to the above technologies.

## Design

The database is composed of a single table:

| id  | habit   | date       | count |
| --- | ------- | ---------- | ----- |
| 1   | Skiing  | 2026-01-01 | 3     |
| 2   | Writing | 2026-01-02 | 4     |
| 3   | Cooking | 2026-01-03 | 1     |
