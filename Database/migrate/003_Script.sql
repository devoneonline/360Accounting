BEGIN_SETUP:
/****** Object:  Table [dbo].[aspnet_WebEvent_Events]    Script Date: 01/18/2016 09:26:43 ******/
/****** Object:  Table [dbo].[aspnet_SchemaVersions]    Script Date: 01/18/2016 09:26:43 ******/
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'common', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'health monitoring', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'membership', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'personalization', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'profile', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'role manager', N'1', 1)
/****** Object:  Table [dbo].[aspnet_Applications]    Script Date: 01/18/2016 09:26:43 ******/
INSERT [dbo].[aspnet_Applications] ([ApplicationName], [LoweredApplicationName], [ApplicationId], [Description]) VALUES (N'/', N'/', N'4284a031-4775-48a3-847c-6587a507d80b', NULL)
/****** Object:  Table [dbo].[aspnet_Paths]    Script Date: 01/18/2016 09:26:43 ******/
/****** Object:  Table [dbo].[aspnet_Users]    Script Date: 01/18/2016 09:26:43 ******/
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4284a031-4775-48a3-847c-6587a507d80b', N'715c35ae-dd65-4b5d-acb9-4572164a476c', N'softone', N'softone', NULL, 0, CAST(0x0000A59000E3E7E4 AS DateTime))
/****** Object:  Table [dbo].[aspnet_Roles]    Script Date: 01/18/2016 09:26:43 ******/
INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'4284a031-4775-48a3-847c-6587a507d80b', N'600e5cc5-b9e7-4852-997b-e7db12676ba7', N'SuperAdmin', N'superadmin', N'Default Role')
INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'4284a031-4775-48a3-847c-6587a507d80b', N'3cd5a451-f757-4944-bbde-89685954d5e1', N'User', N'user', NULL)
/****** Object:  Table [dbo].[aspnet_UsersInRoles]    Script Date: 01/18/2016 09:26:43 ******/
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'715c35ae-dd65-4b5d-acb9-4572164a476c', N'600e5cc5-b9e7-4852-997b-e7db12676ba7')
/****** Object:  Table [dbo].[aspnet_Profile]    Script Date: 01/18/2016 09:26:43 ******/
INSERT [dbo].[aspnet_Profile] ([UserId], [PropertyNames], [PropertyValuesString], [PropertyValuesBinary], [LastUpdatedDate]) VALUES (N'715c35ae-dd65-4b5d-acb9-4572164a476c', N'PhoneNumber:S:0:11:FirstName:S:11:6:LastName:S:17:7:', N'03332111353FaisalKhanani', 0x, CAST(0x0000A59000E3DF26 AS DateTime))
/****** Object:  Table [dbo].[aspnet_PersonalizationPerUser]    Script Date: 01/18/2016 09:26:43 ******/
/****** Object:  Table [dbo].[aspnet_PersonalizationAllUsers]    Script Date: 01/18/2016 09:26:43 ******/
/****** Object:  Table [dbo].[aspnet_Membership]    Script Date: 01/18/2016 09:26:43 ******/
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4284a031-4775-48a3-847c-6587a507d80b', N'715c35ae-dd65-4b5d-acb9-4572164a476c', N'ErKJkFDhmWKaShB5XMk0n70quFo=', 1, N'6QuZAdQCz9Ccd+okR8834w==', NULL, N'faisal.khanani.75@gmail.com', N'faisal.khanani.75@gmail.com', N'What is the name of your first school?', N'p3U6PJSVyaNjeAxdJSWTAphg0oA=', 1, 0, CAST(0x0000A58C012D8894 AS DateTime), CAST(0x0000A59000E3E7E4 AS DateTime), CAST(0x0000A59000E3DF29 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)

END_SETUP: