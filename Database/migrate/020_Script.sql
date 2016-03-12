BEGIN_SETUP:

CREATE TABLE [dbo].[tbInvoiceSource]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[SOBId] [bigint] NOT NULL,
[CodeCombinationId] [bigint] NOT NULL,
[CompanyId] [bigint] NOT NULL,
[Description] [varchar] (50) NOT NULL,
[StartDate] [datetime] NOT NULL,
[EndDate] [datetime] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO

ALTER TABLE [dbo].[tbInvoiceSource] ADD CONSTRAINT [PK_InvoiceSource] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys

ALTER TABLE [dbo].[tbInvoiceSource] ADD CONSTRAINT [FK_tbInvoiceSource_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id])
ALTER TABLE [dbo].[tbInvoiceSource] ADD CONSTRAINT [FK_tbInvoiceSource_tbCodeCombinition] FOREIGN KEY ([CodeCombinationId]) REFERENCES [dbo].[tbCodeCombinition] ([Id])
GO

END_SETUP: