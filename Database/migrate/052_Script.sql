BEGIN_SETUP:

CREATE TABLE [dbo].[tbBuyer]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[Name] [varchar] (30) NOT NULL,
[SOBId] [bigint] NOT NULL,
[CompanyId] [bigint] NOT NULL,
[StartDate] [datetime] NULL,
[EndDate] [datetime] NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

-- Constraints and Indexes

ALTER TABLE [dbo].[tbBuyer] ADD CONSTRAINT [PK_tbBuyer] PRIMARY KEY CLUSTERED ([Id])
ALTER TABLE [dbo].[tbBuyer] ADD CONSTRAINT [FK_tbBuyer_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE

GO

CREATE TABLE [dbo].[tbPurchasingPeriod]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[CalendarId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[CompanyId] [bigint] NOT NULL,
[Status] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbPurchasingPeriod] ADD CONSTRAINT [PK_tbPurchasingPeriod] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys

ALTER TABLE [dbo].[tbPurchasingPeriod] ADD CONSTRAINT [FK_tbPurchasingPeriod_tbCalendar] FOREIGN KEY ([CalendarId]) REFERENCES [dbo].[tbCalendar] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[tbPayablePeriod] ADD CONSTRAINT [FK_tbPurchasingPeriod_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id])
GO

CREATE TABLE tbRFQ
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[CompanyId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[BuyerId] [bigint] NOT NULL,
[RFQNo] [varchar] (30) NOT NULL,
[RFQDate] [DateTime] NOT NULL,
[CloseDate] [DateTime] NULL,
[Status] [varchar] (30) null,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO

ALTER TABLE tbRFQ ADD CONSTRAINT [PK_tbRFQ] PRIMARY KEY CLUSTERED  ([Id])
ALTER TABLE tbRFQ ADD CONSTRAINT [FK_tbRFQ_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE tbRFQ ADD CONSTRAINT [FK_tbRFQ_tbBuyer] FOREIGN KEY ([BuyerId]) REFERENCES [dbo].[tbBuyer] ([Id])

GO

CREATE TABLE tbRFQDetail
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[RFQId] [bigint] NOT NULL,
[ItemId] [bigint] NOT NULL,
[Quantity] [decimal] (18,2) NOT NULL,
[TargetPrice] [money] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

ALTER TABLE tbRFQDetail ADD CONSTRAINT [PK_tbRFQDetail] PRIMARY KEY CLUSTERED  ([Id])
ALTER TABLE tbRFQDetail ADD CONSTRAINT [FK_tbRFQDetail_tbRFQ] FOREIGN KEY ([RFQId]) REFERENCES [dbo].[tbRFQ] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE tbRFQDetail ADD CONSTRAINT [FK_tbRFQDetail_tbItem] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[tbItem] ([Id])

GO

CREATE TABLE tbRequisition
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[CompanyId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[BuyerId] [bigint] NOT NULL,
[RequisitionNo] [varchar] (30) NOT NULL,
[RequisitionDate] [DateTime] NOT NULL,
[Description] [varchar] (255) null,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

ALTER TABLE tbRequisition ADD CONSTRAINT [PK_tbRequisition] PRIMARY KEY CLUSTERED  ([Id])
ALTER TABLE tbRequisition ADD CONSTRAINT [FK_tbRequisition_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE tbRequisition ADD CONSTRAINT [FK_tbRequisition_tbBuyer] FOREIGN KEY ([BuyerId]) REFERENCES [dbo].[tbBuyer] ([Id])

GO

CREATE TABLE tbRequisitionDetail
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[RequisitionId] [bigint] NOT NULL,
[ItemId] [bigint] NOT NULL,
[VendorId] [bigint] NULL,
[VendorSiteId] [bigint] NULL,
[Quantity] [decimal] (18,2) NOT NULL,
[Price] [money] NOT NULL,
[NeedByDate] [DateTime] NULL,
[Status] [varchar] (30),
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

ALTER TABLE tbRequisitionDetail ADD CONSTRAINT [PK_tbRequisitionDetail] PRIMARY KEY CLUSTERED  ([Id])
ALTER TABLE tbRequisitionDetail ADD CONSTRAINT [FK_tbRequisitionDetail_tbRequisition] FOREIGN KEY ([RequisitionId]) REFERENCES [dbo].[tbRequisition] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE tbRequisitionDetail ADD CONSTRAINT [FK_tbRequisitionDetail_tbItem] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[tbItem] ([Id])

GO

CREATE TABLE tbPurchaseOrder
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[CompanyId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[VendorId] [bigint] NOT NULL,
[VendorSiteId] [bigint] NOT NULL,
[BuyerId] [bigint] NULL,
[PONo] [varchar] (30) NOT NULL,
[PODate] [DateTime] NOT NULL,
[Description] [varchar] (255) null,
[Status] [varchar] (255) null,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

ALTER TABLE tbPurchaseOrder ADD CONSTRAINT [PK_tbPurchaseOrder] PRIMARY KEY CLUSTERED  ([Id])
ALTER TABLE tbPurchaseOrder ADD CONSTRAINT [FK_tbPurchaseOrder_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE tbPurchaseOrder ADD CONSTRAINT [FK_tbPurchaseOrder_tbVendor] FOREIGN KEY ([VendorId]) REFERENCES [dbo].[tbVendor] ([Id])
ALTER TABLE tbPurchaseOrder ADD CONSTRAINT [FK_tbPurchaseOrder_tbVendorSite] FOREIGN KEY ([VendorSiteId]) REFERENCES [dbo].[tbVendorSite] ([Id])
ALTER TABLE tbPurchaseOrder ADD CONSTRAINT [FK_tbPurchaseOrder_tbBuyer] FOREIGN KEY ([BuyerId]) REFERENCES [dbo].[tbBuyer] ([Id])

GO

CREATE TABLE tbPurchaseOrderDetail
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[POId] [bigint] NOT NULL,
[ItemId] [bigint] NOT NULL,
[Quantity] [decimal] (18,2) NOT NULL,
[Price] [money] NOT NULL,
[NeedByDate] [DateTime] NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

ALTER TABLE tbPurchaseOrderDetail ADD CONSTRAINT [PK_tbPurchaseOrderDetail] PRIMARY KEY CLUSTERED  ([Id])
ALTER TABLE tbPurchaseOrderDetail ADD CONSTRAINT [FK_tbPurchaseOrderDetail_tbPurchaseOrder] FOREIGN KEY ([POId]) REFERENCES [dbo].[tbPurchaseOrder] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE tbPurchaseOrderDetail ADD CONSTRAINT [FK_tbPurchaseOrderDetail_tbItem] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[tbItem] ([Id])

GO

update tbFeature set Href = '~/Buyer' where id = 92
update tbFeature set Href = '~/RFQ' where id = 93
update tbFeature set Href = '~/Requisition' where id = 95
update tbFeature set Href = '~/PurchaseOrder' where id = 96
update tbFeature set Href = '~/PurchasingPeriod' where id = 98

END_SETUP: