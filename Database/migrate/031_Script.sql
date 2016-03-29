BEGIN_SETUP:

CREATE TABLE [dbo].[tbPayableInvoice]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[SOBId] [bigint] NOT NULL,
[VendorId] [bigint] NOT NULL,
[VendorSiteId] [bigint] NOT NULL,
[PeriodId] [bigint] NOT NULL,
[InvoiceTypeId] [bigint] NOT NULL,
[WHTaxId] [bigint] NULL,
[InvoiceNo] [varchar] (30) NOT NULL,
[InvoiceDate] [datetime] NOT NULL,
[Remarks] [varchar] (255) null,
[Status] [varchar] (30) null,
[Amount] [money] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

-- Constraints and Indexes

ALTER TABLE [dbo].[tbPayableInvoice] ADD CONSTRAINT [PK_PayableInvoice] PRIMARY KEY CLUSTERED ([Id])
ALTER TABLE [dbo].[tbPayableInvoice] ADD CONSTRAINT [FK_tbPayableInvoice_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE

--Need to put constraint with cascading currently giving error when cascade
---------------------------------------------------------------------------
ALTER TABLE [dbo].[tbPayableInvoice] ADD CONSTRAINT [FK_tbPayInvoice_tbVendor] FOREIGN KEY ([VendorId]) REFERENCES [dbo].[tbVendor] ([Id]) 
ALTER TABLE [dbo].[tbPayableInvoice] ADD CONSTRAINT [FK_tbPayableInvoice_tbVendorSite] FOREIGN KEY ([VendorSiteId]) REFERENCES [dbo].[tbVendorSite] ([Id]) ON DELETE NO Action ON UPDATE NO Action
ALTER TABLE [dbo].[tbPayableInvoice] ADD CONSTRAINT [FK_tbPayableInvoice_tbPayablePeriod] FOREIGN KEY ([PeriodId]) REFERENCES [dbo].[tbPayablePeriod] ([Id]) ON DELETE NO Action ON UPDATE NO Action
ALTER TABLE [dbo].[tbPayableInvoice] ADD CONSTRAINT [FK_tbPayableInvoice_tbInvoiceType] FOREIGN KEY ([InvoiceTypeId]) REFERENCES [dbo].[tbInvoiceType] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE [dbo].[tbPayableInvoice] ADD CONSTRAINT [FK_tbPayableInvoice_tbWithholding] FOREIGN KEY ([WHTaxId]) REFERENCES [dbo].[tbWithholding] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
---------------------------------------------------------------------------

GO

CREATE TABLE [dbo].[tbPayableInvoiceDetail]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[InvoiceId] [bigint] NOT NULL,
[CodeCombinationId] [bigint] NULL,
[Amount] [money] NOT NULL,
[Description] [varchar] (255) null,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

-- Constraints and Indexes

ALTER TABLE [dbo].[tbPayableInvoiceDetail] ADD CONSTRAINT [PK_tbPayableInvoiceDetail] PRIMARY KEY CLUSTERED ([Id])
ALTER TABLE [dbo].[tbPayableInvoiceDetail] ADD CONSTRAINT [FK_tbPayableInvoiceDetail_PayInvoice] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[tbPayableInvoice] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE [dbo].[tbPayableInvoiceDetail] ADD CONSTRAINT [FK_tbPayableInvoiceDetail_tbCodeCombinition] FOREIGN KEY ([CodeCombinationId]) REFERENCES [dbo].[tbCodeCombinition] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE

GO

update tbFeature set Href = '~/PayableInvoice' where id = 48
update tbFeature set Href = '~/Bank' where id = 62
update tbFeature set href = '~/Withholding' where id = 51
update tbFeature set href = '~/Vendor' where id = 44
update tbFeature set href = '~/InvoiceType' where id = 46
update tbFeature set href = '~/PayablePeriod' where id = 52
update tbFeature set href = '~/Receipt' where id = 29
END_SETUP: