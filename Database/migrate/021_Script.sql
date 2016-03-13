BEGIN_SETUP:

CREATE TABLE [dbo].[tbVendor]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[Name] [varchar] (30) NOT NULL,
[CompanyId] [bigint] NOT NULL,
[Address] [varchar] (255) NULL,
[ContactNo] [varchar] (15) NULL,
[StartDate] [datetime] NULL,
[EndDate] [datetime] NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

-- Constraints and Indexes

ALTER TABLE [dbo].[tbVendor] ADD CONSTRAINT [PK_tbVendor] PRIMARY KEY CLUSTERED ([Id])
ALTER TABLE [dbo].[tbVendor] ADD CONSTRAINT [FK_tbVendor_tbCompany] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[tbCompany] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE

GO

CREATE TABLE [dbo].[tbVendorSite]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[VendorId] [bigint] NOT NULL,
[Name] [varchar] (30) NOT NULL,
[Address] [varchar] (255) NULL,
[Contact] [varchar] (15) NULL,
[TaxCodeId] [bigint] NULL,
[CodeCombinationId] [bigint] NOT NULL,
[StartDate] [datetime] NULL,
[EndDate] [datetime] NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbVendorSite] ADD CONSTRAINT [PK_tbVendorSite] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys

ALTER TABLE [dbo].[tbVendorSite] ADD CONSTRAINT [FK_tbVendorSite_tbVendor] FOREIGN KEY ([VendorId]) REFERENCES [dbo].[tbVendor] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[tbVendorSite] ADD CONSTRAINT [FK_tbVendorSite_tbTax] FOREIGN KEY ([TaxCodeId]) REFERENCES [dbo].[tbTax] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[tbVendorSite] ADD CONSTRAINT [FK_tbVendorSite_tbCodeCombinition] FOREIGN KEY ([CodeCombinationId]) REFERENCES [dbo].[tbCodeCombinition] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

END_SETUP:

