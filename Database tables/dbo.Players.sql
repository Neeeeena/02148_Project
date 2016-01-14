CREATE TABLE [dbo].[Players] (
    [Name]  VARCHAR (32) NOT NULL,
    [Wood]  INT          DEFAULT ((0)) NOT NULL,
    [Clay]  INT          DEFAULT ((0)) NOT NULL,
    [Wool]  INT          DEFAULT ((0)) NOT NULL,
    [Stone] INT          DEFAULT ((0)) NOT NULL,
    [Iron]  INT          DEFAULT ((0)) NOT NULL,
    [Straw] INT          DEFAULT ((0)) NOT NULL,
    [Food]  INT          DEFAULT ((0)) NOT NULL,
    [Gold]  INT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Name] ASC), 
    CONSTRAINT [CK_Players_WoodNotNegative] CHECK (Wood >= 0), 
	CONSTRAINT [CK_Players_ClayNotNegative] CHECK (Clay >= 0), 
	CONSTRAINT [CK_Players_WoolNotNegative] CHECK (Wool >= 0), 
	CONSTRAINT [CK_Players_StoneNotNegative] CHECK (Stone >= 0), 
	CONSTRAINT [CK_Players_IronNotNegative] CHECK (Iron >= 0), 
	CONSTRAINT [CK_Players_StrawNotNegative] CHECK (Straw >= 0), 
	CONSTRAINT [CK_Players_FoodNotNegative] CHECK (Food >= 0), 
	CONSTRAINT [CK_Players_GoldNotNegative] CHECK (Gold >= 0)
);

