CREATE TABLE [dbo].[EventStates] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (80)   NULL,
    [Content]   VARCHAR (2000) NULL,
    [SenderId]  VARCHAR (80)   NOT NULL,
	[Direction] INT            NOT NULL,
    [EventFrom] DATETIME2 (7)  NOT NULL,
    [EventTo]   DATETIME2 (7)  NOT NULL
	
);

