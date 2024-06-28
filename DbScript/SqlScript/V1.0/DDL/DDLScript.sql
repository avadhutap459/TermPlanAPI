IF (Not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'mstSecurityMech'))
BEGIN
	CREATE TABLE [dbo].[mstSecurityMech](
		[SecurityID] [int] IDENTITY(1,1) NOT NULL,
		[SecurityName] [varchar](50) NULL,
		[Type] [varchar](20) NULL,
		[IsActive] [bit] NULL,
		[createdBy] [varchar](50) NULL,
		[CreatedAt] [datetime] NULL,
		[LastModifiedBy] [nvarchar](50) NULL,
		[LastModifiedAt] [datetime] NULL,
	PRIMARY KEY CLUSTERED 
	(
		[SecurityID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

End

GO
IF (Not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'mstSource'))
BEGIN
	CREATE TABLE [dbo].[mstSource](
		[SourceId] [int] IDENTITY(1,1) NOT NULL,
		[SourceName] [varchar](50) NULL,
		[IsActive] [bit] NULL,
		[createdBy] [varchar](50) NULL,
		[CreatedAt] [datetime] NULL,
		[LastModifiedBy] [nvarchar](50) NULL,
		[LastModifiedAt] [datetime] NULL,
	PRIMARY KEY CLUSTERED 
	(
		[SourceId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

END
GO

IF (Not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'mstProduct'))
BEGIN
	CREATE TABLE [dbo].[mstProduct](
		[ProductId] [int] IDENTITY(1,1) NOT NULL,
		[productName] [int] NULL,
		[ProductCode] [int] NULL,
		[createdBy] [varchar](50) NULL,
		[CreatedAt] [datetime] NULL,
		[LastModifiedBy] [nvarchar](50) NULL,
		[LastModifiedAt] [datetime] NULL,
	PRIMARY KEY CLUSTERED 
	(
		[ProductId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

End
Go

IF (Not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'txnSourceMapwithSecurity'))
BEGIN
	CREATE TABLE [dbo].[txnSourceMapwithSecurity](
		[ssmapId] [int] IDENTITY(1,1) NOT NULL,
		[SourceId] [int] NULL,
		[SecurityID] [int] NULL,
		[orderId] [int] NULL,
		[createdBy] [varchar](50) NULL,
		[CreatedAt] [datetime] NULL,
		[LastModifiedBy] [nvarchar](50) NULL,
		[LastModifiedAt] [datetime] NULL,
	PRIMARY KEY CLUSTERED 
	(
		[ssmapId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[txnSourceMapwithSecurity]  WITH CHECK ADD FOREIGN KEY([SecurityID])
	REFERENCES [dbo].[mstSecurityMech] ([SecurityID])

	ALTER TABLE [dbo].[txnSourceMapwithSecurity]  WITH CHECK ADD FOREIGN KEY([SourceId])
	REFERENCES [dbo].[mstSource] ([SourceId])

END
Go

IF (Not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'txnSeviceLog'))
BEGIN
	CREATE TABLE [dbo].[txnSeviceLog](
		[LogId] [int] IDENTITY(1,1) NOT NULL,
		[SourceId] [int] NULL,
		[ProductId] [int] NULL,
		[PlainReq] [nvarchar](max) NULL,
		[PlainRes] [nvarchar](max) NULL,
		[EncrytedReq] [nvarchar](max) NULL,
		[EncrytedRes] [nvarchar](max) NULL,
		[Header] [nvarchar](max) NULL,
		[createdBy] [varchar](50) NULL,
		[CreatedAt] [datetime] NULL,
		[LastModifiedBy] [nvarchar](50) NULL,
		[LastModifiedAt] [datetime] NULL,
	PRIMARY KEY CLUSTERED 
	(
		[LogId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	ALTER TABLE [dbo].[txnSeviceLog]  WITH CHECK ADD FOREIGN KEY([ProductId])
	REFERENCES [dbo].[mstProduct] ([ProductId])

	ALTER TABLE [dbo].[txnSeviceLog]  WITH CHECK ADD FOREIGN KEY([SourceId])
	REFERENCES [dbo].[mstSource] ([SourceId])
END
Go

IF (Not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'tnxErrorLog'))
BEGIN
	CREATE TABLE [dbo].[tnxErrorLog](
		[ErrorLogId] [int] IDENTITY(1,1) NOT NULL,
		[LogId] [int] NULL,
		[StackMsg] [nvarchar](max) NULL,
		[createdBy] [varchar](50) NULL,
		[CreatedAt] [datetime] NULL,
		[LastModifiedBy] [nvarchar](50) NULL,
		[LastModifiedAt] [datetime] NULL,
	PRIMARY KEY CLUSTERED 
	(
		[ErrorLogId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	ALTER TABLE [dbo].[tnxErrorLog]  WITH CHECK ADD FOREIGN KEY([LogId])
	REFERENCES [dbo].[txnSeviceLog] ([LogId])

End

Alter table [dbo].[mstSource] add PrivateFilePath Nvarchar(max)
Alter table [dbo].[mstSource] add PrivateFilePassword Nvarchar(100)
Alter table [dbo].[mstSource] add PublicFilePath Nvarchar(max)
Alter table [dbo].[mstSource] add EncryptDecryptPassword Nvarchar(max)
Alter table [dbo].[mstSource] add secretkeyfortoken nvarchar(max)
Alter table [dbo].[mstSource] add secretkeyforchecksum nvarchar(max)
