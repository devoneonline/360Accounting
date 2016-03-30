BEGIN_SETUP:

CREATE TABLE [dbo].[tbReceivablePeriod]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[CompanyId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[CalendarId] [bigint] NOT NULL,
[Status] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NULL,
[UpdateDate] [datetime] NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbReceivablePeriod] ADD CONSTRAINT [PK_tbReceivablePeriod] PRIMARY KEY CLUSTERED  ([Id])
GO
-- Foreign Keys
ALTER TABLE [dbo].[tbReceivablePeriod] ADD CONSTRAINT [FK_tbReceivablePeriod_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[tbReceivablePeriod] ADD CONSTRAINT [FK_tbReceivablePeriod_tbCalendar] FOREIGN KEY ([CalendarId]) REFERENCES [dbo].[tbCalendar] ([Id])
GO

update tbFeature set Href = '~/ReceivablePeriod' where id = 32

END_SETUP: