# WebForum

### Create db if necessary by running following commands in powershell in the project directory:
1. dotnet ef migrations add InitialCreate -c UsersDbContext
2. dotnet ef database update -c UsersDbContext
3. dotnet ef migrations add InitialCreate -c ForumDbContext
4. dotnet ef database update -c ForumDbContext

### Postman: 
[Postman url](https://api.postman.com/collections/10754045-8effa15f-9708-4789-bda6-0a0e6d917fe5?access_key=PMAT-01J2TD9H6XMM3BN60TJPAZD2BX)
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
