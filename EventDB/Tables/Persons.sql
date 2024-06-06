CREATE TABLE [dbo].[Persons]
(
	[PersonId] INT NOT NULL PRIMARY KEY IDENTITY,
	[Email] Varchar(255) not null UNIQUE,
    [password] NVARCHAR(255) NOT NULL, 
	[LastName] Varchar(70) not null,
	[FirstName] Varchar(70) not null,
	[IsBanned] Bit not null DEFAULT 0,
	[RoleId] INT not null DEFAULT 1, 
    CONSTRAINT [FK_Persons_Roles] FOREIGN KEY ([RoleId]) REFERENCES [Roles]([RoleId]),

)
