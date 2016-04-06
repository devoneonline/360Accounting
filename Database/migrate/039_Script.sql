BEGIN_SETUP:
ALTER TABLE [dbo].[tbVendorSite] DROP CONSTRAINT [FK_tbVendorSite_tbTax]
GO
ALTER TABLE [dbo].[tbVendorSite] DROP Column [TaxCodeId]
GO
END_SETUP: