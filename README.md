# Enpal_YF_Task

## Prerequisites

- Docker
- Docker Compose

## Steps to Start the Application

1. **Clone the Repository**

    ```sh
    git clone https://github.com/YuriiFozekosh/Enpal_YF_Task.git
    cd Enpal_YF_Task
    ```

2. **Build and Start the Containers**

    ```sh
    docker-compose up --build
    ```

3. **Access the Application**

    - The API should be accessible at `http://localhost:3000`
    - The database should be accessible at `http://localhost:5432`

## Running Tests

1. **Ensure the Database is Running**

    - Make sure the database container is up and running.

2. **Run the Tests**

    ```sh
    dotnet test
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