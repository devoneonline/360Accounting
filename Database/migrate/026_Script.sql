BEGIN_SETUP:

CREATE TABLE [dbo].[tbInvoiceType]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[InvoiceType] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Meaning] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Description] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DateFrom] [datetime] NOT NULL,
[DateTo] [datetime] NOT NULL,
[SOBId] [bigint] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbInvoiceType] ADD CONSTRAINT [PK_tbInvoiceType] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys

ALTER TABLE [dbo].[tbInvoiceType] ADD CONSTRAINT [FK_tbInvoiceType_tbSetOfBook1] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id])
GO


CREATE TABLE [dbo].[tbPayablePeriod]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[CalendarId] [bigint] NOT NULL,
[Status] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SOBId] [bigint] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbPayablePeriod] ADD CONSTRAINT [PK_tbPayablePeriod] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys

ALTER TABLE [dbo].[tbPayablePeriod] ADD CONSTRAINT [FK_tbPayablePeriod_tbCalendar] FOREIGN KEY ([CalendarId]) REFERENCES [dbo].[tbCalendar] ([Id])
GO
ALTER TABLE [dbo].[tbPayablePeriod] ADD CONSTRAINT [FK_tbPayablePeriod_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id])
GO


CREATE TABLE [dbo].[tbPaymentHeader]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[PaymentNo] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PaymentDate] [datetime] NOT NULL,
[PeriodId] [bigint] NOT NULL,
[VendorId] [bigint] NOT NULL,
[VendorSiteId] [bigint] NOT NULL,
[BankId] [bigint] NOT NULL,
[BankAccountId] [bigint] NOT NULL,
[Amount] [money] NOT NULL,
[Status] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SOBId] [bigint] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbPaymentHeader] ADD CONSTRAINT [PK_tbPaymentHeader] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys

ALTER TABLE [dbo].[tbPaymentHeader] ADD CONSTRAINT [FK_tbPaymentHeader_tbBankAccount1] FOREIGN KEY ([BankAccountId]) REFERENCES [dbo].[tbBankAccount] ([Id])
GO
ALTER TABLE [dbo].[tbPaymentHeader] ADD CONSTRAINT [FK_tbPaymentHeader_tbBank1] FOREIGN KEY ([BankId]) REFERENCES [dbo].[tbBank] ([Id])
GO
ALTER TABLE [dbo].[tbPaymentHeader] ADD CONSTRAINT [FK_tbPaymentHeader_tbCalendar1] FOREIGN KEY ([PeriodId]) REFERENCES [dbo].[tbCalendar] ([Id])
GO
ALTER TABLE [dbo].[tbPaymentHeader] ADD CONSTRAINT [FK_tbPaymentHeader_tbSetOfBook1] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id])
GO
ALTER TABLE [dbo].[tbPaymentHeader] ADD CONSTRAINT [FK_tbPaymentHeader_tbVendor] FOREIGN KEY ([VendorId]) REFERENCES [dbo].[tbVendor] ([Id])
GO
ALTER TABLE [dbo].[tbPaymentHeader] ADD CONSTRAINT [FK_tbPaymentHeader_tbVendorSite] FOREIGN KEY ([VendorSiteId]) REFERENCES [dbo].[tbVendorSite] ([Id])
GO


CREATE TABLE [dbo].[tbPaymentInvoiceLines]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[PaymentId] [bigint] NOT NULL,
[InvoiceId] [bigint] NOT NULL,
[Amount] [money] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbPaymentInvoiceLines] ADD CONSTRAINT [PK_tbPaymentInvoiceLines] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys

ALTER TABLE [dbo].[tbPaymentInvoiceLines] ADD CONSTRAINT [FK_tbPaymentInvoiceLines_tbPaymentHeader1] FOREIGN KEY ([PaymentId]) REFERENCES [dbo].[tbPaymentHeader] ([Id])
GO


CREATE TABLE [dbo].[tbWithholding]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[Code] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CodeCombinitionId] [bigint] NOT NULL,
[VendorId] [bigint] NOT NULL,
[VendorSiteId] [bigint] NOT NULL,
[Rate] [money] NOT NULL,
[DateFrom] [datetime] NOT NULL,
[DateTo] [datetime] NOT NULL,
[SOBId] [bigint] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbWithholding] ADD CONSTRAINT [PK_tbWithholding] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys

ALTER TABLE [dbo].[tbWithholding] ADD CONSTRAINT [FK_tbWithholding_tbSetOfBook1] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id])
GO
ALTER TABLE [dbo].[tbWithholding] ADD CONSTRAINT [FK_tbWithholding_tbVendor] FOREIGN KEY ([VendorId]) REFERENCES [dbo].[tbVendor] ([Id])
GO
ALTER TABLE [dbo].[tbWithholding] ADD CONSTRAINT [FK_tbWithholding_tbVendorSite] FOREIGN KEY ([VendorSiteId]) REFERENCES [dbo].[tbVendorSite] ([Id])
GO

END_SETUP: