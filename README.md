# Enpal_YF_Task

## Prerequisites

- Docker
- Docker Compose

## Steps to Start the Application

1. **Clone the Repository**

    ```sh
    git clone https://github.com/yfozekosh/Enpal_YF_Task.git
    cd Enpal_YF_Task
    ```

2. **Build and Start the Containers**

    ```sh
    docker-compose up --build
    ```

3. **Access the Application**

    - The API should be accessible at `http://localhost:3000`
    - The database is accessible on port `5432`. Update user and password in `src/database/Dockerfile` if needed.

## Running Tests

1. **Ensure the Database is Running**

    - Make sure the database container is up and running.

2. **Run the dotnet Tests**

    ```sh
    dotnet test
    ```
   
3. **Run the node Tests**
    ```cmd
   cd "task\2024_06 Take home challenge resources\tests"
   npm install
   npm run tets
    ```

## Stopping the Application

1. **Stop the Containers**

    ```sh
    docker-compose down
    ```

## Used Dependencies

- ASP\.NET
- Dapper
- Npgsql

## Possible improvements
- Add more tests
- Add logging
- Add db indexes
- Rewrite test initialisation to spawn a db on test command and populate it per test-suite