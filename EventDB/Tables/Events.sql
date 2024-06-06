CREATE TABLE [dbo].[Events]
(
	[EventId] INT NOT NULL PRIMARY KEY IDENTITY,
	[Title] Varchar(255),
	[BeginDate] DATETIME2,
	[EndDate] DATETIME2,
	[Address] Varchar(255),
	[Status] int not null,
	
	CONSTRAINT [CK_Event_Date] CHECK ([BeginDate] <= [EndDate]) 
)
