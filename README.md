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

5. Open a browser and go to `http://localhost:6789` (or the port specified in the `app.js` script).

### Features

- **JavaScript & CSS**: For the front-end structure and styling.
- **SQLite3**: For local database storage.
- **User Registration**: Form data is stored in a JSON file.
- **User Login**: Users can log in with their registered credentials.

## Additional Notes

- Make sure you have the necessary permissions to run the scripts.
