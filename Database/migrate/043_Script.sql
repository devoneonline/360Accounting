BEGIN_SETUP:

Delete from tbInvoiceDetail
Delete from tbInvoice

ALTER TABLE tbInvoice DROP CONSTRAINT [FK_tbInvoice_tbCalendar]
ALTER TABLE tbInvoice ADD CONSTRAINT [FK_tbInvoice_tbReceivablePeriod] FOREIGN KEY ([PeriodId]) REFERENCES [dbo].[tbReceivablePeriod] ([Id])


END_SETUP: