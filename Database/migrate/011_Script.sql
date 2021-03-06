BEGIN_SETUP:

delete from tbCurrency
GO

Alter table tbCurrency
	drop column CreateBy
	
GO
	
Alter table tbCurrency
	drop column CreateDate
	
GO

Alter table tbCurrency
	drop column UpdateBy
	
GO

Alter table tbCurrency
	drop column UpdateDate
	
GO

Alter table tbCurrency
	Add CreateBy uniqueidentifier not null

GO

Alter table tbCurrency
	Add CreateDate DateTime not null

GO
	
Alter table tbCurrency
	Add UpdateBy uniqueidentifier

GO
	
Alter table tbCurrency
	Add UpdateDate DateTime
	
GO

IF Exists(SELECT 1 FROM sys.objects WHERE name='DF_tbFeature_SequenceNo')
BEGIN

	ALTER TABLE tbFeature 
	DROP CONSTRAINT [DF_tbFeature_SequenceNo]
END

GO	

IF Not Exists(SELECT 1 FROM sys.objects WHERE name='FK_tbCodeCombinition_tbSetOfBook')
BEGIN

	ALTER TABLE [dbo].[tbCodeCombinition] ADD CONSTRAINT [FK_tbCodeCombinition_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id])

END
GO

IF Not Exists(SELECT 1 FROM sys.objects WHERE name='FK_tbSetOfBook_tbSetOfBook')
BEGIN

	ALTER TABLE [dbo].[tbSetOfBook] ADD CONSTRAINT [FK_tbSetOfBook_tbSetOfBook] FOREIGN KEY ([Id]) REFERENCES [dbo].[tbSetOfBook] ([Id])
END
GO


	
END_SETUP: