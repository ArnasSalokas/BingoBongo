# .NET Core API template

A guideline of how every new project structure should be with containing libraries already.

In this repository we push new updates/improvements of our template project. So please feel free to contribute :smiley: 

List of technologies and libraries are used:

* ORM:
    - We are using Dapper with our created DataStore namespace who has mapping to object functionality (Only for MS SQL/T-SQL)

* Unit tests libs:

    - Xunit
    - Moq
    - Autofixture

* Authentification/Authorization:
    
    - Simple bearer token (in the future it's a good idea to move to JWT bearer token)

* Database migrations:

    - FluentMigrator (we write meanually migrations)
    
* Dependency injection container:

    - .NET Core built in container
    
* To deploy a project:

    - Usually we deploy to Azure but it depends on requirements
    - Azure Database is used
    
Will be added in the future.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

#### Three ways of developing .NET Core on Windows

* Command line

* Visual Studio (Visual Studio 2017 version 15.9 or higher for .NET Core 2.2) [Download link](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2019)

* Visual Studio Code

####  To update Visual Studio to use .NET Core 2.2 SDK (if it's not yet updated)

* Install the .NET Core 2.2 SDK [Link](https://dotnet.microsoft.com/download).

* If you want your project to use the latest .NET Core runtime, retarget each existing or new .NET Core project to .NET Core 2.2 using the following instructions:

* On the Project menu, choose Properties.

* In the Target framework selection menu, set the value to .NET Core 2.2.

![image](https://docs.microsoft.com/en-us/dotnet/core/media/windows-prerequisites/targeting-dotnet-core.jpg)

#### Once you have Visual Studio configured with .NET Core 2.2 SDK, you can do the following actions

* Open, build, and run existing .NET Core 1.x and 2.x projects.

* Retarget .NET Core 1.x and 2.x projects to .NET Core 2.2, build, and run.

* Clone this project with git clone "url" command.

#### Sql Server

* Download Microsoft SQL Server Management Studio (preferable above 17 vesion). [Link](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-2017).

* Download SQL Server 2017 Express edition [Link](https://www.microsoft.com/en-us/sql-server/sql-server-editions-express).

* Create a new local database with the name in src/Template.Api/appsettings.json -> Initial Catalog property's name.

### Once you are ready you can build and run this project.

### Just after the project was run automatic migrations will work and new tables appear on your manually created database.
