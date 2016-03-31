BEGIN_SETUP:

CREATE TABLE [dbo].[tbMoveOrder]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[CompanyId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[MoveOrderNo]	[varchar] (30) NOT NULL,
[MoveOrderDate] [datetime] NOT NULL,
[Description] [varchar] (255) null,
[DateRequired] [datetime] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

-- Constraints and Indexes

ALTER TABLE [dbo].[tbMoveOrder] ADD CONSTRAINT [PK_tbMoveOrder] PRIMARY KEY CLUSTERED ([Id])
ALTER TABLE [dbo].[tbMoveOrder] ADD CONSTRAINT [FK_tbMoveOrder_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE

GO

CREATE TABLE [dbo].[tbMoveOrderDetail]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[MoveOrderId] [bigint] NOT NULL,
[ItemId] [bigint] NOT NULL,
[LocatorId] [bigint] NOT NULL,
[WarehouseId] [bigint] NOT NULL,
[LotNo] [varchar] (30) NOT NULL,
[SerialNo] [varchar] (30) NOT NULL,
[DateRequired] [datetime] NULL,
[Quantity] [decimal] (18,2) null,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

-- Constraints and Indexes

ALTER TABLE [dbo].[tbMoveOrderDetail] ADD CONSTRAINT [PK_tbMoveOrderDetail] PRIMARY KEY CLUSTERED ([Id])
ALTER TABLE [dbo].[tbMoveOrderDetail] ADD CONSTRAINT [FK_tbMoveOrderDetail_tbMoveOrder] FOREIGN KEY ([MoveOrderId]) REFERENCES [dbo].[tbMoveOrder] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE [dbo].[tbMoveOrderDetail] ADD CONSTRAINT [FK_tbMoveOrderDetail_tbItem] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[tbItem] ([Id])
ALTER TABLE [dbo].[tbMoveOrderDetail] ADD CONSTRAINT [FK_tbMoveOrderDetail_tbLocator] FOREIGN KEY ([LocatorId]) REFERENCES [dbo].[tbLocator] ([Id])
ALTER TABLE [dbo].[tbMoveOrderDetail] ADD CONSTRAINT [FK_tbMoveOrderDetail_tbWarehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [dbo].[tbWarehouse] ([Id])

GO

CREATE TABLE [dbo].[tbLotNumber]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[CompanyId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[ItemId] [bigint] NOT NULL,
[LotNo] [varchar] (30) NOT NULL,
[SourceType] [varchar] (30) NOT NULL,
[SourceId] [bigint] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

-- Constraints and Indexes

ALTER TABLE [dbo].[tbLotNumber] ADD CONSTRAINT [PK_tbLotNumber] PRIMARY KEY CLUSTERED ([Id])
ALTER TABLE [dbo].[tbLotNumber] ADD CONSTRAINT [FK_tbLotNumber_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE [dbo].[tbLotNumber] ADD CONSTRAINT [FK_tbLotNumber_tbItem] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[tbItem] ([Id])

GO

CREATE TABLE [dbo].[tbSerialNumber]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[LotNo] [varchar] (30) NOT NULL,
[SerialNo] [varchar] (30) NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

-- Constraints and Indexes

ALTER TABLE [dbo].[tbSerialNumber] ADD CONSTRAINT [PK_tbSerialNumber] PRIMARY KEY CLUSTERED ([Id])

GO

update tbFeature set Href = '~/MoveOrder' where id = 79

GO

END_SETUP: