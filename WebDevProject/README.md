# WebDevProject

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 12.2.1.

## Run Notes
Creating / Running the backend
- Using Visual Studio 2022 (This should work in older versions I just do not have one installed to test)

 1. Right Click on the solution and hit restore nuget packages
 2. In the toolbar go to Tools -> Nuget Package Manager -> Package Manager Console
 3. Enter `update-database`
 4. This should create the database and create all tables. If it doesn't hopefully you get a useful error because I tried this on a blank computer and didn't run into any issues. Maybe make sure you have entity framework core installed through the Visual Studio Installer
 5. Hit the run button at the top, this will open a swagger page with api calls on it. Sadly this no longer works as they don't use authorization.

To run the front end you will have to `npm install` that should be the only requirement.
Run the project using `npm run start`. The code does run in prod however you will get CORS issues without the proxy

Once you it has run navigate to localhost:4200/testing.  
There will be a page with a button that says create everything.

This creates three users TestLiam, TestJamie, and TestDarcy. They all have the password password123.

TestLiam has 2 ingredients, TestDarcy has 1 and TestJamie has none.

TestLiam and TestDarcy are automatically added to a house that should have a join id of 1. You can join the house on TestJamie from the /home page.

If the ID isn't working you should be able to see it by logging into either other user and the join code will be at the top of /home

3 different recipes are created 2 that can already be made with the ingredients available. The third requires someone to add Test Ingredient 3 to themselves to be made.

## Notes
The website does not have as much error correction as I would like, I'm running very short on time to do this and grade for my TA so this will have to do. As long as the server is running you shouldn't run into any issues. If things are acting strangely the issue should be fairly obvious by looking in the console I've added excessive logging.  

I was able to create a rather hacky solution to find recipes that can be created from the ingredients available to all users and from the ingredients within the recipe. If you know of a better way to compare two many-to-many relationships I'd definitely be interested in hearing about it. The code for that is available in the backend under the home controller in the last function called getPossibleRecipes.  

Generally the backend should be fairly resilient the front end just doesn't always communicate it in the best ways, hopefully you don't run into any major issues. If anything isn't showing up double check that the backend is still running and check the console for errors.  

The JWTs I use for authentication last for 3 hours, this should be more than enough for you and theoretically if they expire the routeguards should kick you out to the login page. However if you start getting unauthorized requests it might be a good idea to try reloading.