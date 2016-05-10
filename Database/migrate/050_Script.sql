BEGIN_SETUP:

DELETE FROM tbSerialNumber

ALTER TABLE tbSerialNumber DROP COLUMN LotNo

ALTER TABLE tbSerialNumber ADD LotNoId [bigint] not null

ALTER TABLE [dbo].[tbSerialNumber] ADD CONSTRAINT [FK_tbSerialNumber_tbLotNumber] FOREIGN KEY ([LotNoId]) REFERENCES [dbo].[tbLotNumber] ([Id])

END_SETUP: