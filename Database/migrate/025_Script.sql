BEGIN_SETUP:

CREATE TABLE [dbo].[tbRemittance]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[SOBId] [bigint] NOT NULL,
[BankId] [bigint] NOT NULL,
[BankAccountId] [bigint] NOT NULL,
[ReceiptId] [bigint] NOT NULL,
[RemitNo] [varchar] (30) NOT NULL,
[RemitDate] [datetime] NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)

GO

ALTER TABLE [dbo].[tbRemittance] ADD CONSTRAINT [PK_tbRemittance] PRIMARY KEY CLUSTERED ([Id])
ALTER TABLE [dbo].[tbRemittance] ADD CONSTRAINT [FK_tbRemittance_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE [dbo].[tbRemittance] ADD CONSTRAINT [FK_tbRemittance_tbReceipt] FOREIGN KEY ([ReceiptId]) REFERENCES [dbo].[tbReceipt] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE [dbo].[tbRemittance] ADD CONSTRAINT [FK_tbRemittance_tbBank] FOREIGN KEY ([BankId]) REFERENCES [dbo].[tbBank] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE [dbo].[tbRemittance] ADD CONSTRAINT [FK_tbRemittance_tbBankAccount] FOREIGN KEY ([BankAccountId]) REFERENCES [dbo].[tbBankAccount] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE

GO

UPDATE tbFeature set Href = '~/Remittance' where id = 30

END_SETUP: