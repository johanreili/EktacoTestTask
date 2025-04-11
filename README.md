# EktacoTestTask
Johan Reili Ektaco test task

The project is built with Entity Framework Core
The database provider is Microsoft SQL Server.
ConnectionString is configured in appsettings.json to use a local database named ProductsDatabase - that can be modified to match your environment.

I took code the first approach, so in order to get the project running on your local database, follow these steps:

1. In your server management, create a database named ProductsDatabase (You can even add a user "sa" with a password "johan", then you will only need to change the Server in the ConnectionStrings in order for everything to work)

2. In Visual Studio use the Package Manager Console to Add a migration and create the tables inside of the ProductAPI.Data:
	2.1. Add-Migration InitialCreate
	2.2.Update-Database
