BEGIN_SETUP:
/****** Object:  Table [dbo].[tbFeatureSet]    Script Date: 01/19/2016 00:17:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbFeatureSet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tbFeatureSet](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[AccessType] [varchar](10) NOT NULL,
	[CreateBy] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateBy] [uniqueidentifier] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_tbFeatureSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'tbFeatureSet', N'COLUMN',N'AccessType'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'company or user' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbFeatureSet', @level2type=N'COLUMN',@level2name=N'AccessType'
GO
/****** Object:  Table [dbo].[tbFeatureSetList]    Script Date: 01/19/2016 00:17:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbFeatureSetList]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tbFeatureSetList](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FeatureSetId] [bigint] NOT NULL,
	[FeatureId] [bigint] NOT NULL,
 CONSTRAINT [PK_tbFeatureSetList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tbFeatureSetAccess]    Script Date: 01/19/2016 00:17:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbFeatureSetAccess]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tbFeatureSetAccess](
	[Id] [bigint] NOT NULL IDENTITY(1,1),
	[CompanyId] [bigint] NULL,
	[UserId] [uniqueidentifier] NULL,
	[FeatureSetId] [bigint] NOT NULL,
	[CreateBy] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tbFeatureSetAccess] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Default [DF_tbFeatureSet_AccessType]    Script Date: 01/19/2016 00:17:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tbFeatureSet_AccessType]') AND parent_object_id = OBJECT_ID(N'[dbo].[tbFeatureSet]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tbFeatureSet_AccessType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tbFeatureSet] ADD  CONSTRAINT [DF_tbFeatureSet_AccessType]  DEFAULT ('company') FOR [AccessType]
END


End
GO
/****** Object:  ForeignKey [FK_tbFeatureSetAccess_aspnet_Users]    Script Date: 01/19/2016 00:17:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tbFeatureSetAccess_aspnet_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[tbFeatureSetAccess]'))
ALTER TABLE [dbo].[tbFeatureSetAccess]  WITH CHECK ADD  CONSTRAINT [FK_tbFeatureSetAccess_aspnet_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tbFeatureSetAccess_aspnet_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[tbFeatureSetAccess]'))
ALTER TABLE [dbo].[tbFeatureSetAccess] CHECK CONSTRAINT [FK_tbFeatureSetAccess_aspnet_Users]
GO
/****** Object:  ForeignKey [FK_tbFeatureSetAccess_tbCompany]    Script Date: 01/19/2016 00:17:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tbFeatureSetAccess_tbCompany]') AND parent_object_id = OBJECT_ID(N'[dbo].[tbFeatureSetAccess]'))
ALTER TABLE [dbo].[tbFeatureSetAccess]  WITH CHECK ADD  CONSTRAINT [FK_tbFeatureSetAccess_tbCompany] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[tbCompany] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tbFeatureSetAccess_tbCompany]') AND parent_object_id = OBJECT_ID(N'[dbo].[tbFeatureSetAccess]'))
ALTER TABLE [dbo].[tbFeatureSetAccess] CHECK CONSTRAINT [FK_tbFeatureSetAccess_tbCompany]
GO
/****** Object:  ForeignKey [FK_tbFeatureSetAccess_tbFeatureSet]    Script Date: 01/19/2016 00:17:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tbFeatureSetAccess_tbFeatureSet]') AND parent_object_id = OBJECT_ID(N'[dbo].[tbFeatureSetAccess]'))
ALTER TABLE [dbo].[tbFeatureSetAccess]  WITH CHECK ADD  CONSTRAINT [FK_tbFeatureSetAccess_tbFeatureSet] FOREIGN KEY([FeatureSetId])
REFERENCES [dbo].[tbFeatureSet] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tbFeatureSetAccess_tbFeatureSet]') AND parent_object_id = OBJECT_ID(N'[dbo].[tbFeatureSetAccess]'))
ALTER TABLE [dbo].[tbFeatureSetAccess] CHECK CONSTRAINT [FK_tbFeatureSetAccess_tbFeatureSet]
GO
/****** Object:  ForeignKey [FK_tbFeatureSetList_tbFeature]    Script Date: 01/19/2016 00:17:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tbFeatureSetList_tbFeature]') AND parent_object_id = OBJECT_ID(N'[dbo].[tbFeatureSetList]'))
ALTER TABLE [dbo].[tbFeatureSetList]  WITH CHECK ADD  CONSTRAINT [FK_tbFeatureSetList_tbFeature] FOREIGN KEY([FeatureId])
REFERENCES [dbo].[tbFeature] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tbFeatureSetList_tbFeature]') AND parent_object_id = OBJECT_ID(N'[dbo].[tbFeatureSetList]'))
ALTER TABLE [dbo].[tbFeatureSetList] CHECK CONSTRAINT [FK_tbFeatureSetList_tbFeature]
GO
/****** Object:  ForeignKey [FK_tbFeatureSetList_tbFeatureSet]    Script Date: 01/19/2016 00:17:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tbFeatureSetList_tbFeatureSet]') AND parent_object_id = OBJECT_ID(N'[dbo].[tbFeatureSetList]'))
ALTER TABLE [dbo].[tbFeatureSetList]  WITH CHECK ADD  CONSTRAINT [FK_tbFeatureSetList_tbFeatureSet] FOREIGN KEY([FeatureSetId])
REFERENCES [dbo].[tbFeatureSet] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tbFeatureSetList_tbFeatureSet]') AND parent_object_id = OBJECT_ID(N'[dbo].[tbFeatureSetList]'))
ALTER TABLE [dbo].[tbFeatureSetList] CHECK CONSTRAINT [FK_tbFeatureSetList_tbFeatureSet]
GO

END_SETUP: