# WebForum

### Documentation:
Run in visual studio to open SwaggerUI. Database (Sqlite) was added in source control to make it easier. It also includes test data.

### Create db if necessary by running following commands in powershell in the project directory:
1. dotnet ef migrations add InitialCreate -c UsersDbContext
2. dotnet ef database update -c UsersDbContext
3. dotnet ef migrations add InitialCreate -c ForumDbContext
4. dotnet ef database update -c ForumDbContext

### Postman: 
[Postman url](https://www.postman.com/hermanvzyl-gmail-com/workspace/public/collection/10754045-8effa15f-9708-4789-bda6-0a0e6d917fe5?action=share&creator=10754045)
> Feel free to fork collection to edit.

### Users in db:
- admin@webforum.com (moderator)
- alpha@wf.test
- bravo@wf.test
- charlie@wf.test
- delta@wf.test
- echo@wf.test
- foxtrot@wf.test
- golf@wf.test

all passwords are: P@ssw0rd
