BEGIN_SETUP:

ALTER TABLE tbInvoiceDetail ADD ItemId [bigint] null

ALTER TABLE [dbo].[tbInvoiceDetail] ADD CONSTRAINT [FK_tbInvoiceDetail_tbItem] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[tbItem] ([Id])

END_SETUP: