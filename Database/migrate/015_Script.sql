BEGIN_SETUP:

--DROP TABLE dbo.tbCustomer

CREATE TABLE [dbo].[tbCustomer]
(
[Id] [bigint] NOT NULL IDENTITY(1,1),
[CustomerName] [varchar] (30) NOT NULL,
[Address] [varchar] (255) NULL,
[ContactNo] [varchar] (15) NULL,
[StartDate] [datetime] NULL,
[EndDate] [datetime] NULL,
[CreateBy] [uniqueidentifier] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UpdateBy] [uniqueidentifier] NOT NULL,
[UpdateDate] [datetime] NOT NULL
)

GO

-- Constraints and Indexes

ALTER TABLE [dbo].tbCustomer ADD CONSTRAINT [PK_tbCustomer] PRIMARY KEY CLUSTERED ([Id])

GO

END_SETUP: