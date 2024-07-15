# WebForum

Create db if necessary by running following commands in powershell in the project directory:
dotnet ef migrations add InitialCreate -c UsersDbContext
dotnet ef database update -c UsersDbContext
dotnet ef migrations add InitialCreate -c ForumDbContext
dotnet ef database update -c ForumDbContext

Postman url: https://api.postman.com/collections/10754045-8effa15f-9708-4789-bda6-0a0e6d917fe5?access_key=PMAT-01J2TD9H6XMM3BN60TJPAZD2BX
