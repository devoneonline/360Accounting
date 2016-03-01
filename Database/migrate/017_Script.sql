BEGIN_SETUP:

CREATE TABLE [dbo].[tbCustomerSite]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[CustomerId] [bigint] NOT NULL,
[SiteName] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SiteAddress] [varchar] (255) NULL,
[SiteContact] [varchar] (15) NULL,
[TaxCodeId] [bigint] NOT NULL,
[CodeCombinationId] [bigint] NOT NULL,
[StartDate] [datetime] NULL,
[EndDate] [datetime] NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NOT NULL,
[UpdateDate] [datetime] NOT NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbCustomerSite] ADD CONSTRAINT [PK_tbCustomerSite] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys

ALTER TABLE [dbo].[tbCustomerSite] ADD CONSTRAINT [FK_tbCustomerSite_tbCustomer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[tbCustomer] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[tbCustomerSite] ADD CONSTRAINT [FK_tbCustomerSite_tbTax] FOREIGN KEY ([TaxCodeId]) REFERENCES [dbo].[tbTax] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[tbCustomerSite] ADD CONSTRAINT [FK_tbCustomerSite_tbCodeCombinition] FOREIGN KEY ([CodeCombinationId]) REFERENCES [dbo].[tbCodeCombinition] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

END_SETUP: