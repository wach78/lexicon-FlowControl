# FlowControl

A simple C# console application created to practice basic flow control, menu handling, input validation, loops, switch statements, methods, enums, and string handling.

## Features

The application contains a console menu with several small exercises:

- Calculate cinema ticket price for one visitor
- Calculate total cinema ticket price for a group
- Repeat entered text multiple times
- Print words from a sentence based on a word interval
- Validate user input with `int.TryParse`
- Use enums for menu choices and ticket price types
- Use constants to avoid magic numbers
- Use methods to keep the code readable and structured



## Project Structure

The main logic is currently implemented in `Program.cs`.

The code demonstrates:

- `while` loops
- `for` loops
- `foreach` loops
- `switch` statements
- nullable values such as `int?` and `string?`
- enums
- records
- constants
- basic input validation
- simple method extraction

## Menu Options

The console menu includes the following options:

```text
0. Exit
1. Show ticket price
2. Show ticket price for group
3. Repeat text
4. Print words by interval
```

## Ticket Price Rules

The cinema ticket price is calculated based on age:

| Age group | Price |
|---|---:|
| Child under 5 | Free |
| Youth up to 19 | 80 SEK |
| Adult | 120 SEK |
| Senior from 65 | 90 SEK |
| Visitor over 100 | Free |


## Running the Project

From the project folder, run:

```bash
dotnet run
```

Or open the solution in Visual Studio and start the project from there.

## Purpose

This project is mainly an exercise project for learning C# fundamentals and writing clearer console application code.

The focus is not on advanced architecture, but on:

- readable code
- simple structure
- meaningful variable and method names
- basic validation
- keeping logic separated into smaller methods

## Notes

This is a learning project and may be refactored further as new C# concepts are introduced.