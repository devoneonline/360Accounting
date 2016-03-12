BEGIN_SETUP:

-- Columns

CREATE TABLE [dbo].[tbBank]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[BankName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Remarks] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SOBId] [bigint] NULL,
[StartDate] [datetime] NOT NULL,
[EndDate] [datetime] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbBank] ADD CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys

ALTER TABLE [dbo].[tbBank] ADD CONSTRAINT [FK_tbBank_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id])
GO

-- Columns

CREATE TABLE [dbo].[tbBankAccount]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[BankId] [bigint] NOT NULL,
[AccountName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AdditionalInformation] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StartDate] [datetime] NOT NULL,
[EndDate] [datetime] NOT NULL,
[Cash_CCID] [bigint] NOT NULL,
[RemitCash_CCID] [bigint] NOT NULL,
[Confirm_CCID] [bigint] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbBankAccount] ADD CONSTRAINT [PK_tbBankAccount] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys

ALTER TABLE [dbo].[tbBankAccount] ADD CONSTRAINT [FK_tbBankAccount_Bank] FOREIGN KEY ([BankId]) REFERENCES [dbo].[tbBank] ([Id])
GO

-- Columns

CREATE TABLE [dbo].[tbReceipt]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[ReceiptDate] [datetime] NOT NULL,
[ReceiptNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ReceiptAmount] [money] NOT NULL,
[CurrencyCode] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ConversionRate] [money] NOT NULL,
[Remarks] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Status] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CustomerId] [bigint] NOT NULL,
[CustomerSiteId] [bigint] NOT NULL,
[BankId] [bigint] NOT NULL,
[BankAccountId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[PeriodId] [bigint] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbReceipt] ADD CONSTRAINT [PK_tbReceipt] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys

ALTER TABLE [dbo].[tbReceipt] ADD CONSTRAINT [FK_tbReceipt_tbBankAccount1] FOREIGN KEY ([BankAccountId]) REFERENCES [dbo].[tbBankAccount] ([Id])
GO
ALTER TABLE [dbo].[tbReceipt] ADD CONSTRAINT [FK_tbReceipt_tbBank1] FOREIGN KEY ([BankId]) REFERENCES [dbo].[tbBank] ([Id])
GO
ALTER TABLE [dbo].[tbReceipt] ADD CONSTRAINT [FK_tbReceipt_tbCustomer1] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[tbCustomer] ([Id])
GO
ALTER TABLE [dbo].[tbReceipt] ADD CONSTRAINT [FK_tbReceipt_tbCustomerSite1] FOREIGN KEY ([CustomerSiteId]) REFERENCES [dbo].[tbCustomerSite] ([Id])
GO
ALTER TABLE [dbo].[tbReceipt] ADD CONSTRAINT [FK_tbReceipt_tbCalendar1] FOREIGN KEY ([PeriodId]) REFERENCES [dbo].[tbCalendar] ([Id])
GO
ALTER TABLE [dbo].[tbReceipt] ADD CONSTRAINT [FK_tbReceipt_tbSetOfBook1] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id])
GO

END_SETUP: