﻿CREATE TABLE [dbo].[SentEvents] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [ReceivedEventId] INT            NULL,
    [Name]            VARCHAR (80)   NULL,
    [Content]         VARCHAR (2000) NULL,
    [EventDate]       DATETIME2  NOT NULL,
    [SenderId]        VARCHAR (80)   NOT NULL,
    [SendDate]        DATETIME2  NOT NULL,
    [CreateDate]      DATETIME2   NOT NULL,
    CONSTRAINT [PK_SentEvents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SentEventsReceivedEventId] FOREIGN KEY ([ReceivedEventId]) REFERENCES [dbo].[ReceivedEvents] ([Id])
);

