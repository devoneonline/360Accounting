BEGIN_SETUP:

Alter Table tbFeatureSet Add CompanyId bigint not null default(1), ParentId bigint null

END_SETUP:
