BEGIN_SETUP:

DROP TABLE dbo.tbCalendar

CREATE TABLE [dbo].[tbCalendar]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[CompanyId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[PeriodName] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Adjusting] [bit] NULL,
[PeriodYear] [int] NULL,
[PeriodQuarter] [int] NULL,
[SeqNumber] [int] NULL,
[StartDate] [datetime] NULL,
[EndDate] [datetime] NULL,
[ClosingStatus] [varchar] (30) NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NOT NULL,
[UpdateDate] [datetime] NOT NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbCalendar] ADD CONSTRAINT [PK_tbCalendar] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys

ALTER TABLE [dbo].[tbCalendar] ADD CONSTRAINT [FK_tbCalendar_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO


END_SETUP: