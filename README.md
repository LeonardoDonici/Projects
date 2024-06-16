# Projects

# Web Projects

This repository contains two distinct web projects. The following instructions explain how to run each of them.

## Project 1: Python Web Server

### Description

This project is built using HTML, CSS, and Python. It includes registration forms where user data is stored in a JSON file. Users can log in using the data they registered with. Product information is stored using the browser's local storage and IndexedDB.

To run the first project, which uses a Python web server, follow these steps:

1. Ensure you have Python installed on your computer. You can download Python from [python.org](https://www.python.org/downloads/).
2. Navigate to the project directory using the terminal.
3. Run the following command to start the web server:

 
    python server_web.py

4. Open a browser and go to `http://localhost:5678`.

### Features

- **HTML & CSS**: For the front-end structure and styling.
- **Python**: For server-side logic.
- **User Registration**: Form data is stored in a JSON file.
- **User Login**: Users can log in with their registered credentials.
- **Product Storage**: Uses local storage and IndexedDB for storing product information.


## Project 2: Node.js Web Project

### Description

This project is built using JavaScript, CSS, and a local database such as SQLite3. It includes user registration where user data is stored in a JSON file. Users can log in later using their registered data.

### Instructions

To run the second project, follow these steps:

1. Ensure you have Node.js installed on your computer. You can download Node.js from [nodejs.org](https://nodejs.org/).
2. Navigate to the project directory using the terminal.
3. Install the project dependencies by running the following command:

    ```sh
    npm install
    ```

4. Run the following command to start the web application:

    ```sh
    node app.js
    ```

5. Open a browser and go to `http://localhost:6789`.

### Features

- **JavaScript & CSS**: For the front-end structure and styling.
- **SQLite3**: For local database storage.
- **User Registration**: Form data is stored in a JSON file.
- **User Login**: Users can log in with their registered credentials.


# Habit Tracker

## Overview

The Habit Tracker is a robust application developed using C# and Windows Forms designed to help users build and maintain habits. It offers a user-friendly interface for tracking daily habits and visualizing progress over time. The application integrates with external APIs to provide motivational quotes and offers comprehensive features for monitoring habit streaks.

## Features

- **Interactive Calendar**: Visual representation of your habits over the month with color-coded indicators for completed and missed days.
- **Habit Management**: Easily create, update, and delete habits from the convenient dropdown menu.
- **Daily Check-ins**: Mark habits as completed for the day and track your progress.
- **Streak Tracking**: Monitor your current and maximum streaks to stay motivated.
- **Motivational Quotes**: Get daily motivational quotes to keep you inspired, pulled from an external API.
- **Journal Entries**: Keep a journal for each habit to log your thoughts and progress.
- **User-Friendly Navigation**: Intuitive buttons for navigating between months and managing habits.

## How to Use

1. **Create a New Habit**: Click on `CreateNewHabit` and enter the details of your new habit.
2. **Track Your Progress**: Use the calendar to check off completed days by selecting `Check today!`.
3. **Monitor Streaks**: Keep an eye on your `current streak` and `maximum streak` displayed on the interface.
4. **Get Motivated**: Click `Generate` to receive a new motivational quote from the integrated API.
5. **Manage Habits**: Select a habit from the dropdown menu to delete it or make journal entries.
6. **Navigate the Calendar**: Use the arrow buttons to switch between months.

## Technical Details

- **Language**: C#
- **Framework**: .NET Windows Forms
- **APIs**: Integrates with external APIs for fetching quotes and tracking habits
- **Data Storage**: Utilizes JSON for saving user data and habit information locally

## Design Patterns

- **Strategy Pattern**: Used to encapsulate different behaviors and algorithms for habit tracking and streak calculations.
- **Builder Pattern**: Employed for constructing complex habit objects step by step.
- **Proxy Pattern**: Used to control access to external APIs and manage network requests efficiently.

## Installation

1. Clone the repository.
2. Open the solution file in Visual Studio.
3. Build and run the project.


Stay consistent and achieve your goals with the Habit Tracker!


## Additional Notes

- Make sure you have the necessary permissions to run the scripts.
