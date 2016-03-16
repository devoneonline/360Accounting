BEGIN_SETUP:

--Need to put casecade instead of no action
ALTER TABLE [dbo].[tbCustomerSite] ADD CONSTRAINT [FK_tbCustomerSite_tbTax] FOREIGN KEY ([TaxCodeId]) REFERENCES [dbo].[tbTax] ([Id]) ON DELETE No Action ON UPDATE No Action

GO

--Need to put casecade instead of no action
ALTER TABLE [dbo].[tbVendorSite] ADD CONSTRAINT [FK_tbVendorSite_tbTax] FOREIGN KEY ([TaxCodeId]) REFERENCES [dbo].[tbTax] ([Id]) ON DELETE No Action ON UPDATE No Action

GO

update tbFeature set Href = '~/Tax' where id = 31

END_SETUP: