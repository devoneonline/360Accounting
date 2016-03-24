BEGIN_SETUP:

Alter table tbBank Add CompanyId bigint not null default 1

GO

Alter table tbBankAccount Add CompanyId bigint not null default 1

GO

Alter table tbChartOfAccountValues Add CompanyId bigint not null default 1

GO

Alter table tbInvoiceType Add CompanyId bigint not null default 1

GO

Alter table tbPayableInvoice Add CompanyId bigint not null default 1

GO

Alter table tbPayablePeriod Add CompanyId bigint not null default 1

GO

Alter table tbPaymentHeader Add CompanyId bigint not null default 1

GO

Alter table tbReceipt Add CompanyId bigint not null default 1

GO

Alter table tbRemittance Add CompanyId bigint not null default 1

GO

Alter table tbTax Add CompanyId bigint not null default 1

GO

Alter table tbWithholding Add CompanyId bigint not null default 1

GO

END_SETUP: