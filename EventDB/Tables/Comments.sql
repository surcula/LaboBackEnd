CREATE TABLE [dbo].[Comments]
(
	[CommentId] INT NOT NULL PRIMARY KEY IDENTITY,
	[Title] Varchar (max) not null,
	[Date] DATETIME2 NOT NULL,
	[Message] Varchar(max) not null,
	[EventId] int NOT NULL,
	[PersonId] int NOT NULL, 
    CONSTRAINT [FK_Comments_Persons] FOREIGN KEY ([PersonId]) REFERENCES [Persons]([PersonId]), 
    CONSTRAINT [FK_Comments_Events] FOREIGN KEY ([EventId]) REFERENCES [Events]([EventId]),
)
