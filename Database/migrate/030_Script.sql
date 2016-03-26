BEGIN_SETUP:

CREATE TABLE [dbo].[tbLocator]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[CompanyId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[Description] [varchar] (255) NOT NULL,
[Status] [varchar] (50) NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbLocator] ADD CONSTRAINT [PK_tbLocator] PRIMARY KEY CLUSTERED ([Id])
GO
-- Foreign Keys
ALTER TABLE [dbo].[tbLocator] ADD CONSTRAINT [FK_tbLocator_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

CREATE TABLE [dbo].[tbLocatorWarehouse]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[SOBId] [bigint] NOT NULL,
[LocatorId] [bigint] NOT NULL,
[WarehouseId] [bigint] NOT NULL,
[StartDate] [datetime] NULL,
[EndDate] [datetime] NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbLocatorWarehouse] ADD CONSTRAINT [PK_tbLocatorWarehouse] PRIMARY KEY CLUSTERED ([Id])
GO
-- Foreign Keys

ALTER TABLE [dbo].[tbLocatorWarehouse] ADD CONSTRAINT [FK_tbLocatorWarehouse_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[tbLocatorWarehouse] ADD CONSTRAINT [FK_tbLocatorWarehouse_tbLocator] FOREIGN KEY ([LocatorId]) REFERENCES [dbo].[tbItem] ([Id])
GO
ALTER TABLE [dbo].[tbLocatorWarehouse] ADD CONSTRAINT [FK_tbLocatorWarehouse_tbWarehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [dbo].[tbWarehouse] ([Id])
GO

update tbFeature set Href = '~/Locator' where id = 73

END_SETUP: