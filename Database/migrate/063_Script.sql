BEGIN_SETUP:

ALTER TABLE [dbo].[tbInvoiceDetail] ADD Amount [money]
ALTER TABLE [dbo].[tbInvoiceDetail] ADD TaxAmount [money]

END_SETUP: