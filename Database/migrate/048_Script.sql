BEGIN_SETUP:

CREATE TABLE [dbo].[tbOrderType]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[CompanyId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[OrderTypeName] [varchar] (30) NOT NULL,
[DateFrom] [datetime] NULL,
[DateTo] [datetime] NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

-- Constraints and Indexes

ALTER TABLE [dbo].[tbOrderType] ADD CONSTRAINT [PK_tbOrderType] PRIMARY KEY CLUSTERED ([Id])
ALTER TABLE [dbo].[tbOrderType] ADD CONSTRAINT [FK_tbOrderType_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE

GO

CREATE TABLE [dbo].[tbOrderMaster]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[CompanyId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[OrderTypeId] [bigint] NOT NULL,
[OrderNo] [varchar] (30) NOT NULL,
[OrderDate] [Datetime] NOT NULL,
[CustomerID] [bigint] NOT NULL,
[CustomerSiteId] [bigint] NOT NULL,
[Remarks] [varchar] (255) NULL,
[Status] [varchar] (30) NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

ALTER TABLE [dbo].[tbOrderMaster] ADD CONSTRAINT [PK_tbOrderMaster] PRIMARY KEY CLUSTERED ([Id])
ALTER TABLE [dbo].[tbOrderMaster] ADD CONSTRAINT [FK_tbOrderMaster_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE [dbo].[tbOrderMaster] ADD CONSTRAINT [FK_tbOrderMaster_tbOrderType] FOREIGN KEY ([OrderTypeId]) REFERENCES [dbo].[tbOrderType] ([Id])
ALTER TABLE [dbo].[tbOrderMaster] ADD CONSTRAINT [FK_tbOrderMaster_tbCustomer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[tbCustomer] ([Id])
ALTER TABLE [dbo].[tbOrderMaster] ADD CONSTRAINT [FK_tbOrderMaster_tbCustomerSite] FOREIGN KEY ([CustomerSiteId]) REFERENCES [dbo].[tbCustomerSite] ([Id])

GO

CREATE TABLE [dbo].[tbOrderDetail]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[OrderId] [bigint] NOT NULL,
[ItemId] [bigint] NOT NULL,
[WarehouseId] [bigint] NOT NULL,
[Quantity] [decimal] (18,2) NOT NULL,
[Rate] [money] NOT NULL,
[Amount] [money] NOT NULL,
[TaxId] [bigint] NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

ALTER TABLE [dbo].[tbOrderDetail] ADD CONSTRAINT [PK_tbOrderDetail] PRIMARY KEY CLUSTERED ([Id])
ALTER TABLE [dbo].[tbOrderDetail] ADD CONSTRAINT [FK_tbOrderDetail_tbOrderMaster] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[tbOrderMaster] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE [dbo].[tbOrderDetail] ADD CONSTRAINT [FK_tbOrderDetail_tbItem] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[tbItem] ([Id])
ALTER TABLE [dbo].[tbOrderDetail] ADD CONSTRAINT [FK_tbOrderDetail_tbWarehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [dbo].[tbWarehouse] ([Id])
ALTER TABLE [dbo].[tbOrderDetail] ADD CONSTRAINT [FK_tbOrderDetail_tbTax] FOREIGN KEY ([TaxId]) REFERENCES [dbo].[tbTax] ([Id])


GO

CREATE TABLE [dbo].[tbShipment]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[CompanyId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[OrderId] [bigint] NOT NULL,
[LineId] [bigint] NOT NULL,
[WarehouseId] [bigint] NOT NULL,
[DeliveryDate] [datetime] NOT NULL,
[Quantity] [decimal] (18,2) NOT NULL,
[LocatorId] [bigint] NOT NULL,
[LotNo] [varchar] (30) NOT NULL,
[SerialNo] [varchar] (30) NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

ALTER TABLE [dbo].[tbShipment] ADD CONSTRAINT [PK_tbShipment] PRIMARY KEY CLUSTERED ([Id])
ALTER TABLE [dbo].[tbShipment] ADD CONSTRAINT [FK_tbShipment_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE [dbo].[tbShipment] ADD CONSTRAINT [FK_tbShipment_tbOrderMaster] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[tbOrderMaster] ([Id])
ALTER TABLE [dbo].[tbShipment] ADD CONSTRAINT [FK_tbShipment_tbOrderDetail] FOREIGN KEY ([LineId]) REFERENCES [dbo].[tbOrderDetail] ([Id])
ALTER TABLE [dbo].[tbShipment] ADD CONSTRAINT [FK_tbShipment_tbWarehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [dbo].[tbWarehouse] ([Id])
ALTER TABLE [dbo].[tbShipment] ADD CONSTRAINT [FK_tbShipment_tbLocator] FOREIGN KEY ([LocatorId]) REFERENCES [dbo].[tbLocator] ([Id])

GO

update tbFeature set Href = '~/OrderType' where id = 107
update tbFeature set Href = '~/SaleOrder' where id = 109
update tbFeature set Href = '~/Shipment' where id = 110

GO

END_SETUP: