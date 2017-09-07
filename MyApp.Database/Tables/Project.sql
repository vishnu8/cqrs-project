﻿CREATE TABLE [dbo].[Project]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[Name] VARCHAR(100) NOT NULL,
	[ClientId] UNIQUEIDENTIFIER NOT NULL,
	[Description] VARCHAR(250) NOT NULL,
	[CompletionDate] DATETIME NOT NULL,
	[Active] BIT NOT NULL, 
    CONSTRAINT [FK_Project_ToClient] FOREIGN KEY ([ClientId]) REFERENCES [Client]([Id])
)