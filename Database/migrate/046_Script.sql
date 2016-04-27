BEGIN_SETUP:

ALTER TABLE tbVendor Add SOBId [bigint] Not Null

ALTER TABLE tbVendor ADD CONSTRAINT [FK_tbVendor_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id])

END_SETUP: