BEGIN_SETUP:

ALTER TABLE [dbo].[tbCustomerSite] ADD CONSTRAINT [FK_tbCustomerSite_tbTax] FOREIGN KEY ([TaxCodeId]) REFERENCES [dbo].[tbTax] ([Id]) 

GO

ALTER TABLE [dbo].[tbVendorSite] ADD CONSTRAINT [FK_tbVendorSite_tbTax] FOREIGN KEY ([TaxCodeId]) REFERENCES [dbo].[tbTax] ([Id]) 

GO

update tbFeature set Href = '~/Tax' where id = 31

END_SETUP: