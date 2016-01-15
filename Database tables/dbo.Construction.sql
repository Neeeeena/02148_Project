CREATE TABLE [dbo].[Construction] (
    [Name]     VARCHAR (32) NOT NULL,
    [Cottage]  INT          DEFAULT ((0)) NOT NULL,
    [Forge]    INT          DEFAULT ((0)) NOT NULL,
    [Mill]     INT          DEFAULT ((0)) NOT NULL,
    [Farm]     INT          DEFAULT ((0)) NOT NULL,
    [Townhall] INT          DEFAULT ((0)) NOT NULL,
    [Goldmine] INT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Name] ASC),
    CONSTRAINT [FK_Construction_Players] FOREIGN KEY ([Name]) REFERENCES [dbo].[Players] ([Name]),
    CONSTRAINT [CK_Construction_Cottage_NotNegative] CHECK ([Cottage]>=(0)),
    CONSTRAINT [CK_Construction_Farm_NotNegative] CHECK ([Farm]>=(0)),
    CONSTRAINT [CK_Construction_Forge_NotNegative] CHECK ([Forge]>=(0)),
    CONSTRAINT [CK_Construction_Goldmine_NotNegative] CHECK ([Goldmine]>=(0)),
    CONSTRAINT [CK_Construction_Mill_NotNegative] CHECK ([Mill]>=(0)),
    CONSTRAINT [CK_Construction_Townhall_NotNegative] CHECK ([Townhall]>=(0))
);

