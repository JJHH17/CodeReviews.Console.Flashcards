```Flashcards Application```
- A console based flashcards application, built using C#.
- This project was built following the C# Academies "Flashcard" requirements.
- The project contains a Stack (a container for flashcards), Flashcards for studying, as well as a "Study Area", where users can study their created flashcards.

```Technologies Used```
- SQL Server
- Spectre (for the console/user Interface component of the application).
- Dapper (for SQL integration)

```Initial Installation Steps```
- SQL Server is required for this project.
- The project uses a localhost database, which can be configured via the App.Config file (which feeds directly into the DatabaseConnection class).
- Dapper and Spectre packages also need to be installed for the project.

```Project details```
- The user is able to create stacks for flashcards, which are hosted in an SQL table.
Stacks cannot have duplicate names, and as stack names are unique, I've had the "Stack name" value set as the primary key in the Stacks SQL table.

- The user can then create flashcards for the stack, which are linked via the Stack name (as a foreign key).

- Finally, the user can study their flashcards by using the "Study Area" functionality.
  
- This allows users to review their flashcards, give an answer to the given flashcard, which is scored and then added to a study area SQL table.

<img width="256" height="185" alt="image" src="https://github.com/user-attachments/assets/96f26582-2341-43a4-992c-f6b2505e0993" />

The "main menu" of the application - Users navigate via the menu by using the arrow keys on their keyboard.

<img width="434" height="228" alt="image" src="https://github.com/user-attachments/assets/2015dbba-20d4-42ff-9490-1cf7806635f1" />

Example of the "Study Area" functionality, which allows users to study their flashcards, which are scored and then added to a database table (the date of the session is also stored).

```Key takeaways from the project```
- It took me some time to begin this project compared to others, which was mainly me getting used to SQL Server. I generally use a Sillicon Mac for development, so getting used to using SQL Server outside of using Docker (which will be useful for the future) was a good learning experience.
- I also gained a lot from learning more about SQL, such as linking tables and using foreign keys.
- The project was more complex to the previous ones, and I enjoyed the challenge of planning out the application, seeing how I'd build on the existing application when implementing the "Study Area" section of the app, as well as dealing with more complexity around the database storage.
 
```Future considerations```
- In the future, I'd definitely like to ensure that my code is more maintainable and "neater", which can especially be seen in my UserInterface class, which I find quite complex and could potentially be simplified.
- I have a few nested If statements for example which could potentially be refactored.

```Overview of the project```
- Overall, I really enjoyed the project and would like to thank the C# Academy for giving me the opportunity to complete this, as well as the Discord community for their inspiration on the project.
- I look forward to working on the "Drinks Info" project next!

