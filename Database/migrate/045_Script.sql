BEGIN_SETUP:

ALTER TABLE tbCustomer Add SOBId [bigint] Not Null

ALTER TABLE tbCustomer ADD CONSTRAINT [FK_tbCustomer_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id])

END_SETUP: