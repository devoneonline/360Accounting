BEGIN_SETUP:

ALTER TABLE tbCustomer Add CompanyId bigint not null

GO

ALTER TABLE [dbo].[tbCustomer] ADD CONSTRAINT [FK_tbCustomer_tbCompany] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[tbCompany] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

END_SETUP: