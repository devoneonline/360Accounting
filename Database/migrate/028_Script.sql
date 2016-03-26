BEGIN_SETUP:

CREATE TABLE [dbo].[tbWarehouse]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[CompanyId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[WarehouseName] [varchar] (50) NOT NULL,
[Status] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbWarehouse] ADD CONSTRAINT [PK_tbWarehouse] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys
ALTER TABLE [dbo].[tbWarehouse] ADD CONSTRAINT [FK_tbWarehouse_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

update tbFeature set Href = '~/Warehouse' where id = 72

END_SETUP: