BEGIN_SETUP:

DROP TABLE tbTaxDum --Drop previous tax dummy table that was made to do vendors and customers input.

CREATE TABLE [dbo].[tbTax]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[SOBId] [bigint] NOT NULL,
[TaxName] [varchar] (30) NOT NULL,
[StartDate] [datetime] NULL,
[EndDate] [datetime] NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

-- Constraints and Indexes

ALTER TABLE [dbo].[tbTax] ADD CONSTRAINT [PK_tbTax] PRIMARY KEY CLUSTERED ([Id])
ALTER TABLE [dbo].[tbTax] ADD CONSTRAINT [FK_tbTax_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
--Need to put constraint for customer site & vendors

GO

CREATE TABLE [dbo].[tbTaxDetail]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[TaxId] [bigint] NOT NULL,
[CodeCombinationId] [bigint] NOT NULL,
[Rate] [money] NOT NULL,
[StartDate] [datetime] NULL,
[EndDate] [datetime] NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbTaxDetail] ADD CONSTRAINT [PK_tbTaxDetail] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys

ALTER TABLE [dbo].[tbTaxDetail] ADD CONSTRAINT [FK_tbTaxDetail_tbTax] FOREIGN KEY ([TaxId]) REFERENCES [dbo].[tbTax] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[tbTaxDetail] ADD CONSTRAINT [FK_tbTaxDetail_tbCodeCombinition] FOREIGN KEY ([CodeCombinationId]) REFERENCES [dbo].[tbCodeCombinition] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

END_SETUP: