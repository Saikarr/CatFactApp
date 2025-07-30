# CatFactApp

A simple ASP.NET Core Razor Pages web app that returns random cat facts from an external API and saves them to a local text file.

## Features

- Get random facts about cats from the [catfact.ninja API](https://catfact.ninja/fact) after pressing a button
- Optionally specify the maximum length of returned facts
- Every request automatically saves the fact to a `catfacts.txt` file in the project directory
