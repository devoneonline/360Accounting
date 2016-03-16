BEGIN_SETUP:

CREATE TABLE [dbo].[tbInvoice]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[SOBId] [bigint] NOT NULL,
[PeriodId] [bigint] NOT NULL,
[CurrencyId] [bigint] NOT NULL,
[CustomerId] [bigint] NOT NULL,
[CustomerSiteId] [bigint] NOT NULL,
[CompanyId] [bigint] NOT NULL,
[InvoiceDate] [datetime] NOT NULL,
[InvoiceType] [varchar] (30) NOT NULL,
[InvoiceNo] [varchar] (30) NOT NULL,
[ConversionRate] [money] null,
[Remarks] [varchar] (255) null,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

-- Constraints and Indexes

ALTER TABLE [dbo].[tbInvoice] ADD CONSTRAINT [PK_tbInvoice] PRIMARY KEY CLUSTERED ([Id])
ALTER TABLE [dbo].[tbInvoice] ADD CONSTRAINT [FK_tbInvoice_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE

--Need to put constraint with cascading currently giving error when cascade
---------------------------------------------------------------------------
ALTER TABLE [dbo].[tbInvoice] ADD CONSTRAINT [FK_tbInvoice_tbCalendar] FOREIGN KEY ([PeriodId]) REFERENCES [dbo].[tbCalendar] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
ALTER TABLE [dbo].[tbInvoice] ADD CONSTRAINT [FK_tbInvoice_tbCurrency] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[tbCurrency] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
ALTER TABLE [dbo].[tbInvoice] ADD CONSTRAINT [FK_tbInvoice_tbCustomer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[tbCustomer] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
---------------------------------------------------------------------------

ALTER TABLE [dbo].[tbInvoice] ADD CONSTRAINT [FK_tbInvoice_tbCustomerSite] FOREIGN KEY ([CustomerSiteId]) REFERENCES [dbo].[tbCustomerSite] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION


--Need to put constraint for orders.

GO

CREATE TABLE [dbo].[tbInvoiceDetail]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[InvoiceId] [bigint] NOT NULL,
[InvoiceSourceId] [bigint] NULL,
[TaxId] [bigint] NULL,
[Quantity] [decimal] (18,2) null,
[Rate] [money] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

-- Constraints and Indexes

ALTER TABLE [dbo].[tbInvoiceDetail] ADD CONSTRAINT [PK_tbInvoiceDetail] PRIMARY KEY CLUSTERED ([Id])
ALTER TABLE [dbo].[tbInvoiceDetail] ADD CONSTRAINT [FK_tbInvoiceDetail_tbInvoice] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[tbInvoice] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE [dbo].[tbInvoiceDetail] ADD CONSTRAINT [FK_tbInvoiceDetail_tbInvoiceSource] FOREIGN KEY ([InvoiceSourceId]) REFERENCES [dbo].[tbInvoiceSource] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE

--Need to put constraint with cascading currently giving error when cascade
---------------------------------------------------------------------------
ALTER TABLE [dbo].[tbInvoiceDetail] ADD CONSTRAINT [FK_tbInvoiceDetail_tbTax] FOREIGN KEY ([TaxId]) REFERENCES [dbo].[tbTax] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
---------------------------------------------------------------------------

--Need to put constraint for items.

GO

update tbFeature set Href = '~/Invoice' where id = 27

END_SETUP: