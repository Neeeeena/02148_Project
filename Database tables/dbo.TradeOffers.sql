CREATE TABLE [dbo].[TradeOffers] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [SellerName]   VARCHAR (32) NOT NULL,
    [RecieverName] VARCHAR (32) NOT NULL,
    [ResourceType] INT          NOT NULL,
    [Count]        INT          NOT NULL,
    [PriceType]    INT          NOT NULL,
    [Price]        INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TradeOffers_Players_1] FOREIGN KEY ([RecieverName]) REFERENCES [dbo].[Players] ([Name]),
    CONSTRAINT [FK_TradeOffers_Players] FOREIGN KEY ([SellerName]) REFERENCES [dbo].[Players] ([Name]),
    CONSTRAINT [CK_TradeOffers] CHECK ([SellerName]<>[RecieverName]),
    CONSTRAINT [CK_TradeOffers_Resources] CHECK ([ResourceType] >= 0 AND [ResourceType] <= 7),
	CONSTRAINT [CK_TradeOffers_PriceType] CHECK ([PriceType] >= 0 AND [PriceType] <= 7), 
	CONSTRAINT [CK_TradeOffers_Count_NotNegative] CHECK ([Count] >= 0), 
	CONSTRAINT [CK_TradeOffers_Price_NotNegative] CHECK ([Price] >= 0)
);

