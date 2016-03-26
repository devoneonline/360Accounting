BEGIN_SETUP:

CREATE TABLE [dbo].[tbInventoryPeriod]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[CompanyId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[CalendarId] [bigint] NOT NULL,
[Status] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbInventoryPeriod] ADD CONSTRAINT [PK_tbInventroyPeriod] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys
ALTER TABLE [dbo].[tbInventoryPeriod] ADD CONSTRAINT [FK_tbInventoryPeriod_tbCompany] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[tbCompany] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[tbInventoryPeriod] ADD CONSTRAINT [FK_tbInventoryPeriod_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id])
GO
ALTER TABLE [dbo].[tbInventoryPeriod] ADD CONSTRAINT [FK_tbInventoryPeriod_tbCalendar] FOREIGN KEY ([CalendarId]) REFERENCES [dbo].[tbCalendar] ([Id])
GO

CREATE TABLE [dbo].[tbItem]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[CompanyId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[COGSCodeCombinationId] [bigint] NOT NULL,
[SalesCodeCombinationId] [bigint] NOT NULL,
[ItemCode] [varchar] (50) NOT NULL,
[ItemName] [varchar] (50) NOT NULL,
[Description] [varchar] (255) NULL,
[Status] [varchar] (50) NOT NULL,
[DefaultBuyer] [varchar] (50) NULL,
[ReceiptRouting] [varchar] (50) NOT NULL,
[LotControl] [bit] NOT NULL,
[SerialControl] [bit] NOT NULL,
[Purchaseable] [bit] NOT NULL,
[Orderable] [bit] NOT NULL,
[Shipable] [bit] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbItem] ADD CONSTRAINT [PK_tbItem] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys
ALTER TABLE [dbo].[tbItem] ADD CONSTRAINT [FK_tbItem_tbCompany] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[tbCompany] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[tbItem] ADD CONSTRAINT [FK_tbItem_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id])
GO
ALTER TABLE [dbo].[tbItem] ADD CONSTRAINT [FK_tbItem_tbCodeCombinitionCOGS] FOREIGN KEY ([COGSCodeCombinationId]) REFERENCES [dbo].[tbCodeCombinition] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[tbItem] ADD CONSTRAINT [FK_tbItem_tbCodeCombinitionSales] FOREIGN KEY ([SalesCodeCombinationId]) REFERENCES [dbo].[tbCodeCombinition] ([Id])
GO

CREATE TABLE [dbo].[tbItemWarehouse]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[SOBId] [bigint] NOT NULL,
[ItemId] [bigint] NOT NULL,
[WarehouseId] [bigint] NOT NULL,
[StartDate] [datetime] NOT NULL,
[EndDate] [datetime] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbItemWarehouse] ADD CONSTRAINT [PK_tbItemWarehouse] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys

ALTER TABLE [dbo].[tbItemWarehouse] ADD CONSTRAINT [FK_tbItemWarehouse_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[tbItemWarehouse] ADD CONSTRAINT [FK_tbItemWarehouse_tbItem] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[tbItem] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[tbItemWarehouse] ADD CONSTRAINT [FK_tbItemWarehouse_tbWarehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [dbo].[tbWarehouse] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

update tbFeature set Href = '~/InventoryPeriod' where id = 81
update tbFeature set Href = '~/Item' where id = 71

END_SETUP: