BEGIN_SETUP:

alter table tbBank alter column startdate [datetime] null
alter table tbBank alter column enddate [datetime] null
alter table tbReceipt drop column CurrencyCode

END_SETUP: