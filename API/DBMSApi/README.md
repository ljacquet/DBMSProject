# Project Notes  
Add migration using  
PM> EntityFrameworkCore\Create-Migration migrationname  
PM> EntityFrameworkCore\Update-Database  

# Creating the DB
1. Easiest Method  
In VS 2022  
Tools -> Nuget Package Manager -> Package Manager Console  

PM> Update-Database  
This should create and update the database with all required fields  

2. Install Entity Manager Seperately and run update-database from there.  
More information here: https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli

# Running the Project
1. Visual Studio 2022
Open solution in visual studio. Right click solution in solution explorer and restore nuget packages. create the DB as described above, then you should be able to just run the project. This will give you a gui if you run in debug mode. 

2. Exe
// TODO hopefully will have time to get this working

I'm not sure how to build out the database without using Visual Studio 2022, I figure if you are already in it you may as well use the easier solution.

## Note for future use
need SQLitePCLRaw.bundle_e_sqlite3 installed for some reason


# Helpful Tutorials
Adding Identity to C# API
https://www.c-sharpcorner.com/article/authentication-and-authorization-in-asp-net-core-web-api-with-json-web-tokens/
