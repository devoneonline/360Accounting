BEGIN_SETUP:

ALTER TABLE [dbo].[tbPayablePeriod] DROP CONSTRAINT [FK_tbPurchasingPeriod_tbSetOfBook]

ALTER TABLE [dbo].[tbPurchasingPeriod] ADD CONSTRAINT [FK_tbPurchasingPeriod_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id])

END_SETUP: