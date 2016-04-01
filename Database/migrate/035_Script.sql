BEGIN_SETUP:

CREATE TABLE [dbo].[tbMiscellaneousTransaction]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[CompanyId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[CodeCombinationId] [bigint] NOT NULL,
[ItemId] [bigint] NOT NULL,
[WarehouseId] [bigint] NOT NULL,
[LocatorId] [bigint] NOT NULL,
[TransactionType] [varchar] (30) NOT NULL,
[TransactionDate] [datetime] NOT NULL,
[LotNo] [varchar] (30) NOT NULL,
[SerialNo] [varchar] (30) NOT NULL,
[Quantity] [decimal] (18,2) NOT NULL,
[Cost] [money] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

-- Constraints and Indexes

ALTER TABLE [dbo].[tbMiscellaneousTransaction] ADD CONSTRAINT [PK_tbMiscellaneousTransaction] PRIMARY KEY CLUSTERED ([Id])
ALTER TABLE [dbo].[tbMiscellaneousTransaction] ADD CONSTRAINT [FK_tbMiscellaneousTransaction_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE [dbo].[tbMiscellaneousTransaction] ADD CONSTRAINT [FK_tbMiscellaneousTransaction_tbCodeCombination] FOREIGN KEY ([CodeCombinationId]) REFERENCES [dbo].[tbCodeCombinition] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE [dbo].[tbMiscellaneousTransaction] ADD CONSTRAINT [FK_tbMiscellaneousTransaction_tbItem] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[tbItem] ([Id])
ALTER TABLE [dbo].[tbMiscellaneousTransaction] ADD CONSTRAINT [FK_tbMiscellaneousTransaction_tbWarehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [dbo].[tbWarehouse] ([Id])
ALTER TABLE [dbo].[tbMiscellaneousTransaction] ADD CONSTRAINT [FK_tbMiscellaneousTransaction_tbLocator] FOREIGN KEY ([LocatorId]) REFERENCES [dbo].[tbLocator] ([Id])

GO

update tbFeature set Href = '~/MiscellaneousTransactions' where id = 80

GO

END_SETUP: