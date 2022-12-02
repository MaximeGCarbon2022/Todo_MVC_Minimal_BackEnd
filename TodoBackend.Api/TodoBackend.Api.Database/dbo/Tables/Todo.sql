CREATE TABLE [dbo].[Todo] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Title]     VARCHAR (MAX)    NOT NULL,
    [Completed] BIT              DEFAULT ((0)) NULL,
    [Order]     INT              DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

