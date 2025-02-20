Requires a .env file in the same directory as the docker-compose.yml file with the following parameters:

DB_PASSWORD={yourPassword} -> without the curly braces
DB_CONNECTION_STRING=Server=db,1456;Database=DevOpsDen;User Id=sa;Password={yourPassword};TrustServerCertificate=True; -> again without the curly braces
