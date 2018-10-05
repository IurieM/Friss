Document Storage Application

## Prerequisite
Please make sure .net core sdk 2.1 is installed (https://www.microsoft.com/net/download/dotnet-core/2.1)
Latest Stable Node Version (https://nodejs.org/en/blog/release/v8.9.0/)
Sql server instance 

## Running the application
- In root folder run start-app.bat 
- It will start to processes:
- The angular build (npm install and build)
- The Rest Api.

Wait for all processes to start before testing. Usually Web spa will take longer time to start as need to install node modules.

Once all services are running, open http://localhost:5000 and start testing.

## Running the application manually
- Open scr\Document.Api\Client
- cmd npm install (to get node modules)
- cmd npm run build-prod (to build angular artifacts)
- Open scr\Document.Api\
- dotnet run (to start the rest api)


Database is created on startup. Make sure you have the correct database server name in src\Document.Api\appSettings.json (connection string)

- Available users.
- Username:Admin Password:Admin Role: Admin
- Username:User Password:User Role: User

Only Admin Role user is able to upload and delete files.
User role is able to download only.

JWT bearer authetication is used to authorize users, (authetication setting defined in appSettings.json).

All uploaded files are saved in App_Data\Files folder. Storage type and path is configured in appSettings.json

All requests with ellapsed time are logd in Logs folder.

## Unit and Integration testing

-in test\Document.Identity.UnitTest (ex of unit test for authetication process)

-in test\Document.Api.Test (ex of integration test for Admin and User authorization). It is self-hosted and use InMemoryDatabase. No need to start Document.Api.

-in src\Document.Api\Client - npm run e2e to run end to end integration test (still in progress) 