--USE [master]

--IF EXISTS (SELECT * FROM sysdatabases WHERE name='BusConductor') 
--BEGIN 
--	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'BusConductor'
--	ALTER DATABASE [BusConductor] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
--	DROP DATABASE [BusConductor]
--END
--GO

--CREATE DATABASE [BusConductor] 
--GO

USE [BusConductor]

--IF EXISTS(SELECT name FROM [master].[dbo].syslogins WHERE name = 'intranetuser')
--BEGIN
--	ALTER LOGIN [intranetuser] ENABLE
--END
--GO

--IF NOT EXISTS (SELECT * FROM sys.sysusers WHERE name = N'intranetuser')
--BEGIN
--	CREATE USER [intranetuser] FOR LOGIN [intranetuser] WITH DEFAULT_SCHEMA=[dbo]	
--END
--GO

--IF DATABASE_PRINCIPAL_ID('AllowSelectInsertUpdate') IS NULL
--BEGIN
--	CREATE ROLE [AllowSelectInsertUpdate] 	
--END
--EXEC sp_addrolemember 'AllowSelectInsertUpdate', 'intranetuser'
--GO
--SELECT * FROM Enquiry

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Role')
BEGIN
	DROP TABLE [dbo].[Role]
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Permission')
BEGIN
	DROP TABLE [dbo].[Permission]
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'PermissionRole')
BEGIN
	DROP TABLE [dbo].[PermissionRole]
END 
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'User')
BEGIN
	DROP TABLE [dbo].[User]
END 
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'TaskType')
BEGIN
	DROP TABLE [dbo].[TaskType]
END 
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Task')
BEGIN
	DROP TABLE [dbo].[Task]
END 
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Customer')
BEGIN
	DROP TABLE [dbo].[Customer]
END 
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Bus')
BEGIN
	DROP TABLE [dbo].[Bus]
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'PricingPeriod')
BEGIN
	DROP TABLE [dbo].[PricingPeriod]
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Booking')
BEGIN
	DROP TABLE [dbo].[Booking]
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Enquiry')
BEGIN
	DROP TABLE [dbo].[Enquiry]
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Voucher')
BEGIN
	DROP TABLE [dbo].[Voucher]
END 
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'LogEvent')
BEGIN
	DROP TABLE [dbo].[LogEvent]
END 
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Role')
BEGIN
	CREATE TABLE [dbo].[Role](
		[Id] [uniqueidentifier] NOT NULL,
		[RoleName] [nvarchar](20) NULL,
		[Description] [nvarchar](50) NULL,
		[CreatedById] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedById] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	--GRANT SELECT, INSERT, UPDATE ON [Role] TO [AllowSelectInsertUpdate]
	
	DECLARE @now AS DATETIME
	SET @now = GETDATE()

	INSERT INTO [Role] ([Id], [RoleName], [Description], [CreatedById], [CreatedOn], [LastModifiedById], [LastModifiedOn], [Deleted]) VALUES ('80fc2a10-d07e-4e06-9b91-4ba936e335ba', 'Guest', 'Guest', '188403fb-3c5e-45a3-aa39-5908e86ea372', @now, null, null, 0)
	INSERT INTO [Role] ([Id], [RoleName], [Description], [CreatedById], [CreatedOn], [LastModifiedById], [LastModifiedOn], [Deleted]) VALUES ('8dc59a62-a077-41cc-bac7-f8be505ae4a8', 'Admin', 'Admin User', '188403fb-3c5e-45a3-aa39-5908e86ea372', @now, null, null, 0)
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Permission')
BEGIN
	CREATE TABLE [dbo].[Permission](
		[Id] [uniqueidentifier] NOT NULL,
		[Description] [nvarchar](50) NULL,
		[CreatedById] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedById] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	--GRANT SELECT, INSERT, UPDATE ON [Permission] TO [AllowSelectInsertUpdate]
	
	DECLARE @now AS DATETIME
	SET @now = GETDATE()
	
	INSERT INTO [Permission] ([Id], [Description], [CreatedById], [CreatedOn], [LastModifiedById], [LastModifiedOn], [Deleted]) VALUES ('f76e6b28-993f-410b-82b1-d1ce2baf34a6', 'Complete another user''s task', '188403fb-3c5e-45a3-aa39-5908e86ea372', @now, null, null, 0)
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'PermissionRole')
BEGIN
	CREATE TABLE [dbo].[PermissionRole](
		[Id] [uniqueidentifier] NOT NULL,
		[PermissionId] [uniqueidentifier] NULL,
		[RoleId] [uniqueidentifier] NULL,
		[CreatedById] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedById] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_PermissionRole] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	--GRANT SELECT, INSERT, UPDATE ON [PermissionRole] TO [AllowSelectInsertUpdate]
	
	DECLARE @now AS DATETIME
	SET @now = GETDATE()

	INSERT INTO [PermissionRole] ([Id], [PermissionId], [RoleId], [CreatedById], [CreatedOn], [LastModifiedById], [LastModifiedOn], [Deleted]) VALUES ('8dc59a62-a077-41cc-bac7-f8be505ae4a8', 'f76e6b28-993f-410b-82b1-d1ce2baf34a6', '80fc2a10-d07e-4e06-9b91-4ba936e335ba', '188403fb-3c5e-45a3-aa39-5908e86ea372', @now, null, null, 0)
END 
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'User')
BEGIN
	CREATE TABLE [dbo].[User](
		[Id] [uniqueidentifier] NOT NULL,
		[Username] [nvarchar](50) NULL,
		[RoleId] [uniqueidentifier] NULL,
		[Forename] [nvarchar](50) NULL,
		[Surname] [nvarchar](50) NULL,
		[AddressLine1] [nvarchar](50) NULL,
		[AddressLine2] [nvarchar](50) NULL,
		[AddressLine3] [nvarchar](50) NULL,
		[Town] [nvarchar](50) NULL,
		[County] [nvarchar](50) NULL,
		[PostCode] [nvarchar](10) NULL,
		[Email] [nvarchar](50) NULL,
		[TelephoneNumber] [nvarchar](50) NULL,
		[CreatedById] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedById] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	--GRANT SELECT, INSERT, UPDATE ON [User] TO [AllowSelectInsertUpdate]
	
	DECLARE @now AS DATETIME
	SET @now = GETDATE()

	INSERT INTO [User] ([Id], [Username], [RoleId], [CreatedById], [CreatedOn], [LastModifiedById], [LastModifiedOn], [Deleted]) VALUES ('188403fb-3c5e-45a3-aa39-5908e86ea372', 'Sql Initialise', '8dc59a62-a077-41cc-bac7-f8be505ae4a8', '188403fb-3c5e-45a3-aa39-5908e86ea372', @now, null, null, 0)
	INSERT INTO [User] ([Id], [Username], [RoleId], [CreatedById], [CreatedOn], [LastModifiedById], [LastModifiedOn], [Deleted]) VALUES ('c8238876-47fc-42af-8a32-926061097f1c', 'Application', '8dc59a62-a077-41cc-bac7-f8be505ae4a8', '188403fb-3c5e-45a3-aa39-5908e86ea372', @now, null, null, 0)
	INSERT INTO [User] ([Id], [Username], [RoleId], [CreatedById], [CreatedOn], [LastModifiedById], [LastModifiedOn], [Deleted]) VALUES ('3b50e7c8-c6ce-4446-9d51-6cc7a7877343', 'A. User', '8dc59a62-a077-41cc-bac7-f8be505ae4a8', '188403fb-3c5e-45a3-aa39-5908e86ea372', @now, null, null, 0)
END 
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'TaskType')
BEGIN
CREATE TABLE [dbo].[TaskType](
		[Id] [uniqueidentifier] NOT NULL,
		[Description] [nvarchar](1000) NULL,
		[ServiceLevelAgreementMinutes] [int] NOT NULL,
		[WarningWindowMinutes] [int] NOT NULL,
		[CreatedById] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedById] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_TaskType] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	--GRANT SELECT, INSERT, UPDATE ON [TaskType] TO [AllowSelectInsertUpdate]
	
	DECLARE @now AS DATETIME
	SET @now = GETDATE()

	INSERT INTO [TaskType] ([Id], [Description], [ServiceLevelAgreementMinutes], [WarningWindowMinutes], [CreatedById], [CreatedOn], [LastModifiedById], [LastModifiedOn], [Deleted]) VALUES ('911762e0-31ba-4c6c-83f8-3f2288904975', 'Standard Task', 120, 20, '188403fb-3c5e-45a3-aa39-5908e86ea372', @now, null, null, 0)
	INSERT INTO [TaskType] ([Id], [Description], [ServiceLevelAgreementMinutes], [WarningWindowMinutes], [CreatedById], [CreatedOn], [LastModifiedById], [LastModifiedOn], [Deleted]) VALUES ('a50c62cd-b24a-4d0a-aada-9744fce7022c', 'Urgent Task', 60, 10, '188403fb-3c5e-45a3-aa39-5908e86ea372', @now, null, null, 0)
END 
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Task')
BEGIN
CREATE TABLE [dbo].[Task](
		[Id] [uniqueidentifier] NOT NULL,
		[Description] [nvarchar](1000) NULL,
		[AssignedToId] [uniqueidentifier] NOT NULL,
		[TypeId] [uniqueidentifier] NOT NULL,
		[DueDate] [datetime] NULL,
		[Status] [int] NULL,
		[CompletedOn] [datetime] NULL,
		[CompletedMessage] [datetime] NULL,
		[CreatedById] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedById] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	--GRANT SELECT, INSERT, UPDATE ON [Task] TO [AllowSelectInsertUpdate]
END 
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Customer')
BEGIN
	CREATE TABLE [dbo].[Customer](
		[Id] [uniqueidentifier] NOT NULL,
		[Forename] [nvarchar](50) NULL,
		[Surname] [nvarchar](50) NULL,
		[AddressLine1] [nvarchar](50) NULL,
		[AddressLine2] [nvarchar](50) NULL,
		[AddressLine3] [nvarchar](50) NULL,
		[Town] [nvarchar](50) NULL,
		[County] [nvarchar](50) NULL,
		[PostCode] [nvarchar](10) NULL,
		[Email] [nvarchar](50) NULL,
		[TelephoneNumber] [nvarchar](50) NULL,
		[CreatedById] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedById] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	--GRANT SELECT, INSERT, UPDATE ON [Customer] TO [AllowSelectInsertUpdate]
	
	DECLARE @now AS DATETIME
	SET @now = GETDATE()
END 
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Bus')
BEGIN
CREATE TABLE [dbo].[Bus](
		[Id] [uniqueidentifier] NOT NULL,
		[Name] [nvarchar](25) NULL,
		[Description] [nvarchar](50) NULL,
		[Overview] [nvarchar](1000) NULL,
		[Details] [nvarchar](MAX) NULL,
		[DriveSide] [int] NULL,
		[Berth] [int] NULL,
		[Year] [int] NULL,
		[CreatedById] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedById] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_Bus] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	--GRANT SELECT, INSERT, UPDATE ON [Bus] TO [AllowSelectInsertUpdate]
	
	DECLARE @now AS DATETIME
	SET @now = GETDATE()
	INSERT INTO [Bus] ([Id], [Name], [Description], [Overview], [Details], [DriveSide], [Berth], [Year], [CreatedById], [CreatedOn], [LastModifiedById], [LastModifiedOn], [Deleted]) VALUES ('6a9857a6-d0b0-4e1a-84cb-ee9ade159560', 'Thurston', '1969 Volkswagen T2 Late Bay', 'Thurston is a two berth early bay tintop with a Westfalia interior. He''s alittle rough around the edges, but generally in good, original condition.', 'Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi.', 1, 2, 1969, '188403fb-3c5e-45a3-aa39-5908e86ea372', @now, null, null, 0)
	INSERT INTO [Bus] ([Id], [Name], [Description], [Overview], [Details], [DriveSide], [Berth], [Year], [CreatedById], [CreatedOn], [LastModifiedById], [LastModifiedOn], [Deleted]) VALUES ('ba325fad-9a65-4732-872c-da2069bb37e8', 'Franklin', '1965 Volkswagen T1 Splitscreen', 'Franklin is currently in a sorry state, but give him time and he will be awesome!', 'Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi.', 1, 2, 1969, '188403fb-3c5e-45a3-aa39-5908e86ea372', @now, null, null, 0)
END
GO


IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'PricingPeriod')
BEGIN
CREATE TABLE [dbo].[PricingPeriod](
		[Id] [uniqueidentifier] NOT NULL,
		[StartMonth] [int] NULL,
		[StartDay] [int] NULL,
		[EndMonth] [int] NULL,
		[EndDay] [int] NULL,
		[FridayToFridayRate] [money] NULL,
		[FridayToMondayRate] [money] NULL,
		[MondayToFridayRate] [money] NULL,
		[BusId] [uniqueidentifier] NULL,
		[CreatedById] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedById] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_PricingPeriod] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	--GRANT SELECT, INSERT, UPDATE ON [PricingPeriod] TO [AllowSelectInsertUpdate]
	
	DECLARE @now AS DATETIME
	SET @now = GETDATE()
	INSERT INTO [PricingPeriod] ([Id], [StartMonth], [StartDay], [EndMonth], [EndDay], [FridayToFridayRate], [FridayToMondayRate], [MondayToFridayRate], [BusId], [CreatedById], [CreatedOn], [LastModifiedById], [LastModifiedOn], [Deleted]) VALUES ('5AC9F707-DC38-4DED-A9BE-40EB2BA63DED', 10, 1, 3, 31, 300, 300, 600, '6a9857a6-d0b0-4e1a-84cb-ee9ade159560', '188403fb-3c5e-45a3-aa39-5908e86ea372', @now, null, null, 0)
	INSERT INTO [PricingPeriod] ([Id], [StartMonth], [StartDay], [EndMonth], [EndDay], [FridayToFridayRate], [FridayToMondayRate], [MondayToFridayRate], [BusId], [CreatedById], [CreatedOn], [LastModifiedById], [LastModifiedOn], [Deleted]) VALUES ('57D27133-5F6C-450A-8367-77009E9936F6', 4, 1, 9, 30, 400, 400, 800, '6a9857a6-d0b0-4e1a-84cb-ee9ade159560', '188403fb-3c5e-45a3-aa39-5908e86ea372', @now, null, null, 0)
	INSERT INTO [PricingPeriod] ([Id], [StartMonth], [StartDay], [EndMonth], [EndDay], [FridayToFridayRate], [FridayToMondayRate], [MondayToFridayRate], [BusId], [CreatedById], [CreatedOn], [LastModifiedById], [LastModifiedOn], [Deleted]) VALUES ('d4d0b835-e076-4eee-86bb-dcd66771a58d', 10, 1, 3, 31, 350, 350, 700, 'ba325fad-9a65-4732-872c-da2069bb37e8', '188403fb-3c5e-45a3-aa39-5908e86ea372', @now, null, null, 0)
	INSERT INTO [PricingPeriod] ([Id], [StartMonth], [StartDay], [EndMonth], [EndDay], [FridayToFridayRate], [FridayToMondayRate], [MondayToFridayRate], [BusId], [CreatedById], [CreatedOn], [LastModifiedById], [LastModifiedOn], [Deleted]) VALUES ('41bf3d3a-bd9b-4d89-b7df-149690c0d89a', 4, 1, 9, 30, 450, 450, 900, 'ba325fad-9a65-4732-872c-da2069bb37e8', '188403fb-3c5e-45a3-aa39-5908e86ea372', @now, null, null, 0)
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Booking')
BEGIN
CREATE TABLE [dbo].[Booking](
		[Id] [uniqueidentifier] NOT NULL,
		[BookingNumber] [nvarchar](25) NOT NULL,
		[CustomerId] [uniqueidentifier] NULL,
		[PickUp] [datetime] NULL,
		[DropOff] [datetime] NULL,
		[NumberOfAdults] [int] NULL,
		[NumberOfChildren] [int] NULL,
		[IsMainDriver] [bit] NULL,
		[DrivingLicenceNumber] [nvarchar](30) NULL,
		[DriverForename] [nvarchar](50) NULL,
		[DriverSurname] [nvarchar](50) NULL,
		[VoucherId] [uniqueidentifier] NULL,
		[Status] [int] NULL,
		[BusId] [uniqueidentifier] NULL,
		[TotalCost] [money] NOT NULL,
		[CreatedById] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedById] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	--GRANT SELECT, INSERT, UPDATE ON [Booking] TO [AllowSelectInsertUpdate]
	
	--INSERT INTO [Booking] ([Id], [BookingNumber], [PickUp], [DropOff], [NumberOfAdults], [NumberOfChildren], [IsMainDriver], [DrivingLicenceNumber], [VoucherId],
	--	[Status], [BusId], [TotalCost], [CreatedById], [CreatedOn], [LastModifiedById], [LastModifiedOn], [Deleted])
	--VALUES ('eaa01eab-f3bd-4e24-8368-d3501a227a8b', '201311160001_Grey', '2013-11-18 00:00:00', '2013-11-22 00:00:00',
	--	2, 0, 1, 'ABD0000', NULL, 1, 'ba325fad-9a65-4732-872c-da2069bb37e8', 100, 'c8238876-47fc-42af-8a32-926061097f1c', 
	--	'2013-11-16 00:00:00', NULL, NULL, 0)
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Enquiry')
BEGIN
CREATE TABLE [dbo].[Enquiry](
		[Id] [uniqueidentifier] NOT NULL,
		[Forename] [nvarchar](50) NULL,
		[Surname] [nvarchar](50) NULL,
		[AddressLine1] [nvarchar](50) NULL,
		[AddressLine2] [nvarchar](50) NULL,
		[AddressLine3] [nvarchar](50) NULL,
		[Town] [nvarchar](50) NULL,
		[County] [nvarchar](50) NULL,
		[PostCode] [nvarchar](10) NULL,
		[Email] [nvarchar](50) NULL,
		[TelephoneNumber] [nvarchar](50) NULL,
		[PickUp] [datetime] NULL,
		[DropOff] [datetime] NULL,
		[NumberOfAdults] [int] NULL,
		[NumberOfChildren] [int] NULL,
		[IsMainDriver] [bit] NULL,
		[DrivingLicenceNumber] [nvarchar](30) NULL,
		[DriverForename] [nvarchar](50) NULL,
		[DriverSurname] [nvarchar](50) NULL,
		[BusId] [uniqueidentifier] NULL,
		[ResultingBookingId] [uniqueidentifier] NULL,
		[Status] [int] NULL,
		[CreatedById] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedById] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_Enquiry] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	--GRANT SELECT, INSERT, UPDATE ON [Booking] TO [AllowSelectInsertUpdate]
	
	--INSERT INTO [Booking] ([Id], [BookingNumber], [PickUp], [DropOff], [NumberOfAdults], [NumberOfChildren], [IsMainDriver], [DrivingLicenceNumber], [VoucherId],
	--	[Status], [BusId], [TotalCost], [CreatedById], [CreatedOn], [LastModifiedById], [LastModifiedOn], [Deleted])
	--VALUES ('eaa01eab-f3bd-4e24-8368-d3501a227a8b', '201311160001_Grey', '2013-11-18 00:00:00', '2013-11-22 00:00:00',
	--	2, 0, 1, 'ABD0000', NULL, 1, 'ba325fad-9a65-4732-872c-da2069bb37e8', 100, 'c8238876-47fc-42af-8a32-926061097f1c', 
	--	'2013-11-16 00:00:00', NULL, NULL, 0)
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Voucher')
BEGIN
CREATE TABLE [dbo].[Voucher](
		[Id] [uniqueidentifier] NOT NULL,
		[Code] [nvarchar](20) NOT NULL,
		[Description] [nvarchar](1000) NULL,
		[Discount] [money] NOT NULL,
		[ExpiryDate] [datetime] NULL,
		[CreatedById] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedById] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	--GRANT SELECT, INSERT, UPDATE ON [Voucher] TO [AllowSelectInsertUpdate]
END 
GO

--IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
--	WHERE TABLE_NAME = 'Log')
--BEGIN
--	CREATE TABLE [dbo].[Log] (
--		[Id] [bigint] IDENTITY (1, 1) NOT NULL,
--		[Date] [datetime] NOT NULL,
--		[Thread] [varchar] (255) NOT NULL,
--		[Level] [varchar] (50) NOT NULL,
--		[Logger] [varchar] (255) NOT NULL,
--		[Message] [text] NOT NULL,
--		[Exception] [text] NULL
--	)

--	GRANT SELECT, INSERT, UPDATE ON [Log] TO [AllowSelectInsertUpdate]
--END
--GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'LogEvent')
BEGIN
	CREATE TABLE [dbo].[LogEvent](
		[LogEventId] [bigint] IDENTITY(1,1) NOT NULL,
		[Date] [datetime] NOT NULL,
		[Level] [int] NOT NULL,
		[Message] [varchar](100) COLLATE Latin1_General_CI_AS NULL,
		[User] [varchar](50) COLLATE Latin1_General_CI_AS NULL,
		[Exception] [text] COLLATE Latin1_General_CI_AS NULL,
		[Objects] [xml] NULL,
		[ExecutingMachine] [varchar](50) COLLATE Latin1_General_CI_AS NULL,
		[CallingAssembly] [varchar](50) COLLATE Latin1_General_CI_AS NULL,
		[CallingClass] [varchar](50) COLLATE Latin1_General_CI_AS NULL,
		[CallingMethod] [varchar](50) COLLATE Latin1_General_CI_AS NULL,
		[ContextGuid] [varchar](50) COLLATE Latin1_General_CI_AS NULL,
	 CONSTRAINT [PK_SyncLogEvent] PRIMARY KEY CLUSTERED 
	(
		[LogEventId] ASC
	)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	
	--GRANT SELECT, INSERT, UPDATE ON [LogEvent] TO [AllowSelectInsertUpdate]
END 
GO