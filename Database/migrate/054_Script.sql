BEGIN_SETUP:

Delete from tbShipment

ALTER TABLE tbShipment ADD DeliveryNo varchar(30) not null

ALTER TABLE tbShipment DROP COLUMN LotNo
ALTER TABLE tbShipment Add LotNoId bigint null
ALTER TABLE [dbo].[tbShipment] ADD CONSTRAINT [FK_tbShipment_tbLotNumber] FOREIGN KEY ([LotNoId]) REFERENCES [dbo].[tbLotNumber] ([Id])

ALTER TABLE tbShipment ALTER COLUMN SerialNo varchar(30) null

END_SETUP: