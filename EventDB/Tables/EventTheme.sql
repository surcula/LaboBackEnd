CREATE TABLE [dbo].[EventTheme]
(
	[EventThemeId] INT NOT NULL PRIMARY KEY IDENTITY,
	[EventId] int NOT NULL,
	[ThemeId] int NOT NULL, 
    CONSTRAINT [FK_EventTheme_Events] FOREIGN KEY ([EventId]) REFERENCES [Events]([EventId]), 
    CONSTRAINT [FK_EventTheme_Themes] FOREIGN KEY ([ThemeId]) REFERENCES [Themes]([ThemeId]),

)
