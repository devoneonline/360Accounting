BEGIN_SETUP:

ALTER TABLE tbBankAccount ALTER COLUMN StartDate [datetime] null
ALTER TABLE tbBankAccount ALTER COLUMN EndDate [datetime] null

ALTER TABLE tbInvoiceSource ALTER COLUMN StartDate [datetime] null
ALTER TABLE tbInvoiceSource ALTER COLUMN EndDate [datetime] null

END_SETUP: