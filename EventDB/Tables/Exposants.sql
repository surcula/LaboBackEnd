CREATE TABLE [dbo].[Exposants]
(
	[PersonEventId] INT NOT NULL PRIMARY KEY IDENTITY,
	[PersonId] int NOT NULL,
	[EventId] int NOT NULL,
	[Name] Varchar(255) NOT NULL,
	[Date] DATETIME2 ,
	[Gsm] varchar (20),
	[Comments] Varchar(max),
	[Status] int not null DEFAULT 1,
    CONSTRAINT [FK_Exposants_Persons] FOREIGN KEY ([PersonId]) REFERENCES [Persons]([PersonId]), 
    CONSTRAINT [FK_Exposants_Events] FOREIGN KEY ([EventId]) REFERENCES [Events]([EventId]),
)
