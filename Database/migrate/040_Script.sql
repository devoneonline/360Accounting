BEGIN_SETUP:

CREATE TABLE [dbo].[tbUserSetofBook]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[CompanyId] [bigint] NOT NULL,
[SOBId] [bigint] NOT NULL,
[UserId] [uniqueidentifier] NOT NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[tbUserSetofBook] ADD CONSTRAINT [FK_tbUserSetofBook_tbSetOfBook] FOREIGN KEY ([SOBId]) REFERENCES [dbo].[tbSetOfBook] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

END_SETUP: