BEGIN_SETUP:
Alter Table tbGLHeader Drop Column CurrencyCode

GO

Alter Table tbGLHeader Add CurrencyId bigint not null

END_SETUP: