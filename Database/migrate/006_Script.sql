BEGIN_SETUP:

alter table tbChartOfAccount
	alter column UpdateBy uniqueidentifier null

go
	
alter table tbChartOfAccount
	alter column UpdateDate Datetime null
	
END_SETUP: