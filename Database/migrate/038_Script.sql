BEGIN_SETUP:
ALTER TABLE [dbo].[tbItemWarehouse] DROP CONSTRAINT [FK_tbItemWarehouse_tbSetOfBook]
GO
ALTER TABLE [dbo].[tbItemWarehouse] DROP CONSTRAINT [FK_tbItemWarehouse_tbItem]
GO
ALTER TABLE [dbo].[tbItemWarehouse] ADD CONSTRAINT [FK_tbItemWarehouse_tbItem] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[tbItem] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

END_SETUP: