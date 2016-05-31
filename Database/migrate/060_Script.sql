BEGIN_SETUP:

CREATE TABLE tbReceiving
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[ReceiptNo] [varchar] (30) NOT NULL,
[Date] [DateTime] NOT NULL,
[POId] [bigint] NOT NULL,
[DCNo] [varchar] (30) NOT NULL,
[CompanyId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

ALTER TABLE tbReceiving ADD CONSTRAINT [PK_tbReceiving] PRIMARY KEY CLUSTERED  ([Id])
ALTER TABLE tbReceiving ADD CONSTRAINT [FK_tbReceiving_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE tbReceiving ADD CONSTRAINT [FK_tbReceiving_tbPurchaseOrder] FOREIGN KEY ([POId]) REFERENCES [dbo].[tbPurchaseOrder] ([Id])

GO

CREATE TABLE tbReceivingDetail
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[ReceiptId] [bigint] NOT NULL,
[ItemId] [bigint] NOT NULL,
[Quantity] [decimal] (18,2) NOT NULL,
[WarehouseId] [bigint] NOT NULL,
[LocatorId] [bigint] NOT NULL,
[LotNoId] [bigint] NULL,
[SerialNo] [varchar] (255) NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

ALTER TABLE tbReceivingDetail ADD CONSTRAINT [PK_tbReceivingDetail] PRIMARY KEY CLUSTERED  ([Id])
ALTER TABLE tbReceivingDetail ADD CONSTRAINT [FK_tbReceivingDetail_tbReceiving] FOREIGN KEY ([ReceiptId]) REFERENCES [dbo].[tbReceiving] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE tbReceivingDetail ADD CONSTRAINT [FK_tbReceivingDetail_tbItem] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[tbItem] ([Id])
ALTER TABLE tbReceivingDetail ADD CONSTRAINT [FK_tbReceivingDetail_tbWarehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [dbo].[tbWarehouse] ([Id])
ALTER TABLE tbReceivingDetail ADD CONSTRAINT [FK_tbReceivingDetail_tbLocator] FOREIGN KEY ([LocatorId]) REFERENCES [dbo].[tbLocator] ([Id])
ALTER TABLE tbReceivingDetail ADD CONSTRAINT [FK_tbReceivingDetail_tbLotNumber] FOREIGN KEY ([LotNoId]) REFERENCES [dbo].[tbLotNumber] ([Id])

GO

update tbFeature set Name = 'Receivable' where Id = 2

GO

ALTER TABLE	tbLotNumber ADD Qty [decimal] (18,2) NOT NULL DEFAULT 0

ALTER TABLE tbShipment ALTER COLUMN SerialNo varchar(MAX) null
ALTER TABLE tbShipment ADD Shipped bit NOT NULL DEFAULT 0

ALTER TABLE tbReceiving ADD Confirmed bit NOT NULL DEFAULT 0

Delete from tbReceivingDetail
ALTER TABLE tbReceivingDetail Add PODetailId [bigint] NOT NULL
ALTER TABLE tbReceivingDetail ADD CONSTRAINT [FK_tbReceivingDetail_tbPODetail] FOREIGN KEY ([PODetailId]) REFERENCES [dbo].[tbPurchaseOrderDetail] ([Id])


END_SETUP:
