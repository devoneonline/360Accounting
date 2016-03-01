BEGIN_SETUP:

CREATE TABLE [dbo].[tbTax]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[TaxName] [varchar] (30) NOT NULL,
[TaxRate] [money] NOT NULL,
[StartDate] [datetime] NULL,
[EndDate] [datetime] NULL,
[CodeCombinationId] [bigint] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NOT NULL,
[UpdateDate] [datetime] NOT NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbTax] ADD CONSTRAINT [PK_tbTax] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys

--ALTER TABLE [dbo].[tbTax] ADD CONSTRAINT [FK_tbTax_tbCodeCombinition] FOREIGN KEY ([CodeCombinationId]) REFERENCES [dbo].[tbCodeCombinition] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
--GO

END_SETUP: