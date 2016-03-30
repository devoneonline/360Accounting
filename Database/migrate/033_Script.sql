BEGIN_SETUP:

ALTER TABLE [dbo].[tbLocatorWarehouse] DROP CONSTRAINT [FK_tbLocatorWarehouse_tbLocator]

ALTER TABLE [dbo].[tbLocatorWarehouse]  WITH CHECK ADD  CONSTRAINT [FK_tbLocatorWarehouse_tbLocator] FOREIGN KEY([LocatorId])
REFERENCES [dbo].[tbLocator] ([Id])

END_SETUP: