CREATE TABLE [dbo].[Chat] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Message]      VARCHAR (255) NOT NULL,
    [SenderName]   VARCHAR (32)  NOT NULL,
    [RecieverName] VARCHAR (32)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Chat_Players] FOREIGN KEY ([SenderName]) REFERENCES [dbo].[Players] ([Name]),
    CONSTRAINT [FK_Chat_Players_1] FOREIGN KEY ([RecieverName]) REFERENCES [dbo].[Players] ([Name])
);

