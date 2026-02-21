# Coding Tracker

A simple coding session tracker. Occurrences of coding session are stored in a database via
CRUD operations. This project uses `.NET`, `Dapper`, `SQLite`, and `Spectre`.

## Purpose

Exposure to the above technologies.

## Design

The database is composed of a single table with the following columns:

| id  | Date       | Start Time | End Time | Duration |
| --- | ---------- | ---------- | -------- | -------- |
| 1   | YYYY-MM-DD | HH:MM      | HH:MM    | HH:MM    |

## Development Difficulties

- Date and Time. How the data is stored does not need to match how it's
  presented. Saving dates and times to the database should conform to SQLite
  expectations, but be presented in a 'readable' format after retrieval.
- Spectre Console is a neat library, but I'm not concerned with becoming
  well-versed in it.
- If the user enters an end time that is earlier than the start time, they'll
  wind up with a negative duration. I won't be fixing this issue.

### Disclaimer

To facilitate effective learning, I don't use LLMs while working on personal
projects. I'm not a "hater" (professionally, I'll adapt to whatever workflow is
expected), but I believe that LLM usage incurs a debt that can only be paid
with experiential learning. This belief is backed up by research:

> "We find that AI use impairs conceptual understanding, code reading, and
> debugging abilities, without delivering significant efficiency gains on
> average. Participants who fully delegated coding tasks showed some
> productivity improvements, but at the cost of learning the library.
>
> Our findings suggest that AI-enhanced productivity is not a shortcut to
> competence and AI assistance should be carefully adopted into workflows to
> preserve skill formation."
>
> - Anthropic, [How AI Impacts Skill Formation](https://arxiv.org/abs/2601.20245)
