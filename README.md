Prerequisitos:
1. Tener nodejs V 22 o superior instalado
2. SDK net core V 8.
3. Angular V 19 o superior

    
El desarrollo contiene dos carpetas:
1. Front: Aca se encuentra todo el desarrollo.  
2. Back: Se encuentra la implementacion del api rest con swagger y la base de datos SQLite (task.db)
3. UnitTest: en esta carpeta se encuentran las pruebas unitarias, relacionando el proyecto con Back.

Funcionamiento:
1. Levantar el microservicio rest con el comando "dotnet run2 dentro de la carpeta del back.  Aca indicara la url en  la cual se esta ejecutando.  Por ejemplo: http://localhost:4200
2. Ajustar la url del back en el archivo environment.ts que se encuentra dentro de environment se configura la url de conexion al back.  Se debe conservar el sufijo "/tasks" para el correcto funcionamiento.
3. Levantar el front con el comando "ng serve" dentro de la carpeta front. 
