CREATE TABLE [dbo].[Market] (
    [Id]            INT          IDENTITY (1, 1) NOT NULL,
    [SellerName]    VARCHAR (32) NOT NULL,
    [ResourceType]  INT          NOT NULL,
    [Count]         INT          NOT NULL,
    [Price]         INT          NOT NULL,
    [HighestBidder] VARCHAR (32) NULL,
    [Bid]           INT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Market_Players] FOREIGN KEY ([SellerName]) REFERENCES [dbo].[Players] ([Name]),
    CONSTRAINT [FK_Market_Players2] FOREIGN KEY ([HighestBidder]) REFERENCES [dbo].[Players] ([Name]),
    CONSTRAINT [CK_Market] CHECK ([SellerName]<>[HighestBidder]),
    CONSTRAINT [CK_Market_Resources] CHECK ([ResourceType]>=(0) AND [ResourceType]<(8)),
	CONSTRAINT [CK_Market_Count_NotNegative] CHECK ([Count] >= 0), 
	CONSTRAINT [CK_Market_Price_NotNegative] CHECK ([Price] >= 0), 
	CONSTRAINT [CK_Market_Bid_NotNegative] CHECK ([Bid] >= 0)
);

